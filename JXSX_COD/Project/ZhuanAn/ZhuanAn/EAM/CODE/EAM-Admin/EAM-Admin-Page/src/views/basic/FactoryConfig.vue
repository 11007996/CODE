<!--
 * @Descripttion: (厂区配置/BASE_Factory_Config)
 * @Author: (admin)
 * @Date: (2025-09-24)
-->
<template>
  <div>
    <el-form :model="queryParams" label-position="right" inline ref="queryRef" v-show="showSearch" @submit.prevent>
      <el-form-item>
        <el-button icon="search" type="primary" @click="handleQuery">{{ $t('btn.search') }}</el-button>
        <el-button icon="refresh" @click="resetQuery">{{ $t('btn.reset') }}</el-button>
      </el-form-item>
    </el-form>
    <!-- 工具区域 -->
    <el-row :gutter="15" class="mb10">
      <el-col :span="1.5">
        <el-button type="primary" v-hasPermi="['factory:config:add']" plain icon="plus" @click="handleAdd">
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
      <el-table-column prop="id" label="主键ID" align="center" width="90" v-if="columns.showColumn('id')" />
      <el-table-column prop="configKey" label="配置键名Key" align="center" />
      <el-table-column prop="configKey" label="配置键名" align="center" min-widht="200" v-if="columns.showColumn('configKey')">
        <template #default="scope">
          <dict-tag :options="options.factory_config_key" :value="scope.row.configKey" />
        </template>
      </el-table-column>
      <el-table-column prop="configValue" label="配置键值" align="center" :show-overflow-tooltip="true" v-if="columns.showColumn('configValue')" />
      <el-table-column prop="configType" label="配置值类型" align="center" width="100" v-if="columns.showColumn('configType')">
        <template #default="scope">
          <dict-tag :options="options.configTypeOptions" :value="scope.row.configType" />
        </template>
      </el-table-column>
      <el-table-column prop="category" label="配置分类" align="center" width="100" v-if="columns.showColumn('category')">
        <template #default="scope">
          <dict-tag :options="options.factory_config_category" :value="scope.row.category" />
        </template>
      </el-table-column>
      <el-table-column prop="description" label="配置描述" align="center" :show-overflow-tooltip="true" v-if="columns.showColumn('description')" />
      <el-table-column prop="enableFlag" label="是否启用" align="center" v-if="columns.showColumn('enableFlag')">
        <template #default="scope">
          <el-switch
            v-model="scope.row.enableFlag"
            active-value="Y"
            inactive-value="N"
            active-text="是"
            inactive-text="否"
            inline-prompt
            disabled></el-switch>
        </template>
      </el-table-column>
      <el-table-column label="操作" width="160">
        <template #default="scope">
          <el-button
            type="success"
            size="small"
            icon="edit"
            title="编辑"
            v-hasPermi="['factory:config:edit']"
            @click="handleUpdate(scope.row)"></el-button>
          <el-button
            type="danger"
            size="small"
            icon="delete"
            title="删除"
            v-hasPermi="['factory:config:delete']"
            @click="handleDelete(scope.row)"></el-button>
        </template>
      </el-table-column>
    </el-table>
    <pagination :total="total" v-model:page="queryParams.pageNum" v-model:limit="queryParams.pageSize" @pagination="getList" />

    <!-- 添加或修改厂区配置对话框 -->
    <el-dialog :title="title" :lock-scroll="false" v-model="open">
      <el-form ref="formRef" :model="form" :rules="rules" label-width="100px">
        <el-row :gutter="20">
          <!-- <el-col :lg="12">
            <el-form-item label="主键ID" prop="id">
              <el-input v-model.number="form.id" placeholder="请输入主键ID" :disabled="opertype != 1" />
            </el-form-item>
          </el-col> -->

          <el-col :lg="12">
            <el-form-item label="配置键名" prop="configKey">
              <el-select v-model="form.configKey" placeholder="请选择配置键名">
                <el-option
                  v-for="item in options.factory_config_key"
                  :key="item.dictValue"
                  :label="item.dictLabel"
                  :value="item.dictValue"></el-option>
              </el-select>
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="是否可用" prop="enableFlag">
              <el-switch
                v-model="form.enableFlag"
                active-value="Y"
                inactive-value="N"
                active-text="是"
                inactive-text="否"
                inline-prompt></el-switch>
            </el-form-item>
          </el-col>

          <el-col :lg="24">
            <el-form-item label="配置键值" prop="configValue">
              <el-input type="textarea" v-model="form.configValue" placeholder="请输入配置键值" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="配置值类型" prop="configType">
              <el-select v-model="form.configType" placeholder="请选择配置值类型">
                <el-option
                  v-for="item in options.configTypeOptions"
                  :key="item.dictValue"
                  :label="item.dictLabel"
                  :value="item.dictValue"></el-option>
              </el-select>
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="配置分类" prop="category">
              <el-select v-model="form.category" placeholder="请选择配置分类">
                <el-option
                  v-for="item in options.factory_config_category"
                  :key="item.dictValue"
                  :label="item.dictLabel"
                  :value="item.dictValue"></el-option>
              </el-select>
            </el-form-item>
          </el-col>

          <el-col :lg="24">
            <el-form-item label="配置描述" prop="description">
              <el-input type="textarea" v-model="form.description" placeholder="请输入配置描述" />
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

<script setup name="factoryconfig">
import { listFactoryConfig, addFactoryConfig, delFactoryConfig, updateFactoryConfig, getFactoryConfig } from '@/api/basic/factoryConfig.js'
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
  { visible: true, prop: 'id', label: '主键ID' },
  { visible: true, prop: 'configKey', label: '配置键名' },
  { visible: true, prop: 'configValue', label: '配置键值' },
  { visible: true, prop: 'configType', label: '配置值类型' },
  { visible: true, prop: 'category', label: '配置分类' },
  { visible: true, prop: 'description', label: '配置描述' },
  { visible: true, prop: 'enableFlag', label: '是否启用' }
])
const total = ref(0)
const dataList = ref([])
const queryRef = ref()

var dictParams = [{ dictType: 'factory_config_key' }, { dictType: 'factory_config_category' }]

proxy.getDicts(dictParams).then((response) => {
  response.data.forEach((element) => {
    state.options[element.dictType] = element.list
  })
})

function getList() {
  loading.value = true
  listFactoryConfig(queryParams).then((res) => {
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
    id: [{ required: true, message: '主键ID不能为空', trigger: 'blur', type: 'number' }],
    configKey: [{ required: true, message: '配置键名不能为空', trigger: 'change' }],
    configValue: [{ required: true, message: '配置键值不能为空', trigger: 'blur' }],
    enableFlag: [{ required: true, message: '是否可用不能为空', trigger: 'blur' }]
  },
  options: {
    // 配置键名 选项列表 格式 eg:{ dictLabel: '标签', dictValue: '0'}
    factory_config_key: [],
    // 配置分类
    factory_config_category: [],
    // 配置值类型 选项列表 格式 eg:{ dictLabel: '标签', dictValue: '0'}
    configTypeOptions: [
      { dictLabel: '字符串', dictValue: 'String' },
      { dictLabel: '数组', dictValue: 'Array' },
      { dictLabel: 'JSON', dictValue: 'Json' }
    ]
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
    configKey: null,
    configValue: null,
    configType: null,
    category: null,
    description: null,
    updateBy: null,
    updateTime: null,
    enableFlag: null
  }
  proxy.resetForm('formRef')
}

// 添加按钮操作
function handleAdd() {
  reset()
  open.value = true
  title.value = '添加厂区配置'
  opertype.value = 1
}
// 修改按钮操作
function handleUpdate(row) {
  reset()
  const id = row.id || ids.value
  getFactoryConfig(id).then((res) => {
    const { code, data } = res
    if (code == 200) {
      open.value = true
      title.value = '修改厂区配置'
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
      if (form.value.id != undefined && opertype.value === 2) {
        updateFactoryConfig(form.value).then((res) => {
          proxy.$modal.msgSuccess('修改成功')
          open.value = false
          getList()
        })
      } else {
        addFactoryConfig(form.value).then((res) => {
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
  const Ids = row.id || ids.value

  proxy
    .$confirm('是否确认删除参数编号为"' + Ids + '"的数据项？', '警告', {
      confirmButtonText: proxy.$t('common.ok'),
      cancelButtonText: proxy.$t('common.cancel'),
      type: 'warning'
    })
    .then(function () {
      return delFactoryConfig(Ids)
    })
    .then(() => {
      getList()
      proxy.$modal.msgSuccess('删除成功')
    })
}

handleQuery()
</script>
