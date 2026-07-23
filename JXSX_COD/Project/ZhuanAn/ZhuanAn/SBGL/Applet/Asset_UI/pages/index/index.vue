<!-- 资产搜索页面 -->
<template>
	<view class="container">
		<!-- 搜索功能 -->
		<view class="search-container">
			<view class="search">
				<input type="text" placeholder="请输入资产名称或编号" v-model="keywords" />
				<image src="@/static/search.png" @click="searchAssetList"></image>
			</view>
			<!-- <image src="@/static/scan.png" @click="scanQRCode"></image> -->
		</view>
		<text>搜索结果：{{assetList.length}}条记录</text>
		<!-- 资产列表 -->
		<view class="asset-container">
			<scroll-view>
				<view class="asset-item" v-for="item in assetList" @click="getAssetDetail(item.assetNo)">
					<view class="item-row">
						<text class="title">编号:</text><text class="value">{{item.assetNo}}</text>
					</view>
					<view class="item-row">
						<text class="title">名称:</text><text class="value">{{item.assetName}}</text>
					</view>
				</view>
			</scroll-view>
		</view>
	</view>
</template>

<script>
	import {
		assetListApi
	} from '@/common/api.js'
	import wwUtil from '@/common/wwUtil.js';
	export default {
		data() {
			return {
				keywords: '',
				assetList: [],
			}
		},
		onLoad(options) {},
		methods: {
			//搜索资产信息
			searchAssetList: function() {
				assetListApi({
						"keywords": this.keywords
					})
					.then(res => {
						this.assetList = res;
					});
			},
			//查看资产详情
			getAssetDetail: function(assetNo) {
				uni.navigateTo({
					url: "/pages/asset/detail?assetNo=" + assetNo
				});
			},
			//打开摄像头
			scanQRCode: function() {

				// #ifdef H5
				uni.navigateTo({
					url: '/pages/qrcode/qrcode'
				});
				// #endif

				//#ifndef H5
				let that = this;
				uni.scanCode({
					scanType: 'qrCode',
					success: function(res) {
						that.getAssetDetail(res.result);
					}
				});
				// #endif
			},

		}
	}
</script>

<style scoped>
	.container {
		padding: 10rpx;
	}

	/* 搜索相关 */
	.search-container {
		display: flex;
		flex-direction: row;
		align-items: center;
		justify-content: center;
	}

	.search {
		display: flex;
		justify-content: row;
		flex: 1;
		height: 60rpx;
		border: 1px solid #007aff;
		border-radius: 8rpx;
	}

	.search input {
		flex: 1;
		height: initial;
		font-size: 36rpx;
		padding-left: 10rpx;
	}

	.search-container image,
	.search image {
		height: initial;
		width: 60rpx;
		margin-top: 0rpx;
		margin-left: 10rpx;
		margin-right: auto;
		margin-bottom: 0rpx;
	}

	/* 资产列表相关 */
	.asset-container {
		margin-top: 20rpx;
	}

	.asset-item {
		background-color: palegoldenrod;
		border-radius: 8rpx;
		padding: 10rpx;
		margin-bottom: 10rpx;

	}

	.asset-item .item-row {
		display: flex;
		justify-content: row;
	}

	.asset-item .title {
		display: block;
		color: green;
		width: 100rpx;
	}

	.asset-item .value {
		color: gray;
	}
</style>