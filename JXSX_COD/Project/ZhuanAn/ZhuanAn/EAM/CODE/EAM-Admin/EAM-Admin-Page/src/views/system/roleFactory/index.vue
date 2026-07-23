<!--
 * @Descripttion: (角色厂区绑定/sys_role_factory)
 * @Author: (admin)
 * @Date: (2026-02-02)
-->
<template>
  <div>
    <el-form :model="queryParams" label-position="right" inline ref="queryRef" v-show="showSearch" @submit.prevent>
      <el-form-item label="工厂" prop="factoryId">
        <el-select clearable v-model="queryParams.factoryId" placeholder="请选择工厂">
          <el-option v-for="item in options.factory_options" :key="item.dictValue" :label="item.dictLabel" :value="item.dictValue">
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
      <el-table-column prop="factoryId" label="工厂" align="center" v-if="columns.showColumn('factoryId')">
        <template #default="scope">
          <dict-tag :options="options.factory_options" :value="scope.row.factoryId" />
        </template>
      </el-table-column>
      <el-table-column prop="roleId" label="角色Id" align="center" v-if="columns.showColumn('roleId')" />
      <el-table-column prop="roleName" label="角色" v-if="columns.showColumn('roleName')" />
      <el-table-column label="状态" align="center" width="90">
        <template #default="scope">
          <el-switch v-model="scope.row.status" :disabled="true" :active-value="0" :inactive-value="1"> </el-switch>
        </template>
      </el-table-column>
      <el-table-column label="用户个数" align="center" prop="userNum" width="90">
        <template #default="scope">
          <el-link type="primary" @click="handleAuthUser(scope.row)">{{ scope.row.userNum }}</el-link>
        </template>
      </el-table-column>
      <el-table-column prop="remark" label="备注" align="center" v-if="columns.showColumn('remark')" />
      <el-table-column label="操作" width="90">
        <template #default="scope">
          <el-button type="primary" size="small" @click="handleDataScope(scope.row)">菜单</el-button>
        </template>
      </el-table-column>
    </el-table>
    <pagination :total="total" v-model:page="queryParams.pageNum" v-model:limit="queryParams.pageSize" @pagination="getList" />

    <!-- 角色菜单弹框 -->
    <zr-dialog title="角色菜单权限" key="role" top="0vh" draggable="" v-model="showRoleScope" width="700px" @close="cancel">
      <el-form :model="roleForm" label-width="80px">
        <el-form-item label="菜单搜索">
          <el-input placeholder="请输入关键字进行过滤" v-model="searchText"></el-input>
        </el-form-item>
        <el-form-item label="菜单权限">
          <el-checkbox @change="handleCheckedTreeExpand($event, 'menu')">展开/折叠</el-checkbox>
          <el-tree
            class="tree-border"
            :data="menuOptions"
            show-checkbox
            ref="menuRef"
            node-key="id"
            :check-strictly="!roleForm.menuCheckStrictly"
            empty-text="加载中，请稍后"
            highlight-current
            :filter-node-method="menuFilterNode"
            :props="{ children: 'children', label: 'label', class: customNodeClass }">
            <template #default="{ node, data }">
              <div class="custom-tree-node">
                <span style="float: left">{{ node.label }}</span>
                <span style="float: right; margin-left: 10px">
                  <el-tag v-if="data.status == 1" type="danger">停用</el-tag>
                </span>
              </div>
            </template>
          </el-tree>
        </el-form-item>
      </el-form>
    </zr-dialog>
  </div>
</template>

<script setup name="rolefactory">
import { listRoleFactory } from '@/api/system/roleFactory.js'
import { dictFactory } from '@/api/system/factory.js'
import { roleMenuTreeselect } from '@/api/system/menu'
const { proxy } = getCurrentInstance()
const ids = ref([])
const loading = ref(false)
const showSearch = ref(true)
const queryParams = reactive({
  pageNum: 1,
  pageSize: 10,
  sort: '',
  sortType: 'asc',
  factoryId: undefined
})
const columns = ref([
  { visible: true, prop: 'factoryId', label: '工厂' },
  { visible: false, prop: 'roleId', label: '角色Id' },
  { visible: true, prop: 'roleName', label: '角色' },
  { visible: true, prop: 'status', label: '状态' },
  { visible: true, prop: 'userNum', label: '用户数' },
  { visible: true, prop: 'remark', label: '备注' }
])
const total = ref(0)
const dataList = ref([])
const queryRef = ref()

function getList() {
  loading.value = true
  listRoleFactory(queryParams).then((res) => {
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

const router = useRouter()
/** 分配用户操作 */
function handleAuthUser(row) {
  const roleId = row.roleId
  var hasPermi = proxy.$auth.hasPermi('system:rolefactory:user:list')
  if (hasPermi) {
    router.push({ path: '/system/roleFactory/users', query: { roleId } })
  } else {
    proxy.$modal.msgError('你没有权限访问')
  }
}

// 查询厂区
function handleQueryFactory() {
  const params = {
    pageNum: 1,
    pageSize: 100,
    sort: '',
    sortType: 'asc'
  }
  setTimeout(() => {
    dictFactory(params).then((res) => {
      options.value.factory_options = res.data.result
    })
  }, 200)
}
/*************** form操作 ***************/
// 操作类型 1、add 2、edit 3、view
const open = ref(false)
const state = reactive({
  options: {
    // 角色 选项列表 格式 eg:{ dictLabel: '标签', dictValue: '0'}
    factory_options: []
  }
})

const { options } = toRefs(state)

// 关闭dialog
function cancel() {
  open.value = false
}

/*************** 角色菜单操作 ***************/
const showRoleScope = ref(false)
// 菜单列表
const menuOptions = ref([])
const roleForm = ref({})
const searchText = ref('')
const menuRef = ref()

watch(searchText, (val) => {
  proxy.$refs.menuRef.filter(val)
})
// 新增 和上面代码基本相同
function handleDataScope(row) {
  showRoleScope.value = true
  const roleId = row.roleId || ids.value
  const roleMenu = getRoleMenuTreeselect(roleId)

  roleMenu.then((res) => {
    const checkedKeys = res.data.checkedKeys
    checkedKeys.forEach((v) => {
      nextTick(() => {
        proxy.$refs.menuRef.setChecked(v, true, false)
      })
    })
  })
  roleForm.value = {
    roleId: row.roleId,
    roleName: row.roleName,
    roleKey: row.roleKey,
    menuCheckStrictly: row.menuCheckStrictly
  }
}

/** 根据角色ID查询菜单树结构 */
function getRoleMenuTreeselect(roleId) {
  return roleMenuTreeselect(roleId).then((response) => {
    menuOptions.value = response.data.menus
    return response
  })
}

// 树权限（展开/折叠）
function handleCheckedTreeExpand(value, type) {
  if (type == 'menu') {
    const treeList = menuOptions.value
    for (let i = 0; i < treeList.length; i++) {
      proxy.$refs.menuRef.store.nodesMap[treeList[i].id].expanded = value
    }
  } else if (type == 'dept') {
    const treeList = deptOptions.value
    for (let i = 0; i < treeList.length; i++) {
      proxy.$refs.deptRef.store.nodesMap[treeList[i].id].expanded = value
    }
  }
}

// 菜单筛选
function menuFilterNode(value, data) {
  if (!value) return true
  return data.label.indexOf(value) !== -1
}
function customNodeClass(data, node) {
  if (data.menuType == 'C') {
    return 'tree-item-flex'
  }
  return null
}

handleQueryFactory()
handleQuery()
</script>
<style scoped>
/* tree border */
.tree-border {
  margin-top: 5px;
  border: 1px solid #e5e6e7;
  background: var(--base-bg-main) none;
  border-radius: 4px;
  width: 100%;
  height: 400px;
  overflow-y: auto;
}
</style>
