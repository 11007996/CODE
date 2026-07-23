<!--
 * @Descripttion: (盒子操作记录/CALL_Box_Operate_Record)
 * @Author: (admin)
 * @Date: (2026-05-08)
-->
<template>
  <div>
    <el-form :model="queryParams" label-position="right" inline ref="queryRef" v-show="showSearch" @submit.prevent>
      <el-form-item label="呼叫盒" prop="keyword">
        <el-select v-model="queryParams.keyword" placeholder="呼叫盒名称、MAC、IP" clearable filterable remote :remote-method="handleQueryCallBox">
          <el-option
            v-for="item in options.callBoxOptions"
            :key="item.dictValue"
            :label="item.dictLabel"
            :value="Number(item.dictValue)"></el-option>
        </el-select>
      </el-form-item>

      <el-form-item label="创建时间" prop="createTime">
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
      <!-- <el-col :span="1.5">
        <el-button type="primary" v-hasPermi="['call:box:operate:add']" plain icon="plus" @click="handleAdd">
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
      <el-table-column prop="operateId" label="操作记录Id" align="center" v-if="columns.showColumn('operateId')" />
      <el-table-column prop="boxId" label="呼叫盒ID" align="center" v-if="columns.showColumn('boxId')" />
      <el-table-column prop="boxName" label="呼叫盒名称" align="center" v-if="columns.showColumn('boxName')" />
      <el-table-column prop="operateType" label="操作类型" align="center" v-if="columns.showColumn('operateType')">
        <template #default="scope">
          <dict-tag :options="options.call_box_operate_type" :value="scope.row.operateType" />
        </template>
      </el-table-column>
      <el-table-column prop="createBy" label="创建人" align="center" :show-overflow-tooltip="true" v-if="columns.showColumn('createBy')" />
      <el-table-column prop="createTime" label="创建时间" :show-overflow-tooltip="true" v-if="columns.showColumn('createTime')" />
    </el-table>
    <pagination :total="total" v-model:page="queryParams.pageNum" v-model:limit="queryParams.pageSize" @pagination="getList" />

    <!-- 添加或修改盒子操作记录对话框 -->
    <el-dialog :title="title" :lock-scroll="false" v-model="open">
      <el-form ref="formRef" :model="form" :rules="rules" label-width="100px">
        <el-row :gutter="20">
          <el-col :lg="12">
            <el-form-item label="盒子ID" prop="boxId">
              <el-input v-model.number="form.boxId" placeholder="请输入盒子ID" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="操作类型" prop="operateType">
              <el-select v-model="form.operateType" placeholder="请选择操作类型">
                <el-option
                  v-for="item in options.call_box_operate_type"
                  :key="item.dictValue"
                  :label="item.dictLabel"
                  :value="parseInt(item.dictValue)"></el-option>
              </el-select>
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="创建人" prop="createBy">
              <el-input v-model="form.createBy" placeholder="请输入创建人" />
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

<script setup name="callboxoperate">
import { listCallBoxOperate, addCallBoxOperate, getCallBoxOperate } from '@/api/call/callBoxOperate.js'
import { dictCallBoxBase } from '@/api/call/callBoxBase.js'
const { proxy } = getCurrentInstance()
const ids = ref([])
const loading = ref(false)
const showSearch = ref(true)

const queryParams = reactive({
  pageNum: 1,
  pageSize: 10,
  sort: '',
  sortType: 'asc',
  boxId: null
})
const columns = ref([
  { visible: true, prop: 'operateId', label: '操作记录Id' },
  { visible: true, prop: 'boxId', label: '呼叫盒ID' },
  { visible: true, prop: 'boxName', label: '呼叫盒名称' },
  { visible: true, prop: 'operateType', label: '操作类型' },
  { visible: true, prop: 'createBy', label: '创建人' },
  { visible: true, prop: 'createTime', label: '创建时间' }
])
const total = ref(0)
const dataList = ref([])
const queryRef = ref()
const defaultTime = ref([new Date(2000, 1, 1, 0, 0, 0), new Date(2000, 2, 1, 23, 59, 59)])
// 开始呼叫时间时间范围
const dateRangeCreateTime = ref([])

var dictParams = [{ dictType: 'call_box_operate_type' }]

proxy.getDicts(dictParams).then((response) => {
  response.data.forEach((element) => {
    state.options[element.dictType] = element.list
  })
})

function getList() {
  proxy.addDateRange(queryParams, dateRangeCreateTime.value, 'CreateTime')
  loading.value = true
  listCallBoxOperate(queryParams).then((res) => {
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
  // 开始呼叫时间时间范围
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
    operateId: [{ required: true, message: '操作记录Id不能为空', trigger: 'blur', type: 'number' }],
    boxId: [{ required: true, message: '盒子ID不能为空', trigger: 'blur', type: 'number' }],
    operateType: [{ required: true, message: '操作类型不能为空', trigger: 'change', type: 'number' }],
    createTime: [{ required: true, message: '创建时间不能为空', trigger: 'blur' }]
  },
  options: {
    // 操作类型 选项列表 格式 eg:{ dictLabel: '标签', dictValue: '0'}
    call_box_operate_type: [],
    // 呼叫盒选项列表
    callBoxOptions: []
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
    operateId: null,
    boxId: null,
    operateType: null,
    createBy: null,
    createTime: null
  }
  proxy.resetForm('formRef')
}

// 添加按钮操作
function handleAdd() {
  reset()
  open.value = true
  title.value = '添加盒子操作记录'
  opertype.value = 1
}

// 添加&修改 表单提交
function submitForm() {
  proxy.$refs['formRef'].validate((valid) => {
    if (valid) {
      if (form.value.operateId != undefined && opertype.value === 2) {
      } else {
        addCallBoxOperate(form.value).then((res) => {
          proxy.$modal.msgSuccess('新增成功')
          open.value = false
          getList()
        })
      }
    }
  })
}

//查询呼叫盒信息
function handleQueryCallBox(keyword) {
  if (keyword) {
    const params = {
      pageNum: 1,
      pageSize: 1000,
      sort: '',
      sortType: 'asc',
      keyword: keyword
    }
    setTimeout(() => {
      dictCallBoxBase(params).then((res) => {
        state.options.callBoxOptions = res.data.result
      })
    }, 200)
  }
}

handleQuery()
</script>
