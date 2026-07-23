<!--
 * @Descripttion: (治具储位信息/Fixture_Storage_Space)
 * @Author: (admin)
 * @Date: (2024-05-07)
-->
<template>
  <div>
    <el-form :model="queryParams" label-position="right" inline ref="queryRef" v-show="showSearch" @submit.prevent>
      <el-form-item label="储位名称" prop="storageName">
        <el-input v-model="queryParams.storageName" placeholder="请输入储位名称" />
      </el-form-item>
      <el-form-item>
        <el-button icon="search" type="primary" @click="handleQuery">{{ $t('btn.search') }}</el-button>
        <el-button icon="refresh" @click="resetQuery">{{ $t('btn.reset') }}</el-button>
      </el-form-item>
    </el-form>
    <!-- 工具区域 -->
    <el-row :gutter="10" class="mb8">
      <el-col :span="1.5">
        <el-button type="primary" v-hasPermi="['fixture:storageSpace:add']" plain icon="plus" @click="handleAdd">
          {{ $t('btn.add') }}
        </el-button>
      </el-col>
      <el-col :span="1.5">
        <el-button type="info" plain icon="sort" @click="toggleExpandAll">展开/折叠</el-button>
      </el-col>
      <right-toolbar v-model:showSearch="showSearch" @queryTable="getList" :columns="columns"></right-toolbar>
    </el-row>

    <!-- 数据区域 -->
    <el-table
      v-if="refreshTable"
      :data="dataList"
      v-loading="loading"
      ref="tableRef"
      border
      highlight-current-row
      @selection-change="handleSelectionChange"
      :default-expand-all="isExpandAll"
      row-key="storageId"
      :tree-props="{ children: 'children', hasChildren: 'hasChildren' }">
      <el-table-column type="selection" width="50" align="center" />
      <el-table-column
        prop="storageName"
        label="储位名称"
        min-width="160"
        align="left"
        :show-overflow-tooltip="true"
        v-if="columns.showColumn('storageName')" />
      <el-table-column prop="storageId" label="储位Id" align="center" v-if="columns.showColumn('storageId')" />
      <el-table-column prop="storageType" label="储位类型" width="90" align="center" v-if="columns.showColumn('storageType')">
        <template #default="scope">
          <dict-tag :options="options.storage_type" :value="scope.row.storageType" />
        </template>
      </el-table-column>
      <el-table-column prop="orderNum" label="排序" width="90" align="center" v-if="columns.showColumn('orderNum')" />
      <el-table-column prop="status" label="储位状态" width="90" align="center" v-if="columns.showColumn('status')">
        <template #default="scope">
          <dict-tag :options="options.sys_normal_disable" :value="scope.row.status" />
        </template>
      </el-table-column>
      <el-table-column prop="storageFullName" label="储位全名" v-if="columns.showColumn('storageFullName')" />
      <el-table-column prop="ancestors" label="祖级列表" v-if="columns.showColumn('ancestors')" />
      <el-table-column
        prop="createBy"
        label="创建人"
        width="90"
        align="center"
        :show-overflow-tooltip="true"
        v-if="columns.showColumn('createBy')" />
      <el-table-column
        prop="createTime"
        label="创建时间"
        width="160"
        align="center"
        :show-overflow-tooltip="true"
        v-if="columns.showColumn('createTime')" />
      <el-table-column
        prop="updateBy"
        label="更新人"
        width="90"
        align="center"
        :show-overflow-tooltip="true"
        v-if="columns.showColumn('updateBy')" />
      <el-table-column
        prop="updateTime"
        label="更新时间"
        width="160"
        align="center"
        :show-overflow-tooltip="true"
        v-if="columns.showColumn('updateTime')" />
      <el-table-column prop="remark" label="备注" align="center" :show-overflow-tooltip="true" v-if="columns.showColumn('remark')" />

      <el-table-column label="操作" width="160">
        <template #default="scope">
          <!-- 非【储位】类型显示【增加】按钮 -->
          <el-button
            :disabled="scope.row.storageType == '3'"
            v-hasPermi="['fixture:storageSpace:add']"
            type="primary"
            size="small"
            icon="plus"
            title="增加"
            @click="handleAdd(scope.row)">
          </el-button>
          <el-button
            v-hasPermi="['fixture:storageSpace:edit']"
            type="success"
            size="small"
            icon="edit"
            title="编辑"
            @click="handleUpdate(scope.row)"></el-button>
          <el-button
            v-hasPermi="['fixture:storageSpace:delete']"
            type="danger"
            size="small"
            icon="delete"
            title="删除"
            @click="handleDelete(scope.row)"></el-button>
        </template>
      </el-table-column>
    </el-table>

    <!-- 添加或修改储位信息对话框 -->
    <el-dialog :title="title" :lock-scroll="false" v-model="open">
      <el-form ref="formRef" :model="form" :rules="rules" label-width="100px">
        <el-row :gutter="20">
          <el-col :lg="12" v-if="opertype == 2">
            <el-form-item label="储位Id">{{ form.storageId }}</el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="储位名称" prop="storageName">
              <el-input v-model="form.storageName" placeholder="请输入储位名称" />
            </el-form-item>
          </el-col>

          <el-col :lg="24">
            <el-form-item label="上级" prop="parentId">
              <el-cascader
                ref="parentIdRef"
                class="fullWidth"
                :options="dataList"
                :props="{ checkStrictly: true, value: 'storageId', label: 'storageName', emitPath: false }"
                placeholder="请选择上层位置"
                clearable
                v-model="form.parentId"
                @change="handleParentIdChange">
                <template #default="{ node, data }">
                  <span>{{ data.storageName }}</span>
                  <span v-if="!node.isLeaf"> ({{ data.children.length }}) </span>
                </template>
              </el-cascader>
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="储位类型" prop="storageType">
              <el-select v-model="form.storageType" placeholder="请选择储位类型" class="fullWidth">
                <el-option
                  v-for="item in options.storage_type"
                  :key="item.dictValue"
                  :label="item.dictLabel"
                  :value="item.dictValue"
                  :disabled="parseInt(item.dictValue) <= parseInt(form.parentStorageType)"></el-option>
              </el-select>
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="储位状态" prop="status">
              <el-radio-group v-model="form.status">
                <el-radio v-for="item in options.sys_normal_disable" :key="item.dictValue" :value="item.dictValue">{{ item.dictLabel }}</el-radio>
              </el-radio-group>
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="显示顺序" prop="orderNum">
              <el-input v-model="form.orderNum" placeholder="请输入显示顺序" />
            </el-form-item>
          </el-col>

          <el-col :lg="24">
            <el-form-item label="备注" prop="remark">
              <el-input type="textarea" v-model="form.remark" placeholder="请输入备注" />
            </el-form-item>
          </el-col>
        </el-row>
      </el-form>
      <template #footer>
        <div class="dialog-footer">
          <el-button text @click="cancel">{{ $t('btn.cancel') }}</el-button>
          <el-button type="primary" @click="submitForm">{{ $t('btn.submit') }}</el-button>
        </div>
      </template>
    </el-dialog>
  </div>
</template>

<script setup name="fixturestoragespace">
import {
  treeFixtureStorageSpace,
  listStorageSpace,
  addStorageSpace,
  delStorageSpace,
  updateStorageSpace,
  getStorageSpace
} from '@/api/fixture/fixtureStorageSpace.js'

const { proxy } = getCurrentInstance()
const isExpandAll = ref(false)
const refreshTable = ref(true)
function toggleExpandAll() {
  refreshTable.value = false
  isExpandAll.value = !isExpandAll.value
  nextTick(() => {
    refreshTable.value = true
  })
}

// 选中storageId数组数组
const ids = ref([])
const single = ref(true)
const multiple = ref(true)
const loading = ref(false)
const showSearch = ref(true)
const queryParams = reactive({
  pageNum: 1,
  pageSize: 10,
  sort: 'OrderNum',
  sortType: 'asc',
  storageName: undefined
})
const columns = ref([
  { visible: true, prop: 'storageName', label: '储位名称' },
  { visible: false, prop: 'storageId', label: '储位ID' },
  { visible: true, prop: 'storageType', label: '储位类型' },
  { visible: false, prop: 'orderNum', label: '排序' },
  { visible: false, prop: 'storageFullName', label: '储位全名' },
  { visible: false, prop: 'ancestors', label: '祖级列表' },
  { visible: true, prop: 'status', label: '储位状态' },
  { visible: true, prop: 'createBy', label: '创建人' },
  { visible: false, prop: 'createTime', label: '创建时间' },
  { visible: false, prop: 'updateBy', label: '更新人' },
  { visible: false, prop: 'updateTime', label: '最后更新' },
  { visible: false, prop: 'remark', label: '备注' }
])
const total = ref(0)
const dataList = ref([])
const queryRef = ref()

var dictParams = [{ dictType: 'storage_type' }, { dictType: 'sys_normal_disable' }]

proxy.getDicts(dictParams).then((response) => {
  response.data.forEach((element) => {
    state.options[element.dictType] = element.list
  })
})

function getList() {
  loading.value = true
  treeFixtureStorageSpace(queryParams)
    .then((res) => {
      const { code, data } = res
      if (code == 200) {
        dataList.value = res.data
        loading.value = false
      }
    })
    .catch(() => {
      loading.value = false
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

// 多选框选中数据
function handleSelectionChange(selection) {
  ids.value = selection.map((item) => item.storageId)
  single.value = selection.length != 1
  multiple.value = !selection.length
}

// 自定义排序
function sortChange(column) {
  var sort = undefined
  var sortType = undefined

  if (column.prop != null && column.order != null) {
    sort = column.prop
    sortType = column.order

    if (column.prop == 'orderNum') {
      sort = 'order_Num'
    }
  }
  queryParams.sort = sort
  queryParams.sortType = sortType
  handleQuery()
}

/*************** form操作 ***************/
const formRef = ref()
const parentIdRef = ref()
const title = ref('')
// 操作类型 1、add 2、edit
const opertype = ref(0)
const open = ref(false)
const state = reactive({
  form: {},
  rules: {
    storageName: [{ required: true, message: '储位名称不能为空', trigger: 'blur' }],
    storageType: [{ required: true, message: '储位类型不能为空', trigger: 'change' }],
    status: [{ required: true, message: '储位状态不能为空', trigger: 'blur' }]
  },
  options: {
    // 父级ID 选项列表 格式 eg:{ dictLabel: '标签', dictValue: '0'}
    parentId_options: [],
    // 储位类型 选项列表 格式 eg:{ dictLabel: '标签', dictValue: '0'}  //0:仓库，1:区域，2货架，3:储位
    storage_type: [],
    // 储位状态 选项列表 格式 eg:{ dictLabel: '标签', dictValue: '0'}
    sys_normal_disable: []
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
  proxy.resetForm('formRef')
  form.value = {
    storageId: null,
    parentId: null,
    storageName: null,
    orderNum: null,
    storageType: null,
    status: null,
    remark: null,
    parentStorageType: null //上级储位类型
  }
}

//上级变动事件
function handleParentIdChange(val) {
  if (val) {
    const parentStorageType = parentIdRef.value.getCheckedNodes()[0].data.storageType
    form.value.parentStorageType = parentStorageType
    form.value.storageType = '' + (parseInt(parentStorageType) + 1)
    if (parseInt(form.value.storageType) > 3) {
      form.value.storageType = ''
    }
  } else {
    form.value.parentStorageType = '-1'
    form.value.storageType = '0'
  }
}

// 添加按钮操作
function handleAdd(row) {
  reset()
  if (row.storageId) {
    form.value.parentId = row.storageId
    form.value.parentStorageType = row.storageType
    form.value.storageType = '' + (parseInt(row.storageType) + 1)
  } else {
    form.value.parentId = 0
    form.value.storageType = '0'
    form.value.parentStorageType = '-1'
  }
  form.value.status = '0'
  open.value = true
  title.value = '添加'
  opertype.value = 1
}

// 修改按钮操作
function handleUpdate(row) {
  reset()
  const id = row.storageId || ids.value
  getStorageSpace(id).then((res) => {
    const { code, data } = res
    if (code == 200) {
      open.value = true
      title.value = '修改数据'
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
      if (form.value.storageId != undefined && opertype.value === 2) {
        updateStorageSpace(form.value)
          .then((res) => {
            proxy.$modal.msgSuccess('修改成功')
            open.value = false
            getList()
          })
          .catch(() => {})
      } else {
        addStorageSpace(form.value)
          .then((res) => {
            proxy.$modal.msgSuccess('新增成功')
            open.value = false
            getList()
          })
          .catch(() => {})
      }
    }
  })
}

// 删除按钮操作
function handleDelete(row) {
  const Ids = row.storageId || ids.value

  proxy
    .$confirm('是否确认删除参数编号为"' + Ids + '"的数据项？')
    .then(function () {
      return delStorageSpace(Ids)
    })
    .then(() => {
      getList()
      proxy.$modal.msgSuccess('删除成功')
    })
    .catch(() => {})
}

handleQuery()
</script>
