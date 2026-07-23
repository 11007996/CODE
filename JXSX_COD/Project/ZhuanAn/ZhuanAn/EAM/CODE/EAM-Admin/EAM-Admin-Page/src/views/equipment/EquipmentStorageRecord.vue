<!--
 * @Descripttion: (设备保管记录/EQU_Equipment_Storage_Record)
 * @Author: (admin)
 * @Date: (2024-12-04)
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
      <el-form-item label="业务编号" prop="ticketNo">
        <el-input v-model="queryParams.ticketNo" placeholder="请输入业务编号" />
      </el-form-item>
      <el-form-item label="创建时间">
        <el-date-picker
          style="width: 200px"
          v-model="dateRangeCreateTime"
          type="daterange"
          start-placeholder="Start date"
          end-placeholder="End date"
          value-format="YYYY-MM-DD HH:mm:ss"
          :default-time="defaultTime">
        </el-date-picker>
      </el-form-item>
      <el-form-item>
        <el-button icon="search" type="primary" @click="handleQuery">{{ $t('btn.search') }}</el-button>
        <el-button icon="refresh" @click="resetQuery">{{ $t('btn.reset') }}</el-button>
      </el-form-item>
    </el-form>
    <!-- 工具区域 -->
    <el-row :gutter="15" class="mb10">
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
      <el-table-column prop="equipmentId" label="设备ID" align="center" v-if="columns.showColumn('equipmentId')" />
      <el-table-column prop="assetNo" label="资产编号" align="center" width="210" v-if="columns.showColumn('assetNo')" />
      <el-table-column prop="assetName" label="资产名称" align="center" min-width="200" v-if="columns.showColumn('assetName')" />
      <el-table-column prop="lineId" label="产线ID" align="center" width="90" :show-overflow-tooltip="true" v-if="columns.showColumn('lineId')" />
      <el-table-column prop="lineName" label="产线" align="center" width="90" v-if="columns.showColumn('lineName')" />
      <el-table-column prop="receiverId" label="领用人ID" align="center" width="90" v-if="columns.showColumn('receiverId')" />
      <el-table-column prop="receiverName" label="领用人" align="center" width="90" v-if="columns.showColumn('receiverName')" />
      <el-table-column prop="storageChangeType" label="变动类型" align="center" width="90" v-if="columns.showColumn('storageChangeType')">
        <template #default="scope">
          <dict-tag :options="options.storage_change_type" :value="scope.row.storageChangeType" />
        </template>
      </el-table-column>
      <el-table-column
        prop="ticketNo"
        label="业务编号"
        align="center"
        width="160"
        :show-overflow-tooltip="true"
        v-if="columns.showColumn('ticketNo')" />
      <el-table-column prop="ticketType" label="单据类型" align="center" min-width="150" v-if="columns.showColumn('ticketType')">
        <template #default="scope">
          <dict-tag :options="options.ticket_type" :value="scope.row.ticketType" />
        </template>
      </el-table-column>
      <el-table-column prop="createTime" label="创建时间" align="center" width="160" v-if="columns.showColumn('createTime')" />
      <el-table-column prop="createBy" label="创建人" align="center" width="90" v-if="columns.showColumn('createBy')" />
      <el-table-column prop="remark" label="备注" :show-overflow-tooltip="true" v-if="columns.showColumn('remark')" />
    </el-table>
    <pagination :total="total" v-model:page="queryParams.pageNum" v-model:limit="queryParams.pageSize" @pagination="getList" />
  </div>
</template>

<script setup name="equipmentstoragerecord">
import { listEquipmentStorageRecord } from '@/api/equipment/equipmentStorage.js'
import { dictEquipmentBase } from '@/api/equipment/equipmentBase.js'

const props = defineProps({
  equipmentId: String,
  ticketNo: String
})
watch(props, (val) => {
  queryParams.equipmentId = props.equipmentId
  queryParams.ticketNo = props.ticketNo
  getList()
})
const { proxy } = getCurrentInstance()
const loading = ref(false)
const showSearch = ref(true)
const queryParams = reactive({
  pageNum: 1,
  pageSize: 10,
  sort: 'CreateTime',
  sortType: 'desc',
  equipmentId: props.equipmentId,
  ticketNo: props.ticketNo,
  createTime: undefined
})
const columns = ref([
  { visible: false, prop: 'equipmentId', label: '设备ID' },
  { visible: true, prop: 'assetNo', label: '资产编号' },
  { visible: true, prop: 'assetName', label: '资产名称' },
  { visible: false, prop: 'lineId', label: '产线ID' },
  { visible: true, prop: 'lineName', label: '产线' },
  { visible: false, prop: 'receiverId', label: '领用人ID' },
  { visible: true, prop: 'receiverName', label: '领用人' },
  { visible: true, prop: 'storageChangeType', label: '变动类型' },
  { visible: true, prop: 'ticketNo', label: '业务编号' },
  { visible: true, prop: 'ticketType', label: '单据类型' },
  { visible: true, prop: 'createTime', label: '创建时间' },
  { visible: false, prop: 'createBy', label: '创建人' },
  { visible: false, prop: 'remark', label: '备注' }
])
const total = ref(0)
const dataList = ref([])
const queryRef = ref()
const defaultTime = ref([new Date(2000, 1, 1, 0, 0, 0), new Date(2000, 2, 1, 23, 59, 59)])

// 创建时间时间范围
const dateRangeCreateTime = ref([])

var dictParams = [{ dictType: 'storage_change_type' }, { dictType: 'ticket_type' }]

proxy.getDicts(dictParams).then((response) => {
  response.data.forEach((element) => {
    state.options[element.dictType] = element.list
  })
})

function getList() {
  proxy.addDateRange(queryParams, dateRangeCreateTime.value, 'CreateTime')
  loading.value = true
  listEquipmentStorageRecord(queryParams).then((res) => {
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
const state = reactive({
  options: {
    // 变动类型 选项列表 格式 eg:{ dictLabel: '标签', dictValue: '0'}
    storage_change_type: [],
    // 单据类型 选项列表 格式 eg:{ dictLabel: '标签', dictValue: '0'}
    ticket_type: []
  }
})

const { options } = toRefs(state)

// 查询资产编号
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

handleQuery()
</script>
