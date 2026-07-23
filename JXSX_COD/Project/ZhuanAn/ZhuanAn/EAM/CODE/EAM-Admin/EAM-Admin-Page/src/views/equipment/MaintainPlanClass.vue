<!--
 * @Descripttion: (保养计划班次/EQU_Maintain_Plan_Class)
 * @Author: (admin)
 * @Date: (2026-02-07)
-->
<template>
  <div>
    <el-form :model="queryParams" label-position="right" inline ref="queryRef" v-show="showSearch" @submit.prevent>
      <el-form-item label="计划年份" prop="planYear">
        <el-date-picker type="year" v-model="queryParams.planYear" value-format="YYYY" placeholder="请输入计划年份" />
      </el-form-item>
      <el-form-item label="计划班次" prop="planClass">
        <el-input v-model="queryParams.planClass" placeholder="请输入计划班次" />
      </el-form-item>
      <el-form-item>
        <el-button icon="search" type="primary" @click="handleQuery">{{ $t('btn.search') }}</el-button>
        <el-button icon="refresh" @click="resetQuery">{{ $t('btn.reset') }}</el-button>
      </el-form-item>
    </el-form>
    <!-- 工具区域 -->
    <el-row :gutter="15" class="mb10">
      <el-col :span="1.5">
        <el-button type="primary" v-hasPermi="['maintain:plan:class:add']" plain icon="plus" @click="handleAdd">
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
      <el-table-column align="center" width="90">
        <template #default="scope">
          <el-button text @click="rowClick(scope.row)">{{ $t('btn.details') }}</el-button>
        </template>
      </el-table-column>
      <el-table-column prop="planYear" label="计划年份" align="center" v-if="columns.showColumn('planYear')" />
      <el-table-column prop="planClass" label="计划班次" align="center" :show-overflow-tooltip="true" v-if="columns.showColumn('planClass')" />
      <el-table-column label="操作" width="160">
        <template #default="scope">
          <el-button
            type="success"
            size="small"
            icon="edit"
            title="编辑"
            v-hasPermi="['maintain:plan:class:edit']"
            @click="handleUpdate(scope.row)"></el-button>
          <el-button
            type="danger"
            size="small"
            icon="delete"
            title="删除"
            v-hasPermi="['maintain:plan:class:delete']"
            @click="handleDelete(scope.row)"></el-button>
        </template>
      </el-table-column>
    </el-table>
    <pagination :total="total" v-model:page="queryParams.pageNum" v-model:limit="queryParams.pageSize" @pagination="getList" />

    <el-drawer v-model="drawer" size="50%" direction="rtl">
      <el-table :data="maintainPlanClassItemList" header-row-class-name="text-navy">
        <el-table-column label="序号" type="index" width="80" />
        <el-table-column prop="dateMark" label="日期标记" align="center" width="90" v-if="columns.showColumn('dateMark')">
          <template #default="scope">
            <dict-tag :options="options.date_mark" :value="scope.row.dateMark" />
          </template>
        </el-table-column>
        <el-table-column prop="dateMarkStamp" label="日期标记值" />
        <el-table-column prop="startDate" label="开始日期" :formatter="(row) => formatterDate(row.startDate)" />
        <el-table-column prop="endDate" label="结束日期" :formatter="(row) => formatterDate(row.endDate)" />
      </el-table>
    </el-drawer>

    <!-- 添加或修改保养计划班次对话框 -->
    <el-dialog :title="title" :lock-scroll="false" v-model="open">
      <el-form ref="formRef" :model="form" :rules="rules" label-width="100px">
        <el-row :gutter="20">
          <el-col :lg="12">
            <el-form-item label="计划年份" prop="planYear">
              <el-date-picker type="year" v-model="form.planYear" value-format="YYYY" placeholder="请输入计划年份" :disabled="opertype != 1" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="计划班次" prop="planClass">
              <template #label>
                <span>
                  <el-tooltip placement="top" content="班次名称，用于区分同一年份的不同时间计划，相当于分组名称一样，例如：端子机">
                    <el-icon> <questionFilled /> </el-icon>
                  </el-tooltip>
                  计划班次
                </span>
              </template>
              <el-input v-model="form.planClass" placeholder="请输入计划班次" :disabled="opertype != 1" />
            </el-form-item>
          </el-col>

          <el-divider content-position="center">保养计划班次详情</el-divider>
          <el-row :gutter="10" class="mb8">
            <el-col :span="1.5">
              <el-button type="primary" icon="Plus" @click="handleAddMaintainPlanClassItem">添加</el-button>
            </el-col>
            <el-col :span="1.5">
              <el-button type="danger" icon="Delete" @click="handleDeleteMaintainPlanClassItem">删除</el-button>
            </el-col>
            <el-col :span="1.5">
              <el-button type="success" icon="Grid" @click="handleAutoCreatePlanClassItem">一键生成</el-button>
            </el-col>
          </el-row>
          <el-table
            :data="maintainPlanClassItemList"
            :row-class-name="rowMaintainPlanClassItemIndex"
            @selection-change="handleMaintainPlanClassItemSelectionChange"
            ref="MaintainPlanClassItemRef">
            <el-table-column type="selection" width="40" align="center" />
            <el-table-column label="序号" align="center" prop="index" width="50" />
            <el-table-column label="日期标记" align="center" prop="dateMark" min-width="100">
              <template #default="scope">
                <el-select
                  v-model="scope.row.dateMark"
                  placeholder="请选择日期标记"
                  @change="handleRowDateMarkChange(scope.row)"
                  clearable
                  class="fullWidth">
                  <el-option
                    v-for="item in options.date_mark"
                    :key="item.dictValue"
                    :label="item.dictLabel"
                    :value="item.dictValue"
                    :disabled="item.dictValue == 'D' || item.dictValue == 'W'"></el-option>
                </el-select>
              </template>
            </el-table-column>
            <el-table-column label="日期标记值" align="center" prop="dateMarkStamp">
              <template #default="scope">
                <el-select v-model="scope.row.dateMarkStamp" placeholder="请选择日期标记值" clearable class="fullWidth">
                  <el-option v-for="item in scope.row.date_mark_stamp_options" :label="item" :value="item"></el-option>
                </el-select>
              </template>
            </el-table-column>
            <el-table-column label="保养日期" align="center" prop="maintainDateRange" min-width="160">
              <template #default="scope">
                <el-date-picker
                  clearable
                  v-model="scope.row.maintainDateRange"
                  type="daterange"
                  format="MM-DD"
                  placeholder="选择日期时间"
                  class="fullWidth"></el-date-picker>
              </template>
            </el-table-column>
          </el-table>
        </el-row>
      </el-form>
      <template #footer v-if="opertype != 3">
        <el-button text @click="cancel">{{ $t('btn.cancel') }}</el-button>
        <el-button type="primary" @click="submitForm">{{ $t('btn.submit') }}</el-button>
      </template>
    </el-dialog>
  </div>
</template>

<script setup name="maintainplanclass">
import {
  listMaintainPlanClass,
  addMaintainPlanClass,
  delMaintainPlanClass,
  updateMaintainPlanClass,
  getMaintainPlanClass
} from '@/api/equipment/maintainPlanClass.js'
import { dayjs } from 'element-plus'
const { proxy } = getCurrentInstance()
const ids = ref([])
const loading = ref(false)
const showSearch = ref(true)
const queryParams = reactive({
  pageNum: 1,
  pageSize: 10,
  sort: 'PlanYear',
  sortType: 'desc',
  planYear: undefined,
  planClass: undefined
})
const columns = ref([
  { visible: true, prop: 'planYear', label: '计划年份' },
  { visible: true, prop: 'planClass', label: '计划班次' }
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
  listMaintainPlanClass(queryParams).then((res) => {
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
// 操作类型 1、add 2、edit 3、view
const opertype = ref(0)
const open = ref(false)
const state = reactive({
  single: true,
  multiple: true,
  form: {},
  rules: {
    planYear: [{ required: true, message: '计划年份不能为空', trigger: 'blur' }],
    planClass: [{ required: true, message: '计划班次不能为空', trigger: 'blur' }]
  },
  options: {
    // 日期标记
    date_mark: []
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
    planYear: null,
    planClass: null
  }
  maintainPlanClassItemList.value = []
  proxy.resetForm('formRef')
}

// 添加按钮操作
function handleAdd() {
  reset()
  open.value = true
  title.value = '添加保养计划班次'
  opertype.value = 1
}
// 修改按钮操作
function handleUpdate(row) {
  reset()
  const id = row.planClassId || ids.value
  getMaintainPlanClass(id).then((res) => {
    const { code, data } = res
    if (code == 200) {
      open.value = true
      title.value = '修改保养计划班次'
      opertype.value = 2

      form.value = {
        ...data
      }
      form.value.planYear = '' + data.planYear

      data.maintainPlanClassItemNav.forEach((item) => {
        item.maintainDateRange = [item.startDate, item.endDate]
      })
      maintainPlanClassItemList.value = data.maintainPlanClassItemNav
    }
  })
}

// 添加&修改 表单提交
function submitForm() {
  proxy.$refs['formRef'].validate((valid) => {
    if (valid) {
      //日期项目转换为表单数据
      let planClassItem = []
      maintainPlanClassItemList.value.forEach((item) => {
        planClassItem.push({
          dateMark: item.dateMark,
          dateMarkStamp: item.dateMarkStamp,
          startDate: item.maintainDateRange[0],
          endDate: item.maintainDateRange[1]
        })
      })
      form.value.maintainPlanClassItemNav = planClassItem

      if (form.value.planYear != undefined && opertype.value === 2) {
        updateMaintainPlanClass(form.value).then((res) => {
          proxy.$modal.msgSuccess('修改成功')
          open.value = false
          getList()
        })
      } else {
        addMaintainPlanClass(form.value).then((res) => {
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
  const Ids = row.planClassId || ids.value

  proxy
    .$confirm('是否确认删除参数编号为"' + Ids + '"的数据项？', '警告', {
      confirmButtonText: proxy.$t('common.ok'),
      cancelButtonText: proxy.$t('common.cancel'),
      type: 'warning'
    })
    .then(function () {
      return delMaintainPlanClass(Ids)
    })
    .then(() => {
      getList()
      proxy.$modal.msgSuccess('删除成功')
    })
}

/*********************保养计划班次子表信息*************************/
const maintainPlanClassItemList = ref([])
const checkedMaintainPlanClassItem = ref([])
const drawer = ref(false)

/** 保养计划班次序号 */
function rowMaintainPlanClassItemIndex({ row, rowIndex }) {
  row.index = rowIndex + 1
}

/** 保养计划班次添加按钮操作 */
function handleAddMaintainPlanClassItem() {
  let obj = {}
  //下面的代码自己设置默认值
  //obj.startDate = null;
  //obj.endDate = null;
  maintainPlanClassItemList.value.push(obj)
}

/** 复选框选中数据 */
function handleMaintainPlanClassItemSelectionChange(selection) {
  checkedMaintainPlanClassItem.value = selection.map((item) => item.index)
}

/** 保养计划班次删除按钮操作 */
function handleDeleteMaintainPlanClassItem() {
  if (checkedMaintainPlanClassItem.value.length == 0) {
    proxy.$modal.msgError('请先选择要删除的保养计划班次数据')
  } else {
    const MaintainPlanClasssItems = maintainPlanClassItemList.value
    const checkedMaintainPlanClassItems = checkedMaintainPlanClassItem.value
    maintainPlanClassItemList.value = MaintainPlanClasssItems.filter(function (item) {
      return checkedMaintainPlanClassItems.indexOf(item.index) == -1
    })
  }
}

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

//一键生成
function handleAutoCreatePlanClassItem() {
  maintainPlanClassItemList.value = []
  let planClassList = []
  let now = new Date()
  if (form.value.planYear == undefined || parseInt(form.value.planYear) < now.getFullYear()) {
    proxy.$modal.msgError('请选择年份，且年份不能小于当年')
    return
  }
  let planYear = parseInt(form.value.planYear)

  //添加月份
  for (let i = 1; i <= 12; i++) {
    let endDate = new Date(planYear, i, 0)
    let startDate = new Date(new Date(endDate).setDate(endDate.getDate() - 2))
    planClassList.push({
      dateMark: 'M',
      dateMarkStamp: i,
      date_mark_stamp_options: [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12],
      maintainDateRange: [startDate, endDate]
    })
  }
  //添加季度
  for (let i = 1; i <= 4; i++) {
    let endDate = new Date(planYear, i * 3, 0)
    let startDate = new Date(new Date(endDate).setDate(endDate.getDate() - 2))
    planClassList.push({ dateMark: 'Q', dateMarkStamp: i, date_mark_stamp_options: [1, 2, 3, 4], maintainDateRange: [startDate, endDate] })
  }
  //添加年份
  let endDate = new Date(planYear, 12, 0)
  let startDate = new Date(new Date(endDate).setDate(endDate.getDate() - 2))
  planClassList.push({ dateMark: 'Y', dateMarkStamp: 1, date_mark_stamp_options: [1], maintainDateRange: [startDate, endDate] })

  maintainPlanClassItemList.value = planClassList
}

//日期标记change事件
function handleRowDateMarkChange(row) {
  let len = 1
  let stampOptions = []
  if (row.dateMark == 'M') {
    len = 12
  } else if (row.dateMark == 'Q') {
    len = 4
  }

  for (let i = 1; i <= len; i++) {
    stampOptions.push(i)
  }
  row.date_mark_stamp_options = stampOptions
  row.dateMarkStamp = null
  row.maintainDateRange = [undefined, undefined]
}

//格式化日期
function formatterDate(time) {
  if (time) return dayjs(time).format('YYYY-MM-DD')
}

handleQuery()
</script>
