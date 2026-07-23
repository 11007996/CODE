<!--
 * @Descripttion: (耗品领用单操作/BO_Online_Notice_Ticket_Back)
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
      <el-table-column prop="consumable" label="耗品(料号/名称/规格)" min-width="160" :formatter="formatter" />
      <el-table-column prop="receiveQty" label="领用数量" width="90" align="center" />
      <el-table-column prop="qty" label="未还数量" width="90" align="center">
        <template #default="scope">
          {{ scope.row.receiveQty - scope.row.backQty }}
        </template>
      </el-table-column>
      <el-table-column label="操作" width="90">
        <template #default="scope">
          <el-button
            type="primary"
            size="small"
            title="归还"
            v-if="scope.row.receiveQty - scope.row.backQty > 0"
            v-hasPermi="['consumable:storage:back']"
            @click="handleBack(scope.row)">
            归还
          </el-button>
        </template>
      </el-table-column>
    </el-table>

    <!-- 领用对话对话框-->
    <el-dialog :title="title" :lock-scroll="false" v-model="open">
      <el-form ref="formRef" :model="form" :rules="rules" label-width="100px">
        <el-row :gutter="20">
          <el-col :lg="12">
            <el-form-item label="未还数量" prop="qty">
              <el-input v-model="form.qty" disabled />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="归还数量" prop="changeQty">
              <el-input-number v-model.number="form.changeQty" :controls="true" controls-position="right" placeholder="请输入数量" />
            </el-form-item>
          </el-col>

          <el-col :lg="24">
            <el-form-item label="储位" prop="storageId">
              <el-cascader
                class="w100"
                :options="useBasicStore().getConsumableStorageTree"
                :props="{ checkStrictly: true, value: 'storageId', label: 'storageName', emitPath: false }"
                placeholder="请选择储位"
                clearable
                v-model="form.storageId">
                <template #default="{ node, data }">
                  <span>{{ data.storageName }}</span>
                  <span v-if="!node.isLeaf"> ({{ data.children.length }}) </span>
                </template>
              </el-cascader>
            </el-form-item>
          </el-col>
        </el-row>
      </el-form>
      <template #footer>
        <el-button text @click="cancel">{{ $t('btn.cancel') }}</el-button>
        <el-button type="primary" @click="submitForm">{{ $t('btn.submit') }}</el-button>
      </template>
    </el-dialog>
  </div>
</template>

<script setup name="consumablereceiveticketback">
import { getConsumableReceiveTicketReceive } from '@/api/business/consumableReceiveTicket.js'
import { backConsumableStorage } from '@/api/consumable/consumableStorage.js'
import useBasicStore from '@/store/modules/basic.js'
const props = defineProps({
  ticketNo: String
})
const { proxy } = getCurrentInstance()
const loading = ref(false)
const ticketNo = ref(null)
ticketNo.value = props.ticketNo

const dataList = ref([])
const queryRef = ref()

// 查询
function getList() {
  getConsumableReceiveTicketReceive(ticketNo.value).then((res) => {
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
  rules: {
    changeQty: [{ required: true, message: '数量不能为空', trigger: 'blur' }],
    storageId: [{ required: true, message: '储位不能为空', trigger: 'blur' }]
  }
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
    ticketNo: null,
    consumableId: null,
    qty: 0,
    changeQty: 0,
    storageId: null
  }
  proxy.resetForm('formRef')
}

// 归还按钮操作
function handleBack(row) {
  open.value = true
  title.value = '耗品领用单_归还'
  reset()
  form.value.ticketNo = ticketNo.value
  form.value.consumableId = row.consumableId
  form.value.qty = row.receiveQty - row.backQty
}

// 添加&修改 表单提交
function submitForm() {
  proxy.$refs['formRef'].validate((valid) => {
    if (valid) {
      if (form.value.ticketNo != null) {
        backConsumableStorage(form.value).then((res) => {
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

//单元格式化
function formatter(row, cloumn) {
  return row.consumablePart + ' / ' + row.consumableName + ' / ' + row.spec
}

//监听属性变化
watch(props, (nevVal) => {
  ticketNo.value = props.ticketNo
  getList()
})

getList()
</script>
