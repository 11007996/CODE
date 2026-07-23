<!--
 * @Descripttion: (保养计划/EQU_Maintain_Plan)
 * @Author: (admin)
 * @Date: (2026-02-07)
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
      <el-form-item label="计划年份" prop="planYear">
        <el-date-picker
          type="year"
          v-model="queryParams.planYear"
          value-format="YYYY"
          @change="handlePlanYearChange"
          placeholder="请输入计划年份" />
      </el-form-item>
      <el-form-item label="计划班次" prop="planClassId">
        <el-select v-model="queryParams.planClassId" placeholder="请选择计划班次">
          <el-option v-for="item in options.plan_class_options" :key="item.dictValue" :label="item.dictLabel" :value="item.dictValue"></el-option>
        </el-select>
      </el-form-item>
      <el-form-item label="执行部门" prop="executeDeptId">
        <el-tree-select
          v-model="queryParams.executeDeptId"
          :data="options.dept_tree_options"
          :props="{ value: 'id', label: 'label', children: 'children' }"
          value-key="id"
          placeholder="请选择执行部门"
          check-strictly
          clearable
          :render-after-expand="false" />
      </el-form-item>
      <el-form-item>
        <el-button icon="search" type="primary" @click="handleQuery">{{ $t('btn.search') }}</el-button>
        <el-button icon="refresh" @click="resetQuery">{{ $t('btn.reset') }}</el-button>
      </el-form-item>
    </el-form>
    <!-- 工具区域 -->
    <el-row :gutter="15" class="mb10">
      <el-col :span="1.5">
        <el-button type="primary" v-hasPermi="['maintain:plan:add']" plain icon="plus" @click="handleAdd">
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
      <el-table-column prop="planId" label="计划Id" align="center" width="90" v-if="columns.showColumn('planId')" />
      <el-table-column prop="assetNo" label="资产编号" align="center" width="210" v-if="columns.showColumn('assetNo')" />
      <el-table-column prop="assetName" label="资产名称" align="center" min-width="90" v-if="columns.showColumn('assetName')" />
      <el-table-column prop="equipmentName" label="设备名称" align="center" min-width="90" v-if="columns.showColumn('equipmentName')" />
      <el-table-column prop="planYear" label="计划年份" align="center" width="90" v-if="columns.showColumn('planYear')" />
      <el-table-column prop="planClass" label="计划班次" align="center" min-width="90" v-if="columns.showColumn('planClass')">
        <template #default="scope">
          <el-link type="primary" @click="rowClick(scope.row)">{{ scope.row.planClass }}</el-link>
        </template>
      </el-table-column>
      <el-table-column prop="executeDeptName" label="执行部门" align="center" min-width="90" v-if="columns.showColumn('executeDeptName')" />
      <el-table-column prop="createBy" label="创建人" align="center" :show-overflow-tooltip="true" v-if="columns.showColumn('createBy')" />
      <el-table-column prop="createTime" label="创建时间" :show-overflow-tooltip="true" v-if="columns.showColumn('createTime')" />
      <el-table-column label="操作" width="70">
        <template #default="scope">
          <!-- <el-button
            type="success"
            size="small"
            icon="edit"
            title="编辑"
            v-hasPermi="['maintain:plan:edit']"
            @click="handleUpdate(scope.row)"></el-button> -->
          <el-button
            type="danger"
            size="small"
            icon="delete"
            title="删除"
            v-hasPermi="['maintain:plan:delete']"
            @click="handleDelete(scope.row)"></el-button>
        </template>
      </el-table-column>
    </el-table>
    <pagination :total="total" v-model:page="queryParams.pageNum" v-model:limit="queryParams.pageSize" @pagination="getList" />

    <el-drawer v-model="drawer" size="50%" direction="rtl">
      <el-table :data="maintainPlanClassItemList" header-row-class-name="text-navy">
        <el-table-column label="日期标记" align="center" prop="dateMark" min-width="100">
          <template #default="scope">
            <dict-tag :options="options.date_mark" :value="scope.row.dateMark" />
          </template>
        </el-table-column>
        <el-table-column prop="dateMarkStamp" label="日期标记值" />
        <el-table-column prop="startDate" label="开始日期" :formatter="(row) => formatterDate(row.startDate)" />
        <el-table-column prop="endDate" label="结束日期" :formatter="(row) => formatterDate(row.endDate)" />
      </el-table>
    </el-drawer>

    <!-- 添加或修改保养计划对话框 -->
    <el-dialog :title="title" :lock-scroll="false" v-model="open" :fullscreen="fullScreen">
      <el-form ref="formRef" :model="form" :rules="rules" label-width="100px">
        <el-row :gutter="20">
          <el-col :lg="12">
            <el-form-item label="计划年份" prop="planYear">
              <el-date-picker
                type="year"
                v-model="form.planYear"
                value-format="YYYY"
                @change="handlePlanYearChange"
                placeholder="请输入计划年份"
                :disabled="opertype != 1" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="计划班次" prop="planClassId">
              <el-select v-model="form.planClassId" placeholder="请选择计划班次" @change="handlePlanClassChange" :disabled="opertype != 1">
                <el-option
                  v-for="item in options.plan_class_options"
                  :key="item.dictValue"
                  :label="item.dictLabel"
                  :value="item.dictValue"></el-option>
              </el-select>
              <el-button text icon="view" @click="showPlanClassItem(form.planClassId)"></el-button>
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="执行部门" prop="executeDeptId">
              <el-tree-select
                v-model="form.executeDeptId"
                :data="options.dept_tree_options"
                :props="{ value: 'id', label: 'label', children: 'children' }"
                value-key="id"
                placeholder="请选择执行部门"
                check-strictly
                :render-after-expand="false" />
            </el-form-item>
          </el-col>
        </el-row>

        <el-divider content-position="center">计划设备选择</el-divider>
        <el-row :gutter="20">
          <el-col :lg="4">
            <el-button type="info" icon="FullScreen" @click="fullScreen = !fullScreen">{{ fullScreen ? '退出全屏' : '全屏' }}</el-button>
          </el-col>
          <el-col :lg="20">
            <el-form-item label="设备">
              <el-input
                v-model="queryParams.keyword"
                @input="handleQueryExcludeEquipment"
                placeholder="搜索：资产编号/设备名称/资产名称/自定义机型" />
            </el-form-item>
          </el-col>
        </el-row>
        <el-table
          :data="excludeEquipmentList"
          :row-class-name="rowExcludeEquipmentIndex"
          @selection-change="handleExcludeEquipmentSelectionChange"
          max-height="350"
          ref="excludeEquipmentListRef">
          <el-table-column type="selection" width="40" align="center" />
          <el-table-column prop="assetNo" label="资产编号" width="210" align="center" />
          <el-table-column prop="assetName" label="资产名称" />
          <el-table-column prop="equipmentName" label="设备名称" />
          <el-table-column prop="customModel" label="自定义机型" />
        </el-table>
      </el-form>
      <template #footer v-if="opertype != 3">
        <el-button text @click="cancel">{{ $t('btn.cancel') }}</el-button>
        <el-button type="primary" @click="submitForm">{{ $t('btn.submit') }}</el-button>
      </template>
    </el-dialog>
  </div>
</template>

<script setup name="maintainplan">
import {
  listMaintainPlan,
  addMaintainPlan,
  delMaintainPlan,
  updateMaintainPlan,
  getMaintainPlan,
  listExcludeEquipment
} from '@/api/equipment/maintainPlan.js'
import { dictMaintainPlanClass, getMaintainPlanClass } from '@/api/equipment/maintainPlanClass.js'
import { factoryDeptTreeselect } from '@/api/system/dept.js'
import { dictEquipmentBase } from '@/api/equipment/equipmentBase.js'
import { dayjs } from 'element-plus'
const { proxy } = getCurrentInstance()
const ids = ref([])
const loading = ref(false)
const showSearch = ref(true)
const queryParams = reactive({
  pageNum: 1,
  pageSize: 10,
  sort: '',
  sortType: 'desc',
  equipmentId: undefined,
  planYear: undefined,
  executeDeptId: undefined,
  createTime: undefined
})
const columns = ref([
  { visible: false, prop: 'planId', label: '计划Id' },
  { visible: true, prop: 'assetNo', label: '资产编号' },
  { visible: true, prop: 'assetName', label: '资产名称' },
  { visible: true, prop: 'equipmentName', label: '设备名称' },
  { visible: true, prop: 'planYear', label: '计划年份' },
  { visible: true, prop: 'planClass', label: '计划班次' },
  { visible: true, prop: 'executeDeptName', label: '执行部门' },
  { visible: false, prop: 'createBy', label: '创建人' },
  { visible: false, prop: 'createTime', label: '创建时间' }
])
const total = ref(0)
const dataList = ref([])
const queryRef = ref()
const defaultTime = ref([new Date(2000, 1, 1, 0, 0, 0), new Date(2000, 2, 1, 23, 59, 59)])

var dictParams = [{ dictType: 'date_mark' }]

proxy.getDicts(dictParams).then((response) => {
  response.data.forEach((element) => {
    state.options[element.dictType] = element.list
  })
})

function getList() {
  loading.value = true
  listMaintainPlan(queryParams).then((res) => {
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
    equipmentIds: [{ required: true, message: '设备不能为空', trigger: 'change' }],
    planYear: [{ required: true, message: '计划年份不能为空', trigger: 'blur' }],
    planClassId: [{ required: true, message: '计划班次不能为空', trigger: 'change' }],
    executeDeptId: [{ required: true, message: '执行部门不能为空', trigger: 'change', type: 'number' }]
  },
  options: {
    // 日期标记
    date_mark: [],
    // 设备ID 选项列表 格式 eg:{ dictLabel: '标签', dictValue: '0'}
    equipment_options: [],
    // 部门树形选项数据  格式：
    dept_tree_options: undefined,
    //计划班次
    plan_class_options: [],
    // 未配置计划设备选项列表
    exclude_equipment_options: []
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
    planId: null,
    planYear: null,
    planClassId: null,
    executeDeptId: null,
    equipmentIds: null
  }
  excludeEquipmentList.value = []
  checkedExcludeEquipment.value = []
  proxy.resetForm('formRef')
}

// 添加按钮操作
function handleAdd() {
  reset()
  open.value = true
  title.value = '添加保养计划'
  opertype.value = 1
}
// 修改按钮操作
function handleUpdate(row) {
  reset()
  const id = row.planId || ids.value
  getMaintainPlan(id).then((res) => {
    const { code, data } = res
    if (code == 200) {
      open.value = true
      title.value = '修改保养计划'
      opertype.value = 2

      form.value = {
        ...data
      }
      maintainPlanClassItemList.value = res.data.maintainPlanClassItemNav
    }
  })
}

// 添加&修改 表单提交
function submitForm() {
  proxy.$refs['formRef'].validate((valid) => {
    if (valid) {
      //选中的设备
      form.value.equipmentIds = [...checkedExcludeEquipment.value]
      if (form.value.planId != undefined && opertype.value === 2) {
        updateMaintainPlan(form.value).then((res) => {
          proxy.$modal.msgSuccess('修改成功')
          open.value = false
          getList()
        })
      } else {
        addMaintainPlan(form.value).then((res) => {
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
      return delMaintainPlan(Ids)
    })
    .then(() => {
      getList()
      proxy.$modal.msgSuccess('删除成功')
    })
}

//年份变动事件
function handlePlanYearChange(year) {
  if (year) {
    const params = {
      pageNum: 1,
      pageSize: 100,
      sort: '',
      sortType: 'asc',
      planYear: year
    }
    setTimeout(() => {
      dictMaintainPlanClass(params).then((res) => {
        state.options.plan_class_options = res.data.result
      })
    }, 200)
  }
}

//班次变动事件
function handlePlanClassChange(planClassId) {
  if (planClassId) {
    setTimeout(() => {
      getMaintainPlanClass(planClassId).then((res) => {
        maintainPlanClassItemList.value = res.data.maintainPlanClassItemNav
      })
    }, 200)
  }
}

/** 查询部门下拉树结构 */
function getTreeselect() {
  factoryDeptTreeselect().then((response) => {
    options.value.dept_tree_options = response.data
  })
}
/*********************计划设备子表信息*************************/
const excludeEquipmentList = ref([])
const checkedExcludeEquipment = ref([])
const fullScreen = ref(false)
//获取未排计划的保养设备
function handleQueryExcludeEquipment(keyword) {
  if (keyword) {
    const params = {
      pageNum: 1,
      pageSize: 100,
      sort: '',
      sortType: 'asc',
      planYear: form.value.planYear,
      keyword: keyword
    }
    setTimeout(() => {
      checkedExcludeEquipment.value = []
      listExcludeEquipment(params).then((res) => {
        excludeEquipmentList.value = res.data.result
      })
    }, 200)
  }
}

/** 未排计划的保养设备序号 */
function rowExcludeEquipmentIndex({ row, rowIndex }) {
  row.index = rowIndex + 1
}

/** 复选框选中数据 */
function handleExcludeEquipmentSelectionChange(selection) {
  checkedExcludeEquipment.value = selection.map((item) => item.equipmentId)
}

/*********************保养计划班次子表信息*************************/
const maintainPlanClassItemList = ref([])
const drawer = ref(false)

/** 保养计划班次详情 */
function rowClick(row) {
  const id = row.planClassId || ids.value
  getMaintainPlanClass(id).then((res) => {
    const { code, data } = res
    if (code == 200) {
      drawer.value = true
      maintainPlanClassItemList.value = data.maintainPlanClassItemNav
    }
  })
}

/** 保养计划班次详情 */
function showPlanClassItem(planClassId) {
  if (planClassId)
    getMaintainPlanClass(planClassId).then((res) => {
      const { code, data } = res
      if (code == 200) {
        drawer.value = true
        maintainPlanClassItemList.value = data.maintainPlanClassItemNav
      }
    })
}

//格式化日期
function formatterDate(time) {
  if (time) return dayjs(time).format('YYYY-MM-DD')
}

getTreeselect()
handleQuery()
</script>
