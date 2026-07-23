<template>
	<view class="container">
		<text class="item title">设备系统</text>
		<view class="item">
			<image class="logo" src="@/static/logo.png"></image>
		</view>

		<!-- 表单 -->
		<el-form ref="loginRef" label-width="auto" :model="loginForm" :rules="loginRules">
			<el-form-item prop="username" label="用户">
				<el-input v-model="loginForm.username" type="text" auto-complete="off" />
			</el-form-item>

			<el-form-item prop="password" label="密码">
				<el-input v-model="loginForm.password" show-password type="password" auto-complete="off" />
			</el-form-item>

			<el-form-item style="width: 100%">
				<el-button :loading="loading" size="default" type="primary" style="width: 100%"
					@click.prevent="handleLogin">
					<span v-if="!loading">登入</span>
					<span v-else>登 录 中...</span>
				</el-button>
			</el-form-item>

			<el-form-item style="width: 100%;">
				<el-col :span="12">
					<el-checkbox label=" 使用OA密码" v-model="loginForm.useOaAccount" />
				</el-col>
				<el-col :span="12" style="text-align:end;">
					<el-link type="primary" href="" @click="navToRegister">立即注册</el-link>
				</el-col>
			</el-form-item>
		</el-form>

	</view>
</template>

<script setup>
	import { ref, reactive, onMounted, getCurrentInstance } from 'vue'
	import useUserStore from '@/store/modules/user.js'
	import { encrypt, decrypt } from '@/utils/jsencrypt'
	import { getRSAPublicKey } from '@/api/system/user'
	import { getToken } from '@/utils/auth.js'

	// ---------数据-------------
	const { proxy } = getCurrentInstance()
	const loading = ref(false)
	const serverRsaPublicKey = ref('')

	const loginForm = ref({
		username: '',
		password: '',
		rememberMe: true,
		useOaAccount: true,
		factoryId: ''
	})
	const loginRules = {
		username: [{ required: true, trigger: 'blur', message: '请输入您的账号' }],
		password: [{ required: true, trigger: 'blur', message: '请输入您的密码' }]
	}

	//-------------方法-----------------------
	//登入
	function handleLogin() {
		proxy.$refs.loginRef.validate((valid) => {
			if (valid) {
				const param = loginForm.value
				uni.setStorageSync('username', param.username)
				uni.setStorageSync('password', encrypt(param.password))
				uni.setStorageSync('useOaAccount', param.useOaAccount)

				param.serverRsaPublicKey = serverRsaPublicKey.value
				useUserStore().login(param).then(res => {
					//登入成功
					useUserStore().getInfo().then((res) => {
						let pages = getCurrentPages();
						//只有登入页，跳转到首页
						if (pages.length <= 1)
							uni.switchTab({
								url: '/pages/index'
							})
						else {
							let delta = 0 //返回的页数
							for (let i = pages.length - 1; i >= 0; i--) {
								if (pages[i].route == 'pages/user/login')
									delta++
								else {
									uni.navigateBack({
										delta: delta
									})
									break;
								}

							}
						}
					})
				})
			}
		})
	}

	//跳转到注册
	function navToRegister() {
		uni.navigateTo({
			url: '/pages/user/oaRegister'
		})
	}


	//初始化RSA公钥
	function initRSAPublicKey() {
		getRSAPublicKey().then((res) => {
			serverRsaPublicKey.value = res.data
		})
	}

	//挂载
	onMounted(() => {
		loginForm.value.factoryId = uni.getStorageSync('factoryId')
		loginForm.value.username = uni.getStorageSync('username')
		loginForm.value.password = decrypt(uni.getStorageSync('password'))
		loginForm.value.useOaAccount = uni.getStorageSync('useOaAccount') ? true : false
	})

	function checkIsLogin() {
		let token = getToken()
		if (token) {
			uni.switchTab({
				url: '/pages/index'
			})
		}
	}
	checkIsLogin()
	initRSAPublicKey()
</script>

<style scoped>
	page {
		background-color: aliceblue;
	}

	.container {
		padding: 10rpx;
		display: flex;
		flex-direction: column;
		justify-items: center;
	}

	.title {
		padding-top: 20%;
		font-size: 60rpx;
		font-weight: bold;
	}

	.logo {
		width: 150rpx;
		height: 150rpx;
		margin-bottom: 120rpx;
		margin-top: 40rpx;
		display: block;
	}

	.item {
		width: 95%;
		display: flex;
		justify-content: center;
		align-self: center;
	}
</style>