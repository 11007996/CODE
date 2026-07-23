<!--
 * @Descripttion: (上线通知单操作/BU_Online_Notice_Ticket_Fixture_Handle)
 * @Author: (admin)
 * @Date: (2024-06-27)
-->
<template>
  <div>
    <el-form label-position="right" inline ref="queryRef">
      <el-form-item label="业务编号" prop="ticketNo">
        {{ ticketInfo.ticketNo }}
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
            :data="fixtureDemandList"
            :max-height="tableMaxHeight"
            v-loading="loading"
            ref="table"
            header-cell-class-name="el-table-header-cell"
            highlight-current-row>
            <el-table-column prop="fixtureName" label="治具名称" min-width="160" />
            <el-table-column prop="needQty" label="需求数量" width="90" align="center" />
            <el-table-column label="操作" width="90" align="center">
              <template #default="scope">
                <el-button type="primary" size="small" v-hasPermi="['fixture:storage:receive']" @click="handleReceive(scope.row)"> 领用 </el-button>
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
            :data="fixtureReceiveList"
            :max-height="tableMaxHeight"
            v-loading="loading"
            ref="table"
            header-cell-class-name="el-table-header-cell"
            highlight-current-row>
            <el-table-column prop="fixtureName" label="治具(名称/系列)" min-width="160">
              <template #default="scope"> {{ scope.row.fixtureName }} / {{ scope.row.series }} </template>
            </el-table-column>
            <el-table-column prop="receiveQty" label="领用数量" width="90" align="center" />
            <el-table-column prop="backQty" label="归还数量" width="90" align="center" />
            <el-table-column label="操作" width="90" align="center">
              <template #default="scope">
                <el-button
                  v-if="scope.row.receiveQty > scope.row.backQty"
                  type="primary"
                  size="small"
                  title="归还"
                  v-hasPermi="['fixture:storage:back']"
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
            <el-table-column prop="fixtureName" label="治具(名称/系列)" min-width="160">
              <template #default="scope"> {{ scope.row.fixtureName }} / {{ scope.row.series }} </template>
            </el-table-column>
            <el-table-column prop="changeQty" label="变动数量" width="90" align="center" />
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
        <el-form-item label="治具名称" prop="fixtureName">
          <el-input v-model="queryParams.fixtureName" placeholder="请输入治具名称" />
        </el-form-item>
        <el-form-item label="系列" prop="series">
          <el-input v-model="queryParams.series" placeholder="请输入系列名称" />
        </el-form-item>
        <el-form-item>
          <el-button icon="search" type="primary" @click="handleQuery">{{ $t('btn.search') }}</el-button>
          <el-button icon="refresh" @click="resetQuery">{{ $t('btn.reset') }}</el-button>
        </el-form-item>
      </el-form>
      <!-- 治具领用列表 -->
      <el-form ref="receiveFormRef" :model="receiveForm" label-width="100px">
        <el-table :data="fixtureList" @selection-change="handleSelectionChange">
          <el-table-column type="selection" width="40" align="center" />
          <el-table-column label="系列" min-width="100" align="center" prop="series" />
          <el-table-column label="治具名称" min-width="160" align="center" prop="fixtureName" />
          <el-table-column label="储位" min-width="200" align="center" prop="storageFullName" />
          <el-table-column label="闲置数量" width="90" align="center" prop="qty" />
          <el-table-column label="领用数量" width="90" align="center" prop="changeQty">
            <template #default="scope">
              <el-input-number v-model="scope.row.changeQty" :controls="false" class="fullWidth" />
            </template>
          </el-table-column>
        </el-table>
        <pagination :total="total" v-model:page="queryParams.pageNum" v-model:limit="queryParams.pageSize" @pagination="getFixtureList" />
      </el-form>

      <template #footer>
        <el-button text @click="cancel">{{ $t('btn.cancel') }}</el-button>
        <el-button type="primary" @click="submitReceiveForm">{{ $t('btn.submit') }}</el-button>
      </template>
    </el-dialog>

    <!-- 归还对话框-->
    <el-dialog :title="title" :lock-scroll="false" v-model="openBack">
      <el-form ref="backFormRef" :model="backForm" :rules="backRules" label-width="100px">
        <el-row :gutter="20">
          <el-col :lg="12">
            <el-form-item label="未还数量" prop="notBackQty">
              <el-input v-model="backForm.notBackQty" disabled />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="归还数量" prop="changeQty">
              <el-input-number v-model.number="backForm.changeQty" :controls="true" controls-position="right" placeholder="请输入数量" />
            </el-form-item>
          </el-col>

          <el-col :lg="24">
            <el-form-item label="储位" prop="storageId">
              <el-cascader
                class="w100"
                :options="useBasicStore().getFixtureStorageTree"
                :props="{ checkStrictly: true, value: 'storageId', label: 'storageName', emitPath: false }"
                placeholder="请选择储位"
                clearable
                v-model="backForm.storageId">
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
        <el-button text @click="cancelBack">{{ $t('btn.cancel') }}</el-button>
        <el-button type="primary" @click="submitBackForm">{{ $t('btn.submit') }}</el-button>
      </template>
    </el-dialog>
    <!-- <el-dialog :title="title" :lock-scroll="false" v-model="openBack" width="300">
      <el-form ref="backFormRef" :model="form" label-width="100px"> 确定要归还吗？ <el-input v-model="backForm.fixtureName" disabled /> </el-form>
      <template #footer>
        <el-button text @click="cancelBack">{{ $t('btn.cancel') }}</el-button>
        <el-button type="primary" @click="submitBackForm">{{ $t('btn.submit') }}</el-button>
      </template>
    </el-dialog> -->
  </div>
</template>

<script setup name="onlinenoticeticketfixturehandle">
import { getOnlineNoticeTicketFixtureSummary } from '@/api/business/onlineNoticeTicket.js'
import { batchReceiveFixtureStorage, backFixtureStorage, listFixtureStorage } from '@/api/fixture/fixtureStorage.js'
import useBasicStore from '@/store/modules/basic.js'

const props = defineProps({
  ticketNo: String
})
const { proxy } = getCurrentInstance()
const loading = ref(false)
const ticketInfo = ref({
  ticketNo: props.ticketNo,
  ticketType: null
})
const dataList = ref([])
const queryRef = ref()
const fixtureList = ref([])
const fixtureDemandList = ref([]) //需求清单
const fixtureReceiveList = ref([]) //已领清单
const storageRecordList = ref([]) //储存记录
const total = ref(0)
const tableMaxHeight = ref(500)

var dictParams = [{ dictType: 'fixture_status' }, { dictType: 'storage_change_type' }]

proxy.getDicts(dictParams).then((response) => {
  response.data.forEach((element) => {
    state.options[element.dictType] = element.list
  })
})

// 查询
function getList() {
  getOnlineNoticeTicketFixtureSummary(ticketInfo.value.ticketNo).then((res) => {
    const { code, data } = res
    if (code == 200) {
      ticketInfo.value.ticketNo = data.ticketNo
      ticketInfo.value.ticketType = data.ticketType
      fixtureDemandList.value = data.demandList
      fixtureReceiveList.value = data.receiveList
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
  fixtureName: undefined,
  series: undefined
})
const receiveFormRef = ref()
const title = ref('')
const open = ref(false)
const state = reactive({
  receiveForm: [],
  options: {
    // 治具状态
    fixture_status: [],
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
  getFixtureList()
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
const currFixtureName = ref('')
function handleReceive(row) {
  reset()
  open.value = true
  title.value = '上线通知单_治具领用'
  currFixtureName.value = row.fixtureName
  queryParams.fixtureName = row.fixtureName
  getFixtureList()
}

// 提交领用表单
function submitReceiveForm() {
  if (receiveForm.value.length > 0) {
    receiveForm.value.forEach((item) => {
      item.ticketNo = ticketInfo.value.ticketNo
      item.ticketType = ticketInfo.value.ticketType
    })
    batchReceiveFixtureStorage(receiveForm.value).then((res) => {
      const { code, data } = res
      if (code == 200) {
        proxy.$modal.msgSuccess('领用成功')
        open.value = false
        getList()
      }
    })
  } else {
    proxy.$modal.msgError('未勾选要领用治具')
  }
}

/*************** form操作 (归还)***************/
const openBack = ref(false)
const backRules = ref({
  changeQty: [{ required: true, message: '数量不能为空', trigger: 'blur' }],
  storageId: [{ required: true, message: '储位不能为空', trigger: 'blur' }]
})
const backForm = ref({
  fixtureId: null,
  fixtureName: null,
  notBackQty: 0,
  changeQty: 0,
  storageId: null
})

// 关闭dialog
function cancelBack() {
  openBack.value = false
  resetBack()
}

// 重置表单
function resetBack() {
  backForm.value = {
    fixtureId: null,
    fixtureName: null,
    notBackQty: 0,
    changeQty: 0,
    storageId: null,
    ticketNo: ticketInfo.value.ticketNo
  }
  proxy.resetForm('backFormRef')
}

// 归还按钮操作
function handleBack(row) {
  resetBack()
  backForm.value.fixtureId = row.fixtureId
  backForm.value.fixtureName = row.fixtureName
  backForm.value.notBackQty = row.receiveQty - row.backQty
  backForm.value.changeQty = row.receiveQty - row.backQty
  openBack.value = true
  title.value = '上线通知单_治具归还'
}

// 提交归还表单
function submitBackForm() {
  proxy.$refs['backFormRef'].validate((valid) => {
    if (valid) {
      if (backForm.value.fixtureId != null) {
        backFixtureStorage(backForm.value).then((res) => {
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

//获取闲置治具
function getFixtureList() {
  listFixtureStorage(queryParams).then((res) => {
    const { code, data } = res
    if (code == 200) {
      fixtureList.value = data.result
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
