<!--
 * @Descripttion: (设备保养项目克隆/MaintainItemClone)
 * @Author: (admin)
 * @Date: (2024-10-07)
-->
<template>
  <div>
    <el-row :gutter="20">
      <!-- 左侧:项目列表 -->
      <el-col :md="10">
        <el-form :model="queryParams" label-position="right" inline v-show="showSearch" @submit.prevent>
          <el-form-item label="克隆设备" prop="equipmentId" class="fullWidth">
            <el-select
              v-model="queryParams.equipmentId"
              placeholder="资产编号/设备名称/资产名称/自定义机型"
              clearable
              filterable
              remote
              :remote-method="handleQueryEquipment"
              @change="handleEquipmentChange"
              class="fullWidth">
              <el-option
                v-for="item in options.equipment_options"
                :key="item.dictValue"
                :label="item.dictLabel"
                :value="item.dictValue"></el-option>
            </el-select>
          </el-form-item>
        </el-form>

        <el-table :data="dataList" v-loading="loading" border header-cell-class-name="el-table-header-cell" highlight-current-row height="430">
          <el-table-column prop="dateMark" label="日期标记" align="center" width="90">
            <template #default="scope">
              <dict-tag :options="options.date_mark" :value="scope.row.dateMark" />
            </template>
          </el-table-column>
          <el-table-column prop="itemName" label="项目名称" min-width="200" :show-overflow-tooltip="true" />
        </el-table>
      </el-col>

      <!-- 右侧:设备列表 -->
      <el-col :md="14">
        <el-form :model="queryParams2" label-position="right" inline @submit.prevent>
          <el-form-item label="覆盖目标设备(多选)" prop="equipmentId" class="fullWidth">
            <el-select
              v-model="queryParams2.equipmentId"
              placeholder="资产编号/设备名称/资产名称/自定义机型"
              clearable
              filterable
              remote
              :remote-method="handleQueryEquipment"
              @change="handleEquipmentChange2"
              class="fullWidth">
              <el-option
                v-for="item in options.equipment_options"
                :key="item.dictValue"
                :label="item.dictLabel"
                :value="item.dictValue"></el-option>
            </el-select>
          </el-form-item>
        </el-form>

        <el-table :data="dataList2" v-loading="loading2" border header-cell-class-name="el-table-header-cell" highlight-current-row height="430">
          <el-table-column prop="assetName" label="资产名称" min-width="200" />
          <el-table-column label="操作" width="65" align="center">
            <template #default="scope">
              <el-button type="danger" plain icon="delete" size="small" @click="deleteRow(scope.$index, dataList2)"> </el-button>
            </template>
          </el-table-column>
        </el-table>
      </el-col>
    </el-row>

    <el-row style="justify-content: end; margin-top: 10px">
      <el-button type="primary" @click="submitForm">{{ $t('btn.submit') }}</el-button>
    </el-row>
  </div>
</template>

<script setup name="maintainitemclone">
import { listMaintainItem, cloneMaintainItem } from '@/api/equipment/maintainItem.js'
import { dictEquipmentBase } from '@/api/equipment/equipmentBase.js'
const { proxy } = getCurrentInstance()

var dictParams = [{ dictType: 'date_mark' }]

proxy.getDicts(dictParams).then((response) => {
  response.data.forEach((element) => {
    state.options[element.dictType] = element.list
  })
})

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

/************************* 左侧 **************************/
const loading = ref(false)
const showSearch = ref(true)
const queryParams = reactive({
  pageNum: 1,
  pageSize: 1000,
  equipmentId: undefined
})
const dataList = ref([])

//获取设备的预订保养项目列表
function getList() {
  loading.value = true
  listMaintainItem(queryParams).then((res) => {
    const { code, data } = res
    if (code == 200) {
      dataList.value = data.result
      loading.value = false
    }
  })
}

// 左侧资产选项变更事件
function handleEquipmentChange(keyword) {
  if (keyword) getList()
  else dataList.value = []
}

/************************* 右侧 **************************/
const loading2 = ref(false)
const queryParams2 = reactive({
  pageNum: 1,
  pageSize: 1000,
  equipmentId: undefined
})
const dataList2 = ref([])

//资产选项变更事件
function handleEquipmentChange2(keyword) {
  if (!queryParams.equipmentId) {
    proxy.$modal.msgError('请先选择克隆的设备资产')
    return
  }
  if (keyword && keyword === queryParams.equipmentId) {
    proxy.$modal.msgError('覆盖目标设备资产编号不能与克隆设备相同')
    return
  }
  if (keyword === '') return

  //当前选中项目
  const currOption = state.options.equipment_options.find((option) => option.dictValue === keyword)
  if (currOption) {
    //检查是否已存在相同资产编号
    let isExist = false
    dataList2.value.forEach((item) => {
      if (item.equipmentId === currOption.dictValue) {
        isExist = true
        return
      }
    })
    //添加到表数据中
    if (!isExist) dataList2.value.push({ equipmentId: currOption.dictValue, assetName: currOption.dictLabel })
  }
}

//行数据删除
function deleteRow(index, rows) {
  rows.splice(index, 1)
}

/*************** form操作 ***************/
const state = reactive({
  form: {},
  options: {
    // 日期标记 选项列表 格式 eg:{ dictLabel: '标签', dictValue: '0'}
    date_mark: [],
    // 资产编号 选项列表
    equipment_options: []
  }
})

const { form, options } = toRefs(state)

// 添加&修改 表单提交
function submitForm() {
  if (!queryParams.equipmentId) {
    proxy.$modal.msgError('未选择克隆设备')
    return
  }
  if (dataList2.value.length <= 0) {
    proxy.$modal.msgError('未选择覆盖目标设备')
    return
  }

  let equipmentList = dataList2.value
    .map((obj, index) => {
      return obj.equipmentId
    })
    .join(',')
    .split(',')

  form.value = {
    fromEquipmentId: queryParams.equipmentId,
    toEquipmentIdList: equipmentList
  }

  cloneMaintainItem(form.value).then((res) => {
    proxy.$modal.msgSuccess('克隆成功')
  })
}
</script>
