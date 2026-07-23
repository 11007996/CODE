<!--
 * @Descripttion: (呼叫盒信息/CALL_Box_Base)
 * @Author: (admin)
 * @Date: (2026-05-08)
-->
<template>
  <div>
    <el-form :model="queryParams" label-position="right" inline ref="queryRef" v-show="showSearch" @submit.prevent>
      <el-form-item label="盒子名称" prop="boxName">
        <el-input v-model="queryParams.boxName" placeholder="请输入盒子名称" />
      </el-form-item>
      <el-form-item label="MAC地址" prop="mac">
        <el-input v-model="queryParams.mac" placeholder="请输入MAC地址" />
      </el-form-item>
      <el-form-item label="IP" prop="ip">
        <el-input v-model="queryParams.ip" placeholder="请输入IP" />
      </el-form-item>

      <el-form-item label="绑定产线" prop="lineId">
        <el-select v-model="queryParams.lineId" placeholder="请选择产线" filterable clearable>
          <el-option
            v-for="item in useBasicStore().getLineDict"
            :key="item.dictValue"
            :label="item.dictLabel"
            :value="Number(item.dictValue)"></el-option>
        </el-select>
      </el-form-item>

      <el-form-item label="绑定工站" prop="stationId">
        <el-select v-model="queryParams.stationId" placeholder="请选择工站" clearable filterable remote :remote-method="handleQueryStation">
          <el-option
            v-for="item in options.stationOptions"
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
        <el-button type="primary" v-hasPermi="['call:box:base:add']" plain icon="plus" @click="handleAdd">
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
      <el-table-column prop="boxId" label="呼叫盒Id" align="center" width="90" v-if="columns.showColumn('boxId')" />
      <el-table-column prop="boxName" label="呼叫盒名称" align="center" min-width="200" v-if="columns.showColumn('boxName')" />
      <el-table-column prop="mac" label="Mac地址" align="center" width="150" v-if="columns.showColumn('mac')" />
      <el-table-column prop="ip" label="IP地址" align="center" width="150" v-if="columns.showColumn('ip')" />
      <!-- <el-table-column prop="batteryLevel" label="电量" align="center" width="90" v-if="columns.showColumn('batteryLevel')" /> -->
      <el-table-column prop="isOnline" label="状态" align="center" width="90" v-if="columns.showColumn('isOnline')">
        <template #default="scope">
          <el-switch v-model="scope.row.isOnline" active-text="在线" inactive-text="离线" inline-prompt disabled />
        </template>
      </el-table-column>
      <el-table-column prop="lastOnlineTime" label="最后在线时间" width="160" v-if="columns.showColumn('lastOnlineTime')" />
      <el-table-column prop="lineName" label="绑定产线" align="center" width="90" v-if="columns.showColumn('lineName')" />
      <el-table-column prop="stationName" label="绑定工站" align="center" width="150" v-if="columns.showColumn('stationName')" />
      <el-table-column prop="enabled" label="是否可用" align="center" width="90" v-if="columns.showColumn('enabled')">
        <template #default="scope">
          <el-switch v-model="scope.row.enabled" active-text="是" inactive-text="否" inline-prompt disabled />
        </template>
      </el-table-column>
      <el-table-column prop="createBy" label="创建人" align="center" width="90" v-if="columns.showColumn('createBy')" />
      <el-table-column prop="createTime" label="创建时间" width="160" v-if="columns.showColumn('createTime')" />
      <el-table-column prop="updateBy" label="更新人" align="center" width="90" v-if="columns.showColumn('updateBy')" />
      <el-table-column prop="updateTime" label="更新时间" width="160" v-if="columns.showColumn('updateTime')" />
      <el-table-column label="操作" width="260" fixed="right">
        <template #default="scope">
          <el-button type="primary" size="small" v-hasPermi="['call:box:operate:add']" @click="handleAddCall(scope.row)">呼叫</el-button>
          <el-button type="warning" size="small" v-hasPermi="['call:box:operate:add']" @click="handleStopCall(scope.row)">取消呼叫</el-button>
          <el-button
            type="success"
            size="small"
            icon="edit"
            title="编辑"
            v-hasPermi="['call:box:base:edit']"
            @click="handleUpdate(scope.row)"></el-button>
          <el-button
            type="danger"
            size="small"
            icon="delete"
            title="删除"
            v-hasPermi="['call:box:base:delete']"
            @click="handleDelete(scope.row)"></el-button>
        </template>
      </el-table-column>
    </el-table>
    <pagination :total="total" v-model:page="queryParams.pageNum" v-model:limit="queryParams.pageSize" @pagination="getList" />

    <!-- 添加或修改呼叫盒信息对话框 -->
    <el-dialog :title="title" :lock-scroll="false" v-model="open">
      <el-form ref="formRef" :model="form" :rules="rules" label-width="100px">
        <el-row :gutter="20">
          <el-col :lg="12">
            <el-form-item label="呼叫盒名称" prop="boxName">
              <el-input v-model="form.boxName" placeholder="请输入呼叫盒名称" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="Mac地址" prop="mac">
              <el-input v-model="form.mac" placeholder="请输入Mac地址" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="IP地址" prop="ip">
              <el-input v-model="form.ip" placeholder="请输入IP地址" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="绑定产线" prop="lineId">
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
            <el-form-item label="绑定工站" prop="stationId">
              <el-select v-model="form.stationId" clearable filterable remote :remote-method="handleQueryStation" placeholder="请选择绑定工站">
                <el-option
                  v-for="item in options.stationOptions"
                  :key="item.dictValue"
                  :label="item.dictLabel"
                  :value="parseInt(item.dictValue)"></el-option>
              </el-select>
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="是否可用" prop="enabled">
              <el-switch v-model="form.enabled" active-text="是" inactive-text="否" inline-prompt />
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

<script setup name="callboxbase">
import { listCallBoxBase, addCallBoxBase, delCallBoxBase, updateCallBoxBase, getCallBoxBase } from '@/api/call/callBoxBase.js'
import useBasicStore from '@/store/modules/basic.js'
import { dictStation } from '@/api/basic/station.js'
import { addCallBoxOperate } from '@/api/call/callBoxOperate.js'
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
  { visible: false, prop: 'boxId', label: '呼叫盒Id' },
  { visible: true, prop: 'boxName', label: '呼叫盒名称' },
  { visible: true, prop: 'mac', label: 'Mac地址' },
  { visible: true, prop: 'ip', label: 'IP地址' },
  // { visible: false, prop: 'batteryLevel', label: '电量' },
  { visible: true, prop: 'isOnline', label: '状态' },
  { visible: true, prop: 'lastOnlineTime', label: '最后在线时间' },
  { visible: true, prop: 'lineName', label: '绑定产线' },
  { visible: true, prop: 'stationName', label: '绑定工站' },
  { visible: true, prop: 'enabled', label: '是否可用' },
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
  listCallBoxBase(queryParams).then((res) => {
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
    boxId: [{ required: true, message: '呼叫盒Id不能为空', trigger: 'blur', type: 'number' }],
    boxName: [{ required: true, message: '呼叫盒名称不能为空', trigger: 'blur' }],
    mac: [{ required: true, message: 'Mac地址不能为空', trigger: 'blur' }],
    isOnline: [{ required: true, message: '状态(1:在线,0:离线）不能为空', trigger: 'blur' }],
    enabled: [{ required: true, message: '是否可用不能为空', trigger: 'blur' }],
    createBy: [{ required: true, message: '创建人不能为空', trigger: 'blur' }],
    createTime: [{ required: true, message: '创建时间不能为空', trigger: 'blur' }]
  },
  options: {
    // 绑定工站 选项列表 格式 eg:{ dictLabel: '标签', dictValue: '0'}
    stationOptions: []
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
    boxId: null,
    boxName: null,
    mac: null,
    ip: null,
    batteryLevel: null,
    isOnline: null,
    lastOnlineTime: null,
    lineId: null,
    stationId: null,
    enabled: null,
    createBy: null,
    createTime: null,
    updateBy: null,
    updateTime: null
  }
  proxy.resetForm('formRef')
}

// 添加按钮操作
function handleAdd() {
  reset()
  open.value = true
  title.value = '添加呼叫盒信息'
  opertype.value = 1
}
// 修改按钮操作
function handleUpdate(row) {
  reset()
  const id = row.boxId || ids.value
  getCallBoxBase(id).then((res) => {
    const { code, data } = res
    if (code == 200) {
      open.value = true
      title.value = '修改呼叫盒信息'
      opertype.value = 2

      form.value = {
        ...data
      }
      options.value.stationOptions = [{ dictValue: form.value.stationId, dictLabel: form.value.stationName }]
    }
  })
}

// 添加&修改 表单提交
function submitForm() {
  proxy.$refs['formRef'].validate((valid) => {
    if (valid) {
      if (form.value.boxId != undefined && opertype.value === 2) {
        updateCallBoxBase(form.value).then((res) => {
          proxy.$modal.msgSuccess('修改成功')
          open.value = false
          getList()
        })
      } else {
        addCallBoxBase(form.value).then((res) => {
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
  const Ids = row.boxId || ids.value

  proxy
    .$confirm('是否确认删除参数编号为"' + Ids + '"的数据项？', '警告', {
      confirmButtonText: proxy.$t('common.ok'),
      cancelButtonText: proxy.$t('common.cancel'),
      type: 'warning'
    })
    .then(function () {
      return delCallBoxBase(Ids)
    })
    .then(() => {
      getList()
      proxy.$modal.msgSuccess('删除成功')
    })
}

//呼叫
function handleAddCall(row) {
  const params = {
    boxId: row.boxId,
    operateType: 1
  }
  addCallBoxOperate(params).then((res) => {
    proxy.$modal.msgSuccess('呼叫成功')
  })
}

//取消呼叫
function handleStopCall(row) {
  const params = {
    boxId: row.boxId,
    operateType: 0
  }
  addCallBoxOperate(params).then((res) => {
    proxy.$modal.msgSuccess('取消呼叫成功')
  })
}

//查询工站信息
function handleQueryStation(keyword) {
  if (keyword || form.value.lineId) {
    const params = {
      pageNum: 1,
      pageSize: 1000,
      sort: '',
      sortType: 'asc',
      lineId: form.value.lineId,
      stationName: keyword
    }
    setTimeout(() => {
      dictStation(params).then((res) => {
        state.options.stationOptions = res.data.result
      })
    }, 200)
  }
}

handleQuery()
</script>
