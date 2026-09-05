<template>
  <Hpage propMessage="prop从下到下传值" propEa="propEa值">   <!--propMessage是子组件的prop参数-->
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

  <navigator  class="textLink" url="../pageto/pageA">跳转到pageA</navigator>
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
  <!-- emitE:子组件事件  exports:子组件暴露的参数和方法 -->
<mycom title="提交2" @emitE="emitE" ref="exports"></mycom> <!-- uni-app默认开启easycom，组件路径：components/组件名称/组件名称.(vue|uvue)-->
<button @click="onExports">exports传值</button>

<!-- 动态类名，动态变更样式 -->
<view class="classC">
  <view :class="{ b: isb, a: isa }">hello uni-app</view>
  <view :style="{'font-size':fontsize + 'rpx'}">字体大小</view>
  <button class="styleify" @click="changeClass">动态样式修改</button> 
  <!-- 多事件处理器 触发多个方法，方法可以作为参数传入，类似与委托，也可以用$event访问事件BOM对象 -->
 <button class="styleify" @click="eventFun(changeClass),eventE($event)">动态传入方法</button>
</view> 

<iframe src="https://www.baidu.com" width="100%" height="300px"></iframe></template>

<script setup>


import Hpage from '../../components/mypage/Hpage.vue'
import {defineAsyncComponent, onMounted, ref} from 'vue'
import { onPullDownRefresh } from '@dcloudio/uni-app'

const title = ref('Hello')
const Storage = ref()
const StorageKey = ref()
const isa = ref(false);
const isb = ref(true);
const fontsize = ref(20);
const exports = ref(null);

// exports子组件暴露的参数和方法
function onExports()
{
  console.log(exports.value.name);
  exports.value.callbackFun();
}

// 子组件事件
function emitE(e)
{
  console.log(e);
}

function eventFun(event){
  // console.log("运行方法");
  event();
}

//动态类名
function changeClass(){
  isa.value=!isa.value;
  isb.value=!isb.value;
  fontsize.value += 5;
  // console.log(isa.value);
}

//下拉刷新
onPullDownRefresh(()=>{
  console.log('触发下拉刷新')
  // 业务逻辑
  
  // 无论成功失败必须关闭动画
  setTimeout(() => {
    uni.stopPullDownRefresh();
  }, 2000);
})

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
  width: 300rpx;
  -webkit-scrollbar-thumb:hover {
  background: #999;
}
  #scrolltext
  {
    padding: 5rpx;
    white-space: nowrap;
    color: #ad453d;
    // overflow: hidden;
    // text-overflow: ellipsis;
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

.classC{
  .b{
    color: aqua;
    font-size:35rpx;
  }
  .a{
    color:#ad453d;
    font-size:35rpx;
  }
  .styleify{
    width: 500rpx;
    height: 100rpx;
    border-radius: 15rpx;
    box-shadow:inset 0px 0px 2rpx #666; /*内阴影(外阴影不写此参数) 水平偏移量 | 垂直偏移量 | 模糊半径 | 阴影颜色 */
  }
}


</style>
