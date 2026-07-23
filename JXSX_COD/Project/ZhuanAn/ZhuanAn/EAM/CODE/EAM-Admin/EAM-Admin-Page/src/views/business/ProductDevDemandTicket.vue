<!--
 * @Descripttion: (产品开发需求单/BU_Product_Dev_Demand_Ticket)
 * @Author: (admin)
 * @Date: (2024-07-16)
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
      <el-form-item>
        <el-button icon="search" type="primary" @click="handleQuery">{{ $t('btn.search') }}</el-button>
        <el-button icon="refresh" @click="resetQuery">{{ $t('btn.reset') }}</el-button>
      </el-form-item>
    </el-form>
    <!-- 工具区域 -->
    <el-row :gutter="15" class="mb10">
      <!-- <el-col :span="1.5">
        <el-button type="primary" v-hasPermi="['productDevDemandTicket:add']" plain icon="plus" @click="handleAdd">
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
      <el-table-column
        prop="ticketNo"
        label="业务编号"
        width="160"
        align="center"
        :show-overflow-tooltip="true"
        v-if="columns.showColumn('ticketNo')" />
      <el-table-column
        prop="initiatorName"
        label="发起人"
        width="90"
        align="center"
        :show-overflow-tooltip="true"
        v-if="columns.showColumn('initiatorId')" />
      <el-table-column
        prop="customId"
        label="客户"
        min-width="90"
        align="center"
        :show-overflow-tooltip="true"
        v-if="columns.showColumn('customId')" />
      <el-table-column
        prop="partId"
        label="料号"
        min-width="160"
        align="center"
        :show-overflow-tooltip="true"
        v-if="columns.showColumn('partId')" />
      <el-table-column prop="orderQty" label="预订单量" width="90" align="center" v-if="columns.showColumn('orderQty')" />
      <el-table-column
        prop="needDate"
        label="需求时间"
        width="160"
        align="center"
        :show-overflow-tooltip="true"
        v-if="columns.showColumn('needDate')" />
      <el-table-column prop="processInstanceId" label="流程编号" width="200" align="center" v-if="columns.showColumn('processInstanceId')">
        <template #default="scope">
          <el-link type="primary" @click="openProcess(scope.row)">{{ scope.row.processInstanceId }}</el-link>
        </template>
      </el-table-column>
      <el-table-column prop="status" label="业务状态" width="90" align="center" v-if="columns.showColumn('status')">
        <template #default="scope">
          <dict-tag :options="options.status_options" :value="scope.row.status" />
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
      <el-table-column label="操作" width="170">
        <template #default="scope">
          <el-button type="primary" size="small" title="同步产品料号及延用关联" @click="handleAsync(scope.row)">同步</el-button>
          <el-button
            type="primary"
            size="small"
            icon="view"
            title="查看"
            v-hasPermi="['productDevDemandTicket:query']"
            @click="handlePreview(scope.row)"></el-button>
          <!-- <el-button
            type="success"
            size="small"
            icon="edit"
            title="编辑"
            v-hasPermi="['productDevDemandTicket:edit']"
            @click="handleUpdate(scope.row)"></el-button>-->
          <el-button
            type="danger"
            size="small"
            icon="delete"
            title="删除"
            v-hasPermi="['productDevDemandTicket:delete']"
            @click="handleDelete(scope.row)"></el-button>
        </template>
      </el-table-column>
    </el-table>
    <pagination :total="total" v-model:page="queryParams.pageNum" v-model:limit="queryParams.pageSize" @pagination="getList" />

    <el-drawer v-model="drawer" size="50%" direction="rtl">
      <el-table :data="productDevDemandTicketItemList" header-row-class-name="text-navy">
        <el-table-column label="序号" type="index" width="60" />
        <el-table-column prop="processName" label="制程名称" min-width="120" />
        <el-table-column prop="standardSpec" label="标准规格" min-width="120" />
        <el-table-column prop="equipmentType" label="器材类型" width="90" align="center">
          <template #default="scope">
            <dict-tag :options="options.equipment_type" :value="scope.row.equipmentType" />
          </template>
        </el-table-column>
        <el-table-column prop="caption" label="说明" min-width="90" />
        <el-table-column prop="qty" label="数量" width="90" align="center" />
        <el-table-column prop="devMode" label="开发方式" width="90" align="center">
          <template #default="scope">
            <dict-tag :options="options.equipment_dev_mode" :value="scope.row.devMode" />
          </template>
        </el-table-column>
      </el-table>
    </el-drawer>

    <!-- 添加或修改产品开发需求单对话框 -->
    <el-dialog :title="title" :lock-scroll="false" v-model="open" :fullscreen="fullScreen">
      <el-row :gutter="10" class="mb8">
        <el-col :span="1.5">
          <el-button type="info" icon="FullScreen" @click="fullScreen = !fullScreen">{{ fullScreen ? '退出全屏' : '全屏' }}</el-button>
        </el-col>
      </el-row>
      <el-form ref="formRef" disabled :model="form" :rules="rules" label-width="100px">
        <el-row :gutter="20">
          <el-col :lg="12">
            <el-form-item label="业务编号" prop="ticketNo">
              <el-input v-model="form.ticketNo" placeholder="请输入业务编号" :disabled="opertype != 1" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="发起人姓名" prop="initiatorName">
              <el-input v-model="form.initiatorName" placeholder="请输入发起人姓名" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="客户ID" prop="customId">
              <el-input v-model="form.customId" placeholder="请输入客户ID" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="料号" prop="partId">
              <el-input v-model="form.partId" placeholder="请输入料号" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="预订单量" prop="orderQty">
              <el-input-number v-model.number="form.orderQty" :controls="true" controls-position="right" placeholder="请输入预订单量" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="需求时间" prop="needDate">
              <el-date-picker v-model="form.needDate" type="datetime" :teleported="false" placeholder="选择日期时间"></el-date-picker>
            </el-form-item>
          </el-col>
        </el-row>

        <el-divider content-position="center">产品开发需求单_需求清单信息</el-divider>

        <el-table :data="productDevDemandTicketItemList" :row-class-name="rowProductDevDemandTicketItemIndex" ref="ProductDevDemandTicketItemRef">
          <el-table-column label="序号" align="center" prop="index" width="50" />
          <el-table-column label="制程名称" align="center" prop="processName" min-width="160" />
          <el-table-column label="标准规格" align="center" prop="standardSpec" min-width="90" />
          <el-table-column label="器材类型" align="center" prop="equipmentType" width="90">
            <template #default="scope">
              <dict-tag :options="options.equipment_type" :value="scope.row.equipmentType" />
            </template>
          </el-table-column>
          <el-table-column label="说明" align="center" prop="caption" min-width="90" />
          <el-table-column label="数量" align="center" prop="qty" width="90" />
          <el-table-column label="开发方式" prop="devMode" width="90">
            <template #default="scope">
              <dict-tag :options="options.equipment_dev_mode" :value="scope.row.devMode" />
            </template>
          </el-table-column>
          <el-table-column label="延用目标" align="center" prop="extendTargetDesc" min-width="90" />
          <el-table-column label="附件" align="center" prop="fileList" min-width="250">
            <template #default="scope">
              <el-upload :on-preview="handleFilePreview" multiple :file-list="JSON.parse(scope.row.fileList)" :disabled="true"> </el-upload>
            </template>
          </el-table-column>
        </el-table>

        <el-divider content-position="center">产品开发需求单_流程审批信息</el-divider>
        <el-row>
          <el-col :lg="12">
            <el-form-item label="流程编号" prop="processInstanceId">
              <el-input v-model="form.processInstanceId" placeholder="请输入流程编号" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="工程师" prop="engineerName">
              <el-input v-model="form.engineerName" placeholder="请输入工程师" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="工程师主管" prop="engineerLeaderName">
              <el-input v-model="form.engineerLeaderName" placeholder="请输入工程师主管" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="上级领导" prop="leaderName">
              <el-input v-model="form.leaderName" placeholder="请输入上级领导" />
            </el-form-item>
          </el-col>
        </el-row>
      </el-form>

      <template #footer v-if="opertype != 3">
        <el-button text @click="cancel">{{ $t('btn.cancel') }}</el-button>
        <el-button type="primary" @click="submitForm">{{ $t('btn.submit') }}</el-button>
      </template>
    </el-dialog>
  </div>
</template>

<script setup name="productdevdemandticket">
import {
  listProductDevDemandTicket,
  addProductDevDemandTicket,
  delProductDevDemandTicket,
  updateProductDevDemandTicket,
  asyncProductDevDemandTicket,
  getProductDevDemandTicket
} from '@/api/business/productDevDemandTicket.js'
const { proxy } = getCurrentInstance()
const router = useRouter()
const ids = ref([])
const loading = ref(false)
const showSearch = ref(true)
const queryParams = reactive({
  pageNum: 1,
  pageSize: 10,
  sort: 'CreateTime',
  sortType: 'desc',
  ticketNo: undefined,
  processInstanceId: undefined
})
const columns = ref([
  { visible: true, prop: 'ticketNo', label: '业务编号' },
  { visible: true, prop: 'initiatorId', label: '发起人' },
  { visible: true, prop: 'initiatorName', label: '发起人姓名' },
  { visible: true, prop: 'customId', label: '客户ID' },
  { visible: true, prop: 'partId', label: '料号' },
  { visible: true, prop: 'orderQty', label: '预订单量' },
  { visible: true, prop: 'needDate', label: '需求时间' },
  { visible: true, prop: 'processInstanceId', label: '流程编号' },
  { visible: false, prop: 'status', label: '业务状态' },
  { visible: false, prop: 'createBy', label: '创建人' },
  { visible: false, prop: 'createTime', label: '创建时间' }
])
const total = ref(0)
const dataList = ref([])
const queryRef = ref()

var dictParams = [{ dictType: 'equipment_type' }, { dictType: 'equipment_dev_mode' }]
//获取字典数据
proxy.getDicts(dictParams).then((response) => {
  response.data.forEach((element) => {
    state.options[element.dictType] = element.list
  })
})

function getList() {
  loading.value = true
  listProductDevDemandTicket(queryParams).then((res) => {
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
    ticketNo: [{ required: true, message: '业务编号不能为空', trigger: 'blur' }],
    initiatorId: [{ required: true, message: '发起人不能为空', trigger: 'blur' }],
    customId: [{ required: true, message: '客户ID不能为空', trigger: 'blur' }],
    partId: [{ required: true, message: '料号不能为空', trigger: 'blur' }],
    orderQty: [{ required: true, message: '预订单量不能为空', trigger: 'blur', type: 'number' }],
    needDate: [{ required: true, message: '需求时间不能为空', trigger: 'blur' }],
    delFlag: [{ required: true, message: '删除标志不能为空', trigger: 'blur', type: 'number' }]
  },
  options: {
    // 业务状态 选项列表 格式 eg:{ dictLabel: '标签', dictValue: '0'}
    status_options: [],
    //器材类型
    equipment_type: [],
    //器材开发模式
    equipment_dev_mode: []
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
    customId: null,
    partId: null,
    orderQty: null,
    needDate: null,
    processInstanceId: null,
    engineerId: null,
    engineerLeaderId: null,
    leaderId: null,
    status: null,
    delFlag: null,
    createBy: null,
    createTime: null,
    remark: null
  }
  productDevDemandTicketItemList.value = []
  proxy.resetForm('formRef')
}

// 添加按钮操作
function handleAdd() {
  reset()
  open.value = true
  title.value = '添加产品开发需求单'
  opertype.value = 1
}

// 查看按钮操作
function handlePreview(row) {
  reset()
  const id = row.ticketNo || ids.value
  getProductDevDemandTicket(id).then((res) => {
    const { code, data } = res
    if (code == 200) {
      open.value = true
      title.value = '查看产品开发需求单'
      opertype.value = 3

      form.value = {
        ...data
      }
      productDevDemandTicketItemList.value = res.data.productDevDemandTicketItemNav
    }
  })
}

// 修改按钮操作
function handleAsync(row) {
  reset()
  const id = row.ticketNo || ids.value
  getProductDevDemandTicket(id).then((res) => {
    const { code, data } = res
    if (code == 200) {
      open.value = true
      title.value = '产品开发需求单_同步料号关联'
      opertype.value = 1

      form.value = {
        ...data
      }
      productDevDemandTicketItemList.value = res.data.productDevDemandTicketItemNav
    }
  })
}

// 添加&修改 表单提交
function submitForm() {
  proxy.$refs['formRef'].validate((valid) => {
    if (valid) {
      form.value.productDevDemandTicketItemNav = productDevDemandTicketItemList.value
      if (form.value.ticketNo != undefined && opertype.value === 1) {
        asyncProductDevDemandTicket(form.value.ticketNo).then((res) => {
          proxy.$modal.msgSuccess('同步成功')
          open.value = false
          getList()
        })
      } else {
        addProductDevDemandTicket(form.value).then((res) => {
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
      return delProductDevDemandTicket(Ids)
    })
    .then(() => {
      getList()
      proxy.$modal.msgSuccess('删除成功')
    })
}

/*********************产品开发需求单_需求清单子表信息*************************/
const productDevDemandTicketItemList = ref([])
const checkedProductDevDemandTicketItem = ref([])
const fullScreen = ref(false)
const drawer = ref(false)

/** 产品开发需求单_需求清单序号 */
function rowProductDevDemandTicketItemIndex({ row, rowIndex }) {
  row.index = rowIndex + 1
}

/** 产品开发需求单_需求清单添加按钮操作 */
// function handleAddProductDevDemandTicketItem() {
//   let obj = {}
//   //下面的代码自己设置默认值
//   //obj.processName = null;
//   //obj.standardSpec = null;
//   //obj.equipmentType = null;
//   //obj.caption = null;
//   //obj.qty = null;
//   //obj.devMode = null;
//   //obj.extendTargetId = null;
//   productDevDemandTicketItemList.value.push(obj)
// }

/** 复选框选中数据 */
// function handleProductDevDemandTicketItemSelectionChange(selection) {
//   checkedProductDevDemandTicketItem.value = selection.map((item) => item.index)
// }

/** 产品开发需求单_需求清单删除按钮操作 */
// function handleDeleteProductDevDemandTicketItem() {
//   if (checkedProductDevDemandTicketItem.value.length == 0) {
//     proxy.$modal.msgError('请先选择要删除的产品开发需求单_需求清单数据')
//   } else {
//     const ProductDevDemandTicketItems = productDevDemandTicketItemList.value
//     const checkedProductDevDemandTicketItems = checkedProductDevDemandTicketItem.value
//     productDevDemandTicketItemList.value = ProductDevDemandTicketItems.filter(function (item) {
//       return checkedProductDevDemandTicketItems.indexOf(item.index) == -1
//     })
//   }
// }

/** 产品开发需求单_需求清单详情 */
function rowClick(row) {
  const id = row.ticketNo || ids.value
  getProductDevDemandTicket(id).then((res) => {
    const { code, data } = res
    if (code == 200) {
      drawer.value = true
      productDevDemandTicketItemList.value = data.productDevDemandTicketItemNav
    }
  })
}

//预览：点击文件列表中已上传的文件时的钩子
function handleFilePreview(file) {
  window.open(file.url, '_blank')
}

handleQuery()
</script>
