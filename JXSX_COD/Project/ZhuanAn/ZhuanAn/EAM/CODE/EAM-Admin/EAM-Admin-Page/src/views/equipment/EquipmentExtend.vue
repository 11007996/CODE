<!--
 * @Descripttion: (设备扩展信息/EQU_Equipment_Extend)
 * @Author: (admin)
 * @Date: (2024-12-09)
-->
<template>
  <div>
    <el-form :model="queryParams" label-position="right" inline ref="queryRef" v-show="showSearch" @submit.prevent>
      <el-form-item label="设备" prop="equipmentId">
        <el-select
          v-model="queryParams.equipmentId"
          placeholder="资产编号/设备名称/资产名称/自定义机型"
          clearable
          filterable
          remote
          :remote-method="handleQueryEquipment">
          <el-option v-for="item in options.equipment_options" :key="item.dictValue" :label="item.dictLabel" :value="item.dictValue"></el-option>
        </el-select>
      </el-form-item>
      <el-form-item label="设备编码" prop="equipmentCode">
        <el-input v-model.number="queryParams.equipmentCode" placeholder="请输入设备编码" />
      </el-form-item>
      <el-form-item label="设备名称" prop="equipmentName">
        <el-input v-model="queryParams.equipmentName" placeholder="请输入设备名称" />
      </el-form-item>
      <el-form-item>
        <el-button icon="search" type="primary" @click="handleQuery">{{ $t('btn.search') }}</el-button>
        <el-button icon="refresh" @click="resetQuery">{{ $t('btn.reset') }}</el-button>
      </el-form-item>
    </el-form>
    <!-- 工具区域 -->
    <el-row :gutter="15" class="mb10">
      <el-col :span="1.5">
        <el-button type="primary" v-hasPermi="['equipment:extend:add']" plain icon="plus" @click="handleAdd">
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
      <el-table-column prop="equipmentId" label="设备ID" align="center" v-if="columns.showColumn('equipmentId')" />
      <el-table-column prop="assetNo" label="资产编号" align="center" width="210" v-if="columns.showColumn('assetNo')" />
      <el-table-column
        prop="equipmentName"
        label="设备名称"
        align="center"
        min-width="200"
        :show-overflow-tooltip="true"
        v-if="columns.showColumn('equipmentName')" />
      <el-table-column prop="equipmentCode" label="设备编码" align="center" width="90" v-if="columns.showColumn('equipmentCode')" />
      <el-table-column prop="equipmentNo" label="设备编号" align="center" width="90" v-if="columns.showColumn('equipmentNo')" />
      <el-table-column prop="theoryCT" label="理论CT" align="center" width="90" v-if="columns.showColumn('theoryCT')" />
      <el-table-column prop="power" label="功率(KW)" align="center" width="90" v-if="columns.showColumn('power')" />
      <el-table-column prop="isLink" label="是否连接" align="center" width="90" v-if="columns.showColumn('isLink')">
        <template #default="scope">
          <dict-tag :options="options.sys_yes_no" :value="scope.row.isLink" />
        </template>
      </el-table-column>
      <el-table-column prop="lineId" label="产线Id" align="center" width="90" v-if="columns.showColumn('lineId')" />
      <el-table-column prop="lineName" label="产线" align="center" width="90" v-if="columns.showColumn('lineName')" />
      <el-table-column prop="ip" label="ip" align="center" width="160" v-if="columns.showColumn('ip')" />
      <el-table-column label="操作" width="120" fixed="right">
        <template #default="scope">
          <el-button
            type="success"
            size="small"
            icon="edit"
            title="编辑"
            v-hasPermi="['equipment:extend:edit']"
            @click="handleUpdate(scope.row)"></el-button>
          <el-button
            type="danger"
            size="small"
            icon="delete"
            title="删除"
            v-hasPermi="['equipment:extend:delete']"
            @click="handleDelete(scope.row)"></el-button>
        </template>
      </el-table-column>
    </el-table>
    <pagination :total="total" v-model:page="queryParams.pageNum" v-model:limit="queryParams.pageSize" @pagination="getList" />

    <!-- 添加或修改设备扩展信息对话框 -->
    <el-dialog :title="title" :lock-scroll="false" v-model="open">
      <el-form ref="formRef" :model="form" :rules="rules" label-width="100px">
        <el-row :gutter="20">
          <el-col :lg="12">
            <el-form-item label="设备" prop="equipmentId">
              <el-select
                v-model="form.equipmentId"
                placeholder="资产编号/设备名称/资产名称/自定义机型"
                clearable
                filterable
                remote
                :remote-method="handleQueryEquipment"
                :disabled="opertype != 1"
                class="fullWidth">
                <el-option
                  v-for="item in options.equipment_options"
                  :key="item.dictValue"
                  :label="item.dictLabel"
                  :value="item.dictValue"></el-option>
              </el-select>
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="设备编码" prop="equipmentCode">
              <template #label>
                <span>
                  <el-tooltip placement="top" content="与设备通信时约定的编码值，每个资产都不相同，唯一">
                    <el-icon> <questionFilled /> </el-icon>
                  </el-tooltip>
                  设备编码
                </span>
              </template>
              <el-input-number v-model.number="form.equipmentCode" controls-position="right" placeholder="请输入设备编码" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="设备编号" prop="equipmentNo">
              <template #label>
                <span>
                  <el-tooltip placement="top" content="数值序号,用于区分相同设备名称的不同设备">
                    <el-icon> <questionFilled /> </el-icon>
                  </el-tooltip>
                  设备编号
                </span>
              </template>
              <el-input-number v-model.number="form.equipmentNo" :controls="true" controls-position="right" placeholder="请输入设备编号" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="理论CT" prop="theoryCT">
              <el-input-number v-model.number="form.theoryCT" :controls="true" controls-position="right" placeholder="请输入理论CT" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="绑定产线" prop="lineId">
              <el-select v-model="form.lineId" placeholder="请选择产线" filterable>
                <el-option
                  v-for="item in useBasicStore().getLineDict"
                  :key="item.dictValue"
                  :label="item.dictLabel"
                  :value="Number(item.dictValue)"></el-option>
              </el-select>
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="功率(KW)" prop="power">
              <el-input-number v-model.number="form.power" :controls="true" controls-position="right" placeholder="请输入功率(KW)" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="是否连接" prop="isLink">
              <template #label>
                <span>
                  <el-tooltip placement="top" content="设备监控时，用于判断是否要在看板上统计显示(是:统计)">
                    <el-icon> <questionFilled /> </el-icon>
                  </el-tooltip>
                  是否连接
                </span>
              </template>
              <el-radio-group v-model="form.isLink">
                <el-radio v-for="item in options.sys_yes_no" :key="item.dictValue" :value="item.dictValue" :label="item.dictValue">
                  {{ item.dictLabel }}
                </el-radio>
              </el-radio-group>
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

<script setup name="equipmentextend">
import {
  listEquipmentExtend,
  addEquipmentExtend,
  delEquipmentExtend,
  updateEquipmentExtend,
  getEquipmentExtend
} from '@/api/equipment/equipmentExtend.js'
import { dictEquipmentBase } from '@/api/equipment/equipmentBase.js'
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
  equipmentId: undefined,
  equipmentCode: undefined,
  equipmentName: undefined
})
const columns = ref([
  { visible: false, prop: 'equipmentId', label: '设备Id' },
  { visible: true, prop: 'assetNo', label: '资产编号' },
  { visible: true, prop: 'equipmentName', label: '设备名称' },
  { visible: true, prop: 'equipmentCode', label: '设备编码' },
  { visible: true, prop: 'equipmentNo', label: '设备编号' },
  { visible: true, prop: 'theoryCT', label: '理论CT' },
  { visible: true, prop: 'power', label: '功率(KW)' },
  { visible: true, prop: 'isLink', label: '是否连接' },
  { visible: false, prop: 'lineId', label: '产线ID' },
  { visible: true, prop: 'lineName', label: '产线' },
  { visible: true, prop: 'ip', label: 'ip' }
])
const total = ref(0)
const dataList = ref([])
const queryRef = ref()
const defaultTime = ref([new Date(2000, 1, 1, 0, 0, 0), new Date(2000, 2, 1, 23, 59, 59)])

var dictParams = [{ dictType: 'sys_yes_no' }]

proxy.getDicts(dictParams).then((response) => {
  response.data.forEach((element) => {
    state.options[element.dictType] = element.list
  })
})

function getList() {
  loading.value = true
  listEquipmentExtend(queryParams).then((res) => {
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
    equipmentId: [{ required: true, message: '设备不能为空', trigger: 'blur' }],
    equipmentCode: [{ required: true, message: '设备编码不能为空', trigger: 'blur' }]
  },
  options: {
    // 是否连接 选项列表 格式 eg:{ dictLabel: '标签', dictValue: '0'}
    sys_yes_no: [],
    // 设备
    equipment_options: []
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
    equipmentId: null,
    equipmentCode: null,
    equipmentName: null,
    equipmentNo: null,
    theoryCT: null,
    power: null,
    isLink: null,
    lineId: null
  }
  proxy.resetForm('formRef')
}

// 添加按钮操作
function handleAdd() {
  reset()
  open.value = true
  title.value = '添加设备扩展信息'
  opertype.value = 1
}
// 修改按钮操作
function handleUpdate(row) {
  reset()
  const id = row.equipmentId || ids.value
  getEquipmentExtend(id).then((res) => {
    const { code, data } = res
    if (code == 200) {
      open.value = true
      title.value = '修改设备扩展信息'
      opertype.value = 2

      form.value = {
        ...data
      }
      options.value.equipment_options = [{ dictValue: form.value.equipmentId, dictLabel: form.value.assetNo + ' : ' + form.value.assetName }]
    }
  })
}

// 添加&修改 表单提交
function submitForm() {
  proxy.$refs['formRef'].validate((valid) => {
    if (valid) {
      if (form.value.equipmentId != undefined && opertype.value === 2) {
        updateEquipmentExtend(form.value).then((res) => {
          proxy.$modal.msgSuccess('修改成功')
          open.value = false
          getList()
        })
      } else {
        addEquipmentExtend(form.value).then((res) => {
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
  const Ids = row.equipmentId || ids.value

  proxy
    .$confirm('是否确认删除参数编号为"' + Ids + '"的数据项？', '警告', {
      confirmButtonText: proxy.$t('common.ok'),
      cancelButtonText: proxy.$t('common.cancel'),
      type: 'warning'
    })
    .then(function () {
      return delEquipmentExtend(Ids)
    })
    .then(() => {
      getList()
      proxy.$modal.msgSuccess('删除成功')
    })
}

// 查询设备
function handleQueryEquipment(keyword) {
  if (keyword) {
    const params = {
      pageNum: 1,
      pageSize: 10,
      sort: '',
      sortType: 'asc',
      keyword: keyword
    }
    setTimeout(() => {
      dictEquipmentBase(params).then((res) => {
        state.options.equipment_options = res.data.result
      })
    }, 200)
  }
}

handleQuery()
</script>
