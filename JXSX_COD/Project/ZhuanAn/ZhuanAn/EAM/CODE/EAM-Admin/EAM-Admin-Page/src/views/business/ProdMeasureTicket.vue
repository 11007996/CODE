<!--
 * @Descripttion: (产品测量报告/BU_Prod_Measure_Ticket)
 * @Author: (admin)
 * @Date: (2024-07-17)
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
        <el-button type="primary" v-hasPermi="['prodMeasureTicket:add']" plain icon="plus" @click="handleAdd">
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
        prop="initiatorId"
        label="发起人Id"
        width="90"
        align="center"
        :show-overflow-tooltip="true"
        v-if="columns.showColumn('initiatorId')" />
      <el-table-column
        prop="initiatorName"
        label="发起人"
        align="center"
        :show-overflow-tooltip="true"
        v-if="columns.showColumn('initiatorName')" />
      <el-table-column
        prop="createMode"
        label="创建模式"
        width="90"
        align="center"
        :show-overflow-tooltip="true"
        v-if="columns.showColumn('createMode')">
        <template #default="scope">
          <dict-tag :options="options.measure_create_mode" :value="scope.row.createMode" />
        </template>
      </el-table-column>
      <el-table-column prop="partId" label="产品料号" min-width="160" :show-overflow-tooltip="true" v-if="columns.showColumn('partId')" />
      <el-table-column prop="fixtureName" label="治具名称" min-width="160" :show-overflow-tooltip="true" v-if="columns.showColumn('fixtureName')" />
      <el-table-column prop="drawingNo" label="治具图号" min-width="160" :show-overflow-tooltip="true" v-if="columns.showColumn('drawingNo')" />
      <el-table-column
        prop="fixtureNoDesc"
        label="治具编号"
        width="90"
        align="center"
        :show-overflow-tooltip="true"
        v-if="columns.showColumn('fixtureNo')" />
      <el-table-column prop="version" label="版本" width="90" align="center" :show-overflow-tooltip="true" v-if="columns.showColumn('version')" />
      <el-table-column prop="processInstanceId" label="流程编号" width="200" align="center" v-if="columns.showColumn('processInstanceId')">
        <template #default="scope">
          <el-link type="primary" @click="openProcess(scope.row)">{{ scope.row.processInstanceId }}</el-link>
        </template>
      </el-table-column>
      <el-table-column prop="makerId" label="制工ID" width="90" align="center" :show-overflow-tooltip="true" v-if="columns.showColumn('makerId')" />
      <el-table-column
        prop="engineerId"
        label="工程师ID"
        width="90"
        align="center"
        :show-overflow-tooltip="true"
        v-if="columns.showColumn('engineerId')" />
      <el-table-column
        prop="engineerLeaderId"
        label="工程师主管ID"
        width="90"
        align="center"
        :show-overflow-tooltip="true"
        v-if="columns.showColumn('engineerLeaderId')" />
      <el-table-column prop="qcId" label="品保ID" width="90" align="center" :show-overflow-tooltip="true" v-if="columns.showColumn('qcId')" />
      <el-table-column
        prop="qcLeaderId"
        label="品保主管ID"
        width="90"
        align="center"
        :show-overflow-tooltip="true"
        v-if="columns.showColumn('qcLeaderId')" />
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
      <el-table-column prop="remark" label="备注" align="center" :show-overflow-tooltip="true" v-if="columns.showColumn('remark')" />
      <el-table-column label="操作" width="110">
        <template #default="scope">
          <el-button type="primary" size="small" icon="view" title="详情" @click="handlePreview(scope.row)"></el-button>
          <!-- <el-button
            type="success"
            size="small"
            icon="edit"
            title="编辑"
            v-hasPermi="['prodMeasureTicket:edit']"
            @click="handleUpdate(scope.row)"></el-button> -->
          <el-button
            type="danger"
            size="small"
            icon="delete"
            title="删除"
            v-hasPermi="['prodMeasureTicket:delete']"
            @click="handleDelete(scope.row)"></el-button>
        </template>
      </el-table-column>
    </el-table>
    <pagination :total="total" v-model:page="queryParams.pageNum" v-model:limit="queryParams.pageSize" @pagination="getList" />

    <el-drawer v-model="drawer" size="50%" direction="rtl">
      <el-table :data="prodMeasureTicketItemList" header-row-class-name="text-navy">
        <el-table-column label="产品编号" align="center" prop="productNo" width="90" />
        <el-table-column
          v-for="item in prodMeasureTicketItemDefineList"
          :label="item.itemName"
          width="90"
          align="center"
          :prop="item.orderNo.toString()">
          <!-- 测试项目:自定义表头 -->
          <template #header="scope">
            <el-row v-if="item.itemName" class="itemName">
              <div>{{ item.itemName }}</div>
            </el-row>
            <el-row :gutter="1" type="flex" justify="center">
              <div class="st">
                <div>{{ item.standard }}</div>
              </div>
              <div class="pc">
                <div>+{{ item.positive }}</div>
                <div>-{{ item.caption }}</div>
              </div>
            </el-row>
          </template>
        </el-table-column>
        <el-table-column label="尺寸判定" align="center" prop="sizeResult" width="90">
          <template #default="scope">
            <dict-tag :options="options.judgment_result" :value="scope.row.sizeResult" />
          </template>
        </el-table-column>
        <el-table-column label="外观判定" align="center" prop="facadeResult" width="90">
          <template #default="scope">
            <dict-tag :options="options.judgment_result" :value="scope.row.facadeResult" />
          </template>
        </el-table-column>
      </el-table>
    </el-drawer>

    <!-- 添加或修改产品测量报告对话框 -->
    <el-dialog :title="title" :lock-scroll="false" v-model="open" :fullscreen="fullScreen">
      <el-row :gutter="10" class="mb8">
        <!-- <el-col :span="1.5">
            <el-button type="primary" icon="Plus" @click="handleAddProdMeasureTicketItem">添加</el-button>
          </el-col>
          <el-col :span="1.5">
            <el-button type="danger" icon="Delete" @click="handleDeleteProdMeasureTicketItem">删除</el-button>
          </el-col> -->
        <el-col :span="1.5">
          <el-button type="info" icon="FullScreen" @click="fullScreen = !fullScreen">{{ fullScreen ? '退出全屏' : '全屏' }}</el-button>
        </el-col>
      </el-row>
      <el-form ref="formRef" :model="form" :rules="rules" label-width="100px">
        <el-row :gutter="20">
          <el-col :lg="12">
            <el-form-item label="业务编号" prop="ticketNo">
              <el-input v-model="form.ticketNo" placeholder="请输入业务编号" :disabled="opertype != 1" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="发起人" prop="initiatorId">
              <el-input v-model="form.initiatorId" placeholder="请输入发起人" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="发起人姓名" prop="initiatorName">
              <el-input v-model="form.initiatorName" placeholder="请输入发起人姓名" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="创建模式" prop="createMode">
              <el-select v-model="form.createMode" clearable placeholder="请选择创建模式">
                <el-option
                  v-for="item in options.measure_create_mode"
                  :key="item.dictValue"
                  :label="item.dictLabel"
                  :value="item.dictValue"></el-option>
              </el-select>
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="产品料号" prop="partId">
              <el-input v-model="form.partId" placeholder="请输入产品料号" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="治具名称" prop="fixtureName">
              <el-input v-model="form.fixtureName" placeholder="请输入治具名称" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="治具图号" prop="drawingNo">
              <el-input v-model="form.drawingNo" placeholder="请输入治具图号" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="治具编号" prop="fixtureNo">
              <el-input v-model="form.fixtureNoDesc" placeholder="请输入治具编号" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="版本" prop="version">
              <el-input v-model="form.version" placeholder="请输入版本" />
            </el-form-item>
          </el-col>
        </el-row>

        <el-divider content-position="center">产品测量报告_产品测量值</el-divider>
        <el-table
          :data="prodMeasureTicketItemList"
          :row-class-name="rowProdMeasureTicketItemIndex"
          @selection-change="handleProdMeasureTicketItemSelectionChange"
          ref="ProdMeasureTicketItemRef">
          <el-table-column label="产品编号" align="center" prop="productNo" width="90" />
          <el-table-column
            v-for="item in prodMeasureTicketItemDefineList"
            :label="item.itemName"
            width="90"
            align="center"
            :prop="item.orderNo.toString()">
            <!-- 测试项目:自定义表头 -->
            <template #header="scope">
              <el-row v-if="item.itemName" class="itemName">
                <div>{{ item.itemName }}</div>
              </el-row>
              <el-row :gutter="1" type="flex" justify="center">
                <div class="st">
                  <div>{{ item.standard }}</div>
                </div>
                <div class="pc">
                  <div>+{{ item.positive }}</div>
                  <div>-{{ item.caption }}</div>
                </div>
              </el-row>
            </template>
          </el-table-column>
          <el-table-column label="尺寸判定" align="center" prop="sizeResult" width="90">
            <template #default="scope">
              <dict-tag :options="options.judgment_result" :value="scope.row.sizeResult" />
            </template>
          </el-table-column>
          <el-table-column label="外观判定" align="center" prop="facadeResult" width="90">
            <template #default="scope">
              <dict-tag :options="options.judgment_result" :value="scope.row.facadeResult" />
            </template>
          </el-table-column>
        </el-table>

        <el-divider content-position="center">产品测量报告_流程审批信息</el-divider>
        <el-row>
          <el-col :lg="12">
            <el-form-item label="流程编号" prop="processInstanceId">
              <el-input v-model="form.processInstanceId" placeholder="请输入流程编号" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="制工" prop="makerName">
              <el-input v-model="form.makerName" placeholder="请输入制工" />
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
            <el-form-item label="品保" prop="qcName">
              <el-input v-model="form.qcName" placeholder="请输入品保" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="品保主管" prop="qcLeaderName">
              <el-input v-model="form.qcLeaderName" placeholder="请输入品保主管" />
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

<script setup name="prodmeasureticket">
import {
  listProdMeasureTicket,
  addProdMeasureTicket,
  delProdMeasureTicket,
  updateProdMeasureTicket,
  getProdMeasureTicket
} from '@/api/business/prodMeasureTicket.js'
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
  { visible: true, prop: 'createMode', label: '创建模式' },
  { visible: true, prop: 'partId', label: '产品料号' },
  { visible: true, prop: 'fixtureName', label: '治具名称' },
  { visible: false, prop: 'drawingNo', label: '治具图号' },
  { visible: true, prop: 'fixtureNoDesc', label: '治具编号' },
  { visible: false, prop: 'version', label: '版本' },
  { visible: true, prop: 'processInstanceId', label: '流程编号' },
  { visible: false, prop: 'makerId', label: '制工ID' },
  { visible: false, prop: 'engineerId', label: '工程师ID' },
  { visible: false, prop: 'engineerLeaderId', label: '工程师主管ID' },
  { visible: false, prop: 'qcId', label: '品保ID' },
  { visible: false, prop: 'qcLeaderId', label: '品保主管ID' },
  { visible: false, prop: 'status', label: '业务状态' },
  { visible: false, prop: 'createBy', label: '创建人' },
  { visible: false, prop: 'createTime', label: '创建时间' },
  { visible: false, prop: 'remark', label: '备注' }
])
const total = ref(0)
const dataList = ref([])
const queryRef = ref()
const defaultTime = ref([new Date(2000, 1, 1, 0, 0, 0), new Date(2000, 2, 1, 23, 59, 59)])

var dictParams = [{ dictType: 'judgment_result' }, { dictType: 'measure_create_mode' }]
//获取字典数据
proxy.getDicts(dictParams).then((response) => {
  response.data.forEach((element) => {
    state.options[element.dictType] = element.list
  })
})

function getList() {
  loading.value = true
  listProdMeasureTicket(queryParams).then((res) => {
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
    createMode: [{ required: true, message: '创建模式不能为空', trigger: 'blur' }],
    partId: [{ required: true, message: '产品料号不能为空', trigger: 'blur' }],
    fixtureName: [{ required: true, message: '治具名称不能为空', trigger: 'blur' }],
    drawingNo: [{ required: true, message: '治具图号不能为空', trigger: 'blur' }],
    fixtureNoDesc: [{ required: true, message: '治具编号不能为空', trigger: 'blur' }],
    version: [{ required: true, message: '版本不能为空', trigger: 'blur' }],
    delFlag: [{ required: true, message: '删除标志不能为空', trigger: 'blur', type: 'number' }]
  },
  options: {
    // 业务状态 选项列表 格式 eg:{ dictLabel: '标签', dictValue: '0'}
    status_options: [],
    //判定结果
    judgment_result: [],
    //测量创建模式
    measure_create_mode: []
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
    createMode: null,
    partId: null,
    fixtureName: null,
    drawingNo: null,
    fixtureNoDesc: null,
    version: null,
    processInstanceId: null,
    makerId: null,
    engineerId: null,
    engineerLeaderId: null,
    qcId: null,
    qcLeaderId: null,
    status: null,
    delFlag: null,
    createBy: null,
    createTime: null,
    remark: null
  }
  prodMeasureTicketItemList.value = []
  proxy.resetForm('formRef')
}

/**
 * 查看
 * @param {*} row
 */
function handlePreview(row) {
  reset()
  const id = row.ticketNo
  getProdMeasureTicket(id).then((res) => {
    const { code, data } = res
    if (code == 200) {
      open.value = true
      title.value = '查看'
      opertype.value = 3
      form.value = {
        ...data
      }
      prodMeasureTicketItemDefineList.value = data.prodMeasureTicketItemDefineNav
      prodMeasureTicketItemList.value = res.data.dynamicStatisticalReport
    }
  })
}

// 添加按钮操作
function handleAdd() {
  reset()
  open.value = true
  title.value = '添加产品测量报告'
  opertype.value = 1
}
// 修改按钮操作
function handleUpdate(row) {
  reset()
  const id = row.ticketNo || ids.value
  getProdMeasureTicket(id).then((res) => {
    const { code, data } = res
    if (code == 200) {
      open.value = true
      title.value = '修改产品测量报告'
      opertype.value = 2

      form.value = {
        ...data
      }
      prodMeasureTicketItemDefineList.value = data.prodMeasureTicketItemDefineNav
      prodMeasureTicketItemList.value = data.dynamicStatisticalReport
    }
  })
}

// 添加&修改 表单提交
function submitForm() {
  proxy.$refs['formRef'].validate((valid) => {
    if (valid) {
      form.value.prodMeasureTicketItemNav = prodMeasureTicketItemList.value
      if (form.value.ticketNo != undefined && opertype.value === 2) {
        updateProdMeasureTicket(form.value).then((res) => {
          proxy.$modal.msgSuccess('修改成功')
          open.value = false
          getList()
        })
      } else {
        addProdMeasureTicket(form.value).then((res) => {
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
      return delProdMeasureTicket(Ids)
    })
    .then(() => {
      getList()
      proxy.$modal.msgSuccess('删除成功')
    })
}

/*********************产品测量报告_测试项目定义子表信息*************************/
const prodMeasureTicketItemList = ref([])
const prodMeasureTicketItemDefineList = ref([])
const checkedProdMeasureTicketItem = ref([])
const fullScreen = ref(false)
const drawer = ref(false)

/** 产品测量报告_测试项目定义序号 */
function rowProdMeasureTicketItemIndex({ row, rowIndex }) {
  row.index = rowIndex + 1
}

/** 产品测量报告_测试项目定义添加按钮操作 */
function handleAddProdMeasureTicketItem() {
  let obj = {}
  //下面的代码自己设置默认值
  //obj.itemName = null;
  //obj.standard = null;
  //obj.positive = null;
  //obj.caption = null;
  prodMeasureTicketItemList.value.push(obj)
}

/** 复选框选中数据 */
function handleProdMeasureTicketItemSelectionChange(selection) {
  checkedProdMeasureTicketItem.value = selection.map((item) => item.index)
}

/** 产品测量报告_测试项目定义删除按钮操作 */
function handleDeleteProdMeasureTicketItem() {
  if (checkedProdMeasureTicketItem.value.length == 0) {
    proxy.$modal.msgError('请先选择要删除的产品测量报告_测试项目定义数据')
  } else {
    const ProdMeasureTicketItems = prodMeasureTicketItemList.value
    const checkedProdMeasureTicketItems = checkedProdMeasureTicketItem.value
    prodMeasureTicketItemList.value = ProdMeasureTicketItems.filter(function (item) {
      return checkedProdMeasureTicketItems.indexOf(item.index) == -1
    })
  }
}

/** 产品测量报告_测试项目定义详情 */
function rowClick(row) {
  const id = row.ticketNo || ids.value
  getProdMeasureTicket(id).then((res) => {
    const { code, data } = res
    if (code == 200) {
      drawer.value = true
      prodMeasureTicketItemDefineList.value = data.prodMeasureTicketItemDefineNav
      prodMeasureTicketItemList.value = data.dynamicStatisticalReport
    }
  })
}

handleQuery()
</script>
<style lang="scss">
// 列头：测试项名称
.itemName div {
  margin: auto;
}
// 列头：标准值
.st div {
  font-size: 18px;
  line-height: 18px;
  text-align: right;
  text-wrap: nowrap;
}
// 列头：正负公差值
.pc div {
  font-size: 10px;
  line-height: 10px;
  text-align: left;
  text-wrap: nowrap;
}
</style>
