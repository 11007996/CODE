<!--
 * @Descripttion: (耗品库存记录/CON_Consumable_Storage_Record)
 * @Author: (admin)
 * @Date: (2024-05-20)
-->
<template>
  <div>
    <!-- 查询表单 -->
    <el-form :model="queryParams" label-position="right" inline ref="queryRef" v-show="showSearch" @submit.prevent>
      <el-form-item label="耗品" prop="consumableId">
        <el-select
          v-model="queryParams.consumableId"
          placeholder="请购料号,耗品名称,规格"
          clearable
          filterable
          remote
          :remote-method="handleQueryConsumable">
          <template #header>
            <span>请购料号 / 耗品名称 / 规格</span>
          </template>
          <el-option v-for="item in options.consumable_options" :key="item.dictValue" :label="item.dictLabel" :value="item.dictValue"></el-option>
        </el-select>
      </el-form-item>
      <el-form-item label="类别" prop="category">
        <el-select v-model="queryParams.category" clearable placeholder="请选择类别">
          <el-option v-for="item in options.category_options" :key="item.dictValue" :label="item.dictLabel" :value="item.dictValue"></el-option>
        </el-select>
      </el-form-item>
      <el-form-item label="储位" prop="storageId">
        <el-cascader
          class="w100"
          :options="useBasicStore().getConsumableStorageTree"
          :props="{ checkStrictly: true, value: 'storageId', label: 'storageName', emitPath: false }"
          placeholder="请选择储位"
          clearable
          v-model="queryParams.storageId">
          <template #default="{ node, data }">
            <span>{{ data.storageName }}</span>
            <span v-if="!node.isLeaf"> ({{ data.children.length }}) </span>
          </template>
        </el-cascader>
      </el-form-item>
      <el-form-item label="业务编号" prop="storageId">
        <el-input v-model="queryParams.ticketNo" placeholder="请输入业务编号" />
      </el-form-item>
      <el-form-item>
        <el-button icon="search" type="primary" @click="handleQuery">{{ $t('btn.search') }}</el-button>
        <el-button icon="refresh" @click="resetQuery">{{ $t('btn.reset') }}</el-button>
      </el-form-item>
    </el-form>

    <!-- 工具区域 -->
    <el-row :gutter="15" class="mb10">
      <el-col :span="1.5">
        <el-button type="warning" plain icon="download" @click="handleExport" v-hasPermi="['consumable:storage:export']">
          {{ $t('btn.export') }}
        </el-button>
      </el-col>
      <right-toolbar v-model:showSearch="showSearch" @queryTable="getList" :columns="columns"></right-toolbar>
    </el-row>

    <el-table
      :data="dataList"
      v-loading="loading"
      :cellStyle="cellStyle"
      ref="table"
      border
      header-cell-class-name="el-table-header-cell"
      highlight-current-row
      @sort-change="sortChange">
      <el-table-column prop="consumableId" label="耗品ID" width="90" align="center" v-if="columns.showColumn('consumableId')" />
      <el-table-column label="耗品（请购料号/名称/规格）" min-width="250" v-if="columns.showColumn('consumable')" :formatter="formatter" />
      <el-table-column prop="consumablePart" label="请购料号" v-if="columns.showColumn('consumablePart')" />
      <el-table-column prop="consumableName" label="耗品名称" v-if="columns.showColumn('consumableName')" />
      <el-table-column prop="spec" label="规格" v-if="columns.showColumn('spec')" />
      <el-table-column prop="storageId" label="储位ID" width="90" align="center" v-if="columns.showColumn('storageId')" />
      <el-table-column prop="storageFullName" label="储位" min-width="250" v-if="columns.showColumn('storageFullName')" />
      <el-table-column prop="beforeQty" label="原数量" align="center" width="90" v-if="columns.showColumn('beforeQty')" />
      <el-table-column prop="changeQty" label="变动数量" width="90" align="center" v-if="columns.showColumn('changeQty')" />
      <el-table-column prop="afterQty" label="结存数量" align="center" width="90" v-if="columns.showColumn('afterQty')" />
      <el-table-column prop="storageChangeType" label="变动类型" width="90" align="center" v-if="columns.showColumn('storageChangeType')">
        <template #default="scope">
          <dict-tag :options="options.storage_change_type" :value="scope.row.storageChangeType" />
        </template>
      </el-table-column>
      <el-table-column prop="relatedUser" label="相关人Id" align="center" width="90" v-if="columns.showColumn('relatedUser')" />
      <el-table-column prop="relatedUserName" label="相关人" align="center" width="90" v-if="columns.showColumn('relatedUserName')" />
      <el-table-column
        prop="ticketNo"
        label="业务编号"
        width="160"
        align="center"
        :show-overflow-tooltip="true"
        v-if="columns.showColumn('ticketNo')" />
      <el-table-column prop="ticketType" label="单据类型" width="160" align="center" v-if="columns.showColumn('ticketType')">
        <template #default="scope">
          <dict-tag :options="options.ticket_type" :value="scope.row.ticketType" />
        </template>
      </el-table-column>
      <el-table-column prop="remark" label="备注" align="center" :show-overflow-tooltip="true" v-if="columns.showColumn('remark')" />
      <el-table-column
        prop="createBy"
        label="创建人"
        width="90"
        align="center"
        :show-overflow-tooltip="true"
        v-if="columns.showColumn('createBy')" />
      <el-table-column prop="createTime" label="创建时间" width="160" :show-overflow-tooltip="true" v-if="columns.showColumn('createTime')" />
    </el-table>
    <pagination :total="total" v-model:page="queryParams.pageNum" v-model:limit="queryParams.pageSize" @pagination="getList" />
  </div>
</template>

<script setup name="consumablestoragerecord">
import { listConsumableStorageRecord } from '@/api/consumable/consumableStorage.js'
import { dictConsumableBase, dictConsumableCategory } from '@/api/consumable/consumableBase.js'
import useBasicStore from '@/store/modules/basic.js'
const props = defineProps({
  consumableId: Number,
  ticketNo: String
})

const { proxy } = getCurrentInstance()
const loading = ref(false)
const showSearch = ref(true)
if (props.consumableId) showSearch.value = false
const queryParams = reactive({
  pageNum: 1,
  pageSize: 10,
  sort: 'createtime',
  sortType: 'desc',
  consumableId: props.consumableId,
  storageId: null,
  ticketNo: props.ticketNo
})
const columns = ref([
  { visible: false, prop: 'consumableId', label: '耗品ID' },
  { visible: true, prop: 'consumable', label: '耗品' },
  { visible: false, prop: 'consumablePart', label: '请购料号' },
  { visible: false, prop: 'consumableName', label: '耗品名称' },
  { visible: false, prop: 'spec', label: '规格' },
  { visible: false, prop: 'storageId', label: '储位ID' },
  { visible: true, prop: 'storageFullName', label: '储位' },
  { visible: true, prop: 'beforeQty', label: '原数量' },
  { visible: true, prop: 'changeQty', label: '变动数量' },
  { visible: true, prop: 'afterQty', label: '新库存' },
  { visible: true, prop: 'storageChangeType', label: '变动类型' },
  { visible: false, prop: 'relatedUser', label: '相关人Id' },
  { visible: true, prop: 'relatedUserName', label: '相关人' },
  { visible: false, prop: 'ticketNo', label: '业务编号' },
  { visible: false, prop: 'ticketType', label: '单据类型' },
  { visible: false, prop: 'remark', label: '备注' },
  { visible: false, prop: 'createBy', label: '创建人' },
  { visible: true, prop: 'createTime', label: '创建时间' }
])
const total = ref(0)
const dataList = ref([])

var dictParams = [{ dictType: 'storage_change_type' }, { dictType: 'ticket_type' }]

proxy.getDicts(dictParams).then((response) => {
  response.data.forEach((element) => {
    state.options[element.dictType] = element.list
  })
})

function getList() {
  loading.value = true
  listConsumableStorageRecord(queryParams).then((res) => {
    const { code, data } = res
    if (code == 200) {
      dataList.value = data.result
      total.value = data.totalNum
      loading.value = false
    }
  })
}

function formatter(row, cloumn) {
  return row.consumablePart + ' / ' + row.consumableName + ' / ' + row.spec
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

const state = reactive({
  options: {
    // 变动类型 选项列表 格式 eg:{ dictLabel: '标签', dictValue: '0'}
    storage_change_type: [],
    // 单据类型 选项列表 格式 eg:{ dictLabel: '标签', dictValue: '0'}
    ticket_type: [],
    // 耗品选项
    consumable_options: []
  }
})

const { options } = toRefs(state)

//动态设置单元格颜色
const cellStyle = ({ row, column, rowIndex, columnIndex }) => {
  if (column.property == 'changeQty') {
    if (row.changeQty >= 0) {
      return { color: 'blue' }
    } else {
      return { color: 'red' }
    }
  }
}

// 导出按钮操作
function handleExport() {
  proxy
    .$confirm('是否确认导出耗品存储记录数据项?', '警告', {
      confirmButtonText: '确定',
      cancelButtonText: '取消',
      type: 'warning'
    })
    .then(async () => {
      await proxy.downFile('/consumable/consumableStorage/record/export', { ...queryParams })
    })
}

//********************************其他方法************************* */
//耗品查询
function handleQueryConsumable(keyword) {
  if (keyword) {
    const query = {
      pageNum: 1,
      pageSize: 10,
      sort: '',
      sortType: 'asc',
      keyword: keyword
    }
    if (keyword.indexOf(',') >= 0) {
      const kv = keyword.split(',')
      query.consumablePart = kv[0]
      query.consumableName = kv[1]
      query.spec = kv[2]
      query.keyword = null
    }
    setTimeout(() => {
      dictConsumableBase(query).then((res) => {
        options.value.consumable_options = res.data.result
      })
    }, 200)
  }
}

function handleQueryCategory() {
  const parm = {
    pageNum: 1,
    pageSize: 100,
    sort: '',
    sortType: 'asc'
  }
  dictConsumableCategory(parm).then((res) => {
    const { code, data } = res
    if (code == 200) {
      options.value.category_options = data.result
    }
  })
}

//监听属性变化
watch(props, (newVal) => {
  queryParams.consumableId = props.consumableId
  queryParams.ticketNo = props.ticketNo
  handleQuery()
})

handleQueryCategory()
handleQuery()
</script>
