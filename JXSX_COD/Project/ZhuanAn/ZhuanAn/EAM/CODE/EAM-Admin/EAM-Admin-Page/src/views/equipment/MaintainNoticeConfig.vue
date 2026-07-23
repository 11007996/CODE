<!--
 * @Descripttion: (保养通知配置/EQU_Maintain_Notice_Config)
 * @Author: (admin)
 * @Date: (2025-08-20)
-->
<template>
  <div>
    <el-form :model="queryParams" label-position="right" inline ref="queryRef" v-show="showSearch" @submit.prevent>
      <el-form-item label="日期标记" prop="dateMark">
        <el-select clearable v-model="queryParams.dateMark" placeholder="请选择日期标记">
          <el-option v-for="item in options.date_mark" :key="item.dictValue" :label="item.dictLabel" :value="item.dictValue">
            <span class="fl">{{ item.dictLabel }}</span>
            <span class="fr" style="color: var(--el-text-color-secondary)">{{ item.dictValue }}</span>
          </el-option>
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
        <el-button type="primary" v-hasPermi="['maintain:notice:config:add']" plain icon="plus" @click="handleAdd">
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
      <el-table-column align="center" width="90">
        <template #default="scope">
          <el-button text @click="rowClick(scope.row)">{{ $t('btn.details') }}</el-button>
        </template>
      </el-table-column>
      <el-table-column prop="noticeConfigId" label="通知配置ID" align="center" v-if="columns.showColumn('noticeConfigId')" />
      <el-table-column prop="dateMark" label="日期标记" align="center" v-if="columns.showColumn('dateMark')">
        <template #default="scope">
          <dict-tag :options="options.date_mark" :value="scope.row.dateMark" />
        </template>
      </el-table-column>
      <el-table-column prop="enableFlag" label="是否有效" align="center" v-if="columns.showColumn('enableFlag')">
        <template #default="scope">
          <el-switch
            v-model="scope.row.enableFlag"
            disabled
            inline-prompt
            active-value="Y"
            inactive-value="N"
            active-text="是"
            inactive-text="否" />
        </template>
      </el-table-column>
      <el-table-column label="操作" width="160">
        <template #default="scope">
          <el-button
            type="success"
            size="small"
            icon="edit"
            title="编辑"
            v-hasPermi="['maintain:notice:config:edit']"
            @click="handleUpdate(scope.row)"></el-button>
          <el-button
            type="danger"
            size="small"
            icon="delete"
            title="删除"
            v-hasPermi="['maintain:notice:config:delete']"
            @click="handleDelete(scope.row)"></el-button>
        </template>
      </el-table-column>
    </el-table>
    <pagination :total="total" v-model:page="queryParams.pageNum" v-model:limit="queryParams.pageSize" @pagination="getList" />

    <!-- 添加或修改保养通知配置对话框 -->
    <el-dialog :title="title" :lock-scroll="false" v-model="open">
      <!-- 提示 -->
      <div class="custom-block warning">
        <span style="font-weight: bold">提示</span>
        <li>如果选择了保养日期标记，当未做该保养日期类型时，优先以此配置为准提醒</li>
        <li>日期标记为空值的配置优先级最低</li>
        <li>周提醒触发时机：每周六检查当日是否保养</li>
        <li>月、季、年提醒触发时机：检查设备在保养计划班次设置的保养日期内是否有做保养</li>
      </div>

      <el-form ref="formRef" :model="form" :rules="rules" label-width="100px">
        <el-row :gutter="20">
          <!-- <el-col :lg="12" v-if="opertype != 1">
            <el-form-item label="通知配置ID" prop="noticeConfigId">
              <el-input-number v-model.number="form.noticeConfigId" controls-position="right" placeholder="请输入通知配置ID" :disabled="true" />
            </el-form-item>
          </el-col> -->
          <el-col :lg="12">
            <el-form-item label="日期标记" prop="dateMark">
              <template #label>
                <span>
                  <el-tooltip placement="top" content="非必填，为空表示所有保养日期类型">
                    <el-icon> <questionFilled /> </el-icon>
                  </el-tooltip>
                  日期标记
                </span>
              </template>
              <el-select v-model="form.dateMark" clearable placeholder="请选择日期标记">
                <el-option v-for="item in options.date_mark" :key="item.dictValue" :label="item.dictLabel" :value="item.dictValue"></el-option>
              </el-select>
            </el-form-item>
          </el-col>

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

          <el-col :lg="12">
            <el-form-item label="是否有效" prop="enableFlag">
              <el-switch v-model="form.enableFlag" inline-prompt active-value="Y" inactive-value="N" active-text="是" inactive-text="否" />
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

<script setup name="maintainnoticeconfig">
import {
  listMaintainNoticeConfig,
  addMaintainNoticeConfig,
  delMaintainNoticeConfig,
  updateMaintainNoticeConfig,
  getMaintainNoticeConfig
} from '@/api/equipment/maintainNoticeConfig.js'
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
  sortType: 'asc',
  dateMark: undefined
})
const columns = ref([
  { visible: true, prop: 'noticeConfigId', label: '通知配置ID' },
  { visible: true, prop: 'enableFlag', label: '是否有效' },
  { visible: true, prop: 'dateMark', label: '日期标记' }
])
const total = ref(0)
const dataList = ref([])
const queryRef = ref()

var dictParams = [{ dictType: 'date_mark' }, { dictType: 'sys_yes_no' }]

proxy.getDicts(dictParams).then((response) => {
  response.data.forEach((element) => {
    state.options[element.dictType] = element.list
  })
})

function getList() {
  loading.value = true
  listMaintainNoticeConfig(queryParams).then((res) => {
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
  rules: {},
  options: {
    // 是否有效 选项列表 格式 eg:{ dictLabel: '标签', dictValue: '0'}
    sys_yes_no: [],
    // 日期标记 选项列表 格式 eg:{ dictLabel: '标签', dictValue: '0'}
    date_mark: [],
    //微信群
    wxChatGroup_options: [],
    //员工
    emp_options: []
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
    enableFlag: null,
    dateMark: null
  }
  empList.value = null
  proxy.resetForm('formRef')
}

// 添加按钮操作
function handleAdd() {
  reset()
  open.value = true
  title.value = '添加保养通知配置'
  opertype.value = 1
}
// 修改按钮操作
function handleUpdate(row) {
  reset()
  const id = row.noticeConfigId || ids.value
  getMaintainNoticeConfig(id).then((res) => {
    const { code, data } = res
    if (code == 200) {
      open.value = true
      title.value = '修改保养通知配置'
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
        updateMaintainNoticeConfig(form.value).then((res) => {
          proxy.$modal.msgSuccess('修改成功')
          open.value = false
          getList()
        })
      } else {
        addMaintainNoticeConfig(form.value).then((res) => {
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
      return delMaintainNoticeConfig(Ids)
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

<style lang="scss" scoped>
.custom-block.warning {
  padding: 8px 16px;
  background-color: rgb(245, 108, 108, 0.1);
  border-radius: 4px;
  border-left: 5px solid var(--el-color-danger);
  margin: 20px 0;
}
</style>
