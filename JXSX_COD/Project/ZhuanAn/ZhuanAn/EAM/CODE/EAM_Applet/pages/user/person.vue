<template>
	<view class="container">
		<view class="item">
			<view class="title"> 登入用户 </view>
			<view class="value">{{useUserStore().userInfo.userName}}/{{useUserStore().userInfo.nickName}} </view>
		</view>
		<view class="item" @click="navToSwitchFactory">
			<view class="title">厂区 </view>
			<view class="value active">{{ useUserStore().factoryName+"["+useUserStore().factoryId+"]"}}</view>
		</view>
		<view class="item">
			<view class="title"> 版本 </view>
			<view class="value">{{version}}</view>
		</view>
		<view class="item">
			<view class="title"> Copyright </view>
			<view class="value">{{copyright}}</view>
		</view>
		<view>
			<button @click="handleLogout" class="btn-danger">退出</button>
		</view>
	</view>
</template>

<script setup>
	import { ref } from 'vue'
	import useUserStore from '@/store/modules/user.js'
	import { removeToken } from '@/utils/auth.js'

	const systemInfo = uni.getSystemInfoSync();
	const version = ref(systemInfo.appVersion)
	const copyright = ref('©CPBG_智能资讯处')

	//切换厂区
	function navToSwitchFactory() {
		uni.navigateTo({
			url: '/pages/user/switchFactory'
		})
	}

	//退出登入
	async function handleLogout() {
		await useUserStore().logOut()
		uni.navigateTo({
			url: '/pages/user/login'
		})
	}
</script>

<style scoped>
	page {
		width: 100%;
		height: 100%;
	}

	.container {
		background-color: #f3f3f3;
		height: 100%;
		width: 100%;
	}

	.item {
		display: flex;
		flex-direction: row;
		justify-content: space-between;
		background-color: white;
		height: 80rpx;
		align-items: center;
		margin-bottom: 5rpx;
		padding: 10rpx;
	}

	.item .title {
		margin-left: 20rpx;
		width: 200rpx;
	}

	.item .value {
		margin-right: 20rpx;
		color: gray;
	}

	.item .active {
		color: blue;
	}

	.btn-danger {
		background-color: red;
		color: white;
		border: #f3f3f3 1rpx solid;
		border-radius: 15rpx;
		margin: 20rpx;
	}
</style>