<!--
 * @Descripttion: (设备保管/EQU_Equipment_Storage_Using)
 * @Author: (admin)
 * @Date: (2024-12-04)
-->
<template>
  <div>
    <el-form :model="queryParams" label-position="right" inline ref="queryRef" v-show="showSearch" @submit.prevent>
      <el-form-item label="设备" prop="equipmentId">
        <el-select
          v-model="queryParams.equipmentId"
          placeholder="资产编号/设备名称/资产名称/自定义机型"
          clearable
          filterable
          remote
          :remote-method="handleQueryEquipment">
          <el-option v-for="item in options.equipment_options" :key="item.dictValue" :label="item.dictLabel" :value="item.dictValue"></el-option>
        </el-select>
      </el-form-item>
      <el-form-item label="产线" prop="lineId">
        <el-select v-model="queryParams.lineId" clearable filterable placeholder="请选择线别ID">
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
      <el-col :span="1.5">
        <el-button type="primary" v-hasPermi="['equipment:storage:receive']" plain icon="plus" @click="handleReceive"> 领用 </el-button>
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
      <el-table-column prop="equipmentId" label="设备Id" align="center" v-if="columns.showColumn('equipmentId')" />
      <el-table-column prop="assetNo" label="资产编号" align="center" width="210" v-if="columns.showColumn('assetNo')" />
      <el-table-column prop="assetName" label="资产名称" align="center" min-width="200" v-if="columns.showColumn('assetName')" />
      <el-table-column prop="lineId" label="产线Id" align="center" width="90" v-if="columns.showColumn('lineId')" />
      <el-table-column prop="lineName" label="产线" align="center" width="90" v-if="columns.showColumn('lineName')" />
      <el-table-column prop="receiverId" label="领用人Id" align="center" width="90" v-if="columns.showColumn('receiverId')" />
      <el-table-column prop="receiverName" label="领用人" align="center" width="90" v-if="columns.showColumn('receiverName')" />
      <el-table-column prop="storageChangeType" label="变动类型" align="center" width="90" v-if="columns.showColumn('storageChangeType')">
        <template #default="scope">
          <dict-tag :options="options.storage_change_type" :value="scope.row.storageChangeType" />
        </template>
      </el-table-column>
      <el-table-column
        prop="ticketNo"
        label="业务编号"
        align="center"
        width="160"
        :show-overflow-tooltip="true"
        v-if="columns.showColumn('ticketNo')" />
      <el-table-column prop="ticketType" label="单据类型" align="center" v-if="columns.showColumn('ticketType')">
        <template #default="scope">
          <dict-tag :options="options.ticket_type" :value="scope.row.ticketType" />
        </template>
      </el-table-column>
      <el-table-column prop="createTime" label="创建时间" align="center" width="160" v-if="columns.showColumn('createTime')" />
      <el-table-column prop="createBy" label="创建人" align="center" width="90" v-if="columns.showColumn('createBy')" />
      <el-table-column prop="remark" label="备注" :show-overflow-tooltip="true" v-if="columns.showColumn('remark')" />
      <el-table-column label="操作" align="center" width="90">
        <template #default="scope">
          <el-button type="primary" size="small" v-hasPermi="['equipment:storage:back']" @click="handleUpdate(scope.row)">归还</el-button>
        </template>
      </el-table-column>
    </el-table>
    <pagination :total="total" v-model:page="queryParams.pageNum" v-model:limit="queryParams.pageSize" @pagination="getList" />

    <!-- 添加或修改设备保管对话框 -->
    <el-dialog :title="title" :lock-scroll="false" v-model="open">
      <el-form ref="formRef" :model="form" :rules="rules" label-width="100px">
        <el-row :gutter="20">
          <el-col :lg="12">
            <el-form-item label="设备" prop="equipmentId">
              <el-select
                v-model="form.equipmentId"
                placeholder="资产编号/设备名称/资产名称/自定义机型"
                clearable
                filterable
                remote
                :remote-method="handleQueryEquipment"
                :disabled="opertype != 1"
                class="fullWidth">
                <el-option
                  v-for="item in options.equipment_options"
                  :key="item.dictValue"
                  :label="item.dictLabel"
                  :value="item.dictValue"></el-option>
              </el-select>
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="产线" prop="lineId">
              <el-select v-model="form.lineId" clearable filterable placeholder="请选择线别ID">
                <el-option
                  v-for="item in useBasicStore().getLineDict"
                  :key="item.dictValue"
                  :label="item.dictLabel"
                  :value="Number(item.dictValue)"></el-option>
              </el-select>
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="领用人" prop="receiverId">
              <el-select
                v-model="form.receiverId"
                placeholder="请选择领用人"
                clearable
                filterable
                remote
                :remote-method="handleQueryEmployee"
                class="fullWidth">
                <el-option
                  v-for="item in options.emp_options"
                  :key="item.dictValue"
                  :label="item.dictValue + ' - ' + item.dictLabel"
                  :value="item.dictValue"></el-option>
              </el-select>
            </el-form-item>
          </el-col>
        </el-row>
      </el-form>
      <template #footer v-if="opertype != 3">
        <el-button text @click="cancel">{{ $t('btn.cancel') }}</el-button>
        <el-button type="primary" @click="submitForm">{{ $t('btn.submit') }}</el-button>
      </template>
    </el-dialog>
  </div>
</template>

<script setup name="equipmentstorageusing">
import {
  listEquipmentStorage,
  addEquipmentStorage,
  delEquipmentStorage,
  updateEquipmentStorage,
  getEquipmentStorage,
  receiveEquipmentStorage,
  backEquipmentStorage
} from '@/api/equipment/equipmentStorage.js'
import { dictEquipmentBase } from '@/api/equipment/equipmentBase.js'
import useBasicStore from '@/store/modules/basic.js'
import { dictEmployee } from '@/api/basic/employee.js'

const { proxy } = getCurrentInstance()
const ids = ref([])
const loading = ref(false)
const showSearch = ref(true)
const queryParams = reactive({
  pageNum: 1,
  pageSize: 10,
  sort: '',
  sortType: 'asc'
})
const columns = ref([
  { visible: false, prop: 'equipmentId', label: '设备ID' },
  { visible: true, prop: 'assetNo', label: '资产编号' },
  { visible: true, prop: 'assetName', label: '资产名称' },
  { visible: false, prop: 'lineId', label: '产线Id' },
  { visible: true, prop: 'lineName', label: '产线' },
  { visible: false, prop: 'receiverId', label: '领用人Id' },
  { visible: true, prop: 'receiverName', label: '领用人' },
  { visible: false, prop: 'storageChangeType', label: '变动类型' },
  { visible: true, prop: 'ticketNo', label: '业务编号' },
  { visible: true, prop: 'ticketType', label: '单据类型' },
  { visible: true, prop: 'createTime', label: '创建时间' },
  { visible: false, prop: 'createBy', label: '创建人' },
  { visible: false, prop: 'remark', label: '备注' }
])
const total = ref(0)
const dataList = ref([])
const queryRef = ref()
const defaultTime = ref([new Date(2000, 1, 1, 0, 0, 0), new Date(2000, 2, 1, 23, 59, 59)])

var dictParams = [{ dictType: 'storage_change_type' }, { dictType: 'ticket_type' }]

proxy.getDicts(dictParams).then((response) => {
  response.data.forEach((element) => {
    state.options[element.dictType] = element.list
  })
})

function getList() {
  loading.value = true
  listEquipmentStorage(queryParams).then((res) => {
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
// 操作类型 1、领用 2、归还
const opertype = ref(0)
const open = ref(false)
const state = reactive({
  single: true,
  multiple: true,
  form: {},
  rules: {
    equipmentId: [{ required: true, message: '设备不能为空', trigger: 'blur' }]
  },
  options: {
    // 变动方式
    storage_change_type: [],
    // 单据类型
    ticket_type: [],
    // 设备选项
    equipment_options: [],
    // 员工选项
    emp_options: []
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
    equipmentId: null,
    lineId: null,
    receiverId: null
  }
  proxy.resetForm('formRef')
}

// 添加按钮操作
function handleReceive() {
  reset()
  open.value = true
  title.value = '设备领用'
  opertype.value = 1
}
// 归还按钮操作
function handleUpdate(row) {
  reset()
  const equipmentId = row.equipmentId
  getEquipmentStorage(equipmentId).then((res) => {
    const { code, data } = res
    if (code == 200) {
      open.value = true
      title.value = '设备归还'
      opertype.value = 2

      form.value = {
        ...data
      }

      options.value.emp_options = [{ dictValue: form.value.receiverId, dictLabel: form.value.receiverName }]
      options.value.equipment_options = [{ dictValue: form.value.equipmentId, dictLabel: form.value.assetNo + ' : ' + form.value.assetName }]
    }
  })
}

// 添加&修改 表单提交
function submitForm() {
  proxy.$refs['formRef'].validate((valid) => {
    if (valid) {
      if (form.value.equipmentId != undefined && opertype.value === 2) {
        backEquipmentStorage(form.value).then((res) => {
          proxy.$modal.msgSuccess('归还成功')
          open.value = false
          getList()
        })
      } else {
        receiveEquipmentStorage(form.value).then((res) => {
          proxy.$modal.msgSuccess('领用成功')
          open.value = false
          getList()
        })
      }
    }
  })
}

// 删除按钮操作
function handleDelete(row) {
  const Ids = row.equipmentId || ids.value

  proxy
    .$confirm('是否确认删除参数编号为"' + Ids + '"的数据项？', '警告', {
      confirmButtonText: proxy.$t('common.ok'),
      cancelButtonText: proxy.$t('common.cancel'),
      type: 'warning'
    })
    .then(function () {
      return delEquipmentStorage(Ids)
    })
    .then(() => {
      getList()
      proxy.$modal.msgSuccess('删除成功')
    })
}

//----------------- 基他方法 -------------------

// 查询资产编号
function handleQueryEquipment(keyword) {
  if (keyword) {
    const params = {
      pageNum: 1,
      pageSize: 10,
      sort: '',
      sortType: 'asc',
      assetNo: kvs[0],
      keyword: keyword
    }
    setTimeout(() => {
      dictEquipmentBase(params).then((res) => {
        state.options.equipment_options = res.data.result
      })
    }, 200)
  }
}

//员工查询
function handleQueryEmployee(keyword) {
  if (keyword) {
    const query = {
      pageNum: 1,
      pageSize: 10,
      sortType: 'desc',
      empName: keyword
    }
    setTimeout(() => {
      dictEmployee(query).then((res) => {
        state.options.emp_options = res.data.result
      })
    }, 200)
  }
}

handleQuery()
</script>
