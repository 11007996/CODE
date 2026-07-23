<!--
 * @Descripttion: (产品物模型服务/IOT_Product_Thing_Service)
 * @Author: (admin)
 * @Date: (2026-01-07)
-->
<template>
  <div>
    <el-form :model="queryParams" label-position="right" inline ref="queryRef" v-show="showSearch" @submit.prevent>
      <el-form-item label="产品ID" prop="productId">
        <el-input v-model.number="queryParams.productId" placeholder="请输入产品ID" />
      </el-form-item>
      <el-form-item label="服务名称" prop="serviceName">
        <el-input v-model="queryParams.serviceName" placeholder="请输入服务名称" />
      </el-form-item>
      <el-form-item label="服务标识" prop="identifier">
        <el-input v-model="queryParams.identifier" placeholder="请输入服务标识" />
      </el-form-item>
      <el-form-item>
        <el-button icon="search" type="primary" @click="handleQuery">{{ $t('btn.search') }}</el-button>
        <el-button icon="refresh" @click="resetQuery">{{ $t('btn.reset') }}</el-button>
      </el-form-item>
    </el-form>
    <!-- 工具区域 -->
    <el-row :gutter="15" class="mb10">
      <el-col :span="1.5">
        <el-button type="primary" v-hasPermi="['iot:product:thing:service:add']" plain icon="plus" @click="handleAdd">
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
      <el-table-column prop="serviceId" label="服务ID" align="center" v-if="columns.showColumn('serviceId')" />
      <el-table-column prop="productId" label="产品ID" align="center" v-if="columns.showColumn('productId')" />
      <el-table-column prop="serviceName" label="服务名称" align="center" :show-overflow-tooltip="true" v-if="columns.showColumn('serviceName')" />
      <el-table-column prop="identifier" label="服务标识" align="center" :show-overflow-tooltip="true" v-if="columns.showColumn('identifier')" />
      <el-table-column prop="invokeMode" label="调用方式" align="center" v-if="columns.showColumn('invokeMode')">
        <template #default="scope">
          <dict-tag :options="options.invokeModeOptions" :value="scope.row.invokeMode" />
        </template>
      </el-table-column>
      <el-table-column prop="inputParams" label="输入参数" align="center" :show-overflow-tooltip="true" v-if="columns.showColumn('inputParams')" />
      <el-table-column
        prop="outputParams"
        label="输出参数"
        align="center"
        :show-overflow-tooltip="true"
        v-if="columns.showColumn('outputParams')" />
      <el-table-column prop="expandDesc" label="扩展描述" align="center" :show-overflow-tooltip="true" v-if="columns.showColumn('expandDesc')" />
      <el-table-column prop="description" label="描述" align="center" :show-overflow-tooltip="true" v-if="columns.showColumn('description')" />
      <el-table-column prop="enabled" label="是否可用" align="center" v-if="columns.showColumn('enabled')" />
      <el-table-column prop="createBy" label="创建人" align="center" :show-overflow-tooltip="true" v-if="columns.showColumn('createBy')" />
      <el-table-column prop="createTime" label="创建时间" :show-overflow-tooltip="true" v-if="columns.showColumn('createTime')" />
      <el-table-column prop="updateBy" label="更新人" align="center" :show-overflow-tooltip="true" v-if="columns.showColumn('updateBy')" />
      <el-table-column prop="updateTime" label="更新时间" :show-overflow-tooltip="true" v-if="columns.showColumn('updateTime')" />
      <el-table-column label="操作" width="160">
        <template #default="scope">
          <el-button
            type="success"
            size="small"
            icon="edit"
            title="编辑"
            v-hasPermi="['iot:product:thing:service:edit']"
            @click="handleUpdate(scope.row)"></el-button>
          <el-button
            type="danger"
            size="small"
            icon="delete"
            title="删除"
            v-hasPermi="['iot:product:thing:service:delete']"
            @click="handleDelete(scope.row)"></el-button>
        </template>
      </el-table-column>
    </el-table>
    <pagination :total="total" v-model:page="queryParams.pageNum" v-model:limit="queryParams.pageSize" @pagination="getList" />

    <!-- 添加或修改产品物模型服务对话框 -->
    <el-dialog :title="title" :lock-scroll="false" v-model="open">
      <el-form ref="formRef" :model="form" :rules="rules" label-width="100px">
        <el-row :gutter="20">
          <el-col :lg="12" v-if="opertype != 1">
            <el-form-item label="服务ID" prop="serviceId">
              <el-input-number v-model.number="form.serviceId" controls-position="right" placeholder="请输入服务ID" :disabled="true" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="产品ID" prop="productId">
              <el-input v-model.number="form.productId" placeholder="请输入产品ID" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="服务名称" prop="serviceName">
              <el-input v-model="form.serviceName" placeholder="请输入服务名称" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="服务标识" prop="identifier">
              <el-input v-model="form.identifier" placeholder="请输入服务标识" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="调用方式" prop="invokeMode">
              <el-radio-group v-model="form.invokeMode">
                <el-radio v-for="item in options.invokeModeOptions" :key="item.dictValue" :label="item.dictValue">
                  {{ item.dictLabel }}
                </el-radio>
              </el-radio-group>
            </el-form-item>
          </el-col>

          <el-col :lg="24">
            <el-form-item label="输入参数" prop="inputParams">
              <el-input type="textarea" v-model="form.inputParams" placeholder="请输入输入参数" />
            </el-form-item>
          </el-col>

          <el-col :lg="24">
            <el-form-item label="输出参数" prop="outputParams">
              <el-input type="textarea" v-model="form.outputParams" placeholder="请输入输出参数" />
            </el-form-item>
          </el-col>

          <el-col :lg="24">
            <el-form-item label="扩展描述" prop="expandDesc">
              <el-input type="textarea" v-model="form.expandDesc" placeholder="请输入扩展描述" />
            </el-form-item>
          </el-col>

          <el-col :lg="24">
            <el-form-item label="描述" prop="description">
              <el-input type="textarea" v-model="form.description" placeholder="请输入描述" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="是否可用" prop="enabled">
              <el-switch v-model="form.enabled" :active-value="1" :inactive-value="0" />
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

          <el-col :lg="12">
            <el-form-item label="更新人" prop="updateBy">
              <el-input v-model="form.updateBy" placeholder="请输入更新人" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="更新时间" prop="updateTime">
              <el-date-picker v-model="form.updateTime" type="datetime" :teleported="false" placeholder="选择日期时间"></el-date-picker>
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

<script setup name="iotproductthingservice">
import {
  listIotProductThingService,
  addIotProductThingService,
  delIotProductThingService,
  updateIotProductThingService,
  getIotProductThingService
} from '@/api/iot/iotProductThingService.js'
const { proxy } = getCurrentInstance()
const ids = ref([])
const loading = ref(false)
const showSearch = ref(true)
const queryParams = reactive({
  pageNum: 1,
  pageSize: 10,
  sort: '',
  sortType: 'asc',
  productId: undefined,
  serviceName: undefined,
  identifier: undefined
})
const columns = ref([
  { visible: true, prop: 'serviceId', label: '服务ID' },
  { visible: true, prop: 'productId', label: '产品ID' },
  { visible: true, prop: 'serviceName', label: '服务名称' },
  { visible: true, prop: 'identifier', label: '服务标识' },
  { visible: true, prop: 'invokeMode', label: '调用方式' },
  { visible: true, prop: 'inputParams', label: '输入参数' },
  { visible: true, prop: 'outputParams', label: '输出参数' },
  { visible: true, prop: 'expandDesc', label: '扩展描述' },
  { visible: false, prop: 'description', label: '描述' },
  { visible: false, prop: 'enabled', label: '是否可用' },
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
  listIotProductThingService(queryParams).then((res) => {
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
    productId: [{ required: true, message: '产品ID不能为空', trigger: 'blur', type: 'number' }],
    serviceName: [{ required: true, message: '服务名称不能为空', trigger: 'blur' }],
    identifier: [{ required: true, message: '服务标识不能为空', trigger: 'blur' }],
    enabled: [{ required: true, message: '是否可用不能为空', trigger: 'blur' }],
    createBy: [{ required: true, message: '创建人不能为空', trigger: 'blur' }],
    createTime: [{ required: true, message: '创建时间不能为空', trigger: 'blur' }]
  },
  options: {
    // 调用方式 选项列表 格式 eg:{ dictLabel: '标签', dictValue: '0'}
    invokeModeOptions: []
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
    serviceId: null,
    productId: null,
    serviceName: null,
    identifier: null,
    invokeMode: null,
    inputParams: null,
    outputParams: null,
    expandDesc: null,
    description: null,
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
  title.value = '添加产品物模型服务'
  opertype.value = 1
}
// 修改按钮操作
function handleUpdate(row) {
  reset()
  const id = row.serviceId || ids.value
  getIotProductThingService(id).then((res) => {
    const { code, data } = res
    if (code == 200) {
      open.value = true
      title.value = '修改产品物模型服务'
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
      if (form.value.serviceId != undefined && opertype.value === 2) {
        updateIotProductThingService(form.value).then((res) => {
          proxy.$modal.msgSuccess('修改成功')
          open.value = false
          getList()
        })
      } else {
        addIotProductThingService(form.value).then((res) => {
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
  const Ids = row.serviceId || ids.value

  proxy
    .$confirm('是否确认删除参数编号为"' + Ids + '"的数据项？', '警告', {
      confirmButtonText: proxy.$t('common.ok'),
      cancelButtonText: proxy.$t('common.cancel'),
      type: 'warning'
    })
    .then(function () {
      return delIotProductThingService(Ids)
    })
    .then(() => {
      getList()
      proxy.$modal.msgSuccess('删除成功')
    })
}

handleQuery()
</script>
