<!--
 * @Descripttion: (广播区域/CALL_Area)
 * @Author: (admin)
 * @Date: (2025-07-30)
-->
<template>
  <div>
    <el-form :model="queryParams" label-position="right" inline ref="queryRef" v-show="showSearch" @submit.prevent>
      <el-form-item label="区域名称" prop="areaName">
        <el-input v-model="queryParams.areaName" placeholder="请输入区域名称" />
      </el-form-item>
      <el-form-item>
        <el-button icon="search" type="primary" @click="handleQuery">{{ $t('btn.search') }}</el-button>
        <el-button icon="refresh" @click="resetQuery">{{ $t('btn.reset') }}</el-button>
      </el-form-item>
    </el-form>
    <!-- 工具区域 -->
    <el-row :gutter="15" class="mb10">
      <el-col :span="1.5">
        <el-button type="primary" v-hasPermi="['call:area:add']" plain icon="plus" @click="handleAdd">
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
      <el-table-column prop="areaId" label="区域ID" width="90" align="center" v-if="columns.showColumn('areaId')" />
      <el-table-column prop="areaName" label="区域名称" align="center" :show-overflow-tooltip="true" v-if="columns.showColumn('areaName')" />
      <el-table-column label="操作" width="160">
        <template #default="scope">
          <el-button
            type="success"
            size="small"
            icon="edit"
            title="编辑"
            v-hasPermi="['call:area:edit']"
            @click="handleUpdate(scope.row)"></el-button>
          <el-button
            type="danger"
            size="small"
            icon="delete"
            title="删除"
            v-hasPermi="['call:area:delete']"
            @click="handleDelete(scope.row)"></el-button>
        </template>
      </el-table-column>
    </el-table>
    <pagination :total="total" v-model:page="queryParams.pageNum" v-model:limit="queryParams.pageSize" @pagination="getList" />

    <el-drawer v-model="drawer" size="50%" direction="rtl">
      <el-table :data="callAreaLineList" header-row-class-name="text-navy">
        <el-table-column prop="areaId" label="区域Id" />
        <el-table-column prop="lineId" label="产线ID" />
        <el-table-column prop="lineName" label="产线名称" />
      </el-table>
    </el-drawer>

    <!-- 添加或修改广播区域对话框 -->
    <el-dialog :title="title" :lock-scroll="false" v-model="open" :fullscreen="fullScreen">
      <el-form ref="formRef" :model="form" :rules="rules" label-width="100px">
        <el-row :gutter="20">
          <el-col :lg="12" v-if="opertype != 1">
            <el-form-item label="区域ID" prop="areaId">
              <el-input-number v-model.number="form.areaId" controls-position="right" placeholder="请输入区域ID" :disabled="true" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="区域名称" prop="areaName">
              <el-input v-model="form.areaName" placeholder="请输入区域名称" />
            </el-form-item>
          </el-col>
        </el-row>
        <el-divider content-position="center">区域关联产线</el-divider>
        <el-row :gutter="10" class="mb8">
          <el-col :span="1.5">
            <el-button type="primary" icon="Plus" @click="handleAddCallAreaLine">添加</el-button>
          </el-col>
          <el-col :span="1.5">
            <el-button type="danger" icon="Delete" @click="handleDeleteCallAreaLine">删除</el-button>
          </el-col>
          <el-col :span="1.5">
            <el-button type="info" icon="FullScreen" @click="fullScreen = !fullScreen">{{ fullScreen ? '退出全屏' : '全屏' }}</el-button>
          </el-col>
        </el-row>
        <el-table
          :data="callAreaLineList"
          :row-class-name="rowCallAreaLineIndex"
          @selection-change="handleCallAreaLineSelectionChange"
          ref="CallAreaLineRef">
          <el-table-column type="selection" width="40" align="center" />
          <el-table-column label="序号" align="center" prop="index" width="60" />
          <el-table-column label="产线" prop="lineId">
            <template #default="scope">
              <el-select v-model="scope.row.lineId" placeholder="请选择产线" filterable class="fullWidth">
                <el-option
                  v-for="item in useBasicStore().getLineDict"
                  :key="item.dictValue"
                  :label="item.dictLabel"
                  :value="Number(item.dictValue)"></el-option>
              </el-select>
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

<script setup name="callarea">
import { listCallArea, addCallArea, delCallArea, updateCallArea, getCallArea } from '@/api/call/callArea.js'
import useBasicStore from '@/store/modules/basic.js'
const { proxy } = getCurrentInstance()
const ids = ref([])
const loading = ref(false)
const showSearch = ref(true)
const queryParams = reactive({
  pageNum: 1,
  pageSize: 10,
  sort: '',
  sortType: 'asc'
})
const columns = ref([
  { visible: true, prop: 'areaId', label: '区域ID' },
  { visible: true, prop: 'areaName', label: '区域名称' }
])
const total = ref(0)
const dataList = ref([])
const queryRef = ref()
const defaultTime = ref([new Date(2000, 1, 1, 0, 0, 0), new Date(2000, 2, 1, 23, 59, 59)])

var dictParams = []

function getList() {
  loading.value = true
  listCallArea(queryParams).then((res) => {
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
  form: {},
  rules: {
    areaName: [{ required: true, message: '区域名称不能为空', trigger: 'blur' }]
  },
  options: {}
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
    areaId: null,
    areaName: null
  }
  callAreaLineList.value = []
  proxy.resetForm('formRef')
}

// 添加按钮操作
function handleAdd() {
  reset()
  open.value = true
  title.value = '添加广播区域'
  opertype.value = 1
}
// 修改按钮操作
function handleUpdate(row) {
  reset()
  const id = row.areaId || ids.value
  getCallArea(id).then((res) => {
    const { code, data } = res
    if (code == 200) {
      open.value = true
      title.value = '修改广播区域'
      opertype.value = 2

      callAreaLineList.value = res.data.callAreaLineNav
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
      form.value.callAreaLineNav = callAreaLineList.value
      if (form.value.areaId != undefined && opertype.value === 2) {
        updateCallArea(form.value).then((res) => {
          proxy.$modal.msgSuccess('修改成功')
          open.value = false
          getList()
        })
      } else {
        addCallArea(form.value).then((res) => {
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
  const Ids = row.areaId || ids.value

  proxy
    .$confirm('是否确认删除参数编号为"' + Ids + '"的数据项？', '警告', {
      confirmButtonText: proxy.$t('common.ok'),
      cancelButtonText: proxy.$t('common.cancel'),
      type: 'warning'
    })
    .then(function () {
      return delCallArea(Ids)
    })
    .then(() => {
      getList()
      proxy.$modal.msgSuccess('删除成功')
    })
}

/*********************产线区域绑定子表信息*************************/
const callAreaLineList = ref([])
const checkedCallAreaLine = ref([])
const fullScreen = ref(false)
const drawer = ref(false)

/** 产线区域绑定序号 */
function rowCallAreaLineIndex({ row, rowIndex }) {
  row.index = rowIndex + 1
}

/** 产线区域绑定添加按钮操作 */
function handleAddCallAreaLine() {
  let obj = {}
  //下面的代码自己设置默认值
  callAreaLineList.value.push(obj)
}

/** 复选框选中数据 */
function handleCallAreaLineSelectionChange(selection) {
  checkedCallAreaLine.value = selection.map((item) => item.index)
}

/** 产线区域绑定删除按钮操作 */
function handleDeleteCallAreaLine() {
  if (checkedCallAreaLine.value.length == 0) {
    proxy.$modal.msgError('请先选择要删除的产线区域绑定数据')
  } else {
    const CallAreaLines = callAreaLineList.value
    const checkedCallAreaLines = checkedCallAreaLine.value
    callAreaLineList.value = CallAreaLines.filter(function (item) {
      return checkedCallAreaLines.indexOf(item.index) == -1
    })
  }
}

/** 产线区域绑定详情 */
function rowClick(row) {
  const id = row.areaId || ids.value
  getCallArea(id).then((res) => {
    const { code, data } = res
    if (code == 200) {
      drawer.value = true
      callAreaLineList.value = data.callAreaLineNav
    }
  })
}

handleQuery()
</script>
