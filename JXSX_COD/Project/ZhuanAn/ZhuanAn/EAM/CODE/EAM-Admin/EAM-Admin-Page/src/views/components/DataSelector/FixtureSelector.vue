<template>
  <DataSelectorDialog
    :visible="visible"
    title="选择治具"
    :load-data="loadData"
    :init-query-params="queryParams"
    :row-key="rowKey"
    :multiple="multiple"
    @update:visible="$emit('update:visible', $event)"
    @confirm="$emit('confirm', $event)">
    <template #queryForm="{ queryParams }">
      <el-form-item label="治具名称" prop="fixtureName">
        <el-input v-model="queryParams.fixtureName" placeholder="请输入治具名称" clearable />
      </el-form-item>
      <el-form-item label="系列" prop="series">
        <el-input v-model="queryParams.series" placeholder="请输入系列" clearable />
      </el-form-item>
    </template>

    <template #tableColumns>
      <el-table-column prop="dictValue" label="ID" width="80" />
      <el-table-column prop="dictLabel" label="治具名称" />
    </template>
  </DataSelectorDialog>
</template>

<script setup="FixtureSelector">
import { ref } from 'vue'
import DataSelectorDialog from '@/components/DataSelectorDialog/index.vue'
import { dictFixtureBase } from '@/api/fixture/fixtureBase'

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

const rowKey = 'dictValue'
const queryParams = ref({
  pageNum: 1,
  pageSize: 10,
  sort: '',
  sortType: 'asc',
  fixtureName: '',
  series: ''
})

// 加载数据
const loadData = async (params) => {
  const res = await dictFixtureBase(params)
  return res.data
}
</script>
