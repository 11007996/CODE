<!--
 * @Descripttion: (产品表/IOT_Product)
 * @Author: (admin)
 * @Date: (2026-01-07)
-->
<template>
  <div>
    <el-form :model="queryParams" label-position="right" inline ref="queryRef" v-show="showSearch" @submit.prevent>
      <el-form-item label="产品名称" prop="productName">
        <el-input v-model="queryParams.productName" placeholder="请输入产品名称" />
      </el-form-item>
      <el-form-item>
        <el-button icon="search" type="primary" @click="handleQuery">{{ $t('btn.search') }}</el-button>
        <el-button icon="refresh" @click="resetQuery">{{ $t('btn.reset') }}</el-button>
      </el-form-item>
    </el-form>
    <!-- 工具区域 -->
    <el-row :gutter="15" class="mb10">
      <el-col :span="1.5">
        <el-button type="primary" v-hasPermi="['iot:product:add']" plain icon="plus" @click="handleAdd">
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
      <el-table-column prop="productId" label="产品ID" align="center" width="90" v-if="columns.showColumn('productId')" />
      <el-table-column prop="productName" label="产品名称" align="center" min-width="150" v-if="columns.showColumn('productName')" />
      <el-table-column prop="nodeType" label="节点类型" align="center" width="150" v-if="columns.showColumn('nodeType')">
        <template #default="scope">
          <dict-tag :options="options.iot_product_node_type" :value="scope.row.nodeType" />
        </template>
      </el-table-column>
      <el-table-column prop="accessProtocol" label="接入协议" align="center" width="150" v-if="columns.showColumn('accessProtocol')">
        <template #default="scope">
          <dict-tag :options="options.iot_access_protocol" :value="scope.row.accessProtocol" />
        </template>
      </el-table-column>
      <el-table-column prop="dataFormat" label="数据格式" align="center" width="150" v-if="columns.showColumn('dataFormat')">
        <template #default="scope">
          <dict-tag :options="options.iot_product_data_format" :value="scope.row.dataFormat" />
        </template>
      </el-table-column>
      <el-table-column prop="version" label="版本" align="center" width="90" v-if="columns.showColumn('version')" />
      <el-table-column
        prop="description"
        label="描述"
        align="center"
        min-width="150"
        :show-overflow-tooltip="true"
        v-if="columns.showColumn('description')" />
      <el-table-column
        prop="createBy"
        label="创建人"
        align="center"
        width="90"
        :show-overflow-tooltip="true"
        v-if="columns.showColumn('createBy')" />
      <el-table-column prop="createTime" label="创建时间" width="160" v-if="columns.showColumn('createTime')" />
      <el-table-column prop="updateBy" label="更新人" align="center" width="90" v-if="columns.showColumn('updateBy')" />
      <el-table-column prop="updateTime" label="更新时间" width="160" v-if="columns.showColumn('updateTime')" />
      <el-table-column label="操作" width="220">
        <template #default="scope">
          <el-button type="primary" size="small" icon="view" title="详情" @click="handlePreview(scope.row)"></el-button>
          <el-button
            type="success"
            size="small"
            icon="edit"
            title="编辑"
            v-hasPermi="['iot:product:edit']"
            @click="handleUpdate(scope.row)"></el-button>
          <el-button
            type="danger"
            size="small"
            icon="delete"
            title="删除"
            v-hasPermi="['iot:product:delete']"
            @click="handleDelete(scope.row)"></el-button>
          <el-button type="primary" size="small" v-hasPermi="['iot:product:release']" @click="handleRelease(scope.row)">发布</el-button>
        </template>
      </el-table-column>
    </el-table>
    <pagination :total="total" v-model:page="queryParams.pageNum" v-model:limit="queryParams.pageSize" @pagination="getList" />

    <!-- 添加或修改产品表对话框 -->
    <el-dialog :title="title" :lock-scroll="false" v-model="open">
      <el-form ref="formRef" :model="form" :rules="rules" label-width="100px">
        <el-row :gutter="20">
          <!-- <el-col :lg="12" v-if="opertype != 1">
            <el-form-item label="产品ID" prop="productId">
              <el-input-number v-model.number="form.productId" controls-position="right" placeholder="请输入产品ID" :disabled="true" />
            </el-form-item>
          </el-col> -->

          <el-col :lg="12">
            <el-form-item label="产品名称" prop="productName">
              <el-input v-model="form.productName" placeholder="请输入产品名称" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="节点类型" prop="nodeType">
              <el-select v-model="form.nodeType" placeholder="请选择节点类型">
                <el-option
                  v-for="item in options.iot_product_node_type"
                  :key="item.dictValue"
                  :label="item.dictLabel"
                  :value="item.dictValue"></el-option>
              </el-select>
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="接入协议" prop="accessProtocol">
              <el-select v-model="form.accessProtocol" placeholder="请选择接入协议">
                <el-option
                  v-for="item in options.iot_access_protocol"
                  :key="item.dictValue"
                  :label="item.dictLabel"
                  :value="item.dictValue"></el-option>
              </el-select>
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="数据格式" prop="dataFormat">
              <el-select v-model="form.dataFormat" placeholder="请选择数据格式">
                <el-option
                  v-for="item in options.iot_product_data_format"
                  :key="item.dictValue"
                  :label="item.dictLabel"
                  :value="item.dictValue"></el-option>
              </el-select>
            </el-form-item>
          </el-col>

          <el-col :lg="24">
            <el-form-item label="描述" prop="description">
              <el-input type="textarea" v-model="form.description" placeholder="请输入描述" />
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

<script setup name="iotproduct">
import { listIotProduct, addIotProduct, delIotProduct, updateIotProduct, getIotProduct, releaseIotProduct } from '@/api/iot/iotProduct.js'
const { proxy } = getCurrentInstance()
const ids = ref([])
const loading = ref(false)
const showSearch = ref(true)
const queryParams = reactive({
  pageNum: 1,
  pageSize: 10,
  sort: '',
  sortType: 'asc',
  productName: undefined
})
const columns = ref([
  { visible: false, prop: 'productId', label: '产品ID' },
  { visible: true, prop: 'productName', label: '产品名称' },
  { visible: true, prop: 'nodeType', label: '节点类型' },
  { visible: true, prop: 'accessProtocol', label: '接入协议' },
  { visible: true, prop: 'dataFormat', label: '数据格式' },
  { visible: true, prop: 'version', label: '版本' },
  { visible: true, prop: 'description', label: '描述' },
  { visible: false, prop: 'createBy', label: '创建人' },
  { visible: false, prop: 'createTime', label: '创建时间' },
  { visible: false, prop: 'updateBy', label: '更新人' },
  { visible: false, prop: 'updateTime', label: '更新时间' }
])
const total = ref(0)
const dataList = ref([])
const queryRef = ref()
const defaultTime = ref([new Date(2000, 1, 1, 0, 0, 0), new Date(2000, 2, 1, 23, 59, 59)])

var dictParams = [{ dictType: 'iot_product_node_type' }, { dictType: 'iot_product_data_format' }, { dictType: 'iot_access_protocol' }]

proxy.getDicts(dictParams).then((response) => {
  response.data.forEach((element) => {
    state.options[element.dictType] = element.list
  })
})

function getList() {
  loading.value = true
  listIotProduct(queryParams).then((res) => {
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
    productName: [{ required: true, message: '产品名称不能为空', trigger: 'blur' }],
    nodeType: [{ required: true, message: '节点类型不能为空', trigger: 'change' }],
    accessProtocol: [{ required: true, message: '接入协议不能为空', trigger: 'change' }],
    dataFormat: [{ required: true, message: '数据格式不能为空', trigger: 'change' }]
  },
  options: {
    // 接入协议 选项列表 格式 eg:{ dictLabel: '标签', dictValue: '0'}
    iot_access_protocol: [],
    // 节点类型
    iot_product_node_type: [],
    // 产品数据格式
    iot_product_data_format: []
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
    productId: null,
    productName: null,
    nodeType: null,
    accessProtocol: null,
    dataFormat: null,
    description: null,
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
  title.value = '添加产品表'
  opertype.value = 1
}
// 修改按钮操作
function handleUpdate(row) {
  reset()
  const id = row.productId || ids.value
  getIotProduct(id).then((res) => {
    const { code, data } = res
    if (code == 200) {
      open.value = true
      title.value = '修改产品表'
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
      if (form.value.productId != undefined && opertype.value === 2) {
        updateIotProduct(form.value).then((res) => {
          proxy.$modal.msgSuccess('修改成功')
          open.value = false
          getList()
        })
      } else {
        addIotProduct(form.value).then((res) => {
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
  const Ids = row.productId || ids.value

  proxy
    .$confirm('是否确认删除参数编号为"' + Ids + '"的数据项？', '警告', {
      confirmButtonText: proxy.$t('common.ok'),
      cancelButtonText: proxy.$t('common.cancel'),
      type: 'warning'
    })
    .then(function () {
      return delIotProduct(Ids)
    })
    .then(() => {
      getList()
      proxy.$modal.msgSuccess('删除成功')
    })
}

// 删除按钮操作
function handleRelease(row) {
  const name = row.productName
  const id = row.productId
  proxy
    .$confirm('是否确认发布"' + name + '"？', '警告', {
      confirmButtonText: proxy.$t('common.ok'),
      cancelButtonText: proxy.$t('common.cancel'),
      type: 'warning'
    })
    .then(function () {
      return releaseIotProduct(id)
    })
    .then(() => {
      getList()
      proxy.$modal.msgSuccess('发布成功')
    })
}

const router = useRouter()
/** 打开产品详情窗口 */
function handlePreview(row) {
  const productId = row.productId
  var hasPermi = proxy.$auth.hasPermi('iot:product:query')
  if (hasPermi) {
    router.push({ path: '/iot/IotProductDetail', query: { productId } })
  } else {
    proxy.$modal.msgError('你没有权限访问')
  }
}

handleQuery()
</script>
