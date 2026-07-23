<template>
  <!-- 卡片信息面板 -->
  <el-row :gutter="40" class="panel-group">
    <!-- 流程待处理 -->
    <!-- <el-col :xs="12" :sm="12" :lg="6" class="card-panel-col">
      <div class="card-panel">
        <div class="card-panel-icon-wrapper icon-workflow">
          <svg-icon name="flowCenter" class-name="card-panel-icon" />
        </div>
        <div class="card-panel-description">
          <el-statistic title="流程待处理" :value="state.ProcessPendingCount" />
        </div>
      </div>
    </el-col> -->
    <!-- 设备总数 -->
    <el-col :xs="12" :sm="12" :lg="6" class="card-panel-col">
      <div class="card-panel">
        <div class="card-panel-icon-wrapper icon-workflow">
          <svg-icon name="equipment" class-name="card-panel-icon" />
        </div>
        <div class="card-panel-description">
          <el-statistic title="设备总数" :value="state.EquipmentTotalCount" />
        </div>
      </div>
    </el-col>
    <!-- 治具总数 -->
    <el-col :xs="12" :sm="12" :lg="6" class="card-panel-col">
      <div class="card-panel">
        <div class="card-panel-icon-wrapper icon-fixture">
          <svg-icon name="fixture" class-name="card-panel-icon" />
        </div>
        <div class="card-panel-description">
          <el-statistic title="治具总数" :value="state.FixtureTotalCount" />
        </div>
      </div>
    </el-col>
    <!-- 耗品总数 -->
    <el-col :xs="12" :sm="12" :lg="6" class="card-panel-col">
      <div class="card-panel">
        <div class="card-panel-icon-wrapper icon-consumable">
          <svg-icon name="consumable" class-name="card-panel-icon" />
        </div>
        <div class="card-panel-description">
          <el-statistic title="耗品总数" :value="state.ConsumableTotalCount" />
        </div>
      </div>
    </el-col>
    <!-- 厂区资产 -->
    <el-col :xs="12" :sm="12" :lg="6" class="card-panel-col">
      <div class="card-panel">
        <div class="card-panel-icon-wrapper icon-money">
          <svg-icon name="money" class-name="card-panel-icon" />
        </div>
        <div class="card-panel-description">
          <el-statistic title="总资产" :value="state.AssetTotalCost" :precision="2" />
        </div>
      </div>
    </el-col>
  </el-row>
</template>

<script setup name="index">
import { newestStatStatisticsData } from '@/api/statistics/statisticsData.js'
import { PendingCountProcessInstance } from '@/api/workflow/processInstance.js'

const state = reactive({
  ProcessPendingCount: 0,
  EquipmentTotalCount: 0,
  FixtureTotalCount: 0,
  ConsumableTotalCount: 0,
  AssetTotalCost: 0.0
})

function getNewestData() {
  const query = {
    names: 'EquipmentTotalCount,FixtureTotalCount,ConsumableTotalCount,AssetTotalCost'
  }
  newestStatStatisticsData(query).then((res) => {
    res.data.forEach((item) => {
      state[item.metricName] = item.metricValue
    })
  })
}

function getProcessPendingCount() {
  PendingCountProcessInstance().then((res) => {
    const { code, data } = res
    if (code == 200) {
      state.ProcessPendingCount = data
    }
  })
}

const intervalId = setInterval(function () {
  // getProcessPendingCount()
  getNewestData()
}, 60 * 1000)

onBeforeUnmount(() => {
  clearInterval(intervalId)
})

// getProcessPendingCount()
getNewestData()
</script>

<style scoped lang="scss">
.panel-group {
  margin-top: 18px;

  .card-panel-col {
    margin-bottom: 32px;
  }

  .card-panel {
    height: 90px;
    cursor: pointer;
    font-size: 12px;
    position: relative;
    overflow: hidden;
    color: #666;
    // background: var(--base-bg-main);
    box-shadow: 4px 4px 40px rgba(0, 0, 0, 0.05);
    border-color: rgba(0, 0, 0, 0.05);
    display: flex;
    justify-content: space-between;
    align-items: center;

    &:hover {
      .card-panel-icon-wrapper {
        color: #fff;
      }

      .icon-workflow {
        background: #5240c9;
      }

      .icon-fixture {
        background: #36a3f7;
      }

      .icon-money {
        background: #f4516c;
      }

      .icon-consumable {
        background: #34bfa3;
      }
    }

    .icon-workflow {
      color: #5240c9;
    }

    .icon-fixture {
      color: #36a3f7;
    }

    .icon-money {
      color: #f4516c;
    }

    .icon-consumable {
      color: #34bfa3;
    }

    .card-panel-icon-wrapper {
      // float: left;
      margin: 0px 0 0 14px;
      padding: 16px;
      transition: all 0.38s ease-out;
      border-radius: 6px;
    }

    .card-panel-icon {
      float: left;
      font-size: 28px;
    }

    .card-panel-description {
      // float: right;
      font-weight: bold;
      margin-right: 20px;
      margin-left: 0px;

      .card-panel-text {
        line-height: 18px;
        color: var(--base-color-white);
        font-size: 16px;
        margin-bottom: 12px;
      }

      .card-panel-num {
        font-size: 20px;
        color: var(--base-color-white);
      }
    }
  }
}

@media (max-width: 550px) {
  .card-panel-description {
    display: none;
  }

  .card-panel-icon-wrapper {
    float: none !important;
    width: 100%;
    height: 100%;
    margin: 0 !important;

    .svg-icon {
      display: block;
      margin: 14px auto !important;
      float: none !important;
    }
  }
}
</style>
