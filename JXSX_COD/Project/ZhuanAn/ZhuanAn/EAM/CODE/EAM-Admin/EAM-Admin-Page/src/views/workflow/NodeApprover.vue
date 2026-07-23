<!--
 * @Descripttion: (节点审批人配置/WF_Node_Approver)
 * @Author: (admin)
 * @Date: (2024-06-11)
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
      <el-table-column prop="approverType" label="审批人类型" min-width="120" align="center" v-if="columns.showColumn('approverType')">
        <template #default="scope">
          <dict-tag :options="options.process_approver_type" :value="scope.row.approverType" />
        </template>
      </el-table-column>
      <el-table-column prop="approverValue" label="审批类型值" min-width="120" align="center" v-if="columns.showColumn('approverValue')">
        <template #default="scope">
          <dict-tag :options="state.options.approver_options" :value="scope.row.approverValue.toString()" />
        </template>
      </el-table-column>
      <el-table-column prop="approverDesc" label="审批人描述" min-width="200" align="center" v-if="columns.showColumn('approverDesc')" />
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

    <!-- 添加或修改节点审批人配置对话框 -->
    <el-dialog :title="title" :lock-scroll="false" v-model="open">
      <el-form ref="formRef" :model="form" :rules="rules" label-width="100px">
        <el-row :gutter="20">
          <!-- <el-col :lg="12">
            <el-form-item label="节点ID" prop="nodeId">
              <el-input v-model.number="form.nodeId" placeholder="请输入节点ID" :disabled="opertype != 1" />
            </el-form-item>
          </el-col> -->

          <el-col :lg="12">
            <el-form-item label="审批人类型" prop="approverType">
              <el-select v-model="form.approverType" placeholder="请选择审批人类型" :change="handleQueryApproverDict">
                <el-option
                  v-for="item in options.process_approver_type"
                  :key="item.dictValue"
                  :label="item.dictLabel"
                  :value="item.dictValue"></el-option>
              </el-select>
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="审批类型值" prop="approverValue">
              <el-select
                v-model="form.approverValue"
                placeholder="请选择审批类型值"
                clearable
                filterable
                remote
                :no-data-text="form.approverDesc"
                :remote-method="handleQueryApproverDict">
                <el-option
                  v-for="item in options.approver_options"
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
  </div>
</template>

<script setup name="nodeapprover">
import {
  listNodeApprover,
  dictNodeApprover,
  addNodeApprover,
  delNodeApprover,
  updateNodeApprover,
  getNodeApprover
} from '@/api/workflow/nodeDefine.js'
const { proxy } = getCurrentInstance()
const props = defineProps({
  nodeId: Number
})
const ids = ref([])
const loading = ref(false)
const showSearch = ref(true)
const queryParams = reactive({
  pageNum: 1,
  pageSize: 10,
  sort: '',
  sortType: 'asc',
  nodeId: props.nodeId
})
const columns = ref([
  { visible: false, prop: 'nodeId', label: '节点ID' },
  { visible: true, prop: 'approverType', label: '审批人类型' },
  { visible: false, prop: 'approverValue', label: '审批类型值' },
  { visible: true, prop: 'approverDesc', label: '审批人说明' }
])
const total = ref(0)
const dataList = ref([])
const queryRef = ref()
const defaultTime = ref([new Date(2000, 1, 1, 0, 0, 0), new Date(2000, 2, 1, 23, 59, 59)])

var dictParams = [{ dictType: 'process_approver_type' }]

proxy.getDicts(dictParams).then((response) => {
  response.data.forEach((element) => {
    state.options[element.dictType] = element.list
  })
})

function getList() {
  loading.value = true
  listNodeApprover(queryParams).then((res) => {
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
    nodeId: [{ required: true, message: '节点ID不能为空', trigger: 'blur', type: 'number' }],
    approverType: [{ required: true, message: '审批人类型不能为空', trigger: 'change' }],
    approverValue: [{ required: true, message: '审批类型值不能为空', trigger: 'blur' }]
  },
  options: {
    // 审批人类型 选项列表 格式 eg:{ dictLabel: '标签', dictValue: '0'}
    process_approver_type: [],
    // 审批人
    approver_options: []
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
    nodeId: props.nodeId,
    approverType: null,
    approverValue: null,
    approverDesc: null
  }
  proxy.resetForm('formRef')
}

// 添加按钮操作
function handleAdd() {
  reset()
  open.value = true
  title.value = '添加节点审批人配置'
  opertype.value = 1
}
// 修改按钮操作
function handleUpdate(row) {
  reset()
  const id = row.nodeId || ids.value
  getNodeApprover(id).then((res) => {
    const { code, data } = res
    if (code == 200) {
      open.value = true
      title.value = '修改节点审批人配置'
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
      form.value.approverDesc = state.options.approver_options.find((it) => it.dictValue == form.value.approverValue).dictLabel
      if (form.value.nodeId != undefined && opertype.value === 2) {
        updateNodeApprover(form.value).then((res) => {
          proxy.$modal.msgSuccess('修改成功')
          open.value = false
          getList()
        })
      } else {
        addNodeApprover(form.value).then((res) => {
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
      return delNodeApprover(Ids)
    })
    .then(() => {
      getList()
      proxy.$modal.msgSuccess('删除成功')
    })
}

//根据审批人类型查询字典选项
function handleQueryApproverDict(keyword) {
  const dictType = form.value.approverType
  const queryParams = {
    pageNum: 1,
    pageSize: 20,
    sort: '',
    sortType: 'asc',
    keyword: keyword,
    approverType: dictType,
    nodeId: props.nodeId
  }
  dictNodeApprover(queryParams).then((res) => {
    const { code, data } = res
    if (code == 200) {
      state.options.approver_options = data.result
    }
  })
}

//监听传递的值是否变化，更新内容
watch(props, (newValue) => {
  queryParams.nodeId = newValue.nodeId
  handleQuery()
})

handleQuery()
</script>
