<!--
 * @Descripttion: (治具存储/FIX_Fixture_Storage)
 * @Author: (admin)
 * @Date: (2024-05-07)
-->
<template>
  <div>
    <!-- 查询条件 -->
    <el-form :model="queryParams" label-position="right" inline ref="queryRef" v-show="showSearch" @submit.prevent>
      <el-form-item label="治具" prop="fixtureId">
        <el-select v-model="queryParams.fixtureId" placeholder="系列,名称" clearable filterable remote :remote-method="handleQueryFixture">
          <template #header>
            <span>系列 / 名称</span>
          </template>
          <el-option v-for="item in options.fixture_options" :key="item.dictValue" :label="item.dictLabel" :value="item.dictValue"></el-option>
        </el-select>
      </el-form-item>
      <el-form-item label="储位" prop="storageId">
        <el-cascader
          class="w100"
          :options="useBasicStore().getFixtureStorageTree"
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
      <el-form-item label="系列" prop="series">
        <el-input v-model="queryParams.series" placeholder="请输入系列料号" />
      </el-form-item>
      <el-form-item label="治具名称" prop="fixtureName">
        <el-input v-model="queryParams.fixtureName" placeholder="请输入治具名称" />
      </el-form-item>
      <el-form-item>
        <el-button icon="search" type="primary" @click="handleQuery">{{ $t('btn.search') }}</el-button>
        <el-button icon="refresh" @click="resetQuery">{{ $t('btn.reset') }}</el-button>
      </el-form-item>
    </el-form>

    <!-- 工具区域 -->
    <el-row :gutter="15" class="mb10">
      <el-col :span="1.5">
        <el-button type="primary" v-hasPermi="['fixture:storage:in']" plain icon="plus" @click="handleOperate(null, 1)"> 入库 </el-button>
      </el-col>
      <!-- 导入 -->
      <el-col :span="1.5">
        <el-dropdown trigger="click" v-hasPermi="['fixture:storage:import']">
          <el-button type="primary" plain icon="Upload">
            {{ $t('btn.import') }}<el-icon class="el-icon--right"><arrow-down /></el-icon>
          </el-button>
          <template #dropdown>
            <el-dropdown-menu>
              <el-dropdown-item command="upload">
                <importData
                  templateUrl="fixture/fixtureStorage/importTemplate"
                  importUrl="/fixture/fixtureStorage/importDataCheck"
                  @success="(res) => handleFileSuccess(res, 8)"></importData>
              </el-dropdown-item>
            </el-dropdown-menu>
          </template>
        </el-dropdown>
      </el-col>
      <!-- 操作导入 -->
      <el-col :span="1.5">
        <el-dropdown trigger="click" v-hasPermi="['fixture:storage:import']">
          <el-button type="primary" plain icon="Upload">
            操作{{ $t('btn.import') }}<el-icon class="el-icon--right"><arrow-down /></el-icon>
          </el-button>
          <template #dropdown>
            <el-dropdown-menu>
              <el-dropdown-item command="upload">
                <importData
                  templateUrl="fixture/fixtureStorage/importOperateTemplate"
                  importUrl="/fixture/fixtureStorage/importOperateDataCheck"
                  @success="(res) => handleFileSuccess(res, 9)"></importData>
              </el-dropdown-item>
            </el-dropdown-menu>
          </template>
        </el-dropdown>
      </el-col>
      <el-col :span="1.5">
        <el-button type="warning" plain icon="download" @click="handleExport" v-hasPermi="['fixture:storage:export']">
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
      <el-table-column prop="fixtureId" label="治具ID" align="center" v-if="columns.showColumn('fixtureId')" />
      <el-table-column label="治具(系列/名称)" min-width="250" v-if="columns.showColumn('fixture')" :formatter="formatter" />
      <el-table-column prop="storageId" label="储位ID" width="90" align="center" v-if="columns.showColumn('storageId')" />
      <el-table-column prop="storageFullName" label="储位" min-width="250" v-if="columns.showColumn('storageFullName')" />
      <el-table-column prop="qty" label="数量" width="90" align="center" v-if="columns.showColumn('qty')" />
      <el-table-column label="操作" width="360">
        <template #default="scope">
          <el-button type="primary" size="small" v-hasPermi="['fixture:storage:in']" @click="handleOperate(scope.row, 1)">入库</el-button>
          <el-button type="primary" size="small" v-hasPermi="['fixture:storage:out']" @click="handleOperate(scope.row, 2)">出库</el-button>
          <el-button type="primary" size="small" v-hasPermi="['fixture:storage:receive']" @click="handleOperate(scope.row, 4)">领用</el-button>
          <el-button type="primary" size="small" v-hasPermi="['fixture:storage:scrapped']" @click="handleOperate(scope.row, 3)">报废</el-button>
          <el-button type="primary" size="small" v-hasPermi="['fixture:storage:transfer']" @click="handleOperate(scope.row, 6)">转移</el-button>
          <el-button
            type="danger"
            size="small"
            icon="delete"
            title="删除"
            v-hasPermi="['fixture:storage:delete']"
            @click="handleDelete(scope.row)"></el-button>
        </template>
      </el-table-column>
    </el-table>
    <pagination :total="total" v-model:page="queryParams.pageNum" v-model:limit="queryParams.pageSize" @pagination="getList" />

    <!-- 添加或修改治具存储对话框 -->
    <el-dialog :title="title" :lock-scroll="false" v-model="open">
      <el-form ref="formRef" :model="form" :rules="rules" label-width="100px">
        <el-row :gutter="20">
          <el-col :lg="24">
            <el-form-item label="治具" prop="fixtureId">
              <el-select
                v-model="form.fixtureId"
                placeholder="系列,名称"
                clearable
                filterable
                remote
                :remote-method="handleQueryFixture"
                class="fullWidth">
                <template #header>
                  <span>系列 / 名称</span>
                </template>
                <el-option
                  v-for="item in options.fixture_options"
                  :key="item.dictValue"
                  :label="item.dictLabel"
                  :value="item.dictValue"></el-option>
              </el-select>
            </el-form-item>
          </el-col>

          <el-col :lg="24">
            <el-form-item label="储位" prop="storageId">
              <el-cascader
                class="w100"
                :options="useBasicStore().getFixtureStorageTree"
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
              <el-input-number
                v-model.number="form.changeQty"
                :controls="true"
                controls-position="right"
                placeholder="请输入数量"
                class="fullWidth" />
            </el-form-item>
          </el-col>

          <!-- 目标储位（转移专用） -->
          <el-col :lg="24" v-if="opertype == 6">
            <el-form-item label="目标储位" prop="newStorageId">
              <el-cascader
                class="w100"
                :options="useBasicStore().getFixtureStorageTree"
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

          <el-col :lg="12" v-if="opertype == 4">
            <el-form-item label="领用人" prop="relatedUser">
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

    <!-- 导入表单 -->
    <el-dialog :title="title" :lock-scroll="false" v-model="openImportResult" width="95%" center>
      <el-table :data="importDataList" ref="resultTable" border header-cell-class-name="el-table-header-cell" :row-class-name="tableRowClassName">
        <el-table-column label="序号" type="index" width="80" align="center" />
        <el-table-column prop="fixtureId" label="治具(名称/系列)" min-width="250" align="center">
          <template #default="scope">
            <el-select
              v-model="scope.row.fixtureId"
              placeholder="系列,名称"
              clearable
              filterable
              remote
              :remote-method="(keyword) => handleQueryFixture(keyword, scope.row)"
              class="fullWidth">
              <template #header>
                <span>系列 / 名称</span>
              </template>
              <el-option
                v-for="item in scope.row.fixture_options"
                :key="item.dictValue"
                :label="item.dictLabel"
                :value="item.dictValue"></el-option>
            </el-select>
          </template>
        </el-table-column>
        <el-table-column prop="storageId" label="储位" min-width="300" align="center">
          <template #default="scope">
            <el-cascader
              v-model="scope.row.storageId"
              class="fullWidth"
              :options="useBasicStore().getFixtureStorageTree"
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
              class="fullWidth"
              :disabled="opertype == 8" />
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
        <el-table-column prop="relatedUser" label="领用人" width="150" align="center" v-if="opertype == 9">
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

<script setup name="fixturestorage">
import importData from '@/components/ImportData'
import {
  listFixtureStorage,
  getFixtureStorage,
  inFixtureStorage,
  outFixtureStorage,
  scrappedFixtureStorage,
  receiveFixtureStorage,
  transferFixtureStorage,
  importFixtureStorage,
  importFixtureStorageOperate,
  delFixtureStorage
} from '@/api/fixture/fixtureStorage.js'
import useBasicStore from '@/store/modules/basic.js'
import { dictEmployee } from '@/api/basic/employee.js'
import { dictFixtureBase } from '@/api/fixture/fixtureBase.js'

const { proxy } = getCurrentInstance()
const props = defineProps({
  fixtureId: Number
})
const ids = ref([])
const loading = ref(false)
const showSearch = ref(true)
if (props.fixtureId) showSearch.value = false
const queryParams = reactive({
  pageNum: 1,
  pageSize: 10,
  sort: '',
  sortType: 'asc',
  fixtureId: props.fixtureId,
  storageId: null,
  series: null,
  fixtureName: null
})
const columns = ref([
  { visible: false, prop: 'fixtureId', label: '治具ID' },
  { visible: true, prop: 'fixture', label: '治具(系列/名称)' },
  { visible: false, prop: 'storageId', label: '储位ID' },
  { visible: true, prop: 'storageFullName', label: '储位' },
  { visible: true, prop: 'qty', label: '数量' }
])
const total = ref(0)
const dataList = ref([])
const queryRef = ref()

const defaultTime = ref([new Date(2000, 1, 1, 0, 0, 0), new Date(2000, 2, 1, 23, 59, 59)])

var dictParams = [{ dictType: 'storage_change_type' }]

proxy.getDicts(dictParams).then((response) => {
  response.data.forEach((element) => {
    state.options[element.dictType] = element.list
  })
})

//列表
function getList() {
  loading.value = true
  listFixtureStorage(queryParams).then((res) => {
    const { code, data } = res
    if (code == 200) {
      dataList.value = data.result
      total.value = data.totalNum
      loading.value = false
    }
  })
}

function formatter(row, cloumn) {
  return row.series + ' / ' + row.fixtureName
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
// 操作类型 1、入库 2、出库 3、报废 4、领用
const opertype = ref(0)
const open = ref(false)
const state = reactive({
  single: true,
  multiple: true,
  form: {},
  changeForm: {},
  rules: {
    fixtureId: [{ required: true, message: '治具ID不能为空', trigger: 'blur' }],
    storageId: [{ required: true, message: '储位ID不能为空', trigger: 'change' }],
    changeQty: [{ required: true, message: '数量不能为空', trigger: 'blur', type: 'number' }],
    relatedUser: [{ required: true, message: '领用人不能为空', trigger: 'blur' }],
    lineId: [{ required: true, message: '产线不能为空', trigger: 'blur' }],
    newStorageId: [{ required: true, message: '目标储位不能为空', trigger: 'blur' }]
  },
  options: {
    // 变动类型 选项列表 格式 eg:{ dictLabel: '标签', dictValue: '0'}
    storage_change_type: [],
    //治具选项
    fixture_options: [],
    //员工
    emp_options: []
  }
})

const { form, changeForm, rules, options, single, multiple } = toRefs(state)

// 关闭dialog
function cancel() {
  open.value = false
  reset()
}

// 重置表单
function reset() {
  form.value = {
    fixtureId: null,
    storageId: null,
    changeQty: 0
  }
  proxy.resetForm('formRef')
}

// 操作 1.入库 2.出库 3.报废 4.领用 5.归还 6.转移
function handleOperate(row, operate) {
  reset()
  const operateTitle = ['', '入库', '出库', '报废', '领用', '归还', '转移']
  title.value = operateTitle[operate]
  opertype.value = operate

  if (row) {
    options.value.fixture_options = [{ dictValue: row.fixtureId, dictLabel: formatter(row) }]
    const params = {
      fixtureId: row.fixtureId,
      storageId: row.storageId
    }
    getFixtureStorage(params).then((res) => {
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
        inFixtureStorage(form.value).then((res) => {
          proxy.$modal.msgSuccess('入库成功')
          open.value = false
          getList()
        })
      } else if (opertype.value === 2) {
        outFixtureStorage(form.value).then((res) => {
          proxy.$modal.msgSuccess('出库成功')
          open.value = false
          getList()
        })
      } else if (opertype.value === 3) {
        scrappedFixtureStorage(form.value).then((res) => {
          proxy.$modal.msgSuccess('报废成功')
          open.value = false
          getList()
        })
      } else if (opertype.value === 4) {
        receiveFixtureStorage(form.value).then((res) => {
          proxy.$modal.msgSuccess('领用成功')
          open.value = false
          getList()
        })
      } else if (opertype.value === 6) {
        transferFixtureStorage(form.value).then((res) => {
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
    proxy.$modal.msgError('储位库存数量不为0，无法删除')
    return
  }
  const query = {
    fixtureId: row.fixtureId,
    storageId: row.storageId
  }
  proxy
    .$confirm('是否确认删除数据项？', '警告', {
      confirmButtonText: proxy.$t('common.ok'),
      cancelButtonText: proxy.$t('common.cancel'),
      type: 'warning'
    })
    .then(function () {
      return delFixtureStorage(query)
    })
    .then(() => {
      getList()
      proxy.$modal.msgSuccess('删除成功')
    })
}

// 导出按钮操作
function handleExport() {
  proxy
    .$confirm('是否确认导出治具库存表数据项?', '警告', {
      confirmButtonText: '确定',
      cancelButtonText: '取消',
      type: 'warning'
    })
    .then(async () => {
      await proxy.downFile('/fixture/fixtureStorage/export', { ...queryParams })
    })
}
//********************************其他方法************************* */

//治具查询
function handleQueryFixture(keyword, row) {
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
      query.series = kv[0]
      query.fixtureName = kv[1]
      query.keyword = null
    }

    setTimeout(() => {
      dictFixtureBase(query).then((res) => {
        if (row) row.fixture_options = res.data.result
        else state.options.fixture_options = res.data.result
      })
    }, 200)
  }
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
    if (it.fixtureId)
      it.fixture_options = [
        {
          dictValue: it.fixtureId,
          dictLabel: it.series + ' / ' + it.fixtureName
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
    importFixtureStorage(data).then((res) => {
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
    importFixtureStorageOperate(data).then((res) => {
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
  queryParams.fixtureId = props.fixtureId
  handleQuery()
})

handleQuery()
</script>
<style>
.el-table .danger-row {
  --el-table-tr-bg-color: var(--el-color-danger-light-7);
}
</style>
