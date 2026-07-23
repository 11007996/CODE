<!--
 * @Descripttion: (报表参数/rep_report_param)
 * @Author: (admin)
 * @Date: (2026-03-05)
-->
<template>
  <div>
    <!-- 工具区域 -->
    <el-row :gutter="15" class="mb10">
      <el-col :span="1.5">
        <el-button type="primary" v-hasPermi="['rep:report:base:add']" plain icon="plus" :disabled="!(props.reportId > 0)" @click="handleAdd">
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
      <el-table-column prop="paramKey" label="参数键" align="center" min-width="150" :show-overflow-tooltip="true" />
      <el-table-column prop="paramLabel" label="参数标签" align="center" :show-overflow-tooltip="true" />
      <el-table-column prop="elementType" label="标签类型" align="center" width="90">
        <template #default="scope">
          <dict-tag :options="options.report_param_element_type" :value="scope.row.elementType" />
        </template>
      </el-table-column>
      <el-table-column prop="inputType" label="输入类型" align="center" width="90">
        <template #default="scope">
          <dict-tag :options="options.report_param_input_type" :value="scope.row.inputType" />
        </template>
      </el-table-column>
      <el-table-column prop="defaultValue" label="默认值" align="center" :show-overflow-tooltip="true" />
      <el-table-column prop="required" label="是否必填" align="center" width="90">
        <template #default="scope">
          <el-switch v-model="scope.row.required" active-text="是" inactive-text="否" inline-prompt disabled />
        </template>
      </el-table-column>
      <el-table-column prop="optionsSource" label="选项源" align="center" :show-overflow-tooltip="true" />
      <el-table-column prop="sortOrder" label="排序" align="center" width="60" />
      <el-table-column label="操作" fixed="right" width="120">
        <template #default="scope">
          <el-button
            type="success"
            size="small"
            icon="edit"
            title="编辑"
            v-hasPermi="['rep:report:base:edit']"
            @click="handleUpdate(scope.row)"></el-button>
          <el-button
            type="danger"
            size="small"
            icon="delete"
            title="删除"
            v-hasPermi="['rep:report:base:delete']"
            @click="handleDelete(scope.row)"></el-button>
        </template>
      </el-table-column>
    </el-table>

    <!-- 添加或修改报表参数对话框 -->
    <el-dialog :title="title" :lock-scroll="false" v-model="open">
      <el-form ref="formRef" :model="form" :rules="rules" label-width="100px">
        <el-row :gutter="20">
          <el-col :lg="12">
            <el-form-item label="参数键" prop="paramKey">
              <template #label>
                <span>
                  <el-tooltip placement="top" content="参数键,对应数据库的变量命名，如sql server的变量：@paramName">
                    <el-icon> <questionFilled /> </el-icon>
                  </el-tooltip>
                  参数键
                </span>
              </template>
              <el-input v-model="form.paramKey" placeholder="请输入参数键" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="参数标签" prop="paramLabel">
              <template #label>
                <span>
                  <el-tooltip placement="top" content="前端表单名称">
                    <el-icon> <questionFilled /> </el-icon>
                  </el-tooltip>
                  参数标签
                </span>
              </template>
              <el-input v-model="form.paramLabel" placeholder="请输入参数标签" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="标签类型" prop="elementType">
              <template #label>
                <span>
                  <el-tooltip placement="top" content="前端UI的控件类型">
                    <el-icon> <questionFilled /> </el-icon>
                  </el-tooltip>
                  标签类型
                </span>
              </template>
              <el-select v-model="form.elementType" placeholder="请选择标签类型">
                <el-option
                  v-for="item in options.report_param_element_type"
                  :key="item.dictValue"
                  :label="item.dictLabel"
                  :value="item.dictValue"></el-option>
              </el-select>
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="输入类型" prop="inputType">
              <template #label>
                <span>
                  <el-tooltip placement="top" content="后端SQL参数值的类型">
                    <el-icon> <questionFilled /> </el-icon>
                  </el-tooltip>
                  输入类型
                </span>
              </template>
              <el-select v-model="form.inputType" placeholder="请选择输入类型">
                <el-option
                  v-for="item in options.report_param_input_type"
                  :key="item.dictValue"
                  :label="item.dictLabel"
                  :value="item.dictValue"></el-option>
              </el-select>
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="是否必填" prop="required">
              <el-switch v-model="form.required" active-text="是" inactive-text="否" inline-prompt />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="默认值" prop="defaultValue">
              <el-input v-model="form.defaultValue" placeholder="请输入默认值" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="头部字符" prop="headValue">
              <el-input v-model="form.headValue" placeholder="请输入头部字符" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="尾部字符" prop="tailValue">
              <el-input v-model="form.tailValue" placeholder="请输入尾部字符" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="排序" prop="sortOrder">
              <el-input-number v-model.number="form.sortOrder" :controls="true" controls-position="right" placeholder="请输入排序" />
            </el-form-item>
          </el-col>

          <el-col :lg="24">
            <el-form-item label="选项源" prop="optionsSource">
              <el-input type="textarea" :rows="5" v-model="form.optionsSource" placeholder="SQL语句,可以使用@keyword映射用户输入参数" />
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

<script setup name="reportparam">
import { listReportParam, addReportParam, delReportParam, updateReportParam, getReportParam } from '@/api/report/reportParam.js'
const props = defineProps({
  reportId: Number
})
const { proxy } = getCurrentInstance()
const ids = ref([])
const loading = ref(false)
const showSearch = ref(true)
const queryParams = reactive({
  pageNum: 1,
  pageSize: 100,
  sort: 'sortOrder',
  sortType: 'asc',
  reportId: undefined
})
const columns = ref([
  { visible: true, prop: 'paramId', label: '参数ID' },
  { visible: true, prop: 'reportId', label: '报表ID' },
  { visible: true, prop: 'paramKey', label: '参数键' },
  { visible: true, prop: 'paramLabel', label: '参数标签' },
  { visible: true, prop: 'elementType', label: '标签类型' },
  { visible: true, prop: 'inputType', label: '输入类型' },
  { visible: true, prop: 'defaultValue', label: '默认值' },
  { visible: true, prop: 'required', label: '是否必填' },
  { visible: true, prop: 'optionsSource', label: '选项源' },
  { visible: false, prop: 'sortOrder', label: '排序' }
])
const total = ref(0)
const dataList = ref([])
const queryRef = ref()
const defaultTime = ref([new Date(2000, 1, 1, 0, 0, 0), new Date(2000, 2, 1, 23, 59, 59)])

var dictParams = [{ dictType: 'report_param_element_type' }, { dictType: 'report_param_input_type' }]

proxy.getDicts(dictParams).then((response) => {
  response.data.forEach((element) => {
    state.options[element.dictType] = element.list
  })
})

function getList() {
  if (!queryParams.reportId) return
  loading.value = true
  listReportParam(queryParams).then((res) => {
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
    paramId: [{ required: true, message: '参数ID不能为空', trigger: 'blur', type: 'number' }],
    reportId: [{ required: true, message: '报表ID不能为空', trigger: 'blur', type: 'number' }],
    paramKey: [{ required: true, message: '参数键不能为空', trigger: 'blur' }],
    paramLabel: [{ required: true, message: '参数标签不能为空', trigger: 'blur' }],
    elementType: [{ required: true, message: '标签类型不能为空', trigger: 'change' }],
    inputType: [{ required: true, message: '输入类型不能为空', trigger: 'change' }],
    sortOrder: [{ required: true, message: '排序不能为空', trigger: 'blur', type: 'number' }]
  },
  options: {
    // 标签类型
    report_param_element_type: [],
    // 输入类型 选项列表 格式 eg:{ dictLabel: '标签', dictValue: '0'}
    report_param_input_type: []
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
    paramId: null,
    reportId: null,
    paramKey: null,
    paramLabel: null,
    elementType: null,
    inputType: null,
    defaultValue: null,
    required: null,
    headValue: null,
    tailValue: null,
    optionsSource: null,
    sortOrder: null
  }
  proxy.resetForm('formRef')
}

// 添加按钮操作
function handleAdd() {
  reset()
  open.value = true
  title.value = '添加报表参数'
  opertype.value = 1
}
// 修改按钮操作
function handleUpdate(row) {
  reset()
  const id = row.paramId || ids.value
  getReportParam(id).then((res) => {
    const { code, data } = res
    if (code == 200) {
      open.value = true
      title.value = '修改报表参数'
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
      form.value.reportId = props.reportId
      if (form.value.paramId != undefined && opertype.value === 2) {
        updateReportParam(form.value).then((res) => {
          proxy.$modal.msgSuccess('修改成功')
          open.value = false
          getList()
        })
      } else {
        addReportParam(form.value).then((res) => {
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
  const Ids = row.paramId || ids.value

  proxy
    .$confirm('是否确认删除参数编号为"' + Ids + '"的数据项？', '警告', {
      confirmButtonText: proxy.$t('common.ok'),
      cancelButtonText: proxy.$t('common.cancel'),
      type: 'warning'
    })
    .then(function () {
      return delReportParam(Ids)
    })
    .then(() => {
      getList()
      proxy.$modal.msgSuccess('删除成功')
    })
}

watch(
  props,
  (val) => {
    reset()
    dataList.value = []
    if (props.reportId > 0) {
      queryParams.reportId = props.reportId
      handleQuery()
    }
  },
  { immediate: true }
)

handleQuery()
</script>
