<!--
 * @Descripttion: (耗品管理/详情)
 * @Author: (admin)
 * @Date: (2024-05-07)
-->
<template>
  <div>
    <!-- tab -->
    <el-tabs type="card" v-model="activeTab">
      <el-tab-pane label="耗品库存" name="consumableStorage">
        <ConsumableStorage :consumableId="props.consumableId" />
      </el-tab-pane>
      <el-tab-pane label="耗品存储记录" name="consumableStorageRecord">
        <ConsumableStorageRecord :consumableId="props.consumableId" />
      </el-tab-pane>
    </el-tabs>
  </div>
</template>

<script setup name="consumabledetail">
import ConsumableStorage from './ConsumableStorage.vue'
import ConsumableStorageRecord from './ConsumableStorageRecord.vue'

const props = defineProps({
  consumableId: Number
})
const activeTab = ref('consumableStorage')
</script>
