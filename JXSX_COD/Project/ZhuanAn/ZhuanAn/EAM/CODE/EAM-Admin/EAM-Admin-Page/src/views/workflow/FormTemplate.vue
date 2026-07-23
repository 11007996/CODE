<!--
 * @Descripttion: (表单模板/WF_Form_Template)
 * @Author: (admin)
 * @Date: (2024-06-08)
-->
<template>
  <div>
    <el-form :model="queryParams" label-position="right" inline ref="queryRef" v-show="showSearch" @submit.prevent>
      <el-form-item label="表单名称" prop="formName">
        <el-input v-model="queryParams.formName" placeholder="请输入表单名称" />
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
      <el-table-column align="center" width="90">
        <template #default="scope">
          <el-button text @click="rowClick(scope.row)">{{ $t('btn.details') }}</el-button>
        </template>
      </el-table-column>
      <el-table-column prop="formId" label="表单ID" width="90" align="center" v-if="columns.showColumn('formId')" />
      <el-table-column
        prop="formName"
        label="表单名称"
        min-width="160"
        align="center"
        :show-overflow-tooltip="true"
        v-if="columns.showColumn('formName')" />
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
      <el-table-column label="操作" width="110">
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

    <el-drawer v-model="drawer" size="40%" direction="rtl">
      <el-table :data="formFieldList" header-row-class-name="text-navy">
        <el-table-column prop="fieldName" label="字段名" min-width="160" />
        <el-table-column prop="fieldDesc" label="字段描述" min-width="160" />
        <el-table-column prop="fieldType" label="字段类型" width="90" align="center">
          <template #default="scope">
            <dict-tag :options="options.form_field_type" :value="scope.row.fieldType" />
          </template>
        </el-table-column>
        <el-table-column prop="defaultValue" label="默认值" width="90" align="center" />
        <el-table-column prop="size" label="字符长度" width="90" align="center" />
      </el-table>
    </el-drawer>

    <!-- 添加或修改表单模板对话框 -->
    <el-dialog :title="title" :lock-scroll="false" v-model="open" :fullscreen="fullScreen">
      <el-form ref="formRef" :model="form" :rules="rules" label-width="100px">
        <el-row :gutter="20">
          <el-col :lg="24">
            <el-form-item label="表单名称" prop="formName">
              <el-input v-model="form.formName" placeholder="请输入表单名称" />
            </el-form-item>
          </el-col>
        </el-row>
        <el-divider content-position="center">表单字段配置信息</el-divider>
        <el-row :gutter="10" class="mb8">
          <el-col :span="1.5">
            <el-button type="primary" icon="Plus" @click="handleAddFormField">添加</el-button>
          </el-col>
          <el-col :span="1.5">
            <el-button type="danger" icon="Delete" @click="handleDeleteFormField">删除</el-button>
          </el-col>
          <el-col :span="1.5">
            <el-button type="info" icon="FullScreen" @click="fullScreen = !fullScreen">{{ fullScreen ? '退出全屏' : '全屏' }}</el-button>
          </el-col>
        </el-row>
        <el-table :data="formFieldList" :row-class-name="rowFormFieldIndex" @selection-change="handleFormFieldSelectionChange" ref="FormFieldRef">
          <el-table-column type="selection" width="40" align="center" />
          <el-table-column label="序号" align="center" prop="index" width="50" />
          <el-table-column label="字段名" align="center" prop="fieldName" min-width="150">
            <template #default="scope">
              <el-input v-model="scope.row.fieldName" placeholder="请输入字段名" />
            </template>
          </el-table-column>
          <el-table-column label="字段描述" align="center" prop="fieldDesc" min-width="150">
            <template #default="scope">
              <el-input v-model="scope.row.fieldDesc" placeholder="请输入字段描述" />
            </template>
          </el-table-column>
          <el-table-column label="字段类型" prop="fieldType" min-width="140">
            <template #default="scope">
              <el-select v-model="scope.row.fieldType" placeholder="请选择字段类型" class="fullWidth">
                <el-option
                  v-for="item in options.form_field_type"
                  :key="item.dictValue"
                  :label="item.dictLabel"
                  :value="item.dictValue"></el-option>
              </el-select>
            </template>
          </el-table-column>
          <el-table-column label="默认值" align="center" prop="defaultValue" min-width="80">
            <template #default="scope">
              <el-input v-model="scope.row.defaultValue" />
            </template>
          </el-table-column>
          <el-table-column label="字符长度" align="center" prop="size" min-width="80">
            <template #default="scope">
              <el-input-number v-model="scope.row.size" :controls="false" class="fullWidth" />
            </template>
          </el-table-column>
        </el-table>
      </el-form>
      <template #footer v-if="opertype != 3">
        <el-button text @click="cancel">{{ $t('btn.cancel') }}</el-button>
        <el-button type="primary" @click="submitForm">{{ $t('btn.submit') }}</el-button>
      </template>
    </el-dialog>
  </div>
</template>

<script setup name="formtemplate">
import { listFormTemplate, addFormTemplate, delFormTemplate, updateFormTemplate, getFormTemplate } from '@/api/workflow/formTemplate.js'
const { proxy } = getCurrentInstance()
const ids = ref([])
const loading = ref(false)
const showSearch = ref(true)
const queryParams = reactive({
  pageNum: 1,
  pageSize: 10,
  sort: '',
  sortType: 'asc',
  formName: undefined
})
const columns = ref([
  { visible: false, prop: 'formId', label: '表单ID' },
  { visible: true, prop: 'formName', label: '表单名称' },
  { visible: true, prop: 'createBy', label: '创建人' },
  { visible: true, prop: 'createTime', label: '创建时间' },
  { visible: true, prop: 'updateBy', label: '更新人' },
  { visible: true, prop: 'updateTime', label: '更新时间' }
])
const total = ref(0)
const dataList = ref([])
const queryRef = ref()
const defaultTime = ref([new Date(2000, 1, 1, 0, 0, 0), new Date(2000, 2, 1, 23, 59, 59)])

var dictParams = [{ dictType: 'form_field_type' }]

proxy.getDicts(dictParams).then((response) => {
  response.data.forEach((element) => {
    state.options[element.dictType] = element.list
  })
})

function getList() {
  loading.value = true
  listFormTemplate(queryParams).then((res) => {
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
    formName: [{ required: true, message: '表单名称不能为空', trigger: 'blur' }]
  },
  options: {
    // 字段类型 选项列表 格式 eg:{ dictLabel: '标签', dictValue: '0'}
    form_field_type: []
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
    formId: null,
    formName: null,
    createBy: null,
    createTime: null,
    updateBy: null,
    updateTime: null
  }
  formFieldList.value = []
  proxy.resetForm('formRef')
}

// 添加按钮操作
function handleAdd() {
  reset()
  open.value = true
  title.value = '添加表单模板'
  opertype.value = 1
}
// 修改按钮操作
function handleUpdate(row) {
  reset()
  const id = row.formId || ids.value
  getFormTemplate(id).then((res) => {
    const { code, data } = res
    if (code == 200) {
      open.value = true
      title.value = '修改表单模板'
      opertype.value = 2

      form.value = {
        ...data
      }
      formFieldList.value = res.data.formFieldNav
    }
  })
}

// 添加&修改 表单提交
function submitForm() {
  proxy.$refs['formRef'].validate((valid) => {
    if (valid) {
      form.value.formFieldNav = formFieldList.value
      if (form.value.formId != undefined && opertype.value === 2) {
        updateFormTemplate(form.value).then((res) => {
          proxy.$modal.msgSuccess('修改成功')
          open.value = false
          getList()
        })
      } else {
        addFormTemplate(form.value).then((res) => {
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
  const Ids = row.formId || ids.value

  proxy
    .$confirm('是否确认删除参数编号为"' + Ids + '"的数据项？', '警告', {
      confirmButtonText: proxy.$t('common.ok'),
      cancelButtonText: proxy.$t('common.cancel'),
      type: 'warning'
    })
    .then(function () {
      return delFormTemplate(Ids)
    })
    .then(() => {
      getList()
      proxy.$modal.msgSuccess('删除成功')
    })
}

/*********************表单字段配置子表信息*************************/
const formFieldList = ref([])
const checkedFormField = ref([])
const fullScreen = ref(false)
const drawer = ref(false)

/** 表单字段配置序号 */
function rowFormFieldIndex({ row, rowIndex }) {
  row.index = rowIndex + 1
}

/** 表单字段配置添加按钮操作 */
function handleAddFormField() {
  let obj = {}
  //下面的代码自己设置默认值
  //obj.fieldName = null;
  //obj.fieldDesc = null;
  //obj.fieldType = null;
  //obj.defaultValue = null;
  //obj.size = null;
  formFieldList.value.push(obj)
}

/** 复选框选中数据 */
function handleFormFieldSelectionChange(selection) {
  checkedFormField.value = selection.map((item) => item.index)
}

/** 表单字段配置删除按钮操作 */
function handleDeleteFormField() {
  if (checkedFormField.value.length == 0) {
    proxy.$modal.msgError('请先选择要删除的表单字段配置数据')
  } else {
    const formFields = formFieldList.value
    const checkedFormFields = checkedFormField.value
    formFieldList.value = formFields.filter(function (item) {
      return checkedFormFields.indexOf(item.index) == -1
    })
  }
}

/** 表单字段配置详情 */
function rowClick(row) {
  const id = row.formId || ids.value
  getFormTemplate(id).then((res) => {
    const { code, data } = res
    if (code == 200) {
      drawer.value = true
      formFieldList.value = data.formFieldNav
    }
  })
}

handleQuery()
</script>
