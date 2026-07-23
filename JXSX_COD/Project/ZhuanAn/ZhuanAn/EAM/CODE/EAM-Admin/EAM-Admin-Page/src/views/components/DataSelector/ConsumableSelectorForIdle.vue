<template>
  <DataSelectorDialog
    :visible="visible"
    title="选择耗品"
    :load-data="loadData"
    :init-query-params="queryParams"
    :row-key="rowKey"
    :multiple="multiple"
    @update:visible="$emit('update:visible', $event)"
    @confirm="$emit('confirm', $event)">
    <template #queryForm="{ queryParams }">
      <el-form-item label="耗品名称" prop="consumableName">
        <el-input v-model="queryParams.consumableName" placeholder="请输入耗品名称" clearable />
      </el-form-item>
      <el-form-item label="料号" prop="consumablePart">
        <el-input v-model="queryParams.consumablePart" placeholder="请输入料号" clearable />
      </el-form-item>
      <el-form-item label="规格" prop="spec">
        <el-input v-model="queryParams.spec" placeholder="请输入规格" clearable />
      </el-form-item>
    </template>

    <template #tableColumns>
      <!-- <el-table-column prop="consumableId" label="ID" width="80" /> -->
      <el-table-column prop="consumableName" label="耗品名称" />
      <el-table-column prop="consumablePart" label="料号" />
      <el-table-column prop="spec" label="规则" />
      <el-table-column prop="price" label="单价" />
      <el-table-column prop="totalStackQty" label="库存数量" />
    </template>
  </DataSelectorDialog>
</template>

<script setup="consumableselectorforidle">
import { ref } from 'vue'
import DataSelectorDialog from '@/components/DataSelectorDialog/index.vue'
import { idleConsumableBase } from '@/api/consumable/consumableBase.js'

const props = defineProps({
  visible: {
    type: Boolean,
    default: false
  },
  multiple: {
    type: Boolean,
    default: false
  }
})

const emit = defineEmits(['update:visible', 'confirm'])

const rowKey = 'consumableId'
const queryParams = ref({
  pageNum: 1,
  pageSize: 10,
  sort: '',
  sortType: 'asc',
  consumableName: '',
  consumablePart: '',
  spec: ''
})

// 加载数据
const loadData = async (params) => {
  const res = await idleConsumableBase(params)
  return res.data
}
</script>
