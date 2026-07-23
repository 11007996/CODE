<!--
 * @Descripttion: (设备配置/IOT_Device_Config)
 * @Author: (admin)
 * @Date: (2026-02-27)
-->
<template>
  <div>
    <!-- 添加或修改设备配置对话框 -->
    <el-dialog :title="title" :lock-scroll="false" v-model="open">
      <el-form ref="formRef" :model="form" :rules="rules" label-width="100px">
        <el-row :gutter="20">
          <!-- <el-col :lg="12">
            <el-form-item label="设备ID" prop="deviceId">
              <el-input v-model.number="form.deviceId" placeholder="请输入设备ID" :disabled="opertype != 1" />
            </el-form-item>
          </el-col> -->

          <el-col :lg="12">
            <el-form-item label="关联通道" prop="channelId">
              <el-select v-model="form.channelId" clearable filterable remote :remote-method="handleQueryChannel" placeholder="请选择关联通道">
                <el-option
                  v-for="item in options.channel_options"
                  :key="item.dictValue"
                  :label="item.dictLabel"
                  :value="item.dictValue"></el-option>
              </el-select>
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="从站地址" prop="slaveAddress">
              <el-input v-model="form.slaveAddress" placeholder="请输入从站地址" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="采集间隔(ms)" prop="collectInterval">
              <el-input v-model.number="form.collectInterval" placeholder="请输入采集间隔(ms)" />
            </el-form-item>
          </el-col>
        </el-row>
      </el-form>
      <template #footer v-if="opertype != 3">
        <el-button text @click="cancel">{{ $t('btn.cancel') }}</el-button>
        <el-button type="danger" v-if="opertype == 2" @click="handleDelete">{{ $t('btn.delete') }}</el-button>
        <el-button type="primary" @click="submitForm">{{ $t('btn.submit') }}</el-button>
      </template>
    </el-dialog>
  </div>
</template>

<script setup name="iotdeviceconfig">
import { addIotDeviceConfig, delIotDeviceConfig, updateIotDeviceConfig, getIotDeviceConfig } from '@/api/iot/iotDeviceConfig.js'
import { dictIotCommonChannel } from '@/api/iot/iotCommonChannel.js'
const props = defineProps({
  deviceId: Number
})
const emit = defineEmits(['close-form'])
const { proxy } = getCurrentInstance()
const queryParams = reactive({
  pageNum: 1,
  pageSize: 10,
  sort: '',
  sortType: 'asc',
  deviceId: undefined
})

// 查询
function handleQuery() {
  queryParams.pageNum = 1
  handleUpdate()
}

/*************** form操作 ***************/
const formRef = ref()
const title = ref('')
// 操作类型 1、add 2、edit 3、view
const opertype = ref(0)
const open = ref(false)
const state = reactive({
  form: {},
  rules: {
    deviceId: [{ required: true, message: '设备ID不能为空', trigger: 'change', type: 'number' }],
    channelId: [{ required: true, message: '传输通道不能为空', trigger: 'blur' }],
    slaveAddress: [{ required: true, message: '从站地址不能为空', trigger: 'change' }],
    collectInterval: [{ required: true, message: '采集间隔不能为空', trigger: 'change', type: 'number' }]
  },
  options: {
    // 通道选择
    channel_options: []
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
  form.value = {
    deviceId: null,
    channelId: null,
    slaveAddress: null,
    collectInterval: null,
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
  title.value = '添加设备配置'
  opertype.value = 1
}

// 修改按钮操作
function handleUpdate() {
  reset()
  const id = props.deviceId
  getIotDeviceConfig(id).then((res) => {
    const { code, data } = res
    if (code == 200) {
      open.value = true
      title.value = '修改设备配置'
      opertype.value = 2

      form.value = {
        ...data
      }
      options.value.channel_options = [{ dictLabel: data.channelName, dictValue: data.channelId }]
    } else {
      open.value = true
      title.value = '新增设备配置'
      opertype.value = 1
    }
  })
}

// 添加&修改 表单提交
function submitForm() {
  proxy.$refs['formRef'].validate((valid) => {
    if (valid) {
      if (form.value.deviceId != undefined && opertype.value === 2) {
        updateIotDeviceConfig(form.value).then((res) => {
          proxy.$modal.msgSuccess('修改成功')
          open.value = false
        })
      } else {
        form.value.deviceId = props.deviceId
        addIotDeviceConfig(form.value).then((res) => {
          proxy.$modal.msgSuccess('新增成功')
          open.value = false
        })
      }
    }
  })
}

// 删除按钮操作
function handleDelete() {
  const Ids = props.deviceId
  proxy
    .$confirm('是否确认删除参数编号为"' + Ids + '"的数据项？', '警告', {
      confirmButtonText: proxy.$t('common.ok'),
      cancelButtonText: proxy.$t('common.cancel'),
      type: 'warning'
    })
    .then(function () {
      return delIotDeviceConfig(Ids)
    })
    .then(() => {
      proxy.$modal.msgSuccess('删除成功')
      open.value = false
    })
}

// 查询设备
function handleQueryChannel(keyword) {
  if (keyword) {
    const params = {
      pageNum: 1,
      pageSize: 10,
      sort: '',
      sortType: 'asc',
      channelName: keyword
    }
    setTimeout(() => {
      dictIotCommonChannel(params).then((res) => {
        state.options.channel_options = res.data.result
      })
    }, 200)
  }
}

watch(open, (val) => {
  handleQuery()
  if (open.value === false) emit('close-form')
})

handleQuery()
</script>
