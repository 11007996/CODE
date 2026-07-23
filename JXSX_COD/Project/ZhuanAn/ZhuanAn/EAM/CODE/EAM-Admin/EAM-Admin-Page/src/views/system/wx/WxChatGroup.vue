<!--
 * @Descripttion: (微信聊天群/wx_chat_group)
 * @Author: (admin)
 * @Date: (2026-01-20)
-->
<template>
  <div>
    <el-form :model="queryParams" label-position="right" inline ref="queryRef" v-show="showSearch" @submit.prevent>
      <el-form-item label="厂区" prop="factoryId">
        <el-select v-model="queryParams.factoryId" placeholder="厂区代码" clearable filterable remote :remote-method="handleQueryFactory">
          <el-option v-for="item in options.factory_options" :key="item.dictValue" :label="item.dictLabel" :value="item.dictValue"></el-option>
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
        <el-button type="primary" v-hasPermi="['wx:chat:group:add']" plain icon="plus" @click="handleAdd">
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
      <el-table-column prop="chatId" label="群ID" align="center" :show-overflow-tooltip="true" v-if="columns.showColumn('chatId')" />
      <el-table-column prop="chatName" label="群名称" align="center" :show-overflow-tooltip="true" v-if="columns.showColumn('chatName')" />
      <el-table-column prop="factoryId" label="关联厂区" align="center" v-if="columns.showColumn('factoryId')">
        <template #default="scope">
          <dict-tag :options="options.factory_options" :value="scope.row.factoryId" />
        </template>
      </el-table-column>
      <el-table-column label="操作" width="160">
        <template #default="scope">
          <el-button
            type="success"
            size="small"
            icon="edit"
            title="编辑"
            v-hasPermi="['wx:chat:group:edit']"
            @click="handleUpdate(scope.row)"></el-button>
          <el-button
            type="danger"
            size="small"
            icon="delete"
            title="删除"
            v-hasPermi="['wx:chat:group:delete']"
            @click="handleDelete(scope.row)"></el-button>
        </template>
      </el-table-column>
    </el-table>
    <pagination :total="total" v-model:page="queryParams.pageNum" v-model:limit="queryParams.pageSize" @pagination="getList" />

    <!-- 添加或修改微信聊天群对话框 -->
    <el-dialog :title="title" :lock-scroll="false" v-model="open">
      <el-form ref="formRef" :model="form" :rules="rules" label-width="100px">
        <el-row :gutter="20">
          <el-col :lg="12">
            <el-form-item label="群ID" prop="chatId">
              <el-input v-model="form.chatId" placeholder="请输入群ID" :disabled="opertype != 1" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="群名称" prop="chatName">
              <el-input v-model="form.chatName" placeholder="请输入群名称" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="关联厂区" prop="factoryId">
              <el-select v-model="form.factoryId" placeholder="请选择关联厂区">
                <el-option
                  v-for="item in options.factory_options"
                  :key="item.dictValue"
                  :label="item.dictLabel"
                  :value="item.dictValue"></el-option>
              </el-select>
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

<script setup name="wxchatgroup">
import { listWxChatGroup, addWxChatGroup, delWxChatGroup, updateWxChatGroup, getWxChatGroup } from '@/api/system/wxChatGroup.js'
import { dictFactory } from '@/api/system/factory.js'
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
  { visible: true, prop: 'chatId', label: '群ID' },
  { visible: true, prop: 'chatName', label: '群名称' },
  { visible: true, prop: 'factoryId', label: '关联厂区' }
])
const total = ref(0)
const dataList = ref([])
const queryRef = ref()
const defaultTime = ref([new Date(2000, 1, 1, 0, 0, 0), new Date(2000, 2, 1, 23, 59, 59)])

var dictParams = []

function getList() {
  loading.value = true
  listWxChatGroup(queryParams).then((res) => {
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
    chatId: [{ required: true, message: '群ID不能为空', trigger: 'blur' }],
    chatName: [{ required: true, message: '群名称不能为空', trigger: 'blur' }]
  },
  options: {
    // 关联厂区 选项列表 格式 eg:{ dictLabel: '标签', dictValue: '0'}
    factory_options: []
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
    chatId: null,
    chatName: null,
    factoryId: null
  }
  proxy.resetForm('formRef')
}

// 添加按钮操作
function handleAdd() {
  reset()
  open.value = true
  title.value = '添加微信聊天群'
  opertype.value = 1
}
// 修改按钮操作
function handleUpdate(row) {
  reset()
  const id = row.chatId || ids.value
  getWxChatGroup(id).then((res) => {
    const { code, data } = res
    if (code == 200) {
      open.value = true
      title.value = '修改微信聊天群'
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
      if (form.value.chatId != undefined && opertype.value === 2) {
        updateWxChatGroup(form.value).then((res) => {
          proxy.$modal.msgSuccess('修改成功')
          open.value = false
          getList()
        })
      } else {
        addWxChatGroup(form.value).then((res) => {
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
  const Ids = row.chatId || ids.value

  proxy
    .$confirm('是否确认删除参数编号为"' + Ids + '"的数据项？', '警告', {
      confirmButtonText: proxy.$t('common.ok'),
      cancelButtonText: proxy.$t('common.cancel'),
      type: 'warning'
    })
    .then(function () {
      return delWxChatGroup(Ids)
    })
    .then(() => {
      getList()
      proxy.$modal.msgSuccess('删除成功')
    })
}

// 查询厂区
function handleQueryFactory() {
  const params = {
    pageNum: 1,
    pageSize: 100,
    sort: '',
    sortType: 'asc'
  }
  setTimeout(() => {
    dictFactory(params).then((res) => {
      state.options.factory_options = res.data.result
    })
  }, 200)
}

handleQueryFactory()
handleQuery()
</script>
