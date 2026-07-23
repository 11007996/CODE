<!--
 * @Descripttion: (员工信息/Base_Employee)
 * @Author: (admin)
 * @Date: (2024-06-18)
-->
<template>
  <div>
    <el-form :model="queryParams" label-position="right" inline ref="queryRef" v-show="showSearch" @submit.prevent>
      <el-form-item label="员工工号" prop="empCode">
        <el-input v-model="queryParams.empCode" placeholder="请输入员工工号" clearable style="width: 240px" @keyup.enter="handleQuery" />
      </el-form-item>
      <el-form-item label="员工姓名" prop="empName">
        <el-input v-model="queryParams.empName" placeholder="请输入员工姓名" clearable style="width: 240px" @keyup.enter="handleQuery" />
      </el-form-item>
      <el-form-item>
        <el-button icon="search" type="primary" @click="handleQuery">{{ $t('btn.search') }}</el-button>
        <el-button icon="refresh" @click="resetQuery">{{ $t('btn.reset') }}</el-button>
      </el-form-item>
    </el-form>
    <!-- 工具区域 -->
    <el-row :gutter="15" class="mb10">
      <el-col :span="1.5">
        <el-button type="primary" v-hasPermi="['employee:add']" plain icon="plus" @click="handleAdd">
          {{ $t('btn.add') }}
        </el-button>
      </el-col>
      <el-col :span="1.5">
        <el-dropdown trigger="click" v-hasPermi="['employee:import']">
          <el-button type="primary" plain icon="Upload">
            {{ $t('btn.import') }}<el-icon class="el-icon--right"><arrow-down /></el-icon>
          </el-button>
          <template #dropdown>
            <el-dropdown-menu>
              <el-dropdown-item command="upload">
                <importData
                  templateUrl="basic/employee/importTemplate"
                  importUrl="/basic/employee/importData"
                  @success="handleFileSuccess"></importData>
              </el-dropdown-item>
            </el-dropdown-menu>
          </template>
        </el-dropdown>
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
      <el-table-column
        prop="empCode"
        label="员工工号"
        width="120"
        align="center"
        :show-overflow-tooltip="true"
        v-if="columns.showColumn('empCode')" />
      <el-table-column
        prop="empName"
        label="员工姓名"
        width="90"
        align="center"
        :show-overflow-tooltip="true"
        v-if="columns.showColumn('empName')" />
      <el-table-column prop="deptId" label="部门ID" width="90" align="center" v-if="columns.showColumn('deptId')" />
      <el-table-column prop="email" label="邮箱" min-width="160" align="center" :show-overflow-tooltip="true" v-if="columns.showColumn('email')" />
      <el-table-column
        prop="phonenumber"
        label="手机号码"
        min-width="160"
        align="center"
        :show-overflow-tooltip="true"
        v-if="columns.showColumn('phonenumber')" />
      <el-table-column prop="sex" label="性别" width="90" align="center" v-if="columns.showColumn('sex')">
        <template #default="scope">
          <dict-tag :options="options.sys_user_sex" :value="scope.row.sex" />
        </template>
      </el-table-column>
      <el-table-column prop="status" label="帐号状态" width="90" align="center" v-if="columns.showColumn('status')">
        <template #default="scope">
          <dict-tag :options="options.sys_normal_disable" :value="scope.row.status" />
        </template>
      </el-table-column>
      <el-table-column label="操作" width="110">
        <template #default="scope">
          <el-button
            type="success"
            size="small"
            icon="edit"
            title="编辑"
            v-hasPermi="['employee:edit']"
            @click="handleUpdate(scope.row)"></el-button>
          <el-button
            type="danger"
            size="small"
            icon="delete"
            title="删除"
            v-hasPermi="['employee:delete']"
            @click="handleDelete(scope.row)"></el-button>
        </template>
      </el-table-column>
    </el-table>
    <pagination :total="total" v-model:page="queryParams.pageNum" v-model:limit="queryParams.pageSize" @pagination="getList" />

    <!-- 添加或修改员工信息对话框 -->
    <el-dialog :title="title" :lock-scroll="false" width="600px" v-model="open">
      <el-form ref="formRef" :model="form" :rules="rules" label-width="100px">
        <el-row :gutter="20">
          <el-col :lg="12">
            <el-form-item label="员工工号" prop="empCode">
              <el-input v-model="form.empCode" placeholder="请输入员工工号" :disabled="opertype != 1" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="员工姓名" prop="empName">
              <el-input v-model="form.empName" placeholder="请输入员工姓名" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="归属部门" prop="deptId">
              <el-tree-select
                v-model="form.deptId"
                :data="options.dept_tree_options"
                :props="{ value: 'id', label: 'label', children: 'children' }"
                value-key="id"
                placeholder="请选择归属部门"
                check-strictly
                :render-after-expand="false" />
            </el-form-item>
          </el-col>

          <el-col :lg="24">
            <el-form-item label="岗位">
              <el-select v-model="form.postIds" multiple clearable filterable placeholder="请选择岗位" style="width: 100%">
                <el-option
                  v-for="item in options.post_options"
                  :key="item.postId"
                  :label="item.postName + '(' + item.postCode + ')'"
                  :value="item.postId"
                  :disabled="item.status == 1">
                </el-option>
              </el-select>
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="帐号状态" prop="status">
              <el-radio-group v-model="form.status">
                <el-radio v-for="item in options.sys_normal_disable" :key="item.dictValue" :value="parseInt(item.dictValue)">
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

<script setup name="employee">
import importData from '@/components/ImportData'
import { listEmployee, addEmployee, delEmployee, updateEmployee, getEmployee } from '@/api/basic/employee.js'
import { factoryDeptTreeselect } from '@/api/system/dept.js'
const { proxy } = getCurrentInstance()
const ids = ref([])
const loading = ref(false)
const showSearch = ref(true)
const queryParams = reactive({
  pageNum: 1,
  pageSize: 10,
  sort: '',
  sortType: 'asc',
  empCode: null,
  empName: null
})
const columns = ref([
  { visible: true, prop: 'empCode', label: '员工工号' },
  { visible: true, prop: 'empName', label: '员工姓名' },
  { visible: false, prop: 'deptId', label: '部门ID' },
  { visible: true, prop: 'email', label: '邮箱' },
  { visible: true, prop: 'phonenumber', label: '手机号码' },
  { visible: true, prop: 'sex', label: '用户性别' },
  { visible: false, prop: 'status', label: '帐号状态' }
])
const total = ref(0)
const dataList = ref([])
const queryRef = ref()
const defaultTime = ref([new Date(2000, 1, 1, 0, 0, 0), new Date(2000, 2, 1, 23, 59, 59)])

var dictParams = [{ dictType: 'sys_user_sex' }, { dictType: 'sys_normal_disable' }]

proxy.getDicts(dictParams).then((response) => {
  response.data.forEach((element) => {
    state.options[element.dictType] = element.list
  })
})

function getList() {
  loading.value = true
  listEmployee(queryParams).then((res) => {
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
    empCode: [{ required: true, message: '员工工号不能为空', trigger: 'blur' }],
    empName: [{ required: true, message: '员工姓名不能为空', trigger: 'blur' }]
  },
  options: {
    // 用户性别（0男 1女 2未知） 选项列表 格式 eg:{ dictLabel: '标签', dictValue: '0'}
    sys_user_sex: [],
    // 帐号状态（0正常 1停用） 选项列表 格式 eg:{ dictLabel: '标签', dictValue: '0'}
    sys_normal_disable: [],
    // 部门树形选项数据  格式：
    dept_tree_options: undefined,
    // 岗位选项数据
    post_options: []
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
    empCode: null,
    empName: null,
    deptId: null,
    email: null,
    phonenumber: null,
    sex: null,
    avatar: null,
    status: null,
    delFlag: null,
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
  initDeptTreeData()
  getEmployee('0').then((res) => {
    const { code, data } = res
    if (code == 200) {
      open.value = true
      title.value = '添加员工信息'
      opertype.value = 1
      options.value.post_options = data.posts
    }
  })
}
// 修改按钮操作
function handleUpdate(row) {
  reset()
  initDeptTreeData()
  const id = row.empCode || ids.value
  getEmployee(id).then((res) => {
    const { code, data } = res
    if (code == 200) {
      open.value = true
      title.value = '修改员工信息'
      opertype.value = 2
      options.value.post_options = data.posts
      form.value = {
        ...data.employee
      }
    }
  })
}

// 添加&修改 表单提交
function submitForm() {
  proxy.$refs['formRef'].validate((valid) => {
    if (valid) {
      if (form.value.empCode != undefined && opertype.value === 2) {
        updateEmployee(form.value).then((res) => {
          proxy.$modal.msgSuccess('修改成功')
          open.value = false
          getList()
        })
      } else {
        addEmployee(form.value).then((res) => {
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
  const empCode = row.empCode

  proxy
    .$confirm('是否确认删除工号为"' + empCode + '"的数据项？', '警告', {
      confirmButtonText: proxy.$t('common.ok'),
      cancelButtonText: proxy.$t('common.cancel'),
      type: 'warning'
    })
    .then(function () {
      return delEmployee(empCode)
    })
    .then(() => {
      getList()
      proxy.$modal.msgSuccess('删除成功')
    })
}

/** 查询部门下拉树结构 */
function getTreeselect() {
  factoryDeptTreeselect().then((response) => {
    options.value.dept_tree_options = response.data
  })
}

/** 初始化部门数据 */
function initDeptTreeData() {
  // 判断部门的数据是否存在，存在不获取，不存在则获取
  if (options.value.dept_tree_options === undefined) {
    factoryDeptTreeselect().then((response) => {
      options.value.dept_tree_options = response.data
    })
  }
}

// 导入数据成功处理
const handleFileSuccess = (response) => {
  const { item1, item2 } = response.data
  var error = ''
  item2.forEach((item) => {
    error += item.storageMessage + ','
  })
  proxy.$alert(item1 + '<p>' + error + '</p>', '导入结果', {
    dangerouslyUseHTMLString: true
  })
  getList()
}

getTreeselect()
handleQuery()
</script>
