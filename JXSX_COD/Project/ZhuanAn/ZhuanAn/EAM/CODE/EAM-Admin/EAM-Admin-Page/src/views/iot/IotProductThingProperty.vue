<!--
 * @Descripttion: (产品物模型属性/IOT_Product_Thing_Property)
 * @Author: (admin)
 * @Date: (2026-01-07)
-->
<template>
  <div>
    <el-form :model="queryParams" label-position="right" inline ref="queryRef" v-show="showSearch" @submit.prevent>
      <el-form-item label="属性名称" prop="propertyName">
        <el-input v-model="queryParams.propertyName" placeholder="请输入属性名称" />
      </el-form-item>
      <el-form-item label="属性标识" prop="identifier">
        <el-input v-model="queryParams.identifier" placeholder="请输入属性标识" />
      </el-form-item>
      <el-form-item>
        <el-button icon="search" type="primary" @click="handleQuery">{{ $t('btn.search') }}</el-button>
        <el-button icon="refresh" @click="resetQuery">{{ $t('btn.reset') }}</el-button>
      </el-form-item>
    </el-form>
    <!-- 工具区域 -->
    <el-row :gutter="15" class="mb10">
      <el-col :span="1.5">
        <el-button type="primary" v-hasPermi="['iot:product:thing:property:add']" plain icon="plus" @click="handleAdd">
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
      <el-table-column prop="propertyId" label="属性ID" align="center" width="90" v-if="columns.showColumn('propertyId')" />
      <el-table-column prop="productId" label="产品ID" align="center" width="90" v-if="columns.showColumn('productId')" />
      <el-table-column
        prop="propertyName"
        label="属性名称"
        align="center"
        width="120"
        :show-overflow-tooltip="true"
        v-if="columns.showColumn('propertyName')" />
      <el-table-column
        prop="identifier"
        label="属性标识"
        align="center"
        width="120"
        :show-overflow-tooltip="true"
        v-if="columns.showColumn('identifier')" />
      <el-table-column prop="dataType" label="数据类型" align="center" width="90" v-if="columns.showColumn('dataType')">
        <template #default="scope">
          <dict-tag :options="options.iot_product_data_type" :value="scope.row.dataType" />
        </template>
      </el-table-column>
      <el-table-column prop="rwFlag" label="读写标记" align="center" width="90" v-if="columns.showColumn('rwFlag')">
        <template #default="scope">
          <dict-tag :options="options.iot_product_property_rw" :value="scope.row.rwFlag" />
        </template>
      </el-table-column>
      <el-table-column
        prop="expandDesc"
        label="扩展描述"
        align="center"
        min-width="150"
        :show-overflow-tooltip="true"
        v-if="columns.showColumn('expandDesc')" />
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
      <el-table-column prop="createTime" label="创建时间" width="160" v-if="columns.showColumn('createTime')" />
      <el-table-column prop="updateBy" label="更新人" align="center" width="90" v-if="columns.showColumn('updateBy')" />
      <el-table-column prop="updateTime" label="更新时间" width="160" v-if="columns.showColumn('updateTime')" />
      <el-table-column label="操作" width="190">
        <template #default="scope">
          <el-button
            type="success"
            size="small"
            icon="edit"
            title="编辑"
            v-hasPermi="['iot:product:thing:property:edit']"
            @click="handleOpenExtend(scope.row)"
            >扩展</el-button
          >
          <el-button
            type="success"
            size="small"
            icon="edit"
            title="编辑"
            v-hasPermi="['iot:product:thing:property:edit']"
            @click="handleUpdate(scope.row)"></el-button>
          <el-button
            type="danger"
            size="small"
            icon="delete"
            title="删除"
            v-hasPermi="['iot:product:thing:property:delete']"
            @click="handleDelete(scope.row)"></el-button>
        </template>
      </el-table-column>
    </el-table>
    <pagination :total="total" v-model:page="queryParams.pageNum" v-model:limit="queryParams.pageSize" @pagination="getList" />

    <!-- 添加或修改产品物模型属性对话框 -->
    <el-dialog :title="title" :lock-scroll="false" v-model="open">
      <el-form ref="formRef" :model="form" :rules="rules" label-width="100px">
        <el-row :gutter="20">
          <el-col :lg="12">
            <el-form-item label="属性名称" prop="propertyName">
              <el-input v-model="form.propertyName" placeholder="请输入属性名称" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="属性标识" prop="identifier">
              <el-input v-model="form.identifier" placeholder="请输入属性标识" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="数据类型" prop="dataType">
              <el-select v-model="form.dataType" placeholder="请选择数据类型">
                <el-option
                  v-for="item in options.iot_product_data_type"
                  :key="item.dictValue"
                  :label="item.dictLabel"
                  :value="item.dictValue"></el-option>
              </el-select>
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="读写标记" prop="rwFlag">
              <el-radio-group v-model="form.rwFlag">
                <el-radio v-for="item in options.iot_product_property_rw" :value="item.dictValue" :label="item.dictValue">
                  {{ item.dictLabel }}
                </el-radio>
              </el-radio-group>
            </el-form-item>
          </el-col>

          <!-- <el-col :lg="12">
            <el-form-item label="上限" prop="upperLimit">
              <el-input-number v-model.number="form.upperLimit" :controls="true" controls-position="right" placeholder="请输入上限" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="下限" prop="lowerLimit">
              <el-input-number v-model.number="form.lowerLimit" :controls="true" controls-position="right" placeholder="请输入下限" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="步长" prop="stepSize">
              <el-input-number v-model.number="form.stepSize" :controls="true" controls-position="right" placeholder="请输入步长" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="单位" prop="unit">
              <el-input v-model="form.unit" placeholder="请输入单位" />
            </el-form-item>
          </el-col> -->

          <!-- <el-col :lg="24">
            <el-form-item label="扩展描述" prop="expandDesc">
              <el-input type="textarea" v-model="form.expandDesc" placeholder="请输入扩展描述" />
            </el-form-item>
          </el-col> -->

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

    <!-- 属性扩展 -->
    <IotProductThingPropertyExtend :property-id="propertyId" v-if="openExpend" @closeForm="handleCloseExpendForm" />
  </div>
</template>

<script setup name="iotproductthingproperty">
import {
  listIotProductThingProperty,
  addIotProductThingProperty,
  delIotProductThingProperty,
  updateIotProductThingProperty,
  getIotProductThingProperty
} from '@/api/iot/iotProductThingProperty.js'
import IotProductThingPropertyExtend from './IotProductThingPropertyExtend.vue'
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
  sort: '',
  sortType: 'asc',
  productId: undefined,
  propertyName: undefined,
  identifier: undefined
})
const columns = ref([
  { visible: false, prop: 'propertyId', label: '属性ID' },
  { visible: false, prop: 'productId', label: '产品ID' },
  { visible: true, prop: 'propertyName', label: '属性名称' },
  { visible: true, prop: 'identifier', label: '属性标识' },
  { visible: true, prop: 'dataType', label: '数据类型' },
  { visible: true, prop: 'rwFlag', label: '读写标记' },
  { visible: false, prop: 'expandDesc', label: '扩展描述' },
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

var dictParams = [{ dictType: 'iot_product_data_type' }, { dictType: 'iot_product_property_rw' }]

proxy.getDicts(dictParams).then((response) => {
  response.data.forEach((element) => {
    state.options[element.dictType] = element.list
  })
})

function getList() {
  loading.value = true
  queryParams.productId = props.productId
  listIotProductThingProperty(queryParams).then((res) => {
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
  expendForm: {},
  rules: {
    productId: [{ required: true, message: '产品ID不能为空', trigger: 'change', type: 'number' }],
    propertyName: [{ required: true, message: '属性名称不能为空', trigger: 'blur' }],
    identifier: [{ required: true, message: '属性标识不能为空', trigger: 'blur' }],
    dataType: [{ required: true, message: '数据类型不能为空', trigger: 'change' }],
    rwFlag: [{ required: true, message: '读写标记不能为空', trigger: 'blur' }],
    enabled: [{ required: true, message: '是否可用不能为空', trigger: 'blur' }],
    createBy: [{ required: true, message: '创建人不能为空', trigger: 'blur' }],
    createTime: [{ required: true, message: '创建时间不能为空', trigger: 'blur' }]
  },
  options: {
    // 数据类型 选项列表 格式 eg:{ dictLabel: '标签', dictValue: '0'}
    iot_product_data_type: [],
    // 读写类型 选项列表 格式 eg:{ dictLabel: '标签', dictValue: '0'}
    iot_product_property_rw: []
  }
})

const { form, expendForm, rules, options, single, multiple } = toRefs(state)

// 关闭dialog
function cancel() {
  open.value = false
  reset()
}

// 重置表单
function reset() {
  form.value = {
    propertyId: null,
    productId: null,
    propertyName: null,
    identifier: null,
    dataType: null,
    rwFlag: null,
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
  title.value = '添加产品物模型属性'
  opertype.value = 1
}
// 修改按钮操作
function handleUpdate(row) {
  reset()
  const id = row.propertyId || ids.value
  getIotProductThingProperty(id).then((res) => {
    const { code, data } = res
    if (code == 200) {
      open.value = true
      title.value = '修改产品物模型属性'
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
      if (form.value.propertyId != undefined && opertype.value === 2) {
        updateIotProductThingProperty(form.value).then((res) => {
          proxy.$modal.msgSuccess('修改成功')
          open.value = false
          getList()
        })
      } else {
        form.value.productId = props.productId
        addIotProductThingProperty(form.value).then((res) => {
          proxy.$modal.msgSuccess('新增成功')
          open.value = false
          getList()
        })
      }
    }
  })
}

/*************** 属性扩展操作 ***************/
const propertyId = ref(0)
const openExpend = ref(false)
function handleOpenExtend(row) {
  propertyId.value = row.propertyId
  openExpend.value = true
}

function handleCloseExpendForm() {
  openExpend.value = false
}

// 删除按钮操作
function handleDelete(row) {
  const Ids = row.propertyId || ids.value

  proxy
    .$confirm('是否确认删除参数编号为"' + Ids + '"的数据项？', '警告', {
      confirmButtonText: proxy.$t('common.ok'),
      cancelButtonText: proxy.$t('common.cancel'),
      type: 'warning'
    })
    .then(function () {
      return delIotProductThingProperty(Ids)
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
