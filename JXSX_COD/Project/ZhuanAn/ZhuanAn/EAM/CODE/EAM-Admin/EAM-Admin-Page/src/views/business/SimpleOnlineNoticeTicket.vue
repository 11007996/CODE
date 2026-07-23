<!--
 * @Descripttion: (上线通知单/BU_Simple_Online_Notice_Ticket)
 * @Author: (admin)
 * @Date: (2024-06-27)
-->
<template>
  <div>
    <el-form :model="queryParams" label-position="right" inline ref="queryRef" v-show="showSearch" @submit.prevent>
      <el-form-item label="业务编号" prop="ticketNo">
        <el-input v-model="queryParams.ticketNo" placeholder="请输入业务编号" />
      </el-form-item>
      <el-form-item label="流程编号" prop="processInstanceId">
        <el-input v-model="queryParams.processInstanceId" placeholder="请输入流程编号" />
      </el-form-item>
      <el-form-item label="产线" prop="lineId">
        <el-select v-model="queryParams.lineId" placeholder="请选择产线" filterable clear>
          <el-option
            v-for="item in useBasicStore().getLineDict"
            :key="item.dictValue"
            :label="item.dictLabel"
            :value="Number(item.dictValue)"></el-option>
        </el-select>
      </el-form-item>
      <el-form-item label="上线料号" prop="newPartName">
        <el-input v-model="queryParams.newPartName" placeholder="请输入上线料号" />
      </el-form-item>
      <el-form-item>
        <el-button icon="search" type="primary" @click="handleQuery">{{ $t('btn.search') }}</el-button>
        <el-button icon="refresh" @click="resetQuery">{{ $t('btn.reset') }}</el-button>
      </el-form-item>
    </el-form>
    <!-- 工具区域 -->
    <el-row :gutter="15" class="mb10">
      <!-- <el-col :span="1.5">
        <el-button type="primary" v-hasPermi="['onlineNoticeTicket:add']" plain icon="plus" @click="handleAdd">
          {{ $t('btn.add') }}
        </el-button>
      </el-col> -->
      <right-toolbar v-model:showSearch="showSearch" @queryTable="getList" :columns="columns"></right-toolbar>
    </el-row>

    <el-table
      :data="dataList"
      v-loading="loading"
      ref="table"
      border
      header-cell-class-name="el-table-header-cell"
      highlight-current-row
      @sort-change="sortChange">
      <el-table-column align="center" width="90">
        <template #default="scope">
          <el-button text @click="rowClick(scope.row)">{{ $t('btn.details') }}</el-button>
        </template>
      </el-table-column>
      <el-table-column prop="ticketNo" label="业务编号" width="160" align="center" v-if="columns.showColumn('ticketNo')" />
      <el-table-column prop="initiatorId" label="发起人ID" width="90" align="center" v-if="columns.showColumn('initiatorId')" />
      <el-table-column prop="initiatorName" label="发起人" width="90" align="center" v-if="columns.showColumn('initiatorName')" />
      <el-table-column prop="lineId" label="产线ID" width="90" align="center" v-if="columns.showColumn('lineId')" />
      <el-table-column prop="lineName" label="产线" width="90" align="center" v-if="columns.showColumn('lineName')" />
      <el-table-column prop="newPartName" label="上线料号" width="160" align="center" v-if="columns.showColumn('newPartName')" />
      <el-table-column prop="oldPartName" label="在制料号" width="160" align="center" v-if="columns.showColumn('oldPartName')" />
      <el-table-column prop="productQty" label="产量" width="90" align="center" v-if="columns.showColumn('productQty')" />
      <el-table-column prop="needTime" label="需求时间" width="160" align="center" v-if="columns.showColumn('needTime')" />
      <el-table-column prop="status" label="业务状态" width="90" align="center" v-if="columns.showColumn('status')">
        <template #default="scope">
          <dict-tag :options="options.business_status" :value="scope.row.status" />
        </template>
      </el-table-column>
      <el-table-column prop="processInstanceId" label="流程编号" min-width="200" align="center" v-if="columns.showColumn('processInstanceId')">
        <template #default="scope">
          <el-link type="primary" @click="openProcess(scope.row)">{{ scope.row.processInstanceId }}</el-link>
        </template>
      </el-table-column>
      <el-table-column prop="createBy" label="创建人" width="90" align="center" v-if="columns.showColumn('createBy')" />
      <el-table-column prop="createTime" label="创建时间" width="160" v-if="columns.showColumn('createTime')" />
      <el-table-column label="操作" width="220" fixed="right">
        <template #default="scope">
          <el-button
            type="primary"
            size="small"
            icon="view"
            title="查看"
            v-hasPermi="['onlineNoticeTicket:query']"
            @click="handlePreview(scope.row)"></el-button>
          <el-button
            type="success"
            size="small"
            icon="edit"
            title="编辑"
            v-hasPermi="['onlineNoticeTicket:edit']"
            @click="handleUpdate(scope.row)"></el-button>
          <el-button type="primary" size="small" title="结案" v-hasPermi="['onlineNoticeTicket:close']" @click="handleClose(scope.row)">
            结案
          </el-button>
          <el-button
            type="danger"
            size="small"
            icon="delete"
            title="删除"
            v-hasPermi="['onlineNoticeTicket:delete']"
            @click="handleDelete(scope.row)"></el-button>
        </template>
      </el-table-column>
    </el-table>
    <pagination :total="total" v-model:page="queryParams.pageNum" v-model:limit="queryParams.pageSize" @pagination="getList" />

    <!-- 右侧抽屉:业务详情 -->
    <el-drawer v-model="drawer" size="50%" direction="rtl">
      <el-divider content-position="center">上线通知单_需求清单信息</el-divider>
      <el-table :data="itemList" header-row-class-name="text-navy">
        <el-table-column prop="itemName" label="设备*治具" min-width="160" />
        <el-table-column prop="needQty" label="需求数量" width="90" align="center" />
        <el-table-column prop="isReady" label="准备完成" width="90" align="center">
          <template #default="scope">
            <el-switch v-model="scope.row.isReady" active-text="是" inactive-text="否" inline-prompt disabled />
          </template>
        </el-table-column>
      </el-table>
    </el-drawer>

    <!-- 添加或修改上线通知单对话框 -->
    <el-dialog :title="title" :lock-scroll="false" v-model="open" :fullscreen="fullScreen">
      <el-form ref="formRef" :model="form" :rules="rules" label-width="100px">
        <el-row :gutter="20">
          <el-col :lg="12">
            <el-form-item label="业务编号" prop="ticketNo">
              <el-input v-model="form.ticketNo" placeholder="请输入业务编号" disabled />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="发起人" prop="initiatorId">
              <el-select
                v-model="form.initiatorId"
                placeholder="请选择发起人"
                clearable
                filterable
                remote
                :remote-method="handleQueryEmployee"
                @change="handleInitiatorChange"
                class="fullWidth">
                <el-option
                  v-for="item in options.emp_options"
                  :key="item.dictValue"
                  :label="item.dictValue + ' - ' + item.dictLabel"
                  :value="item.dictValue"></el-option>
              </el-select>
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="新上线料号" prop="newPartName">
              <el-input v-model="form.newPartName" placeholder="请输入料号" class="fullWidth"> </el-input>
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="在制料号" prop="oldPartName">
              <el-input v-model="form.oldPartName" placeholder="请输入料号" class="fullWidth"> </el-input>
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="产量" prop="productQty">
              <el-input-number v-model.number="form.productQty" controls-position="right" placeholder="请输入产量" class="fullWidth" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="产线" prop="lineId">
              <el-select v-model="form.lineId" placeholder="请选择产线" clearable filterable class="fullWidth">
                <el-option
                  v-for="item in useBasicStore().getLineDict"
                  :key="item.dictValue"
                  :label="item.dictLabel"
                  :value="Number(item.dictValue)"></el-option>
              </el-select>
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="需求时间" prop="needTime">
              <el-date-picker v-model="form.needTime" type="datetime" placeholder="选择日期时间" class="fullWidth"></el-date-picker>
            </el-form-item>
          </el-col>
        </el-row>

        <!-- 全屏 -->
        <el-row :gutter="10" class="mb8">
          <el-col :span="1.5">
            <el-button type="info" icon="FullScreen" @click="fullScreen = !fullScreen">{{ fullScreen ? '退出全屏' : '全屏' }}</el-button>
          </el-col>
        </el-row>

        <!-- 设备清单 -->
        <el-divider content-position="center">上线通知单_需求清单信息</el-divider>
        <el-row :gutter="10" class="mb8">
          <el-col :span="1.5" v-if="opertype != 3">
            <el-button type="primary" icon="Plus" @click="handleAddItem">添加</el-button>
          </el-col>
          <el-col :span="1.5" v-if="opertype != 3">
            <el-button type="danger" icon="Delete" @click="handleDeleteItem">删除</el-button>
          </el-col>
        </el-row>
        <el-table :data="itemList" :row-class-name="rowItemIndex" @selection-change="handleItemSelectionChange" ref="ItemRef">
          <el-table-column type="selection" width="40" align="center" />
          <el-table-column label="设备*治具名称" min-width="160" align="center" prop="itemName">
            <template #default="scope">
              <el-input v-model="scope.row.itemName" placeholder="请输入设备*治具名称" clearable class="fullWidth"> </el-input>
            </template>
          </el-table-column>
          <el-table-column label="需求数量" width="90" align="center" prop="needQty">
            <template #default="scope">
              <el-input-number v-model="scope.row.needQty" :controls="false" placeholder="请输入需求数量" class="fullWidth" />
            </template>
          </el-table-column>

          <el-table-column label="准备完成" width="90" align="center" prop="isReady">
            <template #default="scope">
              <el-switch v-model="scope.row.isReady" active-text="是" inactive-text="否" inline-prompt />
            </template>
          </el-table-column>
        </el-table>
      </el-form>

      <template #footer v-if="opertype != 3">
        <el-button text @click="cancel">{{ $t('btn.cancel') }}</el-button>
        <el-button type="primary" @click="submitForm">{{ $t('btn.submit') }}</el-button>
      </template>
    </el-dialog>
  </div>
</template>

<script setup name="simpleonlinenoticeticket">
import {
  listSimpleOnlineNoticeTicket,
  addSimpleOnlineNoticeTicket,
  delSimpleOnlineNoticeTicket,
  updateSimpleOnlineNoticeTicket,
  getSimpleOnlineNoticeTicket,
  closeSimpleOnlineNoticeTicket
} from '@/api/business/simpleOnlineNoticeTicket.js'
import useBasicStore from '@/store/modules/basic.js'
import { dictEmployee } from '@/api/basic/employee.js'

const { proxy } = getCurrentInstance()
const router = useRouter()
const ids = ref([])
const loading = ref(false)
const showSearch = ref(true)
const queryParams = reactive({
  pageNum: 1,
  pageSize: 10,
  sort: 'createtime',
  sortType: 'desc',
  ticketNo: undefined,
  processInstanceId: undefined,
  lineId: undefined,
  newPartName: undefined
})
const columns = ref([
  { visible: true, prop: 'ticketNo', label: '业务编号' },
  { visible: false, prop: 'initiatorId', label: '发起人ID' },
  { visible: true, prop: 'initiatorName', label: '发起人' },
  { visible: false, prop: 'lineId', label: '产线ID' },
  { visible: true, prop: 'lineName', label: '产线' },
  { visible: true, prop: 'newPartName', label: '上线料号' },
  { visible: false, prop: 'oldPartName', label: '在制料号' },
  { visible: false, prop: 'productQty', label: '产量' },
  { visible: true, prop: 'needTime', label: '需求时间' },
  { visible: true, prop: 'status', label: '业务状态' },
  { visible: true, prop: 'processInstanceId', label: '流程编号' },
  { visible: false, prop: 'createBy', label: '创建人' },
  { visible: false, prop: 'createTime', label: '创建时间' }
])
const total = ref(0)
const dataList = ref([])
const queryRef = ref()
const defaultTime = ref([new Date(2000, 1, 1, 0, 0, 0), new Date(2000, 2, 1, 23, 59, 59)])
const fullScreen = ref(false)
const drawer = ref(false)

var dictParams = [{ dictType: 'business_status' }]
//获取字典数据
proxy.getDicts(dictParams).then((response) => {
  response.data.forEach((element) => {
    state.options[element.dictType] = element.list
  })
})

function getList() {
  loading.value = true
  listSimpleOnlineNoticeTicket(queryParams).then((res) => {
    const { code, data } = res
    if (code == 200) {
      dataList.value = data.result
      total.value = data.totalNum
      loading.value = false
    }
  })
}

// 查询
function handleQuery() {
  queryParams.pageNum = 1
  getList()
}

// 重置查询操作
function resetQuery() {
  proxy.resetForm('queryRef')
  handleQuery()
}
// 自定义排序
function sortChange(column) {
  var sort = undefined
  var sortType = undefined

  if (column.prop != null && column.order != null) {
    sort = column.prop
    sortType = column.order
  }
  queryParams.sort = sort
  queryParams.sortType = sortType
  handleQuery()
}

//查看流程
function openProcess(row) {
  const instanceId = row.processInstanceId
  const processId = row.processInstanceId.split('-')[0]
  //使用resolve
  const url = router.resolve({
    path: '/process/' + processId,
    query: { instanceId: instanceId }
  })
  window.open(url.href, '_blank')
}
/*************** form操作 ***************/
const formRef = ref()
const title = ref('')
// 操作类型 1、add 2、edit 3、view
const opertype = ref(0)
const open = ref(false)
const state = reactive({
  single: true,
  multiple: true,
  form: {},
  rules: {
    // ticketNo: [{ required: true, message: '业务编号不能为空', trigger: 'blur' }],
    initiatorId: [{ required: true, message: '发起人不能为空', trigger: 'blur' }],
    newPartName: [{ required: true, message: '新上线料号不能为空', trigger: 'blur' }],
    lineId: [{ required: true, message: '产线不能为空', trigger: 'blur' }],
    productQty: [{ required: true, message: '产量不能为空', trigger: 'blur' }]
  },
  options: {
    // 业务状态 选项列表 格式 eg:{ dictLabel: '标签', dictValue: '0'}
    business_status: [],
    //员工选项
    emp_options: []
  }
})

const { form, rules, options, single, multiple } = toRefs(state)

// 关闭dialog
function cancel() {
  open.value = false
  reset()
}

// 重置表单
function reset() {
  form.value = {
    ticketNo: null,
    initiatorId: null,
    initiatorName: null,
    newPartName: null,
    oldPartName: null,
    productQty: null,
    lineId: null,
    needTime: null,
    status: null,
    processInstanceId: null,
    createBy: null,
    createTime: null
  }
  itemList.value = []
  proxy.resetForm('formRef')
}

// 添加按钮操作
function handleAdd() {
  reset()
  open.value = true
  title.value = '添加上线通知单'
  opertype.value = 1
}
// 修改按钮操作
function handleUpdate(row) {
  reset()
  const id = row.ticketNo || ids.value
  getSimpleOnlineNoticeTicket(id).then((res) => {
    const { code, data } = res
    if (code == 200) {
      open.value = true
      title.value = '修改上线通知单'
      opertype.value = 2

      form.value = {
        ...data
      }

      //设备清单
      itemList.value = res.data.itemNav
      //审批人
      state.options.emp_options = []
      state.options.emp_options.push({ dictValue: res.data.initiatorId, dictLabel: res.data.initiatorName })
    }
  })
}

// 修改按钮操作
function handlePreview(row) {
  reset()
  const id = row.ticketNo || ids.value
  getSimpleOnlineNoticeTicket(id).then((res) => {
    const { code, data } = res
    if (code == 200) {
      open.value = true
      title.value = '查看上线通知单'
      opertype.value = 3

      form.value = { ...data }
      //设备清单
      itemList.value = res.data.itemNav
      //审批人
      state.options.emp_options = []
      state.options.emp_options.push({ dictValue: res.data.initiatorId, dictLabel: res.data.initiatorName })
    }
  })
}

// 添加&修改 表单提交
function submitForm() {
  proxy.$refs['formRef'].validate((valid) => {
    if (valid) {
      form.value.ItemNav = itemList.value
      if (form.value.ticketNo != undefined && opertype.value === 2) {
        updateSimpleOnlineNoticeTicket(form.value).then((res) => {
          proxy.$modal.msgSuccess('修改成功')
          open.value = false
          getList()
        })
      } else {
        addSimpleOnlineNoticeTicket(form.value).then((res) => {
          proxy.$modal.msgSuccess('新增成功')
          open.value = false
          getList()
        })
      }
    }
  })
}

// 删除按钮操作
function handleDelete(row) {
  const Ids = row.ticketNo || ids.value

  proxy
    .$confirm('是否确认删除参数编号为"' + Ids + '"的数据项？', '警告', {
      confirmButtonText: proxy.$t('common.ok'),
      cancelButtonText: proxy.$t('common.cancel'),
      type: 'warning'
    })
    .then(function () {
      return delSimpleOnlineNoticeTicket(Ids)
    })
    .then(() => {
      getList()
      proxy.$modal.msgSuccess('删除成功')
    })
}

//结案处理
function handleClose(row) {
  const Ids = row.ticketNo || ids.value

  proxy
    .$confirm('是否确认结案编号为"' + Ids + '"的数据项？', '警告', {
      confirmButtonText: proxy.$t('common.ok'),
      cancelButtonText: proxy.$t('common.cancel'),
      type: 'warning'
    })
    .then(function () {
      return closeSimpleOnlineNoticeTicket(Ids)
    })
    .then(() => {
      getList()
      proxy.$modal.msgSuccess('结案成功')
    })
}

/** 行单击事件 */
function rowClick(row) {
  const id = row.ticketNo || ids.value
  getSimpleOnlineNoticeTicket(id).then((res) => {
    const { code, data } = res
    if (code == 200) {
      drawer.value = true
      itemList.value = data.itemNav
    }
  })
}

/*********************上线通知单_需求清单子表信息*************************/
const itemList = ref([])
const checkedItem = ref([])

/** 上线通知单_需求清单序号 */
function rowItemIndex({ row, rowIndex }) {
  row.index = rowIndex + 1
}

/** 上线通知单_需求清单添加按钮操作 */
function handleAddItem() {
  let obj = {}
  itemList.value.push(obj)
}

/** 复选框选中数据 */
function handleItemSelectionChange(selection) {
  checkedItem.value = selection.map((item) => item.index)
}

/** 上线通知单_需求清单删除按钮操作 */
function handleDeleteItem() {
  if (checkedItem.value.length == 0) {
    proxy.$modal.msgError('请先选择要删除的上线通知单_需求清单数据')
  } else {
    itemList.value = itemList.value.filter(function (item) {
      return checkedItem.value.indexOf(item.index) == -1
    })
  }
}

//********************************其他方法************************* */
//员工查询
function handleQueryEmployee(keyword) {
  if (keyword) {
    const query = {
      pageNum: 1,
      pageSize: 10,
      sort: '',
      sortType: 'asc',
      empName: keyword
    }
    setTimeout(() => {
      dictEmployee(query).then((res) => {
        state.options.emp_options = res.data.result
      })
    }, 200)
  }
}

//发起人变更
function handleInitiatorChange(value) {
  // 在这里根据value查找对应的label
  const selectedOption = state.options.emp_options.find((item) => item.dictValue === value)
  if (selectedOption) {
    form.value.initiatorName = selectedOption.dictLabel // 更新显示的标签
  }
}

handleQuery()
</script>
