<!--
 * @Descripttion: (耗品领用单/BU_Consumable_Receive_Ticket)
 * @Author: (admin)
 * @Date: (2024-07-05)
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
      <el-form-item label="发起人" prop="initiatorId">
        <el-select
          v-model="queryParams.initiatorId"
          placeholder="请选择发起人"
          clearable
          filterable
          remote
          :remote-method="handleQueryEmployee"
          @change="handleInitiatorChange">
          <el-option
            v-for="item in options.emp_options"
            :key="item.dictValue"
            :label="item.dictValue + ' - ' + item.dictLabel"
            :value="item.dictValue"></el-option>
        </el-select>
      </el-form-item>
      <el-form-item label="线别ID" prop="lineId">
        <el-select v-model="queryParams.lineId" clearable filterable placeholder="请选择线别ID">
          <el-option v-for="item in useBasicStore().getLineDict" :key="item.dictValue" :label="item.dictLabel" :value="item.dictValue"></el-option>
        </el-select>
      </el-form-item>
      <el-form-item>
        <el-button icon="search" type="primary" @click="handleQuery">{{ $t('btn.search') }}</el-button>
        <el-button icon="refresh" @click="resetQuery">{{ $t('btn.reset') }}</el-button>
      </el-form-item>
    </el-form>
    <!-- 工具区域 -->
    <el-row :gutter="15" class="mb10">
      <el-col :span="1.5">
        <el-button type="primary" v-hasPermi="['consumableReceiveTicket:add']" plain icon="plus" @click="handleAdd">
          {{ $t('btn.add') }}
        </el-button>
      </el-col>
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
      <el-table-column
        prop="ticketNo"
        label="业务编号"
        width="160"
        align="center"
        :show-overflow-tooltip="true"
        v-if="columns.showColumn('ticketNo')" />
      <el-table-column prop="initiatorId" label="发起人ID" width="90" align="center" v-if="columns.showColumn('initiatorId')" />
      <el-table-column
        prop="initiatorName"
        label="发起人"
        width="90"
        align="center"
        :show-overflow-tooltip="true"
        v-if="columns.showColumn('initiatorName')" />
      <el-table-column prop="lineId" label="线别ID" width="90" align="center" v-if="columns.showColumn('lineId')" />
      <el-table-column
        prop="needDate"
        label="需求时间"
        width="160"
        align="center"
        :show-overflow-tooltip="true"
        v-if="columns.showColumn('needDate')" />
      <el-table-column prop="purpose" label="用途" align="center" :show-overflow-tooltip="true" v-if="columns.showColumn('purpose')" />
      <el-table-column prop="status" label="业务状态" width="90" align="center" v-if="columns.showColumn('status')">
        <template #default="scope">
          <dict-tag :options="options.status_options" :value="scope.row.status" />
        </template>
      </el-table-column>
      <el-table-column prop="processInstanceId" label="流程编号" width="200" align="center" v-if="columns.showColumn('processInstanceId')">
        <template #default="scope">
          <el-link type="primary" @click="openProcess(scope.row)">{{ scope.row.processInstanceId }}</el-link>
        </template>
      </el-table-column>
      <el-table-column
        prop="createBy"
        label="创建人"
        width="90"
        align="center"
        :show-overflow-tooltip="true"
        v-if="columns.showColumn('createBy')" />
      <el-table-column prop="createTime" label="创建时间" width="160" :show-overflow-tooltip="true" v-if="columns.showColumn('createTime')" />
      <el-table-column prop="remark" label="备注" align="center" :show-overflow-tooltip="true" v-if="columns.showColumn('remark')" />
      <el-table-column label="操作" width="250">
        <template #default="scope">
          <el-button
            type="primary"
            size="small"
            icon="view"
            title="查看"
            v-hasPermi="['consumableReceiveTicket:query']"
            @click="handlePreview(scope.row)"></el-button>
          <el-button
            type="success"
            size="small"
            icon="edit"
            title="编辑"
            v-hasPermi="['consumableReceiveTicket:edit']"
            @click="handleUpdate(scope.row)"></el-button>
          <el-button
            type="danger"
            size="small"
            icon="delete"
            title="删除"
            v-hasPermi="['consumableReceiveTicket:delete']"
            @click="handleDelete(scope.row)"></el-button>

          <el-dropdown
            @command="(command) => handleCommand(command, scope.row)"
            v-hasPermi="['consumable:storage:receive', 'consumable:storage:back', 'consumable:storage:list']">
            <el-button type="warning" size="small">
              耗品<el-icon class="el-icon--right"><arrow-down /></el-icon>
            </el-button>
            <template #dropdown>
              <el-dropdown-menu>
                <div v-hasPermi="['consumable:storage:receive']"><el-dropdown-item command="openReceivePage">领用</el-dropdown-item></div>
                <div v-hasPermi="['consumable:storage:back']"><el-dropdown-item command="openBackPage">归还</el-dropdown-item></div>
                <div v-hasPermi="['consumable:storage:list']"><el-dropdown-item command="openRecordPage">记录</el-dropdown-item></div>
              </el-dropdown-menu>
            </template>
          </el-dropdown>
        </template>
      </el-table-column>
    </el-table>
    <pagination :total="total" v-model:page="queryParams.pageNum" v-model:limit="queryParams.pageSize" @pagination="getList" />

    <el-drawer v-model="drawer" size="50%" direction="rtl">
      <el-table :data="consumableList" header-row-class-name="text-navy">
        <el-table-column prop="consumableName" label="耗品名称" min-width="120" />
        <el-table-column prop="consumablePart" label="料号" min-width="120" />
        <el-table-column prop="spec" label="规格" min-width="120" />
        <el-table-column prop="price" label="单价" width="90" />
        <el-table-column prop="needQty" label="需求数量" width="90" align="center" />
      </el-table>
    </el-drawer>

    <!-- 添加或修改耗品领用单对话框 -->
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
            <el-form-item label="线别ID" prop="lineId">
              <el-select v-model="form.lineId" placeholder="请选择线别ID" clearable filterable class="fullWidth">
                <el-option
                  v-for="item in useBasicStore().getLineDict"
                  :key="item.dictValue"
                  :label="item.dictLabel"
                  :value="Number(item.dictValue)"></el-option>
              </el-select>
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="需求时间" prop="needDate">
              <el-date-picker
                v-model="form.needDate"
                type="datetime"
                :teleported="false"
                placeholder="选择日期时间"
                class="fullWidth"></el-date-picker>
            </el-form-item>
          </el-col>

          <el-col :lg="24">
            <el-form-item label="用途" prop="purpose">
              <el-input v-model="form.purpose" placeholder="请输入用途" />
            </el-form-item>
          </el-col>

          <el-col :lg="24">
            <el-form-item label="备注" prop="remark">
              <el-input type="textarea" v-model="form.remark" placeholder="请输入备注" />
            </el-form-item>
          </el-col>
        </el-row>
        <el-divider content-position="center">耗品领用单_需求清单信息</el-divider>
        <el-row :gutter="10" class="mb8">
          <el-col :span="1.5">
            <el-button type="primary" icon="Plus" @click="handleAddConsumable">添加</el-button>
          </el-col>
          <el-col :span="1.5">
            <el-button type="danger" icon="Delete" @click="handleDeleteConsumable">删除</el-button>
          </el-col>
          <el-col :span="1.5">
            <el-button type="info" icon="FullScreen" @click="fullScreen = !fullScreen">{{ fullScreen ? '退出全屏' : '全屏' }}</el-button>
          </el-col>
        </el-row>
        <el-table
          :data="consumableList"
          :row-class-name="rowConsumableIndex"
          @selection-change="handleConsumableSelectionChange"
          ref="ConsumableRef">
          <el-table-column type="selection" width="40" align="center" />
          <el-table-column label="耗品名称" min-width="160" align="center" prop="consumableName">
            <template #default="scope">
              <el-input v-model="scope.row.consumableName" clearable placeholder="请选择耗品" @focus="handleQueryConsumable(scope.row)" />
            </template>
          </el-table-column>
          <el-table-column label="料号" prop="consumablePart" min-width="120" />
          <el-table-column label="规格" prop="spec" min-width="120" />
          <el-table-column label="单价" prop="price" width="90" />
          <el-table-column label="需求数量" width="90" align="center" prop="needQty">
            <template #default="scope">
              <el-input-number v-model="scope.row.needQty" :controls="false" class="fullWidth" />
            </template>
          </el-table-column>
        </el-table>
      </el-form>
      <template #footer v-if="opertype != 3">
        <el-button text @click="cancel">{{ $t('btn.cancel') }}</el-button>
        <el-button type="primary" @click="submitForm">{{ $t('btn.submit') }}</el-button>
      </template>
    </el-dialog>

    <!-- 耗品选择弹窗 -->
    <ConsumableSelectorForIdle v-model:visible="consumableSelectorVisible" @confirm="handleConsumableSelect"></ConsumableSelectorForIdle>

    <el-dialog :title="title" :lock-scroll="false" v-model="openOper" :fullscreen="fullScreen" width="90%">
      <!-- 领用弹窗 -->
      <ConsumableReceiveTicketReceive :ticketNo="currTicketNo" v-if="opertype == 4"></ConsumableReceiveTicketReceive>
      <!-- 归还弹窗 -->
      <ConsumableReceiveTicketBack :ticketNo="currTicketNo" v-if="opertype == 5"></ConsumableReceiveTicketBack>
      <!-- 记录弹窗 -->
      <ConsumableStorageRecord :ticketNo="currTicketNo" v-if="opertype == 6"></ConsumableStorageRecord>
    </el-dialog>
  </div>
</template>

<script setup name="consumablereceiveticket">
import {
  listConsumableReceiveTicket,
  addConsumableReceiveTicket,
  delConsumableReceiveTicket,
  updateConsumableReceiveTicket,
  getConsumableReceiveTicket
} from '@/api/business/consumableReceiveTicket.js'
import { dictEmployee } from '@/api/basic/employee.js'
import useBasicStore from '@/store/modules/basic.js'
import ConsumableReceiveTicketReceive from './ConsumableReceiveTicketReceive.vue'
import ConsumableReceiveTicketBack from './ConsumableReceiveTicketBack.vue'
import ConsumableStorageRecord from '../consumable/ConsumableStorageRecord.vue'
import ConsumableSelectorForIdle from '@/views/components/DataSelector/ConsumableSelectorForIdle.vue'

const { proxy } = getCurrentInstance()
const router = useRouter()
const ids = ref([])
const loading = ref(false)
const showSearch = ref(true)
const queryParams = reactive({
  pageNum: 1,
  pageSize: 10,
  sort: 'create_time',
  sortType: 'desc',
  ticketNo: undefined,
  processInstanceId: undefined,
  initiatorId: undefined,
  lineId: undefined
})
const columns = ref([
  { visible: true, prop: 'ticketNo', label: '业务编号' },
  { visible: false, prop: 'initiatorId', label: '发起人ID' },
  { visible: true, prop: 'initiatorName', label: '发起人姓名' },
  { visible: true, prop: 'lineId', label: '线别ID' },
  { visible: true, prop: 'needDate', label: '需求时间' },
  { visible: true, prop: 'purpose', label: '用途' },
  { visible: false, prop: 'status', label: '业务状态' },
  { visible: true, prop: 'processInstanceId', label: '流程编号' },
  { visible: false, prop: 'createBy', label: '创建人' },
  { visible: false, prop: 'createTime', label: '创建时间' },
  { visible: false, prop: 'remark', label: '备注' }
])
const total = ref(0)
const dataList = ref([])
const storageRecord = ref([])
const queryRef = ref()
const defaultTime = ref([new Date(2000, 1, 1, 0, 0, 0), new Date(2000, 2, 1, 23, 59, 59)])

var dictParams = [{ dictType: 'storage_change_type' }]
//获取字典数据
proxy.getDicts(dictParams).then((response) => {
  response.data.forEach((element) => {
    state.options[element.dictType] = element.list
  })
})

function getList() {
  loading.value = true
  listConsumableReceiveTicket(queryParams).then((res) => {
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
const openOper = ref(false)
const currTicketNo = ref('')
const state = reactive({
  single: true,
  multiple: true,
  form: {},
  rules: {
    initiatorId: [{ required: true, message: '发起人不能为空', trigger: 'change' }],
    lineId: [{ required: true, message: '线别ID不能为空', trigger: 'change' }]
  },
  options: {
    //库存变动类型
    storage_change_type: [],
    // 发起人 选项列表 格式 eg:{ dictLabel: '标签', dictValue: '0'}
    emp_options: [],
    //业务状态
    status_options: []
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
    purpose: null,
    lineId: null,
    needDate: null,
    processInstanceId: null,
    status: null,
    delFlag: null,
    createBy: null,
    createTime: null,
    remark: null
  }
  consumableList.value = []
  proxy.resetForm('formRef')
}

// 添加按钮操作
function handleAdd() {
  reset()
  open.value = true
  title.value = '添加耗品领用单'
  opertype.value = 1
}

// 修改按钮操作
function handlePreview(row) {
  reset()
  const id = row.ticketNo || ids.value
  getConsumableReceiveTicket(id).then((res) => {
    const { code, data } = res
    if (code == 200) {
      open.value = true
      title.value = '查看耗品领用单'
      opertype.value = 3

      form.value = {
        ...data
      }
      consumableList.value = res.data.consumableNav

      //初始 用户
      state.options.emp_options = []
      state.options.emp_options.push({ dictValue: res.data.initiatorId, dictLabel: res.data.initiatorName })
    }
  })
}

// 修改按钮操作
function handleUpdate(row) {
  reset()
  const id = row.ticketNo || ids.value
  getConsumableReceiveTicket(id).then((res) => {
    const { code, data } = res
    if (code == 200) {
      open.value = true
      title.value = '修改耗品领用单'
      opertype.value = 2

      form.value = {
        ...data
      }
      consumableList.value = res.data.consumableNav

      //初始 用户
      state.options.emp_options = []
      state.options.emp_options.push({ dictValue: res.data.initiatorId, dictLabel: res.data.initiatorName })
    }
  })
}

// 添加&修改 表单提交
function submitForm() {
  proxy.$refs['formRef'].validate((valid) => {
    if (valid) {
      form.value.consumableNav = consumableList.value
      if (form.value.ticketNo != undefined && opertype.value === 2) {
        updateConsumableReceiveTicket(form.value).then((res) => {
          proxy.$modal.msgSuccess('修改成功')
          open.value = false
          getList()
        })
      } else {
        addConsumableReceiveTicket(form.value).then((res) => {
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
      return delConsumableReceiveTicket(Ids)
    })
    .then(() => {
      getList()
      proxy.$modal.msgSuccess('删除成功')
    })
}

/*********************耗品领用单_需求清单子表信息*************************/
const consumableList = ref([])
const checkedConsumable = ref([])
const fullScreen = ref(false)
const drawer = ref(false)
const consumableSelectorVisible = ref(false)
const consumableSelectorRowIndex = ref(0)

/** 上线通知单_治具需求清单治具查询 */
function handleQueryConsumable(row) {
  consumableSelectorRowIndex.value = row.index
  consumableSelectorVisible.value = true
}

//选择治具
const handleConsumableSelect = (consumable) => {
  consumableList.value[consumableSelectorRowIndex.value - 1].consumableId = consumable.consumableId
  consumableList.value[consumableSelectorRowIndex.value - 1].consumableName = consumable.consumableName
  consumableList.value[consumableSelectorRowIndex.value - 1].consumablePart = consumable.consumablePart
  consumableList.value[consumableSelectorRowIndex.value - 1].spec = consumable.spec
  consumableList.value[consumableSelectorRowIndex.value - 1].price = consumable.price
  consumableList.value[consumableSelectorRowIndex.value - 1].needQty = 1
}

/** 耗品领用单_需求清单序号 */
function rowConsumableIndex({ row, rowIndex }) {
  row.index = rowIndex + 1
}

/** 耗品领用单_需求清单添加按钮操作 */
function handleAddConsumable() {
  let obj = {}
  //下面的代码自己设置默认值
  //obj.consumableName = null;
  //obj.needQty = null;
  consumableList.value.push(obj)
}

/** 复选框选中数据 */
function handleConsumableSelectionChange(selection) {
  checkedConsumable.value = selection.map((item) => item.index)
}

/** 耗品领用单_需求清单删除按钮操作 */
function handleDeleteConsumable() {
  if (checkedConsumable.value.length == 0) {
    proxy.$modal.msgError('请先选择要删除的耗品领用单_需求清单数据')
  } else {
    consumableList.value = consumableList.value.filter(function (item) {
      return checkedConsumable.value.indexOf(item.index) == -1
    })
  }
}

/** 耗品领用单_需求清单详情 */
function rowClick(row) {
  const id = row.ticketNo || ids.value
  getConsumableReceiveTicket(id).then((res) => {
    const { code, data } = res
    if (code == 200) {
      drawer.value = true
      consumableList.value = data.consumableNav
    }
  })
}

//********************************其他方法************************* */
//员工查询
function handleQueryEmployee(keyword) {
  if (keyword) {
    const query = {
      pageNum: 1,
      pageSize: 10,
      sort: 'create_time',
      sortType: 'desc',
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

// 更多操作触发
function handleCommand(command, row) {
  switch (command) {
    case 'openReceivePage':
      openReceivePage(row)
      break
    case 'openBackPage':
      openBackPage(row)
      break
    case 'openRecordPage':
      openRecordPage(row)
      break
    default:
      break
  }
}

//领用
function openReceivePage(row) {
  currTicketNo.value = row.ticketNo
  title.value = '耗品领用单_需求清单'
  opertype.value = 4
  openOper.value = true
}
//归还
function openBackPage(row) {
  currTicketNo.value = row.ticketNo
  title.value = '耗品领用单_可还清单'
  opertype.value = 5
  openOper.value = true
}
//记录
function openRecordPage(row) {
  currTicketNo.value = row.ticketNo
  title.value = '耗品领用单_操作记录'
  opertype.value = 6
  openOper.value = true
}

//单元格式化
function formatter(row, cloumn) {
  return row.consumablePart + ' / ' + row.consumableName + ' / ' + row.spec
}

handleQuery()
</script>
