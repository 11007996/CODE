<template>
  <div class="qr-scanner">
    <video ref="video" autoplay playsinline></video>
    <p v-if="lastResult" class="last-result">{{ lastResult }}</p>
    <button @click="toggleCamera">Toggle Camera</button>
  </div>
</template>

<script setup>
import { onMounted, ref } from 'vue'
import { startScan, stopScan } from '@zxing/browser'

const lastResult = ref(null)
const videoElement = ref(null)
let scanner

const toggleCamera = async () => {
  if (scanner) {
    await stopScan(scanner)
  } else {
    scanner = await startCamera()
  }
}

const startCamera = async () => {
  try {
    scanner = await startScan(videoElement.value, {
      numOfWorkers: 2,
      interval: 1000 / 60, // 60 FPS
      maxContinuousScans: 0, // 无限扫描
      scanDelay: 100, // 100 ms
      trackCodeHints: ['QR_CODE'],
      onScan: (result) => {
        lastResult.value = result?.text || ''
        console.log('Decoded:', result.text)
      },
      onInit: (api) => {
        console.log('Initialized:', api)
      },
      onError: (error) => {
        console.error('Error:', error)
      }
    })
  } catch (error) {
    console.error('Error starting camera:', error)
  }
}

onMounted(async () => {
  await startCamera()
})
</script>

<style scoped>
.qr-scanner {
  display: flex;
  justify-content: center;
  align-items: center;
  height: 100vh;
  flex-direction: column;
}

.last-result {
  font-size: 1.5em;
  margin-top: 20px;
}
</style>
