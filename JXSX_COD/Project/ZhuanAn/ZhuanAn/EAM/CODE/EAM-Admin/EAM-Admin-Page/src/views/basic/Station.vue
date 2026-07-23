<!--
 * @Descripttion: (工站信息/BASE_Station)
 * @Author: (admin)
 * @Date: (2026-05-08)
-->
<template>
  <div>
    <el-form :model="queryParams" label-position="right" inline ref="queryRef" v-show="showSearch" @submit.prevent>
      <el-form-item label="工站名称" prop="stationName">
        <el-input v-model="queryParams.stationName" placeholder="请输入工站名称" />
      </el-form-item>
      <el-form-item label="工站编码" prop="stationCode">
        <el-input v-model="queryParams.stationCode" placeholder="请输入工站编码" />
      </el-form-item>
      <el-form-item label="所属产线" prop="lineId">
        <el-select v-model="queryParams.lineId" placeholder="请选择产线" filterable clearable>
          <el-option
            v-for="item in useBasicStore().getLineDict"
            :key="item.dictValue"
            :label="item.dictLabel"
            :value="Number(item.dictValue)"></el-option>
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
        <el-button type="primary" v-hasPermi="['station:add']" plain icon="plus" @click="handleAdd">
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
      <el-table-column prop="stationId" label="工站ID" align="center" width="90" v-if="columns.showColumn('stationId')" />
      <el-table-column prop="stationName" label="工站名称" align="center" min-width="150" v-if="columns.showColumn('stationName')" />
      <el-table-column prop="stationCode" label="工站编码" align="center" width="150" v-if="columns.showColumn('stationCode')" />
      <el-table-column prop="enabled" label="是否可用" align="center" width="90" v-if="columns.showColumn('enabled')">
        <template #default="scope">
          <el-switch v-model="scope.row.enabled" active-text="是" inactive-text="否" inline-prompt disabled />
        </template>
      </el-table-column>
      <el-table-column prop="lineName" label="所属产线" align="center" width="100" v-if="columns.showColumn('lineName')" />
      <el-table-column prop="remark" label="备注" align="center" min-width="200" v-if="columns.showColumn('remark')" />
      <el-table-column prop="createBy" label="创建人" align="center" width="90" v-if="columns.showColumn('createBy')" />
      <el-table-column prop="createTime" label="创建时间" width="160" v-if="columns.showColumn('createTime')" />
      <el-table-column prop="updateBy" label="更新人" align="center" width="90" v-if="columns.showColumn('updateBy')" />
      <el-table-column prop="updateTime" label="更新时间" width="160" v-if="columns.showColumn('updateTime')" />
      <el-table-column label="操作" width="120" fixed="right">
        <template #default="scope">
          <el-button
            type="success"
            size="small"
            icon="edit"
            title="编辑"
            v-hasPermi="['station:edit']"
            @click="handleUpdate(scope.row)"></el-button>
          <el-button
            type="danger"
            size="small"
            icon="delete"
            title="删除"
            v-hasPermi="['station:delete']"
            @click="handleDelete(scope.row)"></el-button>
        </template>
      </el-table-column>
    </el-table>
    <pagination :total="total" v-model:page="queryParams.pageNum" v-model:limit="queryParams.pageSize" @pagination="getList" />

    <!-- 添加或修改工站信息对话框 -->
    <el-dialog :title="title" :lock-scroll="false" v-model="open">
      <el-form ref="formRef" :model="form" :rules="rules" label-width="100px">
        <el-row :gutter="20">
          <el-col :lg="12">
            <el-form-item label="工站名称" prop="stationName">
              <el-input v-model="form.stationName" placeholder="请输入工站名称" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="工站编码" prop="stationCode">
              <el-input v-model="form.stationCode" placeholder="请输入工站编码" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="所属产线" prop="lineId">
              <el-select v-model="form.lineId" placeholder="请选择产线" filterable clearable>
                <el-option
                  v-for="item in useBasicStore().getLineDict"
                  :key="item.dictValue"
                  :label="item.dictLabel"
                  :value="Number(item.dictValue)"></el-option>
              </el-select>
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="是否可用" prop="enabled">
              <el-switch v-model="form.enabled" active-text="是" inactive-text="否" inline-prompt />
            </el-form-item>
          </el-col>

          <el-col :lg="24">
            <el-form-item label="备注" prop="remark">
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

<script setup name="station">
import { listStation, addStation, delStation, updateStation, getStation } from '@/api/basic/station.js'
import useBasicStore from '@/store/modules/basic.js'
const { proxy } = getCurrentInstance()
const ids = ref([])
const loading = ref(false)
const showSearch = ref(true)
const queryParams = reactive({
  pageNum: 1,
  pageSize: 10,
  sort: '',
  sortType: 'asc',
  stationName: null,
  stationCode: null,
  lineId: null
})
const columns = ref([
  { visible: false, prop: 'stationId', label: '工站ID' },
  { visible: true, prop: 'stationName', label: '工站名称' },
  { visible: true, prop: 'stationCode', label: '工站编码' },
  { visible: true, prop: 'enabled', label: '是否可用' },
  { visible: true, prop: 'lineName', label: '所属产线' },
  { visible: true, prop: 'remark', label: '备注' },
  { visible: false, prop: 'createBy', label: '创建人' },
  { visible: false, prop: 'createTime', label: '创建时间' },
  { visible: false, prop: 'updateBy', label: '更新人' },
  { visible: false, prop: 'updateTime', label: '更新时间' }
])
const total = ref(0)
const dataList = ref([])
const queryRef = ref()
const defaultTime = ref([new Date(2000, 1, 1, 0, 0, 0), new Date(2000, 2, 1, 23, 59, 59)])

var dictParams = []

function getList() {
  loading.value = true
  listStation(queryParams).then((res) => {
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
    stationId: [{ required: true, message: '工站ID不能为空', trigger: 'blur', type: 'number' }],
    stationName: [{ required: true, message: '工站名称不能为空', trigger: 'blur' }],
    stationCode: [{ required: true, message: '工站编码不能为空', trigger: 'blur' }],
    enabled: [{ required: true, message: '是否可用不能为空', trigger: 'blur' }],
    createBy: [{ required: true, message: '创建人不能为空', trigger: 'blur' }],
    createTime: [{ required: true, message: '创建时间不能为空', trigger: 'blur' }]
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
    stationId: null,
    stationName: null,
    stationCode: null,
    enabled: null,
    lineId: null,
    remark: null
  }
  proxy.resetForm('formRef')
}

// 添加按钮操作
function handleAdd() {
  reset()
  open.value = true
  title.value = '添加工站信息'
  opertype.value = 1
}
// 修改按钮操作
function handleUpdate(row) {
  reset()
  const id = row.stationId || ids.value
  getStation(id).then((res) => {
    const { code, data } = res
    if (code == 200) {
      open.value = true
      title.value = '修改工站信息'
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
      if (form.value.stationId != undefined && opertype.value === 2) {
        updateStation(form.value).then((res) => {
          proxy.$modal.msgSuccess('修改成功')
          open.value = false
          getList()
        })
      } else {
        addStation(form.value).then((res) => {
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
  const Ids = row.stationId

  proxy
    .$confirm('是否确认删除参数编号为"' + Ids + '"的数据项？', '警告', {
      confirmButtonText: proxy.$t('common.ok'),
      cancelButtonText: proxy.$t('common.cancel'),
      type: 'warning'
    })
    .then(function () {
      return delStation(Ids)
    })
    .then(() => {
      getList()
      proxy.$modal.msgSuccess('删除成功')
    })
}

handleQuery()
</script>
