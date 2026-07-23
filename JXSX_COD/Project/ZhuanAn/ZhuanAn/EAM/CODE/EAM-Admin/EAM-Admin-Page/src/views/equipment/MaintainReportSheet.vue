<!--
 * @Descripttion: (设备保养报表纸单/MaintainReportSheet)
 * @Author: (admin)
 * @Date: (2024-10-11)
-->
<template>
  <div>
    <el-form :model="equipment" label-position="right" inline ref="queryRef" @submit.prevent v-show="showSearch">
      <el-row :gutter="20">
        <el-form-item label="资产编号">
          <el-input v-model="equipment.assetNo" disabled />
        </el-form-item>
        <el-form-item label="资产名称">
          <el-input v-model="equipment.assetName" disabled />
        </el-form-item>
        <el-form-item label="机型">
          <el-input v-model="equipment.model" disabled />
        </el-form-item>
        <el-form-item label="资产单位">
          <el-input v-model="equipment.costCenter" disabled />
        </el-form-item>
      </el-row>
      <el-row>
        <el-form-item label="年份">
          <el-date-picker v-model="props.year" type="year" value-format="YYYY" disabled> </el-date-picker>
        </el-form-item>
        <el-form-item label="月份">
          <el-date-picker :model-value="props.year + '-' + props.month" type="month" value-format="YYYY-M" disabled> </el-date-picker>
        </el-form-item>
      </el-row>
    </el-form>
    <!-- 工具区域 -->
    <el-row :gutter="15" class="mb10">
      <el-col :span="1.5">
        <el-button type="warning" plain icon="download" @click="handleExport" v-hasPermi="['maintain:record:export']">
          {{ $t('btn.export') }}
        </el-button>
      </el-col>
      <right-toolbar v-model:showSearch="showSearch" @queryTable="getList"></right-toolbar>
    </el-row>

    <el-collapse v-model="activeNames">
      <el-collapse-item :name="item.dateMark" v-for="(item, tableIndex) in sheetPart">
        <template #title>
          <div class="collapse-title"><dict-tag :options="options.date_mark" :value="item.dateMark" /> 保养项目</div>
          <el-select
            v-if="item.dateMark == 'D'"
            @change="handleTimeMarkChange"
            @click.stop=""
            clearable
            placeholder="切换时间标记"
            v-model="timeMark">
            <el-option v-for="dict in options.time_mark" :key="dict.dictValue" :label="dict.dictLabel" :value="dict.dictValue"></el-option>
          </el-select>
        </template>
        <div>
          <el-table
            :data="item.sheetTable"
            v-loading="loading"
            ref="tablesRef"
            border
            header-cell-class-name="sheet-el-table-header-cell"
            :row-class-name="tableRowClass"
            @cell-dblclick="
              (row, col) => {
                handleCellDblclick(row, col, item.dateMark, tableIndex)
              }
            ">
            <el-table-column prop="itemName" label="保养项目" align="center" :show-overflow-tooltip="true" min-width="200" fixed="left" />
            <el-table-column
              :prop="String(col.dateMarkStamp)"
              :label="col.columnName + (item.dateMark === 'D' ? '' : options.date_mark.find((dm) => dm.dictValue === item.dateMark).dictLabel)"
              :width="col.widthPercent"
              align="center"
              v-for="col in item.partColumn">
            </el-table-column>
          </el-table>
        </div>
      </el-collapse-item>
    </el-collapse>
    <el-input v-model="monthTip" type="textarea" v-if="props.month" input-style="color:red" />
    <el-input v-model="yearTip" type="textarea" v-if="!props.month" input-style="color:red" />

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
      <template #footer v-if="opertype != 3">
        <el-button text @click="cancel">{{ $t('btn.cancel') }}</el-button>
        <el-button type="primary" @click="submitForm" v-hasPermi="['maintain:record:add']">{{ $t('btn.submit') }}</el-button>
      </template>
    </el-dialog>
  </div>
</template>

<script setup name="maintainreportsheet">
import { addMaintainRecord, updateMaintainRecord, detailMaintainRecord } from '@/api/equipment/maintainRecord.js'
import { sheetMaintainReport } from '@/api/equipment/maintainReport.js'
import useUserStore from '@/store/modules/user.js'
import useBasicStore from '@/store/modules/basic.js'

const props = defineProps({
  equipmentId: Number,
  year: String,
  month: String
})
const { proxy } = getCurrentInstance()
const ids = ref([])
const loading = ref(false)
const showSearch = ref(false)
const timeMark = ref() //日保养的时间标记下拉选
const activeNames = ref(['D', 'W', 'M', 'Q', 'Y']) //展开的面板
const equipment = ref({}) //设备的基本信息
const sheetPart = ref({}) //不同保养项目的数据，用来循环页面结构及数据
const queryRef = ref()
const tablesRef = ref([]) //表格引用对象，多个
const monthTip = ref('')
const yearTip = ref('')

var dictParams = [{ dictType: 'date_mark' }, { dictType: 'time_mark' }, { dictType: 'sys_yes_no' }]

proxy.getDicts(dictParams).then((response) => {
  response.data.forEach((element) => {
    state.options[element.dictType] = element.list
  })
})

function getList() {
  loading.value = true
  const parm = { ...props }
  parm.timeMark = timeMark.value
  sheetMaintainReport(parm).then((res) => {
    const { code, data } = res
    if (code == 200) {
      equipment.value = data.equipment
      sheetPart.value = data.sheetPart
      loading.value = false
    }
  })
}

// 单元格行样式处理
function tableRowClass({ row, rowIndex }) {
  //隐藏项目名称为ID的数据行
  if (rowIndex == 0) return 'row-hide'
}

// 查询
function handleQuery() {
  getList()
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

// 导出按钮操作
function handleExport() {
  proxy
    .$confirm('是否确认导出设备保养报表数据项?', '警告', {
      confirmButtonText: '确定',
      cancelButtonText: '取消',
      type: 'warning'
    })
    .then(async () => {
      await proxy.downFile('/equipment/MaintainReport/export', { ...props })
    })
}

/*********************设备保养记录详情子表信息*************************/
const maintainRecordDetailList = ref([])

const queryParams = ref({
  equipmentId: null,
  year: null,
  dateMark: null,
  dateMarkStamp: null
})

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
function handleCellDblclick(row, col, dateMark, tableIndex) {
  //判断是否是记录
  if (col.property == 'itemId' || col.property == 'itemName') return

  //获取记录ID
  let recordId
  if (tablesRef.value[tableIndex].data[0].itemName == 'ID') {
    recordId = tablesRef.value[tableIndex].data[0][col.property]
  }

  //参数
  queryParams.value = {
    id: recordId,
    equipmentId: props.equipmentId,
    year: props.year,
    dateMark: dateMark,
    dateMarkStamp: col.property,
    timeMark: timeMark.value
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

      //判断是修改还是新增
      if (form.value.id > 0) {
        opertype.value = 2
        title.value = '修改保养记录'
      } else {
        opertype.value = 1
        title.value = '添加保养记录'
      }
      open.value = true
    }
  })
}

//时间标记变动事件
function handleTimeMarkChange(val) {
  handleQuery()
}

//获取报表的提示配置
function initTipConfig() {
  useBasicStore()
    .getFactoryConfig('MonthMaintainTip')
    .then((it) => {
      monthTip.value = it.configValue
    })
  useBasicStore()
    .getFactoryConfig('YearMaintainTip')
    .then((it) => {
      yearTip.value = it.configValue
    })
}

//监听属性变更
watch(props, () => {
  handleQuery()
})

handleQuery()
initTipConfig()
</script>

<style lang="scss">
.collapse-title {
  color: blue;
  font-size: 16px;
}

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
