<template>
  <div ref="chartRef" :class="className" :style="{ height: height, width: width }" />
</template>

<script setup>
import * as echarts from 'echarts'
import { daysStatStatisticsData } from '@/api/statistics/statisticsData.js'
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
const names = ['fixtureReceiveCount', 'fixtureBackCount']
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
      text: '治具领用归还',
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
        name: '领用',
        type: 'bar',
        color: ['#fa796f'],
        data: seriesDataOptions[names[0]],
        animationDuration,
        label: {
          show: true
        }
      },
      {
        name: '归还',
        type: 'bar',
        color: ['#54c1fb'],
        data: seriesDataOptions[names[1]],
        animationDuration,
        label: {
          show: true
        }
      }
    ]
  })
}

function refreshData() {
  const query = {
    names: names.join(','),
    days: 7
  }
  daysStatStatisticsData(query).then((res) => {
    const { code, data } = { ...res }
    if (code == 200) {
      xData = []
      seriesDataOptions = {}
      names.forEach((name) => {
        xData = []
        seriesDataOptions[name] = []
        data[name].forEach((item) => {
          xData.push(item.metricName)
          seriesDataOptions[name].push({ value: item.metricValue })
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
        data: seriesDataOptions[names[0]]
      },
      {
        data: seriesDataOptions[names[1]]
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
