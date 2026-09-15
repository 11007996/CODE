<template>
 <view>{{txtContent}}</view>
  <button @click="requestClick" class="btnre">同步API</button>
  <button @click="requestClickAsync" class="btnre">前端异步API</button>
  <button @click="requestClickHouAsync" class="btnre">后端异步API</button>
</template>

<script setup>
  import {http} from '../../utils/http'
  import {ref} from 'vue';

  const txtContent = ref("初始值");
  function requestClick()
  {
    http({
      url:"/api/Home/strShow",
      method:"GET",
      data:{
        "req":"nihao"
      },
      header:{
        "token":"aaa"
      },
      success:((res)=>{
        txtContent.value = res.data.msg;
      }),
      fail:((res)=>{
        console.log(res)
      })
    })
  }

  async function requestClickAsync()
  {
    //前端异常不要混用 async+success 回调，如果用 await 就不要写 success，二者会同时触发。
    //前端异常是不卡住页面
    const res = await uni.request({
      url:"/api/Home/str",
      method:"GET",
      data:{
        "msg":"nihao"
      }
    });
    if(res.statusCode==200)
    {
      txtContent.value = res.data;
    }
    else
    {
      txtContent.value="请求失败";
    }
  }

  function requestClickHouAsync()
  {
    //后端异常与前端不影响，是两回事
    const res = uni.request({
      url:"/api/Home/str",
      method:"GET",
      data:{
        "msg":"nihao"
      },
      success:((res)=>{
        if(res.statusCode==200)
        {
          txtContent.value = res.data;
        }
          else
        {
          txtContent.value="请求失败";
        }
      })
    });
    
  }
</script>

<style lang="scss" scoped>
  .btnre{
    width: 500rpx;
    height: 80rpx;
    border: 1px solid #aaa;
  }
</style>
