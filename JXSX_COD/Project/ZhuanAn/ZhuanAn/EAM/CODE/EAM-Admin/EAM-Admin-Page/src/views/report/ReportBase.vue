<!--
 * @Descripttion: (报表基本信息/rep_report_base)
 * @Author: (admin)
 * @Date: (2026-03-05)
-->
<template>
  <div>
    <el-row :gutter="15">
      <el-col :sm="6">
        <el-form :model="queryParams" label-position="right" inline ref="queryRef" v-show="showSearch" @submit.prevent>
          <el-form-item label="分组" prop="groupId">
            <el-select v-model="queryParams.groupId" @change="handleGroupChange" placeholder="请选择分组">
              <el-option
                v-for="item in options.group_options"
                :key="item.dictValue"
                :label="item.dictLabel"
                :value="parseInt(item.dictValue)"></el-option>
            </el-select>
          </el-form-item>
        </el-form>
        <!-- 工具区域 -->
        <el-row :gutter="15" class="mb10">
          <el-col :span="1.5">
            <el-button type="primary" size="small" v-hasPermi="['rep:report:base:add']" plain icon="plus" @click="handleAdd"> </el-button>
          </el-col>
          <el-col :span="1.5">
            <el-button
              type="success"
              size="small"
              icon="edit"
              title="编辑"
              v-hasPermi="['rep:report:base:edit']"
              :disabled="!(form.reportId > 0)"
              @click="handleUpdate"></el-button>
          </el-col>
          <el-col :span="1.5">
            <el-button
              type="danger"
              size="small"
              icon="delete"
              title="删除"
              v-hasPermi="['rep:report:base:delete']"
              :disabled="!(form.reportId > 0)"
              @click="handleDelete">
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
          @row-click="handleRowClick">
          <el-table-column prop="reportName" label="报表名称" :show-overflow-tooltip="true" />
          <el-table-column prop="enabled" label="是否可用" align="center" width="90">
            <template #default="scope">
              <el-switch v-model="scope.row.enabled" active-text="是" inactive-text="否" inline-prompt disabled />
            </template>
          </el-table-column>
        </el-table>
      </el-col>

      <el-col :sm="18">
        <el-form-item label-position="top" label="SQL模板" prop="sqlTemplate">
          <template #label>
            <el-button
              type="primary"
              size="small"
              v-hasPermi="['rep:report:base:add']"
              :disabled="!(form.reportId > 0)"
              plain
              icon="check"
              @click="submitSql"
              >保存
            </el-button>
            SQL模板
          </template>
          <el-input type="textarea" :rows="10" v-model="form.sqlTemplate" />
        </el-form-item>

        <el-tabs type="card" v-model="activeTab">
          <el-tab-pane label="参数" name="reportParamTag"><ReportParam :reportId="form.reportId" /></el-tab-pane>
          <el-tab-pane label="显示列名" name="reportColumnTag"><ReportColumn :reportId="form.reportId" /></el-tab-pane>
        </el-tabs>
      </el-col>
    </el-row>

    <!-- 添加或修改报表基本信息对话框 -->
    <el-dialog :title="title" :lock-scroll="false" v-model="open">
      <el-form ref="formRef" :model="form" :rules="rules" label-width="100px">
        <el-row :gutter="20">
          <el-col :lg="12">
            <el-form-item label="报表名称" prop="reportName">
              <el-input v-model="form.reportName" placeholder="请输入报表名称" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="分组" prop="groupId">
              <el-select v-model="form.groupId" placeholder="请选择分组ID">
                <el-option
                  v-for="item in options.group_options"
                  :key="item.dictValue"
                  :label="item.dictLabel"
                  :value="parseInt(item.dictValue)"></el-option>
              </el-select>
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="是否可用" prop="enabled">
              <el-switch v-model="form.enabled" active-text="是" inactive-text="否" inline-prompt />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="排序" prop="sortOrder">
              <el-input-number v-model.number="form.sortOrder" :controls="true" controls-position="right" placeholder="请输入排序" />
            </el-form-item>
          </el-col>

          <el-col :lg="24">
            <el-form-item label="备注" prop="remark">
              <el-input type="textarea" v-model="form.remark" placeholder="请输入备注" />
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

<script setup name="reportbase">
import { listReportBase, addReportBase, delReportBase, updateReportBase, getReportBase } from '@/api/report/reportBase.js'
import { dictReportGroup } from '@/api/report/reportGroup.js'
import ReportColumn from './ReportColumn.vue'
import ReportParam from './ReportParam.vue'

const { proxy } = getCurrentInstance()
const loading = ref(false)
const showSearch = ref(true)
const activeTab = ref('reportParamTag')
const queryParams = reactive({
  pageNum: 1,
  pageSize: 10,
  sort: 'SortOrder',
  sortType: 'asc',
  reportName: undefined
})
const columns = ref([
  { visible: false, prop: 'reportId', label: '报表ID' },
  { visible: true, prop: 'reportName', label: '报表名称' },
  { visible: true, prop: 'groupId', label: '分组ID' },
  { visible: true, prop: 'datasourceId', label: '数据源ID' },
  { visible: true, prop: 'enabled', label: '是否可用' },
  { visible: true, prop: 'remark', label: '备注' },
  { visible: true, prop: 'sortOrder', label: '排序' },
  { visible: false, prop: 'createBy', label: '创建人' },
  { visible: false, prop: 'createTime', label: '创建时间' },
  { visible: false, prop: 'updateBy', label: '更新人' },
  { visible: false, prop: 'updateTime', label: '最后更新时间' }
])
const total = ref(0)
const dataList = ref([])
const queryRef = ref()
const defaultTime = ref([new Date(2000, 1, 1, 0, 0, 0), new Date(2000, 2, 1, 23, 59, 59)])

var dictParams = []

function getList() {
  loading.value = true
  listReportBase(queryParams).then((res) => {
    const { code, data } = res
    if (code == 200) {
      dataList.value = data.result
      total.value = data.totalNum
      loading.value = false
    }
  })
}
//分组下拉变化事件
function handleGroupChange() {
  reset()
  handleQuery()
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
    reportId: [{ required: true, message: '报表ID不能为空', trigger: 'blur', type: 'number' }],
    reportName: [{ required: true, message: '报表名称不能为空', trigger: 'blur' }],
    groupId: [{ required: true, message: '分组ID不能为空', trigger: 'change', type: 'number' }],
    sqlTemplate: [{ required: true, message: 'SQL模板不能为空', trigger: 'blur' }],
    enabled: [{ required: true, message: '是否可用不能为空', trigger: 'blur' }],
    sortOrder: [{ required: true, message: '排序不能为空', trigger: 'blur', type: 'number' }],
    createBy: [{ required: true, message: '创建人不能为空', trigger: 'blur' }],
    createTime: [{ required: true, message: '创建时间不能为空', trigger: 'blur' }]
  },
  options: {
    // 分组ID 选项列表 格式 eg:{ dictLabel: '标签', dictValue: '0'}
    group_options: []
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
    reportId: null,
    reportName: null,
    groupId: null,
    datasourceId: null,
    sqlTemplate: null,
    enabled: null,
    remark: null,
    sortOrder: null,
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
  title.value = '添加报表基本信息'
  opertype.value = 1
}
// 修改按钮操作
function handleUpdate() {
  const id = form.value.reportId
  reset()
  getReportBase(id).then((res) => {
    const { code, data } = res
    if (code == 200) {
      open.value = true
      title.value = '修改报表基本信息'
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
      if (form.value.reportId != undefined && opertype.value === 2) {
        updateReportBase(form.value).then((res) => {
          proxy.$modal.msgSuccess('修改成功')
          open.value = false
          getList()
        })
      } else {
        addReportBase(form.value).then((res) => {
          proxy.$modal.msgSuccess('新增成功')
          open.value = false
          getList()
        })
      }
    }
  })
}
//保存sql
function submitSql() {
  updateReportBase(form.value).then((res) => {
    proxy.$modal.msgSuccess('修改成功')
    open.value = false
    getList()
  })
}

// 删除按钮操作
function handleDelete() {
  const Ids = form.value.reportId
  proxy
    .$confirm('是否确认删除参数编号为"' + Ids + '"的数据项？', '警告', {
      confirmButtonText: proxy.$t('common.ok'),
      cancelButtonText: proxy.$t('common.cancel'),
      type: 'warning'
    })
    .then(function () {
      return delReportBase(Ids)
    })
    .then(() => {
      getList()
      reset()
      proxy.$modal.msgSuccess('删除成功')
    })
}

function handleQueryReportGroup() {
  const params = {
    pageNum: 1,
    pageSize: 100,
    sort: '',
    sortType: 'asc'
  }
  setTimeout(() => {
    dictReportGroup(params).then((res) => {
      options.value.group_options = res.data.result
    })
  }, 200)
}

//行单击事件
function handleRowClick(row) {
  getReportBase(row.reportId).then((res) => {
    const { code, data } = res
    if (code == 200) {
      form.value = {
        ...data
      }
    }
  })
}

handleQueryReportGroup()
handleQuery()
</script>
