<!--
 * @Descripttion: (设备保养报表概况/MaintainOverview)
 * @Author: (admin)
 * @Date: (2024-10-11)
-->
<template>
  <div>
    <el-form :model="queryParams" label-position="right" inline ref="queryRef" @submit.prevent v-show="showSearch">
      <el-form-item label="年份" prop="year">
        <el-date-picker v-model="queryParams.year" type="year" value-format="YYYY"> </el-date-picker>
      </el-form-item>
      <el-form-item label="月份" prop="month">
        <el-date-picker v-model="queryParams.month" type="month" value-format="M"> </el-date-picker>
      </el-form-item>
      <el-form-item label="日期标记" prop="dateMark">
        <el-select placeholder="切换日期标记" v-model="queryParams.dateMark">
          <el-option v-for="dict in options.date_mark" :key="dict.dictValue" :label="dict.dictLabel" :value="dict.dictValue"></el-option>
        </el-select>
      </el-form-item>
      <el-form-item label="时间标记" prop="timeMark">
        <el-select clearable placeholder="切换时间标记" v-model="queryParams.timeMark" :disabled="queryParams.dateMark != 'D'">
          <el-option v-for="dict in options.time_mark" :key="dict.dictValue" :label="dict.dictLabel" :value="dict.dictValue"></el-option>
        </el-select>
      </el-form-item>
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
      <el-form-item label="成本中心" prop="costCenter">
        <el-input v-model="queryParams.costCenter" />
      </el-form-item>
      <el-form-item>
        <el-button icon="search" type="primary" @click="handleQuery">{{ $t('btn.search') }}</el-button>
        <el-button icon="refresh" @click="resetQuery">{{ $t('btn.reset') }}</el-button>
      </el-form-item>
    </el-form>
    <!-- 工具区域 -->
    <el-row :gutter="15" class="mb10">
      <el-col :span="1.5">
        <el-button type="warning" plain icon="download" @click="handleExport" v-hasPermi="['maintain:overview:export']">
          {{ $t('btn.export') }}
        </el-button>
      </el-col>
      <right-toolbar v-model:showSearch="showSearch" @queryTable="getList" :columns="columns"></right-toolbar>
    </el-row>

    <el-table
      :data="dataList"
      v-loading="loading"
      ref="tablesRef"
      border
      header-cell-class-name="sheet-el-table-header-cell"
      :row-class-name="tableRowClass"
      @cell-dblclick="handleCellDblclick">
      <el-table-column
        prop="equipmentId"
        label="设备Id"
        align="center"
        :show-overflow-tooltip="true"
        width="100"
        fixed="left"
        v-if="columns.showColumn('equipmentId')" />
      <el-table-column
        prop="equipmentName"
        label="设备名称"
        align="center"
        :show-overflow-tooltip="true"
        width="200"
        fixed="left"
        v-if="columns.showColumn('equipmentName')" />
      <el-table-column
        prop="assetNo"
        label="资产编号"
        align="center"
        :show-overflow-tooltip="true"
        width="200"
        fixed="left"
        v-if="columns.showColumn('assetNo')" />
      <!--   :label="col.columnName + (col.dateMark === 'D' ? '' : options.date_mark.find((dm) => dm.dictValue === item.dateMark).dictLabel)" -->
      <el-table-column
        v-for="col in partColumn"
        :prop="String(col.dateMarkStamp)"
        :label="col.columnName + (col.dateMark === 'D' ? '' : options.date_mark.find((dm) => dm.dictValue === col.dateMark).dictLabel)"
        align="center"
        :width="col.dateMark === 'D' ? 40 : col.dateMark === 'M' ? '60' : 'auto'"
        :formatter="formatter">
      </el-table-column>
    </el-table>
    <pagination :total="total" v-model:page="queryParams.pageNum" v-model:limit="queryParams.pageSize" @pagination="getList" />

    <!-- 添加或修改设备保养记录对话框 -->
    <el-dialog :title="title" :lock-scroll="false" v-model="open">
      <el-descriptions :column="3" title="保养信息" border>
        <el-descriptions-item label="资产编号" :span="2">{{ form.assetNo }}</el-descriptions-item>
        <el-descriptions-item label="年份">{{ form.year }}</el-descriptions-item>
        <el-descriptions-item label="日期标记">
          <dict-tag :options="options.date_mark" :value="form.dateMark" />
        </el-descriptions-item>
        <el-descriptions-item label="日期标记值">{{ form.dateMarkStamp }}</el-descriptions-item>
        <el-descriptions-item label="时间标记">
          <dict-tag :options="options.time_mark" :value="form.timeMark" />
        </el-descriptions-item>
        <el-descriptions-item label="执行时间" :span="3">{{ form.createTime }}</el-descriptions-item>
      </el-descriptions>

      <el-form ref="formRef" :model="form" :rules="rules" label-width="100px">
        <el-table :data="maintainRecordDetailList" ref="MaintainRecordDetailRef">
          <el-table-column label="保养项目ID" align="center" prop="itemId" v-if="false">
            <template #default="scope">
              <el-input v-model="scope.row.itemId" placeholder="请输入保养项目ID" />
            </template>
          </el-table-column>
          <el-table-column label="保养项目" prop="itemName" />
          <el-table-column label="保养结果" align="center" prop="itemValue" min-width="140">
            <template #default="scope">
              <div class="item-value-cell">
                <el-switch
                  v-if="scope.row.type != 'input'"
                  v-model="scope.row.itemValue"
                  inline-prompt
                  active-value="V"
                  inactive-value="X"
                  active-text="V"
                  inactive-text="X" />
                <el-input v-else v-model="scope.row.itemValue"></el-input>
                <el-link @click="handleTypeSwitch(scope.row)">{{ scope.row.type == 'input' ? '勾选' : '输入' }}</el-link>
              </div>
            </template>
          </el-table-column>
        </el-table>

        <el-row :gutter="20" class="mrg-top-10">
          <el-col :lg="24">
            <el-form-item label="保养人签名" prop="executorName">
              <el-input v-model="form.executorName" readonly input-style="text-align: right" :placeholder="'当前用户:' + useUserStore().name" />
            </el-form-item>
          </el-col>
        </el-row>
      </el-form>
    </el-dialog>
  </div>
</template>

<script setup name="maintainoverview">
import { detailMaintainRecord } from '@/api/equipment/maintainRecord.js'
import { overviewMaintainReport } from '@/api/equipment/maintainReport.js'
import useUserStore from '@/store/modules/user.js'
import { dictEquipmentBase } from '@/api/equipment/equipmentBase.js'

const { proxy } = getCurrentInstance()
const ids = ref([])
const loading = ref(false)
const showSearch = ref(true)
const queryRef = ref()
const tablesRef = ref([]) //表格引用对象，多个
const dataList = ref([])
const total = ref(0)
const partColumn = ref([])
const queryParams = reactive({
  pageNum: 1,
  pageSize: 10,
  sort: '',
  sortType: '',
  equipmentId: null,
  year: new Date().getFullYear().toString(),
  month: (new Date().getMonth() + 1).toString(),
  dateMark: 'D',
  timeMark: null
})

const columns = ref([
  { visible: false, prop: 'equipmentId', label: '设备ID' },
  { visible: true, prop: 'equipmentName', label: '资产名称' },
  { visible: true, prop: 'assetNo', label: '资产编号' }
])

var dictParams = [{ dictType: 'date_mark' }, { dictType: 'time_mark' }, { dictType: 'sys_yes_no' }]

proxy.getDicts(dictParams).then((response) => {
  response.data.forEach((element) => {
    state.options[element.dictType] = element.list
  })
})

function getList() {
  loading.value = true
  const parm = { ...queryParams }
  overviewMaintainReport(parm).then((res) => {
    const { code, data } = res
    if (code == 200) {
      total.value = data.item1.totalNum
      dataList.value = data.item2
      partColumn.value = data.item3
      loading.value = false
    }
  })
}

// 单元格行样式处理
function tableRowClass({ row, rowIndex }) {
  //隐藏项目名称为ID的数据行
  // if (rowIndex == 0) return 'row-hide'
}

// 查询
function handleQuery() {
  getList()
}

// 重置查询操作
function resetQuery() {
  proxy.resetForm('queryRef')
  queryParams.pageNum = 1
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
    year: [{ required: true, message: '年份标记不能为空', trigger: 'blur', type: 'number' }],
    dateMark: [{ required: true, message: '日期标记不能为空', trigger: 'change' }],
    dateMarkStamp: [{ required: true, message: '日期标记戳不能为空', trigger: 'blur', type: 'number' }]
  },
  options: {
    // 日期标记 选项列表 格式 eg:{ dictLabel: '标签', dictValue: '0'}
    date_mark: [],
    // 时间标记 选项列表 格式 eg:{ dictLabel: '标签', dictValue: '0'}
    time_mark: [],
    // 是否可见 选项列表 格式 eg:{ dictLabel: '标签', dictValue: '0'}
    sys_yes_no: []
  }
})

const { form, rules, options } = toRefs(state)

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
    year: null,
    dateMark: null,
    dateMarkStamp: null,
    timeMark: null
  }
  maintainRecordDetailList.value = []
  proxy.resetForm('formRef')
}

// 导出按钮操作
function handleExport() {
  proxy
    .$confirm('是否确认导出设备保养报表数据项?', '警告', {
      confirmButtonText: '确定',
      cancelButtonText: '取消',
      type: 'warning'
    })
    .then(async () => {
      await proxy.downFile('/equipment/MaintainReport/overview/export', { ...queryParams })
    })
}

/*********************设备保养记录详情子表信息*************************/
const maintainRecordDetailList = ref([])

//切换类型
function handleTypeSwitch(row) {
  if (row.type === undefined) {
    if (row.itemValue === 'V' || row.itemValue === 'X' || !row.itemValue) {
      row.type = 'switch'
    } else {
      row.type = 'input'
    }
  }
  if (row.type == 'input') {
    row.type = 'switch'
  } else {
    row.type = 'input'
  }
}

//单元格双击事件
function handleCellDblclick(row, col) {
  //判断是否是记录
  if (col.property == 'equipmentId' || col.property == 'equipmentName' || col.property == 'assetNo') return
  //获取记录ID
  let recordId = row[col.property]
  if (!recordId) return

  //参数
  queryParams.value = {
    id: recordId
  }
  reset()

  //查看详情
  detailMaintainRecord(queryParams.value).then((res) => {
    const { code, data } = res
    if (code == 200) {
      form.value = {
        ...data
      }
      res.data.maintainRecordDetailNav.forEach((item) => {
        item.type = item.itemValue == 'V' || item.itemValue == 'X' || item.itemValue == null ? 'switch' : 'input'
      })
      maintainRecordDetailList.value = res.data.maintainRecordDetailNav
      title.value = '查看保养记录'
      opertype.value = 3

      open.value = true
    }
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

//单元格式化
function formatter(row, cloumn, value) {
  if (value) return 'V'
  else return ''
}

//handleQuery()
</script>

<style lang="scss">
.sheet-el-table-header-cell {
  .cell {
    padding: 1px !important;
  }
}
.mrg-top-10 {
  margin-top: 10px;
}
.row-hide {
  display: none;
}
.item-value-cell {
  width: 100%;
  display: flex;
  justify-content: end;
  .el-input {
    flex: 1;
  }
}
</style>
