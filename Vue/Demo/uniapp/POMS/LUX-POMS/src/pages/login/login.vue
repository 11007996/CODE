<template>
  <view class="login">
    <view class="logo">
      <view  class="icon iconfont icon-gongyesheji"></view>
      <text class="titleHeader">生产操作管理系统</text>
    </view>
    <uni-forms class="loginFrom" ref="loginFrom" :model="loginData" :rules="rules">
      <uni-forms-item name="userNo" required label="账号">
        <uni-easyinput
          class="inputText"
          prefixIcon="person-filled"
          v-model="loginData.userNo"
          placeholder="请输入用户名"
        ></uni-easyinput>
      </uni-forms-item>
      <uni-forms-item name="password" required label="密码">
        <uni-easyinput
          class="inputText"
          type="password"
          prefixIcon="person-filled"
          v-model="loginData.password"
          placeholder="请输入密码"
        ></uni-easyinput>
        </uni-forms-item>
      <uni-forms-item>
        <button class="loginBtn" type="primary" :loading="loading" @click="loginClick">登录</button>
      </uni-forms-item>
    </uni-forms>
    <view class="property">
      <p>Copyright @ 2026 CPBG_智能资讯处</p>
    </view>
  </view>
</template>




<script setup>
    import {login,getAppVersion} from '../../api/base'
    import {app_version} from '../../utils/config'
    import {ref,reactive} from 'vue'
    import { onLoad } from '@dcloudio/uni-app';
    const loading = ref(false)
    const loginFrom = ref(null)
    const loginData=reactive({
        userNo:'',
        password:'',
    })

    const rules = reactive({
      userNo:{
        rules: [
          { required: true, errorMessage: '账号不能为空' },
          // { minLength: 8,maxLength:10, errorMessage: '账号长度8-10位' }
        ]},
      password: {
        rules: [
        { required: true, errorMessage: '密码不能为空' },
        { minLength: 7,maxLength:12, errorMessage: '密码长度7-12位' }
      ]}
    })

    onLoad(async ()=>{
      const res =await getAppVersion();
      if(res.data.status===0)
      {
        const newVersion = res.data.data[0];
        if(app_version!==newVersion)
        {
          const nowtime = new Date();
          const newtime = new Date(res.data.data[1]);
          const timespan = (nowtime-newtime) / (1000 * 60 * 60 * 24);
          if(timespan>5)
          {
            uni.showModal({
              title:"更新通知",
              content:"版本强制更新...",
              showCancel:false,
              success:function(){
                console.log("更新安装包")
                uni.showLoading({
                	title: '下载中，请稍后',
                  mask:true
                });
                //下载安装包
              }
            })
          }
          else
          {
            uni.showModal({
              title:"更新通知",
              content:"有效期剩余"+Math.round(5-timespan)+"天,请更新系统...",
              showCancel:true,
              cancelText:"暂不更新",
              confirmText:"立即更新",
              success:function(res){
                if(res.confirm)
                {
                  console.log("更新安装包")
                  //下载安装包
                }
                else
                {
                  console.log("暂不更新")
                }
                
              }
            })
          }
        }
      }
    })


    async function loginClick()
    {
      if (!loginFrom.value) return
        try{
          await loginFrom.value.validate()
          const res = await login({
              UserNo: loginData.userNo,
              Password: loginData.password,
              Appversion:app_version
            });
            // console.log(res)
          if (res.data.status === 0) {
              uni.setStorage({
                key: 'token',
                data: res?.data?.data[0]?.token,
                success: async () => {
					      //const res1 = await getAuthority()
					      uni.setStorageSync('expiration', res?.data?.data[0]?.expiration)
                },
              });
              uni.setStorage({
                key: 'userNo',
                data: loginData.userNo,
              });
              uni.setStorage({
                key: 'userId',
                data: res.data.data[1].userId,
                success: async () => {
                uni.switchTab({
                    url: '/pages/index/index',
                  });
                }
              });
            }
        }catch(err){
          console.log('校验失败',err)
          uni.showToast({
            title: err,
            icon:'none',
	          duration: 3000
          });
    }
}
</script>






<style lang="scss" scoped>
@import '@/static/style/base-style.scss';
.login{
  .logo{
    display:flex;
    align-items: center;
    justify-content: center;
    margin-top: 250rpx;
    margin-bottom: 150rpx;
    .icon.iconfont{
      font-size: 50rpx;
      color:$lux-primary;
    }

    .titleHeader
    {
      display: flex;
      margin-left: 30rpx;
      font-size: 50rpx;
      color:$lux-primary;
    }
  }
  .loginFrom{
      display:flex;
      justify-content:center;
      align-items: center;
    .inputText{
      width:550rpx;
      margin-bottom: 20rpx;
    }
    .loginBtn{
      height:80rpx;
      border-radius: 40rpx;
      border:1px solid $lux-primary;
      background-color: $lux-primary;
      color:white;
      margin-top: 50rpx;
      text-align: center;
      line-height: 80rpx;
      
    }
  }
  .property{
  display:flex;
  justify-content: center;
  align-items: center;
  position: fixed;
  bottom: 30rpx;
  left:0;
  right:0;
  color:gray;
  font-size:25rpx;
}
}


</style>