<!--
 * @Descripttion: (治具管理/详情)
 * @Author: (admin)
 * @Date: (2024-05-07)
-->
<template>
  <div>
    <!-- tab -->
    <el-tabs type="card" v-model="activeTab">
      <el-tab-pane :label="$t('fixture.fixtureStorage')" name="fixtureStorage">
        <FixtureStorage :fixtureId="props.fixtureId" />
      </el-tab-pane>
      <el-tab-pane :label="$t('fixture.fixtureStorageUsing')" name="fixtureStorageUsing">
        <FixtureStorageUsing :fixtureId="props.fixtureId" />
      </el-tab-pane>
      <el-tab-pane :label="$t('fixture.fixtureStorageRecord')" name="fixtureStorageRecord">
        <FixtureStorageRecord :fixtureId="props.fixtureId" />
      </el-tab-pane>
      <el-tab-pane :label="$t('fixture.fixtureBindPart')" name="fixtureBindPart">
        <FixturePart :fixtureId="props.fixtureId" />
      </el-tab-pane>
      <el-tab-pane :label="$t('fixture.fixtureBindFile')" name="fixtureBindFile">
        <FixtureFile :fixtureId="props.fixtureId" />
      </el-tab-pane>
    </el-tabs>
  </div>
</template>

<script setup name="fixturedetail">
import FixtureStorage from './FixtureStorage.vue'
import FixtureStorageRecord from './FixtureStorageRecord.vue'
import FixturePart from './FixturePart.vue'
import FixtureFile from './FixtureFile.vue'
import FixtureStorageUsing from './FixtureStorageUsing.vue'

const props = defineProps({
  fixtureId: Number
})

const activeTab = ref('fixtureStorage')
</script>
