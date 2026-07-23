<!--
 * @Descripttion: (上线通知单/BO_Online_Notice_Ticket)
 * @Author: (admin)
 * @Date: (2024-06-27)
-->
<template>
  <div>
    <el-form :model="queryParams" label-position="right" inline ref="queryRef" v-show="showSearch" @submit.prevent>
      <el-form-item label="业务编号" prop="ticketNo">
        <el-input v-model="queryParams.ticketNo" placeholder="请输入业务编号" />
      </el-form-item>
      <el-form-item label="流程编号" prop="processInstanceId">
        <el-input v-model="queryParams.processInstanceId" placeholder="请输入流程编号" />
      </el-form-item>
      <el-form-item label="产线" prop="lineId">
        <el-select v-model="queryParams.lineId" placeholder="请选择产线" filterable clear>
          <el-option
            v-for="item in useBasicStore().getLineDict"
            :key="item.dictValue"
            :label="item.dictLabel"
            :value="Number(item.dictValue)"></el-option>
        </el-select>
      </el-form-item>
      <el-form-item>
        <el-button icon="search" type="primary" @click="handleQuery">{{ $t('btn.search') }}</el-button>
        <el-button icon="refresh" @click="resetQuery">{{ $t('btn.reset') }}</el-button>
      </el-form-item>
    </el-form>
    <!-- 工具区域 -->
    <el-row :gutter="15" class="mb10">
      <!-- <el-col :span="1.5">
        <el-button type="primary" v-hasPermi="['onlineNoticeTicket:add']" plain icon="plus" @click="handleAdd">
          {{ $t('btn.add') }}
        </el-button>
      </el-col> -->
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
      <el-table-column align="center" width="90">
        <template #default="scope">
          <el-button text @click="rowClick(scope.row)">{{ $t('btn.details') }}</el-button>
        </template>
      </el-table-column>
      <el-table-column prop="ticketNo" label="业务编号" width="160" align="center" v-if="columns.showColumn('ticketNo')" />
      <el-table-column prop="initiatorId" label="发起人ID" width="90" align="center" v-if="columns.showColumn('initiatorId')" />
      <el-table-column prop="initiatorName" label="发起人" width="90" align="center" v-if="columns.showColumn('initiatorName')" />
      <el-table-column prop="lineId" label="产线ID" width="90" align="center" v-if="columns.showColumn('lineId')" />
      <el-table-column prop="lineName" label="产线" width="90" align="center" v-if="columns.showColumn('lineName')" />
      <el-table-column prop="productQty" label="产量" width="90" align="center" v-if="columns.showColumn('productQty')" />
      <el-table-column
        prop="needTime"
        label="需求时间"
        width="160"
        align="center"
        :show-overflow-tooltip="true"
        v-if="columns.showColumn('needTime')" />
      <el-table-column prop="status" label="业务状态" width="90" align="center" v-if="columns.showColumn('status')">
        <template #default="scope">
          <dict-tag :options="options.status_options" :value="scope.row.status" />
        </template>
      </el-table-column>
      <el-table-column prop="processInstanceId" label="流程编号" min-width="200" align="center" v-if="columns.showColumn('processInstanceId')">
        <template #default="scope">
          <el-link type="primary" @click="openProcess(scope.row)">{{ scope.row.processInstanceId }}</el-link>
        </template>
      </el-table-column>
      <el-table-column prop="createBy" label="创建人" width="90" align="center" v-if="columns.showColumn('createBy')" />
      <el-table-column prop="createTime" label="创建时间" width="160" v-if="columns.showColumn('createTime')" />
      <el-table-column label="操作" width="280">
        <template #default="scope">
          <el-button
            type="primary"
            size="small"
            icon="view"
            title="查看"
            v-hasPermi="['onlineNoticeTicket:query']"
            @click="handlePreview(scope.row)"></el-button>
          <el-button
            type="success"
            size="small"
            icon="edit"
            title="编辑"
            v-hasPermi="['onlineNoticeTicket:edit']"
            @click="handleUpdate(scope.row)"></el-button>
          <el-button
            type="warning"
            size="small"
            title="设备"
            v-hasPermi="['equipment:storage:receive', 'equipment:storage:back', 'equipment:storage:list']"
            @click="openEquipmentReceive(scope.row)"
            >设备</el-button
          >
          <el-button
            type="warning"
            size="small"
            title="治具"
            v-hasPermi="['fixture:storage:receive', 'fixture:storage:back', 'fixture:storage:list']"
            @click="openFixtureReceive(scope.row)"
            >治具</el-button
          >
          <el-button
            type="danger"
            size="small"
            icon="delete"
            title="删除"
            v-hasPermi="['onlineNoticeTicket:delete']"
            @click="handleDelete(scope.row)"></el-button>
          <!-- 设备操作 -->
          <!-- <el-dropdown
            style="margin-right: 12px"
            @command="(command) => handleCommand(command, scope.row)"
            v-hasPermi="['equipment:storage:receive', 'equipment:storage:back', 'equipment:storage:list']">
            <el-button type="warning" size="small">
              设备
              <el-icon class="el-icon--right"> <arrow-down /> </el-icon>
            </el-button>
            <template #dropdown>
              <el-dropdown-menu>
                <div v-hasPermi="['equipment:storage:receive']"><el-dropdown-item command="openEquipmentReceive">领用</el-dropdown-item></div>
                <div v-hasPermi="['equipment:storage:back']"><el-dropdown-item command="openEquipmentBack">归还</el-dropdown-item></div>
                <div v-hasPermi="['equipment:storage:list']"><el-dropdown-item command="openEquipmentRecord">记录</el-dropdown-item></div>
              </el-dropdown-menu>
            </template>
          </el-dropdown> -->
          <!-- 治具操作 -->
          <!-- <el-dropdown
            @command="(command) => handleCommand(command, scope.row)"
            v-hasPermi="['fixture:storage:receive', 'fixture:storage:back', 'fixture:storage:list']">
            <el-button type="warning" size="small">
              治具
              <el-icon class="el-icon--right"> <arrow-down /> </el-icon>
            </el-button>
            <template #dropdown>
              <el-dropdown-menu>
                <div v-hasPermi="['fixture:storage:receive']"><el-dropdown-item command="openFixtureReceive">领用</el-dropdown-item></div>
                <div v-hasPermi="['fixture:storage:back']"><el-dropdown-item command="openFixtureBack">归还</el-dropdown-item></div>
                <div v-hasPermi="['fixture:storage:list']"><el-dropdown-item command="openFixtureRecord">记录</el-dropdown-item></div>
              </el-dropdown-menu>
            </template>
          </el-dropdown> -->
        </template>
      </el-table-column>
    </el-table>
    <pagination :total="total" v-model:page="queryParams.pageNum" v-model:limit="queryParams.pageSize" @pagination="getList" />

    <!-- 右侧抽屉:业务详情 -->
    <el-drawer v-model="drawer" size="50%" direction="rtl">
      <el-divider content-position="center">上线通知单_设备需求清单信息</el-divider>
      <el-table :data="equipmentList" header-row-class-name="text-navy">
        <el-table-column prop="equipmentName" label="设备名称" min-width="160" />
        <el-table-column prop="needQty" label="需求数量" width="90" align="center" />
      </el-table>
      <el-divider content-position="center">上线通知单_治具需求清单信息</el-divider>
      <el-table :data="fixtureList" header-row-class-name="text-navy">
        <el-table-column prop="fixtureName" label="治具名称" min-width="160" />
        <el-table-column prop="needQty" label="需求数量" width="90" align="center" />
      </el-table>
    </el-drawer>

    <!-- 添加或修改上线通知单对话框 -->
    <el-dialog :title="title" :lock-scroll="false" v-model="open" :fullscreen="fullScreen">
      <el-form ref="formRef" :model="form" :rules="rules" label-width="100px">
        <el-row :gutter="20">
          <el-col :lg="12">
            <el-form-item label="业务编号" prop="ticketNo">
              <el-input v-model="form.ticketNo" placeholder="请输入业务编号" disabled />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="发起人" prop="initiatorId">
              <el-select
                v-model="form.initiatorId"
                placeholder="请选择发起人"
                clearable
                filterable
                remote
                :remote-method="handleQueryEmployee"
                @change="handleInitiatorChange"
                class="fullWidth">
                <el-option
                  v-for="item in options.emp_options"
                  :key="item.dictValue"
                  :label="item.dictValue + ' - ' + item.dictLabel"
                  :value="item.dictValue"></el-option>
              </el-select>
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="料号" prop="partId">
              <el-select
                v-model="form.partId"
                placeholder="请选择料号"
                clearable
                filterable
                remote
                :remote-method="handleQueryPart"
                @change="handlePartChange"
                class="fullWidth">
                <el-option
                  v-for="item in options.part_options"
                  :key="item.dictValue"
                  :label="item.dictLabel"
                  :value="Number(item.dictValue)"></el-option>
              </el-select>
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="产量" prop="productQty">
              <el-input-number v-model.number="form.productQty" controls-position="right" placeholder="请输入产量" class="fullWidth" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="产线" prop="lineId">
              <el-select v-model="form.lineId" placeholder="请选择产线" clearable filterable class="fullWidth">
                <el-option
                  v-for="item in useBasicStore().getLineDict"
                  :key="item.dictValue"
                  :label="item.dictLabel"
                  :value="Number(item.dictValue)"></el-option>
              </el-select>
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="需求时间" prop="needTime">
              <el-date-picker v-model="form.needTime" type="datetime" placeholder="选择日期时间" class="fullWidth"></el-date-picker>
            </el-form-item>
          </el-col>
        </el-row>

        <!-- 全屏 -->
        <el-row :gutter="10" class="mb8">
          <el-col :span="1.5">
            <el-button type="info" icon="FullScreen" @click="fullScreen = !fullScreen">{{ fullScreen ? '退出全屏' : '全屏' }}</el-button>
          </el-col>
        </el-row>

        <!-- 设备清单 -->
        <el-divider content-position="center">上线通知单_设备需求清单信息</el-divider>
        <el-row :gutter="10" class="mb8">
          <el-col :span="1.5" v-if="opertype != 3">
            <el-button type="primary" icon="Plus" @click="handleAddEquipment">添加</el-button>
          </el-col>
          <el-col :span="1.5" v-if="opertype != 3">
            <el-button type="danger" icon="Delete" @click="handleDeleteEquipment">删除</el-button>
          </el-col>
        </el-row>
        <el-table :data="equipmentList" :row-class-name="rowEquipmentIndex" @selection-change="handleEquipmentSelectionChange" ref="EquipmentRef">
          <el-table-column type="selection" width="40" align="center" />
          <el-table-column label="设备名称" min-width="160" align="center" prop="equipmentName">
            <template #default="scope">
              <el-select
                v-model="scope.row.equipmentName"
                placeholder="请选择设备"
                clearable
                filterable
                allow-create
                remote
                :remote-method="handleQueryEquipment"
                class="fullWidth">
                <el-option v-for="item in options.equipment_options" :label="item.dictLabel" :value="item.dictValue"></el-option>
              </el-select>
            </template>
          </el-table-column>
          <el-table-column label="需求数量" width="90" align="center" prop="needQty">
            <template #default="scope">
              <el-input-number v-model="scope.row.needQty" :controls="false" placeholder="请输入需求数量" class="fullWidth" />
            </template>
          </el-table-column>
        </el-table>

        <!-- 治具清单 -->
        <el-divider content-position="center">上线通知单_治具需求清单信息</el-divider>
        <el-row :gutter="10" class="mb8">
          <el-col :span="1.5" v-if="opertype != 3">
            <el-button type="primary" icon="Plus" @click="handleAddFixture">添加</el-button>
          </el-col>
          <el-col :span="1.5" v-if="opertype != 3">
            <el-button type="danger" icon="Delete" @click="handleDeleteFixture">删除</el-button>
          </el-col>
        </el-row>
        <el-table :data="fixtureList" :row-class-name="rowFixtureIndex" @selection-change="handleSelectionChange" ref="FixtureRef">
          <el-table-column type="selection" width="40" align="center" />
          <el-table-column label="治具名称" min-width="160" align="center" prop="fixtureName">
            <template #default="scope">
              <el-select
                v-model="scope.row.fixtureName"
                placeholder="请输入治具名称"
                clearable
                filterable
                allow-create
                remote
                :remote-method="handleQueryFixture"
                class="fullWidth">
                <template #header>
                  <span>系列 / 名称</span>
                </template>
                <el-option v-for="item in options.fixture_options" :label="item.dictLabel" :value="item.dictLabel"></el-option>
              </el-select>
            </template>
          </el-table-column>
          <el-table-column label="需求数量" width="90" align="center" prop="needQty">
            <template #default="scope">
              <el-input-number v-model="scope.row.needQty" :controls="false" placeholder="请输入需求数量" class="fullWidth" />
            </template>
          </el-table-column>
        </el-table>
      </el-form>

      <template #footer v-if="opertype != 3">
        <el-button text @click="cancel">{{ $t('btn.cancel') }}</el-button>
        <el-button type="primary" @click="submitForm">{{ $t('btn.submit') }}</el-button>
      </template>
    </el-dialog>

    <el-dialog :title="title" :lock-scroll="false" v-model="openOper" width="90%" v-if="openOper" :fullscreen="true">
      <!-- 设备领用 -->
      <OnlineNoticeTicketEquipmentHandle :ticketNo="currTicketNo" v-if="opertype == 4"></OnlineNoticeTicketEquipmentHandle>
      <!-- 设备归还 -->
      <!-- <OnlineNoticeTicketEquipmentBack :ticketNo="currTicketNo" v-if="opertype == 5"></OnlineNoticeTicketEquipmentBack> -->
      <!-- 治具记录 -->
      <!-- <EquipmentStorageRecord :ticketNo="currTicketNo" v-if="opertype == 6"></EquipmentStorageRecord> -->
      <!-- 治具领用 -->
      <OnlineNoticeTicketFixtureHandle :ticketNo="currTicketNo" v-if="opertype == 7"></OnlineNoticeTicketFixtureHandle>
      <!-- 治具归还 -->
      <!-- <OnlineNoticeTicketFixtureBack :ticketNo="currTicketNo" v-if="opertype == 8"></OnlineNoticeTicketFixtureBack> -->
      <!-- 治具记录 -->
      <!-- <FixtureStorageRecord :ticketNo="currTicketNo" v-if="opertype == 9" /> -->
    </el-dialog>
  </div>
</template>

<script setup name="onlinenoticeticket">
import {
  listOnlineNoticeTicket,
  addOnlineNoticeTicket,
  delOnlineNoticeTicket,
  updateOnlineNoticeTicket,
  getOnlineNoticeTicket
} from '@/api/business/onlineNoticeTicket.js'
import { dictPart } from '@/api/basic/part.js'
import { idleEquipmentBase } from '@/api/equipment/equipmentBase.js'
import { dictFixtureBase, idleFixtureBase } from '@/api/fixture/fixtureBase.js'
import { dictEmployee } from '@/api/basic/employee.js'
import useBasicStore from '@/store/modules/basic.js'
import OnlineNoticeTicketEquipmentReceive from './OnlineNoticeTicketEquipmentReceive.vue'
import OnlineNoticeTicketEquipmentHandle from './OnlineNoticeTicketEquipmentHandle.vue'
import OnlineNoticeTicketEquipmentBack from './OnlineNoticeTicketEquipmentBack.vue'
import OnlineNoticeTicketFixtureReceive from './OnlineNoticeTicketFixtureReceive.vue'
import OnlineNoticeTicketFixtureHandle from './OnlineNoticeTicketFixtureHandle.vue'
import OnlineNoticeTicketFixtureBack from './OnlineNoticeTicketFixtureBack.vue'
import EquipmentStorageRecord from '@/views/equipment/EquipmentStorageRecord.vue'
import FixtureStorageRecord from '@/views/fixture/FixtureStorageRecord.vue'

const { proxy } = getCurrentInstance()
const router = useRouter()
const ids = ref([])
const loading = ref(false)
const showSearch = ref(true)
const queryParams = reactive({
  pageNum: 1,
  pageSize: 10,
  sort: 'createtime',
  sortType: 'desc',
  ticketNo: undefined,
  processInstanceId: undefined
})
const columns = ref([
  { visible: true, prop: 'ticketNo', label: '业务编号' },
  { visible: false, prop: 'initiatorId', label: '发起人ID' },
  { visible: true, prop: 'initiatorName', label: '发起人' },
  { visible: false, prop: 'lineId', label: '产线ID' },
  { visible: true, prop: 'lineName', label: '产线' },
  { visible: true, prop: 'productQty', label: '产量' },
  { visible: true, prop: 'needTime', label: '需求时间' },
  { visible: false, prop: 'status', label: '业务状态' },
  { visible: true, prop: 'processInstanceId', label: '流程编号' },
  { visible: false, prop: 'createBy', label: '创建人' },
  { visible: true, prop: 'createTime', label: '创建时间' }
])
const total = ref(0)
const dataList = ref([])
const queryRef = ref()
const defaultTime = ref([new Date(2000, 1, 1, 0, 0, 0), new Date(2000, 2, 1, 23, 59, 59)])
const fullScreen = ref(false)
const drawer = ref(false)

var dictParams = [{ dictType: 'storage_change_type' }]
//获取字典数据
proxy.getDicts(dictParams).then((response) => {
  response.data.forEach((element) => {
    state.options[element.dictType] = element.list
  })
})

function getList() {
  loading.value = true
  listOnlineNoticeTicket(queryParams).then((res) => {
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

//查看流程
function openProcess(row) {
  const instanceId = row.processInstanceId
  const processId = row.processInstanceId.split('-')[0]
  //使用resolve
  const url = router.resolve({
    path: '/process/' + processId,
    query: { instanceId: instanceId }
  })
  window.open(url.href, '_blank')
}
/*************** form操作 ***************/
const formRef = ref()
const title = ref('')
// 操作类型 1、add 2、edit 3、view ,4.设备领用，5.设备归还，6.设备记录，7.治具领用，8.治具归还，9.治具详情
const opertype = ref(0)
const open = ref(false)
const state = reactive({
  single: true,
  multiple: true,
  form: {},
  rules: {
    // ticketNo: [{ required: true, message: '业务编号不能为空', trigger: 'blur' }],
    initiatorId: [{ required: true, message: '发起人不能为空', trigger: 'blur' }],
    partId: [{ required: true, message: '料号不能为空', trigger: 'blur' }],
    lineId: [{ required: true, message: '产线不能为空', trigger: 'blur' }],
    productQty: [{ required: true, message: '产量不能为空', trigger: 'blur' }]
  },
  options: {
    storage_change_type: [],
    // 业务状态 选项列表 格式 eg:{ dictLabel: '标签', dictValue: '0'}
    status_options: [],
    //员工选项
    emp_options: [],
    //料号选项
    part_options: [],
    //设备选项
    equipment_options: [],
    //治具选项
    fixture_options: []
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
    ticketNo: null,
    initiatorId: null,
    initiatorName: null,
    partId: null,
    productQty: null,
    lineId: null,
    needTime: null,
    status: null,
    processInstanceId: null,
    createBy: null,
    createTime: null
  }
  equipmentList.value = []
  fixtureList.value = []
  state.options.equipment_options = []
  proxy.resetForm('formRef')
}

// 添加按钮操作
function handleAdd() {
  reset()
  open.value = true
  title.value = '添加上线通知单'
  opertype.value = 1
}
// 修改按钮操作
function handleUpdate(row) {
  reset()
  const id = row.ticketNo || ids.value
  getOnlineNoticeTicket(id).then((res) => {
    const { code, data } = res
    if (code == 200) {
      open.value = true
      title.value = '修改上线通知单'
      opertype.value = 2

      form.value = {
        ...data
      }

      //设备清单
      equipmentList.value = res.data.equipmentNav
      state.options.equipment_options = []
      res.data.equipmentNav.forEach((item) => {
        state.options.equipment_options.push({ dictValue: item.equipmentName, dictLabel: item.equipmentName })
      })
      //治具清单
      fixtureList.value = res.data.fixtureNav

      //审批人
      state.options.emp_options = []
      state.options.emp_options.push({ dictValue: res.data.initiatorId, dictLabel: res.data.initiatorName })
      //料号选项
      if (form.value.partId) {
        const queryPartParams = {
          pageNum: 1,
          pageSize: 10,
          sort: '',
          sortType: 'asc',
          partId: form.value.partId
        }
        dictPart(queryPartParams).then((res) => {
          state.options.part_options = res.data.result
        })
      }
    }
  })
}

// 修改按钮操作
function handlePreview(row) {
  reset()
  const id = row.ticketNo || ids.value
  getOnlineNoticeTicket(id).then((res) => {
    const { code, data } = res
    if (code == 200) {
      open.value = true
      title.value = '查看上线通知单'
      opertype.value = 3

      form.value = { ...data }
      //设备清单
      equipmentList.value = res.data.equipmentNav
      state.options.equipment_options = []
      res.data.equipmentNav.forEach((item) => {
        state.options.equipment_options.push({ dictValue: item.equipmentName, dictLabel: item.equipmentName })
      })
      //治具清单
      fixtureList.value = res.data.fixtureNav

      //审批人
      state.options.emp_options = []
      state.options.emp_options.push({ dictValue: res.data.initiatorId, dictLabel: res.data.initiatorName })
      //料号选项
      if (form.value.partId) {
        const queryPartParams = {
          pageNum: 1,
          pageSize: 10,
          sort: '',
          sortType: 'asc',
          partId: form.value.partId
        }
        dictPart(queryPartParams).then((res) => {
          state.options.part_options = res.data.result
        })
      }
    }
  })
}

// 添加&修改 表单提交
function submitForm() {
  proxy.$refs['formRef'].validate((valid) => {
    if (valid) {
      form.value.equipmentNav = equipmentList.value
      form.value.fixtureNav = fixtureList.value
      if (form.value.ticketNo != undefined && opertype.value === 2) {
        updateOnlineNoticeTicket(form.value).then((res) => {
          proxy.$modal.msgSuccess('修改成功')
          open.value = false
          getList()
        })
      } else {
        addOnlineNoticeTicket(form.value).then((res) => {
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
  const Ids = row.ticketNo || ids.value

  proxy
    .$confirm('是否确认删除参数编号为"' + Ids + '"的数据项？', '警告', {
      confirmButtonText: proxy.$t('common.ok'),
      cancelButtonText: proxy.$t('common.cancel'),
      type: 'warning'
    })
    .then(function () {
      return delOnlineNoticeTicket(Ids)
    })
    .then(() => {
      getList()
      proxy.$modal.msgSuccess('删除成功')
    })
}

/** 行单击事件 */
function rowClick(row) {
  const id = row.ticketNo || ids.value
  getOnlineNoticeTicket(id).then((res) => {
    const { code, data } = res
    if (code == 200) {
      drawer.value = true
      fixtureList.value = data.fixtureNav
      equipmentList.value = data.equipmentNav
    }
  })
}

/*********************上线通知单_设备需求清单子表信息*************************/
const equipmentList = ref([])
const checkedEquipment = ref([])

/** 上线通知单_治具需求清单序号 */
function rowEquipmentIndex({ row, rowIndex }) {
  row.index = rowIndex + 1
}

/** 上线通知单_治具需求清单添加按钮操作 */
function handleAddEquipment() {
  let obj = {}
  equipmentList.value.push(obj)
}

/** 复选框选中数据 */
function handleEquipmentSelectionChange(selection) {
  checkedEquipment.value = selection.map((item) => item.index)
}

/** 上线通知单_治具需求清单删除按钮操作 */
function handleDeleteEquipment() {
  if (checkedEquipment.value.length == 0) {
    proxy.$modal.msgError('请先选择要删除的上线通知单_设备需求清单数据')
  } else {
    equipmentList.value = equipmentList.value.filter(function (item) {
      return checkedEquipment.value.indexOf(item.index) == -1
    })
  }
}

/** 上线通知单_治具需求清单治具查询 */
function handleQueryEquipment(keyword) {
  if (keyword) {
    const query = {
      equipmentName: keyword
    }
    setTimeout(() => {
      idleEquipmentBase(query).then((res) => {
        if (res.data) {
          let eo = []
          res.data.result.forEach((item) => {
            eo.push({ dictValue: item.equipmentName, dictLabel: item.equipmentName })
          })
          state.options.equipment_options = eo
        }
      })
    }, 200)
  }
}
/*********************上线通知单_治具需求清单子表信息*************************/
const fixtureList = ref([])
const checkedFixture = ref([])

/** 上线通知单_治具需求清单治具查询 */
function handleQueryFixture(keyword) {
  if (keyword) {
    const query = {
      pageNum: 1,
      pageSize: 10,
      sort: '',
      sortType: 'asc',
      keyword: keyword
    }
    if (keyword.indexOf(',') >= 0) {
      const kv = keyword.split(',')
      query.series = kv[0]
      query.fixtureName = kv[1]
      query.keyword = null
    }
    setTimeout(() => {
      dictFixtureBase(query).then((res) => {
        if (res.data) {
          state.options.fixture_options = res.data.result
        }
      })
    }, 200)
  }
}

/** 上线通知单_治具需求清单序号 */
function rowFixtureIndex({ row, rowIndex }) {
  row.index = rowIndex + 1
}

/** 上线通知单_治具需求清单添加按钮操作 */
function handleAddFixture() {
  let obj = {}
  //下面的代码自己设置默认值
  //obj.needQty = null;
  fixtureList.value.push(obj)
}

/** 复选框选中数据 */
function handleSelectionChange(selection) {
  checkedFixture.value = selection.map((item) => item.index)
}

/** 上线通知单_治具需求清单删除按钮操作 */
function handleDeleteFixture() {
  if (checkedFixture.value.length == 0) {
    proxy.$modal.msgError('请先选择要删除的上线通知单_治具需求清单数据')
  } else {
    fixtureList.value = fixtureList.value.filter(function (item) {
      return checkedFixture.value.indexOf(item.index) == -1
    })
  }
}

//********************************其他方法************************* */
//员工查询
function handleQueryEmployee(keyword) {
  if (keyword) {
    const query = {
      pageNum: 1,
      pageSize: 10,
      sort: '',
      sortType: 'asc',
      empName: keyword
    }
    setTimeout(() => {
      dictEmployee(query).then((res) => {
        state.options.emp_options = res.data.result
      })
    }, 200)
  }
}

//料号查询
function handleQueryPart(keyword) {
  if (keyword) {
    const queryPartParams = {
      pageNum: 1,
      pageSize: 10,
      sort: '',
      sortType: 'asc',
      partNo: keyword
    }
    setTimeout(() => {
      dictPart(queryPartParams).then((res) => {
        state.options.part_options = res.data.result
      })
    }, 200)
  }
}

//料号变更事件，将治具带出来
function handlePartChange(partId) {
  if (partId) {
    const query = {
      pageNum: 1,
      pageSize: 10,
      sort: '',
      sortType: 'asc',
      fixtureName: null,
      partId: partId
    }
    setTimeout(() => {
      idleFixtureBase(query).then((res) => {
        if (res.data) {
          const fixtures = []
          res.data.result.forEach((item) => {
            fixtures.push({
              fixtureName: item.fixtureName,
              needQty: item.defaultQty
            })
          })
          if (!fixtureList || fixtureList.value.length <= 0) fixtureList.value = fixtures
        }
      })
    }, 200)
  }
}

//发起人变更
function handleInitiatorChange(value) {
  // 在这里根据value查找对应的label
  const selectedOption = state.options.emp_options.find((item) => item.dictValue === value)
  if (selectedOption) {
    form.value.initiatorName = selectedOption.dictLabel // 更新显示的标签
  }
}

// 更多操作触发
function handleCommand(command, row) {
  switch (command) {
    case 'openEquipmentReceive':
      openEquipmentReceive(row)
      break
    case 'openEquipmentBack':
      openEquipmentBack(row)
      break
    case 'openEquipmentRecord':
      openEquipmentRecord(row)
      break
    case 'openFixtureReceive':
      openFixtureReceive(row)
      break
    case 'openFixtureBack':
      openFixtureBack(row)
      break
    case 'openFixtureRecord':
      openFixtureRecord(row)
      break
    default:
      break
  }
}

const openOper = ref(false)
const currTicketNo = ref('')
/***********************设备相关****************************/
//领用
function openEquipmentReceive(row) {
  currTicketNo.value = row.ticketNo
  title.value = '上线通知单_设备需求清单'
  opertype.value = 4
  openOper.value = true
}

//归还
function openEquipmentBack(row) {
  currTicketNo.value = row.ticketNo
  title.value = '上线通知单_设备未还清单'
  opertype.value = 5
  openOper.value = true
}

//记录
function openEquipmentRecord(row) {
  title.value = '上线通知单_设备操作记录'
  currTicketNo.value = row.ticketNo
  opertype.value = 6
  openOper.value = true
}

/***********************治具相关****************************/
//领用
function openFixtureReceive(row) {
  currTicketNo.value = row.ticketNo
  title.value = '上线通知单_治具需求清单'
  opertype.value = 7
  openOper.value = true
}

//归还
function openFixtureBack(row) {
  currTicketNo.value = row.ticketNo
  title.value = '上线通知单_治具未还清单'
  opertype.value = 8
  openOper.value = true
}

//记录
function openFixtureRecord(row) {
  title.value = '上线通知单_治具操作记录'
  opertype.value = 9
  openOper.value = true
  currTicketNo.value = row.ticketNo
}

handleQuery()
</script>
