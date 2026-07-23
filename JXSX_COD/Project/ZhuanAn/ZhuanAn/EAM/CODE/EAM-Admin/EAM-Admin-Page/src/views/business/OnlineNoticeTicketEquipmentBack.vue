<!--
 * @Descripttion: (上线通知单操作/BO_Online_Notice_Ticket_Back)
 * @Author: (admin)
 * @Date: (2024-06-27)
-->
<template>
  <div>
    <el-form label-position="right" inline ref="queryRef">
      <el-form-item label="业务编号" prop="ticketNo">
        {{ ticketNo }}
      </el-form-item>
    </el-form>

    <el-table :data="dataList" v-loading="loading" ref="table" header-cell-class-name="el-table-header-cell" highlight-current-row>
      <el-table-column prop="assetNo" label="治具资产编号" min-width="210" />
      <el-table-column prop="assetName" label="资产名称" width="90" align="center" />
      <el-table-column prop="remark" label="备注（设备名称）" min-width="160" align="center" />
      <el-table-column label="操作" width="90" align="center">
        <template #default="scope">
          <el-button type="primary" size="small" title="归还" v-hasPermi="['equipment:storage:back']" @click="handleBack(scope.row)">
            归还
          </el-button>
        </template>
      </el-table-column>
    </el-table>
    <pagination :total="total" v-model:page="queryParams.pageNum" v-model:limit="queryParams.pageSize" @pagination="getList" />

    <!-- 领用对话对话框-->
    <el-dialog :title="title" :lock-scroll="false" v-model="open" width="300">
      <el-form ref="formRef" :model="form" :rules="rules" label-width="100px">
        确定要归还吗？ <el-input v-model="form.assetNo" disabled />
      </el-form>
      <template #footer>
        <el-button text @click="cancel">{{ $t('btn.cancel') }}</el-button>
        <el-button type="primary" @click="submitForm">{{ $t('btn.submit') }}</el-button>
      </template>
    </el-dialog>
  </div>
</template>

<script setup name="onlinenoticeticketequipmentback">
import { listEquipmentStorage, backEquipmentStorage } from '@/api/equipment/equipmentStorage.js'

const props = defineProps({
  ticketNo: String
})
watch(props, (val) => {
  queryParams.ticketNo = props.ticketNo
  handleQuery()
})
const { proxy } = getCurrentInstance()
const loading = ref(false)
const ticketNo = props.ticketNo

const dataList = ref([])
const queryRef = ref()
const total = ref(0)
const queryParams = reactive({
  pageNum: 1,
  pageSize: 10,
  sort: '',
  sortType: 'asc',
  ticketNo: props.ticketNo
})

// 查询
function handleQuery() {
  queryParams.pageNum = 1
  getList()
}

// 查询
function getList() {
  listEquipmentStorage(queryParams).then((res) => {
    const { code, data } = res
    if (code == 200) {
      dataList.value = data.result
    }
  })
}

/*************** form操作 ***************/
const formRef = ref()
const title = ref('')
const open = ref(false)
const state = reactive({
  form: {},
  rules: {}
})
const { form, rules } = toRefs(state)

// 关闭dialog
function cancel() {
  open.value = false
  reset()
}

// 重置表单
function reset() {
  form.value = {
    assetNo: null
  }
  proxy.resetForm('formRef')
}

// 归还按钮操作
function handleBack(row) {
  reset()
  form.value.assetNo = row.assetNo
  open.value = true
  title.value = '上线通知单_设备归还'
}

// 添加&修改 表单提交
function submitForm() {
  proxy.$refs['formRef'].validate((valid) => {
    if (valid) {
      if (form.value.assetNo != null) {
        backEquipmentStorage(form.value).then((res) => {
          const { code, data } = res
          if (code == 200) {
            proxy.$modal.msgSuccess('归还成功')
            open.value = false
            getList()
          }
        })
      }
    }
  })
}

handleQuery()
</script>
