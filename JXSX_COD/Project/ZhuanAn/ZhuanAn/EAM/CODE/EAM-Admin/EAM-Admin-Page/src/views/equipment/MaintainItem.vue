<!--
 * @Descripttion: (设备保养项目/EQU_Maintain_Item)
 * @Author: (admin)
 * @Date: (2024-10-07)
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
      <el-form-item>
        <el-button icon="search" type="primary" @click="handleQuery">{{ $t('btn.search') }}</el-button>
        <el-button icon="refresh" @click="resetQuery">{{ $t('btn.reset') }}</el-button>
      </el-form-item>
    </el-form>
    <!-- 工具区域 -->
    <el-row :gutter="15" class="mb10">
      <el-col :span="1.5">
        <el-button type="primary" v-hasPermi="['maintain:item:add']" plain icon="plus" @click="handleAdd">
          {{ $t('btn.add') }}
        </el-button>
      </el-col>
      <el-col :span="1.5">
        <el-dropdown trigger="click" v-hasPermi="['maintain:item:import']">
          <el-button type="primary" plain icon="Upload">
            {{ $t('btn.import') }}<el-icon class="el-icon--right"><arrow-down /></el-icon>
          </el-button>
          <template #dropdown>
            <el-dropdown-menu>
              <el-dropdown-item command="upload">
                <importData
                  templateUrl="equipment/MaintainItem/importTemplate"
                  importUrl="/equipment/MaintainItem/importData"
                  @success="handleFileSuccess"></importData>
              </el-dropdown-item>
            </el-dropdown-menu>
          </template>
        </el-dropdown>
      </el-col>
      <el-col :span="1.5">
        <el-button type="warning" plain icon="download" @click="handleExport" v-hasPermi="['maintain:item:export']">
          {{ $t('btn.export') }}
        </el-button>
      </el-col>
      <el-col :span="1.5">
        <el-button type="primary" plain icon="CopyDocument" @click="handleItemClone" v-hasPermi="['maintain:item:add']"> 克隆 </el-button>
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
      <el-table-column prop="itemId" label="ID" align="center" v-if="columns.showColumn('itemId')" />
      <el-table-column prop="equipmentId" label="设备Id" align="center" v-if="columns.showColumn('equipmentId')" />
      <el-table-column prop="assetNo" label="资产编号" align="center" width="210" v-if="columns.showColumn('assetNo')" />
      <el-table-column
        prop="assetName"
        label="资产名称"
        align="center"
        min-width="150"
        :show-overflow-tooltip="true"
        v-if="columns.showColumn('assetName')" />
      <el-table-column
        prop="equipmentName"
        label="设备名称"
        align="center"
        min-width="150"
        :show-overflow-tooltip="true"
        v-if="columns.showColumn('equipmentName')" />
      <el-table-column prop="dateMark" label="日期标记" align="center" width="90" v-if="columns.showColumn('dateMark')">
        <template #default="scope">
          <dict-tag :options="options.date_mark" :value="scope.row.dateMark" />
        </template>
      </el-table-column>
      <el-table-column prop="itemName" label="项目名称" min-width="200" :show-overflow-tooltip="true" v-if="columns.showColumn('itemName')" />
      <el-table-column prop="sortNo" label="排序" align="center" width="90" v-if="columns.showColumn('sortNo')" />
      <el-table-column label="操作" width="110">
        <template #default="scope">
          <el-button
            type="success"
            size="small"
            icon="edit"
            title="编辑"
            v-hasPermi="['maintain:item:edit']"
            @click="handleUpdate(scope.row)"></el-button>
          <el-button
            type="danger"
            size="small"
            icon="delete"
            title="删除"
            v-hasPermi="['maintain:item:delete']"
            @click="handleDelete(scope.row)"></el-button>
        </template>
      </el-table-column>
    </el-table>
    <pagination :total="total" v-model:page="queryParams.pageNum" v-model:limit="queryParams.pageSize" @pagination="getList" />

    <!-- 添加或修改设备保养项目对话框 -->
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
            <el-form-item label="日期标记" prop="dateMark">
              <el-select v-model="form.dateMark" placeholder="请选择日期标记">
                <el-option v-for="item in options.date_mark" :key="item.dictValue" :label="item.dictLabel" :value="item.dictValue"></el-option>
              </el-select>
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="项目名称" prop="itemName">
              <el-input v-model="form.itemName" placeholder="请输入项目名称" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="排序" prop="sortNo">
              <el-input-number v-model.number="form.sortNo" :controls="true" controls-position="right" placeholder="请输入排序" />
            </el-form-item>
          </el-col>
        </el-row>
      </el-form>
      <template #footer v-if="opertype != 3">
        <el-button text @click="cancel">{{ $t('btn.cancel') }}</el-button>
        <el-button type="primary" @click="submitForm">{{ $t('btn.submit') }}</el-button>
      </template>
    </el-dialog>

    <!-- 克隆 -->
    <el-dialog :title="title" :lock-scroll="true" v-model="openItemClone" v-if="openItemClone" width="900">
      <MaintainItemClone></MaintainItemClone>
    </el-dialog>
  </div>
</template>

<script setup name="maintainitem">
import { listMaintainItem, addMaintainItem, delMaintainItem, updateMaintainItem, getMaintainItem } from '@/api/equipment/maintainItem.js'
import importData from '@/components/ImportData'
import { dictEquipmentBase } from '@/api/equipment/equipmentBase.js'
import MaintainItemClone from './MaintainItemClone.vue'
const props = defineProps({ equipmentId: Number })
const { proxy } = getCurrentInstance()
const ids = ref([])
const loading = ref(false)
const showSearch = ref(true)
const queryParams = reactive({
  pageNum: 1,
  pageSize: 10,
  sort: 'SortNo',
  sortType: 'asc',
  equipmentId: props.equipmentId
})
const columns = ref([
  { visible: false, prop: 'itemId', label: 'ID' },
  { visible: false, prop: 'equipmentId', label: '设备ID' },
  { visible: !props.equipmentId, prop: 'assetNo', label: '资产编号' },
  { visible: !props.equipmentId, prop: 'assetName', label: '资产名称' },
  { visible: !props.equipmentId, prop: 'equipmentName', label: '设备名称' },
  { visible: true, prop: 'dateMark', label: '日期标记' },
  { visible: true, prop: 'itemName', label: '项目名称' },
  { visible: true, prop: 'sortNo', label: '排序' }
])
const total = ref(0)
const dataList = ref([])
const queryRef = ref()
const defaultTime = ref([new Date(2000, 1, 1, 0, 0, 0), new Date(2000, 2, 1, 23, 59, 59)])

var dictParams = [{ dictType: 'date_mark' }]

proxy.getDicts(dictParams).then((response) => {
  response.data.forEach((element) => {
    state.options[element.dictType] = element.list
  })
})

function getList() {
  loading.value = true
  listMaintainItem(queryParams).then((res) => {
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
    dateMark: [{ required: true, message: '日期标记不能为空', trigger: 'change' }],
    itemName: [{ required: true, message: '项目名称不能为空', trigger: 'blur' }]
  },
  options: {
    // 日期标记 选项列表 格式 eg:{ dictLabel: '标签', dictValue: '0'}
    date_mark: [],
    // 资产编号 选项列表
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
    itemId: null,
    equipmentId: null,
    dateMark: null,
    itemName: null,
    sortNo: null
  }
  options.equipment_options = []
  proxy.resetForm('formRef')
}

// 添加按钮操作
function handleAdd() {
  reset()
  open.value = true
  title.value = '添加设备保养项目'
  opertype.value = 1
}
// 修改按钮操作
function handleUpdate(row) {
  reset()
  const id = row.itemId || ids.value
  getMaintainItem(id).then((res) => {
    const { code, data } = res
    if (code == 200) {
      open.value = true
      title.value = '修改设备保养项目'
      opertype.value = 2

      form.value = {
        ...data
      }
      options.value.equipment_options = [{ dictValue: form.value.equipmentId, dictLabel: form.value.assetNo }]
    }
  })
}

// 添加&修改 表单提交
function submitForm() {
  proxy.$refs['formRef'].validate((valid) => {
    if (valid) {
      if (form.value.itemId != undefined && opertype.value === 2) {
        updateMaintainItem(form.value).then((res) => {
          proxy.$modal.msgSuccess('修改成功')
          open.value = false
          getList()
        })
      } else {
        addMaintainItem(form.value).then((res) => {
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
  const Ids = row.itemId || ids.value

  proxy
    .$confirm('是否确认删除参数编号为"' + Ids + '"的数据项？', '警告', {
      confirmButtonText: proxy.$t('common.ok'),
      cancelButtonText: proxy.$t('common.cancel'),
      type: 'warning'
    })
    .then(function () {
      return delMaintainItem(Ids)
    })
    .then(() => {
      getList()
      proxy.$modal.msgSuccess('删除成功')
    })
}

// 导入数据成功处理
const handleFileSuccess = (response) => {
  const { item1, item2 } = response.data
  var error = ''
  item2.forEach((item) => {
    error += item.storageMessage + ','
  })
  proxy.$alert(item1 + '<p>' + error + '</p>', '导入结果', {
    dangerouslyUseHTMLString: true
  })
  getList()
}

// 导出按钮操作
function handleExport() {
  proxy
    .$confirm('是否确认导出设备保养项目数据项?', '警告', {
      confirmButtonText: '确定',
      cancelButtonText: '取消',
      type: 'warning'
    })
    .then(async () => {
      await proxy.downFile('/equipment/MaintainItem/export', { ...queryParams })
    })
}

// 查询资产编号
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

//克隆
const openItemClone = ref(false)
function handleItemClone() {
  title.value = '设备保养项目克隆'
  openItemClone.value = true
}

handleQuery()
</script>
