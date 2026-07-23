<!-- 资产搜索页面 -->
<template>
	<view class="container">

		<!-- 搜索功能 -->
		<view class="search-container">
			<el-input v-model="queryParams.assetNo" class="responsive-input" placeholder="请输入资产编号">
				<template #append>
					<el-button @click="handleQueryEquipment" icon="Search" />
				</template>
			</el-input>
		</view>
		<text>搜索结果：{{assetList.length}}条记录</text>
		<!-- 资产列表 -->
		<view class="asset-container">
			<scroll-view>
				<view class="asset-item" v-for="item in assetList" @click="navToEquipmentDetail(item.assetNo)"
					:key="item.equipmentId">
					<el-descriptions size="small" :column="1" border>
						<el-descriptions-item label="资产编号" label-align="right" align="left" label-width="150rpx">
							{{item.assetNo}}
						</el-descriptions-item>
						<el-descriptions-item label="资产名称" label-align="right" align="left" label-width="150rpx">
							{{item.assetName}}
						</el-descriptions-item>
					</el-descriptions>
				</view>
			</scroll-view>
		</view>
	</view>
</template>

<script setup>
	import { ref, reactive } from 'vue'
	import { listEquipment } from '@/api/equipment/equipment.js'

	const assetList = ref([])
	const queryParams = reactive({
		pageNum: 1,
		pageSize: 10,
		sort: '',
		sortType: 'asc',
		assetNo: undefined
	})

	//搜索设备信息
	function handleQueryEquipment() {
		listEquipment(queryParams)
			.then(res => {
				assetList.value = res.data.result;
			});
	}

	//查看设备详情
	function navToEquipmentDetail(assetNo) {
		uni.navigateTo({
			url: "/pages/asset/detail?assetNo=" + assetNo
		});
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
		border-radius: 4rpx;
		padding: 0rpx 6rpx;
		margin-bottom: 10rpx;

	}
</style>