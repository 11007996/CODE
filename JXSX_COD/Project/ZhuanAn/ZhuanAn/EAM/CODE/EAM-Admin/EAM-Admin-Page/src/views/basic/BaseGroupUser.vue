<!--
 * @Descripttion: (分组用户/BASE_Group_User)
 * @Author: (admin)
 * @Date: (2025-02-08)
-->
<template>
  <div>
    <el-form :model="queryParams" label-position="right" inline ref="queryRef" v-show="showSearch" @submit.prevent>
      <el-form-item label="分组Id">
        <el-input v-model="queryParams.groupId" disabled />
      </el-form-item>
      <el-form-item label="分组名称">
        <el-input v-model="queryParams.groupName" disabled />
      </el-form-item>
      <el-form-item label="用户工号">
        <el-input v-model="queryParams.empCode" placeholder="请输入用户工号" clearable prefix-icon="search" @keyup.enter="handleQuery" />
      </el-form-item>
      <el-form-item>
        <el-button icon="search" type="primary" @click="handleQuery">{{ $t('btn.search') }}</el-button>
      </el-form-item>
    </el-form>
    <!-- 工具区域 -->
    <el-row :gutter="15" class="mb10">
      <el-col :span="1.5">
        <el-button type="primary" v-hasPermi="['group:user:add']" plain icon="plus" @click="handleAdd">
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
      <el-table-column prop="groupId" label="组ID" align="center" v-if="columns.showColumn('groupId')" />
      <el-table-column prop="empCode" label="用户工号" align="center" v-if="columns.showColumn('empCode')" />
      <el-table-column prop="empName" label="用户姓名" align="center" v-if="columns.showColumn('empName')" />

      <el-table-column label="操作" width="160">
        <template #default="scope">
          <el-button
            type="danger"
            size="small"
            icon="delete"
            title="删除"
            v-hasPermi="['group:user:delete']"
            @click="handleDelete(scope.row)"></el-button>
        </template>
      </el-table-column>
    </el-table>
    <pagination :total="total" v-model:page="queryParams.pageNum" v-model:limit="queryParams.pageSize" @pagination="getList" />

    <!-- 添加或修改分组用户对话框 -->
    <!-- 添加或修改菜单对话框 -->
    <el-dialog title="添加用户" v-model="open" append-to-body @close="cancel">
      <el-form :inline="true" @submit.prevent>
        <el-form-item>
          <el-input
            v-model="userQueryParams.empName"
            placeholder="请输入用户姓名"
            clearable
            prefix-icon="search"
            @keyup.enter="handleSearchGroupUser" />
        </el-form-item>
      </el-form>
      <el-row>
        <el-col>
          <el-table
            ref="userTable"
            v-loading="loadingUser"
            :data="dataUserTable"
            @selection-change="handleSelectionChange"
            row-key="userId"
            stripe
            border
            :height="tableHeight * 0.5">
            <el-table-column type="selection" width="55" align="center" />
            <el-table-column prop="empCode" align="center" label="工号" width="150" />
            <el-table-column prop="empName" align="center" label="姓名" width="150" />
            <el-table-column prop="status" label="状态" width="90" align="center" v-if="columns.showColumn('status')">
              <template #default="scope">
                <dict-tag :options="options.sys_normal_disable" :value="scope.row.status" />
              </template>
            </el-table-column>
          </el-table>
          <pagination
            :total="dataUserCount"
            v-model:page="userQueryParams.pageNum"
            v-model:limit="userQueryParams.pageSize"
            @pagination="handleGetUserTable" />
        </el-col>
      </el-row>
      <template #footer>
        <div class="dialog-footer">
          <el-button text @click="open = false">{{ $t('btn.cancel') }}</el-button>
          <el-button type="primary" @click="handleSubmit">{{ $t('btn.submit') }}</el-button>
        </div>
      </template>
    </el-dialog>
  </div>
</template>

<script setup name="basegroupuser">
import { getBaseGroup } from '@/api/basic/baseGroup'
import { listBaseGroupUser, batchAddBaseGroupUser, delBaseGroupUser, getBaseGroupUser, getExcludeUsers } from '@/api/basic/baseGroupUser.js'
const { proxy } = getCurrentInstance()
const route = useRoute()
const loading = ref(false)
const loadingUser = ref(false)
const groupId = route.query.groupId
const showSearch = ref(true)
const queryParams = reactive({
  pageNum: 1,
  pageSize: 10,
  sort: '',
  sortType: 'asc',
  groupId: groupId,
  groupName: null,
  empCode: null
})
const columns = ref([
  { visible: false, prop: 'groupId', label: '组ID' },
  { visible: true, prop: 'empCode', label: '用户工号' },
  { visible: true, prop: 'empName', label: '用户姓名' }
])
const total = ref(0)
const dataList = ref([])
const queryRef = ref()

var dictParams = [{ dictType: 'sys_normal_disable' }]

proxy.getDicts(dictParams).then((response) => {
  response.data.forEach((element) => {
    state.options[element.dictType] = element.list
  })
})

function getList() {
  loading.value = true
  listBaseGroupUser(queryParams).then((res) => {
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
const userQueryParams = reactive({
  pageNum: 1,
  pageSize: 10,
  groupId: groupId,
  empName: undefined
})
// 表格高度
const tableHeight = ref(window.innerHeight)
// 未添加用户列表
const dataUserTable = ref([])
const dataUserCount = ref(0)
// 勾选添加用户列表
const addSelections = ref([])
// 勾选删除用户列表
const delSelections = ref([])
const title = ref('')
// 操作类型 1、add 2、edit 3、view
const opertype = ref(0)
const open = ref(false)
const state = reactive({
  form: {},
  options: {
    // 用户状态
    sys_normal_disable: []
  }
})

const { form, options } = toRefs(state)

// 关闭dialog
function cancel() {
  open.value = false
  reset()
}

// 重置表单
function reset() {
  form.value = {
    groupId: null,
    empCode: null
  }
  proxy.resetForm('formRef')
}

// 添加按钮操作
function handleAdd() {
  open.value = true
  title.value = '添加分组用户'
  opertype.value = 1
}

// 新增用户分组
function handleSubmit() {
  if (addSelections.value.length <= 0) {
    proxy.$modal.msgError('请选择要添加的用户')
    return
  }
  batchAddBaseGroupUser({
    groupId: groupId,
    empCodes: addSelections.value
  }).then((response) => {
    if (response.code === 200) {
      proxy.$message({
        message: '成功添加' + response.data + '条数据',
        type: 'success'
      })
      getList()
      open.value = false
    }
  })
}

// 删除按钮操作
function handleDelete(row) {
  const parm = { groupId: groupId, empCodes: [row.empCode] }
  proxy
    .$confirm('是否确认删除工号为"' + row.empCode + '"的数据项？', '警告', {
      confirmButtonText: proxy.$t('common.ok'),
      cancelButtonText: proxy.$t('common.cancel'),
      type: 'warning'
    })
    .then(function () {
      return delBaseGroupUser(parm)
    })
    .then(() => {
      getList()
      proxy.$modal.msgSuccess('删除成功')
    })
}

// 多选框选中数据
function handleSelectionChange(selection) {
  addSelections.value = selection.map((item) => item.empCode)
}

//用户查询
function handleSearchGroupUser() {
  userQueryParams.pageNum = 1
  handleGetUserTable()
}
// 获取未添加分组用户列表
function handleGetUserTable() {
  open.value = true
  loadingUser.value = true
  getExcludeUsers(userQueryParams).then((response) => {
    dataUserTable.value = response.data.result
    dataUserCount.value = response.data.totalNum
    loadingUser.value = false
  })
}

function init() {
  getBaseGroup(groupId).then((res) => {
    const { code, data } = res
    if (code == 200) {
      queryParams.groupName = data.groupName
    }
  })
  handleQuery()
}

init()
</script>
