<!--
 * @Descripttion: (耗品通知人员配置/CON_Config_Notice)
 * @Author: (admin)
 * @Date: (2025-10-16)
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
        <el-button type="primary" v-hasPermi="['consumable:config:notice:add']" plain icon="plus" @click="handleAdd">
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
      <el-table-column prop="noticeConfigId" label="通知配置ID" align="center" v-if="columns.showColumn('noticeConfigId')" />
      <el-table-column prop="empCodes" label="人员工号" align="center" v-if="columns.showColumn('empCode')" />
      <el-table-column prop="wxChatId" label="微信群id" align="center" v-if="columns.showColumn('wxChatId')" />
      <el-table-column label="操作" width="160">
        <template #default="scope">
          <el-button
            type="success"
            size="small"
            icon="edit"
            title="编辑"
            v-hasPermi="['consumable:config:notice:edit']"
            @click="handleUpdate(scope.row)"></el-button>
          <el-button
            type="danger"
            size="small"
            icon="delete"
            title="删除"
            v-hasPermi="['consumable:config:notice:delete']"
            @click="handleDelete(scope.row)"></el-button>
        </template>
      </el-table-column>
    </el-table>
    <pagination :total="total" v-model:page="queryParams.pageNum" v-model:limit="queryParams.pageSize" @pagination="getList" />

    <!-- 添加或修改耗品通知人员配置对话框 -->
    <el-dialog :title="title" :lock-scroll="false" v-model="open">
      <el-form ref="formRef" :model="form" :rules="rules" label-width="100px">
        <el-row :gutter="20">
          <el-col :lg="12">
            <el-form-item label="微信群" prop="wxChatId">
              <el-select clearable v-model="form.wxChatId" placeholder="请选择消息通知微信群">
                <el-option
                  v-for="item in options.wxChatGroup_options"
                  :key="item.dictValue"
                  :label="item.dictLabel"
                  :value="item.dictValue"></el-option>
              </el-select>
            </el-form-item>
          </el-col>

          <el-col :lg="24">
            <el-form-item label="员工">
              <el-select
                v-model="empList"
                placeholder="请输入员工姓名"
                clearable
                filterable
                multiple
                remote
                :remote-method="handleQueryEmployee"
                class="fullWidth">
                <el-option v-for="item in options.emp_options" :label="item.dictValue + ' - ' + item.dictLabel" :value="item.dictValue"></el-option>
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

<script setup name="consumableconfignotice">
import {
  listConsumableConfigNotice,
  addConsumableConfigNotice,
  delConsumableConfigNotice,
  updateConsumableConfigNotice,
  getConsumableConfigNotice
} from '@/api/consumable/consumableConfigNotice.js'
import { dictEmployee } from '@/api/basic/employee.js'
import { dictWxChatGroup } from '@/api/system/wxChatGroup.js'
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
  { visible: true, prop: 'noticeConfigId', label: '通知配置ID' },
  { visible: true, prop: 'empCode', label: '人员工号' },
  { visible: true, prop: 'empName', label: '人员姓名' }
])
const total = ref(0)
const dataList = ref([])
const queryRef = ref()
const defaultTime = ref([new Date(2000, 1, 1, 0, 0, 0), new Date(2000, 2, 1, 23, 59, 59)])

var dictParams = []

function getList() {
  loading.value = true
  listConsumableConfigNotice(queryParams).then((res) => {
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
const empList = ref([])
const state = reactive({
  single: true,
  multiple: true,
  form: {},
  rules: {
    noticeConfigId: [{ required: true, message: '通知配置ID不能为空', trigger: 'blur', type: 'number' }],
    empCode: [{ required: true, message: '人员工号不能为空', trigger: 'change' }]
  },
  options: {
    // 人员工号 选项列表 格式 eg:{ dictLabel: '标签', dictValue: '0'}
    emp_options: [],
    //微信群组：
    wxChatGroup_options: []
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
    noticeConfigId: null,
    wxChatId: null,
    empCodes: null
  }
  empList.value = null
  proxy.resetForm('formRef')
}

// 添加按钮操作
function handleAdd() {
  reset()
  open.value = true
  title.value = '添加耗品通知人员配置'
  opertype.value = 1
}
// 修改按钮操作
function handleUpdate(row) {
  reset()
  const id = row.noticeConfigId || ids.value
  getConsumableConfigNotice(id).then((res) => {
    const { code, data } = res
    if (code == 200) {
      open.value = true
      title.value = '修改耗品通知人员配置'
      opertype.value = 2

      form.value = {
        ...data
      }

      if (form.value.empCodes) empList.value = form.value.empCodes.split(',')
      if (res.data.empNav) {
        let noticeEmps = []
        res.data.empNav.forEach((item) => {
          noticeEmps.push({ dictValue: item.empCode, dictLabel: item.empName })
        })
        options.value.emp_options = noticeEmps
      }
    }
  })
}

// 添加&修改 表单提交
function submitForm() {
  proxy.$refs['formRef'].validate((valid) => {
    if (valid) {
      form.value.empCodes = empList && empList.value ? empList.value.join(',') : null
      if (form.value.noticeConfigId != undefined && opertype.value === 2) {
        updateConsumableConfigNotice(form.value).then((res) => {
          proxy.$modal.msgSuccess('修改成功')
          open.value = false
          getList()
        })
      } else {
        addConsumableConfigNotice(form.value).then((res) => {
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
  const Ids = row.noticeConfigId || ids.value

  proxy
    .$confirm('是否确认删除参数编号为"' + Ids + '"的数据项？', '警告', {
      confirmButtonText: proxy.$t('common.ok'),
      cancelButtonText: proxy.$t('common.cancel'),
      type: 'warning'
    })
    .then(function () {
      return delConsumableConfigNotice(Ids)
    })
    .then(() => {
      getList()
      proxy.$modal.msgSuccess('删除成功')
    })
}

//员工查询
function handleQueryEmployee(keyword) {
  if (keyword) {
    const query = {
      pageNum: 1,
      pageSize: 10,
      keyword: keyword
    }
    setTimeout(() => {
      dictEmployee(query).then((res) => {
        options.value.emp_options = res.data.result
      })
    }, 200)
  }
}

// 查询微信群
function handleQueryWxChatGroup() {
  const params = {
    pageNum: 1,
    pageSize: 100,
    sort: '',
    sortType: 'asc'
  }
  setTimeout(() => {
    dictWxChatGroup(params).then((res) => {
      state.options.wxChatGroup_options = res.data.result
    })
  }, 200)
}

handleQueryWxChatGroup()

handleQuery()
</script>
