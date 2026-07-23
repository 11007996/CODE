<!--
 * @Descripttion: (节点流向/WF_Node_Flow)
 * @Author: (admin)
 * @Date: (2024-06-07)
-->
<template>
  <div>
    <!-- 工具区域 -->
    <el-row :gutter="15" class="mb10">
      <el-col :span="1.5">
        <el-button type="primary" v-hasPermi="['process:add']" plain icon="plus" @click="handleAdd">
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
      <el-table-column prop="nodeFlowId" label="节点流向ID" align="center" v-if="columns.showColumn('nodeFlowId')" />
      <el-table-column prop="fromNodeId" label="来源节点" min-width="160" align="center" v-if="columns.showColumn('fromNodeId')">
        <template #default="scope">
          <dict-tag :options="options.node_options" :value="scope.row.fromNodeId" />
        </template>
      </el-table-column>
      <el-table-column prop="actionType" label="操作类型" width="90" align="center" v-if="columns.showColumn('actionType')">
        <template #default="scope">
          <dict-tag :options="options.process_node_action" :value="scope.row.actionType" />
        </template>
      </el-table-column>
      <el-table-column prop="toNodeId" label="目标节点" min-width="160" align="center" v-if="columns.showColumn('toNodeId')">
        <template #default="scope">
          <dict-tag :options="options.node_options" :value="scope.row.toNodeId" />
        </template>
      </el-table-column>
      <el-table-column
        prop="conditionExpression"
        label="条件表达式"
        align="center"
        :show-overflow-tooltip="true"
        v-if="columns.showColumn('conditionExpression')" />
      <el-table-column label="操作" width="140">
        <template #default="scope">
          <el-button
            type="success"
            size="small"
            icon="edit"
            title="编辑"
            v-hasPermi="['process:edit']"
            @click="handleUpdate(scope.row)"></el-button>
          <el-button
            type="danger"
            size="small"
            icon="delete"
            title="删除"
            v-hasPermi="['process:delete']"
            @click="handleDelete(scope.row)"></el-button>
        </template>
      </el-table-column>
    </el-table>
    <pagination :total="total" v-model:page="queryParams.pageNum" v-model:limit="queryParams.pageSize" @pagination="getList" />

    <!-- 添加或修改节点流向对话框 -->
    <el-dialog :title="title" :lock-scroll="false" width="400px" v-model="open">
      <el-form ref="formRef" :model="form" :rules="rules" label-width="100px">
        <el-row :gutter="20">
          <el-col :lg="24">
            <el-form-item label="来源节点" prop="fromNodeId">
              <el-select v-model="form.fromNodeId" placeholder="请选择来源节点" class="fullWidth" disabled>
                <el-option
                  v-for="item in options.node_options"
                  :key="item.dictValue"
                  :label="item.dictLabel"
                  :value="parseInt(item.dictValue)"></el-option>
              </el-select>
            </el-form-item>
          </el-col>

          <el-col :lg="24">
            <el-form-item label="操作类型" prop="actionType">
              <el-select v-model="form.actionType" placeholder="请选择操作类型" class="fullWidth">
                <template v-for="item in options.process_node_action">
                  <el-option
                    v-if="nodeInfo.value.allowedActions.indexOf(item.dictValue) >= 0"
                    :key="item.dictValue"
                    :label="item.dictLabel"
                    :value="item.dictValue"></el-option
                ></template>
              </el-select>
            </el-form-item>
          </el-col>

          <el-col :lg="24">
            <el-form-item label="目标节点" prop="toNodeId">
              <el-select v-model="form.toNodeId" placeholder="请选择目标节点" class="fullWidth">
                <template v-for="item in options.node_options">
                  <el-option
                    v-if="nodeInfo.value.nodeId != item.dictValue"
                    :key="item.dictValue"
                    :label="item.dictLabel"
                    :value="parseInt(item.dictValue)"></el-option>
                </template>
              </el-select>
            </el-form-item>
          </el-col>

          <el-col :lg="24">
            <el-form-item label="条件表达式" prop="conditionExpression">
              <el-input v-model="form.conditionExpression" placeholder="请输入条件表达式" />
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

<script setup name="nodeflow">
import { listNodeFlow, addNodeFlow, delNodeFlow, updateNodeFlow, getNodeFlow } from '@/api/workflow/nodeDefine.js'
import { getNodeDefine, dictNodeDefine } from '@/api/workflow/nodeDefine.js'

const props = defineProps({
  nodeId: Number
})
//当前来源节点的信息
const nodeInfo = reactive({})
const { proxy } = getCurrentInstance()
const ids = ref([])
const loading = ref(false)
const showSearch = ref(true)
const queryParams = reactive({
  pageNum: 1,
  pageSize: 10,
  sort: '',
  sortType: 'asc',
  fromNodeId: props.nodeId
})
const columns = ref([
  { visible: false, prop: 'nodeFlowId', label: '节点流向ID' },
  { visible: false, prop: 'fromNodeId', label: '来源节点' },
  { visible: true, prop: 'actionType', label: '操作类型' },
  { visible: true, prop: 'toNodeId', label: '目标节点' },
  { visible: true, prop: 'conditionExpression', label: '条件表达式' }
])
const total = ref(0)
const dataList = ref([])
const queryRef = ref()

var dictParams = [{ dictType: 'process_node_action' }]

proxy.getDicts(dictParams).then((response) => {
  response.data.forEach((element) => {
    state.options[element.dictType] = element.list
  })
})

function getList() {
  loading.value = true
  listNodeFlow(queryParams).then((res) => {
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
    fromNodeId: [{ required: true, message: '来源节点不能为空', trigger: 'change', type: 'number' }],
    actionType: [{ required: true, message: '操作类型不能为空', trigger: 'change' }],
    toNodeId: [{ required: true, message: '目标节点不能为空', trigger: 'change', type: 'number' }]
  },
  options: {
    // 来源节点 选项列表 格式 eg:{ dictLabel: '标签', dictValue: '0'}
    node_options: [],
    // 操作类型 选项列表 格式 eg:{ dictLabel: '标签', dictValue: '0'}
    process_node_action: []
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
    nodeFlowId: null,
    fromNodeId: props.nodeId,
    actionType: null,
    toNodeId: null,
    conditionExpression: null
  }
  proxy.resetForm('formRef')
}

// 添加按钮操作
function handleAdd() {
  reset()
  open.value = true
  title.value = '添加节点流向'
  opertype.value = 1
}
// 修改按钮操作
function handleUpdate(row) {
  reset()
  const id = row.nodeFlowId || ids.value
  getNodeFlow(id).then((res) => {
    const { code, data } = res
    if (code == 200) {
      open.value = true
      title.value = '修改节点流向'
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
      if (form.value.nodeFlowId != undefined && opertype.value === 2) {
        updateNodeFlow(form.value).then((res) => {
          proxy.$modal.msgSuccess('修改成功')
          open.value = false
          getList()
        })
      } else {
        addNodeFlow(form.value).then((res) => {
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
  const Ids = row.nodeFlowId || ids.value

  proxy
    .$confirm('是否确认删除参数编号为"' + Ids + '"的数据项？', '警告', {
      confirmButtonText: proxy.$t('common.ok'),
      cancelButtonText: proxy.$t('common.cancel'),
      type: 'warning'
    })
    .then(function () {
      return delNodeFlow(Ids)
    })
    .then(() => {
      getList()
      proxy.$modal.msgSuccess('删除成功')
    })
}

//获取节点信息
async function getNodeInfo() {
  const id = props.nodeId
  await getNodeDefine(id).then((res) => {
    const { code, data } = res
    if (code == 200) {
      nodeInfo.value = { ...data }
      getDictNodes()
    }
  })
}

//获取当前流程的所有可选节点
async function getDictNodes() {
  const queryParams2 = { processId: nodeInfo.value.processId, nodeId: props.nodeId }
  await dictNodeDefine(queryParams2).then((res) => {
    const { code, data } = res
    if (code == 200) {
      state.options['node_options'] = data
    }
  })
}

getNodeInfo()
handleQuery()

//监听传递的值是否变化，更新内容
watch(props, (newValue) => {
  queryParams.fromNodeId = newValue.nodeId
  getNodeInfo()
  handleQuery()
})
</script>
