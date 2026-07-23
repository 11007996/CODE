<!--
 * @Descripttion: (设备运行数据/EQU_Equipment_Runing_Record)
 * @Author: (admin)
 * @Date: (2024-12-09)
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
      <el-form-item label="运行状态" prop="runState">
        <el-input v-model="queryParams.runState" placeholder="请输入运行状态" />
      </el-form-item>
      <el-form-item label="创建时间">
        <el-date-picker
          v-model="dateRangeCreateTime"
          type="datetimerange"
          start-placeholder="开始日期"
          end-placeholder="结束日期"
          value-format="YYYY-MM-DD HH:mm:ss"
          :default-time="defaultTime"
          :shortcuts="dateOptions">
        </el-date-picker>
      </el-form-item>
      <el-form-item>
        <el-button icon="search" type="primary" @click="handleQuery">{{ $t('btn.search') }}</el-button>
        <el-button icon="refresh" @click="resetQuery">{{ $t('btn.reset') }}</el-button>
      </el-form-item>
    </el-form>
    <!-- 工具区域 -->
    <el-row :gutter="15" class="mb10">
      <el-col :span="1.5">
        <el-button type="primary" v-hasPermi="['equipment:runing:record:add']" plain icon="plus" @click="handleAdd">
          {{ $t('btn.add') }}
        </el-button>
      </el-col>
      <el-col :span="1.5">
        <el-button type="warning" plain icon="download" @click="handleExport" v-hasPermi="['equipment:runing:record:export']">
          {{ $t('btn.export') }}
        </el-button>
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
      <el-table-column prop="id" label="ID" align="center" width="150" v-if="columns.showColumn('id')" />
      <el-table-column prop="equipmentId" label="设备Id" align="center" width="90" v-if="columns.showColumn('equipmentId')" />
      <el-table-column prop="equipmentName" label="设备名称" align="center" min-width="150" v-if="columns.showColumn('equipmentName')" />
      <el-table-column prop="assetNo" label="资产编号" align="center" width="210" v-if="columns.showColumn('assetNo')" />
      <el-table-column prop="assetName" label="资产名称" align="center" min-width="150" v-if="columns.showColumn('assetName')" />
      <el-table-column prop="runState" label="运行状态" align="center" width="90" v-if="columns.showColumn('runState')" />
      <el-table-column prop="productCount" label="产能数量" align="center" width="90" v-if="columns.showColumn('productCount')" />
      <el-table-column prop="defectCount" label="不良数量" align="center" width="90" v-if="columns.showColumn('defectCount')" />
      <el-table-column prop="warnState" label="报警状态" align="center" width="90" v-if="columns.showColumn('warnState')" />
      <el-table-column prop="warnCode" label="报警代码" align="center" width="90" v-if="columns.showColumn('warnCode')" />
      <el-table-column prop="createTime" label="创建时间" align="center" width="160" v-if="columns.showColumn('createTime')" />
      <el-table-column label="操作" align="center" width="90">
        <template #default="scope">
          <el-button
            type="danger"
            size="small"
            icon="delete"
            title="删除"
            v-hasPermi="['equipment:runing:record:delete']"
            @click="handleDelete(scope.row)"></el-button>
        </template>
      </el-table-column>
    </el-table>
    <pagination :total="total" v-model:page="queryParams.pageNum" v-model:limit="queryParams.pageSize" @pagination="getList" />

    <!-- 添加或修改设备运行数据对话框 -->
    <el-dialog :title="title" :lock-scroll="false" v-model="open">
      <el-form ref="formRef" :model="form" :rules="rules" label-width="100px">
        <el-row :gutter="20">
          <el-col :lg="12">
            <el-form-item label="设备编码" prop="equipmentId">
              <el-input v-model.number="form.equipmentId" placeholder="请输入设备Id" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="运行状态" prop="runState">
              <el-input v-model="form.runState" placeholder="请输入运行状态" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="产能数量" prop="productCount">
              <el-input v-model.number="form.productCount" placeholder="请输入产能数量" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="不良数量" prop="defectCount">
              <el-input v-model.number="form.defectCount" placeholder="请输入不良数量" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="报警状态" prop="warnState">
              <el-input v-model.number="form.warnState" placeholder="请输入报警状态" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="报警代码" prop="warnCode">
              <el-input v-model.number="form.warnCode" placeholder="请输入报警代码" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="创建时间" prop="createTime">
              <el-date-picker v-model="form.createTime" type="datetime" :teleported="false" placeholder="选择日期时间"></el-date-picker>
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

<script setup name="equipmentruningrecord">
import { listEquipmentRuningRecord, addEquipmentRuningRecord, delEquipmentRuningRecord } from '@/api/equipment/equipmentRuningRecord.js'
import { dictEquipmentBase } from '@/api/equipment/equipmentBase.js'
const { proxy } = getCurrentInstance()
const ids = ref([])
const loading = ref(false)
const showSearch = ref(true)
const queryParams = reactive({
  pageNum: 1,
  pageSize: 10,
  sort: 'CreateTime',
  sortType: 'desc',
  equipmentId: null,
  runState: undefined,
  createTime: undefined
})
const columns = ref([
  { visible: false, prop: 'id', label: 'ID' },
  { visible: false, prop: 'equipmentId', label: '设备Id' },
  { visible: true, prop: 'equipmentName', label: '设备名称' },
  { visible: true, prop: 'assetNo', label: '资产编号' },
  { visible: true, prop: 'assetName', label: '资产名称' },
  { visible: true, prop: 'runState', label: '运行状态' },
  { visible: true, prop: 'productCount', label: '产能数量' },
  { visible: true, prop: 'defectCount', label: '不良数量' },
  { visible: true, prop: 'warnState', label: '报警状态' },
  { visible: true, prop: 'warnCode', label: '报警代码' },
  { visible: true, prop: 'createTime', label: '创建时间' }
])
const total = ref(0)
const dataList = ref([])
const queryRef = ref()
const defaultTime = ref([new Date(2000, 1, 1, 0, 0, 0), new Date(2000, 2, 1, 23, 59, 59)])

// 创建时间时间范围
const dateRangeCreateTime = ref([])

var dictParams = []

function getList() {
  proxy.addDateRange(queryParams, dateRangeCreateTime.value, 'CreateTime')
  loading.value = true
  listEquipmentRuningRecord(queryParams).then((res) => {
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
  // 创建时间时间范围
  dateRangeCreateTime.value = []
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
// 操作类型 1、add 2、edit 3、view
const opertype = ref(0)
const open = ref(false)
const state = reactive({
  single: true,
  multiple: true,
  form: {},
  rules: {
    equipmentId: [{ required: true, message: '设备Id不能为空', trigger: 'blur', type: 'number' }],
    createTime: [{ required: true, message: '创建时间不能为空', trigger: 'blur' }]
  },
  options: {
    //设备
    equipment_options: []
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
    runState: null,
    productCount: null,
    defectCount: null,
    warnState: null,
    warnCode: null,
    createTime: null
  }
  proxy.resetForm('formRef')
}

// 添加按钮操作
function handleAdd() {
  reset()
  open.value = true
  title.value = '添加设备运行数据'
  opertype.value = 1
}

// 添加&修改 表单提交
function submitForm() {
  proxy.$refs['formRef'].validate((valid) => {
    if (valid) {
      if (form.value.id != undefined && opertype.value === 2) {
      } else {
        addEquipmentRuningRecord(form.value).then((res) => {
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
  const Ids = row.id || ids.value

  proxy
    .$confirm('是否确认删除参数编号为"' + Ids + '"的数据项？', '警告', {
      confirmButtonText: proxy.$t('common.ok'),
      cancelButtonText: proxy.$t('common.cancel'),
      type: 'warning'
    })
    .then(function () {
      return delEquipmentRuningRecord(Ids)
    })
    .then(() => {
      getList()
      proxy.$modal.msgSuccess('删除成功')
    })
}

// 导出按钮操作
function handleExport() {
  proxy
    .$confirm('是否确认导出设备运行数据数据项?', '警告', {
      confirmButtonText: '确定',
      cancelButtonText: '取消',
      type: 'warning'
    })
    .then(async () => {
      await proxy.downFile('/equipment/EquipmentRuningRecord/export', { ...queryParams })
    })
}

// 查询设备
function handleQueryEquipment(keyword) {
  if (keyword) {
    const params = {
      pageNum: 1,
      pageSize: 10,
      sort: '',
      sortType: 'asc',
      keyword: keyword
    }
    setTimeout(() => {
      dictEquipmentBase(params).then((res) => {
        state.options.equipment_options = res.data.result
      })
    }, 200)
  }
}

handleQuery()
</script>
