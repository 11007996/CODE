<!--
 * @Descripttion: (企业微信发送记录表/wx_message)
 * @Author: (admin)
 * @Date: (2024-08-05)
-->
<template>
  <div>
    <el-form :model="queryParams" label-position="right" inline ref="queryRef" v-show="showSearch" @submit.prevent>
      <el-form-item label="发送时间">
        <el-date-picker
          v-model="dateRangeSendTime"
          type="datetimerange"
          start-placeholder="开始日期"
          end-placeholder="结束日期"
          value-format="YYYY-MM-DD HH:mm:ss"
          :default-time="defaultTime"
          :shortcuts="dateOptions">
        </el-date-picker>
      </el-form-item>
      <el-form-item>
        <el-button icon="search" type="primary" @click="handleQuery">{{ $t('btn.search') }}</el-button>
        <el-button icon="refresh" @click="resetQuery">{{ $t('btn.reset') }}</el-button>
      </el-form-item>
    </el-form>
    <!-- 工具区域 -->
    <el-row :gutter="15" class="mb10">
      <el-col :span="1.5">
        <el-button type="primary" v-hasPermi="['wxmessage:add']" plain icon="plus" @click="handleAdd">
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
      <el-table-column prop="id" label="主键ID" align="center" v-if="columns.showColumn('id')" />
      <el-table-column prop="title" label="消息标题" align="center" :show-overflow-tooltip="true" v-if="columns.showColumn('title')" />
      <el-table-column prop="content" label="消息内容" align="center" :show-overflow-tooltip="true" v-if="columns.showColumn('content')" />
      <el-table-column prop="chatId" label="微信群id" align="center" v-if="columns.showColumn('chatId')" />
      <el-table-column prop="empCodes" label="工号" align="center" v-if="columns.showColumn('empCodes')" />
      <el-table-column prop="msgType" label="消息类型" align="center" v-if="columns.showColumn('msgType')">
        <template #default="scope">
          <dict-tag :options="options.wx_message_type" :value="scope.row.msgType" />
        </template>
      </el-table-column>
      <el-table-column prop="linkUrl" label="链接" align="center" :show-overflow-tooltip="true" v-if="columns.showColumn('linkUrl')" />
      <el-table-column prop="articles" label="图文集合" align="center" :show-overflow-tooltip="true" v-if="columns.showColumn('articles')" />
      <el-table-column prop="sendTime" label="发送时间" :show-overflow-tooltip="true" v-if="columns.showColumn('sendTime')" />
      <el-table-column prop="resultMsg" label="发送结果" align="center" :show-overflow-tooltip="true" v-if="columns.showColumn('resultMsg')" />
      <el-table-column label="操作" width="160">
        <template #default="scope">
          <el-button type="success" icon="edit" title="编辑" v-hasPermi="['wxmessage:edit']" @click="handleUpdate(scope.row)"></el-button>
          <el-button type="danger" icon="delete" title="删除" v-hasPermi="['wxmessage:delete']" @click="handleDelete(scope.row)"></el-button>
        </template>
      </el-table-column>
    </el-table>
    <pagination :total="total" v-model:page="queryParams.pageNum" v-model:limit="queryParams.pageSize" @pagination="getList" />

    <!-- 添加或修改企业微信发送记录表对话框 -->
    <el-dialog :title="title" :lock-scroll="false" v-model="open">
      <el-form ref="formRef" :model="form" :rules="rules" label-width="100px">
        <el-row :gutter="20">
          <el-col :lg="24">
            <el-form-item label="消息标题" prop="title">
              <el-input v-model="form.title" placeholder="请输入消息标题" />
            </el-form-item>
          </el-col>

          <el-col :lg="24">
            <el-form-item label="消息内容" prop="content">
              <editor v-model="form.content" :min-height="200" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="微信群id" prop="title">
              <el-input v-model="form.chatId" placeholder="请输入微信群id" />
            </el-form-item>
          </el-col>

          <el-col :lg="24">
            <el-form-item label="工号" prop="empCodes">
              <el-select v-model="form.empCodesChecked" multiple placeholder="请选择工号" class="fullWidth">
                <el-option
                  v-for="item in options.empCodes_options"
                  :key="item.dictValue"
                  :label="item.dictLabel"
                  :value="item.dictValue"></el-option>
              </el-select>
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="消息类型" prop="msgType">
              <el-select v-model="form.msgType" placeholder="请选择消息类型">
                <el-option v-for="item in options.msgTypeOptions" :key="item.dictValue" :label="item.dictLabel" :value="item.dictValue"></el-option>
              </el-select>
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="发送时间" prop="sendTime">
              <el-date-picker v-model="form.sendTime" type="datetime" :teleported="false" placeholder="选择日期时间"></el-date-picker>
            </el-form-item>
          </el-col>

          <el-col :lg="24">
            <el-form-item label="链接" prop="linkUrl">
              <el-input type="textarea" v-model="form.linkUrl" placeholder="请输入链接" />
            </el-form-item>
          </el-col>

          <el-col :lg="24">
            <el-form-item label="图文集合" prop="articles">
              <el-input type="textarea" v-model="form.articles" placeholder="请输入图文集合" />
            </el-form-item>
          </el-col>

          <el-col :lg="24">
            <el-form-item label="发送结果" prop="resultMsg">
              <el-input type="textarea" v-model="form.resultMsg" placeholder="请输入发送结果" />
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

<script setup name="wxmessage">
import { listWxMessage, addWxMessage, delWxMessage, updateWxMessage, getWxMessage } from '@/api/system/wxMessage.js'
import Editor from '@/components/Editor'
const { proxy } = getCurrentInstance()
const ids = ref([])
const loading = ref(false)
const showSearch = ref(true)
const queryParams = reactive({
  pageNum: 1,
  pageSize: 10,
  sort: '',
  sortType: 'asc',
  sendTime: undefined
})
const columns = ref([
  { visible: true, prop: 'id', label: '主键ID' },
  { visible: true, prop: 'title', label: '消息标题' },
  { visible: true, prop: 'content', label: '消息内容' },
  { visible: true, prop: 'chatId', label: '微信群id' },
  { visible: true, prop: 'empCodes', label: '工号' },
  { visible: true, prop: 'msgType', label: '消息类型' },
  { visible: true, prop: 'linkUrl', label: '链接' },
  { visible: true, prop: 'articles', label: '图文集合' },
  { visible: true, prop: 'sendTime', label: '发送时间' },
  { visible: false, prop: 'resultMsg', label: '发送结果' }
])
const total = ref(0)
const dataList = ref([])
const queryRef = ref()
const defaultTime = ref([new Date(2000, 1, 1, 0, 0, 0), new Date(2000, 2, 1, 23, 59, 59)])

// 发送时间时间范围
const dateRangeSendTime = ref([])

var dictParams = [{ dictType: 'wx_message_type' }]
//获取字典数据
proxy.getDicts(dictParams).then((response) => {
  response.data.forEach((element) => {
    state.options[element.dictType] = element.list
  })
})

function getList() {
  proxy.addDateRange(queryParams, dateRangeSendTime.value, 'SendTime')
  loading.value = true
  listWxMessage(queryParams).then((res) => {
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
  // 发送时间时间范围
  dateRangeSendTime.value = []
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
    msgType: [{ required: true, message: '消息类型不能为空', trigger: 'change' }],
    sendTime: [{ required: true, message: '发送时间不能为空', trigger: 'blur' }]
  },
  options: {
    // 工号 选项列表 格式 eg:{ dictLabel: '标签', dictValue: '0'}
    empCodes_options: [],
    // 微信群
    wx_chat_group_options: [],
    //消息类型
    wx_message_type: []
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
    id: null,
    title: null,
    content: null,
    empCodesChecked: [],
    msgType: null,
    linkUrl: null,
    articles: null,
    sendTime: null,
    resultMsg: null
  }
  proxy.resetForm('formRef')
}

// 添加按钮操作
function handleAdd() {
  reset()
  open.value = true
  title.value = '添加企业微信发送记录表'
  opertype.value = 1
}
// 修改按钮操作
function handleUpdate(row) {
  reset()
  const id = row.id || ids.value
  getWxMessage(id).then((res) => {
    const { code, data } = res
    if (code == 200) {
      open.value = true
      title.value = '修改企业微信发送记录表'
      opertype.value = 2

      form.value = {
        ...data,
        empCodesChecked: data.empCodes ? data.empCodes.split(',') : []
      }
    }
  })
}

// 添加&修改 表单提交
function submitForm() {
  proxy.$refs['formRef'].validate((valid) => {
    if (valid) {
      form.value.empCodes = form.value.empCodesChecked.toString()

      if (form.value.id != undefined && opertype.value === 2) {
        updateWxMessage(form.value).then((res) => {
          proxy.$modal.msgSuccess('修改成功')
          open.value = false
          getList()
        })
      } else {
        addWxMessage(form.value).then((res) => {
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
      return delWxMessage(Ids)
    })
    .then(() => {
      getList()
      proxy.$modal.msgSuccess('删除成功')
    })
}

handleQuery()
</script>
