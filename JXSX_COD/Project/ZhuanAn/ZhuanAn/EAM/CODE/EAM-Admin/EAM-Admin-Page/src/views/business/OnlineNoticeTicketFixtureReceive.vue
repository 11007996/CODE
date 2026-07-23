<!--
 * @Descripttion: (上线通知单操作/BO_Online_Notice_Ticket_Receive)
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

    <!-- 治具清单 -->
    <el-table :data="dataList" v-loading="loading" ref="table" header-cell-class-name="el-table-header-cell" highlight-current-row>
      <el-table-column prop="fixtureName" label="治具名称" min-width="160" />
      <el-table-column prop="needQty" label="需求数量" width="90" align="center" />
      <el-table-column prop="receiveQty" label="已领数量" width="90" align="center" />
      <el-table-column prop="backQty" label="已还数量" width="90" align="center" />
      <el-table-column label="操作" width="90" align="center">
        <template #default="scope">
          <el-button
            v-if="scope.row.receiveQty < scope.row.needQty"
            type="primary"
            size="small"
            v-hasPermi="['fixture:storage:receive']"
            @click="handleReceive(scope.row)">
            领用
          </el-button>
        </template>
      </el-table-column>
    </el-table>

    <!-- 领用对话对话框-->
    <el-dialog :title="title" :lock-scroll="false" v-model="open">
      <el-form ref="formRef" :model="form" label-width="100px">
        <el-table :data="receiveList" @selection-change="handleSelectionChange">
          <el-table-column type="selection" width="40" align="center" />
          <el-table-column label="治具名称" min-width="160" align="center" prop="fixtureName" />
          <el-table-column label="储位" min-width="300" align="center" prop="storageFullName" />
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

<script setup name="onlinenoticeticketfixturereceive">
import { getOnlineNoticeTicketFixtureSummary } from '@/api/business/onlineNoticeTicket.js'
import { listFixtureStorage, batchReceiveFixtureStorage } from '@/api/fixture/fixtureStorage.js'
const props = defineProps({
  ticketNo: String
})
const { proxy } = getCurrentInstance()
const loading = ref(false)
const ticketNo = props.ticketNo

const dataList = ref([])
const queryRef = ref()

// 查询
function getList() {
  getOnlineNoticeTicketFixtureSummary(ticketNo).then((res) => {
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
  selectedFixtures: []
})

const { form, selectedFixtures } = toRefs(state)

// 关闭dialog
function cancel() {
  open.value = false
  reset()
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
function handleReceive(row) {
  reset()
  open.value = true
  title.value = '上线通知单_治具领用'
  const params = {
    pageNum: 1,
    pageSize: 100,
    sortType: 'desc',
    fixtureId: row.fixtureId
  }
  listFixtureStorage(params).then((res) => {
    receiveList.value = res.data.result
  })
}

// 添加&修改 表单提交
function submitForm() {
  form.value.fixtures = selectedFixtures.value
  if (form.value.ticketNo != null) {
    batchReceiveFixtureStorage(form.value.ticketNo, form.value).then((res) => {
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
  selectedFixtures.value = selection
}

getList()
</script>
