<!--
 * @Descripttion: (设备日志/IOT_Device_Log)
 * @Author: (admin)
 * @Date: (2026-01-07)
-->
<template>
  <div>
    <el-form :model="queryParams" label-position="right" inline ref="queryRef" v-show="showSearch" @submit.prevent>
      <el-form-item label="设备" prop="deviceId">
        <el-select
          v-model="queryParams.deviceId"
          clearable
          filterable
          remote
          :remote-method="handleQueryIotDevice"
          placeholder="设备名称、设备key、注册包">
          <el-option v-for="item in options.device_options" :key="item.dictValue" :label="item.dictLabel" :value="item.dictValue"> </el-option>
        </el-select>
      </el-form-item>
      <el-form-item label="追踪ID" prop="traceId">
        <el-input v-model="queryParams.traceId" placeholder="请输入追踪ID" />
      </el-form-item>
      <el-form-item label="创建时间">
        <el-date-picker
          v-model="dateRangeCreateTime"
          type="datetimerange"
          start-placeholder="开始日期"
          end-placeholder="结束日期"
          value-format="YYYY-MM-DD HH:mm:ss"
          :default-time="defaultTime"
          :shortcuts="dateOptions">
        </el-date-picker>
      </el-form-item>
      <el-form-item>
        <el-button icon="search" type="primary" @click="handleQuery">{{ $t('btn.search') }}</el-button>
        <el-button icon="refresh" @click="resetQuery">{{ $t('btn.reset') }}</el-button>
      </el-form-item>
    </el-form>
    <!-- 工具区域 -->
    <el-row :gutter="15" class="mb10">
      <!--<el-col :span="1.5">
        <el-button type="primary" v-hasPermi="['iot:device:log:add']" plain icon="plus" @click="handleAdd">
          {{ $t('btn.add') }}
        </el-button>
      </el-col>-->
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
      <el-table-column prop="logId" label="日志ID" align="center" width="90" v-if="columns.showColumn('logId')" />
      <el-table-column prop="deviceId" label="设备ID" align="center" width="90" v-if="columns.showColumn('deviceId')" />
      <el-table-column prop="deviceName" label="设备名称" align="center" width="160" v-if="columns.showColumn('deviceName')" />
      <el-table-column prop="deviceKey" label="设备Key" align="center" width="160" v-if="columns.showColumn('deviceKey')" />
      <el-table-column
        prop="traceId"
        label="追踪ID"
        align="center"
        width="160"
        :show-overflow-tooltip="true"
        v-if="columns.showColumn('traceId')" />
      <el-table-column prop="businessType" label="业务类型" align="center" width="150" v-if="columns.showColumn('businessType')">
        <template #default="scope">
          <dict-tag :options="options.iot_log_business_type" :value="scope.row.businessType" />
        </template>
      </el-table-column>
      <el-table-column prop="operation" label="操作" align="center" width="150" v-if="columns.showColumn('operation')">
        <template #default="scope">
          <dict-tag :options="options.iot_log_operation" :value="scope.row.operation" />
        </template>
      </el-table-column>
      <el-table-column
        prop="content"
        label="内容"
        align="center"
        min-width="200"
        :show-overflow-tooltip="true"
        v-if="columns.showColumn('content')" />
      <el-table-column prop="status" label="日志状态" align="center" width="90" v-if="columns.showColumn('status')">
        <template #default="scope">
          <dict-tag :options="options.iot_log_status" :value="scope.row.status" />
        </template>
      </el-table-column>
      <el-table-column prop="createTime" label="创建时间" width="160" v-if="columns.showColumn('createTime')" />
      <el-table-column label="操作" fixed="right" width="70" align="center">
        <template #default="scope">
          <el-button type="primary" size="small" icon="view" title="详情" @click="handlePreview(scope.row)"></el-button>
        </template>
      </el-table-column>
    </el-table>
    <pagination :total="total" v-model:page="queryParams.pageNum" v-model:limit="queryParams.pageSize" @pagination="getList" />

    <!-- 添加或修改设备日志对话框 -->
    <el-dialog :title="title" :lock-scroll="false" v-model="open">
      <el-form ref="formRef" :model="form" :rules="rules" label-width="100px">
        <el-row :gutter="20">
          <el-col :lg="12">
            <el-form-item label="设备ID" prop="deviceId">
              <el-select v-model="form.deviceId" placeholder="请选择设备ID">
                <el-option
                  v-for="item in options.device_options"
                  :key="item.dictValue"
                  :label="item.dictLabel"
                  :value="parseInt(item.dictValue)"></el-option>
              </el-select>
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="追踪ID" prop="traceId">
              <el-input v-model="form.traceId" placeholder="请输入追踪ID" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="业务类型" prop="businessType">
              <el-select v-model="form.businessType" placeholder="请选择业务类型">
                <el-option
                  v-for="item in options.iot_log_business_type"
                  :key="item.dictValue"
                  :label="item.dictLabel"
                  :value="item.dictValue"></el-option>
              </el-select>
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="操作" prop="operation">
              <el-select v-model="form.operation" placeholder="请选择操作">
                <el-option
                  v-for="item in options.iot_log_operation"
                  :key="item.dictValue"
                  :label="item.dictLabel"
                  :value="item.dictValue"></el-option>
              </el-select>
            </el-form-item>
          </el-col>

          <el-col :lg="24">
            <el-form-item label="内容" prop="content">
              <el-input type="textarea" v-model="form.content" placeholder="请输入内容" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="日志状态" prop="status">
              <el-radio-group v-model="form.status">
                <el-radio v-for="item in options.iot_log_status" :key="item.dictValue" :label="item.dictValue">
                  {{ item.dictLabel }}
                </el-radio>
              </el-radio-group>
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="创建时间" prop="createTime">
              <el-date-picker v-model="form.createTime" type="datetime" :teleported="false" placeholder="选择日期时间"></el-date-picker>
            </el-form-item>
          </el-col>
        </el-row>
      </el-form>
      <!-- <template #footer v-if="opertype != 3">
        <el-button text @click="cancel">{{ $t('btn.cancel') }}</el-button>
        <el-button type="primary" @click="submitForm">{{ $t('btn.submit') }}</el-button>
      </template> -->
    </el-dialog>
  </div>
</template>

<script setup name="iotdevicelog">
import { listIotDeviceLog, addIotDeviceLog, getIotDeviceLog } from '@/api/iot/iotDeviceLog.js'
import { dictIotDevice } from '@/api/iot/iotDevice'
const { proxy } = getCurrentInstance()
const ids = ref([])
const loading = ref(false)
const showSearch = ref(true)
const queryParams = reactive({
  pageNum: 1,
  pageSize: 10,
  sort: 'CreateTime',
  sortType: 'desc',
  deviceId: undefined,
  traceId: undefined,
  createTime: undefined
})
const columns = ref([
  { visible: false, prop: 'logId', label: '日志ID' },
  { visible: false, prop: 'deviceId', label: '设备ID' },
  { visible: true, prop: 'deviceName', label: '设备名称' },
  { visible: true, prop: 'deviceKey', label: '设备Key' },
  { visible: true, prop: 'traceId', label: '追踪ID' },
  { visible: true, prop: 'businessType', label: '业务类型' },
  { visible: true, prop: 'operation', label: '操作' },
  { visible: true, prop: 'content', label: '内容' },
  { visible: true, prop: 'status', label: '日志状态' },
  { visible: true, prop: 'createTime', label: '创建时间' }
])
const total = ref(0)
const dataList = ref([])
const queryRef = ref()
const defaultTime = ref([new Date(2000, 1, 1, 0, 0, 0), new Date(2000, 2, 1, 23, 59, 59)])

// 创建时间时间范围
const dateRangeCreateTime = ref([])

var dictParams = [{ dictType: 'iot_log_status' }, { dictType: 'iot_log_operation' }, { dictType: 'iot_log_business_type' }]

proxy.getDicts(dictParams).then((response) => {
  response.data.forEach((element) => {
    state.options[element.dictType] = element.list
  })
})

function getList() {
  proxy.addDateRange(queryParams, dateRangeCreateTime.value, 'CreateTime')
  loading.value = true
  listIotDeviceLog(queryParams).then((res) => {
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
  // 创建时间时间范围
  dateRangeCreateTime.value = []
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
    deviceId: [{ required: true, message: '设备ID不能为空', trigger: 'change', type: 'number' }],
    businessType: [{ required: true, message: '业务类型不能为空', trigger: 'change' }],
    content: [{ required: true, message: '内容不能为空', trigger: 'blur' }],
    status: [{ required: true, message: '日志状态不能为空', trigger: 'blur' }],
    createTime: [{ required: true, message: '创建时间不能为空', trigger: 'blur' }]
  },
  options: {
    // 设备ID 选项列表 格式 eg:{ dictLabel: '标签', dictValue: '0'}
    device_options: [],
    // 日志操作
    iot_log_operation: [],
    // 日志业务
    iot_log_business_type: [],
    // 日志状态
    iot_log_status: []
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
    logId: null,
    deviceId: null,
    traceId: null,
    businessType: null,
    operation: null,
    content: null,
    status: null,
    createTime: null
  }
  proxy.resetForm('formRef')
}

// 添加按钮操作
function handleAdd() {
  reset()
  open.value = true
  title.value = '添加设备日志'
  opertype.value = 1
}

// 添加&修改 表单提交
function submitForm() {
  proxy.$refs['formRef'].validate((valid) => {
    if (valid) {
      if (form.value.logId != undefined && opertype.value === 2) {
      } else {
        addIotDeviceLog(form.value).then((res) => {
          proxy.$modal.msgSuccess('新增成功')
          open.value = false
          getList()
        })
      }
    }
  })
}

// 查询iot设备
function handleQueryIotDevice(keyword) {
  if (keyword) {
    const params = {
      pageNum: 1,
      pageSize: 10,
      sort: '',
      sortType: 'asc',
      keyword: keyword
    }
    setTimeout(() => {
      dictIotDevice(params).then((res) => {
        state.options.device_options = res.data.result
      })
    }, 200)
  }
}

/** 打开产品详情窗口 */
function handlePreview(row) {
  form.value = { ...row }
  opertype.value = 3
  open.value = true
}

handleQuery()
</script>
