<!--
 * @Descripttion: (资产保养报表/EQU_Maintain_Report)
 * @Author: (admin)
 * @Date: (2024-10-08)
-->
<template>
  <div>
    <el-form :model="queryParams" label-position="right" inline ref="queryRef" v-show="showSearch" @submit.prevent>
      <el-form-item label="设备" prop="equipmentId">
        <el-select
          v-model="queryParams.equipmentId"
          placeholder="资产编号/设备名称/资产名称/自定义机型"
          clearable
          filterable
          remote
          :remote-method="handleQueryEquipment">
          <el-option v-for="item in options.equipment_options" :key="item.dictValue" :label="item.dictLabel" :value="item.dictValue"></el-option>
        </el-select>
      </el-form-item>
      <el-form-item label="年份" prop="year">
        <el-date-picker v-model="queryParams.year" type="year" value-format="YYYY" placeholder="选择年"> </el-date-picker>
      </el-form-item>
      <el-form-item>
        <el-button icon="search" type="primary" @click="handleQuery">{{ $t('btn.search') }}</el-button>
        <el-button icon="refresh" @click="resetQuery">{{ $t('btn.reset') }}</el-button>
      </el-form-item>
    </el-form>

    <!-- 工具区域 -->
    <el-row :gutter="15" class="mb10">
      <el-col :span="1.5">
        <el-button type="primary" v-hasPermi="['maintain:report:add']" plain icon="plus" @click="handleAdd">
          {{ $t('btn.add') }}
        </el-button>
        <el-button type="primary" v-hasPermi="['maintain:report:add']" plain icon="plus" @click="handleBatchAdd"> 批量新增 </el-button>
      </el-col>
      <el-col :span="1.5">
        <el-button type="success" v-hasPermi="['maintain:report:query']" plain icon="grid" @click="handleQRCode"> 二维码 </el-button>
      </el-col>
      <el-col :span="1.5">
        <el-button type="danger" v-hasPermi="['maintain:record:globalMaintain']" plain icon="MagicStick" @click="handleGlobalMaintain">
          全局保养
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
      @cell-dblclick="handleCellDblclick"
      @sort-change="sortChange">
      <el-table-column prop="equipmentId" label="设备ID" align="center" fixed="left" v-if="columns.showColumn('equipmentId')" />
      <el-table-column
        prop="assetNo"
        label="资产编号"
        align="center"
        width="200"
        fixed="left"
        :show-overflow-tooltip="true"
        v-if="columns.showColumn('assetNo')">
        <template #default="scope">
          <el-link type="primary" @click="openMaintainItem(scope.row)"> {{ scope.row.assetNo }} </el-link>
        </template>
      </el-table-column>
      <el-table-column
        prop="assetName"
        label="资产名称"
        align="center"
        width="200"
        :show-overflow-tooltip="true"
        v-if="columns.showColumn('assetName')" />
      <el-table-column prop="year" label="年份标记" align="center" width="90" v-if="columns.showColumn('year')" />

      <el-table-column :prop="item.name" :label="item.label" align="center" v-for="item in monthNames">
        <template #default="scope">
          <div v-if="scope.row[item.name] && scope.row[item.name] != 0">
            <el-link type="primary" @click="openReportSheet(scope.row, item.name)"> 查看 </el-link>
          </div>
          <div v-if="!scope.row[item.name] || scope.row[item.name] == 0"></div>
        </template>
      </el-table-column>
    </el-table>
    <pagination :total="total" v-model:page="queryParams.pageNum" v-model:limit="queryParams.pageSize" @pagination="getList" />

    <!-- 添加或修改资产保养报表对话框 -->
    <el-dialog :title="title" :lock-scroll="false" v-model="open">
      <el-form ref="formRef" :model="form" :rules="rules" label-width="100px">
        <el-row :gutter="20">
          <el-col :lg="12" v-if="opertype == 1">
            <el-form-item label="设备" prop="equipmentId">
              <el-select
                v-model="form.equipmentId"
                placeholder="资产编号/设备名称/资产名称/自定义机型"
                clearable
                filterable
                remote
                :remote-method="handleQueryEquipment"
                @change="handleEquipmentChange">
                <el-option
                  v-for="item in options.equipment_options"
                  :key="item.dictValue"
                  :label="item.dictLabel"
                  :value="item.dictValue"></el-option>
              </el-select>
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="年标记" prop="year">
              <el-date-picker v-model="form.year" type="year" value-format="YYYY" placeholder="选择年"> </el-date-picker>
            </el-form-item>
          </el-col>
        </el-row>
      </el-form>

      <!-- 预览保养项目 -->
      <div v-if="opertype == 1">
        <h3>预览保养项目</h3>
        <el-table :data="itemDataList" v-loading="loading" border header-cell-class-name="el-table-header-cell" highlight-current-row height="430">
          <el-table-column prop="dateMark" label="日期标记" align="center" width="90">
            <template #default="scope">
              <dict-tag :options="options.date_mark" :value="scope.row.dateMark" />
            </template>
          </el-table-column>
          <el-table-column prop="itemName" label="项目名称" min-width="200" :show-overflow-tooltip="true" />
        </el-table>
      </div>

      <template #footer v-if="opertype != 3">
        <el-button text @click="cancel">{{ $t('btn.cancel') }}</el-button>
        <el-button type="primary" @click="submitForm">{{ $t('btn.submit') }}</el-button>
      </template>
    </el-dialog>

    <!-- 二维码 -->
    <el-dialog :title="title" :lock-scroll="false" v-model="openQRCode" width="70%">
      <MaintainQRCode v-if="openQRCode"></MaintainQRCode>
    </el-dialog>

    <!-- 全局维护 -->
    <el-dialog :title="title" :lock-scroll="false" v-model="openGlobalMaintain">
      <GlobalMaintain v-if="openGlobalMaintain"></GlobalMaintain>
    </el-dialog>

    <!-- 保养项目 -->
    <el-dialog :title="title" :lock-scroll="false" v-model="openItem">
      <MaintainItem :equipment-id="currEquipmentId" :year="currYear" v-if="openItem"></MaintainItem>
    </el-dialog>

    <!-- 保养报表Sheet页 -->
    <el-dialog :title="title" :lock-scroll="false" v-model="openSheet" width="99%" top="1vh">
      <MaintainReportSheet :equipment-id="currEquipmentId" :year="currYear" :month="currMonth" v-if="openSheet"></MaintainReportSheet>
    </el-dialog>
  </div>
</template>

<script setup name="maintainreport">
import { listMaintainReport, addMaintainReport, updateMaintainReport, batchAddMaintainReport } from '@/api/equipment/maintainReport.js'
import { listMaintainItem } from '@/api/equipment/maintainItem.js'
import { dictEquipmentBase } from '@/api/equipment/equipmentBase.js'
import MaintainItem from './MaintainItem.vue'
import MaintainReportSheet from './MaintainReportSheet.vue'
import MaintainQRCode from './MaintainQRCode.vue'
import GlobalMaintain from './GlobalMaintain.vue'

const { proxy } = getCurrentInstance()
const loading = ref(false)
const showSearch = ref(true)
const queryParams = reactive({
  pageNum: 1,
  pageSize: 10,
  sort: 'AssetNo',
  sortType: 'asc',
  equipmentId: undefined,
  year: new Date().getFullYear().toString()
})
const columns = ref([
  { visible: false, prop: 'equipmentId', label: '设备ID' },
  { visible: true, prop: 'assetNo', label: '资产编号' },
  { visible: true, prop: 'assetName', label: '资产名称' },
  { visible: false, prop: 'year', label: '年标记' }
])
//-- Jan、Feb、Mar、Apr 、May、Jun、Jul、Aug、Sept、Oct、Nov、Dec
const monthNames = ref([
  { name: 'jan', label: '1月' },
  { name: 'feb', label: '2月' },
  { name: 'mar', label: '3月' },
  { name: 'apr', label: '4月' },
  { name: 'may', label: '5月' },
  { name: 'jun', label: '6月' },
  { name: 'jul', label: '7月' },
  { name: 'aug', label: '8月' },
  { name: 'sept', label: '9月' },
  { name: 'oct', label: '10月' },
  { name: 'nov', label: '11月' },
  { name: 'dec', label: '12月' },
  { name: 'yearly', label: '年度' }
])
const total = ref(0)
const dataList = ref([])
const itemDataList = ref([]) //保养项目
const queryRef = ref()

var dictParams = [{ dictType: 'sys_yes_no' }, { dictType: 'date_mark' }]

proxy.getDicts(dictParams).then((response) => {
  response.data.forEach((element) => {
    state.options[element.dictType] = element.list
  })
})

function getList() {
  loading.value = true
  listMaintainReport(queryParams).then((res) => {
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
// 操作类型 1、add 2、batchAdd
const opertype = ref(0)
const open = ref(false)
const state = reactive({
  single: true,
  multiple: true,
  form: {},
  rules: {
    equipmentId: [{ required: true, message: '设备不能为空', trigger: 'blur' }],
    year: [{ required: true, message: '年标记不能为空', trigger: 'blur' }]
  },
  options: {
    // 报表状态 选项列表 格式 eg:{ dictLabel: '标签', dictValue: '0'}
    sys_yes_no: [],
    // 日期标记
    date_mark: [],
    //设备
    equipment_options: []
  }
})

const { form, rules, options } = toRefs(state)

// 关闭dialog
function cancel() {
  open.value = false
  reset()
}

// 重置表单
function reset() {
  form.value = {
    equipmentId: null,
    year: null
  }
  itemDataList.value = []
  proxy.resetForm('formRef')
}

// 添加按钮操作
function handleAdd() {
  reset()
  open.value = true
  title.value = '添加资产保养报表'
  opertype.value = 1
}

// 批量创建按钮操作
function handleBatchAdd() {
  reset()
  open.value = true
  title.value = '批量新增保养报表'
  opertype.value = 2
}

// 添加&修改 表单提交
function submitForm() {
  proxy.$refs['formRef'].validate((valid) => {
    if (valid) {
      if (opertype.value === 2) {
        batchAddMaintainReport(form.value).then((res) => {
          proxy.$modal.msgSuccess('批量新增成功')
          open.value = false
          getList()
        })
      } else {
        addMaintainReport(form.value).then((res) => {
          proxy.$modal.msgSuccess('新增成功')
          open.value = false
          getList()
        })
      }
    }
  })
}

// 查询设备
function handleQueryEquipment(keyword) {
  if (keyword) {
    const params = {
      pageNum: 1,
      pageSize: 10,
      sort: '',
      sortType: 'asc',
      keyword: keyword
    }
    setTimeout(() => {
      dictEquipmentBase(params).then((res) => {
        state.options.equipment_options = res.data.result
      })
    }, 200)
  }
}

// 资产编号变更事件
function handleEquipmentChange(keyword) {
  if (keyword)
    setTimeout(() => {
      getMaintainItemList()
    }, 200)
  else itemDataList.value = []
}

//获取设备的预订保养项目列表
function getMaintainItemList() {
  loading.value = true
  let param = {
    pageNum: 1,
    pageSize: 1000,
    equipmentId: form.value.equipmentId
  }
  listMaintainItem(param).then((res) => {
    const { code, data } = res
    if (code == 200) {
      itemDataList.value = data.result
      loading.value = false
    }
  })
}

//打开 二维码
const openQRCode = ref(false)
function handleQRCode() {
  title.value = '保养二维码'
  openQRCode.value = true
}

//打开 全局保养
const openGlobalMaintain = ref(false)
function handleGlobalMaintain() {
  title.value = '资产全局保养'
  openGlobalMaintain.value = true
}

// 打开 设备保养项目
const openItem = ref(false)
function openMaintainItem(row) {
  title.value = '设备保养项目'
  currEquipmentId.value = row.equipmentId
  currYear.value = String(row.year)
  openItem.value = true
}

// ****************** Sheet操作 *******************
const currEquipmentId = ref('')
const currYear = ref('')
const currMonth = ref('')
const openSheet = ref(false)
//打开设备的保养页Sheet
function openReportSheet(row, column) {
  currEquipmentId.value = row.equipmentId
  currYear.value = String(row.year)
  let month = ['', 'jan', 'feb', 'mar', 'apr', 'may', 'jun', 'jul', 'aug', 'sept', 'oct', 'nov', 'dec']
  currMonth.value = String(month.indexOf(column))
  if (currMonth.value <= 0) currMonth.value = null
  openSheet.value = true
}

//单元格双击
function handleCellDblclick(row, column) {
  const colProps = ['year', 'assetNo', 'assetName', 'equipmentId']
  if (colProps.indexOf(column.property) >= 0) return
  openReportSheet(row, column.property)
}

handleQuery()
</script>
