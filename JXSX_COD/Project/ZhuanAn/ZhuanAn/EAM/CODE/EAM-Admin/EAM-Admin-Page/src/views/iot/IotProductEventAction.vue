<!--
 * @Descripttion: (产品事件处理动作/IOT_Product_Event_Action)
 * @Author: (admin)
 * @Date: (2026-01-08)
-->
<template>
  <div>
    <!-- <el-form :model="queryParams" label-position="right" inline ref="queryRef" v-show="showSearch" @submit.prevent>
      <el-form-item>
        <el-button icon="search" type="primary" @click="handleQuery">{{ $t('btn.search') }}</el-button>
        <el-button icon="refresh" @click="resetQuery">{{ $t('btn.reset') }}</el-button>
      </el-form-item>
    </el-form> -->
    <!-- 工具区域 -->
    <el-row :gutter="15" class="mb10">
      <el-col :span="1.5">
        <el-button type="primary" v-hasPermi="['iot:product:event:action:add']" plain icon="plus" @click="handleAdd">
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
      <el-table-column prop="actionId" label="动作Id" align="center" v-if="columns.showColumn('actionId')" />
      <el-table-column prop="eventId" label="事件ID" align="center" v-if="columns.showColumn('eventId')" />
      <el-table-column
        prop="actionName"
        label="动作名称"
        align="center"
        width="150"
        :show-overflow-tooltip="true"
        v-if="columns.showColumn('actionName')" />
      <el-table-column prop="actionType" label="动作类型" align="center" width="150" v-if="columns.showColumn('actionType')">
        <template #default="scope">
          <dict-tag :options="options.iot_event_action_type" :value="scope.row.actionType" />
        </template>
      </el-table-column>
      <el-table-column
        prop="actionConfig"
        label="动作配置"
        align="center"
        min-width="150"
        :show-overflow-tooltip="true"
        v-if="columns.showColumn('actionConfig')" />
      <el-table-column prop="sortOrder" label="动作顺序" align="center" width="90" v-if="columns.showColumn('sortOrder')" />
      <el-table-column prop="timeout" label="执行超时" align="center" width="90" v-if="columns.showColumn('timeout')" />
      <el-table-column prop="enabled" label="是否可用" align="center" width="90" v-if="columns.showColumn('enabled')">
        <template #default="scope">
          <el-switch v-model="scope.row.enabled" active-text="是" inactive-text="否" inline-prompt disabled />
        </template>
      </el-table-column>
      <el-table-column prop="createBy" label="创建人" align="center" width="90" v-if="columns.showColumn('createBy')" />
      <el-table-column prop="createTime" label="创建时间" align="center" width="160" v-if="columns.showColumn('createTime')" />
      <el-table-column prop="updateBy" label="更新人" align="center" width="90" v-if="columns.showColumn('updateBy')" />
      <el-table-column prop="updateTime" label="更新时间" align="center" width="160" v-if="columns.showColumn('updateTime')" />
      <el-table-column label="操作" width="120">
        <template #default="scope">
          <el-button
            type="success"
            size="small"
            icon="edit"
            title="编辑"
            v-hasPermi="['iot:product:event:action:edit']"
            @click="handleUpdate(scope.row)"></el-button>
          <el-button
            type="danger"
            size="small"
            icon="delete"
            title="删除"
            v-hasPermi="['iot:product:event:action:delete']"
            @click="handleDelete(scope.row)"></el-button>
        </template>
      </el-table-column>
    </el-table>
    <pagination :total="total" v-model:page="queryParams.pageNum" v-model:limit="queryParams.pageSize" @pagination="getList" />

    <!-- 添加或修改产品事件处理动作对话框 -->
    <el-dialog :title="title" :lock-scroll="false" v-model="open">
      <el-form ref="formRef" :model="form" :rules="rules" label-width="100px">
        <el-row :gutter="20">
          <!-- <el-col :lg="12" v-if="opertype != 1">
            <el-form-item label="动作Id" prop="actionId">
              <el-input-number v-model.number="form.actionId" controls-position="right" placeholder="请输入动作Id" :disabled="true" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="事件ID" prop="eventId">
              <el-input v-model.number="form.eventId" placeholder="请输入事件ID" />
            </el-form-item>
          </el-col> -->

          <el-col :lg="12">
            <el-form-item label="动作名称" prop="actionName">
              <el-input v-model="form.actionName" placeholder="请输入动作名称" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="动作类型" prop="actionType">
              <el-select v-model="form.actionType" placeholder="请选择动作类型" @change="handleActionTypeChange">
                <el-option
                  v-for="item in options.iot_event_action_type"
                  :key="item.dictValue"
                  :label="item.dictLabel"
                  :value="item.dictValue"></el-option>
              </el-select>
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="动作顺序" prop="sortOrder">
              <el-input-number v-model.number="form.sortOrder" :controls="true" controls-position="right" placeholder="请输入动作顺序" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="执行超时" prop="timeout">
              <el-input-number v-model.number="form.timeout" :controls="true" controls-position="right" placeholder="请输入执行超时" />(秒)
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="是否可用" prop="enabled">
              <el-switch v-model="form.enabled" active-text="是" inactive-text="否" inline-prompt />
            </el-form-item>
          </el-col>

          <el-divider content-position="center">动作配置</el-divider>
          <!-- 添加呼叫盒操作 -->
          <template v-if="form.actionType == 'add_call_box_operate'">
            <el-col :lg="12">
              <el-form-item label="操作类型参数标识" required>
                <el-input v-model="actionConfigJson.OperateTypeIdentifier" placeholder="请输入事件的参数标识" />
              </el-form-item>
            </el-col>
            <el-col :lg="12">
              <el-form-item label="操作用户参数标识">
                <el-input v-model="actionConfigJson.UsernameIdentifier" placeholder="请输入事件的参数标识" />
              </el-form-item>
            </el-col>
            <el-divider content-position="center">事件操作类型值映射</el-divider>

            <el-row :gutter="10" class="mb8">
              <el-col :span="1.5">
                <el-button type="primary" icon="Plus" @click="handleAddActionConfigItem('OperateTypeMapping')">添加</el-button>
              </el-col>
              <el-col :span="1.5"> </el-col>
            </el-row>
            <el-table :data="actionConfigJson.OperateTypeMapping" ref="EventParamRef">
              <!-- <el-table-column type="selection" width="40" align="center" /> -->
              <!-- <el-table-column label="序号" align="center" prop="index" width="0" /> -->
              <el-table-column label="原操作类型值" align="center">
                <template #default="scope">
                  <el-input v-model="scope.row.Key" placeholder="请输入属性key" />
                </template>
              </el-table-column>
              <el-table-column label="呼叫盒操作枚举值" align="center">
                <template #default="scope">
                  <el-input v-model="scope.row.Value" placeholder="请输入value" />
                </template>
              </el-table-column>
              <el-table-column label="操作" align="center">
                <template #default="scope">
                  <el-button type="danger" icon="Delete" @click="handleDeleteActionConfigItem('OperateTypeMapping', scope.$index)"></el-button>
                </template>
              </el-table-column>
            </el-table>
          </template>

          <!-- 同步产线 -->
          <template v-if="form.actionType == 'sync_line'">
            <el-col :lg="12">
              <el-form-item label="参数标识" required>
                <el-input v-model="actionConfigJson.ParamIdentifier" placeholder="请输入事件的参数标识" />
              </el-form-item>
            </el-col>
            <el-col :lg="12">
              <el-form-item label="参数类型" required>
                <el-input v-model="actionConfigJson.ParamValueType" placeholder="枚举值：lineId,lineCode,lineName" />
              </el-form-item>
            </el-col>
          </template>

          <!-- 发送微信通知配置 -->
          <template v-if="form.actionType == 'send_wx_msg'">
            <el-col :lg="12">
              <el-form-item label="微信群">
                <el-input v-model="actionConfigJson.WxChatId" placeholder="请输入微信群ID" />
              </el-form-item>
            </el-col>
            <el-col :lg="24">
              <el-form-item label="员工工号">
                <el-input type="textarea" v-model="actionConfigJson.EmpCodes" placeholder="请输入通知用户工号，多个用逗号隔开" />
              </el-form-item>
            </el-col>
            <el-col :lg="24">
              <el-form-item label="消息模板" required>
                <el-input
                  type="textarea"
                  v-model="actionConfigJson.WxMsgTemplate"
                  placeholder="请输入消息模板，可以使用${事件参数标识}来引用事件触发上报的参数值" />
              </el-form-item>
            </el-col>
          </template>

          <!-- 检查保养状态 -->
          <template v-if="form.actionType == 'check_maintain_status'">
            <el-col :lg="12">
              <el-form-item label="日分隔" required>
                <el-input-number v-model="actionConfigJson.DaySeparation" placeholder="请输入日分隔,如:8" />(小时)
              </el-form-item>
            </el-col>
            <el-col :lg="12">
              <el-form-item label="日期标记" required>
                <el-input v-model="actionConfigJson.DateMarks" placeholder="合法值：D,W,M,Q,Y" />
              </el-form-item>
            </el-col>
          </template>

          <!-- 响应数据 -->
          <template v-if="form.actionType == 'response_data'">
            <el-row :gutter="10" class="mb8">
              <el-col :span="1.5">
                <el-button type="primary" icon="Plus" @click="handleAddActionConfigItem(null)">添加</el-button>
              </el-col>
              <el-col :span="1.5"> </el-col>
            </el-row>
            <el-table :data="actionConfigJson" ref="EventParamRef">
              <!-- <el-table-column type="selection" width="40" align="center" /> -->
              <!-- <el-table-column label="序号" align="center" prop="index" width="0" /> -->
              <el-table-column label="属性key" align="center">
                <template #default="scope">
                  <el-input v-model="scope.row.Key" placeholder="请输入属性key" />
                </template>
              </el-table-column>
              <el-table-column label="值来源" align="center">
                <template #default="scope">
                  <el-input v-model="scope.row.ValueFromType" placeholder="请输入值来源" />
                </template>
              </el-table-column>
              <el-table-column label="固定值" align="center">
                <template #default="scope">
                  <el-input v-model="scope.row.FixedValue" />
                </template>
              </el-table-column>
              <el-table-column label="值路径" align="center">
                <template #default="scope">
                  <el-input v-model="scope.row.FromTypePath" placeholder="请输入值路径" />
                </template>
              </el-table-column>
              <el-table-column label="操作" align="center">
                <template #default="scope">
                  <el-button type="danger" icon="Delete" @click="handleDeleteActionConfigItem(null, scope.$index)"></el-button>
                </template>
              </el-table-column>
            </el-table>
          </template>
        </el-row>
      </el-form>
      <template #footer v-if="opertype != 3">
        <el-button text @click="cancel">{{ $t('btn.cancel') }}</el-button>
        <el-button type="primary" @click="submitForm">{{ $t('btn.submit') }}</el-button>
      </template>
    </el-dialog>
  </div>
</template>

<script setup name="iotproducteventaction">
import {
  listIotProductEventAction,
  addIotProductEventAction,
  delIotProductEventAction,
  updateIotProductEventAction,
  getIotProductEventAction
} from '@/api/iot/iotProductEventAction.js'
const props = defineProps({
  eventId: Number
})
const { proxy } = getCurrentInstance()
const ids = ref([])
const loading = ref(false)
const showSearch = ref(true)
const queryParams = reactive({
  pageNum: 1,
  pageSize: 10,
  sort: 'SortOrder',
  sortType: 'asc'
})
const columns = ref([
  { visible: false, prop: 'actionId', label: '动作Id' },
  { visible: false, prop: 'eventId', label: '事件ID' },
  { visible: true, prop: 'actionName', label: '动作名称' },
  { visible: true, prop: 'actionType', label: '动作类型' },
  { visible: true, prop: 'actionConfig', label: '动作配置' },
  { visible: true, prop: 'sortOrder', label: '动作顺序' },
  { visible: true, prop: 'timeout', label: '执行超时(秒)' },
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

var dictParams = [{ dictType: 'iot_event_action_type' }]

proxy.getDicts(dictParams).then((response) => {
  response.data.forEach((element) => {
    state.options[element.dictType] = element.list
  })
})

function getList() {
  loading.value = true
  queryParams.eventId = props.eventId
  listIotProductEventAction(queryParams).then((res) => {
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
const actionConfigJson = ref({})
const state = reactive({
  single: true,
  multiple: true,
  form: {},
  rules: {
    eventId: [{ required: true, message: '事件ID不能为空', trigger: 'blur', type: 'number' }],
    actionName: [{ required: true, message: '动作名称不能为空', trigger: 'blur' }],
    actionType: [{ required: true, message: '动作类型不能为空', trigger: 'change' }],
    actionConfig: [{ required: true, message: '动作配置不能为空', trigger: 'blur' }],
    sortOrder: [{ required: true, message: '动作顺序不能为空', trigger: 'blur', type: 'number' }],
    timeout: [{ required: true, message: '执行超时(秒)不能为空', trigger: 'blur', type: 'number' }],
    enabled: [{ required: true, message: '是否可用不能为空', trigger: 'blur' }]
  },
  options: {
    // 动作类型 选项列表 格式 eg:{ dictLabel: '标签', dictValue: '0'}
    iot_event_action_type: []
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
    actionId: null,
    eventId: null,
    actionName: null,
    actionType: null,
    actionConfig: null,
    sortOrder: null,
    timeout: null,
    enabled: null,
    createBy: null,
    createTime: null,
    updateBy: null,
    updateTime: null
  }
  actionConfigJson.value = {}
  proxy.resetForm('formRef')
}

// 添加按钮操作
function handleAdd() {
  reset()
  open.value = true
  title.value = '添加产品事件处理动作'
  opertype.value = 1
}
// 修改按钮操作
function handleUpdate(row) {
  reset()
  const id = row.actionId || ids.value
  getIotProductEventAction(id).then((res) => {
    const { code, data } = res
    if (code == 200) {
      open.value = true
      title.value = '修改产品事件处理动作'
      opertype.value = 2

      form.value = {
        ...data
      }

      if (form.value.actionConfig) {
        //转为json
        actionConfigJson.value = JSON.parse(form.value.actionConfig)
      } else {
        actionConfigJson.value = {}
      }
    }
  })
}

// 添加&修改 表单提交
function submitForm() {
  proxy.$refs['formRef'].validate((valid) => {
    if (valid) {
      if (actionConfigJson.value) form.value.actionConfig = JSON.stringify(actionConfigJson.value)
      if (form.value.actionId != undefined && opertype.value === 2) {
        updateIotProductEventAction(form.value).then((res) => {
          proxy.$modal.msgSuccess('修改成功')
          open.value = false
          getList()
        })
      } else {
        form.value.eventId = props.eventId
        addIotProductEventAction(form.value).then((res) => {
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
  const Ids = row.actionId || ids.value

  proxy
    .$confirm('是否确认删除参数编号为"' + Ids + '"的数据项？', '警告', {
      confirmButtonText: proxy.$t('common.ok'),
      cancelButtonText: proxy.$t('common.cancel'),
      type: 'warning'
    })
    .then(function () {
      return delIotProductEventAction(Ids)
    })
    .then(() => {
      getList()
      proxy.$modal.msgSuccess('删除成功')
    })
}

/*********************事件参数表信息*************************/

/** 事件参数配置 表格添加行操作 */
function handleAddActionConfigItem(key) {
  let obj = {}
  if (key) {
    if (!actionConfigJson.value[key]) actionConfigJson.value[key] = []
    actionConfigJson.value[key].push(obj)
  } else {
    if (actionConfigJson.value === null) actionConfigJson.value = []
    actionConfigJson.value.push(obj)
  }
}

/** 事件参数配置 表格删除按钮操作 */
function handleDeleteActionConfigItem(key, index) {
  if (key) {
    actionConfigJson.value[key].splice(index, 1)
  } else {
    actionConfigJson.value.splice(index, 1)
  }
}

// 动作类型切换
function handleActionTypeChange(actionType) {
  if (form.value.actionType === 'response_data') {
    actionConfigJson.value = []
  } else {
    actionConfigJson.value = {}
  }
}

//监听属性变化
watch(props, (val) => {
  handleQuery()
})

handleQuery()
</script>
