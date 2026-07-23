<!--
 * @Descripttion: (厂区管理/sys_factory)
 * @Author: (admin)
 * @Date: (2024-05-21)
-->
<template>
  <div>
    <el-form :model="queryParams" label-position="right" inline ref="queryRef" v-show="showSearch" @submit.prevent>
      <el-form-item label="厂区代码" prop="factoryId">
        <el-input v-model="queryParams.factoryId" placeholder="请输入厂区代码" />
      </el-form-item>
      <el-form-item label="厂区名称" prop="factoryName">
        <el-input v-model="queryParams.factoryName" placeholder="请输入厂区名称" />
      </el-form-item>
      <el-form-item>
        <el-button icon="search" type="primary" @click="handleQuery">{{ $t('btn.search') }}</el-button>
        <el-button icon="refresh" @click="resetQuery">{{ $t('btn.reset') }}</el-button>
      </el-form-item>
    </el-form>
    <!-- 工具区域 -->
    <el-row :gutter="15" class="mb10">
      <el-col :span="1.5">
        <el-button type="primary" v-hasPermi="['factory:add']" plain icon="plus" @click="handleAdd">
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
      <el-table-column prop="factoryId" label="厂区代码" align="center" :show-overflow-tooltip="true" v-if="columns.showColumn('factoryId')" />
      <el-table-column prop="factoryName" label="厂区名称" align="center" :show-overflow-tooltip="true" v-if="columns.showColumn('factoryName')" />
      <el-table-column prop="userCount" label="用户数" align="center" sortable>
        <template #default="scope">
          <el-link type="primary" @click="handleAuthUser(scope.row)">{{ scope.row.userCount }}</el-link>
        </template>
      </el-table-column>
      <el-table-column prop="rootDeptName" label="根部门" align="center" />
      <el-table-column prop="defaultRoleName" label="默认角色" align="center" />
      <el-table-column label="操作" width="110">
        <template #default="scope">
          <el-button
            type="success"
            size="small"
            icon="edit"
            title="编辑"
            v-hasPermi="['factory:edit']"
            @click="handleUpdate(scope.row)"></el-button>
          <el-button
            type="danger"
            size="small"
            icon="delete"
            title="删除"
            v-hasPermi="['factory:delete']"
            @click="handleDelete(scope.row)"></el-button>
        </template>
      </el-table-column>
    </el-table>
    <pagination :total="total" v-model:page="queryParams.pageNum" v-model:limit="queryParams.pageSize" @pagination="getList" />

    <!-- 添加或修改厂区管理对话框 -->
    <el-dialog :title="title" :lock-scroll="false" v-model="open">
      <el-form ref="formRef" :model="form" :rules="rules" label-width="100px">
        <el-row :gutter="20">
          <el-col :lg="12">
            <el-form-item label="厂区代码" prop="factoryId">
              <el-input v-model="form.factoryId" placeholder="请输入厂区代码" :disabled="opertype != 1" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="厂区名称" prop="factoryName">
              <el-input v-model="form.factoryName" placeholder="请输入厂区名称" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="根部门" prop="rootDeptId">
              <el-tree-select
                v-model="form.rootDeptId"
                :data="deptOptions"
                :props="{ value: 'id', label: 'label', children: 'children' }"
                value-key="id"
                placeholder="请选择根部门"
                check-strictly
                :render-after-expand="false" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="默认角色" prop="defaultRoleId">
              <el-select v-model="form.defaultRoleId" placeholder="请选择角色" clearable filterable remote :remote-method="handleQueryRole">
                <el-option
                  v-for="item in options.role_options"
                  :label="item.dictLabel"
                  :value="Number(item.dictValue)"
                  :disabled="item.dictValue == '1' || form.defaultRoleId == 1">
                  <span style="float: left">{{ item.dictLabel }}</span>
                  <span style="float: right">{{ item.dictDesc }}</span>
                </el-option>
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

<script setup name="factory">
import { listFactory, addFactory, delFactory, updateFactory, getFactory, listFactoryDetail } from '@/api/system/factory.js'
import { treeDept } from '@/api/system/dept'
import { dictRole } from '@/api/system/role'
const { proxy } = getCurrentInstance()
const ids = ref([])
const loading = ref(false)
const showSearch = ref(true)
const queryParams = reactive({
  pageNum: 1,
  pageSize: 10,
  sort: 'FactoryId',
  sortType: 'asc',
  factoryId: undefined,
  factoryName: undefined
})
const columns = ref([
  { visible: true, prop: 'factoryId', label: '厂区代码' },
  { visible: true, prop: 'factoryName', label: '厂区名称' }
])
const total = ref(0)
const dataList = ref([])
const queryRef = ref()
const deptOptions = ref(undefined)
const defaultTime = ref([new Date(2000, 1, 1, 0, 0, 0), new Date(2000, 2, 1, 23, 59, 59)])

var dictParams = []

function getList() {
  loading.value = true
  listFactoryDetail(queryParams).then((res) => {
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
    factoryId: [{ required: true, message: '厂区代码不能为空', trigger: 'blur' }],
    factoryName: [{ required: true, message: '厂区名称不能为空', trigger: 'blur' }]
  },
  options: {
    //角色选项
    role_options: []
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
    factoryId: null,
    factoryName: null,
    rootDeptId: null,
    defaultRoleId: null
  }
  proxy.resetForm('formRef')
}

// 添加按钮操作
function handleAdd() {
  reset()
  open.value = true
  title.value = '添加厂区管理'
  opertype.value = 1
}
// 修改按钮操作
function handleUpdate(row) {
  reset()
  const id = row.factoryId || ids.value
  getFactory(id).then((res) => {
    const { code, data } = res
    if (code == 200) {
      open.value = true
      title.value = '修改厂区管理'
      opertype.value = 2

      form.value = {
        ...data
      }
      if (form.value.defaultRoleId) {
        options.value.role_options = [{ dictValue: form.value.defaultRoleId, dictLabel: form.value.defaultRoleName }]
      }
    }
  })
}

// 添加&修改 表单提交
function submitForm() {
  proxy.$refs['formRef'].validate((valid) => {
    if (valid) {
      if (form.value.factoryId != undefined && opertype.value === 2) {
        updateFactory(form.value).then((res) => {
          proxy.$modal.msgSuccess('修改成功')
          open.value = false
          getList()
        })
      } else {
        addFactory(form.value).then((res) => {
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
  const Ids = row.factoryId || ids.value

  proxy
    .$confirm('是否确认删除参数编号为"' + Ids + '"的数据项？', '警告', {
      confirmButtonText: proxy.$t('common.ok'),
      cancelButtonText: proxy.$t('common.cancel'),
      type: 'warning'
    })
    .then(function () {
      return delFactory(Ids)
    })
    .then(() => {
      getList()
      proxy.$modal.msgSuccess('删除成功')
    })
}

/** 初始化部门数据 */
function initTreeData() {
  // 判断部门的数据是否存在，存在不获取，不存在则获取
  if (deptOptions.value === undefined) {
    treeDept().then((response) => {
      deptOptions.value = response.data
    })
  }
}

// 查询角色
function handleQueryRole(keyword) {
  if (keyword) {
    const kvs = keyword.split(',')
    const params = {
      pageNum: 1,
      pageSize: 10,
      sort: '',
      sortType: 'asc',
      roleName: kvs[0],
      roleKey: kvs.length >= 2 ? kvs[1] : null
    }
    setTimeout(() => {
      dictRole(params).then((res) => {
        state.options.role_options = res.data.result
      })
    }, 200)
  }
}

const router = useRouter()
/** 分配用户操作 */
function handleAuthUser(row) {
  const factoryId = row.factoryId
  var hasPermi = proxy.$auth.hasPermi('system:factory:user:list')
  if (hasPermi) {
    router.push({ path: '/system/factoryUser', query: { factoryId } })
  } else {
    proxy.$modal.msgError('你没有权限访问')
  }
}

initTreeData()
handleQuery()
</script>
