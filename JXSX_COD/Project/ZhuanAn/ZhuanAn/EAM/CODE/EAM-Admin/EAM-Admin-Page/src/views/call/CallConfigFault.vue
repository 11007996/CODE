<!--
 * @Descripttion: (故障配置/CALL_Config_Fault)
 * @Author: (admin)
 * @Date: (2025-07-30)
-->
<template>
  <div>
    <el-form :model="queryParams" label-position="right" inline ref="queryRef" v-show="showSearch" @submit.prevent>
      <el-form-item label="设备类型" prop="equipmentType">
        <el-select
          v-model="queryParams.equipmentType"
          placeholder="请选择设备类型"
          clearable
          filterable
          remote
          :remote-method="handleQueryEquipmentType">
          <el-option
            v-for="item in options.equipment_type_options"
            :key="item.dictValue"
            :label="item.dictLabel"
            :value="item.dictValue"></el-option>
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
        <el-button type="primary" v-hasPermi="['call:config:fault:add']" plain icon="plus" @click="handleAdd">
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
      <el-table-column prop="faultConfigId" label="配置ID" width="90" align="center" v-if="columns.showColumn('faultConfigId')" />
      <el-table-column prop="equipmentType" label="设备类型" align="center" v-if="columns.showColumn('equipmentType')" />
      <el-table-column prop="maxHandleTimes" label="最大处理时间" width="140" align="center" v-if="columns.showColumn('maxHandleTimes')" />
      <el-table-column prop="maxHelpTimes" label="最大支援时间" width="140" align="center" v-if="columns.showColumn('maxHelpTimes')" />
      <el-table-column prop="autoHelpFlag" label="自动支援" width="100" align="center" v-if="columns.showColumn('autoHelpFlag')">
        <template #default="scope">
          <el-switch
            v-model="scope.row.autoHelpFlag"
            inline-prompt
            active-value="Y"
            inactive-value="N"
            active-text="是"
            inactive-text="否"
            disabled />
        </template>
      </el-table-column>
      <el-table-column label="操作" width="160">
        <template #default="scope">
          <el-button
            type="success"
            size="small"
            icon="edit"
            title="编辑"
            v-hasPermi="['call:config:fault:edit']"
            @click="handleUpdate(scope.row)"></el-button>
          <el-button
            type="danger"
            size="small"
            icon="delete"
            title="删除"
            v-hasPermi="['call:config:fault:delete']"
            @click="handleDelete(scope.row)"></el-button>
        </template>
      </el-table-column>
    </el-table>
    <pagination :total="total" v-model:page="queryParams.pageNum" v-model:limit="queryParams.pageSize" @pagination="getList" />

    <el-drawer v-model="drawer" size="50%" direction="rtl">
      <el-table :data="callConfigFaultSolutionList" header-row-class-name="text-navy">
        <el-table-column prop="faultConfigId" label="故障配置ID" />
        <el-table-column prop="faultContent" label="故障内容" />
        <el-table-column prop="solutionContent" label="解决方案" />
      </el-table>
    </el-drawer>

    <!-- 添加或修改故障配置对话框 -->
    <el-dialog :title="title" :lock-scroll="false" v-model="open" :fullscreen="fullScreen">
      <el-form ref="formRef" :model="form" :rules="rules" label-width="100px">
        <el-row :gutter="20">
          <el-col :lg="12" v-if="opertype != 1">
            <el-form-item label="配置ID" prop="faultConfigId">
              <el-input-number v-model.number="form.faultConfigId" controls-position="right" placeholder="请输入配置ID" :disabled="true" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="设备类型" prop="equipmentType">
              <el-select
                v-model="form.equipmentType"
                placeholder="请选择设备类型"
                clearable
                filterable
                remote
                :remote-method="handleQueryEquipmentType"
                class="fullWidth">
                <el-option
                  v-for="item in options.equipment_type_options"
                  :key="item.dictValue"
                  :label="item.dictLabel"
                  :value="item.dictValue"></el-option>
              </el-select>
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="最大处理时间" prop="maxHandleTimes">
              <el-input-number v-model.number="form.maxHandleTimes" :controls="true" controls-position="right" placeholder="请输入最大处理时间" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="最大支援时间" prop="maxHelpTimes">
              <el-input-number v-model.number="form.maxHelpTimes" :controls="true" controls-position="right" placeholder="请输入最大支援时间" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="自动支援" prop="autoHelpFlag">
              <el-switch v-model="form.autoHelpFlag" inline-prompt active-value="Y" inactive-value="N" active-text="是" inactive-text="否" />
            </el-form-item>
          </el-col>
        </el-row>
        <el-divider content-position="center">解决方案配置信息</el-divider>
        <el-row :gutter="10" class="mb8">
          <el-col :span="1.5">
            <el-button type="primary" icon="Plus" @click="handleAddCallConfigFaultSolution">添加</el-button>
          </el-col>
          <el-col :span="1.5">
            <el-button type="danger" icon="Delete" @click="handleDeleteCallConfigFaultSolution">删除</el-button>
          </el-col>
          <el-col :span="1.5">
            <el-button type="info" icon="FullScreen" @click="fullScreen = !fullScreen">{{ fullScreen ? '退出全屏' : '全屏' }}</el-button>
          </el-col>
        </el-row>
        <el-table
          :data="callConfigFaultSolutionList"
          :row-class-name="rowCallConfigFaultSolutionIndex"
          @selection-change="handleCallConfigFaultSolutionSelectionChange"
          ref="CallConfigFaultSolutionRef">
          <el-table-column type="selection" width="40" align="center" />
          <el-table-column label="序号" align="center" prop="index" width="50" />
          <el-table-column label="故障内容" align="center" prop="faultContent">
            <template #default="scope">
              <el-input v-model="scope.row.faultContent" placeholder="请输入故障内容" />
            </template>
          </el-table-column>
          <el-table-column label="解决方案" align="center" prop="solutionContent">
            <template #default="scope">
              <el-input v-model="scope.row.solutionContent" placeholder="请输入解决方案" />
            </template>
          </el-table-column>
        </el-table>
      </el-form>
      <template #footer v-if="opertype != 3">
        <el-button text @click="cancel">{{ $t('btn.cancel') }}</el-button>
        <el-button type="primary" @click="submitForm">{{ $t('btn.submit') }}</el-button>
      </template>
    </el-dialog>
  </div>
</template>

<script setup name="callconfigfault">
import {
  listCallConfigFault,
  addCallConfigFault,
  delCallConfigFault,
  updateCallConfigFault,
  getCallConfigFault
} from '@/api/call/callConfigFault.js'
import { dictEquipmentType } from '@/api/equipment/equipmentType.js'
const { proxy } = getCurrentInstance()
const ids = ref([])
const loading = ref(false)
const showSearch = ref(true)
const queryParams = reactive({
  pageNum: 1,
  pageSize: 10,
  sort: '',
  sortType: 'asc',
  equipmentType: null
})
const columns = ref([
  { visible: true, prop: 'faultConfigId', label: '配置ID' },
  { visible: true, prop: 'equipmentType', label: '设备类型' },
  { visible: true, prop: 'maxHandleTimes', label: '最大处理时间' },
  { visible: true, prop: 'maxHelpTimes', label: '最大支援时间' },
  { visible: true, prop: 'autoHelpFlag', label: '自动支援' }
])
const total = ref(0)
const dataList = ref([])
const queryRef = ref()

function getList() {
  loading.value = true
  listCallConfigFault(queryParams).then((res) => {
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
    equipmentType: [{ required: true, message: '机台类型不能为空', trigger: 'change' }]
  },
  options: {
    // 机台类型 选项列表 格式 eg:{ dictLabel: '标签', dictValue: '0'}
    equipment_type_options: []
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
    faultConfigId: null,
    equipmentType: null,
    maxHandleTimes: null,
    maxHelpTimes: null,
    autoHelpFlag: null
  }
  callConfigFaultSolutionList.value = []
  proxy.resetForm('formRef')
}

// 添加按钮操作
function handleAdd() {
  reset()
  open.value = true
  title.value = '添加故障配置'
  opertype.value = 1
}
// 修改按钮操作
function handleUpdate(row) {
  reset()
  const id = row.faultConfigId || ids.value
  getCallConfigFault(id).then((res) => {
    const { code, data } = res
    if (code == 200) {
      open.value = true
      title.value = '修改故障配置'
      opertype.value = 2

      form.value = {
        ...data
      }
      callConfigFaultSolutionList.value = res.data.callConfigFaultSolutionNav
    }
  })
}

// 添加&修改 表单提交
function submitForm() {
  proxy.$refs['formRef'].validate((valid) => {
    if (valid) {
      form.value.callConfigFaultSolutionNav = callConfigFaultSolutionList.value
      if (form.value.faultConfigId != undefined && opertype.value === 2) {
        updateCallConfigFault(form.value).then((res) => {
          proxy.$modal.msgSuccess('修改成功')
          open.value = false
          getList()
        })
      } else {
        addCallConfigFault(form.value).then((res) => {
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
  const Ids = row.faultConfigId || ids.value

  proxy
    .$confirm('是否确认删除参数编号为"' + Ids + '"的数据项？', '警告', {
      confirmButtonText: proxy.$t('common.ok'),
      cancelButtonText: proxy.$t('common.cancel'),
      type: 'warning'
    })
    .then(function () {
      return delCallConfigFault(Ids)
    })
    .then(() => {
      getList()
      proxy.$modal.msgSuccess('删除成功')
    })
}

/*********************解决方案配置子表信息*************************/
const callConfigFaultSolutionList = ref([])
const checkedCallConfigFaultSolution = ref([])
const fullScreen = ref(false)
const drawer = ref(false)

/** 解决方案配置序号 */
function rowCallConfigFaultSolutionIndex({ row, rowIndex }) {
  row.index = rowIndex + 1
}

/** 解决方案配置添加按钮操作 */
function handleAddCallConfigFaultSolution() {
  let obj = {}
  //下面的代码自己设置默认值
  //obj.faultContent = null;
  //obj.solutionContent = null;
  callConfigFaultSolutionList.value.push(obj)
}

/** 复选框选中数据 */
function handleCallConfigFaultSolutionSelectionChange(selection) {
  checkedCallConfigFaultSolution.value = selection.map((item) => item.index)
}

/** 解决方案配置删除按钮操作 */
function handleDeleteCallConfigFaultSolution() {
  if (checkedCallConfigFaultSolution.value.length == 0) {
    proxy.$modal.msgError('请先选择要删除的解决方案配置数据')
  } else {
    const CallConfigFaultSolutions = callConfigFaultSolutionList.value
    const checkedCallConfigFaultSolutions = checkedCallConfigFaultSolution.value
    callConfigFaultSolutionList.value = CallConfigFaultSolutions.filter(function (item) {
      return checkedCallConfigFaultSolutions.indexOf(item.index) == -1
    })
  }
}

/** 解决方案配置详情 */
function rowClick(row) {
  const id = row.faultConfigId || ids.value
  getCallConfigFault(id).then((res) => {
    const { code, data } = res
    if (code == 200) {
      drawer.value = true
      callConfigFaultSolutionList.value = data.callConfigFaultSolutionNav
    }
  })
}

/**获取设备类型 */
function handleQueryEquipmentType(keyword) {
  if (keyword) {
    const params = {
      pageNum: 1,
      pageSize: 1000,
      sort: '',
      sortType: 'asc',
      equipmentTypeName: keyword
    }
    setTimeout(() => {
      dictEquipmentType(params).then((res) => {
        state.options.equipment_type_options = res.data.result
      })
    }, 200)
  }
}

handleQuery()
</script>
