<!--
 * @Descripttion: (流程节点定义/WF_Node_Define)
 * @Author: (admin)
 * @Date: (2024-06-07)
-->
<template>
  <div>
    <el-form :model="queryParams" label-position="right" inline ref="queryRef" v-show="showSearch" @submit.prevent>
      <el-form-item label="流程" prop="processId">
        <el-select v-model="queryParams.processId" placeholder="请选择流程" clearable filterable remote :remote-method="handleQueryProcess">
          <el-option v-for="item in options.process_options" :key="item.dictValue" :label="item.dictLabel" :value="item.dictValue"></el-option>
        </el-select>
      </el-form-item>
      <el-form-item label="节点名称" prop="nodeName">
        <el-input v-model="queryParams.nodeName" placeholder="请输入节点名称" />
      </el-form-item>
      <el-form-item>
        <el-button icon="search" type="primary" @click="handleQuery">{{ $t('btn.search') }}</el-button>
        <el-button icon="refresh" @click="resetQuery">{{ $t('btn.reset') }}</el-button>
      </el-form-item>
    </el-form>
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
      <el-table-column prop="nodeId" label="节点ID" align="center" v-if="columns.showColumn('nodeId')" />
      <el-table-column prop="processId" label="流程ID" align="center" :show-overflow-tooltip="true" v-if="columns.showColumn('processId')" />
      <el-table-column
        prop="processName"
        label="流程名称"
        min-width="160"
        align="center"
        :show-overflow-tooltip="true"
        v-if="columns.showColumn('processName')" />
      <el-table-column
        prop="nodeName"
        label="节点名称"
        min-width="160"
        align="center"
        :show-overflow-tooltip="true"
        v-if="columns.showColumn('nodeName')" />
      <el-table-column prop="nodeType" label="节点类型" width="90" align="center" v-if="columns.showColumn('nodeType')">
        <template #default="scope">
          <dict-tag :options="options.process_node_type" :value="scope.row.nodeType" />
        </template>
      </el-table-column>
      <el-table-column prop="allowedActions" label="允许的操作" align="left" min-width="400" v-if="columns.showColumn('allowedActions')">
        <template #default="scope">
          <dict-tag :options="options.process_node_action" :value="scope.row.allowedActions" split="," />
        </template>
      </el-table-column>
      <el-table-column prop="orderNo" label="节点顺序" width="90" align="center" sortable v-if="columns.showColumn('orderNo')" />
      <el-table-column
        prop="createBy"
        label="创建人"
        width="90"
        align="center"
        :show-overflow-tooltip="true"
        v-if="columns.showColumn('createBy')" />
      <el-table-column prop="createTime" label="创建时间" width="160" :show-overflow-tooltip="true" v-if="columns.showColumn('createTime')" />
      <el-table-column
        prop="updateBy"
        label="更新人"
        width="90"
        align="center"
        :show-overflow-tooltip="true"
        v-if="columns.showColumn('updateBy')" />
      <el-table-column prop="updateTime" label="更新时间" width="160" :show-overflow-tooltip="true" v-if="columns.showColumn('updateTime')" />
      <el-table-column label="操作" width="300">
        <template #default="scope">
          <el-button type="primary" size="small" @click="handleSetNodeFlow(scope.row)">流向</el-button>
          <el-button type="primary" size="small" @click="handleSetNodeField(scope.row)">字段</el-button>
          <el-button type="primary" size="small" @click="handleSetNodeApprover(scope.row)">审批人</el-button>
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

    <!-- 添加或修改流程节点定义对话框 -->
    <el-dialog :title="title" :lock-scroll="false" v-model="open">
      <el-form ref="formRef" :model="form" :rules="rules" label-width="100px">
        <el-row :gutter="20">
          <el-col :lg="12">
            <el-form-item label="流程" prop="processId">
              <el-select
                v-model="form.processId"
                placeholder="请选择流程"
                clearable
                filterable
                remote
                :remote-method="handleQueryProcess"
                class="fullWidth">
                <el-option
                  v-for="item in options.process_options"
                  :key="item.dictValue"
                  :label="item.dictLabel"
                  :value="item.dictValue"></el-option>
              </el-select>
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="节点名称" prop="nodeName">
              <el-input v-model="form.nodeName" placeholder="请输入节点名称" class="fullWidth" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="节点类型" prop="nodeType">
              <el-select v-model="form.nodeType" placeholder="请选择节点类型" class="fullWidth">
                <el-option
                  v-for="item in options.process_node_type"
                  :key="item.dictValue"
                  :label="item.dictLabel"
                  :value="item.dictValue"></el-option>
              </el-select>
            </el-form-item>
          </el-col>

          <el-col :lg="24">
            <el-form-item label="允许的操作" prop="allowedActions">
              <el-select v-model="form.allowedActionsChecked" multiple placeholder="请选择允许的操作" class="fullWidth">
                <el-option
                  v-for="item in options.process_node_action"
                  :key="item.dictValue"
                  :label="item.dictLabel"
                  :value="item.dictValue"></el-option>
              </el-select>
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="节点顺序" prop="orderNo">
              <el-input-number
                v-model.number="form.orderNo"
                :controls="true"
                controls-position="right"
                placeholder="请输入节点顺序"
                class="fullWidth" />
            </el-form-item>
          </el-col>
        </el-row>
      </el-form>
      <template #footer v-if="opertype != 3">
        <el-button text @click="cancel">{{ $t('btn.cancel') }}</el-button>
        <el-button type="primary" @click="submitForm">{{ $t('btn.submit') }}</el-button>
      </template>
    </el-dialog>

    <!-- 节点流向 -->
    <el-dialog v-model="openSetNodeFlow">
      <NodeFlow :nodeId="selectedNodeId" />
    </el-dialog>

    <!-- 节点表单字段 -->
    <el-dialog v-model="openSetNodeField">
      <NodeFieldControl :nodeId="selectedNodeId" />
    </el-dialog>

    <!-- 审批人配置 -->
    <el-dialog v-model="openSetNodeApprover">
      <NodeApprover :nodeId="selectedNodeId" />
    </el-dialog>
  </div>
</template>

<script setup name="nodedefine">
import { listNodeDefine, detailListNodeDefine, addNodeDefine, delNodeDefine, updateNodeDefine, getNodeDefine } from '@/api/workflow/nodeDefine.js'
import { dictProcessDefine } from '@/api/workflow/processDefine.js'
import NodeFlow from './NodeFlow.vue'
import NodeFieldControl from './NodeFieldControl.vue'
import NodeApprover from './NodeApprover.vue'
const { proxy } = getCurrentInstance()
const router = useRouter()

const ids = ref([])
const loading = ref(false)
const showSearch = ref(true)
const queryParams = reactive({
  pageNum: 1,
  pageSize: 10,
  sort: 'OrderNo',
  sortType: 'asc',
  processId: undefined,
  nodeName: undefined
})
queryParams.processId = router.currentRoute.value.query.processId

const columns = ref([
  { visible: false, prop: 'nodeId', label: '节点ID' },
  { visible: false, prop: 'processId', label: '流程ID' },
  { visible: true, prop: 'processName', label: '流程名称' },
  { visible: true, prop: 'nodeName', label: '节点名称' },
  { visible: true, prop: 'nodeType', label: '节点类型' },
  { visible: true, prop: 'allowedActions', label: '允许的操作' },
  { visible: false, prop: 'orderNo', label: '节点顺序' },
  { visible: false, prop: 'createBy', label: '创建人' },
  { visible: false, prop: 'createTime', label: '创建时间' },
  { visible: false, prop: 'updateBy', label: '更新人' },
  { visible: false, prop: 'updateTime', label: '更新时间' }
])
const total = ref(0)
const dataList = ref([])
const queryRef = ref()
const defaultTime = ref([new Date(2000, 1, 1, 0, 0, 0), new Date(2000, 2, 1, 23, 59, 59)])

var dictParams = [{ dictType: 'process_node_type' }, { dictType: 'process_node_action' }]

proxy.getDicts(dictParams).then((response) => {
  response.data.forEach((element) => {
    state.options[element.dictType] = element.list
  })
})

function getList() {
  loading.value = true
  detailListNodeDefine(queryParams).then((res) => {
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

    if (column.prop == 'orderNo') {
      sort = 'order_No'
    }
  }
  queryParams.sort = sort
  queryParams.sortType = sortType
  handleQuery()
}

/**
 * 配置节点流向
 * @param {*} row
 */
const selectedNodeId = ref()
const openSetNodeFlow = ref(false)
function handleSetNodeFlow(row) {
  selectedNodeId.value = row.nodeId
  openSetNodeFlow.value = true
}

/**
 * 配置节点字段控制
 */
const openSetNodeField = ref(false)
function handleSetNodeField(row) {
  selectedNodeId.value = row.nodeId
  openSetNodeField.value = true
}

/**
 * 配置节点审批人
 */
const openSetNodeApprover = ref(false)
function handleSetNodeApprover(row) {
  selectedNodeId.value = row.nodeId
  openSetNodeApprover.value = true
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
    processId: [{ required: true, message: '流程ID不能为空', trigger: 'blur' }],
    nodeName: [{ required: true, message: '节点名称不能为空', trigger: 'blur' }],
    nodeType: [{ required: true, message: '节点类型不能为空', trigger: 'change' }],
    orderNo: [{ required: true, message: '节点顺序不能为空', trigger: 'blur', type: 'number' }]
  },
  options: {
    // 流程
    process_options: [],
    // 节点类型 选项列表 格式 eg:{ dictLabel: '标签', dictValue: '0'}
    process_node_type: [],
    // 允许的操作 选项列表 格式 eg:{ dictLabel: '标签', dictValue: '0'}
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
    nodeId: null,
    processId: null,
    nodeName: null,
    nodeType: null,
    allowedActionsChecked: [],
    orderNo: null,
    createBy: null,
    createTime: null,
    updateBy: null,
    updateTime: null
  }
  proxy.resetForm('formRef')
}

// 添加按钮操作
function handleAdd() {
  reset()
  open.value = true
  title.value = '添加流程节点定义'
  opertype.value = 1
}
// 修改按钮操作
function handleUpdate(row) {
  reset()
  const id = row.nodeId || ids.value
  getNodeDefine(id).then((res) => {
    const { code, data } = res
    if (code == 200) {
      open.value = true
      title.value = '修改流程节点定义'
      opertype.value = 2

      form.value = {
        ...data,
        allowedActionsChecked: data.allowedActions ? data.allowedActions.split(',') : []
      }
    }
  })
}

// 添加&修改 表单提交
function submitForm() {
  proxy.$refs['formRef'].validate((valid) => {
    if (valid) {
      form.value.allowedActions = form.value.allowedActionsChecked.toString()

      if (form.value.nodeId != undefined && opertype.value === 2) {
        updateNodeDefine(form.value).then((res) => {
          proxy.$modal.msgSuccess('修改成功')
          open.value = false
          getList()
        })
      } else {
        addNodeDefine(form.value).then((res) => {
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
  const Ids = row.nodeId || ids.value

  proxy
    .$confirm('是否确认删除参数编号为"' + Ids + '"的数据项？', '警告', {
      confirmButtonText: proxy.$t('common.ok'),
      cancelButtonText: proxy.$t('common.cancel'),
      type: 'warning'
    })
    .then(function () {
      return delNodeDefine(Ids)
    })
    .then(() => {
      getList()
      proxy.$modal.msgSuccess('删除成功')
    })
}

function handleQueryProcess(keyword) {
  if (keyword) {
    const queryPartParams = {
      pageNum: 1,
      pageSize: 10,
      sort: '',
      sortType: 'asc',
      processName: keyword
    }
    setTimeout(() => {
      dictProcessDefine(queryPartParams).then((res) => {
        state.options.process_options = res.data.result
      })
    }, 200)
  }
}

handleQuery()
</script>
