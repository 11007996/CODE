<!--
 * @Descripttion: (耗品存储表/CON_Consumable_Storage)
 * @Author: (admin)
 * @Date: (2024-05-18)
-->
<template>
  <div>
    <!-- 查询表单 -->
    <el-form :model="queryParams" label-position="right" inline ref="queryRef" v-show="showSearch" @submit.prevent>
      <el-form-item label="耗品" prop="consumableId">
        <el-select
          v-model="queryParams.consumableId"
          placeholder="请购料号,耗品名称,规格"
          clearable
          filterable
          remote
          :remote-method="handleQueryConsumable">
          <template #header>
            <span>请购料号 / 耗品名称 / 规格</span>
          </template>
          <el-option v-for="item in options.consumable_options" :key="item.dictValue" :label="item.dictLabel" :value="item.dictValue"> </el-option>
        </el-select>
      </el-form-item>
      <el-form-item label="储位" prop="storageId">
        <el-cascader
          class="w100"
          :options="useBasicStore().getConsumableStorageTree"
          :props="{ checkStrictly: true, value: 'storageId', label: 'storageName', emitPath: false }"
          placeholder="请选择储位"
          clearable
          v-model="queryParams.storageId">
          <template #default="{ node, data }">
            <span>{{ data.storageName }}</span>
            <span v-if="!node.isLeaf"> ({{ data.children.length }}) </span>
          </template>
        </el-cascader>
      </el-form-item>
      <el-form-item label="类别" prop="category">
        <el-select v-model="queryParams.category" clearable placeholder="请选择类别">
          <el-option v-for="item in options.category_options" :key="item.dictValue" :label="item.dictLabel" :value="item.dictValue"></el-option>
        </el-select>
      </el-form-item>
      <el-form-item label="请购料号" prop="consumablePart">
        <el-input v-model="queryParams.consumablePart" placeholder="请输入请购料号" />
      </el-form-item>
      <el-form-item label="耗品名称" prop="consumableName">
        <el-input v-model="queryParams.consumableName" placeholder="请输入耗品名称" />
      </el-form-item>
      <el-form-item label="规格" prop="spec">
        <el-input v-model="queryParams.spec" placeholder="请输入规格" />
      </el-form-item>
      <el-form-item>
        <el-button icon="search" type="primary" @click="handleQuery">{{ $t('btn.search') }}</el-button>
        <el-button icon="refresh" @click="resetQuery">{{ $t('btn.reset') }}</el-button>
      </el-form-item>
    </el-form>

    <!-- 工具区域 -->
    <el-row :gutter="15" class="mb10">
      <el-col :span="1.5">
        <el-button type="primary" v-hasPermi="['consumable:storage:in']" plain icon="plus" @click="handleOperate(null, 1)"> 入库 </el-button>
      </el-col>
      <!-- 导入 -->
      <el-col :span="1.5">
        <el-dropdown trigger="click" v-hasPermi="['consumable:storage:import']">
          <el-button type="primary" plain icon="Upload">
            {{ $t('btn.import') }}<el-icon class="el-icon--right"><arrow-down /></el-icon>
          </el-button>
          <template #dropdown>
            <el-dropdown-menu>
              <el-dropdown-item command="upload">
                <importData
                  templateUrl="consumable/consumableStorage/importTemplate"
                  importUrl="/consumable/consumableStorage/importDataCheck"
                  @success="(res) => handleFileSuccess(res, 8)"></importData>
              </el-dropdown-item>
            </el-dropdown-menu>
          </template>
        </el-dropdown>
      </el-col>
      <!-- 操作导入 -->
      <el-col :span="1.5">
        <el-dropdown trigger="click" v-hasPermi="['consumable:storage:import']">
          <el-button type="primary" plain icon="Upload">
            操作{{ $t('btn.import') }}<el-icon class="el-icon--right"><arrow-down /></el-icon>
          </el-button>
          <template #dropdown>
            <el-dropdown-menu>
              <el-dropdown-item command="upload">
                <importData
                  templateUrl="consumable/consumableStorage/importOperateTemplate"
                  importUrl="/consumable/consumableStorage/importOperateDataCheck"
                  @success="(res) => handleFileSuccess(res, 9)"></importData>
              </el-dropdown-item>
            </el-dropdown-menu>
          </template>
        </el-dropdown>
      </el-col>
      <el-col :span="1.5">
        <el-button type="warning" plain icon="download" @click="handleExport" v-hasPermi="['consumable:storage:export']">
          {{ $t('btn.export') }}
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
      <el-table-column prop="consumableId" label="耗品ID" width="90" align="center" v-if="columns.showColumn('consumableId')" />
      <el-table-column label="耗品(请购料号/名称/规格)" min-width="250" v-if="columns.showColumn('consumable')" :formatter="formatter" />
      <el-table-column prop="consumablePart" label="请购料号" v-if="columns.showColumn('consumablePart')" />
      <el-table-column prop="consumableName" label="耗品名称" v-if="columns.showColumn('consumableName')" />
      <el-table-column prop="spec" label="规格" v-if="columns.showColumn('spec')" />
      <el-table-column prop="storageId" label="储位ID" width="90" align="center" v-if="columns.showColumn('storageId')" />
      <el-table-column prop="storageFullName" label="储位" min-width="250" v-if="columns.showColumn('storageFullName')" />
      <el-table-column prop="qty" label="数量" width="90" align="center" v-if="columns.showColumn('qty')" />
      <el-table-column label="操作" width="420">
        <template #default="scope">
          <el-button type="primary" size="small" v-hasPermi="['consumable:storage:in']" @click="handleOperate(scope.row, 1)">入库</el-button>
          <el-button type="primary" size="small" v-hasPermi="['consumable:storage:out']" @click="handleOperate(scope.row, 2)">出库</el-button>
          <el-button type="primary" size="small" v-hasPermi="['consumable:storage:receive']" @click="handleOperate(scope.row, 4)">领用</el-button>
          <el-button type="primary" size="small" v-hasPermi="['consumable:storage:back']" @click="handleOperate(scope.row, 5)">归还</el-button>
          <el-button type="primary" size="small" v-hasPermi="['consumable:storage:scrapped']" @click="handleOperate(scope.row, 3)">报废</el-button>
          <el-button type="primary" size="small" v-hasPermi="['consumable:storage:transfer']" @click="handleOperate(scope.row, 6)">转移</el-button>
          <el-button
            type="danger"
            size="small"
            icon="delete"
            title="删除"
            v-hasPermi="['consumable:storage:delete']"
            @click="handleDelete(scope.row)"></el-button>
        </template>
      </el-table-column>
    </el-table>
    <pagination :total="total" v-model:page="queryParams.pageNum" v-model:limit="queryParams.pageSize" @pagination="getList" />

    <!-- 添加或修改耗品存储表对话框 -->
    <el-dialog :title="title" :lock-scroll="false" v-model="open">
      <el-form ref="formRef" :model="form" :rules="rules" label-width="100px">
        <el-row :gutter="20">
          <el-col :lg="24">
            <el-form-item label="耗品" prop="consumableId">
              <el-select
                v-model="form.consumableId"
                placeholder="请购料号,耗品名称,规格"
                clearable
                filterable
                remote
                :remote-method="handleQueryConsumable"
                class="fullWidth">
                <template #header>
                  <span>请购料号 / 耗品名称 / 规格</span>
                </template>
                <el-option v-for="item in options.consumable_options" :key="item.dictValue" :label="item.dictLabel" :value="item.dictValue">
                </el-option>
              </el-select>
            </el-form-item>
          </el-col>

          <el-col :lg="24">
            <el-form-item label="储位" prop="storageId">
              <el-cascader
                class="w100"
                :options="useBasicStore().getConsumableStorageTree"
                :props="{ checkStrictly: true, value: 'storageId', label: 'storageName', emitPath: false }"
                :disabled="opertype != 1"
                placeholder="请选择储位"
                clearable
                v-model="form.storageId">
                <template #default="{ node, data }">
                  <span>{{ data.storageName }}</span>
                  <span v-if="!node.isLeaf"> ({{ data.children.length }}) </span>
                </template>
              </el-cascader>
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="变动数量" prop="changeQty">
              <el-input-number v-model.number="form.changeQty" :controls="true" controls-position="right" placeholder="请输入数量" />
            </el-form-item>
          </el-col>

          <!-- 目标储位（转移专用） -->
          <el-col :lg="24" v-if="opertype == 6">
            <el-form-item label="目标储位" prop="newStorageId">
              <el-cascader
                class="w100"
                :options="useBasicStore().getConsumableStorageTree"
                :props="{ checkStrictly: true, value: 'storageId', label: 'storageName', emitPath: false }"
                placeholder="请选择储位"
                clearable
                v-model="form.newStorageId">
                <template #default="{ node, data }">
                  <span>{{ data.storageName }}</span>
                  <span v-if="!node.isLeaf"> ({{ data.children.length }}) </span>
                </template>
              </el-cascader>
            </el-form-item>
          </el-col>

          <el-col :lg="12" v-if="opertype == 4 || opertype == 5">
            <el-form-item :label="opertype == 4 ? '领用人' : '归还人'" prop="relatedUser">
              <el-select
                v-model="form.relatedUser"
                placeholder="请选择人员"
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

          <el-col :lg="12" v-if="opertype == 4">
            <el-form-item label="产线" prop="lineId">
              <el-select v-model="form.lineId" placeholder="请选择产线" clearable filterable class="fullWidth">
                <el-option
                  v-for="item in useBasicStore().getLineDict"
                  :key="item.dictValue"
                  :label="item.dictLabel"
                  :value="Number(item.dictValue)"></el-option>
              </el-select>
            </el-form-item>
          </el-col>

          <el-col :lg="24">
            <el-form-item label="备注">
              <el-input type="textarea" v-model="form.remark" :controls="true" controls-position="right" />
            </el-form-item>
          </el-col>
        </el-row>
      </el-form>
      <template #footer>
        <el-button text @click="cancel">{{ $t('btn.cancel') }}</el-button>
        <el-button type="primary" @click="submitForm">{{ $t('btn.submit') }}</el-button>
      </template>
    </el-dialog>

    <!-- 导入上传表单 -->
    <el-dialog :title="title" :lock-scroll="false" v-model="openImportResult" width="95%" center>
      <el-table :data="importDataList" ref="resultTable" border header-cell-class-name="el-table-header-cell" :row-class-name="tableRowClassName">
        <el-table-column label="序号" type="index" width="80" align="center" />
        <el-table-column prop="consumableId" label="耗品(名称/规格/料号)" min-width="250" align="center">
          <template #default="scope">
            <el-select
              v-model="scope.row.consumableId"
              placeholder="请购料号,耗品名称,规格"
              clearable
              filterable
              remote
              :remote-method="(keyword) => handleQueryConsumable(keyword, scope.row)"
              class="fullWidth">
              <template #header>
                <span>请购料号 / 耗品名称 / 规格</span>
              </template>
              <el-option v-for="item in scope.row.consumable_options" :key="item.dictValue" :label="item.dictLabel" :value="item.dictValue">
              </el-option>
            </el-select>
          </template>
        </el-table-column>
        <el-table-column prop="storageId" label="储位" min-width="300" align="center">
          <template #default="scope">
            <el-cascader
              v-model="scope.row.storageId"
              class="fullWidth"
              :options="useBasicStore().getConsumableStorageTree"
              :props="{ checkStrictly: true, value: 'storageId', label: 'storageName', emitPath: false }"
              placeholder="请选择储位"
              clearable>
              <template #default="{ node, data }">
                <span>{{ data.storageName }}</span>
                <span v-if="!node.isLeaf"> ({{ data.children.length }}) </span>
              </template>
            </el-cascader>
          </template>
        </el-table-column>
        <el-table-column prop="oldQty" label="原来数量" width="90" align="center" v-if="opertype == 8" />
        <el-table-column prop="qty" label="数量" width="120" align="center" v-if="opertype == 8">
          <template #default="scope">
            <el-input-number
              v-model.number="scope.row.qty"
              :controls="true"
              controls-position="right"
              placeholder="请输入库存数量"
              class="fullWidth" />
          </template>
        </el-table-column>
        <el-table-column prop="changeQty" label="变动数量" width="120" align="center">
          <template #default="scope">
            <el-input-number
              v-model.number="scope.row.changeQty"
              :controls="true"
              controls-position="right"
              placeholder="请输入变动数量"
              :disabled="opertype == 8"
              class="fullWidth" />
          </template>
        </el-table-column>
        <el-table-column prop="storageChangeType" label="变动类型" width="120" align="center">
          <template #default="scope">
            <dict-tag v-if="opertype == 8" :options="options.storage_change_type" :value="scope.row.storageChangeType" />
            <el-select v-if="opertype == 9" v-model="scope.row.storageChangeType" placeholder="请选择变动类型" clearable class="fullWidth">
              <el-option
                v-for="item in options.storage_change_type"
                :key="item.dictValue"
                :label="item.dictLabel"
                :value="item.dictValue"
                :disabled="item.dictLabel === '转移'"></el-option>
            </el-select>
          </template>
        </el-table-column>
        <el-table-column prop="relatedUser" label="领用/归还人" width="150" align="center" v-if="opertype == 9">
          <template #default="scope">
            <el-select
              v-model="scope.row.relatedUser"
              placeholder="请输入姓名"
              clearable
              filterable
              remote
              :remote-method="(keyword) => handleQueryEmployee(keyword, scope.row)"
              class="fullWidth">
              <el-option v-for="item in scope.row.emp_options" :key="item.dictValue" :label="item.dictLabel" :value="item.dictValue">
                {{ item.dictLabel }} {{ ' : ' + item.dictValue }}
              </el-option>
            </el-select>
          </template>
        </el-table-column>
        <el-table-column prop="lineId" label="产线" width="150" align="center" v-if="opertype == 9">
          <template #default="scope">
            <el-select v-model="scope.row.lineId" placeholder="请选择产线" clearable filterable class="fullWidth">
              <el-option
                v-for="item in useBasicStore().getLineDict"
                :key="item.dictValue"
                :label="item.dictLabel"
                :value="Number(item.dictValue)"></el-option>
            </el-select>
          </template>
        </el-table-column>
        <el-table-column prop="remark" label="备注" width="200" align="left">
          <template #default="scope">
            <el-input v-model="scope.row.remark" />
          </template>
        </el-table-column>
        <el-table-column prop="createBy" label="创建人" width="90" align="center" />
        <el-table-column prop="createTime" label="创建时间" width="160" align="center" />
        <el-table-column prop="errorDesc" label="错误描述" min-width="250" align="left" fixed="right" />
      </el-table>
      <template #footer>
        <el-button text @click="cancelImport">{{ $t('btn.cancel') }}</el-button>
        <el-button type="primary" @click="submitImport">{{ $t('btn.submit') }}</el-button>
      </template>
    </el-dialog>
  </div>
</template>

<script setup name="consumablestorage">
import importData from '@/components/ImportData'
import {
  listConsumableStorage,
  getConsumableStorage,
  inConsumableStorage,
  outConsumableStorage,
  scrappedConsumableStorage,
  receiveConsumableStorage,
  backConsumableStorage,
  transferConsumableStorage,
  importConsumableStorage,
  importConsumableStorageOperate,
  delConsumableStorage
} from '@/api/consumable/consumableStorage.js'
import { dictConsumableBase, dictConsumableCategory } from '@/api/consumable/consumableBase.js'
import useBasicStore from '@/store/modules/basic.js'
import { dictEmployee } from '@/api/basic/employee.js'

const props = defineProps({
  consumableId: Number
})
const { proxy } = getCurrentInstance()
const ids = ref([])
const loading = ref(false)
const showSearch = ref(true)
if (props.consumableId) showSearch.value = false
const queryParams = reactive({
  pageNum: 1,
  pageSize: 10,
  sort: '',
  sortType: 'asc',
  consumableId: props.consumableId,
  consumablePart: null,
  consumableName: null,
  spec: null,
  storageId: null
})
const columns = ref([
  { visible: false, prop: 'consumableId', label: '耗品ID' },
  { visible: true, prop: 'consumable', label: '耗品(料号/名称/规格)' },
  { visible: false, prop: 'consumablePart', label: '请购料号' },
  { visible: false, prop: 'consumableName', label: '耗品名称' },
  { visible: false, prop: 'spec', label: '规格' },
  { visible: false, prop: 'storageId', label: '储位ID' },
  { visible: true, prop: 'storageFullName', label: '储位' },
  { visible: true, prop: 'qty', label: '数量' }
])
const total = ref(0)
const dataList = ref([])
const queryRef = ref()

var dictParams = [{ dictType: 'storage_change_type' }, { dictType: 'ticket_type' }]
proxy.getDicts(dictParams).then((response) => {
  response.data.forEach((element) => {
    state.options[element.dictType] = element.list
  })
})

function formatter(row, cloumn) {
  return row.consumablePart + ' / ' + row.consumableName + ' / ' + row.spec
}

function getList() {
  loading.value = true
  listConsumableStorage(queryParams).then((res) => {
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
// 操作类型 1.入库 2.出库 3.报废 4.领用 5.归还 6.转移 ,8、导入 9、操作导入
const opertype = ref(0)
const open = ref(false)
const state = reactive({
  single: true,
  multiple: true,
  form: {},
  rules: {
    consumableId: [{ required: true, message: '耗品ID不能为空', trigger: 'blur' }],
    storageId: [{ required: true, message: '储位ID不能为空', trigger: 'blur' }],
    changeQty: [{ required: true, message: '变动数量不能为空', trigger: 'blur', type: 'number' }],
    relatedUser: [{ required: true, message: '领用/归还人不能为空', trigger: 'blur' }],
    newStorageId: [{ required: true, message: '目标储位不能为空', trigger: 'blur' }]
  },
  options: {
    // 变动类型 选项列表 格式 eg:{ dictLabel: '标签', dictValue: '0'}
    storage_change_type: [],
    // 耗品选择项
    consumable_options: [],
    // 员工
    emp_options: [],
    // 类别
    category_options: []
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
    consumableId: props.consumableId,
    storageId: null,
    changeQty: 0
  }
  proxy.resetForm('formRef')
}

// 操作 1.入库 2.出库 3.报废 4.领用 5.归还
function handleOperate(row, operate) {
  reset()
  const operateTitle = ['', '入库', '出库', '报废', '领用', '归还', '转移']
  title.value = operateTitle[operate]
  opertype.value = operate

  if (row) {
    options.value.consumable_options = [{ dictValue: row.consumableId, dictLabel: formatter(row) }]

    const params = {
      consumableId: row.consumableId,
      storageId: row.storageId
    }

    getConsumableStorage(params).then((res) => {
      const { code, data } = res
      if (code == 200) {
        open.value = true
        form.value = {
          ...data
        }
      }
    })
  } else {
    //入库
    form.value.changeQty = 0
    open.value = true
  }
}

// 入库&出库&报废&领用 表单提交
function submitForm() {
  proxy.$refs['formRef'].validate((valid) => {
    if (valid) {
      if (opertype.value === 1) {
        inConsumableStorage(form.value).then((res) => {
          proxy.$modal.msgSuccess('入库成功')
          open.value = false
          getList()
        })
      } else if (opertype.value === 2) {
        outConsumableStorage(form.value).then((res) => {
          proxy.$modal.msgSuccess('出库成功')
          open.value = false
          getList()
        })
      } else if (opertype.value === 3) {
        scrappedConsumableStorage(form.value).then((res) => {
          proxy.$modal.msgSuccess('报废成功')
          open.value = false
          getList()
        })
      } else if (opertype.value === 4) {
        receiveConsumableStorage(form.value).then((res) => {
          proxy.$modal.msgSuccess('领用成功')
          open.value = false
          getList()
        })
      } else if (opertype.value === 5) {
        backConsumableStorage(form.value).then((res) => {
          proxy.$modal.msgSuccess('归还成功')
          open.value = false
          getList()
        })
      } else if (opertype.value === 6) {
        transferConsumableStorage(form.value).then((res) => {
          proxy.$modal.msgSuccess('转移成功')
          open.value = false
          getList()
        })
      }
    }
  })
}

// 删除按钮操作
function handleDelete(row) {
  if (row.qty > 0) {
    proxy.$modal.msgError('库存数量不为0，无法删除')
    return
  }
  const query = {
    consumableId: row.consumableId,
    storageId: row.storageId
  }
  proxy
    .$confirm('是否确认删除数据项？', '警告', {
      confirmButtonText: proxy.$t('common.ok'),
      cancelButtonText: proxy.$t('common.cancel'),
      type: 'warning'
    })
    .then(function () {
      return delConsumableStorage(query)
    })
    .then(() => {
      getList()
      proxy.$modal.msgSuccess('删除成功')
    })
}

// 导出按钮操作
function handleExport() {
  proxy
    .$confirm('是否确认导出耗品库存表数据项?', '警告', {
      confirmButtonText: '确定',
      cancelButtonText: '取消',
      type: 'warning'
    })
    .then(async () => {
      await proxy.downFile('/consumable/consumableStorage/export', { ...queryParams })
    })
}

//********************************其他方法************************* */
//耗品查询
function handleQueryConsumable(keyword, row) {
  if (keyword) {
    const query = {
      pageNum: 1,
      pageSize: 10,
      sort: '',
      sortType: 'asc',
      keyword: keyword
    }

    if (keyword.indexOf(',') >= 0) {
      const kv = keyword.split(',')
      query.consumablePart = kv[0]
      query.consumableName = kv[1]
      query.spec = kv[2]
      query.keyword = null
    }
    setTimeout(() => {
      dictConsumableBase(query).then((res) => {
        if (row) row.consumable_options = res.data.result
        else options.value.consumable_options = res.data.result
      })
    }, 200)
  }
}

function handleQueryCategory() {
  const parm = {
    pageNum: 1,
    pageSize: 100,
    sort: '',
    sortType: 'asc'
  }
  dictConsumableCategory(parm).then((res) => {
    const { code, data } = res
    if (code == 200) {
      options.value.category_options = data.result
    }
  })
}

//员工查询
function handleQueryEmployee(keyword, row) {
  if (keyword) {
    const queryPartParams = {
      pageNum: 1,
      pageSize: 10,
      sort: '',
      sortType: 'asc',
      empName: keyword
    }
    setTimeout(() => {
      dictEmployee(queryPartParams).then((res) => {
        if (row) row.emp_options = res.data.result
        else state.options.emp_options = res.data.result
      })
    }, 200)
  }
}

//---------------------导入相关---------------------------
const openImportResult = ref(false)
const importDataList = ref([])

// 导入文件成功
const handleFileSuccess = (response, otype) => {
  importDataList.value = []
  opertype.value = otype
  const { item1, item2 } = response.data
  if (item1 == true) {
    proxy.$modal.msgSuccess('数据检查成功')
  } else {
    proxy.$modal.msgError('有错误数据,请检查')
  }
  refreshImportData(item2)
  title.value = otype == 8 ? '导入确认' : '操作导入确认'
  openImportResult.value = true
}

//刷新导入结果
function refreshImportData(data) {
  data.forEach((it) => {
    if (it.consumableId)
      it.consumable_options = [
        {
          dictValue: it.consumableId,
          dictLabel: it.consumablePart + ' / ' + it.consumableName + ' / ' + it.spec
        }
      ]
    if (it.relatedUser) it.emp_options = [{ dictValue: it.relatedUser, dictLabel: it.relatedUserName }]
  })
  importDataList.value = data
}

// 关闭dialog
function cancelImport() {
  openImportResult.value = false
  importDataList.value = []
}

// 导入提交
function submitImport() {
  const data = [...importDataList.value]
  if (opertype.value == 8) {
    //导入
    importConsumableStorage(data).then((res) => {
      const { item1, item2 } = res.data
      if (item1 == true) {
        proxy.$modal.msgSuccess('数据导入成功')
        openImportResult.value = false
        importDataList.value = []
        getList()
      } else {
        proxy.$modal.msgError('有错误数据,请检查')
        refreshImportData(item2)
      }
    })
  } else if (opertype.value == 9) {
    //操作导入提交
    importConsumableStorageOperate(data).then((res) => {
      const { item1, item2 } = res.data
      if (item1 == true) {
        proxy.$modal.msgSuccess('数据导入成功')
        openImportResult.value = false
        importDataList.value = []
        getList()
      } else {
        proxy.$modal.msgError('有错误数据,请检查')
        refreshImportData(item2)
      }
    })
  }
}

//行样式
const tableRowClassName = ({ row, rowIndex }) => {
  if (row.errorDesc) {
    return 'danger-row'
  }
  return ''
}

//监听属性变化
watch(props, (val) => {
  queryParams.consumableId = props.consumableId
  handleQuery()
})

handleQueryCategory()
handleQuery()
</script>
