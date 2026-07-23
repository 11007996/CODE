<template>
  <div ref="chartRef" :class="className" :style="{ height: height, width: width }" />
</template>

<script setup>
import * as echarts from 'echarts'
import { daysStatStatisticsData } from '@/api/statistics/statisticsData.js'
import { onBeforeUnmount } from 'vue'
let chart = null
const { proxy } = getCurrentInstance()
const animationDuration = 6000
const props = defineProps({
  className: {
    type: String,
    default: 'chart'
  },
  width: {
    type: String,
    default: '100%'
  },
  height: {
    type: String,
    default: '300px'
  }
})

const keys = ['consumableInCount', 'consumableReceiveCount']
let xData = []
let seriesDataOptions = {}

function initChart() {
  chart = echarts.init(proxy.$refs.chartRef, 'macarons')

  chart.setOption({
    tooltip: {
      trigger: 'axis',
      axisPointer: {
        // 坐标轴指示器，坐标轴触发有效
        type: 'shadow' // 默认为直线，可选为：'line' | 'shadow'
      }
    },
    title: {
      text: '耗品入库领用',
      // 文字属性设置
      textStyle: {
        color: '#00e4ff'
      }
    },
    grid: {
      top: 60,
      left: '2%',
      right: '2%',
      bottom: '3%',
      containLabel: true
    },
    legend: {
      orient: 'vertical',
      right: 10,
      top: 'top'
    },
    xAxis: [
      {
        type: 'category',
        data: xData,
        axisTick: {
          alignWithLabel: true
        }
      }
    ],
    yAxis: [
      {
        type: 'value',
        axisTick: {
          show: false
        }
      }
    ],
    series: [
      {
        name: '入库',
        type: 'bar',
        color: ['#54c010'],
        data: seriesDataOptions[keys[0]],
        animationDuration,
        label: {
          show: true
        }
      },
      {
        name: '领用',
        type: 'bar',
        color: ['#fa796f'],
        data: seriesDataOptions[keys[1]],
        animationDuration,
        label: {
          show: true
        }
      }
    ]
  })
}

// upMounted(() => {
//   initChart()
// })

//刷新数据
function refreshData() {
  const query = {
    names: keys.join(','),
    days: 7
  }
  daysStatStatisticsData(query).then((res) => {
    const { code, data } = { ...res }
    if (code == 200) {
      xData = []
      seriesDataOptions = {}
      keys.forEach((key) => {
        xData = []
        seriesDataOptions[key] = []
        data[key].forEach((item) => {
          xData.push(item.metricName)
          seriesDataOptions[key].push({ value: item.metricValue })
        })
      })
      updateChartData()
    }
  })
}

function updateChartData() {
  chart.setOption({
    xAxis: [
      {
        data: xData
      }
    ],
    series: [
      {
        data: []
      },
      {
        data: []
      }
    ]
  })
  chart.setOption({
    xAxis: [
      {
        data: xData
      }
    ],
    series: [
      {
        data: seriesDataOptions[keys[0]]
      },
      {
        data: seriesDataOptions[keys[1]]
      }
    ]
  })
}

const intervalId = setInterval(function () {
  refreshData()
}, 60 * 1000)

onMounted(() => {
  initChart()
})

onBeforeUnmount(() => {
  clearInterval(intervalId)
})
refreshData()
</script>
