<!--
 * @Descripttion: (产线领用中的治具/Fixture_Storage_Using)
 * @Author: (admin)
 * @Date: (2024-07-02)
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
      <el-form-item label="产线" prop="lineId">
        <el-select v-model="queryParams.lineId" placeholder="请选择产线" filterable clear>
          <el-option
            v-for="item in useBasicStore().getLineDict"
            :key="item.dictValue"
            :label="item.dictLabel"
            :value="Number(item.dictValue)"></el-option>
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
        <el-button
          type="primary"
          title="批量归还"
          v-hasPermi="['fixture:storage:back']"
          @click="handleBatchBackFixtureStorage"
          v-if="opertype != 2">
          批量归还
        </el-button>
        <el-button type="danger" title="取消" @click="cancelBatchBackFixtureStorage" v-if="opertype == 2"> 取消 </el-button>
        <el-button type="success" title="确认" @click="confirmBatchBackFixtureStorage" v-if="opertype == 2"> 确认 </el-button>
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
      @selection-change="handleFixtureSelectionChange"
      @sort-change="sortChange">
      <el-table-column type="selection" width="40" align="center" v-if="opertype == 2" />
      <el-table-column prop="fixtureUsingId" label="主键" align="center" v-if="columns.showColumn('fixtureUsingId')" />
      <el-table-column prop="relatedUser" label="领用人ID" width="90" align="center" v-if="columns.showColumn('relatedUser')" />
      <el-table-column prop="relatedUserName" label="领用人" width="90" align="center" v-if="columns.showColumn('relatedUserName')" />
      <el-table-column prop="lineId" label="产线Id" width="90" align="center" v-if="columns.showColumn('lineId')" />
      <el-table-column prop="lineName" label="产线" width="90" align="center" v-if="columns.showColumn('lineName')" />
      <el-table-column prop="fixtureId" label="治具ID" align="center" v-if="columns.showColumn('fixtureId')" />
      <el-table-column label="治具(系列/名称)" min-width="250" v-if="columns.showColumn('fixture')" :formatter="formatter" />
      <el-table-column prop="storageId" label="储位ID" align="center" v-if="columns.showColumn('storageId')" />
      <el-table-column
        prop="storageFullName"
        label="原储位"
        min-width="250"
        :show-overflow-tooltip="true"
        v-if="columns.showColumn('storageFullName') && opertype != 2" />
      <el-table-column prop="storageId" label="储位" min-width="300" align="center" v-if="opertype == 2">
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
      <el-table-column prop="receiveQty" label="领用数量" width="90" align="center" v-if="columns.showColumn('receiveQty')" />
      <el-table-column prop="qty" label="未还数量" width="90" align="center" v-if="columns.showColumn('qty')" />
      <el-table-column prop="changeQty" label="归还数量" width="150" align="center" v-if="opertype == 2">
        <template #default="scope">
          <el-input-number
            v-model.number="scope.row.changeQty"
            :controls="true"
            controls-position="right"
            placeholder="请输入变动数量"
            class="fullWidth" />
        </template>
      </el-table-column>
      <el-table-column prop="ticketNo" label="业务编号" width="160" align="center" v-if="columns.showColumn('ticketNo')" />
      <el-table-column
        prop="ticketType"
        label="业务类型"
        width="160"
        align="center"
        :show-overflow-tooltip="true"
        sortable
        v-if="columns.showColumn('ticketType')">
        <template #default="scope">
          <dict-tag :options="options.ticket_type" :value="scope.row.ticketType" />
        </template>
      </el-table-column>
      <el-table-column prop="createBy" label="创建人" width="90" align="center" v-if="columns.showColumn('createBy')" />
      <el-table-column prop="createTime" label="创建时间" width="160" align="center" v-if="columns.showColumn('createTime')" />
      <el-table-column label="操作" width="90" align="center" fixed="right" v-if="opertype != 2">
        <template #default="scope">
          <el-button type="primary" size="small" title="归还" v-hasPermi="['fixture:storage:back']" @click="handleBackFixtureStorage(scope.row)">
            归还
          </el-button>
        </template>
      </el-table-column>
    </el-table>
    <pagination :total="total" v-model:page="queryParams.pageNum" v-model:limit="queryParams.pageSize" @pagination="getList" />

    <!-- 添加或修改产线领用中的治具对话框 -->
    <el-dialog :title="title" :lock-scroll="false" v-model="open">
      <el-form ref="formRef" :model="form" :rules="rules" label-width="100px">
        <el-row :gutter="20">
          <el-col :lg="12">
            <el-form-item label="未还数量" prop="qty">
              <el-input v-model="form.qty" disabled />
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
        </el-row>
      </el-form>
      <template #footer v-if="opertype != 3">
        <el-button text @click="cancel">{{ $t('btn.cancel') }}</el-button>
        <el-button type="primary" @click="submitForm">{{ $t('btn.submit') }}</el-button>
      </template>
    </el-dialog>
  </div>
</template>

<script setup name="fixturestorageusing">
import { listFixtureStorageUsing, getFixtureStorageUsing, backFixtureStorage, batchBackFixtureStorage } from '@/api/fixture/fixtureStorage.js'
import useBasicStore from '@/store/modules/basic.js'
import { dictFixtureBase } from '@/api/fixture/fixtureBase.js'

const props = defineProps({
  fixtureId: Number
})
const { proxy } = getCurrentInstance()
const ids = ref([])
const loading = ref(false)
const showSearch = ref(true)
if (props.fixtureId) showSearch.value = false
const queryParams = reactive({
  pageNum: 1,
  pageSize: 10,
  sort: '',
  sortType: 'asc',
  fixtureId: props.fixtureId
})
const columns = ref([
  { visible: false, prop: 'fixtureUsingId', label: '主键' },
  { visible: false, prop: 'relatedUser', label: '领用人ID' },
  { visible: true, prop: 'relatedUserName', label: '领用人' },
  { visible: false, prop: 'lineId', label: '产线Id' },
  { visible: true, prop: 'lineName', label: '产线' },
  { visible: false, prop: 'fixtureId', label: '治具ID' },
  { visible: true, prop: 'fixture', label: '治具(系列/名称)' },
  { visible: false, prop: 'storageId', label: '储位ID' },
  { visible: true, prop: 'storageFullName', label: '储位' },
  { visible: true, prop: 'receiveQty', label: '领用数量' },
  { visible: true, prop: 'qty', label: '占用数量' },
  { visible: false, prop: 'ticketNo', label: '业务编号' },
  { visible: false, prop: 'ticketType', label: '单据类型' },
  { visible: false, prop: 'createBy', label: '创建人' },
  { visible: false, prop: 'createTime', label: '创建时间' }
])
const total = ref(0)
const dataList = ref([])
const checkedDataList = ref([])
const queryRef = ref()
const defaultTime = ref([new Date(2000, 1, 1, 0, 0, 0), new Date(2000, 2, 1, 23, 59, 59)])

var dictParams = [{ dictType: 'ticket_type' }]
//获取字典数据
proxy.getDicts(dictParams).then((response) => {
  response.data.forEach((element) => {
    state.options[element.dictType] = element.list
  })
})

function getList() {
  loading.value = true
  listFixtureStorageUsing(queryParams).then((res) => {
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

//处理批量归还
function handleBatchBackFixtureStorage() {
  if (opertype.value != 2) {
    //显示归还相关操作
    opertype.value = 2
    dataList.value.forEach((item) => {
      item.changeQty = item.qty
    })
  }
}

//取消批量归还
function cancelBatchBackFixtureStorage() {
  opertype.value = 0
}

//确认批量归还
function confirmBatchBackFixtureStorage() {
  if (!checkedDataList.value || checkedDataList.value.length <= 0) {
    proxy.$modal.msgError('请勾选最少一条数据')
  } else {
    const data = [...checkedDataList.value]
    batchBackFixtureStorage(data).then((res) => {
      proxy.$modal.msgSuccess('归还成功')
      opertype.value = 0
      getList()
    })
  }
}

/** 复选框选中数据 */
function handleFixtureSelectionChange(selection) {
  checkedDataList.value = selection
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

function formatter(row, cloumn) {
  return row.series + ' / ' + row.fixtureName
}
/*************** form操作 ***************/
const formRef = ref()
const title = ref('')
// 操作类型 1、单个归还 2、批量归还
const opertype = ref(0)
const open = ref(false)

const state = reactive({
  single: true,
  multiple: true,
  form: {},
  rules: {
    fixtureUsingId: [{ required: true, message: 'ID不能为空', trigger: 'blur' }],
    fixtureId: [{ required: true, message: '治具ID不能为空', trigger: 'blur', type: 'number' }],
    changeQty: [{ required: true, message: '数量不能为空', trigger: 'blur', type: 'number' }],
    storageId: [{ required: true, message: '储位ID不能为空', trigger: 'blur', type: 'number' }]
  },
  options: {
    //单据类型
    ticket_type: [],
    //治具选项
    fixture_options: []
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
    fixtureUsingId: null,
    ticketNo: null,
    fixtureId: null,
    qty: null,
    storageId: null,
    strorageDesc: null,
    createBy: null,
    createTime: null,
    lineId: null
  }
  proxy.resetForm('formRef')
}

// 归还
function handleBackFixtureStorage(row) {
  reset()
  const id = row.fixtureUsingId
  getFixtureStorageUsing(id).then((res) => {
    const { code, data } = res
    if (code == 200) {
      open.value = true
      title.value = '归还'
      opertype.value = 1

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
      if (form.value.fixtureUsingId != undefined && opertype.value === 1) {
        backFixtureStorage(form.value).then((res) => {
          proxy.$modal.msgSuccess('归还成功')
          open.value = false
          getList()
        })
      }
    }
  })
}

//治具查询
function handleQueryFixture(keyword) {
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
        state.options.fixture_options = res.data.result
      })
    }, 200)
  }
}

//监听属性变化
watch(props, (val) => {
  queryParams.fixtureId = props.fixtureId
  handleQuery()
})

handleQuery()
</script>
