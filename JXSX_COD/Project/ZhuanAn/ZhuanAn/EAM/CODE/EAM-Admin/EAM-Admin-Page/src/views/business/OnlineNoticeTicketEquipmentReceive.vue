<!--
 * @Descripttion: (上线通知单操作/BO_Online_Notice_Ticket_Equipment_Receive)
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

    <!-- 设备清单 -->
    <el-table :data="dataList" v-loading="loading" ref="table" header-cell-class-name="el-table-header-cell" highlight-current-row>
      <el-table-column prop="equipmentName" label="设备名称" min-width="160" />
      <el-table-column prop="needQty" label="需求数量" width="90" align="center" />
      <el-table-column prop="receiveQty" label="已领数量" width="90" align="center" />
      <el-table-column prop="backQty" label="已还数量" width="90" align="center" />
      <el-table-column label="操作" width="90" align="center">
        <template #default="scope">
          <el-button
            v-if="scope.row.receiveQty < scope.row.needQty"
            type="primary"
            size="small"
            v-hasPermi="['equipment:storage:receive']"
            @click="handleReceive(scope.row)">
            领用
          </el-button>
        </template>
      </el-table-column>
    </el-table>

    <!-- 领用对话对话框-->
    <el-dialog :title="title" :lock-scroll="false" v-model="open">
      <!-- 查询表单 -->
      <el-form :model="queryParams" label-position="right" inline ref="queryRef" @submit.prevent>
        <el-form-item label="设备名称" prop="equipmentName">
          <el-input v-model="queryParams.equipmentName" placeholder="请输入设备名称" />
        </el-form-item>
        <el-form-item>
          <el-button icon="search" type="primary" @click="handleQuery">{{ $t('btn.search') }}</el-button>
          <el-button icon="refresh" @click="resetQuery">{{ $t('btn.reset') }}</el-button>
        </el-form-item>
      </el-form>
      <!-- 设备领用列表 -->
      <el-form ref="formRef" :model="form" label-width="100px">
        <el-table :data="equipmentList" @selection-change="handleSelectionChange">
          <el-table-column type="selection" width="40" align="center" />
          <el-table-column label="设备名称" min-width="200" align="center" prop="equipmentName" />
          <el-table-column label="资产编号" width="200" align="center" prop="assetNo" />
          <el-table-column label="资产名称" min-width="200" align="center" prop="assetName" />
          <el-table-column prop="status" label="状态" align="center" width="90">
            <template #default="scope">
              <dict-tag :options="options.equipment_status" :value="scope.row.status" />
            </template>
          </el-table-column>
        </el-table>
        <pagination :total="total" v-model:page="queryParams.pageNum" v-model:limit="queryParams.pageSize" @pagination="getEquipmentList" />
      </el-form>

      <template #footer>
        <el-button text @click="cancel">{{ $t('btn.cancel') }}</el-button>
        <el-button type="primary" @click="submitForm">{{ $t('btn.submit') }}</el-button>
      </template>
    </el-dialog>
  </div>
</template>

<script setup name="onlinenoticeticketequipmentreceive">
import { getOnlineNoticeTicketEquipmentSummary } from '@/api/business/onlineNoticeTicket.js'
import { listEquipmentBase } from '@/api/equipment/equipmentBase.js'
const props = defineProps({
  ticketNo: String
})
const { proxy } = getCurrentInstance()
const loading = ref(false)
const ticketNo = props.ticketNo

const dataList = ref([])
const queryRef = ref()
const equipmentList = ref([])
const total = ref(0)

var dictParams = [{ dictType: 'equipment_status' }]

proxy.getDicts(dictParams).then((response) => {
  response.data.forEach((element) => {
    state.options[element.dictType] = element.list
  })
})

// 查询
function getList() {
  getOnlineNoticeTicketEquipmentSummary(ticketNo).then((res) => {
    const { code, data } = res
    if (code == 200) {
      dataList.value = data
    }
  })
}

/*************** form操作 ***************/
const queryParams = reactive({
  pageNum: 1,
  pageSize: 10,
  sort: '',
  sortType: 'asc',
  assetNo: undefined,
  assetName: undefined,
  equipmentName: undefined
})
const formRef = ref()
const title = ref('')
const open = ref(false)
const state = reactive({
  form: {},
  selectedEquipments: [],
  options: {
    // 设备状态
    equipment_status: []
  }
})

const { form, selectedEquipments, options } = toRefs(state)

// 关闭dialog
function cancel() {
  open.value = false
  reset()
}

// 查询
function handleQuery() {
  queryParams.pageNum = 1
  getEquipmentList()
}

// 重置查询操作
function resetQuery() {
  proxy.resetForm('queryRef')
  handleQuery()
}

// 重置表单
function reset() {
  form.value = {
    ticketNo: ticketNo
  }
  receiveList.value = []
  proxy.resetForm('formRef')
}

// 领用按钮操作
const receiveList = ref([])
const currEquipmentName = ref('')
function handleReceive(row) {
  reset()
  open.value = true
  title.value = '上线通知单_设备领用'
  currEquipmentName.value = row.equipmentName
  queryParams.equipmentName = row.equipmentName
  getEquipmentList()
}

// 添加&修改 表单提交
function submitForm() {
  selectedEquipments.value.forEach((item) => {
    item.remark = currEquipmentName.value
  })
  form.value.equipments = selectedEquipments.value
  if (form.value.ticketNo != null) {
    batchReceiveEquipment(form.value.ticketNo, form.value).then((res) => {
      const { code, data } = res
      if (code == 200) {
        proxy.$modal.msgSuccess('领用成功')
        open.value = false
        getList()
      }
    })
  }
}

//获取闲置设备

function getEquipmentList() {
  listEquipmentBase(queryParams).then((res) => {
    const { code, data } = res
    if (code == 200) {
      equipmentList.value = data.result
      total.value = data.totalNum
    }
  })
}

//选择
function handleSelectionChange(selection) {
  selectedEquipments.value = selection
}

getList()
</script>
