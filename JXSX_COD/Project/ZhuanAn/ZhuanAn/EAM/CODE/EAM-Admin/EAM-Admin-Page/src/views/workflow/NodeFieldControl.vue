<!--
 * @Descripttion: (节点字段控件配置/WF_Node_Field_Control)
 * @Author: (admin)
 * @Date: (2024-06-11)
-->
<template>
  <div>
    <el-table
      :data="dataList"
      v-loading="loading"
      ref="table"
      header-cell-class-name="el-table-header-cell"
      highlight-current-row
      @sort-change="sortChange">
      <el-table-column
        prop="fieldName"
        label="字段名称"
        align="center"
        min-width="160"
        :show-overflow-tooltip="true"
        v-if="columns.showColumn('fieldName')" />
      <el-table-column
        prop="fieldDesc"
        label="字段描述"
        align="center"
        min-width="160"
        :show-overflow-tooltip="true"
        v-if="columns.showColumn('fieldDesc')" />
      <el-table-column prop="hidden" label="是否隐藏" align="center" width="90" v-if="columns.showColumn('hidden')">
        <template #default="scope">
          <el-switch v-model="scope.row.hidden" :active-value="true" :inactive-value="false" />
        </template>
      </el-table-column>
      <el-table-column prop="editable" label="是否编辑" align="center" width="90" v-if="columns.showColumn('editable')">
        <template #default="scope">
          <el-switch v-model="scope.row.editable" :active-value="true" :inactive-value="false" />
        </template>
      </el-table-column>
      <el-table-column prop="required" label="是否必填" align="center" width="90" v-if="columns.showColumn('required')">
        <template #default="scope">
          <el-switch v-model="scope.row.required" :active-value="true" :inactive-value="false" />
        </template>
      </el-table-column>
    </el-table>

    <el-row :gutter="15" class="mb10 mt10" type="flex" justify="end">
      <el-col off :span="3">
        <el-button type="primary" @click="submitForm">{{ $t('btn.submit') }}</el-button>
      </el-col>
    </el-row>
  </div>
</template>

<script setup name="nodefieldcontrol">
import { detailListNodeFieldControl, batchUpdateNodeFieldControl } from '@/api/workflow/nodeDefine.js'

const props = defineProps({
  nodeId: Number
})
const { proxy } = getCurrentInstance()
const loading = ref(false)
const queryParams = reactive({
  pageNum: 1,
  pageSize: 10,
  sort: '',
  sortType: 'asc',
  nodeId: props.nodeId
})
const columns = ref([
  { visible: true, prop: 'fieldName', label: '字段名称' },
  { visible: true, prop: 'fieldDesc', label: '字段描述' },
  { visible: true, prop: 'nodeId', label: '节点名称' },
  { visible: true, prop: 'hidden', label: '是否隐藏' },
  { visible: true, prop: 'editable', label: '是否编辑' },
  { visible: true, prop: 'required', label: '是否必填' }
])
const total = ref(0)
const dataList = ref([])

function getList() {
  loading.value = true
  detailListNodeFieldControl(queryParams).then((res) => {
    const { code, data } = res
    if (code == 200) {
      dataList.value = data
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

// 添加&修改 表单提交
function submitForm() {
  batchUpdateNodeFieldControl(dataList.value).then((res) => {
    proxy.$modal.msgSuccess('修改成功')
    getList()
  })
}

handleQuery()

//监听传递的值是否变化，更新内容
watch(props, (newValue) => {
  queryParams.nodeId = newValue.nodeId
  handleQuery()
})
</script>
