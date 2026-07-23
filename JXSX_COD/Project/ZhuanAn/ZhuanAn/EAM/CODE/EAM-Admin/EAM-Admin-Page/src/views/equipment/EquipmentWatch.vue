<!--
 * @Descripttion: (设备运行数据/EQU_Equipment_Watch)
 * @Author: (admin)
 * @Date: (2024-12-09)
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
      <el-form-item label="运行状态" prop="runState">
        <el-select v-model="queryParams.runState" clearable style="width: 240px">
          <el-option v-for="dict in options.equipment_run_state" :key="dict.dictValue" :label="dict.dictLabel" :value="dict.dictValue" />
        </el-select>
      </el-form-item>
      <el-form-item label="创建时间">
        <el-date-picker
          v-model="dateRangeCreateTime"
          type="datetimerange"
          start-placeholder="开始日期"
          end-placeholder="结束日期"
          value-format="YYYY-MM-DD HH:mm:ss"
          :default-time="defaultTime"
          :shortcuts="dateOptions">
        </el-date-picker>
      </el-form-item>
      <el-form-item>
        <el-button icon="search" type="primary" @click="handleQuery">{{ $t('btn.search') }}</el-button>
        <el-button icon="refresh" @click="resetQuery">{{ $t('btn.reset') }}</el-button>
      </el-form-item>
    </el-form>
    <!-- 工具区域 -->
    <el-row :gutter="15" class="mb10">
      <pagination :total="total" v-model:page="queryParams.pageNum" v-model:limit="queryParams.pageSize" @pagination="getList" />
      <el-switch
        class="mt10"
        inline-prompt
        v-model="showMode"
        active-value="table"
        in-active-value="card"
        active-text="列表"
        inactive-text="卡片"></el-switch>
      <right-toolbar v-model:showSearch="showSearch" @queryTable="getList" :columns="columns"></right-toolbar>
    </el-row>

    <!-- 数据区域 -->
    <el-table v-if="showMode === 'table'" :data="dataList" v-loading="loading" ref="table" border highlight-current-row>
      <el-table-column prop="equipmentId" label="设备ID" align="center" width="90" v-if="columns.showColumn('equipmentId')" />
      <el-table-column prop="equipment" label="设备名称(编号)" align="center" min-width="200">
        <template #default="scope"> {{ scope.row.equipmentName }}({{ scope.row.equipmentNo }}) </template>
      </el-table-column>
      <el-table-column prop="assetNo" label="资产编号" align="center" width="210" v-if="columns.showColumn('assetNo')" />
      <el-table-column prop="assetName" label="资产名称" align="center" width="200" v-if="columns.showColumn('assetName')" />
      <el-table-column prop="runState" label="运行状态" align="center" width="90" v-if="columns.showColumn('runState')">
        <template #default="scope">
          <dict-tag :options="options.equipment_run_state" :value="scope.row.runState" />
        </template>
      </el-table-column>
      <el-table-column prop="lineName" label="线别" width="90" align="center" v-if="columns.showColumn('lineName')" />
      <el-table-column prop="productCount" label="产能数量" align="right" width="90" v-if="columns.showColumn('productCount')" />
      <el-table-column prop="defectCount" label="不良数量" align="right" width="90" v-if="columns.showColumn('defectCount')" />
      <el-table-column prop="warnState" label="报警状态" align="center" width="90" v-if="columns.showColumn('warnState')">
        <template #default="scope">
          <dict-tag :options="options.equipment_warn_state" :value="scope.row.warnState" />
        </template>
      </el-table-column>
      <el-table-column prop="warnCode" label="报警代码" align="center" width="90" v-if="columns.showColumn('warnCode')" />
      <el-table-column prop="createTime" label="上报时间" align="center" width="160" v-if="columns.showColumn('createTime')" />
      <el-table-column label="操作" fixed="right" align="center" width="70">
        <template #default="scope">
          <el-button text icon="view" title="查看" @click="handleShowDetail(scope.row.equipmentId)"></el-button>
        </template>
      </el-table-column>
    </el-table>

    <el-row v-if="showMode !== 'table'">
      <el-card style="width: 270px; margin: 5px" v-for="item in dataList" @click="handleShowDetail(item.equipmentId)">
        <template #header>
          <div class="card-header">
            <span>{{ item.equipmentName }}({{ item.equipmentNo }})</span>
          </div>
        </template>
        <el-descriptions :column="1" label-width="70" size="small" border>
          <el-descriptions-item label="设备ID" v-if="columns.showColumn('equipmentId')" label-align="right">
            {{ item.equipmentId }}
          </el-descriptions-item>

          <el-descriptions-item label="资产编号" v-if="columns.showColumn('assetNo')" label-align="right">
            {{ item.assetNo }}
          </el-descriptions-item>

          <el-descriptions-item label="资产名称" v-if="columns.showColumn('assetName')" label-align="right">
            {{ item.assetName }}
          </el-descriptions-item>

          <el-descriptions-item label="运行状态" v-if="columns.showColumn('runState')" label-align="right">
            <dict-tag :options="options.equipment_run_state" :value="item.runState" />
          </el-descriptions-item>

          <el-descriptions-item label="线别" v-if="columns.showColumn('lineName')" label-align="right">
            {{ item.lineName }}
          </el-descriptions-item>

          <el-descriptions-item label="产能数量" v-if="columns.showColumn('productCount')" label-align="right">
            {{ item.productCount }}
          </el-descriptions-item>

          <el-descriptions-item label="不良数量" v-if="columns.showColumn('defectCount')" label-align="right">
            {{ item.defectCount }}
          </el-descriptions-item>

          <el-descriptions-item label="报警状态" v-if="columns.showColumn('warnState')" label-align="right">
            <dict-tag :options="options.equipment_warn_state" :value="item.warnState" />
          </el-descriptions-item>

          <el-descriptions-item label="报警代码" v-if="columns.showColumn('warnCode')" label-align="right">
            {{ item.warnCode }}
          </el-descriptions-item>

          <el-descriptions-item label="上报时间" v-if="columns.showColumn('createTime')" label-align="right">
            {{ item.createTime }}
          </el-descriptions-item>
        </el-descriptions>
      </el-card>
    </el-row>

    <!-- 添加或修改设备运行数据对话框 -->
    <el-dialog :title="title" :lock-scroll="false" v-model="open">
      <el-form :model="queryDetailParam" label-position="right" inline>
        <el-form-item label="设备" prop="equipmentName">
          <el-input :value="equipmentSummary.equipmentName + '(' + equipmentSummary.equipmentNo + ')'" disabled />
        </el-form-item>
        <el-form-item label="日期" prop="equipmentName">
          <el-date-picker
            v-model="dateRange"
            value-format="YYYY-MM-DD HH:mm:ss"
            type="datetimerange"
            range-separator="-"
            start-placeholder="开始日期"
            end-placeholder="结束日期"
            :default-time="defaultTime"
            @change="changeTagDate">
          </el-date-picker>
        </el-form-item>
      </el-form>

      <!-- 统计分析 -->
      <el-descriptions :column="4" title="统计分析" label-width="80" size="small" border>
        <el-descriptions-item label="OEE" label-align="right">
          <el-text type="primary"> {{ equipmentSummary.oee }}</el-text>
        </el-descriptions-item>
        <el-descriptions-item label="时间稼动率" label-align="right">
          <el-text type="primary"> {{ equipmentSummary.availabilityRate }}</el-text>
        </el-descriptions-item>
        <el-descriptions-item label="性能稼动率" label-align="right">
          <el-text type="primary"> {{ equipmentSummary.performanceRate }}</el-text>
        </el-descriptions-item>
        <el-descriptions-item label="良品率" label-align="right">
          <el-text type="primary"> {{ equipmentSummary.qualityRate }}</el-text>
        </el-descriptions-item>
        <el-descriptions-item label="计划节拍" label-align="right">
          <el-text type="primary"> {{ equipmentSummary.theoryCT }}</el-text>
          (秒)
        </el-descriptions-item>
        <el-descriptions-item label="实际产量" label-align="right">
          <el-text type="primary"> {{ equipmentSummary.productCount }}</el-text>
        </el-descriptions-item>
        <el-descriptions-item label="上报产量" label-align="right">
          <el-text type="primary"> {{ equipmentSummary.lastProductCount }}</el-text>
        </el-descriptions-item>
      </el-descriptions>

      <!-- 时间分析 -->
      <br />
      <el-descriptions :column="2" title="时间分析(分钟)" label-width="80" size="small" border>
        <el-descriptions-item label="开机时间" label-align="right">
          <el-text type="primary"> {{ Math.round((equipmentSummary.runSeconds / 60) * 10) / 10 }}</el-text>
        </el-descriptions-item>
        <el-descriptions-item label="计划停机时间" label-align="right">
          <el-text type="primary"> {{ Math.round((equipmentSummary.planEffectSeconds / 60) * 10) / 10 }}</el-text>
        </el-descriptions-item>
        <el-descriptions-item label="停机时间" label-align="right">
          <el-text type="primary"> {{ Math.round((equipmentSummary.stopSeconds / 60) * 10) / 10 }}</el-text>
        </el-descriptions-item>
        <el-descriptions-item label="故障时间" label-align="right">
          <el-text type="primary"> {{ Math.round((equipmentSummary.faultSeconds / 60) * 10) / 10 }}</el-text>
        </el-descriptions-item>
      </el-descriptions>

      <!-- 报警记录分析 -->
      <br />
      <el-text class="el-descriptions__title" size="large">报警记录({{ warnSummaryList.length }}次)</el-text>
      <el-table :data="warnSummaryList" border highlight-current-row :loading="loading" max-height="250">
        <el-table-column prop="warnDesc" label="报警代码/描述" align="center">
          <template #default="scope">
            {{ scope.row.warnCode + '/' + (scope.row.warnDesc ? scope.row.warnDesc : '未知') }}
          </template>
        </el-table-column>
        <el-table-column prop="dataStartTime" label="开始时间" align="center" />
        <el-table-column prop="dataEndTime" label="结束时间" align="center" />
        <el-table-column prop="faultSeconds" label="时间(秒)" align="center" />
      </el-table>
    </el-dialog>
  </div>
</template>

<script setup name="equipmentwatch">
import { listEquipmentRuningWatch, detailEquipmentRuningWatch } from '@/api/equipment/equipmentRuningRecord.js'
import { dictEquipmentBase } from '@/api/equipment/equipmentBase.js'
const { proxy } = getCurrentInstance()
const loading = ref(false)
const showSearch = ref(true)
const showMode = ref('table')
const queryParams = reactive({
  pageNum: 1,
  pageSize: 100,
  sort: 'CreateTime',
  sortType: 'desc',
  equipmentId: null,
  runState: undefined,
  createTime: undefined
})
const columns = ref([
  { visible: false, prop: 'equipmentId', label: '设备Id' },
  { visible: true, prop: 'assetNo', label: '资产编号' },
  { visible: false, prop: 'assetName', label: '资产名称' },
  { visible: true, prop: 'runState', label: '运行状态' },
  { visible: true, prop: 'lineName', label: '线别' },
  { visible: true, prop: 'productCount', label: '产能数量' },
  { visible: false, prop: 'defectCount', label: '不良数量' },
  { visible: true, prop: 'warnState', label: '报警状态' },
  { visible: false, prop: 'warnCode', label: '报警代码' },
  { visible: true, prop: 'createTime', label: '上报时间' }
])
const total = ref(0)
const dataList = ref([])
const queryRef = ref()
// 单个设备查询的日期时间范围
const dateRange = ref([])
const defaultTime = ref([new Date(2000, 1, 1, 8, 0, 0), new Date(2000, 2, 1, 20, 0, 0)])
// 创建时间时间范围
const dateRangeCreateTime = ref([])
const equipmentSummary = ref({})
const warnSummaryList = ref([])

var dictParams = [{ dictType: 'equipment_warn_state' }, { dictType: 'equipment_run_state' }]
proxy.getDicts(dictParams).then((response) => {
  response.data.forEach((element) => {
    state.options[element.dictType] = element.list
  })
})

function getList() {
  proxy.addDateRange(queryParams, dateRangeCreateTime.value, 'CreateTime')
  loading.value = true
  listEquipmentRuningWatch(queryParams).then((res) => {
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
  // 创建时间时间范围
  dateRangeCreateTime.value = []
  proxy.resetForm('queryRef')
  handleQuery()
}

/*************** form操作 ***************/
const open = ref(false)
const title = ref('')
const state = reactive({
  options: {
    //设备
    equipment_options: [],
    //设备报警状态
    equipment_warn_state: [],
    //设备运行状态
    equipment_run_state: []
  }
})

const { options } = toRefs(state)

function changeTagDate() {
  queryEquipmentWatchDetail()
}

// 关闭dialog
function cancel() {
  open.value = false
  reset()
}

// 重置表单
function reset() {
  form.value = {
    equipmentId: null,
    runState: null,
    productCount: null,
    defectCount: null,
    warnState: null,
    warnCode: null,
    createTime: null
  }
  proxy.resetForm('formRef')
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

const queryDetailParam = reactive({
  equipmentId: ''
})

//显示详情
function handleShowDetail(equipmentId) {
  queryDetailParam.equipmentId = equipmentId
  equipmentSummary.value = {}
  warnSummaryList.value = []
  title.value = '监控数据分析'
  open.value = true
  queryEquipmentWatchDetail()
}

function queryEquipmentWatchDetail() {
  loading.value = true
  proxy.addDateRange(queryDetailParam, dateRange.value, 'CreateTime')
  detailEquipmentRuningWatch(queryDetailParam).then((res) => {
    equipmentSummary.value = res.data
    warnSummaryList.value = res.data.statEquipmentRuningWarnNav
    loading.value = false
  })
}

handleQuery()
</script>
