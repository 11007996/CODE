<template>
	<view class="container">
		<text class="item title">EAM用户注册</text>
		<view class="item">
			<image class="logo" src="@/static/logo.png"></image>
		</view>

		<!-- 表单 -->
		<el-form ref="registerRef" :model="form" :rules="rules" label-width="auto">
			<el-form-item label="OA用户" prop="username">
				<el-input v-model="form.username" type="text" auto-complete="off" />
			</el-form-item>

			<el-form-item label="OA密码" prop="password">
				<el-input v-model="form.password" type="password" auto-complete="off" />
			</el-form-item>

			<el-form-item label="确认密码" prop="confirmPassword">
				<el-input v-model="form.confirmPassword" type="password" auto-complete="off" />
			</el-form-item>

			<el-form-item style="width: 100%">
				<el-button :loading="loading" size="default" type="primary" style="width: 100%"
					@click.prevent="handleRegister">
					<span v-if="!loading">注册</span>
					<span v-else>注 册 中...</span>
				</el-button>
			</el-form-item>
		</el-form>

	</view>
</template>

<script setup>
	import { ref, reactive, onMounted, getCurrentInstance } from 'vue'
	import useUserStore from '@/store/modules/user.js'
	import { getRSAPublicKey } from '@/api/system/user'
	import { encrypt, decrypt } from '@/utils/jsencrypt'

	// ---------数据-------------
	const { proxy } = getCurrentInstance()
	const loading = ref(false)
	const serverRsaPublicKey = ref('')

	//表单数据
	const form = ref({
		username: '',
		password: '',
		wxCode: ''
	})

	//表单规则
	const rules = {
		username: [{ required: true, trigger: 'blur', message: '请输入您的账号' }],
		password: [{ required: true, trigger: 'blur', message: '请输入您的密码' }],
		confirmPassword: [
			{ required: true, trigger: 'blur', message: '请再次输入您的密码' },
			{ required: true, validator: equalToPassword, trigger: 'blur' }
		]
	}

	//-------------方法-----------------------
	//注册
	function handleRegister() {
		proxy.$refs.registerRef.validate((valid) => {
			if (valid) {
				const param = form.value
				param.serverRsaPublicKey = serverRsaPublicKey.value
				useUserStore().oaRegister(param).then(res => {
					proxy.$modal.msgSuccess('注册成功')
					//注册成功
					uni.setStorageSync('username', param.username)
					uni.setStorageSync('password', encrypt(param.password))
					uni.setStorageSync('useOaAccount', true)
					uni.navigateBack()
				})
			}
		})
	}

	//初始化RSA公钥
	function initRSAPublicKey() {
		getRSAPublicKey().then((res) => {
			serverRsaPublicKey.value = res.data
		})
	}

	//比较两个密码
	function equalToPassword(rule, value, callback) {
		if (form.value.password !== value) {
			callback(new Error('两次输入的密码不一致'))
		} else {
			callback()
		}
	}

	//挂载
	onMounted(() => {
		form.value.wxCode = sessionStorage.getItem('wxCode');
	})

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