<!--
 * @Descripttion: (上线通知单操作/BU_Online_Notice_Ticket_Equipment_Handle)
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

    <el-row :gutter="20">
      <el-col :lg="8">
        <el-card>
          <template #header>
            <div class="card-header">
              <span>需求清单</span>
            </div>
          </template>

          <!-- 需求清单 -->
          <el-table
            :data="equipmentDemandList"
            :max-height="tableMaxHeight"
            v-loading="loading"
            ref="table"
            header-cell-class-name="el-table-header-cell"
            highlight-current-row>
            <el-table-column prop="equipmentName" label="设备名称" min-width="160" />
            <el-table-column prop="needQty" label="需求数量" width="90" align="center" />
            <el-table-column label="操作" width="90" align="center">
              <template #default="scope">
                <el-button type="primary" size="small" v-hasPermi="['equipment:storage:receive']" @click="handleReceive(scope.row)">
                  领用
                </el-button>
              </template>
            </el-table-column>
          </el-table>
        </el-card>
      </el-col>

      <el-col :lg="8">
        <el-card>
          <template #header>
            <div class="card-header">
              <span>已领清单</span>
            </div>
          </template>
          <!-- 已领清单 -->
          <el-table
            :data="equipmentReceiveList"
            :max-height="tableMaxHeight"
            v-loading="loading"
            ref="table"
            header-cell-class-name="el-table-header-cell"
            highlight-current-row>
            <el-table-column prop="equipmentName" label="设备名称" min-width="160" />
            <el-table-column label="操作" width="90" align="center">
              <template #default="scope">
                <el-button
                  v-if="scope.row.receiveQty > scope.row.backQty"
                  type="primary"
                  size="small"
                  title="归还"
                  v-hasPermi="['equipment:storage:back']"
                  @click="handleBack(scope.row)">
                  归还
                </el-button>
              </template>
            </el-table-column>
          </el-table>
        </el-card>
      </el-col>

      <el-col :lg="8">
        <el-card>
          <template #header>
            <div class="card-header">
              <span>操作记录</span>
            </div>
          </template>
          <!-- 已领清单 -->
          <el-table
            :data="storageRecordList"
            :max-height="tableMaxHeight"
            v-loading="loading"
            ref="table"
            header-cell-class-name="el-table-header-cell"
            highlight-current-row>
            <!-- <el-table-column prop="assetNo" label="资产编号" width="210" /> -->
            <el-table-column prop="assetName" label="资产名称" min-width="160" />
            <el-table-column prop="storageChangeType" label="变动类型" width="90" align="center">
              <template #default="scope">
                <dict-tag :options="options.storage_change_type" :value="scope.row.storageChangeType" />
              </template>
            </el-table-column>
            <el-table-column prop="createTime" label="操作时间" width="160" />
          </el-table>
        </el-card>
      </el-col>
    </el-row>

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
      <el-form ref="receiveFormRef" :model="receiveForm" label-width="100px">
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

    <!-- 领用对话对话框-->
    <el-dialog :title="title" :lock-scroll="false" v-model="openBack" width="300">
      <el-form ref="backFormRef" :model="receiveForm" label-width="100px">
        确定要归还吗？ <el-input v-model="backForm.equipmentName" disabled />
      </el-form>
      <template #footer>
        <el-button text @click="cancelBack">{{ $t('btn.cancel') }}</el-button>
        <el-button type="primary" @click="submitBackForm">{{ $t('btn.submit') }}</el-button>
      </template>
    </el-dialog>
  </div>
</template>

<script setup name="onlinenoticeticketequipmenthandle">
import { getOnlineNoticeTicketEquipmentSummary } from '@/api/business/onlineNoticeTicket.js'
import { listEquipmentBase } from '@/api/equipment/equipmentBase.js'
import { batchReceiveEquipment, backEquipmentStorage } from '@/api/equipment/equipmentStorage.js'

const props = defineProps({
  ticketNo: String
})
const { proxy } = getCurrentInstance()
const loading = ref(false)
const ticketNo = props.ticketNo
const ticketInfo = ref({
  ticketNo: props.ticketNo,
  ticketType: null
})
const queryRef = ref()
const equipmentList = ref([])
const equipmentDemandList = ref([])
const equipmentReceiveList = ref([])
const storageRecordList = ref([])
const total = ref(0)
const tableMaxHeight = ref(500)

var dictParams = [{ dictType: 'equipment_status' }, { dictType: 'storage_change_type' }]

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
      ticketInfo.value.ticketNo = data.ticketNo
      ticketInfo.value.ticketType = data.ticketType
      equipmentDemandList.value = data.demandList
      equipmentReceiveList.value = data.receiveList
      storageRecordList.value = data.storageRecordList
    }
  })
}

/*************** form操作 (领用)***************/
const queryParams = reactive({
  pageNum: 1,
  pageSize: 10,
  sort: '',
  sortType: 'asc',
  assetNo: undefined,
  assetName: undefined,
  equipmentName: undefined
})
const receiveFormRef = ref()
const title = ref('')
const open = ref(false)
const state = reactive({
  receiveForm: [],
  options: {
    // 设备状态
    equipment_status: [],
    //存储变动类型
    storage_change_type: []
  }
})

const { receiveForm, options } = toRefs(state)

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
  receiveForm.value = []
  proxy.resetForm('receiveFormRef')
}

// 领用按钮操作
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
  if (receiveForm.value.length > 0) {
    receiveForm.value.forEach((item) => {
      item.ticketNo = ticketInfo.value.ticketNo
      item.ticketType = ticketInfo.value.ticketType
    })
    batchReceiveEquipment(receiveForm.value.ticketNo, receiveForm.value).then((res) => {
      const { code, data } = res
      if (code == 200) {
        proxy.$modal.msgSuccess('领用成功')
        open.value = false
        getList()
      }
    })
  } else {
    proxy.$modal.msgError('请勾选设备')
  }
}

/*************** form操作 (归还)***************/
const openBack = ref(false)
const backForm = ref({
  equipmentId: null,
  equipmentName: null
})

// 关闭dialog
function cancelBack() {
  openBack.value = false
  resetBack()
}

// 重置表单
function resetBack() {
  backForm.value = {
    equipmentId: null,
    equipmentName: null
  }
  proxy.resetForm('backFormRef')
}

// 归还按钮操作
function handleBack(row) {
  resetBack()
  backForm.value.equipmentId = row.equipmentId
  backForm.value.equipmentName = row.equipmentName
  openBack.value = true
  title.value = '上线通知单_设备归还'
}

// 添加&修改 表单提交
function submitBackForm() {
  proxy.$refs['backFormRef'].validate((valid) => {
    if (valid) {
      if (backForm.value.equipmentId != null) {
        backEquipmentStorage(backForm.value).then((res) => {
          const { code, data } = res
          if (code == 200) {
            proxy.$modal.msgSuccess('归还成功')
            openBack.value = false
            getList()
          }
        })
      }
    }
  })
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
  receiveForm.value = selection
}

getList()
</script>
