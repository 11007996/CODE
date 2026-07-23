<!--
 * @Descripttion: (流程定义/WF_Process_Define)
 * @Author: (admin)
 * @Date: (2024-06-04)
-->
<template>
  <div>
    <!-- 头部信息 -->
    <div>
      <el-menu :default-active="activeIndex" mode="horizontal" @select="handleSelect">
        <el-menu-item index="Pending">待处理</el-menu-item>
        <el-menu-item index="Processed">已处理</el-menu-item>
        <el-menu-item index="Completed">已完结</el-menu-item>
        <el-menu-item index="Created">我的请求</el-menu-item>
      </el-menu>
    </div>

    <div class="tabPanel">
      <div v-if="activeIndex == 'Pending'">
        <ProcessInstance status="Pending"></ProcessInstance>
      </div>

      <div v-if="activeIndex == 'Processed'">
        <ProcessInstance status="Processed"></ProcessInstance>
      </div>
      <div v-if="activeIndex == 'Completed'">
        <ProcessInstance status="Completed"></ProcessInstance>
      </div>
      <div v-if="activeIndex == 'Created'">
        <ProcessInstance status="Created"></ProcessInstance>
      </div>
    </div>
  </div>
</template>

<script setup name="myprocess">
import ProcessInstance from './ProcessInstance.vue'
const activeIndex = ref('Pending')

function handleSelect(index) {
  activeIndex.value = index
}
</script>
<style>
.tabPanel {
  padding: 10px;
}
</style>
