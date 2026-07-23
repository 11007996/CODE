<!--
 * @Descripttion: (产品设备表/IOT_Device)
 * @Author: (admin)
 * @Date: (2026-01-07)
-->
<template>
  <div>
    <el-form :model="queryParams" label-position="right" inline ref="queryRef" v-show="showSearch" @submit.prevent>
      <el-form-item label="产品" prop="productId">
        <el-select clearable filterable remote :remote-method="handleQueryIotProduct" v-model="queryParams.productId" placeholder="请选择产品">
          <el-option v-for="item in options.product_options" :key="item.dictValue" :label="item.dictLabel" :value="item.dictValue">
            <span class="fl">{{ item.dictLabel }}</span>
            <span class="fr" style="color: var(--el-text-color-secondary)">{{ item.dictValue }}</span>
          </el-option>
        </el-select>
      </el-form-item>
      <el-form-item label="设备名称" prop="deviceName">
        <el-input v-model="queryParams.deviceName" placeholder="请输入设备名称" />
      </el-form-item>
      <el-form-item label="设备Key" prop="deviceKey">
        <el-input v-model="queryParams.deviceKey" placeholder="请输入设备Key" />
      </el-form-item>
      <el-form-item label="注册包" prop="registerPacket">
        <el-input v-model="queryParams.registerPacket" placeholder="请输入注册包" />
      </el-form-item>
      <el-form-item>
        <el-button icon="search" type="primary" @click="handleQuery">{{ $t('btn.search') }}</el-button>
        <el-button icon="refresh" @click="resetQuery">{{ $t('btn.reset') }}</el-button>
      </el-form-item>
    </el-form>
    <!-- 工具区域 -->
    <el-row :gutter="15" class="mb10">
      <el-col :span="1.5">
        <el-button type="primary" v-hasPermi="['iot:device:add']" plain icon="plus" @click="handleAdd">
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
      <el-table-column prop="deviceId" label="设备Id" align="center" width="90" v-if="columns.showColumn('deviceId')" />
      <el-table-column prop="productId" label="产品ID" align="center" width="90" v-if="columns.showColumn('productId')" />
      <el-table-column prop="productName" label="产品名称" align="center" min-width="150" v-if="columns.showColumn('productName')" />
      <el-table-column
        prop="deviceName"
        label="iot设备名称"
        align="center"
        min-width="150"
        :show-overflow-tooltip="true"
        v-if="columns.showColumn('deviceName')" />
      <el-table-column prop="deviceKey" label="设备Key" align="center" min-width="150" v-if="columns.showColumn('deviceKey')" />
      <el-table-column prop="registerPacket" label="注册包" align="center" width="150" v-if="columns.showColumn('registerPacket')" />
      <el-table-column
        prop="equipmentName"
        label="绑定资产"
        align="center"
        min-width="250"
        :show-overflow-tooltip="true"
        v-if="columns.showColumn('equipmentName')" />
      <el-table-column
        prop="boxName"
        label="绑定盒子"
        align="center"
        min-width="250"
        :show-overflow-tooltip="true"
        v-if="columns.showColumn('boxName')" />
      <el-table-column prop="status" label="设备状态" align="center" width="90" v-if="columns.showColumn('status')">
        <template #default="scope">
          <dict-tag :options="options.iot_device_status" :value="scope.row.status" />
        </template>
      </el-table-column>
      <el-table-column
        prop="ipAddress"
        label="IP地址"
        align="center"
        width="150"
        :show-overflow-tooltip="true"
        v-if="columns.showColumn('ipAddress')" />
      <el-table-column prop="activateTime" label="激活时间" align="center" width="160" v-if="columns.showColumn('activateTime')" />
      <el-table-column prop="lastOnlineTime" label="最后上线时间" align="center" width="160" v-if="columns.showColumn('lastOnlineTime')" />
      <el-table-column prop="enabled" label="是否可用" align="center" width="90" v-if="columns.showColumn('enabled')">
        <template #default="scope">
          <el-switch v-model="scope.row.enabled" active-text="是" inactive-text="否" inline-prompt disabled />
        </template>
      </el-table-column>
      <el-table-column prop="createBy" label="创建人" align="center" width="90" v-if="columns.showColumn('createBy')" />
      <el-table-column prop="createTime" label="创建时间" align="center" width="160" v-if="columns.showColumn('createTime')" />
      <el-table-column prop="updateBy" label="更新人" align="center" width="90" v-if="columns.showColumn('updateBy')" />
      <el-table-column prop="updateTime" label="更新时间" align="center" width="160" v-if="columns.showColumn('updateTime')" />
      <el-table-column label="操作" width="220" fixed="right">
        <template #default="scope">
          <el-button
            type="success"
            size="small"
            icon="edit"
            title="编辑"
            v-hasPermi="['iot:device:edit']"
            @click="handleUpdate(scope.row)"></el-button>
          <el-button
            type="danger"
            size="small"
            icon="delete"
            title="删除"
            v-hasPermi="['iot:device:delete']"
            @click="handleDelete(scope.row)"></el-button>
          <el-button type="info" size="small" icon="Link" title="绑定" v-hasPermi="['iot:device:bind']" @click="handleBind(scope.row)"></el-button>
          <el-button type="primary" size="small" @click="handleOpenConfig(scope.row)">配置</el-button>
        </template>
      </el-table-column>
    </el-table>
    <pagination :total="total" v-model:page="queryParams.pageNum" v-model:limit="queryParams.pageSize" @pagination="getList" />

    <!-- 添加或修改产品设备表对话框 -->
    <el-dialog :title="title" :lock-scroll="false" v-model="open">
      <el-form ref="formRef" :model="form" :rules="rules" label-width="100px">
        <el-row :gutter="20">
          <!-- <el-col :lg="12" v-if="opertype != 1">
            <el-form-item label="设备Id" prop="deviceId">
              <el-input-number v-model.number="form.deviceId" controls-position="right" placeholder="请输入设备Id" :disabled="true" />
            </el-form-item>
          </el-col> -->

          <el-col :lg="12">
            <el-form-item label="产品" prop="productId">
              <el-select
                v-model="form.productId"
                clearable
                filterable
                remote
                :remote-method="handleQueryIotProduct"
                :disabled="opertype != 1"
                placeholder="请选择产品">
                <el-option
                  v-for="item in options.product_options"
                  :key="item.dictValue"
                  :label="item.dictLabel"
                  :value="parseInt(item.dictValue)"></el-option>
              </el-select>
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="设备名称" prop="deviceName">
              <el-input v-model="form.deviceName" placeholder="请输入设备名称" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="设备Key" prop="deviceKey">
              <el-input v-model="form.deviceKey" placeholder="请输入设备Key" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="注册包" prop="registerPacket">
              <template #label>
                <span>
                  <el-tooltip placement="top" content="自定义TCP协议使用，格式：12位字符，大写字母与数字组成。">
                    <el-icon> <questionFilled /> </el-icon>
                  </el-tooltip>
                  注册包
                </span>
              </template>
              <el-input v-model="form.registerPacket" placeholder="请输入注册包">
                <template #append>
                  <el-button type="success" size="small" icon="refresh" @click="GenerateSecureString"></el-button>
                </template>
              </el-input>
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="是否可用" prop="enabled">
              <el-switch v-model="form.enabled" active-text="是" inactive-text="否" inline-prompt />
            </el-form-item>
          </el-col>
        </el-row>
      </el-form>
      <template #footer v-if="opertype != 3">
        <el-button text @click="cancel">{{ $t('btn.cancel') }}</el-button>
        <el-button type="primary" @click="submitForm">{{ $t('btn.submit') }}</el-button>
      </template>
    </el-dialog>

    <!-- Iot设备绑定表单 -->
    <el-dialog :title="title" :lock-scroll="false" v-model="openBind">
      <el-form ref="bindFormRef" :model="bindForm" :rules="rules" label-width="100px">
        <el-row :gutter="20">
          <el-col :lg="24">
            <el-form-item label="生产设备" prop="equipmentId">
              <el-select
                v-model="bindForm.equipmentId"
                placeholder="资产编号/设备名称/资产名称/自定义机型"
                clearable
                filterable
                remote
                :remote-method="handleQueryEquipment"
                class="fullWidth">
                <el-option
                  v-for="item in options.equipment_options"
                  :key="item.dictValue"
                  :label="item.dictLabel"
                  :value="item.dictValue"></el-option>
              </el-select>
            </el-form-item>
          </el-col>

          <el-col :lg="24">
            <el-form-item label="呼叫盒" prop="boxId">
              <el-select
                v-model="bindForm.boxId"
                placeholder="呼叫盒名称、MAC、IP"
                clearable
                filterable
                remote
                :remote-method="handleQueryCallBox"
                class="fullWidth">
                <el-option
                  v-for="item in options.callBox_options"
                  :key="item.dictValue"
                  :label="item.dictLabel"
                  :value="item.dictValue"></el-option>
              </el-select>
            </el-form-item>
          </el-col>
        </el-row>
      </el-form>
      <template #footer v-if="opertype != 3">
        <el-button text @click="cancelBind">{{ $t('btn.cancel') }}</el-button>
        <el-button type="primary" @click="submitBindForm">{{ $t('btn.submit') }}</el-button>
      </template>
    </el-dialog>

    <IotDeviceConfig :deviceId="deviceId" v-if="openConfig" @closeForm="handleCloseConfigForm" />
  </div>
</template>

<script setup name="iotdevice">
import IotDeviceConfig from './IotDeviceConfig.vue'
import { listIotDevice, addIotDevice, delIotDevice, updateIotDevice, getIotDevice, bindIotDevice, unbindIotDevice } from '@/api/iot/iotDevice.js'
import { dictIotProduct } from '@/api/iot/iotProduct.js'
import { dictEquipmentBase } from '@/api/equipment/equipmentBase.js'
import { dictCallBoxBase } from '@/api/call/callBoxBase'

const { proxy } = getCurrentInstance()
const ids = ref([])
const loading = ref(false)
const showSearch = ref(true)
const queryParams = reactive({
  pageNum: 1,
  pageSize: 10,
  sort: '',
  sortType: 'asc',
  productId: undefined,
  deviceName: undefined,
  deviceKey: undefined,
  registerPacket: undefined
})
const columns = ref([
  { visible: false, prop: 'deviceId', label: '设备Id' },
  { visible: false, prop: 'productId', label: '产品ID' },
  { visible: true, prop: 'productName', label: '产品名称' },
  { visible: true, prop: 'deviceName', label: '设备名称' },
  { visible: true, prop: 'deviceKey', label: '设备Key' },
  { visible: true, prop: 'registerPacket', label: '注册包' },
  { visible: true, prop: 'equipmentName', label: '绑定资产' },
  { visible: true, prop: 'boxName', label: '绑定盒子' },
  { visible: true, prop: 'status', label: '设备状态' },
  { visible: false, prop: 'ipAddress', label: 'IP地址' },
  { visible: false, prop: 'activateTime', label: '激活时间' },
  { visible: true, prop: 'lastOnlineTime', label: '最后上线时间' },
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

var dictParams = [{ dictType: 'iot_device_status' }]

proxy.getDicts(dictParams).then((response) => {
  response.data.forEach((element) => {
    state.options[element.dictType] = element.list
  })
})

function getList() {
  loading.value = true
  listIotDevice(queryParams).then((res) => {
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
// 操作类型 1、add 2、edit 3、view,4、bind,5、unbind
const opertype = ref(0)
const open = ref(false)
const state = reactive({
  single: true,
  multiple: true,
  form: {},
  rules: {
    productId: [{ required: true, message: '产品ID不能为空', trigger: 'change', type: 'number' }],
    deviceName: [{ required: true, message: '设备名称不能为空', trigger: 'blur' }],
    deviceKey: [{ required: true, message: '设备Key不能为空', trigger: 'blur' }],
    status: [{ required: true, message: '设备状态不能为空', trigger: 'change' }],
    enabled: [{ required: true, message: '是否可用不能为空', trigger: 'blur' }]
  },
  options: {
    // 产品ID 选项列表 格式 eg:{ dictLabel: '标签', dictValue: '0'}
    product_options: [],
    // 设备状态
    iot_device_status: [],
    // 生产设备
    equipment_options: []
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
    productId: null,
    deviceName: null,
    deviceKey: null,
    registerPacket: null,
    status: null,
    ipAddress: null,
    activateTime: null,
    lastOnlineTime: null,
    enabled: null,
    createBy: null,
    createTime: null,
    updateBy: null,
    updateTime: null
  }
  proxy.resetForm('formRef')
}

// 添加按钮操作
function handleAdd() {
  reset()
  open.value = true
  title.value = '添加产品设备表'
  opertype.value = 1
}
// 修改按钮操作
function handleUpdate(row) {
  reset()
  const id = row.deviceId || ids.value
  getIotDevice(id).then((res) => {
    const { code, data } = res
    if (code == 200) {
      open.value = true
      title.value = '修改产品设备表'
      opertype.value = 2

      form.value = {
        ...data
      }

      options.value.product_options = [{ dictValue: form.value.productId, dictLabel: form.value.productName }]
    }
  })
}

// 添加&修改 表单提交
function submitForm() {
  proxy.$refs['formRef'].validate((valid) => {
    if (valid) {
      if (form.value.deviceId != undefined && opertype.value === 2) {
        updateIotDevice(form.value).then((res) => {
          proxy.$modal.msgSuccess('修改成功')
          open.value = false
          getList()
        })
      } else {
        addIotDevice(form.value).then((res) => {
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
      return delIotDevice(Ids)
    })
    .then(() => {
      getList()
      proxy.$modal.msgSuccess('删除成功')
    })
}

// 查询产品
function handleQueryIotProduct(keyword) {
  if (keyword) {
    const params = {
      pageNum: 1,
      pageSize: 10,
      sort: '',
      sortType: 'asc',
      productName: keyword
    }
    setTimeout(() => {
      dictIotProduct(params).then((res) => {
        state.options.product_options = res.data.result
      })
    }, 200)
  }
}

/// <summary>
/// 生成随机字符串
/// </summary>
/// <param name="length"></param>
/// <returns></returns>
function GenerateSecureString() {
  let _chars = 'ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789'
  let result = []
  for (let i = 0; i < 12; i++) {
    result.push(_chars[Math.floor(Math.random() * _chars.length)])
  }
  form.value.registerPacket = result.toString().replaceAll(',', '')
}

// -----------------------绑定表单---------------------------
const bindFormRef = ref()
const openBind = ref(false)
const bindForm = ref({
  deviceId: undefined,
  equipmentId: undefined
})
// 查询设备
function handleQueryEquipment(keyword) {
  if (keyword) {
    const params = {
      pageNum: 1,
      pageSize: 10,
      sort: '',
      sortType: 'asc',
      keyword: keyword
    }
    setTimeout(() => {
      dictEquipmentBase(params).then((res) => {
        state.options.equipment_options = res.data.result
      })
    }, 200)
  }
}

//查询呼叫盒
function handleQueryCallBox(keyword) {
  if (keyword) {
    const params = {
      pageNum: 1,
      pageSize: 10,
      sort: '',
      sortType: 'asc',
      keyword: keyword
    }
    setTimeout(() => {
      dictCallBoxBase(params).then((res) => {
        state.options.callBox_options = res.data.result
      })
    }, 200)
  }
}

// 绑定&解绑 表单提交
function submitBindForm() {
  proxy.$refs['bindFormRef'].validate((valid) => {
    if (valid) {
      if (opertype.value == 4) {
        bindIotDevice(bindForm.value).then((res) => {
          proxy.$modal.msgSuccess('绑定成功')
          openBind.value = false
          getList()
        })
      }
      // } else {
      //   unbindIotDevice(bindForm.value.deviceId).then((res) => {
      //     proxy.$modal.msgSuccess('解绑成功')
      //     openBind.value = false
      //     getList()
      //   })
      // }
    }
  })
}

function handleBind(row) {
  bindForm.value = {}
  const id = row.deviceId || ids.value
  getIotDevice(id).then((res) => {
    const { code, data } = res
    if (code == 200) {
      if (data.bindInfo) {
        //存在绑定
        bindForm.value = { ...data.bindInfo }
        options.value.equipment_options = [{ dictValue: bindForm.value.equipmentId, dictLabel: bindForm.value.equipmentName }]
        options.value.callBox_options = [{ dictValue: bindForm.value.boxId, dictLabel: bindForm.value.boxName }]
        opertype.value = 4
        title.value = '绑定生产设备'
      } else {
        //没有绑定
        bindForm.value = { deviceId: id }
        options.value.equipment_options = []
        options.value.callBox_options = []
      }
      opertype.value = 4
      title.value = '绑定生产设备'
      openBind.value = true
    }
  })

  bindForm.value = {
    deviceId: row.deviceId,
    equipmentId: row.equipmentId
  }
  openBind.value = true
  title.value = '绑定生产设备'
}

// 关闭dialog
function cancelBind() {
  openBind.value = false
}

//-----------------设备配置-----------------------
const deviceId = ref()
const openConfig = ref(false)
function handleOpenConfig(row) {
  deviceId.value = row.deviceId
  openConfig.value = true
}
function handleCloseConfigForm() {
  openConfig.value = false
}

handleQuery()
</script>
