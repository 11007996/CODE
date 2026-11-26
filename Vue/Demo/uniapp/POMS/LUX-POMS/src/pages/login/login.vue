<template>
  <view class="login">
    <view class="logo">
      <view  class="icon iconfont icon-gongyesheji"></view>
      <text class="titleHeader">生产操作管理系统</text>
    </view>
    <uni-forms class="loginFrom" ref="loginFrom" :model="loginData" :rules="rules">
      <uni-forms-item name="userName" required label="账号">
        <uni-easyinput
          class="inputText"
          prefixIcon="person-filled"
          v-model="loginData.userName"
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
    import {login} from '../../api/base'
    import {ref,reactive} from 'vue'
    const loading = ref(false)
    const loginFrom = ref(null)
    const loginData=reactive({
        userName:'',
        password:'',
    })
    const rules = reactive({
      userName:{
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

    async function loginClick()
    {
      if (!loginFrom.value) return
        try{
          await loginFrom.value.validate()
          const res = await login({
              username: loginData.userName,
              password: loginData.password,
            });
            // console.log(res)
          if (res.data.status === 0) {
              uni.setStorage({
                key: 'token',
                data: res?.data?.data[0]?.token,
                success: async () => {
					//const res1 = await getAuthority()
					uni.setStorageSync('expiration', res?.data?.data[0]?.expiration)
                  uni.navigateTo({
                    url: '/pages/index/index',
                  });
                },
              });
              uni.setStorage({
                key: 'userName',
                data: loginData.userName,
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