<!--
 * @Descripttion: (流程定义/WF_Process_Define)
 * @Author: (admin)
 * @Date: (2024-06-04)
-->
<template>
  <div>
    <el-form :model="queryParams" label-position="right" inline ref="queryRef" v-show="showSearch" @submit.prevent>
      <el-form-item label="流程名称" prop="processName">
        <el-input v-model="queryParams.processName" placeholder="请输入流程名称" />
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
      <el-table-column prop="processId" label="流程编号" width="90" align="center" v-if="columns.showColumn('processId')" />
      <el-table-column
        prop="processName"
        label="流程名称"
        min-width="160"
        align="center"
        :show-overflow-tooltip="true"
        v-if="columns.showColumn('processName')" />
      <el-table-column prop="description" label="流程描述" min-width="160" :show-overflow-tooltip="true" v-if="columns.showColumn('description')" />
      <el-table-column prop="status" label="状态" width="90" align="center" v-if="columns.showColumn('status')">
        <template #default="scope">
          <dict-tag :options="options.sys_normal_disable" :value="scope.row.status" />
        </template>
      </el-table-column>
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
      <el-table-column label="操作" width="170">
        <template #default="scope">
          <el-button type="primary" size="small" @click="handlePreviewNode(scope.row)">节点</el-button>
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

    <!-- 添加或修改流程定义对话框 -->
    <el-dialog :title="title" :lock-scroll="false" v-model="open">
      <el-form ref="formRef" :model="form" :rules="rules" label-width="100px">
        <el-row :gutter="20">
          <el-col :lg="12">
            <el-form-item label="流程编号" prop="processId">
              <el-input v-model.number="form.processId" placeholder="请输入流程编号" :disabled="opertype != 1" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="流程名称" prop="processName">
              <el-input v-model="form.processName" placeholder="请输入流程名称" />
            </el-form-item>
          </el-col>

          <el-col :lg="24">
            <el-form-item label="流程描述" prop="description">
              <el-input v-model="form.description" placeholder="请输入流程描述" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="关联表单" prop="formId">
              <el-select v-model="form.formId" placeholder="请选择表单模板" clearable filterable class="fullWidth">
                <el-option
                  v-for="item in options.form_options"
                  :key="item.dictValue"
                  :label="item.dictLabel"
                  :value="parseInt(item.dictValue)"></el-option>
              </el-select>
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="流程模板" prop="processTemplate">
              <el-select v-model="form.processTemplate" placeholder="请选择流程模板" clearable filterable class="fullWidth">
                <el-option
                  v-for="item in options.process_template"
                  :key="item.dictValue"
                  :label="item.dictLabel"
                  :value="item.dictValue"></el-option>
              </el-select>
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="状态" prop="status">
              <el-radio-group v-model="form.status">
                <el-radio v-for="item in options.sys_normal_disable" :key="item.dictValue" :value="item.dictValue">
                  {{ item.dictLabel }}
                </el-radio>
              </el-radio-group>
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

<script setup name="processdefine">
import { listProcessDefine, addProcessDefine, delProcessDefine, updateProcessDefine, getProcessDefine } from '@/api/workflow/processDefine.js'
import { dictFormTemplate } from '@/api/workflow/formTemplate.js'
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
  processName: undefined
})
const columns = ref([
  { visible: true, prop: 'processId', label: '流程ID' },
  { visible: true, prop: 'processName', label: '流程名称' },
  { visible: true, prop: 'description', label: '流程描述' },
  { visible: true, prop: 'status', label: '状态' },
  { visible: true, prop: 'createBy', label: '创建人' },
  { visible: false, prop: 'createTime', label: '创建时间' },
  { visible: false, prop: 'updateBy', label: '更新人' },
  { visible: false, prop: 'updateTime', label: '更新时间' }
])
const total = ref(0)
const dataList = ref([])
const queryRef = ref()
const defaultTime = ref([new Date(2000, 1, 1, 0, 0, 0), new Date(2000, 2, 1, 23, 59, 59)])

var dictParams = [{ dictType: 'sys_normal_disable' }, { dictType: 'process_template' }]

proxy.getDicts(dictParams).then((response) => {
  response.data.forEach((element) => {
    state.options[element.dictType] = element.list
  })
})

function getList() {
  loading.value = true
  listProcessDefine(queryParams).then((res) => {
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

/**
 * 查看节点
 * @param {*} row
 */
function handlePreviewNode(row) {
  const id = row.processId
  var hasPermi = proxy.$auth.hasPermi('process:list')
  if (hasPermi) {
    router.push({ path: '/workflow/NodeDefine', query: { processId: id } })
  } else {
    proxy.$modal.msgError('你没有权限访问')
  }
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
    processId: [
      { required: true, message: '流程编号不能为空', trigger: 'blur' },
      { min: 6, max: 6, message: '字符长度固定6位,如:WF0001', trigger: 'blur' }
    ],
    processName: [{ required: true, message: '流程名称不能为空', trigger: 'blur' }],
    processTemplate: [{ required: true, message: '流程模板不能为空', trigger: 'blur' }],
    status: [{ required: true, message: '状态不能为空', trigger: 'blur' }]
  },
  options: {
    // 状态 选项列表 格式 eg:{ dictLabel: '标签', dictValue: '0'}
    sys_normal_disable: [],
    // 流程模板
    process_template: [],
    // 表单选项
    form_options: []
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
    processId: null,
    processName: null,
    description: null,
    formId: null,
    processTemplate: null,
    status: null,
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
  title.value = '添加流程定义'
  opertype.value = 1
}
// 修改按钮操作
function handleUpdate(row) {
  reset()
  const id = row.processId || ids.value
  getProcessDefine(id).then((res) => {
    const { code, data } = res
    if (code == 200) {
      open.value = true
      title.value = '修改流程定义'
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
      if (form.value.processId != undefined && opertype.value === 2) {
        updateProcessDefine(form.value).then((res) => {
          proxy.$modal.msgSuccess('修改成功')
          open.value = false
          getList()
        })
      } else {
        addProcessDefine(form.value).then((res) => {
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
  const Ids = row.processId || ids.value
  proxy
    .$confirm('是否确认删除参数编号为"' + Ids + '"的数据项？', '警告', {
      confirmButtonText: proxy.$t('common.ok'),
      cancelButtonText: proxy.$t('common.cancel'),
      type: 'warning'
    })
    .then(function () {
      return delProcessDefine(Ids)
    })
    .then(() => {
      getList()
      proxy.$modal.msgSuccess('删除成功')
    })
}

function getFormOptions() {
  dictFormTemplate().then((res) => {
    const { code, data } = res
    if (code == 200) {
      state.options.form_options = data
    }
  })
}
getFormOptions()

handleQuery()
</script>
