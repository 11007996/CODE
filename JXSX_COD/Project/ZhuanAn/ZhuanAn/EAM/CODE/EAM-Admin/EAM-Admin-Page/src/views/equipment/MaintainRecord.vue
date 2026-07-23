<!--
 * @Descripttion: (设备保养记录/EQU_Maintain_Record)
 * @Author: (admin)
 * @Date: (2024-10-11)
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
      <el-form-item label="年份" prop="year">
        <el-date-picker v-model="queryParams.year" type="year" value-format="YYYY" placeholder="选择年份"> </el-date-picker>
      </el-form-item>
      <el-form-item label="日期标记" prop="dateMark">
        <el-select v-model="queryParams.dateMark" placeholder="请选择日期标记">
          <el-option v-for="item in options.date_mark" :key="item.dictValue" :label="item.dictLabel" :value="item.dictValue"></el-option>
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
        <el-button type="primary" v-hasPermi="['maintain:record:add']" plain icon="plus" @click="handleAdd">
          {{ $t('btn.add') }}
        </el-button>
      </el-col>
      <el-col :span="1.5">
        <el-button type="warning" plain icon="download" @click="handleExport" v-hasPermi="['maintain:record:export']">
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
      <el-table-column align="center" width="90">
        <template #default="scope">
          <el-button text @click="rowClick(scope.row)">{{ $t('btn.details') }}</el-button>
        </template>
      </el-table-column>
      <el-table-column prop="id" label="Id" align="center" width="90" v-if="columns.showColumn('id')" />
      <el-table-column prop="equipmentId" label="设备ID" width="90" align="center" v-if="columns.showColumn('equipmentId')" />
      <el-table-column
        prop="assetNo"
        label="资产编号"
        align="center"
        width="200"
        :show-overflow-tooltip="true"
        v-if="columns.showColumn('assetNo')" />
      <el-table-column prop="assetName" label="资产名称" width="150" :show-overflow-tooltip="true" v-if="columns.showColumn('assetName')" />
      <el-table-column prop="year" label="年份标记" align="center" width="90" v-if="columns.showColumn('year')" />
      <el-table-column prop="dateMark" label="日期标记" align="center" width="90" v-if="columns.showColumn('dateMark')">
        <template #default="scope">
          <dict-tag :options="options.date_mark" :value="scope.row.dateMark" />
        </template>
      </el-table-column>
      <el-table-column prop="dateMarkStamp" label="日期标记戳" align="center" width="100" v-if="columns.showColumn('dateMarkStamp')" />
      <el-table-column prop="timeMark" label="时间标记" align="center" width="90" v-if="columns.showColumn('timeMark')">
        <template #default="scope">
          <dict-tag :options="options.time_mark" :value="scope.row.timeMark" />
        </template>
      </el-table-column>
      <el-table-column prop="executorId" label="执行人工号" align="center" :show-overflow-tooltip="true" v-if="columns.showColumn('executorId')" />
      <el-table-column
        prop="executorName"
        label="执行人名称"
        align="center"
        :show-overflow-tooltip="true"
        v-if="columns.showColumn('executorName')" />
      <el-table-column
        prop="createTime"
        label="执行时间"
        align="center"
        width="160"
        :show-overflow-tooltip="true"
        v-if="columns.showColumn('createTime')" />
      <el-table-column prop="updateBy" label="更新人员" align="center" :show-overflow-tooltip="true" v-if="columns.showColumn('updateBy')" />
      <el-table-column
        prop="updateTime"
        label="更新时间"
        align="center"
        width="160"
        :show-overflow-tooltip="true"
        v-if="columns.showColumn('updateTime')" />
      <el-table-column prop="isVisible" label="是否可见" align="center" v-if="columns.showColumn('isVisible')">
        <template #default="scope">
          <dict-tag :options="options.sys_yes_no" :value="scope.row.isVisible" />
        </template>
      </el-table-column>
      <el-table-column label="操作" width="160">
        <template #default="scope">
          <el-button type="primary" size="small" icon="view" title="详情" @click="handlePreview(scope.row)"></el-button>
          <el-button
            type="success"
            size="small"
            icon="edit"
            title="编辑"
            v-hasPermi="['maintain:record:edit']"
            @click="handleUpdate(scope.row)"></el-button>
          <el-button
            type="danger"
            size="small"
            icon="delete"
            title="删除"
            v-hasPermi="['maintain:record:delete']"
            @click="handleDelete(scope.row)"></el-button>
        </template>
      </el-table-column>
    </el-table>
    <pagination :total="total" v-model:page="queryParams.pageNum" v-model:limit="queryParams.pageSize" @pagination="getList" />

    <el-drawer v-model="drawer" size="50%" direction="rtl">
      <el-table :data="maintainRecordDetailList" header-row-class-name="text-navy">
        <el-table-column prop="itemName" label="保养项目" />
        <el-table-column prop="itemValue" label="保养结果" />
      </el-table>
    </el-drawer>

    <!-- 添加或修改设备保养记录对话框 -->
    <el-dialog :title="title" :lock-scroll="false" v-model="open">
      <el-form ref="formRef" :model="form" :rules="rules" label-width="100px">
        <el-row :gutter="20">
          <el-col :lg="12" v-if="opertype != 1">
            <el-form-item label="Id" prop="id">
              <el-input-number v-model.number="form.id" controls-position="right" placeholder="请输入Id" :disabled="true" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="设备" prop="equipmentId">
              <el-select
                v-model="form.equipmentId"
                placeholder="资产编号/设备名称/资产名称/自定义机型"
                clearable
                filterable
                remote
                :remote-method="handleQueryEquipment"
                :disabled="opertype == 2"
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
            <el-form-item label="日期标记" prop="dateMark">
              <el-select v-model="form.dateMark" placeholder="请选择日期标记" :disabled="opertype == 2">
                <el-option
                  v-for="item in options.date_mark"
                  :key="item.dictValue"
                  :label="item.dictLabel"
                  :value="item.dictValue"
                  :disabled="opertype == 2"></el-option>
              </el-select>
            </el-form-item>
          </el-col>

          <el-col :lg="12" v-if="opertype == 1">
            <el-form-item label="保养日期" prop="maintainDate">
              <el-date-picker v-model="form.maintainDate" type="date" value-format="YYYY-MM-DD"> </el-date-picker>
            </el-form-item>
          </el-col>

          <el-col :lg="12" v-if="opertype != 1">
            <el-form-item label="年份标记" prop="year">
              <el-date-picker v-model="form.year" type="year" value-format="YYYY" placeholder="选择年" :disabled="opertype == 2"> </el-date-picker>
            </el-form-item>
          </el-col>
          <el-col :lg="12" v-if="opertype != 1">
            <el-form-item label="日期标记戳" prop="dateMarkStamp">
              <el-input v-model="form.dateMarkStamp" disabled />
            </el-form-item>
          </el-col>

          <el-col :lg="12" v-if="form.dateMark == 'D'">
            <el-form-item label="时间标记" prop="timeMark">
              <el-select v-model="form.timeMark" clearable placeholder="请选择时间标记" :disabled="opertype == 2">
                <el-option
                  v-for="item in options.time_mark"
                  :key="item.dictValue"
                  :label="item.dictLabel"
                  :value="item.dictValue"
                  :disabled="opertype == 2"></el-option>
              </el-select>
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="执行人" prop="executorId">
              <el-select
                v-model="form.executorId"
                placeholder="请输入员工姓名"
                clearable
                filterable
                remote
                :remote-method="handleQueryEmployee"
                @change="handleChangeEmployee">
                <el-option
                  v-for="item in options.emp_options"
                  :key="item.dictValue"
                  :label="item.dictValue + ' - ' + item.dictLabel"
                  :value="item.dictValue"></el-option>
              </el-select>
            </el-form-item>
          </el-col>
        </el-row>
        <el-divider content-position="center">设备保养记录详情信息</el-divider>
        <el-table :data="maintainRecordDetailList" :row-class-name="rowMaintainRecordDetailIndex" ref="MaintainRecordDetailRef">
          <el-table-column label="保养项目" align="center" prop="itemName" />
          <el-table-column label="保养结果" align="center" prop="itemValue" width="100">
            <template #default="scope">
              <el-switch
                v-model="scope.row.itemValue"
                inline-prompt
                active-value="V"
                inactive-value="X"
                active-text="V"
                inactive-text="X"
                :disabled="opertype == 3" />
            </template>
          </el-table-column>
        </el-table>
      </el-form>
      <template #footer v-if="opertype != 3">
        <el-button text @click="cancel">{{ $t('btn.cancel') }}</el-button>
        <el-button type="primary" @click="submitForm">{{ $t('btn.submit') }}</el-button>
      </template>
    </el-dialog>
  </div>
</template>

<script setup name="maintainrecord">
import {
  listMaintainRecord,
  addMaintainRecord,
  delMaintainRecord,
  updateMaintainRecord,
  getMaintainRecord,
  detailMaintainRecord
} from '@/api/equipment/maintainRecord.js'
import { dictEquipmentBase } from '@/api/equipment/equipmentBase.js'
import { dictEmployee } from '@/api/basic/employee.js'

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
  year: new Date().getFullYear().toString(),
  dateMark: undefined
})
const columns = ref([
  { visible: false, prop: 'id', label: 'Id' },
  { visible: false, prop: 'equipmentId', label: '设备ID' },
  { visible: true, prop: 'assetNo', label: '资产编号' },
  { visible: true, prop: 'assetName', label: '资产名称' },
  { visible: true, prop: 'year', label: '年份标记' },
  { visible: true, prop: 'dateMark', label: '日期标记' },
  { visible: true, prop: 'dateMarkStamp', label: '日期标记戳' },
  { visible: false, prop: 'executorId', label: '执行人工号' },
  { visible: true, prop: 'executorName', label: '执行人名称' },
  { visible: true, prop: 'createTime', label: '执行时间' },
  { visible: false, prop: 'updateBy', label: '更新人员' },
  { visible: false, prop: 'updateTime', label: '更新时间' },
  { visible: false, prop: 'isVisible', label: '是否可见' }
])
const total = ref(0)
const dataList = ref([])
const queryRef = ref()

var dictParams = [{ dictType: 'date_mark' }, { dictType: 'time_mark' }, { dictType: 'sys_yes_no' }]

proxy.getDicts(dictParams).then((response) => {
  response.data.forEach((element) => {
    state.options[element.dictType] = element.list
  })
})

function getList() {
  loading.value = true
  listMaintainRecord(queryParams).then((res) => {
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
    equipmentId: [{ required: true, message: '设备不能为空', trigger: 'blur' }],
    year: [{ required: true, message: '年份标记不能为空', trigger: 'blur' }],
    dateMark: [{ required: true, message: '日期标记不能为空', trigger: 'change' }],
    maintainDate: [{ required: true, message: '保养日期不能为空', trigger: 'blur' }],
    dateMarkStamp: [{ required: true, message: '日期标记戳不能为空', trigger: 'blur', type: 'number' }],
    isVisible: [{ required: true, message: '是否可见不能为空', trigger: 'blur' }],
    executorId: [{ required: true, message: '执行人不能为空', trigger: 'blur' }]
  },
  options: {
    // 日期标记 选项列表 格式 eg:{ dictLabel: '标签', dictValue: '0'}
    date_mark: [],
    // 时间标记 选项列表 格式 eg:{ dictLabel: '标签', dictValue: '0'}
    date_mark: [],
    // 是否可见 选项列表 格式 eg:{ dictLabel: '标签', dictValue: '0'}
    sys_yes_no: [],
    // 设备选项
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
    year: new Date().getFullYear().toString(),
    dateMark: null,
    dateMarkStamp: null,
    executorId: null,
    executorName: null,
    isVisible: 'Y'
  }
  maintainRecordDetailList.value = []
  proxy.resetForm('formRef')
}

/**
 * 查看
 * @param {*} row
 */
function handlePreview(row) {
  reset()
  const id = row.id
  getMaintainRecord(id).then((res) => {
    const { code, data } = res
    if (code == 200) {
      open.value = true
      title.value = '查看'
      opertype.value = 3

      if (data.year) data.year = String(data.year)
      form.value = {
        ...data
      }
      if (form.value.year) form.value.year = String(form.value.year)
      options.value.emp_options = [{ dictValue: form.value.executorId, dictLabel: form.value.executorName }]
      options.value.equipment_options = [{ dictValue: form.value.equipmentId, dictLabel: form.value.assetNo + ' : ' + form.value.assetName }]
      maintainRecordDetailList.value = res.data.maintainRecordDetailNav
    }
  })
}

// 添加按钮操作
function handleAdd() {
  reset()
  open.value = true
  title.value = '添加设备保养记录'
  opertype.value = 1
}
// 修改按钮操作
function handleUpdate(row) {
  reset()
  const id = row.id || ids.value
  getMaintainRecord(id).then((res) => {
    const { code, data } = res
    if (code == 200) {
      open.value = true
      title.value = '修改设备保养记录'
      opertype.value = 2

      if (data.year) data.year = String(data.year)
      form.value = {
        ...data
      }
      options.value.emp_options = [{ dictValue: form.value.executorId, dictLabel: form.value.executorName }]
      options.value.equipment_options = [{ dictValue: form.value.equipmentId, dictLabel: form.value.assetNo + ' : ' + form.value.assetName }]
      maintainRecordDetailList.value = res.data.maintainRecordDetailNav
    }
  })
}

// 添加&修改 表单提交
function submitForm() {
  proxy.$refs['formRef'].validate((valid) => {
    if (valid) {
      form.value.maintainRecordDetailNav = maintainRecordDetailList.value
      if (form.value.id != undefined && opertype.value === 2) {
        updateMaintainRecord(form.value).then((res) => {
          proxy.$modal.msgSuccess('修改成功')
          open.value = false
          getList()
        })
      } else {
        addMaintainRecord(form.value).then((res) => {
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
      return delMaintainRecord(Ids)
    })
    .then(() => {
      getList()
      proxy.$modal.msgSuccess('删除成功')
    })
}

// 导出按钮操作
function handleExport() {
  proxy
    .$confirm('是否确认导出设备保养记录数据项?', '警告', {
      confirmButtonText: '确定',
      cancelButtonText: '取消',
      type: 'warning'
    })
    .then(async () => {
      await proxy.downFile('/equipment/MaintainRecord/export', { ...queryParams })
    })
}

/*********************设备保养记录详情子表信息*************************/
const maintainRecordDetailList = ref([])
const drawer = ref(false)

/** 设备保养记录详情序号 */
function rowMaintainRecordDetailIndex({ row, rowIndex }) {
  row.index = rowIndex + 1
}

/** 设备保养记录详情详情 */
function rowClick(row) {
  const id = row.id || ids.value
  getMaintainRecord(id).then((res) => {
    const { code, data } = res
    if (code == 200) {
      drawer.value = true
      maintainRecordDetailList.value = data.maintainRecordDetailNav
    }
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

watch(
  () => [form.value.equipmentId, form.value.dateMark, form.value.maintainDate, form.value.timeMark],
  (newValue, oldValue) => {
    //表示新增
    if (opertype.value == 1 && form.value.equipmentId && form.value.dateMark && form.value.maintainDate) {
      //参数
      let param = {
        equipmentId: form.value.equipmentId,
        year: form.value.year,
        dateMark: form.value.dateMark,
        maintainDate: form.value.maintainDate,
        timeMark: form.value.timeMark
      }
      //查看详情
      detailMaintainRecord(param).then((res) => {
        const { code, data } = res
        if (code == 200) {
          maintainRecordDetailList.value = res.data.maintainRecordDetailNav

          //判断是否存在记录
          if (data.id) proxy.$modal.msgError('已存在记录，不可新增')
        }
      })
    }
  }
)

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

// 员工更改
function handleChangeEmployee(value) {
  const empDict = options.value.emp_options.find((item) => item.dictValue == value)
  form.value.executorName = empDict.dictLabel
}

handleQuery()
</script>
