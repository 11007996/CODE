<!--
 * @Descripttion: (设备资产信息/EQU_Equipment_Base)
 * @Author: (admin)
 * @Date: (2024-09-30)
-->
<template>
  <div>
    <el-form :model="queryParams" label-position="right" inline ref="queryRef" v-show="showSearch" @submit.prevent>
      <el-form-item label="设备名称" prop="equipmentName">
        <el-input v-model="queryParams.equipmentName" placeholder="请输入设备名称" />
      </el-form-item>
      <el-form-item label="资产编号" prop="assetNo">
        <el-input v-model="queryParams.assetNo" placeholder="请输入资产编号" />
      </el-form-item>
      <el-form-item label="资产名称" prop="assetName">
        <el-input v-model="queryParams.assetName" placeholder="请输入资产名称" />
      </el-form-item>
      <el-form-item label="资产分类" prop="assetClass">
        <el-input v-model="queryParams.assetClass" placeholder="请输入资产分类" />
      </el-form-item>
      <el-form-item label="成本中心" prop="costCenter">
        <el-input v-model="queryParams.costCenter" placeholder="请输入成本中心" />
      </el-form-item>
      <el-form-item label="自定义机型" prop="customModel">
        <el-input v-model="queryParams.customModel" placeholder="请输入自定义机型" />
      </el-form-item>
      <el-form-item>
        <el-button icon="search" type="primary" @click="handleQuery">{{ $t('btn.search') }}</el-button>
        <el-button icon="refresh" @click="resetQuery">{{ $t('btn.reset') }}</el-button>
      </el-form-item>
    </el-form>
    <!-- 工具区域 -->
    <el-row :gutter="15" class="mb10">
      <el-col :span="1.5">
        <el-button type="primary" v-hasPermi="['equipment:add']" plain icon="plus" @click="handleAdd">
          {{ $t('btn.add') }}
        </el-button>
      </el-col>
      <el-col :span="1.5">
        <el-dropdown trigger="click" v-hasPermi="['equipment:import']">
          <el-button type="primary" plain icon="Upload">
            {{ $t('btn.import') }}<el-icon class="el-icon--right"><arrow-down /></el-icon>
          </el-button>
          <template #dropdown>
            <el-dropdown-menu>
              <el-dropdown-item command="upload">
                <importData
                  templateUrl="equipment/equipmentBase/importTemplate"
                  importUrl="/equipment/equipmentBase/importData"
                  @success="handleFileSuccess"></importData>
              </el-dropdown-item>
            </el-dropdown-menu>
          </template>
        </el-dropdown>
      </el-col>
      <el-col :span="1.5">
        <el-button type="warning" plain icon="download" @click="handleExport" v-hasPermi="['equipment:export']">
          {{ $t('btn.export') }}
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
      <el-table-column prop="equipmentId" label="设备ID" align="center" width="90" v-if="columns.showColumn('equipmentId')" />
      <el-table-column
        prop="equipmentName"
        label="设备名称"
        align="center"
        min-width="200"
        :show-overflow-tooltip="true"
        v-if="columns.showColumn('equipmentName')" />
      <el-table-column prop="assetNo" label="资产编号" align="center" width="210" v-if="columns.showColumn('assetNo')" />
      <el-table-column prop="factoryCode" label="公司代码" align="center" width="90" v-if="columns.showColumn('factoryCode')" />
      <el-table-column prop="assetMainNo" label="资产主编号" align="center" width="90" v-if="columns.showColumn('assetMainNo')" />
      <el-table-column prop="assetSubNo" label="资产子编号" align="center" width="90" v-if="columns.showColumn('assetSubNo')" />
      <el-table-column
        prop="assetName"
        label="资产名称"
        align="center"
        min-width="200"
        :show-overflow-tooltip="true"
        v-if="columns.showColumn('assetName')" />
      <el-table-column
        prop="assetClass"
        label="资产分类"
        align="center"
        width="120"
        :show-overflow-tooltip="true"
        v-if="columns.showColumn('assetClass')" />
      <el-table-column prop="model" label="型号规格" align="center" width="120" :show-overflow-tooltip="true" v-if="columns.showColumn('model')" />
      <el-table-column
        prop="entryDate"
        label="购置日期"
        align="center"
        width="110"
        :formatter="(row) => formatterDate(row.entryDate)"
        v-if="columns.showColumn('entryDate')" />
      <el-table-column prop="costCenter" label="成本中心" min-width="150" :show-overflow-tooltip="true" v-if="columns.showColumn('costCenter')" />
      <el-table-column prop="durableYear" label="耐用年限" align="center" width="90" v-if="columns.showColumn('durableYear')" />
      <el-table-column prop="durableMonth" label="耐用月数" align="center" width="90" v-if="columns.showColumn('durableMonth')" />
      <el-table-column prop="madeFactory" label="制造厂商" width="150" :show-overflow-tooltip="true" v-if="columns.showColumn('madeFactory')" />
      <el-table-column
        prop="controlNo"
        label="校验管制编号"
        align="center"
        width="90"
        :show-overflow-tooltip="true"
        v-if="columns.showColumn('controlNo')" />
      <el-table-column
        prop="customModel"
        label="自定义机型"
        align="center"
        width="150"
        :show-overflow-tooltip="true"
        v-if="columns.showColumn('customModel')" />
      <el-table-column prop="status" label="状态" align="center" width="90" v-if="columns.showColumn('status')">
        <template #default="scope">
          <dict-tag :options="options.equipment_status" :value="scope.row.status" />
        </template>
      </el-table-column>
      <el-table-column prop="updateTime" label="更新时间" width="160" v-if="columns.showColumn('updateTime')" />
      <el-table-column prop="updateUser" label="更新人" align="center" width="90" v-if="columns.showColumn('updateUser')" />
      <el-table-column label="操作" width="160" fixed="right">
        <template #default="scope">
          <el-button type="primary" size="small" icon="view" title="详情" @click="handlePreview(scope.row)"></el-button>
          <el-button
            type="success"
            size="small"
            icon="edit"
            title="编辑"
            v-hasPermi="['equipment:edit']"
            @click="handleUpdate(scope.row)"></el-button>
          <el-button
            type="danger"
            size="small"
            icon="delete"
            title="删除"
            v-hasPermi="['equipment:delete']"
            @click="handleDelete(scope.row)"></el-button>
        </template>
      </el-table-column>
    </el-table>
    <pagination :total="total" v-model:page="queryParams.pageNum" v-model:limit="queryParams.pageSize" @pagination="getList" />

    <!-- 添加或修改设备资产信息对话框 -->
    <el-dialog :title="title" :lock-scroll="false" v-model="open">
      <el-form ref="formRef" :model="form" :rules="rules" label-width="100px">
        <el-row :gutter="20">
          <!-- <el-col :lg="12" v-if="opertype != 1">
            <el-form-item label="设备ID" prop="equipmentId">
              <el-input v-model="form.equipmentId" disabled />
            </el-form-item>
          </el-col> -->

          <el-col :lg="12">
            <el-form-item label="设备名称" prop="equipmentName">
              <template #label>
                <span>
                  <el-tooltip placement="top" content="通常设置为设备的类型名称(方便记忆),下拉选内容请在[设备类型]菜单中维护,也可以自定义名称">
                    <el-icon> <questionFilled /> </el-icon>
                  </el-tooltip>
                  设备名称
                </span>
              </template>
              <el-select
                v-model="form.equipmentName"
                placeholder="请选择设备名称(类型)"
                clearable
                filterable
                allow-create
                remote
                :remote-method="handleQueryEquipmentType"
                class="fullWidth">
                <el-option
                  v-for="item in options.equipment_type_options"
                  :key="item.dictValue"
                  :label="item.dictLabel"
                  :value="item.dictValue"></el-option>
              </el-select>
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="资产编号" prop="assetNo">
              <el-input v-model="form.assetNo" placeholder="请输入资产编号" disabled />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="公司代码" prop="factoryCode">
              <el-input v-model="form.factoryCode" placeholder="请输入公司代码" :disabled="opertype != 1" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="资产主编号" prop="assetMainNo">
              <el-input v-model="form.assetMainNo" placeholder="请输入资产主编号" :disabled="opertype != 1" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="资产子编号" prop="assetSubNo">
              <el-input v-model="form.assetSubNo" placeholder="请输入资产子编号" :disabled="opertype != 1" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="资产名称" prop="assetName">
              <el-input v-model="form.assetName" placeholder="请输入资产名称" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="资产分类" prop="assetClass">
              <el-input v-model="form.assetClass" placeholder="请输入资产分类" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="型号规格" prop="model">
              <el-input v-model="form.model" placeholder="请输入型号规格" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="购置日期" prop="entryDate">
              <el-date-picker v-model="form.entryDate" type="date" :teleported="false" placeholder="选择日期时间"></el-date-picker>
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="成本中心" prop="costCenter">
              <el-input v-model="form.costCenter" placeholder="请输入成本中心" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="耐用年限" prop="durableYear">
              <el-input-number v-model.number="form.durableYear" :controls="true" controls-position="right" placeholder="请输入耐用年限" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="耐用月数" prop="durableMonth">
              <el-input-number v-model.number="form.durableMonth" :controls="true" controls-position="right" placeholder="请输入耐用月数" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="制造厂商" prop="madeFactory">
              <el-input v-model="form.madeFactory" placeholder="请输入制造厂商" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="校验管制编号" prop="controlNo">
              <el-input v-model="form.controlNo" placeholder="请输入校验管制编号" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="自定义机型" prop="customModel">
              <el-select
                v-model="form.customModel"
                placeholder="请选择机型"
                clearable
                filterable
                allow-create
                remote
                :remote-method="handleQueryCustomModel"
                class="fullWidth">
                <el-option
                  v-for="item in options.customModel_options"
                  :key="item.dictValue"
                  :label="item.dictLabel"
                  :value="item.dictValue"></el-option>
              </el-select>
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="状态" prop="status">
              <el-select v-model="form.status" placeholder="请选择状态" clearable class="fullWidth">
                <el-option
                  v-for="item in options.equipment_status"
                  :key="item.dictValue"
                  :label="item.dictLabel"
                  :value="item.dictValue"></el-option>
              </el-select>
            </el-form-item>
          </el-col>
        </el-row>
      </el-form>
      <template #footer v-if="opertype != 3">
        <el-button text @click="cancel">{{ $t('btn.cancel') }}</el-button>
        <el-button type="primary" @click="submitForm">{{ $t('btn.submit') }}</el-button>
      </template>
    </el-dialog>

    <!-- 详情 -->
    <el-dialog :title="title" :lock-scroll="false" v-model="openDetail" width="85%">
      <EquipmentDetail :equipmentId="equipmentId" />
    </el-dialog>
  </div>
</template>

<script setup name="equipment">
import {
  listEquipmentBase,
  addEquipmentBase,
  delEquipmentBase,
  updateEquipmentBase,
  getEquipmentBase,
  dictCustomModel
} from '@/api/equipment/equipmentBase.js'
import { dictEquipmentType } from '@/api/equipment/equipmentType.js'
import importData from '@/components/ImportData'
import { dayjs } from 'element-plus'
import EquipmentDetail from './EquipmentDetail.vue'

const { proxy } = getCurrentInstance()
const router = useRouter()
const ids = ref([])
const loading = ref(false)
const showSearch = ref(true)
const queryParams = reactive({
  pageNum: 1,
  pageSize: 10,
  sort: '',
  sortType: 'asc',
  equipmentName: undefined,
  assetNo: undefined,
  assetName: undefined,
  assetClass: undefined,
  costCenter: undefined,
  customModel: undefined
})
const columns = ref([
  { visible: false, prop: 'equipmentId', label: '设备ID' },
  { visible: true, prop: 'equipmentName', label: '设备名称' },
  { visible: true, prop: 'assetNo', label: '资产编号' },
  { visible: false, prop: 'factoryCode', label: '公司代码' },
  { visible: false, prop: 'assetMainNo', label: '资产主编号' },
  { visible: false, prop: 'assetSubNo', label: '资产子编号' },
  { visible: false, prop: 'assetName', label: '资产名称' },
  { visible: true, prop: 'assetClass', label: '资产分类' },
  { visible: true, prop: 'model', label: '型号规格' },
  { visible: true, prop: 'entryDate', label: '购置日期' },
  { visible: true, prop: 'costCenter', label: '成本中心' },
  { visible: false, prop: 'durableYear', label: '耐用年限' },
  { visible: false, prop: 'durableMonth', label: '耐用月数' },
  { visible: false, prop: 'madeFactory', label: '制造厂商' },
  { visible: false, prop: 'controlNo', label: '校验管制编号' },
  { visible: false, prop: 'customModel', label: '自定义机型' },
  { visible: true, prop: 'status', label: '状态' },
  { visible: false, prop: 'updateTime', label: '更新时间' },
  { visible: false, prop: 'updateUser', label: '更新人' }
])
const total = ref(0)
const dataList = ref([])
const queryRef = ref()

var dictParams = [{ dictType: 'equipment_status' }]

proxy.getDicts(dictParams).then((response) => {
  response.data.forEach((element) => {
    state.options[element.dictType] = element.list
  })
})

function getList() {
  loading.value = true
  listEquipmentBase(queryParams).then((res) => {
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
    equipmentName: [{ required: true, message: '设备名称不能为空', trigger: 'blur' }],
    factoryCode: [{ required: true, message: '公司代码不能为空', trigger: 'blur' }],
    assetMainNo: [{ required: true, message: '资产主编号不能为空', trigger: 'blur' }],
    assetSubNo: [{ required: true, message: '资产子编号不能为空', trigger: 'blur' }],
    assetName: [{ required: true, message: '资产名称不能为空', trigger: 'blur' }],
    status: [{ required: true, message: '状态不能为空', trigger: 'blur' }],
    updateTime: [{ required: true, message: '更新时间不能为空', trigger: 'blur' }]
  },
  options: {
    // 设备状态
    equipment_status: [],
    // 设备类型名称
    equipment_type_options: [],
    // 自定义机型字典
    customModel_options: []
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
    equipmentId: null,
    equipmentName: null,
    assetNo: null,
    factoryCode: null,
    assetMainNo: null,
    assetSubNo: null,
    assetName: null,
    assetClass: null,
    model: null,
    entryDate: null,
    costCenter: null,
    durableYear: null,
    durableMonth: null,
    madeFactory: null,
    controlNo: null,
    customModel: null,
    status: null,
    updateTime: null,
    updateUser: null
  }
  proxy.resetForm('formRef')
}

// 添加按钮操作
function handleAdd() {
  reset()
  open.value = true
  title.value = '添加设备资产信息'
  opertype.value = 1
}
// 修改按钮操作
function handleUpdate(row) {
  reset()
  const id = row.equipmentId || ids.value
  getEquipmentBase(id).then((res) => {
    const { code, data } = res
    if (code == 200) {
      open.value = true
      title.value = '修改设备资产信息'
      opertype.value = 2

      form.value = {
        ...data
      }
    }
  })
}

// 添加&修改 表单提交
function submitForm() {
  proxy.$refs['formRef'].validate((valid) => {
    if (valid) {
      if (form.value.equipmentId != undefined && opertype.value === 2) {
        updateEquipmentBase(form.value).then((res) => {
          proxy.$modal.msgSuccess('修改成功')
          open.value = false
          getList()
        })
      } else {
        addEquipmentBase(form.value).then((res) => {
          proxy.$modal.msgSuccess('新增成功')
          open.value = false
          getList()
        })
      }
    }
  })
}

/**
 * 查看
 * @param {*} row
 */
const openDetail = ref(false)
const equipmentId = ref()
function handlePreview(row) {
  equipmentId.value = row.equipmentId
  openDetail.value = true
  title.value = '设备详情'
}

// 删除按钮操作
function handleDelete(row) {
  const Ids = row.equipmentId || ids.value

  proxy
    .$confirm('是否确认删除参数编号为"' + Ids + '"的数据项？', '警告', {
      confirmButtonText: proxy.$t('common.ok'),
      cancelButtonText: proxy.$t('common.cancel'),
      type: 'warning'
    })
    .then(function () {
      return delEquipmentBase(Ids)
    })
    .then(() => {
      getList()
      proxy.$modal.msgSuccess('删除成功')
    })
}

// 导入数据成功处理
const handleFileSuccess = (response) => {
  const { item1, item2 } = response.data
  var error = ''
  item2.forEach((item) => {
    error += item.storageMessage + ','
  })
  proxy.$alert(item1 + '<p>' + error + '</p>', '导入结果', {
    dangerouslyUseHTMLString: true
  })
  getList()
}

// 导出按钮操作
function handleExport() {
  proxy
    .$confirm('是否确认导出设备资产信息数据项?', '警告', {
      confirmButtonText: '确定',
      cancelButtonText: '取消',
      type: 'warning'
    })
    .then(async () => {
      await proxy.downFile('/equipment/equipmentBase/export', { ...queryParams })
    })
}

//格式化日期
function formatterDate(time) {
  if (time) return dayjs(time).format('YYYY-MM-DD')
}

function handleQueryEquipmentType(keyword) {
  if (keyword) {
    const params = {
      pageNum: 1,
      pageSize: 10,
      sort: '',
      sortType: 'asc',
      equipmentTypeName: keyword
    }
    setTimeout(() => {
      dictEquipmentType(params).then((res) => {
        state.options.equipment_type_options = res.data.result
      })
    }, 200)
  }
}

//查询自定义机型字典
function handleQueryCustomModel(keyword) {
  const params = {
    pageNum: 1,
    pageSize: 100,
    sort: '',
    sortType: 'asc',
    customModel: keyword
  }
  setTimeout(() => {
    dictCustomModel(params).then((res) => {
      state.options.customModel_options = res.data.result
    })
  }, 200)
}

handleQuery()
</script>
