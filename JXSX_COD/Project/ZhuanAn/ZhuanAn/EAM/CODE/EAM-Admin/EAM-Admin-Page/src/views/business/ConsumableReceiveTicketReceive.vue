<!--
 * @Descripttion: (耗品领用单操作/BO_Consumable_Receive_Ticket_Receive)
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

    <!-- 耗品清单 -->
    <el-table :data="dataList" v-loading="loading" ref="table" header-cell-class-name="el-table-header-cell" highlight-current-row>
      <el-table-column prop="consumable" label="耗品(料号/名称/规格)" min-width="160" :formatter="formatter" />
      <el-table-column prop="needQty" label="需求" width="90" align="center" />
      <el-table-column prop="receiveQty" label="领取" width="90" align="center" />
      <el-table-column prop="backQty" label="归还" width="90" align="center" />
      <el-table-column label="操作" width="90" align="center">
        <template #default="scope">
          <el-button
            v-if="scope.row.receiveQty < scope.row.needQty"
            type="primary"
            size="small"
            v-hasPermi="['consumable:storage:receive']"
            @click="handleReceive(scope.row)">
            领用
          </el-button>
        </template>
      </el-table-column>
    </el-table>

    <!-- 领用对话对话框-->
    <el-dialog :title="title" :lock-scroll="false" v-model="open">
      <el-form ref="formRef" :model="form" label-width="100px">
        <el-table :data="consumableList" @selection-change="handleSelectionChange">
          <el-table-column type="selection" width="40" align="center" />
          <el-table-column label="耗品(料号/名称/规格)" min-width="160" prop="consumable" :formatter="formatter" />
          <el-table-column label="储位" min-width="250" prop="storageFullName" />
          <el-table-column label="闲置数量" width="90" align="center" prop="qty" />
          <el-table-column label="领用数量" width="90" align="center" prop="changeQty">
            <template #default="scope">
              <el-input-number v-model="scope.row.changeQty" :controls="false" class="fullWidth" />
            </template>
          </el-table-column>
        </el-table>
      </el-form>

      <template #footer>
        <el-button text @click="cancel">{{ $t('btn.cancel') }}</el-button>
        <el-button type="primary" @click="submitForm">{{ $t('btn.submit') }}</el-button>
      </template>
    </el-dialog>
  </div>
</template>

<script setup name="consumablereceiveticketreceive">
import { getConsumableReceiveTicketSummary, batchReceiveConsumable } from '@/api/business/consumableReceiveTicket.js'
import { listConsumableStorage } from '@/api/consumable/consumableStorage.js'

const props = defineProps({
  ticketNo: String
})
const { proxy } = getCurrentInstance()
const loading = ref(false)
const ticketNo = ref(null)
ticketNo.value = props.ticketNo

const dataList = ref([])
const queryRef = ref()
const consumableList = ref([])

// 查询
function getList() {
  getConsumableReceiveTicketSummary(ticketNo.value).then((res) => {
    const { code, data } = res
    if (code == 200) {
      dataList.value = data
    }
  })
}

/*************** form操作 ***************/
const formRef = ref()
const title = ref('')
const open = ref(false)
const state = reactive({
  form: {},
  selectedConsumables: []
})

const { form, selectedConsumables } = toRefs(state)

// 关闭dialog
function cancel() {
  open.value = false
  reset()
}

// 重置表单
function reset() {
  form.value = {
    ticketNo: ticketNo.value
  }
  consumableList.value = []
  proxy.resetForm('formRef')
}

// 领用按钮操作
function handleReceive(row) {
  reset()

  title.value = '耗品领用单_领用'
  const params = {
    pageNum: 1,
    pageSize: 100,
    sortType: 'desc',
    consumableId: row.consumableId
  }
  listConsumableStorage(params).then((res) => {
    const { code, data } = { ...res }
    if (code == 200) {
      consumableList.value = res.data.result
      open.value = true
    }
  })
}

// 添加&修改 表单提交
function submitForm() {
  form.value.consumables = selectedConsumables.value
  if (form.value.ticketNo != null) {
    batchReceiveConsumable(form.value).then((res) => {
      const { code, data } = res
      if (code == 200) {
        proxy.$modal.msgSuccess('领用成功')
        open.value = false
        getList()
      }
    })
  }
}

//选择
function handleSelectionChange(selection) {
  selectedConsumables.value = selection
}

//单元格式化
function formatter(row, cloumn) {
  return row.consumablePart + ' / ' + row.consumableName + ' / ' + row.spec
}

watch(props, (nevVal) => {
  ticketNo.value = props.ticketNo
  getList()
})
getList()
</script>
