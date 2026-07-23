<!--
 * @Descripttion: (设备采集数据/IOT_Device_Data)
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
      <el-form-item label="属性标识" prop="identifier">
        <el-input v-model="queryParams.identifier" />
      </el-form-item>
      <el-form-item label="上报时间">
        <el-date-picker
          v-model="dateRangeCollectTime"
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
      <!-- <el-col :span="1.5">
        <el-button type="primary" v-hasPermi="['iot:collect:data:add']" plain icon="plus" @click="handleAdd">
          {{ $t('btn.add') }}
        </el-button>
      </el-col> -->
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
      <el-table-column prop="deviceId" label="设备ID" align="center" width="90" v-if="columns.showColumn('deviceId')" />
      <el-table-column prop="deviceName" label="设备名称" width="150" align="center" v-if="columns.showColumn('deviceName')" />
      <el-table-column prop="deviceKey" label="设备Key" width="150" align="center" v-if="columns.showColumn('deviceKey')" />
      <el-table-column prop="identifier" label="属性标识" width="200" align="center" v-if="columns.showColumn('identifier')" />
      <el-table-column prop="collectTime" label="上报时间" width="160" v-if="columns.showColumn('collectTime')" />
      <el-table-column prop="value" label="可用值" align="center" width="160" v-if="columns.showColumn('value')" />
      <el-table-column prop="rawValue" label="原始值" min-width="160" :show-overflow-tooltip="true" v-if="columns.showColumn('rawValue')" />
      <el-table-column prop="quality" label="数据质量" align="center" width="90" v-if="columns.showColumn('quality')">
        <template #default="scope">
          <dict-tag :options="options.iot_data_quality" :value="scope.row.quality" />
        </template>
      </el-table-column>
      <el-table-column prop="unit" label="单位" align="center" width="90" v-if="columns.showColumn('unit')" />
      <el-table-column label="操作" width="80">
        <template #default="scope">
          <el-button
            type="danger"
            size="small"
            icon="delete"
            title="删除"
            v-hasPermi="['iot:collect:data:delete']"
            @click="handleDelete(scope.row)"></el-button>
        </template>
      </el-table-column>
    </el-table>
    <pagination :total="total" v-model:page="queryParams.pageNum" v-model:limit="queryParams.pageSize" @pagination="getList" />

    <!-- 添加或修改设备采集数据对话框 -->
    <el-dialog :title="title" :lock-scroll="false" v-model="open">
      <el-form ref="formRef" :model="form" :rules="rules" label-width="100px">
        <el-row :gutter="20">
          <el-col :lg="12">
            <el-form-item label="设备ID" prop="deviceId">
              <el-input v-model.number="form.deviceId" placeholder="请输入设备ID" :disabled="opertype != 1" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="属性标识" prop="identifier">
              <el-input v-model="form.identifier" placeholder="请输入属性标识" :disabled="opertype != 1" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="上报时间" prop="collectTime">
              <el-input v-model="form.collectTime" placeholder="请输入上报时间" :disabled="opertype != 1" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="可用值" prop="value">
              <el-input v-model="form.value" placeholder="请输入可用值" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="原始值" prop="rawValue">
              <el-input v-model="form.rawValue" placeholder="请输入原始值" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="数据质量" prop="quality">
              <el-input v-model="form.quality" placeholder="请输入数据质量" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="单位" prop="unit">
              <el-input v-model="form.unit" placeholder="请输入单位" />
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

<script setup name="iotdevicedata">
import { listIotDeviceData, addIotDeviceData, delIotDeviceData, getIotDeviceData } from '@/api/iot/iotDeviceData.js'
import { dictIotDevice } from '@/api/iot/iotDevice'
const { proxy } = getCurrentInstance()
const ids = ref([])
const loading = ref(false)
const showSearch = ref(true)
const queryParams = reactive({
  pageNum: 1,
  pageSize: 10,
  sort: 'CollectTime',
  sortType: 'desc',
  deviceId: undefined,
  identifier: undefined,
  collectTime: undefined
})
const columns = ref([
  { visible: false, prop: 'deviceId', label: '设备ID' },
  { visible: true, prop: 'deviceName', label: '设备名称' },
  { visible: true, prop: 'deviceKey', label: '设备Key' },
  { visible: true, prop: 'identifier', label: '属性标识' },
  { visible: true, prop: 'collectTime', label: '上报时间' },
  { visible: true, prop: 'value', label: '可用值' },
  { visible: true, prop: 'rawValue', label: '原始值' },
  { visible: true, prop: 'quality', label: '数据质量' },
  { visible: true, prop: 'unit', label: '单位' }
])
const total = ref(0)
const dataList = ref([])
const queryRef = ref()
const defaultTime = ref([new Date(2000, 1, 1, 0, 0, 0), new Date(2000, 2, 1, 23, 59, 59)])

// 上报时间时间范围
const dateRangeCollectTime = ref([])

var dictParams = [{ dictType: 'iot_data_quality' }]

proxy.getDicts(dictParams).then((response) => {
  response.data.forEach((element) => {
    state.options[element.dictType] = element.list
  })
})

function getList() {
  proxy.addDateRange(queryParams, dateRangeCollectTime.value, 'CollectTime')
  loading.value = true
  listIotDeviceData(queryParams).then((res) => {
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
  // 上报时间时间范围
  dateRangeCollectTime.value = []
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
    identifier: [{ required: true, message: '属性标识不能为空', trigger: 'change' }],
    collectTime: [{ required: true, message: '上报时间不能为空', trigger: 'blur' }],
    rawValue: [{ required: true, message: '原始值不能为空', trigger: 'blur' }],
    quality: [{ required: true, message: '数据质量不能为空', trigger: 'blur' }]
  },
  options: {
    // 设备ID 选项列表 格式 eg:{ dictLabel: '标签', dictValue: '0'}
    device_options: [],
    // 数据质量
    iot_data_quality: []
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
    deviceId: null,
    identifier: null,
    collectTime: null,
    value: null,
    rawValue: null,
    quality: null,
    unit: null
  }
  proxy.resetForm('formRef')
}

// 添加按钮操作
function handleAdd() {
  reset()
  open.value = true
  title.value = '添加设备采集数据'
  opertype.value = 1
}

// 添加&修改 表单提交
function submitForm() {
  proxy.$refs['formRef'].validate((valid) => {
    if (valid) {
      if (form.value.deviceId != undefined && opertype.value === 2) {
      } else {
        addIotDeviceData(form.value).then((res) => {
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
  const Ids = row.deviceId || ids.value

  proxy
    .$confirm('是否确认删除参数编号为"' + Ids + '"的数据项？', '警告', {
      confirmButtonText: proxy.$t('common.ok'),
      cancelButtonText: proxy.$t('common.cancel'),
      type: 'warning'
    })
    .then(function () {
      return delIotDeviceData(Ids)
    })
    .then(() => {
      getList()
      proxy.$modal.msgSuccess('删除成功')
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

handleQuery()
</script>
