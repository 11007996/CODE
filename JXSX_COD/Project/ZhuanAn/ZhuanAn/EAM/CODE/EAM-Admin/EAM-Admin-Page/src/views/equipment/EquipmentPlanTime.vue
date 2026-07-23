<!--
 * @Descripttion: (设备计划停机时间/EQU_Plan_Time)
 * @Author: (admin)
 * @Date: (2025-05-12)
-->
<template>
  <div>
    <el-form :model="queryParams" label-position="right" inline ref="queryRef" v-show="showSearch" @submit.prevent>
      <el-form-item>
        <el-button icon="search" type="primary" @click="handleQuery">{{ $t('btn.search') }}</el-button>
        <el-button icon="refresh" @click="resetQuery">{{ $t('btn.reset') }}</el-button>
      </el-form-item>
    </el-form>
    <!-- 工具区域 -->
    <el-row :gutter="15" class="mb10">
      <el-col :span="1.5">
        <el-button type="primary" v-hasPermi="['equipment:plantime:add']" plain icon="plus" @click="handleAdd">
          {{ $t('btn.add') }}
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
      <el-table-column prop="planId" label="计划Id" align="center" v-if="columns.showColumn('planId')" />
      <el-table-column prop="planName" label="计划名称" align="center" :show-overflow-tooltip="true" v-if="columns.showColumn('planName')" />
      <el-table-column
        prop="startTime"
        label="开始时间"
        align="center"
        :show-overflow-tooltip="true"
        sortable
        v-if="columns.showColumn('startTime')" />
      <el-table-column prop="endTime" label="结束时间" align="center" :show-overflow-tooltip="true" v-if="columns.showColumn('endTime')" />
      <el-table-column prop="maxSeconds" label="最大生效时间(秒)" align="center" v-if="columns.showColumn('maxSeconds')" />
      <el-table-column label="操作" width="160">
        <template #default="scope">
          <el-button
            type="success"
            size="small"
            icon="edit"
            title="编辑"
            v-hasPermi="['equipment:plantime:edit']"
            @click="handleUpdate(scope.row)"></el-button>
          <el-button
            type="danger"
            size="small"
            icon="delete"
            title="删除"
            v-hasPermi="['equipment:plantime:delete']"
            @click="handleDelete(scope.row)"></el-button>
        </template>
      </el-table-column>
    </el-table>
    <pagination :total="total" v-model:page="queryParams.pageNum" v-model:limit="queryParams.pageSize" @pagination="getList" />

    <!-- 添加或修改设备计划停机时间对话框 -->
    <el-dialog :title="title" :lock-scroll="false" v-model="open">
      <el-form ref="formRef" :model="form" :rules="rules" label-width="100px">
        <el-row :gutter="20">
          <el-col :lg="12" v-if="opertype != 1">
            <el-form-item label="计划Id" prop="planId">
              <el-input v-model="form.planId" disabled />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="计划名称" prop="planName">
              <el-input v-model="form.planName" placeholder="请输入计划名称" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="开始时间" prop="startTime">
              <el-input v-model="form.startTime" placeholder="请输入开始时间" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="结束时间" prop="endTime">
              <el-input v-model="form.endTime" placeholder="请输入结束时间" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="最大生效时间(秒)" prop="maxSeconds">
              <el-input-number v-model.number="form.maxSeconds" :controls="true" controls-position="right" placeholder="请输入最大生效时间(秒)" />
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

<script setup name="equipmentplantime">
import {
  listEquipmentPlanTime,
  addEquipmentPlanTime,
  delEquipmentPlanTime,
  updateEquipmentPlanTime,
  getEquipmentPlanTime
} from '@/api/equipment/equipmentPlanTime.js'
const { proxy } = getCurrentInstance()
const ids = ref([])
const loading = ref(false)
const showSearch = ref(true)
const queryParams = reactive({
  pageNum: 1,
  pageSize: 10,
  sort: 'StartTime',
  sortType: 'asc'
})
const columns = ref([
  { visible: false, prop: 'planId', label: '计划Id' },
  { visible: true, prop: 'planName', label: '计划名称' },
  { visible: true, prop: 'startTime', label: '开始时间' },
  { visible: true, prop: 'endTime', label: '结束时间' },
  { visible: true, prop: 'maxSeconds', label: '最大生效时间(秒)' }
])
const total = ref(0)
const dataList = ref([])
const queryRef = ref()
const defaultTime = ref([new Date(2000, 1, 1, 0, 0, 0), new Date(2000, 2, 1, 23, 59, 59)])

var dictParams = []

function getList() {
  loading.value = true
  listEquipmentPlanTime(queryParams).then((res) => {
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

    if (column.prop == 'startTime') {
      sort = 'start_Time'
    }
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
    planName: [{ required: true, message: '计划名称不能为空', trigger: 'blur' }],
    startTime: [{ required: true, message: '开始时间不能为空', trigger: 'blur' }],
    endTime: [{ required: true, message: '结束时间不能为空', trigger: 'blur' }],
    maxSeconds: [{ required: true, message: '最大生效时间(秒)不能为空', trigger: 'blur', type: 'number' }]
  },
  options: {}
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
    planId: null,
    planName: null,
    startTime: null,
    endTime: null,
    maxSeconds: null
  }
  proxy.resetForm('formRef')
}

// 添加按钮操作
function handleAdd() {
  reset()
  open.value = true
  title.value = '添加设备计划停机时间'
  opertype.value = 1
}
// 修改按钮操作
function handleUpdate(row) {
  reset()
  const id = row.planId || ids.value
  getEquipmentPlanTime(id).then((res) => {
    const { code, data } = res
    if (code == 200) {
      open.value = true
      title.value = '修改设备计划停机时间'
      opertype.value = 2

      form.value = {
        ...data
      }
    }
  })
}

// 添加&修改 表单提交
function submitForm() {
  proxy.$refs['formRef'].validate((valid) => {
    if (valid) {
      if (form.value.planId != undefined && opertype.value === 2) {
        updateEquipmentPlanTime(form.value).then((res) => {
          proxy.$modal.msgSuccess('修改成功')
          open.value = false
          getList()
        })
      } else {
        addEquipmentPlanTime(form.value).then((res) => {
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
  const Ids = row.planId || ids.value

  proxy
    .$confirm('是否确认删除参数编号为"' + Ids + '"的数据项？', '警告', {
      confirmButtonText: proxy.$t('common.ok'),
      cancelButtonText: proxy.$t('common.cancel'),
      type: 'warning'
    })
    .then(function () {
      return delEquipmentPlanTime(Ids)
    })
    .then(() => {
      getList()
      proxy.$modal.msgSuccess('删除成功')
    })
}

handleQuery()
</script>
