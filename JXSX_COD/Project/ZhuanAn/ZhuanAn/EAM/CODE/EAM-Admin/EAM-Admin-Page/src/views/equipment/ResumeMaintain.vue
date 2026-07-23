<!--
 * @Descripttion: (履历保养记录/EQU_Resume_Maintain)
 * @Author: (admin)
 * @Date: (2024-10-18)
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
      <el-form-item label="实施日期">
        <el-date-picker
          v-model="dateRangeExecuteDate"
          type="daterange"
          start-placeholder="开始日期"
          end-placeholder="结束日期"
          value-format="YYYY-MM-DD"
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
        <el-button type="primary" v-hasPermi="['resume:maintain:add']" plain icon="plus" @click="handleAdd">
          {{ $t('btn.add') }}
        </el-button>
      </el-col>
      <el-col :span="1.5">
        <el-button type="warning" plain icon="download" @click="handleExport" v-hasPermi="['resume:maintain:export']">
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
      <el-table-column prop="id" label="ID" align="center" v-if="columns.showColumn('id')" />
      <el-table-column prop="equipmentId" label="设备ID" align="center" v-if="columns.showColumn('equipmentId')" />
      <el-table-column
        prop="assetNo"
        label="资产编号"
        align="center"
        width="200"
        :show-overflow-tooltip="true"
        v-if="columns.showColumn('assetNo')" />
      <el-table-column prop="executeCategory" label="实施类别" align="center" width="90" v-if="columns.showColumn('executeCategory')">
        <template #default="scope">
          <dict-tag :options="options.execute_category" :value="scope.row.executeCategory" />
        </template>
      </el-table-column>
      <el-table-column
        prop="executeDate"
        label="实施日期"
        align="center"
        width="100"
        v-if="columns.showColumn('executeDate')"
        :formatter="(row) => formatterDate(row.executeDate)" />
      <el-table-column
        prop="takenDept"
        label="保管部门"
        align="center"
        min-width="150"
        :show-overflow-tooltip="true"
        v-if="columns.showColumn('takenDept')" />
      <el-table-column
        prop="executeState"
        label="实施状况"
        align="center"
        min-width="150"
        :show-overflow-tooltip="true"
        v-if="columns.showColumn('executeState')" />
      <el-table-column prop="executeUser" label="实施人员工号" align="center" width="90" v-if="columns.showColumn('executeUser')" />
      <el-table-column prop="executeUserName" label="实施人员" align="center" width="90" v-if="columns.showColumn('executeUserName')" />
      <el-table-column
        prop="nextExecuteDate"
        label="下次实施日期"
        width="100"
        v-if="columns.showColumn('nextExecuteDate')"
        :formatter="(row) => formatterDate(row.nextExecuteDate)" />
      <el-table-column prop="remark" label="备注/校验报告编号" align="center" :show-overflow-tooltip="true" v-if="columns.showColumn('remark')" />
      <el-table-column prop="createBy" label="创建用户" align="center" width="90" v-if="columns.showColumn('createBy')" />
      <el-table-column prop="createTime" label="创建时间" align="center" width="160" sortable v-if="columns.showColumn('createTime')" />
      <el-table-column prop="updateBy" label="更新用户" align="center" width="90" v-if="columns.showColumn('updateBy')" />
      <el-table-column prop="updateTime" label="更新时间" align="center" width="160" v-if="columns.showColumn('updateTime')" />
      <el-table-column label="操作" width="120">
        <template #default="scope">
          <el-button
            type="success"
            size="small"
            icon="edit"
            title="编辑"
            v-hasPermi="['resume:maintain:edit']"
            @click="handleUpdate(scope.row)"></el-button>
          <el-button
            type="danger"
            size="small"
            icon="delete"
            title="删除"
            v-hasPermi="['resume:maintain:delete']"
            @click="handleDelete(scope.row)"></el-button>
        </template>
      </el-table-column>
    </el-table>
    <pagination :total="total" v-model:page="queryParams.pageNum" v-model:limit="queryParams.pageSize" @pagination="getList" />

    <!-- 添加或修改履历保养记录对话框 -->
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
            <el-form-item label="实施类别" prop="executeCategory">
              <el-select v-model="form.executeCategory" placeholder="请选择实施类别">
                <el-option
                  v-for="item in options.execute_category"
                  :key="item.dictValue"
                  :label="item.dictLabel"
                  :value="item.dictValue"></el-option>
              </el-select>
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="实施日期" prop="executeDate">
              <el-date-picker v-model="form.executeDate" type="date" :teleported="false" placeholder="选择日期时间"></el-date-picker>
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="保管部门" prop="takenDept">
              <el-input v-model="form.takenDept" placeholder="请输入保管部门" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="实施状况" prop="executeState">
              <el-input v-model="form.executeState" placeholder="请输入实施状况" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="实施人员" prop="executeUser">
              <el-select v-model="form.executeUser" placeholder="请输入员工姓名" clearable filterable remote :remote-method="handleQueryEmployee">
                <el-option
                  v-for="item in options.emp_options"
                  :key="item.dictValue"
                  :label="item.dictValue + ' - ' + item.dictLabel"
                  :value="item.dictValue"></el-option>
              </el-select>
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="下次实施日期" prop="nextExecuteDate">
              <el-date-picker v-model="form.nextExecuteDate" type="date" :teleported="false" placeholder="选择日期时间"></el-date-picker>
            </el-form-item>
          </el-col>

          <el-col :lg="24">
            <el-form-item label="备注/校验报告编号" prop="remark">
              <el-input type="textarea" v-model="form.remark" placeholder="请输入备注" />
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

<script setup name="resumemaintain">
import {
  listResumeMaintain,
  addResumeMaintain,
  delResumeMaintain,
  updateResumeMaintain,
  getResumeMaintain
} from '@/api/equipment/resumeMaintain.js'
import { dayjs } from 'element-plus'
import { dictEquipmentBase } from '@/api/equipment/equipmentBase.js'
import { dictEmployee } from '@/api/basic/employee'

const props = defineProps({ equipmentId: Number })
const { proxy } = getCurrentInstance()
const ids = ref([])
const loading = ref(false)
const showSearch = ref(true)
const queryParams = reactive({
  pageNum: 1,
  pageSize: 10,
  sort: 'CreateTime',
  sortType: 'desc',
  equipmentId: undefined,
  executeDate: undefined
})
const columns = ref([
  { visible: false, prop: 'id', label: 'ID' },
  { visible: false, prop: 'equipmentId', label: '设备ID' },
  { visible: true, prop: 'assetNo', label: '资产编号' },
  { visible: true, prop: 'executeCategory', label: '实施类别' },
  { visible: true, prop: 'executeDate', label: '实施日期' },
  { visible: true, prop: 'takenDept', label: '保管部门' },
  { visible: true, prop: 'executeState', label: '实施状况' },
  { visible: false, prop: 'executeUser', label: '实施人员工号' },
  { visible: true, prop: 'executeUserName', label: '实施人员' },
  { visible: true, prop: 'nextExecuteDate', label: '下次实施日期' },
  { visible: true, prop: 'remark', label: '备注/校验报告编号' },
  { visible: false, prop: 'createBy', label: '创建用户' },
  { visible: false, prop: 'createTime', label: '创建时间' },
  { visible: false, prop: 'updateBy', label: '更新用户' },
  { visible: false, prop: 'updateTime', label: '更新时间' }
])
const total = ref(0)
const dataList = ref([])
const queryRef = ref()
const defaultTime = ref([new Date(2000, 1, 1, 0, 0, 0), new Date(2000, 2, 1, 23, 59, 59)])

// 实施日期时间范围
const dateRangeExecuteDate = ref([])

var dictParams = [{ dictType: 'execute_category' }]

proxy.getDicts(dictParams).then((response) => {
  response.data.forEach((element) => {
    state.options[element.dictType] = element.list
  })
})

function getList() {
  proxy.addDateRange(queryParams, dateRangeExecuteDate.value, 'ExecuteDate')
  loading.value = true
  listResumeMaintain(queryParams).then((res) => {
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
  // 实施日期时间范围
  dateRangeExecuteDate.value = []
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

    if (column.prop == 'updateTime') {
      sort = 'update_Time'
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
    equipmentId: [{ required: true, message: '设备不能为空', trigger: 'blur' }],
    executeCategory: [{ required: true, message: '实施类别不能为空', trigger: 'change' }]
  },
  options: {
    // 实施类别 选项列表 格式 eg:{ dictLabel: '标签', dictValue: '0'}
    execute_category: [],
    // 设备资产
    equipment_options: [],
    // 员工
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
    id: null,
    equipmentId: null,
    executeCategory: null,
    executeDate: null,
    takenDept: null,
    executeState: null,
    executeUser: null,
    nextExecuteDate: null,
    remark: null
  }
  proxy.resetForm('formRef')
}

// 添加按钮操作
function handleAdd() {
  reset()
  open.value = true
  title.value = '添加履历保养记录'
  opertype.value = 1
}
// 修改按钮操作
function handleUpdate(row) {
  reset()
  const id = row.id || ids.value
  getResumeMaintain(id).then((res) => {
    const { code, data } = res
    if (code == 200) {
      open.value = true
      title.value = '修改履历保养记录'
      opertype.value = 2

      form.value = {
        ...data
      }

      options.value.emp_options = [{ dictValue: form.value.executeUser, dictLabel: form.value.executeUserName }]
      options.value.equipment_options = [{ dictValue: form.value.equipmentId, dictLabel: form.value.assetNo + ' : ' + form.value.assetName }]
    }
  })
}

// 添加&修改 表单提交
function submitForm() {
  proxy.$refs['formRef'].validate((valid) => {
    if (valid) {
      if (form.value.id != undefined && opertype.value === 2) {
        updateResumeMaintain(form.value).then((res) => {
          proxy.$modal.msgSuccess('修改成功')
          open.value = false
          getList()
        })
      } else {
        addResumeMaintain(form.value).then((res) => {
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
      return delResumeMaintain(Ids)
    })
    .then(() => {
      getList()
      proxy.$modal.msgSuccess('删除成功')
    })
}

// 导出按钮操作
function handleExport() {
  proxy
    .$confirm('是否确认导出设备履历保养校验记录数据?', '警告', {
      confirmButtonText: '确定',
      cancelButtonText: '取消',
      type: 'warning'
    })
    .then(async () => {
      await proxy.downFile('/equipment/ResumeMaintain/export', { ...queryParams })
    })
}

// 查询资产编号
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

//员工查询
function handleQueryEmployee(keyword) {
  if (keyword) {
    const query = {
      pageNum: 1,
      pageSize: 10,
      empName: keyword
    }
    setTimeout(() => {
      dictEmployee(query).then((res) => {
        state.options.emp_options = res.data.result
      })
    }, 200)
  }
}

//格式化日期
function formatterDate(time) {
  if (time) return dayjs(time).format('YYYY-MM-DD')
}

//监听 props变化
watch(
  props,
  (newValue) => {
    queryParams.equipmentId = props.equipmentId
    handleQuery()
  },
  { immediate: true }
)

handleQuery()
</script>
