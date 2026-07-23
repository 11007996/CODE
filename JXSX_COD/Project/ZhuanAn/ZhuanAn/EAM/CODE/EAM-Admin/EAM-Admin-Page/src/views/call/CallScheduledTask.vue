<!--
 * @Descripttion: (广播定时任务/CALL_Scheduled_Task)
 * @Author: (admin)
 * @Date: (2026-05-15)
-->
<template>
  <div>
    <el-form :model="queryParams" label-position="right" inline ref="queryRef" v-show="showSearch" @submit.prevent>
      <el-form-item label="任务名称" prop="taskName">
        <el-input v-model="queryParams.taskName" placeholder="请输入任务名称" />
      </el-form-item>
      <el-form-item>
        <el-button icon="search" type="primary" @click="handleQuery">{{ $t('btn.search') }}</el-button>
        <el-button icon="refresh" @click="resetQuery">{{ $t('btn.reset') }}</el-button>
      </el-form-item>
    </el-form>
    <!-- 工具区域 -->
    <el-row :gutter="15" class="mb10">
      <el-col :span="1.5">
        <el-button type="primary" v-hasPermi="['call:scheduled:task:add']" plain icon="plus" @click="handleAdd">
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
      <el-table-column prop="callTaskId" label="任务ID" align="center" width="90" v-if="columns.showColumn('callTaskId')" />
      <el-table-column prop="taskName" label="任务名称" align="center" min-width="200" v-if="columns.showColumn('taskName')" />
      <el-table-column prop="taskTime" label="任务时间" align="center" width="90" v-if="columns.showColumn('taskTime')" />
      <el-table-column prop="playCount" label="播放次数" align="center" width="90" v-if="columns.showColumn('playCount')" />
      <el-table-column prop="playMedium" label="播放介质" align="center" width="90" v-if="columns.showColumn('playMedium')">
        <template #default="scope">
          <dict-tag :options="options.call_play_medium" :value="scope.row.playMedium" />
        </template>
      </el-table-column>
      <el-table-column prop="fileId" label="文件Id" align="center" width="200" v-if="columns.showColumn('fileId')" />
      <el-table-column prop="fileOriginalName" label="文件" align="center" min-width="200" v-if="columns.showColumn('fileOriginalName')">
        <template #default="scope">
          <el-link type="primary" v-if="scope.row.fileAccessUrl" :href="`${scope.row.fileAccessUrl}`" underline="never" target="_blank">
            <svg-icon class-name="doc-icon" name="documentation" />
            {{ scope.row.fileOriginalName }}
          </el-link>
        </template>
      </el-table-column>
      <el-table-column
        prop="textContent"
        label="文本内容"
        align="left"
        min-width="200"
        :show-overflow-tooltip="true"
        v-if="columns.showColumn('textContent')" />
      <el-table-column prop="areaName" label="限定区域" align="center" width="150" v-if="columns.showColumn('areaName')" />
      <el-table-column prop="enable" label="是否可用" align="center" width="90" v-if="columns.showColumn('enable')">
        <template #default="scope">
          <el-switch v-model="scope.row.enable" active-text="是" inactive-text="否" inline-prompt disabled />
        </template>
      </el-table-column>
      <el-table-column prop="createBy" label="创建人" align="center" width="90" v-if="columns.showColumn('createBy')" />
      <el-table-column prop="createTime" label="创建时间" width="160" v-if="columns.showColumn('createTime')" />
      <el-table-column prop="updateBy" label="更新人" align="center" width="90" v-if="columns.showColumn('updateBy')" />
      <el-table-column prop="updateTime" label="更新时间" width="160" v-if="columns.showColumn('updateTime')" />
      <el-table-column label="操作" width="160" fixed="right">
        <template #default="scope">
          <el-button
            type="success"
            size="small"
            icon="edit"
            title="编辑"
            v-hasPermi="['call:scheduled:task:edit']"
            @click="handleUpdate(scope.row)"></el-button>
          <el-button
            type="danger"
            size="small"
            icon="delete"
            title="删除"
            v-hasPermi="['call:scheduled:task:delete']"
            @click="handleDelete(scope.row)"></el-button>
          <el-button
            v-if="scope.row.textContent"
            type="primary"
            size="small"
            icon="VideoPlay"
            title="播放"
            @click="handlePlay(scope.row)"></el-button>
        </template>
      </el-table-column>
    </el-table>
    <pagination :total="total" v-model:page="queryParams.pageNum" v-model:limit="queryParams.pageSize" @pagination="getList" />

    <!-- 添加或修改广播定时任务对话框 -->
    <el-dialog :title="title" :lock-scroll="false" v-model="open">
      <el-form ref="formRef" :model="form" :rules="rules" label-width="100px">
        <el-row :gutter="20">
          <el-col :lg="12">
            <el-form-item label="任务名称" prop="taskName">
              <el-input v-model="form.taskName" placeholder="请输入任务名称" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="任务时间" prop="taskTime">
              <el-time-picker v-model="form.taskTime" placeholder="请输入任务时间" value-format="HH:mm:ss" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="播放次数" prop="playCount">
              <el-input-number
                v-model.number="form.playCount"
                :controls="true"
                controls-position="right"
                placeholder="请输入播放次数"
                :precision="0"
                :max="10" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="播放介质" prop="playMedium">
              <el-select v-model="form.playMedium" placeholder="请选择播放介质">
                <el-option
                  v-for="item in options.call_play_medium"
                  :key="item.dictValue"
                  :label="item.dictLabel"
                  :value="item.dictValue"></el-option>
              </el-select>
            </el-form-item>
          </el-col>

          <el-col :lg="24" v-if="form.playMedium == 'file'">
            <el-form-item label="文件" prop="fileId">
              <el-input v-model.number="form.fileId" placeholder="请输入文件Id" style="display: none" />
              <FileUpload ref="uploadRef" @success="handleUploadSuccess" :fileType="fileType" :limit="1" :autoUpload="true" :data="uploadData" />
            </el-form-item>
          </el-col>

          <el-col :lg="24" v-if="form.playMedium == 'text'">
            <el-form-item label="文本内容" prop="textContent">
              <el-input type="textarea" v-model="form.textContent" placeholder="请输入文本内容" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="限定区域" prop="areaId">
              <template #label>
                <span>
                  <el-tooltip placement="top" content="用于限制广播的区域范围，非必填，如果不填则所有区域都会播放">
                    <el-icon> <questionFilled /> </el-icon>
                  </el-tooltip>
                  限定区域
                </span>
              </template>
              <el-select v-model="form.areaId" placeholder="请选择限定区域" clearable>
                <el-option
                  v-for="item in options.call_area_options"
                  :key="item.dictValue"
                  :label="item.dictLabel"
                  :value="Number(item.dictValue)"></el-option>
              </el-select>
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="是否可用" prop="enable">
              <el-switch v-model="form.enable" active-text="是" inactive-text="否" inline-prompt />
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

<script setup name="callscheduledtask">
import FileUpload from '@/components/FileUpload/index'
import useUserStore from '@/store/modules/user'
import {
  listCallScheduledTask,
  addCallScheduledTask,
  delCallScheduledTask,
  updateCallScheduledTask,
  getCallScheduledTask
} from '@/api/call/callScheduledTask.js'
import { dictCallArea } from '@/api/call/callArea'
const { proxy } = getCurrentInstance()
const ids = ref([])
const loading = ref(false)
const showSearch = ref(true)
const queryParams = reactive({
  pageNum: 1,
  pageSize: 10,
  sort: '',
  sortType: 'asc',
  taskName: undefined
})
const columns = ref([
  { visible: false, prop: 'callTaskId', label: '任务ID' },
  { visible: true, prop: 'taskName', label: '任务名称' },
  { visible: true, prop: 'taskTime', label: '任务时间' },
  { visible: true, prop: 'playCount', label: '播放次数' },
  { visible: true, prop: 'playMedium', label: '播放介质' },
  { visible: false, prop: 'fileId', label: '文件Id' },
  { visible: true, prop: 'fileOriginalName', label: '文件' },
  { visible: true, prop: 'textContent', label: '文本内容' },
  { visible: true, prop: 'areaName', label: '限定区域' },
  { visible: true, prop: 'enable', label: '是否可用' },
  { visible: false, prop: 'createBy', label: '创建人' },
  { visible: false, prop: 'createTime', label: '创建时间' },
  { visible: false, prop: 'updateBy', label: '更新人' },
  { visible: false, prop: 'updateTime', label: '更新时间' }
])
const total = ref(0)
const dataList = ref([])
const queryRef = ref()
const defaultTime = ref([new Date(2000, 1, 1, 0, 0, 0), new Date(2000, 2, 1, 23, 59, 59)])
const factoryId = useUserStore().factoryId
const uploadData = ref({ fileDir: 'Upload/call/' + factoryId })

var dictParams = [{ dictType: 'call_play_medium' }]

proxy.getDicts(dictParams).then((response) => {
  response.data.forEach((element) => {
    state.options[element.dictType] = element.list
  })
})

function getList() {
  loading.value = true
  listCallScheduledTask(queryParams).then((res) => {
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

    if (column.prop == 'taskTime') {
      sort = 'task_Time'
    }
  }
  queryParams.sort = sort
  queryParams.sortType = sortType
  handleQuery()
}

/*************** form操作 ***************/
const formRef = ref()
const title = ref('')
const fileType = ref(['mp3'])
// 操作类型 1、add 2、edit 3、view
const opertype = ref(0)
const open = ref(false)
const state = reactive({
  single: true,
  multiple: true,
  form: {},
  rules: {
    callTaskId: [{ required: true, message: '呼叫任务ID不能为空', trigger: 'blur', type: 'number' }],
    taskName: [{ required: true, message: '任务名称不能为空', trigger: 'blur' }],
    taskTime: [{ required: true, message: '任务时间不能为空', trigger: 'blur' }],
    playCount: [{ required: true, message: '播放次数不能为空', trigger: 'blur', type: 'number' }],
    playMedium: [{ required: true, message: '播放介质不能为空', trigger: 'change' }]
  },
  options: {
    // 播放介质 选项列表 格式 eg:{ dictLabel: '标签', dictValue: '0'}
    call_play_medium: [],
    // 区域选项
    call_area_options: []
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
    callTaskId: null,
    taskName: null,
    taskTime: null,
    playCount: null,
    playMedium: null,
    fileId: null,
    textContent: null,
    areaId: null,
    enable: null,
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
  title.value = '添加广播定时任务'
  opertype.value = 1
}
// 修改按钮操作
function handleUpdate(row) {
  reset()
  const id = row.callTaskId || ids.value
  getCallScheduledTask(id).then((res) => {
    const { code, data } = res
    if (code == 200) {
      open.value = true
      title.value = '修改广播定时任务'
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
      if (form.value.callTaskId != undefined && opertype.value === 2) {
        updateCallScheduledTask(form.value).then((res) => {
          proxy.$modal.msgSuccess('修改成功')
          open.value = false
          getList()
        })
      } else {
        addCallScheduledTask(form.value).then((res) => {
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
  const Ids = row.callTaskId || ids.value

  proxy
    .$confirm('是否确认删除参数编号为"' + Ids + '"的数据项？', '警告', {
      confirmButtonText: proxy.$t('common.ok'),
      cancelButtonText: proxy.$t('common.cancel'),
      type: 'warning'
    })
    .then(function () {
      return delCallScheduledTask(Ids)
    })
    .then(() => {
      getList()
      proxy.$modal.msgSuccess('删除成功')
    })
}

// 上传成功方法
function handleUploadSuccess(fileUrlStr, filelist) {
  let files = filelist.value
  if (files && files.length > 0) {
    form.value.fileId = files[0].fileId
  }
}

//播放
function handlePlay(row) {
  if (!window.speechSynthesis) {
    proxy.$modal.msgError('当前浏览器不支持 Web Speech API')
    return
  }

  if (row.playMedium == 'text') {
    let utterance = new SpeechSynthesisUtterance(row.textContent)
    // 开始朗读
    window.speechSynthesis.speak(utterance)
  } else if (row.playMedium == 'file') {
  }
}

//查询区域
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

initCallAreaOptions()
handleQuery()
</script>
