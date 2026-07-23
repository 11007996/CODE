<!--
 * @Descripttion: (流程实例/WF_Process_Instance)
 * @Author: (admin)
 * @Date: (2024-06-13)
-->
<template>
  <div>
    <el-form :model="queryParams" label-position="right" inline ref="queryRef" v-show="showSearch" @submit.prevent>
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
      <right-toolbar v-model:showSearch="showSearch" @queryTable="getList" :columns="columns"></right-toolbar>
    </el-row>

    <el-table
      :data="dataList"
      v-loading="loading"
      ref="table"
      header-cell-class-name="el-table-header-cell"
      highlight-current-row
      @sort-change="sortChange">
      <el-table-column prop="processId" label="流程ID" align="center" :show-overflow-tooltip="true" v-if="columns.showColumn('processId')" />
      <el-table-column
        prop="processName"
        label="流程"
        width="150"
        align="left"
        :show-overflow-tooltip="true"
        v-if="columns.showColumn('processName')" />
      <el-table-column
        prop="processInstanceId"
        label="流程编号"
        width="200"
        align="center"
        :show-overflow-tooltip="true"
        v-if="columns.showColumn('processInstanceId')">
        <template #default="scope">
          <el-link type="primary" @click="openProcess(scope.row)">{{ scope.row.processInstanceId }}</el-link>
        </template>
      </el-table-column>
      <el-table-column prop="title" label="标题" min-width="320" align="left" :show-overflow-tooltip="true" v-if="columns.showColumn('title')" />
      <el-table-column prop="currentNodeId" label="当前节点ID" align="center" v-if="columns.showColumn('currentNodeId')" />
      <el-table-column prop="currentNodeName" label="当前节点" width="150" align="center" v-if="columns.showColumn('currentNodeName')" />
      <el-table-column prop="status" label="流程状态" width="100" align="center" v-if="columns.showColumn('status')">
        <template #default="scope">
          <dict-tag :options="options.process_status" :value="scope.row.status" />
        </template>
      </el-table-column>
      <el-table-column prop="initiatorId" label="发起人ID" width="100" align="center" v-if="columns.showColumn('initiatorId')" />
      <el-table-column prop="initiatorName" label="发起人" width="100" align="center" v-if="columns.showColumn('initiatorName')" />
      <el-table-column prop="createBy" label="创建人" align="center" :show-overflow-tooltip="true" v-if="columns.showColumn('createBy')" />
      <el-table-column prop="createTime" label="创建时间" width="170" :show-overflow-tooltip="true" v-if="columns.showColumn('createTime')" />
    </el-table>
    <pagination :total="total" v-model:page="queryParams.pageNum" v-model:limit="queryParams.pageSize" @pagination="getList" />
  </div>
</template>

<script setup name="processinstance">
import { listProcessInstanceByStatus } from '@/api/workflow/processInstance.js'

const props = defineProps({
  status: String
})
const { proxy } = getCurrentInstance()
const router = useRouter()
const loading = ref(false)
const showSearch = ref(false)
const queryParams = reactive({
  pageNum: 1,
  pageSize: 10,
  sort: '',
  sortType: 'asc',
  processInstanceId: undefined,
  status: props.status
})
const columns = ref([
  { visible: true, prop: 'processName', label: '流程' },
  { visible: true, prop: 'processInstanceId', label: '流程编号' },
  { visible: true, prop: 'title', label: '标题' },
  { visible: false, prop: 'processId', label: '流程ID' },
  { visible: false, prop: 'currentNodeId', label: '当前节点ID' },
  { visible: true, prop: 'currentNodeName', label: '当前节点' },
  { visible: false, prop: 'initiatorId', label: '发起人ID' },
  { visible: true, prop: 'initiatorName', label: '发起人' },
  { visible: true, prop: 'status', label: '流程状态' },
  { visible: false, prop: 'createBy', label: '创建人' },
  { visible: true, prop: 'createTime', label: '创建时间' }
])
const total = ref(0)
const dataList = ref([])
const queryRef = ref()

var dictParams = [{ dictType: 'process_status' }]
proxy.getDicts(dictParams).then((response) => {
  response.data.forEach((element) => {
    state.options[element.dictType] = element.list
  })
})

function getList() {
  loading.value = true
  listProcessInstanceByStatus(queryParams).then((res) => {
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
const state = reactive({
  options: {
    // 流程状态 选项列表 格式 eg:{ dictLabel: '标签', dictValue: '0'}
    process_status: []
  }
})

const { options } = toRefs(state)

//查看流程
function openProcess(row) {
  const instanceId = row.processInstanceId
  const processId = row.processId
  //使用resolve
  const url = router.resolve({
    path: '/process/' + processId,
    query: { instanceId: instanceId }
  })
  window.open(url.href, '_blank')
}

handleQuery()
</script>
