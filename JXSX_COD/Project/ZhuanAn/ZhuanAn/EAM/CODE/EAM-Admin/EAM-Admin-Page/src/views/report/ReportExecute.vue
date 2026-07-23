<!--
 * @Descripttion: (报表执行/rep_report_execute)
 * @Author: (admin)
 * @Date: (2026-03-05)
-->
<template>
  <div>
    <el-row :gutter="5">
      <el-col :sm="5">
        <el-form-item label="分组">
          <el-select @change="handleGroupChange" placeholder="请选择分组">
            <el-option
              v-for="item in options.group_options"
              :key="item.dictValue"
              :label="item.dictLabel"
              :value="parseInt(item.dictValue)"></el-option>
          </el-select>
        </el-form-item>

        <el-table
          :data="reportList"
          border
          header-cell-class-name="el-table-header-cell"
          max-height="1000"
          :highlight-current-row="true"
          @row-click="handleRowClick">
          <el-table-column prop="dictLabel" label="报表" :show-overflow-tooltip="true" />
        </el-table>
      </el-col>

      <el-col :sm="19">
        <!-- 查询表单 -->
        <el-form label-position="right" inline ref="queryRef" v-show="showSearch" @submit.prevent>
          <template v-for="item in reportParams">
            <el-form-item :label="item.paramLabel" :required="item.required">
              <el-input v-if="item.elementType == 'input'" v-model="queryParams.jsonParams[item.paramKey]" />
              <el-input-number v-if="item.elementType == 'number'" v-model="queryParams.jsonParams[item.paramKey]" />
              <el-switch
                v-if="item.elementType == 'switch'"
                v-model="queryParams.jsonParams[item.paramKey]"
                active-text="是"
                inactive-text="否"
                inline-prompt />
              <el-date-picker
                v-if="item.elementType == 'datetime'"
                type="datetime"
                value-format="YYYY-MM-DD HH:mm:ss"
                v-model="queryParams.jsonParams[item.paramKey]"></el-date-picker>
              <el-date-picker
                v-if="item.elementType == 'date'"
                type="date"
                value-format="YYYY-MM-DD"
                v-model="queryParams.jsonParams[item.paramKey]"></el-date-picker>
              <el-select
                v-if="item.elementType == 'select'"
                v-model="queryParams.jsonParams[item.paramKey]"
                clearable
                filterable
                remote
                :remote-method="(keyword) => queryParamOptions(keyword, item.paramKey)">
                <el-option v-for="item in options[item.paramKey]" :key="item.dictValue" :label="item.dictLabel" :value="item.dictValue"></el-option>
              </el-select>
              <el-select
                v-if="item.elementType == 'multiSelect'"
                v-model="queryParams.jsonParams[item.paramKey]"
                clearable
                filterable
                multiple
                remote
                :remote-method="(keyword) => queryParamOptions(keyword, item.paramKey)">
                <el-option v-for="item in options[item.paramKey]" :key="item.dictValue" :label="item.dictLabel" :value="item.dictValue"></el-option>
              </el-select>
            </el-form-item>
          </template>
        </el-form>
        <!-- 工具区域 -->
        <el-row :gutter="15" class="mb10">
          <el-col :span="1.5">
            <el-button type="primary" v-hasPermi="['rep:report:execute:query']" plain icon="search" @click="handleQuery">
              {{ $t('btn.search') }}
            </el-button>
            <el-button icon="refresh" @click="resetQuery">{{ $t('btn.reset') }}</el-button>
          </el-col>
          <el-col :span="1.5">
            <el-button type="warning" plain icon="download" v-hasPermi="['rep:report:execute:export']" @click="handleExport">
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
          <template v-for="item in reportColumns">
            <el-table-column
              :prop="item.columnKey"
              :label="item.columnLabel"
              :width="item.width"
              :sortable="item.isSort === true ? 'custom' : false"
              v-if="columns.showColumn(item.columnKey)" />
          </template>
        </el-table>
        <pagination :total="total" v-model:page="queryParams.pageNum" v-model:limit="queryParams.pageSize" @pagination="getList" />
      </el-col>
    </el-row>
  </div>
</template>

<script setup name="reportexecute">
import { dictReportBase } from '@/api/report/reportBase.js'
import { dictReportGroup } from '@/api/report/reportGroup.js'
import { getReportInfo, listReportExecute, exportReportExecute, getReportParamOptions } from '@/api/report/reportExecute.js'
const { proxy } = getCurrentInstance()

//---------------------报表基本信息-----------------------
const reportList = ref([])

//查询报表分组
function handleQueryReportGroup() {
  const params = {
    pageNum: 1,
    pageSize: 100,
    sort: '',
    sortType: 'asc'
  }
  setTimeout(() => {
    dictReportGroup(params).then((res) => {
      options.value.group_options = res.data.result
    })
  }, 200)
}

//分组下拉变化事件
function handleGroupChange(val) {
  resetReport()
  const params = {
    pageNum: 1,
    pageSize: 1000,
    sort: '',
    sortType: 'asc',
    groupId: val
  }
  dictReportBase(params).then((res) => {
    const { code, data } = res
    if (code == 200) {
      reportList.value = data.result
    }
  })
}

//报表信息行单击事件
function handleRowClick(row) {
  resetReport()
  queryParams.reportId = parseInt(row.dictValue)
  getReportInfo(queryParams.reportId).then((res) => {
    const { code, data } = res
    if (code == 200) {
      reportParams.value = data.params
      reportColumns.value = data.columns
      columns.value = data.columns.map((item) => ({ visible: item.isVisible, prop: item.columnKey, label: item.columnLabel }))
      data.params.forEach((p) => {
        //下拉选初始化
        if (p.optionsSource) {
          getReportParamOptions(null, p.paramKey)
        }

        //默认值处理
        if (p.defaultValue) {
          switch (p.inputType) {
            case 'int':
              queryParams.jsonParams[p.paramKey] = parseInt(p.defaultValue)
              break
            case 'long':
              queryParams.jsonParams[p.paramKey] = BigInt(p.defaultValue)
              break
            case 'double':
              queryParams.jsonParams[p.paramKey] = Number(p.defaultValue)
              break
            case 'bool':
              queryParams.jsonParams[p.paramKey] = p.defaultValue.toLowerCase() === 'true'
              break
            case 'date':
            case 'datetime':
              queryParams.jsonParams[p.paramKey] = new Date(p.defaultValue)
              break
            default:
              queryParams.jsonParams[p.paramKey] = p.defaultValue
              break
          }
        }
      })
    }
  })
}

//重置所有的内容
function resetReport() {
  loading.value = false
  //查询参数
  queryParams.pageNum = 1
  queryParams.pageSize = 10
  queryParams.sort = null
  queryParams.sortType = null
  queryParams.reportId = null
  queryParams.jsonParams = {}
  //报表动态参数
  reportParams.value = []
  //报表动态数据列
  reportColumns.value = []
  //报表动态列属性
  columns.value = []
  //报表数据
  dataList.value = []
  //数据总计
  total.value = 0
}

//-----------------------动态报表-----------------------
const loading = ref(false)
const showSearch = ref(true)
const queryParams = reactive({
  pageNum: 1,
  pageSize: 10,
  sort: '',
  sortType: 'asc',
  reportId: null,
  jsonParams: {}
})
const reportParams = ref([])
const reportColumns = ref([])
const columns = ref([
  //{ visible: false, prop: 'reportId', label: '报表ID' },
])
//下接选
const options = ref([])

const total = ref(0)
const dataList = ref([])
const queryRef = ref()
const defaultTime = ref([new Date(2000, 1, 1, 0, 0, 0), new Date(2000, 2, 1, 23, 59, 59)])
var dictParams = []

function getList() {
  loading.value = true
  const params = { ...queryParams }
  params.jsonParams = JSON.stringify(queryParams.jsonParams)
  listReportExecute(params)
    .then((res) => {
      const { code, data } = res
      if (code == 200) {
        dataList.value = data.result[0]
        total.value = data.totalNum
        loading.value = false
      } else {
        loading.value = false
      }
    })
    .catch((err) => {
      console.log(err)
      loading.value = false
    })
}

// 自定义排序
function sortChange(column) {
  if (column.prop == null || column.order == null) {
    queryParams.sort = undefined
    queryParams.sortType = undefined
  } else {
    queryParams.sort = column.prop
    queryParams.sortType = column.order == 'ascending' ? 'ASC' : 'DESC'
  }

  handleQuery()
}

// 查询
function handleQuery() {
  queryParams.pageNum = 1
  getList()
}

// 重置查询操作
function resetQuery() {
  queryParams.pageNum = 1
  queryParams.jsonParams = {}
  dataList.value = []
}

//获取查询组件的options内容
function queryParamOptions(keyword, paramKey) {
  const parm = {
    pageNum: 1,
    pageSize: 10,
    sort: '',
    sortType: 'asc',
    reportId: queryParams.reportId,
    paramKey: paramKey,
    keyword: keyword
  }
  getReportParamOptions(parm).then((res) => {
    const { code, data } = res
    if (code == 200) {
      options.value[paramKey] = data
    }
  })
}

// 导出按钮操作
function handleExport() {
  const params = { ...queryParams }
  params.jsonParams = JSON.stringify(queryParams.jsonParams)
  proxy
    .$confirm('是否确认导出报表数据项?', '警告', {
      confirmButtonText: '确定',
      cancelButtonText: '取消',
      type: 'warning'
    })
    .then(async () => {
      await proxy.downFile('report/ReportExecute/export', params)
    })
}

handleQueryReportGroup()
</script>
