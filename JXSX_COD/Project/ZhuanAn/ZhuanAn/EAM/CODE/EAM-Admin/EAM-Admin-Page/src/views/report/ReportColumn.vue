<!--
 * @Descripttion: (报表数据列/rep_report_column)
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
      <el-table-column prop="columnKey" label="列字段名" align="center" min-width="150" />
      <el-table-column prop="columnLabel" label="显示名称" align="center" :show-overflow-tooltip="true" />
      <el-table-column prop="width" label="列宽" align="center" width="90" />
      <el-table-column prop="isVisible" label="默认显示" align="center" width="90">
        <template #default="scope">
          <el-switch v-model="scope.row.isVisible" active-text="是" inactive-text="否" inline-prompt disabled />
        </template>
      </el-table-column>
      <el-table-column prop="isSort" label="排序开关" align="center" width="90">
        <template #default="scope">
          <el-switch v-model="scope.row.isSort" active-text="是" inactive-text="否" inline-prompt disabled />
        </template>
      </el-table-column>
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

    <!-- 添加或修改报表数据列对话框 -->
    <el-dialog :title="title" :lock-scroll="false" v-model="open">
      <el-form ref="formRef" :model="form" :rules="rules" label-width="100px">
        <el-row :gutter="20">
          <el-col :lg="12">
            <el-form-item label="列字段名" prop="columnKey">
              <el-input v-model="form.columnKey" placeholder="请输入列字段名" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="显示名称" prop="columnLabel">
              <el-input v-model="form.columnLabel" placeholder="请输入显示名称" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="列宽" prop="width">
              <el-input-number v-model.number="form.width" :controls="true" controls-position="right" placeholder="请输入列宽" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="默认显示" prop="isVisible">
              <el-switch v-model="form.isVisible" active-text="是" inactive-text="否" inline-prompt />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="排序开关" prop="isSort">
              <el-switch v-model="form.isSort" active-text="是" inactive-text="否" inline-prompt />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="排序" prop="sortOrder">
              <el-input-number v-model.number="form.sortOrder" :controls="true" controls-position="right" placeholder="请输入排序" />
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

<script setup name="reportcolumn">
import { listReportColumn, addReportColumn, delReportColumn, updateReportColumn, getReportColumn } from '@/api/report/reportColumn.js'
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
  { visible: true, prop: 'columnId', label: '主键ID' },
  { visible: true, prop: 'reportId', label: '报表ID' },
  { visible: true, prop: 'columnKey', label: '列字段名' },
  { visible: true, prop: 'columnLabel', label: '显示名称' },
  { visible: true, prop: 'width', label: '列宽' },
  { visible: true, prop: 'isVisible', label: '默认是否显示' },
  { visible: true, prop: 'isSort', label: '是否可以排序' },
  { visible: true, prop: 'sortOrder', label: '排序' }
])
const total = ref(0)
const dataList = ref([])
const queryRef = ref()
const defaultTime = ref([new Date(2000, 1, 1, 0, 0, 0), new Date(2000, 2, 1, 23, 59, 59)])

var dictParams = []

function getList() {
  if (!queryParams.reportId) return
  loading.value = true
  listReportColumn(queryParams).then((res) => {
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

    if (column.prop == 'sortOrder') {
      sort = 'sort_Order'
    }
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
    columnId: [{ required: true, message: '主键ID不能为空', trigger: 'blur', type: 'number' }],
    reportId: [{ required: true, message: '报表ID不能为空', trigger: 'blur', type: 'number' }],
    columnKey: [{ required: true, message: '列字段名不能为空', trigger: 'blur' }],
    columnLabel: [{ required: true, message: '显示名称不能为空', trigger: 'blur' }],
    sortOrder: [{ required: true, message: '排序不能为空', trigger: 'blur', type: 'number' }]
  },
  options: {}
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
    columnId: null,
    reportId: null,
    columnKey: null,
    columnLabel: null,
    width: null,
    isVisible: null,
    isSort: null,
    sortOrder: null
  }
  proxy.resetForm('formRef')
}

// 添加按钮操作
function handleAdd() {
  reset()
  open.value = true
  title.value = '添加报表数据列'
  opertype.value = 1
}
// 修改按钮操作
function handleUpdate(row) {
  reset()
  const id = row.columnId || ids.value
  getReportColumn(id).then((res) => {
    const { code, data } = res
    if (code == 200) {
      open.value = true
      title.value = '修改报表数据列'
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
      if (form.value.columnId != undefined && opertype.value === 2) {
        updateReportColumn(form.value).then((res) => {
          proxy.$modal.msgSuccess('修改成功')
          open.value = false
          getList()
        })
      } else {
        addReportColumn(form.value).then((res) => {
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
  const Ids = row.columnId || ids.value

  proxy
    .$confirm('是否确认删除参数编号为"' + Ids + '"的数据项？', '警告', {
      confirmButtonText: proxy.$t('common.ok'),
      cancelButtonText: proxy.$t('common.cancel'),
      type: 'warning'
    })
    .then(function () {
      return delReportColumn(Ids)
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
