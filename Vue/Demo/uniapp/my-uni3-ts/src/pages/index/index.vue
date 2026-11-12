<template>
  <view class="content">
    <image class="logo" src="/static/logo.png" />
    <view class="text-area">
      <text class="title">{{ title }}</text>
      <button @click="show()">点击</button>
    </view>
    <componT></componT>
  </view>
</template>

<script setup lang="ts">
import { ref } from 'vue'
const title = ref<number>(111111)

function show():void
{
  // if(title.value>50)
  // {
  //   title.value = 12;
  // }
  // else
  // {
  //   title.value = 53;
  // }
  uni.redirectTo({ url: '/pages/first/first' })
  // uni.navigateTo({ url: '/pages/first/first' })
}
</script>

<style lange="scss" scoped>
.content {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
}

.logo {
  height: 200rpx;
  width: 200rpx;
  margin-top: 200rpx;
  margin-left: auto;
  margin-right: auto;
  margin-bottom: 50rpx;
}

.text-area {
  display: flex;
  justify-content: center;
}

.title {
  font-size: 36rpx;
  color: #8f8f94;
}
.show
{
  display:flex;
  justify-content:center;
}
</style>
