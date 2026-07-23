<!-- 首页 -->
<template>
  <div class="app-container home">
    <NoticeBar :dataList="noticeList"> </NoticeBar>
    <!-- 卡片信息面板 -->
    <PanelGroup />

    <!-- 柱状图 -->
    <el-row :gutter="20">
      <el-col :lg="12" :sm="24">
        <FixtureBarChart />
      </el-col>
      <el-col :lg="12" :sm="24">
        <ConsumableBarChart />
      </el-col>
    </el-row>

    <!-- 线性图 -->
    <!-- <el-row :gutter="20">
      <el-col :lg="24" :sm="24">
        <LineChart />
      </el-col>
    </el-row> -->
  </div>
</template>

<script setup name="index">
import NoticeBar from '@/components/NoticeBar/index'
import PanelGroup from './home/PanelGroup'
import ConsumableBarChart from './home/ConsumableBarChart'
import FixtureBarChart from './home/FixtureBarChart'
import LineChart from './home/LineChart'

const noticeList = ref(['欢迎使用  【设备管理系统】 '])

import defaultSettings from '@/settings'

const state = reactive({
  factoryNum: 1
})
</script>

<style scoped lang="scss">
.home {
  font-family: 'open sans', 'Helvetica Neue', Helvetica, Arial, sans-serif;
  font-size: 13px;
  color: #676a6c;
  overflow-x: hidden;
}
</style>
