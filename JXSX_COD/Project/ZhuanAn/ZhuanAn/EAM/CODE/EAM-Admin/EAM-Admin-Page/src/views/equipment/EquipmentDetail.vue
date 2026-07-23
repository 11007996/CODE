<!--
 * @Descripttion: (设备管理/详情)
 * @Author: (admin)
 * @Date: (2024-05-07)
-->
<template>
  <div>
    <!-- tab -->
    <el-tabs type="card" v-model="activeTab">
      <el-tab-pane label="设备文件关联" name="equipmentFile">
        <EquipmentFileBind :equipmentId="props.equipmentId" />
      </el-tab-pane>
    </el-tabs>
  </div>
</template>

<script setup name="equipmentdetail">
import EquipmentFileBind from './EquipmentFileBind.vue'
const props = defineProps({
  equipmentId: String
})
const activeTab = ref('equipmentFile')
</script>
