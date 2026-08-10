<template>
  <Hpage>
    <template v-slot:slotname>    <!-- 也可以简写：<template #slotname></template> -->
      <p>插槽</p>
      <a href="https://cn.bing.com/" target="_blank">bing</a>
    </template>
  </Hpage>
  <button class="btn" @click="getData">API调用</button>

  <view class="textLink" @click="storageSave">设置缓存</view>
  <view>缓存：{{ Storage }}</view>
  <view class="textLink" @click="storageRemove('store')">删除缓存</view>
  
  <view class="textLink" @click="chooseFile('store')">选择文件</view>

  <!-- <navigator open-type="reLaunch" class="textLink" url="../pages/pageto/pageA">跳转到pageA</navigator> -->
 <scroll-view scroll-x class="scroll">
   <view id="scrolltext">qwert yui opas dfghjk lzx cvbnmq we rtyuio pasdfghjkl zxcvbn masdfghjk lqwe rtyuiopa sdfgh jklzxcvb nmqwer tyuiopa sdfghjklzxcv bnmas dfghjkl</view>
 </scroll-view>
 <view class="sizeview">
  <p>段落AAA</p>
  <view ></view>
  <p>段落BBBBBB</p>
 </view>
<view>
    <view  class="icon iconfont">&#xe605;</view>
    <view  class="icon iconfont icon-zhenduan"></view>
  </view>


</template>

<script setup>
import Hpage from '../../components/mypage/Hpage.vue'

import {defineAsyncComponent, ref} from 'vue'



const title = ref('Hello')
const Storage = ref()
const StorageKey = ref()

function getData()
{
  //解决跨域问题，在响应头中加入Access-Control-Allow-Origin字段
  uni.request({
    url:"http://localhost:6054/api/home",
    method:"GET",
    //success:(res)=>{console.log(res);}
  }).then((res)=>{
    console.log(res);
  })
}

function storageSave(){
  // uni.navigateTo({
  //   url:"../../components/mypage/pageA"
  // })
  
  // let pagename = getCurrentPages();
  // console.log(pagename[0].route);
 
  uni.setStorageSync("store","storeData");
  uni.setStorageSync("store1","storeData1");
  console.log("缓存完成")
  Storage.value=uni.getStorageSync("store");


  StorageKey.value = uni.getStorageInfoSync();
	console.log(StorageKey._rawValue.keys[1]);

}

function storageRemove(key)
{
  //uni.removeStorageSync(key)
  uni.clearStorageSync()
}

function chooseFile()
{
  uni.chooseFile({
    count: 1, //默认100
  extension:['.txt'],
	success: function (res) {
		console.log(res);
		//console.log(res.tempFiles[0].name);
	}
  });
}

</script>

<style lang="scss" scoped>


.btn{
  width: 250rpx;
  height: 100rpx;
  border-radius: 3rpx;
  font-size: 30rpx;
  align-content: center;
}
.textLink{
  text-decoration: underline;
}
.scroll{
  width: 100%;
  -webkit-scrollbar-thumb:hover {
  background: #999;
}
  #scrolltext
  {
    padding: 5rpx;
    white-space: nowrap;
  }
}
.sizeview
{
  display: block;
  align-items: center;
  justify-items: center;
  view{
    width: 100rpx;
    height: 30rpx;
    border:red solid 1rpx;
  }
}
.icon{
  font-size: 100rpx;
  color: rebeccapurple;
}

</style>
