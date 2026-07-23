<!--
 * @Descripttion: (产线信息/BASE_Line)
 * @Author: (admin)
 * @Date: (2024-06-27)
-->
<template>
  <div>
    <el-form :model="queryParams" label-position="right" inline ref="queryRef" v-show="showSearch" @submit.prevent>
      <el-form-item label="产线" prop="lineName">
        <el-input v-model="queryParams.lineName" placeholder="请输入产线" />
      </el-form-item>
      <el-form-item label="产线编码" prop="lineCode">
        <el-input v-model="queryParams.lineCode" placeholder="请输入产线编码" />
      </el-form-item>
      <el-form-item>
        <el-button icon="search" type="primary" @click="handleQuery">{{ $t('btn.search') }}</el-button>
        <el-button icon="refresh" @click="resetQuery">{{ $t('btn.reset') }}</el-button>
      </el-form-item>
    </el-form>
    <!-- 工具区域 -->
    <el-row :gutter="15" class="mb10">
      <el-col :span="1.5">
        <el-button type="primary" v-hasPermi="['line:add']" plain icon="plus" @click="handleAdd">
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
      <el-table-column prop="lineId" label="产线ID" width="90" align="center" v-if="columns.showColumn('lineId')" />
      <el-table-column prop="lineName" label="产线" min-width="90" align="center" v-if="columns.showColumn('lineName')" />
      <el-table-column prop="lineCode" label="产线编码" min-width="90" align="center" v-if="columns.showColumn('lineCode')" />
      <el-table-column prop="remark" label="备注" align="center" :show-overflow-tooltip="true" v-if="columns.showColumn('remark')" />
      <el-table-column label="操作" width="110">
        <template #default="scope">
          <el-button type="success" size="small" icon="edit" title="编辑" v-hasPermi="['line:edit']" @click="handleUpdate(scope.row)"></el-button>
          <el-button
            type="danger"
            size="small"
            icon="delete"
            title="删除"
            v-hasPermi="['line:delete']"
            @click="handleDelete(scope.row)"></el-button>
        </template>
      </el-table-column>
    </el-table>
    <pagination :total="total" v-model:page="queryParams.pageNum" v-model:limit="queryParams.pageSize" @pagination="getList" />

    <!-- 添加或修改产线信息对话框 -->
    <el-dialog :title="title" :lock-scroll="false" v-model="open">
      <el-form ref="formRef" :model="form" :rules="rules" label-width="100px">
        <el-row :gutter="20">
          <!-- <el-col :lg="12" v-if="opertype != 1">
            <el-form-item label="产线Id" prop="lineId">
              <el-input v-model="form.lineId" placeholder="请输入产线名称" disabled />
            </el-form-item>
          </el-col> -->
          <el-col :lg="12">
            <el-form-item label="产线名称" prop="lineName">
              <el-input v-model="form.lineName" placeholder="请输入产线名称" />
            </el-form-item>
          </el-col>

          <el-col :lg="12" v-hasPermi="['line:edit:code']">
            <el-form-item label="产线编码" prop="lineCode">
              <template #label>
                <span>
                  <el-tooltip placement="top" content="在设备上报数据时可能会用到，通常由设备对接人员配置">
                    <el-icon> <questionFilled /> </el-icon>
                  </el-tooltip>
                  产线编码
                </span>
              </template>
              <el-input v-model="form.lineCode" placeholder="请输入产线编码" />
            </el-form-item>
          </el-col>
        </el-row>

        <el-col :lg="24">
          <el-form-item label="备注" prop="remark">
            <el-input type="textarea" v-model="form.remark" />
          </el-form-item>
        </el-col>
      </el-form>
      <template #footer v-if="opertype != 3">
        <el-button text @click="cancel">{{ $t('btn.cancel') }}</el-button>
        <el-button type="primary" @click="submitForm">{{ $t('btn.submit') }}</el-button>
      </template>
    </el-dialog>
  </div>
</template>

<script setup name="lineinfo">
import { listLine, addLine, delLine, updateLine, getLine } from '@/api/basic/line.js'
const { proxy } = getCurrentInstance()
const ids = ref([])
const loading = ref(false)
const showSearch = ref(true)
const queryParams = reactive({
  pageNum: 1,
  pageSize: 10,
  sort: 'LineId',
  sortType: 'asc',
  lineName: undefined,
  lineCode: undefined
})
const columns = ref([
  { visible: true, prop: 'lineId', label: '产线Id' },
  { visible: true, prop: 'lineName', label: '产线名称' },
  { visible: true, prop: 'lineCode', label: '产线编码' },
  { visible: true, prop: 'remark', label: '备注' }
])
const total = ref(0)
const dataList = ref([])
const queryRef = ref()

function getList() {
  loading.value = true
  listLine(queryParams).then((res) => {
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
    lineName: [{ required: true, message: '产线名称不能为空', trigger: 'blur' }]
  },
  options: {}
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
    lineId: null,
    lineName: null,
    lineCode: null,
    remark: null
  }
  proxy.resetForm('formRef')
}

// 添加按钮操作
function handleAdd() {
  reset()
  open.value = true
  title.value = '添加产线信息'
  opertype.value = 1
}
// 修改按钮操作
function handleUpdate(row) {
  reset()
  const id = row.lineId
  getLine(id).then((res) => {
    const { code, data } = res
    if (code == 200) {
      open.value = true
      title.value = '修改产线信息'
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
      if (form.value.lineId != undefined && opertype.value === 2) {
        updateLine(form.value).then((res) => {
          proxy.$modal.msgSuccess('修改成功')
          open.value = false
          getList()
        })
      } else {
        addLine(form.value).then((res) => {
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
  const Ids = row.lineId || ids.value

  proxy
    .$confirm('是否确认删除参数编号为"' + Ids + '"的数据项？', '警告', {
      confirmButtonText: proxy.$t('common.ok'),
      cancelButtonText: proxy.$t('common.cancel'),
      type: 'warning'
    })
    .then(function () {
      return delLine(Ids)
    })
    .then(() => {
      getList()
      proxy.$modal.msgSuccess('删除成功')
    })
}

handleQuery()
</script>
