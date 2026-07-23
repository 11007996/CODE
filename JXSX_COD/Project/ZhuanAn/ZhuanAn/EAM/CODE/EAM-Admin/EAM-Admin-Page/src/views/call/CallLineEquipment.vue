<!--
 * @Descripttion: (产线设备/CALL_Line_Equipment)
 * @Author: (admin)
 * @Date: (2025-07-30)
-->
<template>
  <div>
    <el-row :gutter="20">
      <!-- 产线数据 -->
      <el-col :span="4" :xs="24">
        <div class="line-container">
          <el-table
            :data="useBasicStore().getLineDict"
            v-loading="loading"
            ref="lineTable"
            height="100%"
            border
            header-cell-class-name="el-table-header-cell"
            highlight-current-row
            @row-click="rowClickLineTable">
            <el-table-column prop="dictLabel" label="产线" align="center" />
          </el-table>
        </div>
      </el-col>

      <!-- 设备数据 -->
      <el-col :span="20" :xs="24">
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
            <el-button type="primary" v-hasPermi="['call:line:equipment:add']" plain icon="plus" @click="handleAdd">
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
          <el-table-column prop="lineEquipmentId" label="关联ID" width="90" align="center" v-if="columns.showColumn('lineEquipmentId')" />
          <el-table-column prop="lineId" label="产线ID" width="90" align="center" v-if="columns.showColumn('lineId')" />
          <el-table-column prop="lineName" label="产线名称" align="center" v-if="columns.showColumn('lineName')" />
          <el-table-column prop="equipmentType" label="设备类型" align="center" v-if="columns.showColumn('equipmentType')" />
          <el-table-column prop="equipmentNo" label="设备编号" align="center" v-if="columns.showColumn('equipmentNo')" />
          <el-table-column label="操作" width="160">
            <template #default="scope">
              <el-button
                type="success"
                size="small"
                icon="edit"
                title="编辑"
                v-hasPermi="['call:line:equipment:edit']"
                @click="handleUpdate(scope.row)"></el-button>
              <el-button
                type="danger"
                size="small"
                icon="delete"
                title="删除"
                v-hasPermi="['call:line:equipment:delete']"
                @click="handleDelete(scope.row)"></el-button>
            </template>
          </el-table-column>
        </el-table>
        <pagination :total="total" v-model:page="queryParams.pageNum" v-model:limit="queryParams.pageSize" @pagination="getList" />
      </el-col>
    </el-row>

    <!-- 添加或修改产线设备对话框 -->
    <el-dialog :title="title" :lock-scroll="false" v-model="open">
      <el-form ref="formRef" :model="form" :rules="rules" label-width="100px">
        <el-row :gutter="20">
          <el-col :lg="12">
            <el-form-item label="产线ID" prop="lineId">
              <el-select v-model="form.lineId" placeholder="请选择产线" filterable class="fullWidth">
                <el-option
                  v-for="item in useBasicStore().getLineDict"
                  :key="item.dictValue"
                  :label="item.dictLabel"
                  :value="Number(item.dictValue)"></el-option>
              </el-select>
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
            <el-form-item label="设备编号" prop="equipmentNo">
              <template #label>
                <span>
                  <el-tooltip placement="top" content="非必填，当同一产线有多个相同类型的设备时，则需要填写，用于区分。如：1号">
                    <el-icon> <questionFilled /> </el-icon>
                  </el-tooltip>
                  设备编号
                </span>
              </template>
              <el-input v-model="form.equipmentNo" placeholder="请输入设备编号" />
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

<script setup name="calllineequipment">
import {
  listCallLineEquipment,
  addCallLineEquipment,
  delCallLineEquipment,
  updateCallLineEquipment,
  getCallLineEquipment
} from '@/api/call/callLineEquipment.js'
import { dictEquipmentType } from '@/api/equipment/equipmentType.js'
import useBasicStore from '@/store/modules/basic.js'

const { proxy } = getCurrentInstance()
const ids = ref([])
const loading = ref(false)
const showSearch = ref(true)
const queryParams = reactive({
  pageNum: 1,
  pageSize: 10,
  sort: '',
  sortType: 'asc',
  lineId: 0,
  equipmentType: null
})
const columns = ref([
  { visible: true, prop: 'lineEquipmentId', label: '关联ID' },
  { visible: false, prop: 'lineId', label: '产线ID' },
  { visible: false, prop: 'lineName', label: '产线' },
  { visible: true, prop: 'equipmentType', label: '设备类型' },
  { visible: true, prop: 'equipmentNo', label: '设备编号' }
])
const total = ref(0)
const dataList = ref([])
const queryRef = ref()

function getList() {
  loading.value = true
  listCallLineEquipment(queryParams).then((res) => {
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

//产线行点击事件
function rowClickLineTable(row) {
  queryParams.lineId = Number(row.dictValue)
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
    lineId: [{ required: true, message: '产线ID不能为空', trigger: 'blur', type: 'number' }],
    equipmentType: [{ required: true, message: '设备类型不能为空', trigger: 'change' }]
  },
  options: {
    // 设备类型 选项列表 格式 eg:{ dictLabel: '标签', dictValue: '0'}
    equipment_type_options: []
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
    lineEquipmentId: null,
    lineId: null,
    equipmentType: null,
    equipmentNo: null
  }
  proxy.resetForm('formRef')
}

// 添加按钮操作
function handleAdd() {
  reset()
  form.value.lineId = queryParams.lineId
  open.value = true
  title.value = '添加产线设备'
  opertype.value = 1
}
// 修改按钮操作
function handleUpdate(row) {
  reset()
  const id = row.lineEquipmentId || ids.value
  getCallLineEquipment(id).then((res) => {
    const { code, data } = res
    if (code == 200) {
      open.value = true
      title.value = '修改产线设备'
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
      if (form.value.lineEquipmentId != undefined && opertype.value === 2) {
        updateCallLineEquipment(form.value).then((res) => {
          proxy.$modal.msgSuccess('修改成功')
          open.value = false
          getList()
        })
      } else {
        addCallLineEquipment(form.value).then((res) => {
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
  const Ids = row.lineEquipmentId || ids.value

  proxy
    .$confirm('是否确认删除参数编号为"' + Ids + '"的数据项？', '警告', {
      confirmButtonText: proxy.$t('common.ok'),
      cancelButtonText: proxy.$t('common.cancel'),
      type: 'warning'
    })
    .then(function () {
      return delCallLineEquipment(Ids)
    })
    .then(() => {
      getList()
      proxy.$modal.msgSuccess('删除成功')
    })
}

/**获取设备类型 */
function handleQueryEquipmentType(keyword) {
  const params = {
    pageNum: 1,
    pageSize: 200,
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

handleQueryEquipmentType()
handleQuery()
</script>
<style>
.line-container {
  height: calc(100vh - 140px);
}
</style>
