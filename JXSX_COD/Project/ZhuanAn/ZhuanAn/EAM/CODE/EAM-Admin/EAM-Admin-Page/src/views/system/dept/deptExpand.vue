<!--
 * @Descripttion: (系统部门扩展/sys_dept_expand)
 * @Author: (admin)
 * @Date: (2026-01-29)
-->
<template>
  <div>
    <el-form :model="queryParams" label-position="right" inline ref="queryRef" v-show="showSearch" @submit.prevent>
      <el-form-item label="部门名称" prop="deptName">
        <el-input v-model="queryParams.deptName" placeholder="请输入部门名称" clearable />
      </el-form-item>
      <el-form-item label="厂区" prop="factoryId">
        <el-select v-model="queryParams.factoryId" placeholder="请选择工厂">
          <el-option v-for="item in options.factoryOptions" :key="item.dictValue" :label="item.dictLabel" :value="item.dictValue"></el-option>
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
        <el-button type="primary" v-hasPermi="['system:dept:expand:add']" plain icon="plus" @click="handleAdd">
          {{ $t('btn.add') }}
        </el-button>
        <el-button type="info" plain icon="sort" @click="toggleExpandAll">{{ $t('btn.expand') }}/{{ $t('btn.collapse') }}</el-button>
        <el-button type="success" v-hasPermi="['system:dept:expand:sync']" plain @click="handleSync"> 同步部门名称 </el-button>
      </el-col>
      <right-toolbar v-model:showSearch="showSearch" @queryTable="getList" :columns="columns"></right-toolbar>
    </el-row>

    <el-table
      v-if="refreshTable"
      :data="dataList"
      v-loading="loading"
      ref="table"
      border
      header-cell-class-name="el-table-header-cell"
      highlight-current-row
      row-key="sysDeptId"
      :default-expand-all="isExpandAll"
      :tree-props="{ children: 'children', hasChildren: 'hasChildren' }">
      <el-table-column prop="sysDeptId" label="系统部门Id" width="90" align="center" v-if="columns.showColumn('sysDeptId')" />
      <el-table-column prop="deptName" label="系统部门" v-if="columns.showColumn('deptName')" />
      <el-table-column prop="luxDeptId" label="立讯部门Id" align="center" width="120" v-if="columns.showColumn('luxDeptId')" />
      <el-table-column prop="wxDeptId" label="微信部门Id" align="center" width="120" v-if="columns.showColumn('wxDeptId')" />
      <el-table-column prop="defaultFactoryId" label="默认工厂" align="center" v-if="columns.showColumn('defaultFactoryId')">
        <template #default="scope">
          <dict-tag :options="options.factoryOptions" :value="scope.row.defaultFactoryId" />
        </template>
      </el-table-column>
      <el-table-column label="操作" width="130">
        <template #default="scope">
          <el-button
            type="success"
            size="small"
            icon="edit"
            title="编辑"
            v-hasPermi="['system:dept:expand:edit']"
            @click="handleUpdate(scope.row)"></el-button>
          <el-button
            type="danger"
            size="small"
            icon="delete"
            title="删除"
            v-hasPermi="['system:dept:expand:delete']"
            @click="handleDelete(scope.row)"></el-button>
        </template>
      </el-table-column>
    </el-table>
    <!-- <pagination :total="total" v-model:page="queryParams.pageNum" v-model:limit="queryParams.pageSize" @pagination="getList" /> -->

    <!-- 添加或修改系统部门扩展对话框 -->
    <el-dialog :title="title" :lock-scroll="false" v-model="open">
      <el-form ref="formRef" :model="form" :rules="rules" label-width="100px">
        <el-row :gutter="20">
          <el-col :lg="12">
            <el-form-item label="系统部门" prop="sysDeptId">
              <el-tree-select
                v-model="form.sysDeptId"
                :data="options.deptOptions"
                :props="{ value: 'id', label: 'label', children: 'children' }"
                value-key="id"
                placeholder="请选择根部门"
                check-strictly
                filterable
                :render-after-expand="false"
                :disabled="opertype == 2" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="立讯部门ID" prop="luxDeptId">
              <el-input v-model="form.luxDeptId" placeholder="请输入立讯部门ID" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="微信部门ID" prop="wxDeptId">
              <el-input v-model="form.wxDeptId" placeholder="请输入微信部门ID" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="默认工厂" prop="defaultFactoryId">
              <el-select v-model="form.defaultFactoryId" clearable placeholder="请选择默认工厂">
                <el-option v-for="item in options.factoryOptions" :key="item.dictValue" :label="item.dictLabel" :value="item.dictValue"></el-option>
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

<script setup name="sysdeptexpand">
import {
  listSysDeptExpand,
  addSysDeptExpand,
  delSysDeptExpand,
  updateSysDeptExpand,
  getSysDeptExpand,
  SyncSysDeptExpand
} from '@/api/system/deptExpand.js'
import { treeDept } from '@/api/system/dept.js'
import { dictFactory } from '@/api/system/factory'
const { proxy } = getCurrentInstance()
const ids = ref([])
const loading = ref(false)
const showSearch = ref(true)
// 是否展开，默认全部折叠
const isExpandAll = ref(false)
// 重新渲染表格状态
const refreshTable = ref(true)
const queryParams = reactive({
  factoryId: undefined,
  sysDeptId: undefined,
  deptName: undefined
})
const columns = ref([
  { visible: false, prop: 'sysDeptId', label: '系统部门ID' },
  { visible: true, prop: 'deptName', label: '系统部门' },
  { visible: true, prop: 'luxDeptId', label: '立讯部门ID' },
  { visible: true, prop: 'wxDeptId', label: '微信部门ID' },
  { visible: true, prop: 'defaultFactoryId', label: '默认工厂' }
])
const dataList = ref([])
const queryRef = ref()

function getList() {
  loading.value = true
  listSysDeptExpand(queryParams).then((res) => {
    const { code, data } = res
    if (code == 200) {
      dataList.value = proxy.handleTree(data, 'sysDeptId')
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
  proxy.$refs.deptTreeRef.setCurrentKey(null)
  handleQuery()
}

// 同步部门名称
function handleSync() {
  loading.value = true
  SyncSysDeptExpand()
    .then((res) => {
      const { code, data } = res
      if (code == 200) {
        proxy.$modal.msgSuccess('同步成功')
        getList()
      }
      open.value = false
    })
    .catch((err) => {
      open.value = false
    })
}

//展开/折叠操作
function toggleExpandAll() {
  refreshTable.value = false
  isExpandAll.value = !isExpandAll.value
  nextTick(() => {
    refreshTable.value = true
  })
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
    sysDeptId: [{ required: true, message: '系统部门ID不能为空', trigger: 'change', type: 'number' }]
  },
  options: {
    // 系统部门ID 选项列表 格式 eg:{ dictLabel: '标签', dictValue: '0'}
    deptOptions: undefined,
    factoryOptions: undefined
  }
})

const { form, rules, options } = toRefs(state)

// 关闭dialog
function cancel() {
  open.value = false
  reset()
}

// 重置表单
function reset() {
  form.value = {
    sysDeptId: null,
    luxDeptId: null,
    wxDeptId: null,
    defaultFactoryId: null
  }
  proxy.resetForm('formRef')
}

// 添加按钮操作
function handleAdd() {
  reset()
  open.value = true
  title.value = '添加系统部门扩展'
  opertype.value = 1
}
// 修改按钮操作
function handleUpdate(row) {
  reset()
  const id = row.sysDeptId || ids.value
  getSysDeptExpand(id).then((res) => {
    const { code, data } = res
    if (code == 200) {
      open.value = true
      title.value = '修改系统部门扩展'
      opertype.value = 2

      form.value = {
        ...data
      }
    } else if (code == 210) {
      open.value = true
      title.value = '新增系统部门扩展'
      opertype.value = 1
      form.value.sysDeptId = id
    }
  })
}

// 添加&修改 表单提交
function submitForm() {
  proxy.$refs['formRef'].validate((valid) => {
    if (valid) {
      if (form.value.sysDeptId != undefined && opertype.value === 2) {
        updateSysDeptExpand(form.value).then((res) => {
          proxy.$modal.msgSuccess('修改成功')
          open.value = false
          getList()
        })
      } else {
        addSysDeptExpand(form.value).then((res) => {
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
  const Ids = row.sysDeptId || ids.value

  proxy
    .$confirm('是否确认删除参数编号为"' + Ids + '"的数据项？', '警告', {
      confirmButtonText: proxy.$t('common.ok'),
      cancelButtonText: proxy.$t('common.cancel'),
      type: 'warning'
    })
    .then(function () {
      return delSysDeptExpand(Ids)
    })
    .then(() => {
      getList()
      proxy.$modal.msgSuccess('删除成功')
    })
}

//初始化部门树
function initDeptTreeData() {
  // 判断部门的数据是否存在，存在不获取，不存在则获取
  if (options.value.deptOptions === undefined) {
    treeDept().then((res) => {
      options.value.deptOptions = res.data
    })
  }
}

//初始化厂区选项
function initFactoryData() {
  // 判断部门的数据是否存在，存在不获取，不存在则获取
  if (options.value.factoryOptions === undefined) {
    let params = {
      pageNum: 1,
      pageSize: 100,
      sort: 'factoryId',
      sortType: 'asc'
    }
    dictFactory(params).then((res) => {
      options.value.factoryOptions = res.data.result
    })
  }
}

initDeptTreeData()
initFactoryData()
handleQuery()
</script>
