<!--
 * @Descripttion: (传输通道/IOT_Common_Channel)
 * @Author: (admin)
 * @Date: (2026-02-27)
-->
<template>
  <div>
    <el-form :model="queryParams" label-position="right" inline ref="queryRef" v-show="showSearch" @submit.prevent>
      <el-form-item label="通道名称" prop="channelName">
        <el-input v-model="queryParams.channelName" placeholder="请输入通道名称" />
      </el-form-item>
      <el-form-item>
        <el-button icon="search" type="primary" @click="handleQuery">{{ $t('btn.search') }}</el-button>
        <el-button icon="refresh" @click="resetQuery">{{ $t('btn.reset') }}</el-button>
      </el-form-item>
    </el-form>
    <!-- 工具区域 -->
    <el-row :gutter="15" class="mb10">
      <el-col :span="1.5">
        <el-button type="primary" v-hasPermi="['iot:common:channel:add']" plain icon="plus" @click="handleAdd">
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
      <el-table-column prop="channelId" label="通道Id" align="center" v-if="columns.showColumn('channelId')" />
      <el-table-column prop="channelName" label="通道名称" align="center" :show-overflow-tooltip="true" v-if="columns.showColumn('channelName')" />
      <el-table-column
        prop="transferMode"
        label="传输模式"
        align="center"
        :show-overflow-tooltip="true"
        v-if="columns.showColumn('transferMode')" />
      <el-table-column prop="serialPort" label="串口" align="center" :show-overflow-tooltip="true" v-if="columns.showColumn('serialPort')" />
      <el-table-column prop="baudRate" label="比特率" align="center" v-if="columns.showColumn('baudRate')">
        <template #default="scope">
          <dict-tag :options="options.baudRateOptions" :value="scope.row.baudRate" />
        </template>
      </el-table-column>
      <el-table-column prop="dataBits" label="数据位" align="center" v-if="columns.showColumn('dataBits')">
        <template #default="scope">
          <dict-tag :options="options.dataBitsOptions" :value="scope.row.dataBits" />
        </template>
      </el-table-column>
      <el-table-column prop="stopBits" label="停止位" align="center" v-if="columns.showColumn('stopBits')">
        <template #default="scope">
          <dict-tag :options="options.stopBitsOptions" :value="scope.row.stopBits" />
        </template>
      </el-table-column>
      <el-table-column prop="parity" label="奇偶校验" align="center" v-if="columns.showColumn('parity')">
        <template #default="scope">
          <dict-tag :options="options.parityOptions" :value="scope.row.parity" />
        </template>
      </el-table-column>
      <el-table-column prop="ip" label="IP地址" align="center" :show-overflow-tooltip="true" v-if="columns.showColumn('ip')" />
      <el-table-column prop="port" label="端口号" align="center" v-if="columns.showColumn('port')" />
      <el-table-column prop="createBy" label="创建人" align="center" :show-overflow-tooltip="true" v-if="columns.showColumn('createBy')" />
      <el-table-column prop="createTime" label="创建时间" :show-overflow-tooltip="true" v-if="columns.showColumn('createTime')" />
      <el-table-column label="操作" width="160">
        <template #default="scope">
          <el-button
            type="success"
            size="small"
            icon="edit"
            title="编辑"
            v-hasPermi="['iot:common:channel:edit']"
            @click="handleUpdate(scope.row)"></el-button>
          <el-button
            type="danger"
            size="small"
            icon="delete"
            title="删除"
            v-hasPermi="['iot:common:channel:delete']"
            @click="handleDelete(scope.row)"></el-button>
        </template>
      </el-table-column>
    </el-table>
    <pagination :total="total" v-model:page="queryParams.pageNum" v-model:limit="queryParams.pageSize" @pagination="getList" />

    <!-- 添加或修改传输通道对话框 -->
    <el-dialog :title="title" :lock-scroll="false" v-model="open">
      <el-form ref="formRef" :model="form" :rules="rules" label-width="100px">
        <el-row :gutter="20">
          <el-col :lg="12">
            <el-form-item label="通道名称" prop="channelName">
              <el-input v-model="form.channelName" placeholder="请输入通道名称" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="传输模式" prop="transferMode">
              <el-select v-model="form.transferMode" placeholder="请选择传输模式">
                <el-option
                  v-for="item in options.iot_transfer_mode"
                  :key="item.dictValue"
                  :label="item.dictLabel"
                  :value="item.dictValue"></el-option>
              </el-select>
            </el-form-item>
          </el-col>

          <template v-if="form.transferMode == 'RTU'">
            <el-col :lg="12">
              <el-form-item label="串口" prop="serialPort">
                <el-input v-model="form.serialPort" placeholder="请输入串口" />
              </el-form-item>
            </el-col>

            <el-col :lg="12">
              <el-form-item label="比特率" prop="baudRate">
                <el-select v-model="form.baudRate" clearable filterable allow-create placeholder="请选择比特率">
                  <el-option
                    v-for="item in options.baudRateOptions"
                    :key="item.dictValue"
                    :label="item.dictLabel"
                    :value="parseInt(item.dictValue)"></el-option>
                </el-select>
              </el-form-item>
            </el-col>

            <el-col :lg="12">
              <el-form-item label="数据位" prop="dataBits">
                <el-select v-model="form.dataBits" placeholder="请选择数据位">
                  <el-option
                    v-for="item in options.dataBitsOptions"
                    :key="item.dictValue"
                    :label="item.dictLabel"
                    :value="parseInt(item.dictValue)"></el-option>
                </el-select>
              </el-form-item>
            </el-col>

            <el-col :lg="12">
              <el-form-item label="停止位" prop="stopBits">
                <el-select v-model="form.stopBits" placeholder="请选择停止位">
                  <el-option
                    v-for="item in options.stopBitsOptions"
                    :key="item.dictValue"
                    :label="item.dictLabel"
                    :value="parseInt(item.dictValue)"></el-option>
                </el-select>
              </el-form-item>
            </el-col>

            <el-col :lg="12">
              <el-form-item label="奇偶校验" prop="parity">
                <el-select v-model="form.parity" placeholder="请选择奇偶校验">
                  <el-option
                    v-for="item in options.parityOptions"
                    :key="item.dictValue"
                    :label="item.dictLabel"
                    :value="parseInt(item.dictValue)"></el-option>
                </el-select>
              </el-form-item>
            </el-col>
          </template>

          <template v-if="form.transferMode == 'TCP'">
            <el-col :lg="12">
              <el-form-item label="IP地址" prop="ip">
                <el-input v-model="form.ip" placeholder="请输入IP地址" />
              </el-form-item>
            </el-col>

            <el-col :lg="12">
              <el-form-item label="端口号" prop="port">
                <el-input v-model.number="form.port" placeholder="请输入端口号" />
              </el-form-item>
            </el-col>
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

<script setup name="iotcommonchannel">
import {
  listIotCommonChannel,
  addIotCommonChannel,
  delIotCommonChannel,
  updateIotCommonChannel,
  getIotCommonChannel
} from '@/api/iot/iotCommonChannel.js'
const { proxy } = getCurrentInstance()
const ids = ref([])
const loading = ref(false)
const showSearch = ref(true)
const queryParams = reactive({
  pageNum: 1,
  pageSize: 10,
  sort: '',
  sortType: 'asc',
  channelName: undefined
})
const columns = ref([
  { visible: false, prop: 'channelId', label: '通道Id' },
  { visible: true, prop: 'channelName', label: '通道名称' },
  { visible: true, prop: 'transferMode', label: '传输模式' },
  { visible: true, prop: 'serialPort', label: '串口' },
  { visible: true, prop: 'baudRate', label: '比特率' },
  { visible: true, prop: 'dataBits', label: '数据位' },
  { visible: true, prop: 'stopBits', label: '停止位' },
  { visible: true, prop: 'parity', label: '奇偶校验' },
  { visible: true, prop: 'ip', label: 'IP地址' },
  { visible: true, prop: 'port', label: '端口号' },
  { visible: false, prop: 'createBy', label: '创建人' },
  { visible: false, prop: 'createTime', label: '创建时间' }
])
const total = ref(0)
const dataList = ref([])
const queryRef = ref()
const defaultTime = ref([new Date(2000, 1, 1, 0, 0, 0), new Date(2000, 2, 1, 23, 59, 59)])

var dictParams = [{ dictType: 'iot_transfer_mode' }]

proxy.getDicts(dictParams).then((response) => {
  response.data.forEach((element) => {
    state.options[element.dictType] = element.list
  })
})

function getList() {
  loading.value = true
  listIotCommonChannel(queryParams).then((res) => {
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
    channelName: [{ required: true, message: '通道名称不能为空', trigger: 'blur' }],
    transferMode: [{ required: true, message: '传输模式不能为空', trigger: 'blur' }]
  },
  options: {
    // 比特率 选项列表 格式 eg:{ dictLabel: '标签', dictValue: '0'}
    baudRateOptions: [],
    // 数据位
    dataBitsOptions: [],
    // 停止位
    stopBitsOptions: [],
    // 奇偶校验
    parityOptions: [],
    // 传输模式
    iot_transfer_mode: []
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
    channelId: null,
    channelName: null,
    transferMode: null,
    serialPort: null,
    baudRate: null,
    dataBits: null,
    stopBits: null,
    parity: null,
    ip: null,
    port: null,
    createBy: null,
    createTime: null
  }
  proxy.resetForm('formRef')
}

// 添加按钮操作
function handleAdd() {
  reset()
  open.value = true
  title.value = '添加传输通道'
  opertype.value = 1
}
// 修改按钮操作
function handleUpdate(row) {
  reset()
  const id = row.channelId || ids.value
  getIotCommonChannel(id).then((res) => {
    const { code, data } = res
    if (code == 200) {
      open.value = true
      title.value = '修改传输通道'
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
      if (form.value.channelId != undefined && opertype.value === 2) {
        updateIotCommonChannel(form.value).then((res) => {
          proxy.$modal.msgSuccess('修改成功')
          open.value = false
          getList()
        })
      } else {
        addIotCommonChannel(form.value).then((res) => {
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
  const Ids = row.channelId || ids.value

  proxy
    .$confirm('是否确认删除参数编号为"' + Ids + '"的数据项？', '警告', {
      confirmButtonText: proxy.$t('common.ok'),
      cancelButtonText: proxy.$t('common.cancel'),
      type: 'warning'
    })
    .then(function () {
      return delIotCommonChannel(Ids)
    })
    .then(() => {
      getList()
      proxy.$modal.msgSuccess('删除成功')
    })
}

//初始化表单下拉选
function initFormItemOptions() {
  options.value.baudRateOptions = [
    { dictLabel: '4800', dictValue: '4800' },
    { dictLabel: '9600', dictValue: '9600' },
    { dictLabel: '14400', dictValue: '14400' },
    { dictLabel: '19200', dictValue: '19200' },
    { dictLabel: '38400', dictValue: '38400' },
    { dictLabel: '56000', dictValue: '56000' },
    { dictLabel: '57600', dictValue: '57600' },
    { dictLabel: '115200', dictValue: '115200' },
    { dictLabel: '128000', dictValue: '128000' },
    { dictLabel: '230400', dictValue: '230400' }
  ]
  options.value.dataBitsOptions = [
    { dictLabel: '5', dictValue: '5' },
    { dictLabel: '6', dictValue: '6' },
    { dictLabel: '7', dictValue: '7' },
    { dictLabel: '8', dictValue: '8' }
  ]
  options.value.stopBitsOptions = [
    { dictLabel: '1', dictValue: '1' },
    { dictLabel: '1.5', dictValue: '3' },
    { dictLabel: '2', dictValue: '2' }
  ]
  options.value.parityOptions = [
    { dictLabel: 'None', dictValue: '0' },
    { dictLabel: 'Odd', dictValue: '1' },
    { dictLabel: 'Even', dictValue: '2' },
    { dictLabel: 'Mark', dictValue: '3' },
    { dictLabel: 'Space', dictValue: '4' }
  ]
}

initFormItemOptions()
handleQuery()
</script>
