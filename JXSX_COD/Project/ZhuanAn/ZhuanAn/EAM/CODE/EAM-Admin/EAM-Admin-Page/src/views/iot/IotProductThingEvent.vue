<!--
 * @Descripttion: (产品物模型事件/IOT_Product_Thing_Event)
 * @Author: (admin)
 * @Date: (2026-01-07)
-->
<template>
  <div>
    <el-form :model="queryParams" label-position="right" inline ref="queryRef" v-show="showSearch" @submit.prevent>
      <!-- <el-form-item label="产品ID" prop="productId">
        <el-select clearable v-model="queryParams.productId" placeholder="请选择产品ID">
          <el-option v-for="item in options.productIdOptions" :key="item.dictValue" :label="item.dictLabel" :value="item.dictValue">
            <span class="fl">{{ item.dictLabel }}</span>
            <span class="fr" style="color: var(--el-text-color-secondary)">{{ item.dictValue }}</span>
          </el-option>
        </el-select>
      </el-form-item> -->
      <el-form-item label="事件名称" prop="eventName">
        <el-input v-model="queryParams.eventName" placeholder="请输入事件名称" />
      </el-form-item>
      <el-form-item label="事件标识" prop="identifier">
        <el-input v-model="queryParams.identifier" placeholder="请输入事件标识" />
      </el-form-item>
      <el-form-item>
        <el-button icon="search" type="primary" @click="handleQuery">{{ $t('btn.search') }}</el-button>
        <el-button icon="refresh" @click="resetQuery">{{ $t('btn.reset') }}</el-button>
      </el-form-item>
    </el-form>
    <!-- 工具区域 -->
    <el-row :gutter="15" class="mb10">
      <el-col :span="1.5">
        <el-button type="primary" v-hasPermi="['iot:product:thing:event:add']" plain icon="plus" @click="handleAdd">
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
      <el-table-column prop="eventId" label="事件ID" align="center" width="90" v-if="columns.showColumn('eventId')" />
      <el-table-column prop="productId" label="产品ID" align="center" width="90" v-if="columns.showColumn('productId')" />
      <el-table-column
        prop="eventName"
        label="事件名称"
        align="center"
        min-width="150"
        :show-overflow-tooltip="true"
        v-if="columns.showColumn('eventName')" />
      <el-table-column
        prop="identifier"
        label="事件标识"
        align="center"
        min-width="150"
        :show-overflow-tooltip="true"
        v-if="columns.showColumn('identifier')" />

      <el-table-column prop="eventType" label="事件类型" align="center" width="90" v-if="columns.showColumn('eventType')">
        <template #default="scope">
          <dict-tag :options="options.iot_product_event_type" :value="scope.row.eventType" />
        </template>
      </el-table-column>
      <el-table-column
        prop="outputParams"
        label="输出参数"
        min-width="150"
        :show-overflow-tooltip="true"
        v-if="columns.showColumn('outputParams')" />
      <el-table-column prop="expandDesc" label="扩展描述" min-width="150" :show-overflow-tooltip="true" v-if="columns.showColumn('expandDesc')" />
      <el-table-column prop="description" label="描述" min-width="150" :show-overflow-tooltip="true" v-if="columns.showColumn('description')" />
      <el-table-column prop="enabled" label="是否可用" align="center" width="90" v-if="columns.showColumn('enabled')">
        <template #default="scope">
          <el-switch v-model="scope.row.enabled" active-text="是" inactive-text="否" inline-prompt disabled />
        </template>
      </el-table-column>
      <el-table-column prop="createBy" label="创建人" align="center" width="90" v-if="columns.showColumn('createBy')" />
      <el-table-column prop="createTime" label="创建时间" width="160" v-if="columns.showColumn('createTime')" />
      <el-table-column prop="updateBy" label="更新人" align="center" width="90" v-if="columns.showColumn('updateBy')" />
      <el-table-column prop="updateTime" label="更新时间" width="160" v-if="columns.showColumn('updateTime')" />
      <el-table-column label="操作" width="180">
        <template #default="scope">
          <el-button type="primary" size="small" v-hasPermi="['iot:product:event:action:list']" @click="handleOpenAction(scope.row)"
            >动作</el-button
          >
          <el-button
            type="success"
            size="small"
            icon="edit"
            title="编辑"
            v-hasPermi="['iot:product:thing:event:edit']"
            @click="handleUpdate(scope.row)"></el-button>
          <el-button
            type="danger"
            size="small"
            icon="delete"
            title="删除"
            v-hasPermi="['iot:product:thing:event:delete']"
            @click="handleDelete(scope.row)"></el-button>
        </template>
      </el-table-column>
    </el-table>
    <pagination :total="total" v-model:page="queryParams.pageNum" v-model:limit="queryParams.pageSize" @pagination="getList" />

    <!-- 添加或修改产品物模型事件对话框 -->
    <el-dialog :title="title" :lock-scroll="false" v-model="open">
      <el-form ref="formRef" :model="form" :rules="rules" label-width="100px">
        <el-row :gutter="20">
          <!-- <el-col :lg="12" v-if="opertype != 1">
            <el-form-item label="事件ID" prop="eventId">
              <el-input-number v-model.number="form.eventId" controls-position="right" placeholder="请输入事件ID" :disabled="true" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="产品ID" prop="productId">
              <el-select v-model="form.productId" placeholder="请选择产品ID">
                <el-option
                  v-for="item in options.productIdOptions"
                  :key="item.dictValue"
                  :label="item.dictLabel"
                  :value="parseInt(item.dictValue)"></el-option>
              </el-select>
            </el-form-item>
          </el-col> -->

          <el-col :lg="12">
            <el-form-item label="事件名称" prop="eventName">
              <el-input v-model="form.eventName" placeholder="请输入事件名称" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="事件标识" prop="identifier">
              <el-input v-model="form.identifier" placeholder="请输入事件标识" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="事件类型" prop="eventType">
              <el-select v-model="form.eventType" placeholder="请选择事件类型">
                <el-option
                  v-for="item in options.iot_product_event_type"
                  :key="item.dictValue"
                  :label="item.dictLabel"
                  :value="item.dictValue"></el-option>
              </el-select>
            </el-form-item>
          </el-col>

          <!-- <el-col :lg="24">
            <el-form-item label="扩展描述" prop="expandDesc">
              <el-input type="textarea" v-model="form.expandDesc" placeholder="请输入扩展描述" />
            </el-form-item>
          </el-col> -->

          <el-col :lg="24">
            <el-form-item label="描述" prop="description">
              <el-input type="textarea" v-model="form.description" placeholder="请输入描述" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="是否可用" prop="enabled">
              <el-switch v-model="form.enabled" active-text="是" inactive-text="否" inline-prompt />
            </el-form-item>
          </el-col>
        </el-row>

        <el-divider content-position="center">输出参数</el-divider>
        <el-row :gutter="10" class="mb8">
          <el-col :span="1.5">
            <el-button type="primary" icon="Plus" @click="handleAddEventParam">添加</el-button>
          </el-col>
          <el-col :span="1.5">
            <el-button type="danger" icon="Delete" @click="handleDeleteEventParam">删除</el-button>
          </el-col>
        </el-row>
        <el-table
          :data="eventParamList"
          :row-class-name="rowEventParamIndex"
          @selection-change="handleEventParamSelectionChange"
          ref="EventParamRef">
          <el-table-column type="selection" width="40" align="center" />
          <el-table-column label="序号" align="center" prop="index" width="50" />
          <el-table-column label="参数名称" align="center" prop="paramName">
            <template #default="scope">
              <el-input v-model="scope.row.paramName" placeholder="请输入参数名称" />
            </template>
          </el-table-column>
          <el-table-column label="参数标识" align="center" prop="identifier">
            <template #default="scope">
              <el-input v-model="scope.row.identifier" placeholder="请输入参数标识" />
            </template>
          </el-table-column>
          <el-table-column label="数据类型" align="center" prop="dataType">
            <template #default="scope">
              <el-select v-model="scope.row.dataType" placeholder="请选择数据类型">
                <el-option
                  v-for="item in options.iot_product_data_type"
                  :key="item.dictValue"
                  :label="item.dictLabel"
                  :value="item.dictValue"></el-option>
              </el-select>
            </template>
          </el-table-column>
        </el-table>
      </el-form>
      <template #footer v-if="opertype != 3">
        <el-button text @click="cancel">{{ $t('btn.cancel') }}</el-button>
        <el-button type="primary" @click="submitForm">{{ $t('btn.submit') }}</el-button>
      </template>
    </el-dialog>

    <el-dialog :title="title" :lock-scroll="false" v-model="openEventActionDialog" width="70%">
      <IotProductEventAction :eventId="currEventId" />
    </el-dialog>
  </div>
</template>

<script setup name="iotproductthingevent">
import {
  listIotProductThingEvent,
  addIotProductThingEvent,
  delIotProductThingEvent,
  updateIotProductThingEvent,
  getIotProductThingEvent
} from '@/api/iot/iotProductThingEvent.js'
import IotProductEventAction from './IotProductEventAction.vue'
const props = defineProps({
  productId: Number
})
const { proxy } = getCurrentInstance()
const ids = ref([])
const loading = ref(false)
const showSearch = ref(false)
const queryParams = reactive({
  pageNum: 1,
  pageSize: 10,
  sort: '',
  sortType: 'asc',
  productId: undefined,
  eventName: undefined,
  identifier: undefined
})
const columns = ref([
  { visible: false, prop: 'eventId', label: '事件ID' },
  { visible: false, prop: 'productId', label: '产品ID' },
  { visible: true, prop: 'eventName', label: '事件名称' },
  { visible: true, prop: 'identifier', label: '事件标识' },
  { visible: true, prop: 'eventType', label: '事件类型' },
  { visible: false, prop: 'outputParams', label: '输出参数' },
  { visible: false, prop: 'expandDesc', label: '扩展描述' },
  { visible: true, prop: 'description', label: '描述' },
  { visible: true, prop: 'enabled', label: '是否可用' },
  { visible: false, prop: 'createBy', label: '创建人' },
  { visible: false, prop: 'createTime', label: '创建时间' },
  { visible: false, prop: 'updateBy', label: '更新人' },
  { visible: false, prop: 'updateTime', label: '更新时间' }
])
const total = ref(0)
const dataList = ref([])
const queryRef = ref()
const defaultTime = ref([new Date(2000, 1, 1, 0, 0, 0), new Date(2000, 2, 1, 23, 59, 59)])

var dictParams = [{ dictType: 'iot_product_event_type' }, { dictType: 'iot_product_data_type' }]

proxy.getDicts(dictParams).then((response) => {
  response.data.forEach((element) => {
    state.options[element.dictType] = element.list
  })
})

function getList() {
  loading.value = true
  queryParams.productId = props.productId
  listIotProductThingEvent(queryParams).then((res) => {
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
    productId: [{ required: true, message: '产品ID不能为空', trigger: 'change', type: 'number' }],
    eventName: [{ required: true, message: '事件名称不能为空', trigger: 'blur' }],
    identifier: [{ required: true, message: '事件标识不能为空', trigger: 'blur' }],
    eventType: [{ required: true, message: '事件类型不能为空', trigger: 'blur' }],
    enabled: [{ required: true, message: '是否可用不能为空', trigger: 'blur' }]
  },
  options: {
    // 产品ID 选项列表 格式 eg:{ dictLabel: '标签', dictValue: '0'}
    productIdOptions: [],
    // 事件类型
    iot_product_event_type: [],
    // 数据类型 选项列表 格式 eg:{ dictLabel: '标签', dictValue: '0'}
    iot_product_data_type: []
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
    eventId: null,
    productId: null,
    eventName: null,
    identifier: null,
    outputParams: null,
    expandDesc: null,
    description: null,
    enabled: null,
    createBy: null,
    createTime: null,
    updateBy: null,
    updateTime: null
  }
  eventParamList.value = []
  proxy.resetForm('formRef')
}

// 添加按钮操作
function handleAdd() {
  reset()
  open.value = true
  title.value = '添加产品物模型事件'
  opertype.value = 1
}
// 修改按钮操作
function handleUpdate(row) {
  reset()
  const id = row.eventId || ids.value
  getIotProductThingEvent(id).then((res) => {
    const { code, data } = res
    if (code == 200) {
      open.value = true
      title.value = '修改产品物模型事件'
      opertype.value = 2

      form.value = {
        ...data
      }
      eventParamList.value = res.data.outputParams
    }
  })
}

// 添加&修改 表单提交
function submitForm() {
  proxy.$refs['formRef'].validate((valid) => {
    if (valid) {
      form.value.outputParams = eventParamList.value
      if (form.value.eventId != undefined && opertype.value === 2) {
        updateIotProductThingEvent(form.value).then((res) => {
          proxy.$modal.msgSuccess('修改成功')
          open.value = false
          getList()
        })
      } else {
        form.value.productId = props.productId
        addIotProductThingEvent(form.value).then((res) => {
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
  const Ids = row.eventId || ids.value

  proxy
    .$confirm('是否确认删除参数编号为"' + Ids + '"的数据项？', '警告', {
      confirmButtonText: proxy.$t('common.ok'),
      cancelButtonText: proxy.$t('common.cancel'),
      type: 'warning'
    })
    .then(function () {
      return delIotProductThingEvent(Ids)
    })
    .then(() => {
      getList()
      proxy.$modal.msgSuccess('删除成功')
    })
}

/*********************事件参数表信息*************************/
const eventParamList = ref([])
const checkedEventParam = ref([])
const drawer = ref(false)

/** 事件参数配置序号 */
function rowEventParamIndex({ row, rowIndex }) {
  row.index = rowIndex + 1
}

/** 事件参数配置添加按钮操作 */
function handleAddEventParam() {
  let obj = {}
  //下面的代码自己设置默认值
  eventParamList.value.push(obj)
}

/** 复选框选中数据 */
function handleEventParamSelectionChange(selection) {
  checkedEventParam.value = selection.map((item) => item.index)
}

/** 事件参数配置删除按钮操作 */
function handleDeleteEventParam() {
  if (checkedEventParam.value.length == 0) {
    proxy.$modal.msgError('请先选择要删除的事件参数配置数据')
  } else {
    const eventParams = eventParamList.value
    const checkedEventParams = checkedEventParam.value
    eventParamList.value = eventParams.filter(function (item) {
      return checkedEventParams.indexOf(item.index) == -1
    })
  }
}

/** 事件参数配置详情 */
function rowClick(row) {
  const id = row.faultConfigId || ids.value
  getCallConfigFault(id).then((res) => {
    const { code, data } = res
    if (code == 200) {
      drawer.value = true
      eventParamList.value = data.callConfigFaultSolutionNav
    }
  })
}

//-------------------事件动作-------------------
const currEventId = ref()
const openEventActionDialog = ref(false)
function handleOpenAction(row) {
  title.value = '事件动作配置'
  currEventId.value = row.eventId
  openEventActionDialog.value = true
}

watch(
  () => props.productId,
  (newValue, oldValue) => {
    handleQuery()
  }
)

handleQuery()
</script>
