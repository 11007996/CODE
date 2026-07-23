<!--
 * @Descripttion: (产线员工关联/BASE_Line_Emp)
 * @Author: (admin)
 * @Date: (2025-09-15)
-->
<template>
  <div>
    <el-form :model="queryParams" label-position="right" inline ref="queryRef" v-show="showSearch" @submit.prevent>
      <el-form-item label="线别ID" prop="lineId">
        <el-select clearable v-model="queryParams.lineId" placeholder="请选择线别ID">
          <el-option v-for="item in options.lineIdOptions" :key="item.dictValue" :label="item.dictLabel" :value="item.dictValue">
            <span class="fl">{{ item.dictLabel }}</span>
            <span class="fr" style="color: var(--el-text-color-secondary)">{{ item.dictValue }}</span>
          </el-option>
        </el-select>
      </el-form-item>
      <el-form-item label="员工工号" prop="empCode">
        <el-select clearable v-model="queryParams.empCode" placeholder="请选择员工工号">
          <el-option v-for="item in options.emp_options" :key="item.dictValue" :label="item.dictLabel" :value="item.dictValue">
            <span class="fl">{{ item.dictLabel }}</span>
            <span class="fr" style="color: var(--el-text-color-secondary)">{{ item.dictValue }}</span>
          </el-option>
        </el-select>
      </el-form-item>
      <el-form-item>
        <el-button icon="search" type="primary" @click="handleQuery">{{ $t('btn.search') }}</el-button>
        <el-button icon="refresh" @click="resetQuery">{{ $t('btn.reset') }}</el-button>
      </el-form-item>
    </el-form>
    <!-- 工具区域 -->
    <el-row :gutter="15" class="mb10">
      <el-col :span="1.5">
        <el-button type="primary" v-hasPermi="['line:emp:add']" plain icon="plus" @click="handleAdd">
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
      <el-table-column prop="id" label="主键ID" align="center" v-if="columns.showColumn('id')" />
      <el-table-column prop="lineId" label="线别ID" align="center" v-if="columns.showColumn('lineId')" />
      <el-table-column prop="lineName" label="线别" align="center" v-if="columns.showColumn('lineName')" />
      <el-table-column prop="empCode" label="员工工号" align="center" v-if="columns.showColumn('empCode')" />
      <el-table-column prop="empName" label="员工" align="center" v-if="columns.showColumn('empName')" />
      <el-table-column prop="position" label="员工职位" align="center" v-if="columns.showColumn('position')">
        <template #default="scope">
          <dict-tag :options="options.line_emp_position" :value="scope.row.position" />
        </template>
      </el-table-column>
      <el-table-column label="操作" width="160">
        <template #default="scope">
          <el-button
            type="success"
            size="small"
            icon="edit"
            title="编辑"
            v-hasPermi="['line:emp:edit']"
            @click="handleUpdate(scope.row)"></el-button>
          <el-button
            type="danger"
            size="small"
            icon="delete"
            title="删除"
            v-hasPermi="['line:emp:delete']"
            @click="handleDelete(scope.row)"></el-button>
        </template>
      </el-table-column>
    </el-table>
    <pagination :total="total" v-model:page="queryParams.pageNum" v-model:limit="queryParams.pageSize" @pagination="getList" />

    <!-- 添加或修改产线员工关联对话框 -->
    <el-dialog :title="title" :lock-scroll="false" v-model="open">
      <el-form ref="formRef" :model="form" :rules="rules" label-width="100px">
        <el-row :gutter="20">
          <el-col :lg="12" v-if="opertype != 1">
            <el-form-item label="主键ID" prop="id">
              <el-input-number v-model.number="form.id" controls-position="right" placeholder="请输入主键ID" :disabled="true" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="线别" prop="lineId">
              <el-select v-model="form.lineId" placeholder="请选择线别ID">
                <el-option
                  v-for="item in useBasicStore().getLineDict"
                  :key="item.dictValue"
                  :label="item.dictLabel"
                  :value="parseInt(item.dictValue)"></el-option>
              </el-select>
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="员工" prop="empCode">
              <el-select
                v-model="form.empCode"
                placeholder="请输入姓名或工号"
                clearable
                filterable
                remote
                :remote-method="handleQueryEmployee"
                class="fullWidth">
                <el-option
                  v-for="item in options.emp_options"
                  :key="item.dictValue"
                  :label="item.dictValue + ' - ' + item.dictLabel"
                  :value="item.dictValue"></el-option>
              </el-select>
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="员工职位" prop="position">
              <el-select v-model="form.position" placeholder="请选择员工职位">
                <el-option
                  v-for="item in options.line_emp_position"
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

<script setup name="lineemp">
import { listLineEmp, addLineEmp, delLineEmp, updateLineEmp, getLineEmp } from '@/api/basic/lineEmp.js'
import { dictEmployee } from '@/api/basic/employee.js'
import useBasicStore from '@/store/modules/basic.js'
const { proxy } = getCurrentInstance()
const ids = ref([])
const loading = ref(false)
const showSearch = ref(true)
const queryParams = reactive({
  pageNum: 1,
  pageSize: 10,
  sort: '',
  sortType: 'asc',
  lineId: undefined,
  empCode: undefined
})
const columns = ref([
  { visible: true, prop: 'id', label: '主键ID' },
  { visible: true, prop: 'lineId', label: '线别ID' },
  { visible: true, prop: 'lineName', label: '线别' },
  { visible: true, prop: 'empCode', label: '员工工号' },
  { visible: true, prop: 'empName', label: '员工' },
  { visible: true, prop: 'position', label: '员工职位' }
])
const total = ref(0)
const dataList = ref([])
const queryRef = ref()
const defaultTime = ref([new Date(2000, 1, 1, 0, 0, 0), new Date(2000, 2, 1, 23, 59, 59)])

var dictParams = [{ dictType: 'line_emp_position' }]

proxy.getDicts(dictParams).then((response) => {
  response.data.forEach((element) => {
    state.options[element.dictType] = element.list
  })
})

function getList() {
  loading.value = true
  listLineEmp(queryParams).then((res) => {
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
    lineId: [{ required: true, message: '线别ID不能为空', trigger: 'change', type: 'number' }],
    empCode: [{ required: true, message: '员工工号不能为空', trigger: 'change' }],
    position: [{ required: true, message: '员工职位不能为空', trigger: 'change' }]
  },
  options: {
    // 员工 选项列表 格式 eg:{ dictLabel: '标签', dictValue: '0'}
    emp_options: [],
    // 员工职位 选项列表 格式 eg:{ dictLabel: '标签', dictValue: '0'}
    line_emp_position: []
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
    id: null,
    lineId: null,
    empCode: null,
    position: null
  }
  proxy.resetForm('formRef')
}

// 添加按钮操作
function handleAdd() {
  reset()
  open.value = true
  title.value = '添加产线员工关联'
  opertype.value = 1
}
// 修改按钮操作
function handleUpdate(row) {
  reset()
  const id = row.id || ids.value
  getLineEmp(id).then((res) => {
    const { code, data } = res
    if (code == 200) {
      open.value = true
      title.value = '修改产线员工关联'
      opertype.value = 2

      form.value = {
        ...data
      }

      options.value.emp_options = [{ dictValue: res.data.empCode, dictLabel: res.data.empName }]
    }
  })
}

// 添加&修改 表单提交
function submitForm() {
  proxy.$refs['formRef'].validate((valid) => {
    if (valid) {
      if (form.value.id != undefined && opertype.value === 2) {
        updateLineEmp(form.value).then((res) => {
          proxy.$modal.msgSuccess('修改成功')
          open.value = false
          getList()
        })
      } else {
        addLineEmp(form.value).then((res) => {
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
  const Ids = row.id || ids.value

  proxy
    .$confirm('是否确认删除参数编号为"' + Ids + '"的数据项？', '警告', {
      confirmButtonText: proxy.$t('common.ok'),
      cancelButtonText: proxy.$t('common.cancel'),
      type: 'warning'
    })
    .then(function () {
      return delLineEmp(Ids)
    })
    .then(() => {
      getList()
      proxy.$modal.msgSuccess('删除成功')
    })
}

//员工查询
function handleQueryEmployee(keyword) {
  if (keyword) {
    const query = {
      pageNum: 1,
      pageSize: 10,
      keyword: keyword
    }
    setTimeout(() => {
      dictEmployee(query).then((res) => {
        options.value.emp_options = res.data.result
      })
    }, 200)
  }
}

handleQuery()
</script>
