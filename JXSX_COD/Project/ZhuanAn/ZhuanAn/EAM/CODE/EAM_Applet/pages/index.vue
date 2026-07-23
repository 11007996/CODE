<!-- 资产搜索页面 -->
<template>
	<view class="container">
		<view class="menu-item" v-for="item in menus" @click="navToPage(item.url)" :key="item.id">
			<image :src="item.imagePath"></image>
			<text>{{item.name}}</text>
		</view>
	</view>
</template>

<script setup>
	import { reactive } from 'vue';

	const menus = reactive([{
			imagePath: '/static/equipment.png',
			name: '设备',
			url: '/pages/asset/index'
		},
		{
			imagePath: '/static/consumable.png',
			name: '耗品',
			url: '/pages/consumable/index'
		},
		{
			imagePath: '/static/back-console.png',
			name: 'EAM管理',
			url: 'http://172.18.20.84:8887/index'
		}
	])

	//跳转到对应菜单的功能页面
	function navToPage(url) {
		if (url.indexOf('http') >= 0) {
			location.href = url
		} else {
			uni.navigateTo({
				url: url
			});
		}
	}
</script>

<style>
	.container {
		width: auto;
		padding: 20rpx;
		display: flex;
		flex-direction: row;
	}

	.menu-item {
		width: 180rpx;
		height: 180rpx;
		background-color: lightyellow;
		border-radius: 16rpx;
		display: flex;
		flex-direction: column;
		align-items: center;
		justify-content: space-evenly;
		padding: 10rpx;
		margin: 10rpx;
		box-shadow: 15rpx 15rpx 29rpx -13rpx rgba(0, 0, 0, 0.2);
	}

	.menu-item image {
		width: 80rpx;
		height: 80rpx;
	}

	.menu-item text {
		font-size: 32rpx;
		color: gray;
	}
</style>