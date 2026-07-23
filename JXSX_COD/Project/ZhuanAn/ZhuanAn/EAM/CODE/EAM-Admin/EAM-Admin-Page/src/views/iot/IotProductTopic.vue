<!--
 * @Descripttion: (产品主题类表/IOT_Product_Topic)
 * @Author: (admin)
 * @Date: (2026-01-07)
-->
<template>
  <div>
    <el-form :model="queryParams" label-position="right" inline ref="queryRef" v-show="showSearch" @submit.prevent>
      <!-- <el-form-item label="产品ID" prop="productId">
        <el-select clearable v-model="queryParams.productId" placeholder="请选择产品ID">
          <el-option v-for="item in options.productIdOptions" :key="item.dictValue" :label="item.dictLabel" :value="item.dictValue">
            <span class="fl">{{ item.dictLabel }}</span>
            <span class="fr" style="color: var(--el-text-color-secondary)">{{ item.dictValue }}</span>
          </el-option>
        </el-select>
      </el-form-item> -->
      <el-form-item label="主题名称" prop="topicName">
        <el-input v-model="queryParams.topicName" placeholder="请输入主题名称" />
      </el-form-item>
      <el-form-item>
        <el-button icon="search" type="primary" @click="handleQuery">{{ $t('btn.search') }}</el-button>
        <el-button icon="refresh" @click="resetQuery">{{ $t('btn.reset') }}</el-button>
      </el-form-item>
    </el-form>
    <!-- 工具区域 -->
    <el-row :gutter="15" class="mb10">
      <el-col :span="1.5">
        <el-button type="primary" v-hasPermi="['iot:product:topic:add']" plain icon="plus" @click="handleAdd">
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
      <el-table-column prop="topicId" label="主题ID" align="center" width="90" v-if="columns.showColumn('topicId')" />
      <el-table-column prop="productId" label="产品ID" align="center" width="90" v-if="columns.showColumn('productId')" />
      <el-table-column prop="topicName" label="主题名称" align="center" width="150" v-if="columns.showColumn('topicName')" />
      <el-table-column prop="topicFormat" label="主题格式" min-width="250" :show-overflow-tooltip="true" v-if="columns.showColumn('topicFormat')" />
      <el-table-column prop="operation" label="操作" align="center" width="90" v-if="columns.showColumn('operation')">
        <template #default="scope">
          <dict-tag :options="options.iot_topic_operation" :value="scope.row.operation" />
        </template>
      </el-table-column>
      <el-table-column
        prop="description"
        label="描述"
        align="center"
        min-width="150"
        :show-overflow-tooltip="true"
        v-if="columns.showColumn('description')" />
      <el-table-column prop="enabled" label="是否可用" align="center" width="90" v-if="columns.showColumn('enabled')">
        <template #default="scope">
          <el-switch v-model="scope.row.enabled" active-text="是" inactive-text="否" inline-prompt disabled />
        </template>
      </el-table-column>
      <el-table-column prop="createBy" label="创建人" align="center" width="90" v-if="columns.showColumn('createBy')" />
      <el-table-column prop="createTime" label="创建时间" align="center" width="160" v-if="columns.showColumn('createTime')" />
      <el-table-column prop="updateBy" label="更新人" align="center" width="90" v-if="columns.showColumn('updateBy')" />
      <el-table-column prop="updateTime" label="更新时间" align="center" width="160" v-if="columns.showColumn('updateTime')" />
      <el-table-column label="操作" width="120">
        <template #default="scope">
          <el-button
            type="success"
            size="small"
            icon="edit"
            title="编辑"
            v-hasPermi="['iot:product:topic:edit']"
            @click="handleUpdate(scope.row)"></el-button>
          <el-button
            type="danger"
            size="small"
            icon="delete"
            title="删除"
            v-hasPermi="['iot:product:topic:delete']"
            @click="handleDelete(scope.row)"></el-button>
        </template>
      </el-table-column>
    </el-table>
    <pagination :total="total" v-model:page="queryParams.pageNum" v-model:limit="queryParams.pageSize" @pagination="getList" />

    <!-- 添加或修改产品主题类表对话框 -->
    <el-dialog :title="title" :lock-scroll="false" v-model="open">
      <el-form ref="formRef" :model="form" :rules="rules" label-width="100px">
        <el-row :gutter="20">
          <!-- <el-col :lg="12" v-if="opertype != 1">
            <el-form-item label="主题ID" prop="topicId">
              <el-input-number v-model.number="form.topicId" controls-position="right" placeholder="请输入主题ID" :disabled="true" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="产品ID" prop="productId">
              <el-select v-model="form.productId" placeholder="请选择产品ID">
                <el-option
                  v-for="item in options.productIdOptions"
                  :key="item.dictValue"
                  :label="item.dictLabel"
                  :value="parseInt(item.dictValue)"></el-option>
              </el-select>
            </el-form-item>
          </el-col> -->

          <el-col :lg="12">
            <el-form-item label="主题名称" prop="topicName">
              <el-input v-model="form.topicName" placeholder="请输入主题名称" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="操作" prop="operation">
              <el-select v-model="form.operation" placeholder="请选择操作">
                <el-option
                  v-for="item in options.iot_topic_operation"
                  :key="item.dictValue"
                  :label="item.dictLabel"
                  :value="item.dictValue"></el-option>
              </el-select>
            </el-form-item>
          </el-col>

          <el-col :lg="24">
            <el-form-item label="主题格式" prop="topicFormat">
              <el-input v-model="form.topicFormat" placeholder="请输入主题格式" />
            </el-form-item>
          </el-col>

          <el-col :lg="24">
            <el-form-item label="描述" prop="description">
              <el-input type="textarea" v-model="form.description" placeholder="请输入描述" />
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

<script setup name="iotproducttopic">
import {
  listIotProductTopic,
  addIotProductTopic,
  delIotProductTopic,
  updateIotProductTopic,
  getIotProductTopic
} from '@/api/iot/iotProductTopic.js'
const props = defineProps({
  productId: Number
})

const { proxy } = getCurrentInstance()
const ids = ref([])
const loading = ref(false)
const showSearch = ref(false)
const queryParams = reactive({
  pageNum: 1,
  pageSize: 10,
  sort: 'TopicFormat',
  sortType: 'asc',
  productId: undefined,
  topicName: undefined
})
const columns = ref([
  { visible: false, prop: 'topicId', label: '主题ID' },
  { visible: false, prop: 'productId', label: '产品ID' },
  { visible: true, prop: 'topicName', label: '主题名称' },
  { visible: true, prop: 'topicFormat', label: '主题格式' },
  { visible: true, prop: 'operation', label: '操作' },
  { visible: true, prop: 'description', label: '描述' },
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

var dictParams = [{ dictType: 'iot_topic_operation' }]

proxy.getDicts(dictParams).then((response) => {
  response.data.forEach((element) => {
    state.options[element.dictType] = element.list
  })
})

function getList() {
  loading.value = true
  queryParams.productId = props.productId
  listIotProductTopic(queryParams).then((res) => {
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
    productId: [{ required: true, message: '产品ID不能为空', trigger: 'change', type: 'number' }],
    topicName: [{ required: true, message: '主题名称不能为空', trigger: 'blur' }],
    topicFormat: [{ required: true, message: '主题格式不能为空', trigger: 'blur' }],
    operation: [{ required: true, message: '操作不能为空', trigger: 'change' }],
    enabled: [{ required: true, message: '是否可用不能为空', trigger: 'blur' }]
  },
  options: {
    // 产品ID 选项列表 格式 eg:{ dictLabel: '标签', dictValue: '0'}
    product_options: [],
    // 主题操作方式
    iot_topic_operation: []
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
    topicId: null,
    productId: null,
    topicName: null,
    topicFormat: null,
    operation: null,
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
  title.value = '添加产品主题类表'
  opertype.value = 1
}
// 修改按钮操作
function handleUpdate(row) {
  reset()
  const id = row.topicId || ids.value
  getIotProductTopic(id).then((res) => {
    const { code, data } = res
    if (code == 200) {
      open.value = true
      title.value = '修改产品主题类表'
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
      if (form.value.topicId != undefined && opertype.value === 2) {
        updateIotProductTopic(form.value).then((res) => {
          proxy.$modal.msgSuccess('修改成功')
          open.value = false
          getList()
        })
      } else {
        form.value.productId = props.productId
        addIotProductTopic(form.value).then((res) => {
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
  const Ids = row.topicId || ids.value

  proxy
    .$confirm('是否确认删除参数编号为"' + Ids + '"的数据项？', '警告', {
      confirmButtonText: proxy.$t('common.ok'),
      cancelButtonText: proxy.$t('common.cancel'),
      type: 'warning'
    })
    .then(function () {
      return delIotProductTopic(Ids)
    })
    .then(() => {
      getList()
      proxy.$modal.msgSuccess('删除成功')
    })
}

watch(
  () => props.productId,
  (newValue, oldValue) => {
    handleQuery()
  }
)

handleQuery()
</script>
