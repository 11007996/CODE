<!--
 * @Descripttion: (消息接收配置表/CALL_Config_Notice)
 * @Author: (admin)
 * @Date: (2025-07-30)
-->
<template>
  <div>
    <el-form :model="queryParams" label-position="right" inline ref="queryRef" v-show="showSearch" @submit.prevent>
      <el-form-item label="呼叫阶段" prop="callStageType">
        <el-select v-model="queryParams.callStageType" placeholder="请选择呼叫阶段" clearable filterable>
          <el-option v-for="item in options.call_stage_type" :key="item.dictValue" :label="item.dictLabel" :value="item.dictValue"></el-option>
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
        <el-button type="primary" v-hasPermi="['call:config:notice:add']" plain icon="plus" @click="handleAdd">
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
      <el-table-column prop="noticeConfigId" label="配置ID" width="90" align="center" v-if="columns.showColumn('noticeConfigId')" />
      <el-table-column prop="callStageType" label="呼叫阶段" width="150" align="center" v-if="columns.showColumn('callStageType')">
        <template #default="scope">
          <dict-tag :options="options.call_stage_type" :value="scope.row.callStageType" />
        </template>
      </el-table-column>
      <el-table-column prop="areaId" label="区域ID" width="90" align="center" v-if="columns.showColumn('areaId')" />
      <el-table-column prop="areaName" label="区域名称" align="center" v-if="columns.showColumn('areaName')" />
      <el-table-column prop="callTargetType" label="呼叫目标" align="center" v-if="columns.showColumn('callTargetType')">
        <template #default="scope">
          <dict-tag :options="options.call_target_type" :value="scope.row.callTargetType" />
        </template>
      </el-table-column>
      <el-table-column label="操作" width="160">
        <template #default="scope">
          <el-button
            type="success"
            size="small"
            icon="edit"
            title="编辑"
            v-hasPermi="['call:config:notice:edit']"
            @click="handleUpdate(scope.row)"></el-button>
          <el-button
            type="danger"
            size="small"
            icon="delete"
            title="删除"
            v-hasPermi="['call:config:notice:delete']"
            @click="handleDelete(scope.row)"></el-button>
        </template>
      </el-table-column>
    </el-table>
    <pagination :total="total" v-model:page="queryParams.pageNum" v-model:limit="queryParams.pageSize" @pagination="getList" />

    <!-- 添加或修改消息接收配置表对话框 -->
    <el-dialog :title="title" :lock-scroll="false" v-model="open">
      <!-- 提示 -->
      <div class="custom-block warning">
        <li>作用：用于在呼叫的不同状态阶段发送消息通知对应人员。比如:产线发起呼叫时，通知到指定微信群。</li>
        <li>
          范围限制：非必填，以下是通知时优先级的判断（从高到底）： <br />1：同时填写【区域】、【呼叫目标】的配置 <br />2：只填写【呼叫目标】的配置
          <br />3：只填写【区域】的配置 <br />4：没有限制范围
        </li>
        <li>通知对象：微信群与指定员工只能写入其中一个，请勿同时填写。</li>
      </div>
      <el-form ref="formRef" :model="form" :rules="rules" label-width="100px">
        <el-row :gutter="20">
          <!-- <el-col :lg="12" v-if="opertype != 1">
            <el-form-item label="ID" prop="noticeConfigId">
              <el-input-number v-model.number="form.noticeConfigId" controls-position="right" placeholder="请输入ID" :disabled="true" />
            </el-form-item>
          </el-col> -->

          <el-col :lg="12">
            <el-form-item label="呼叫阶段" prop="callStageType">
              <el-select v-model="form.callStageType" placeholder="请选择呼叫阶段">
                <el-option
                  v-for="item in options.call_stage_type"
                  :key="item.dictValue"
                  :label="item.dictLabel"
                  :value="item.dictValue"></el-option>
              </el-select>
            </el-form-item>
          </el-col>

          <el-divider content-position="left">范围限制（非必填）</el-divider>
          <el-col :lg="12">
            <el-form-item label="区域" prop="areaId">
              <el-select v-model="form.areaId" clearable placeholder="请选择区域">
                <el-option
                  v-for="item in options.call_area_options"
                  :key="item.dictValue"
                  :label="item.dictLabel"
                  :value="parseInt(item.dictValue)"></el-option>
              </el-select>
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="呼叫目标" prop="callTargetType">
              <el-select v-model="form.callTargetType" clearable placeholder="请选择呼叫目标">
                <el-option
                  v-for="item in options.call_target_type"
                  :key="item.dictValue"
                  :label="item.dictLabel"
                  :value="item.dictValue"></el-option>
              </el-select>
            </el-form-item>
          </el-col>

          <el-divider content-position="left">通知对象（有且只能填写一个）</el-divider>
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

<script setup name="callconfignotice">
import {
  listCallConfigNotice,
  addCallConfigNotice,
  delCallConfigNotice,
  updateCallConfigNotice,
  getCallConfigNotice
} from '@/api/call/callConfigNotice.js'
import { dictCallArea } from '@/api/call/callArea.js'
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
  { visible: true, prop: 'noticeConfigId', label: '配置ID' },
  { visible: true, prop: 'callStageType', label: '呼叫阶段' },
  { visible: false, prop: 'areaId', label: '区域Id' },
  { visible: true, prop: 'areaName', label: '区域' },
  { visible: true, prop: 'callTargetType', label: '呼叫目标' }
])
const total = ref(0)
const dataList = ref([])
const queryRef = ref()

var dictParams = [{ dictType: 'call_stage_type' }, { dictType: 'call_target_type' }]

proxy.getDicts(dictParams).then((response) => {
  response.data.forEach((element) => {
    state.options[element.dictType] = element.list
  })
})

function getList() {
  loading.value = true
  listCallConfigNotice(queryParams).then((res) => {
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
  form: {},
  rules: {
    callStageType: [{ required: true, message: '呼叫阶段不能为空', trigger: 'change' }]
  },
  options: {
    // 呼叫阶段 选项列表 格式 eg:{ dictLabel: '标签', dictValue: '0'}
    call_stage_type: [],
    // 区域
    call_area_options: [],
    //呼叫目标
    call_target_type: [],
    //人员
    emp_options: [],
    //微信群组：
    wxChatGroup_options: []
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
    noticeConfigId: null,
    callStageType: null,
    areaId: null,
    callTargetType: null,
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
  title.value = '添加消息接收配置表'
  opertype.value = 1
}
// 修改按钮操作
function handleUpdate(row) {
  reset()
  const id = row.noticeConfigId || ids.value
  getCallConfigNotice(id).then((res) => {
    const { code, data } = res
    if (code == 200) {
      open.value = true
      title.value = '修改消息接收配置表'
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
        updateCallConfigNotice(form.value).then((res) => {
          proxy.$modal.msgSuccess('修改成功')
          open.value = false
          getList()
        })
      } else {
        addCallConfigNotice(form.value).then((res) => {
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
      return delCallConfigNotice(Ids)
    })
    .then(() => {
      getList()
      proxy.$modal.msgSuccess('删除成功')
    })
}

function initCallAreaOptions() {
  const query = {
    pageNum: 1,
    pageSize: 100,
    sort: '',
    sortType: 'asc'
  }
  dictCallArea(query).then((res) => {
    options.value.call_area_options = res.data.result
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
initCallAreaOptions()
handleQuery()
</script>
<style lang="scss" scoped>
.custom-block.warning {
  padding: 8px 16px;
  background-color: rgb(245, 108, 108, 0.1);
  border-radius: 4px;
  border-left: 5px solid var(--el-color-danger);
  margin: 20px 0;
}
</style>
