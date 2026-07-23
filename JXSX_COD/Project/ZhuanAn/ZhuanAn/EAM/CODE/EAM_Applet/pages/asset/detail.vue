<!-- 设备信息页面 -->
<template>
	<view class="container">
		<!-- 设备信息 -->
		<el-descriptions title="设备信息" :column="1" border>
			<el-descriptions-item label="资产编号" label-align="right" align="left" label-class-name="my-item-title"
				class-name="my-content" label-width="180rpx">
				{{info.assetNo}}
			</el-descriptions-item>
			<el-descriptions-item label="资产名称" label-align="right" align="left" label-class-name="item-title"
				class-name="my-content" label-width="180rpx">
				{{info.assetName}}
			</el-descriptions-item>
			<el-descriptions-item label="分类" label-align="right" align="left" label-class-name="item-title"
				class-name="my-content" label-width="180rpx">
				{{info.assetClass}}
			</el-descriptions-item>
			<el-descriptions-item label="型号规格" label-align="right" align="left" label-class-name="item-title"
				class-name="my-content" label-width="180rpx">
				{{info.model}}
			</el-descriptions-item>
			<el-descriptions-item label="购置日期" label-align="right" align="left" label-class-name="item-title"
				class-name="my-content" label-width="180rpx">
				{{info.entryDate.substring(0,10)}}
			</el-descriptions-item>
			<el-descriptions-item label="成本中心" label-align="right" align="left" label-class-name="item-title"
				class-name="my-content" label-width="180rpx">
				{{info.costCenter}}
			</el-descriptions-item>
			<el-descriptions-item label="耐用年限" label-align="right" align="left" label-class-name="item-title"
				class-name="my-content" label-width="180rpx">
				{{info.durableYear}}
			</el-descriptions-item>
			<el-descriptions-item label="耐用月数" label-align="right" align="left" label-class-name="item-title"
				class-name="my-content" label-width="180rpx">
				{{info.durableMonth}}
			</el-descriptions-item>
			<el-descriptions-item label="制造厂商" label-align="right" align="left" label-class-name="item-title"
				class-name="my-content" label-width="180rpx">
				{{info.madeFactory}}
			</el-descriptions-item>
		</el-descriptions>

		<!-- 操作 -->
		<view class="operate-container">
			<!--  保养 -->
			<view class="operate-Item">
				<el-button type="primary" size="large" class="operate-btn" @click="maintenanceOperate">保养</el-button>
			</view>

			<!-- 电子档 -->
			<!-- <view class="btn-container">
				<view class="btn-item" v-for="(fileItem,index) in info.fileInfo">
					<view @click="openPreview(index)">{{fileClassText[fileItem.fileClass]}}</view>
				</view>
			</view> -->
		</view>
	</view>
</template>

<script setup>
	import { ref, reactive, onMounted, getCurrentInstance } from 'vue'
	import { getEquipment } from '@/api/equipment/equipment.js'
	import { waitForInit } from '@/utils/app-init'

	const { proxy } = getCurrentInstance()
	const info = ref({
		assetNo: '',
		assetName: '',
		assetClass: '',
		model: '',
		entryDate: '',
		costCenter: '',
		durableYear: '',
		durableMonth: '',
		madeFactory: '',
		fileInfo: {}
	})
	const fileClassText = ref(['', '操作手册', '保养周期表', '作业标准书'])

	//获取资产详情
	function queryEquipmentInfo(assetNo) {
		getEquipment(assetNo).then(res => {
			info.value = res.data;
		});
	}

	//打开保养操作页面
	function maintenanceOperate() {
		let param = "assetNo=" + info.value.assetNo
		uni.navigateTo({
			url: "/pages/asset/maintenance?" + param,
		});
	}

	//预览电子档图片
	function openPreview(index) {
		let fileInfo = this.info.fileInfo[index];
		let param = `fileId=${fileInfo.fileId}&fileAliasName=${fileInfo.fileAliasName}`
		uni.navigateTo({
			url: "/pages/asset/showImg?" + param
		});
	}

	//组件挂载
	onMounted(async () => {
		await waitForInit()
		if (proxy.$attrs.assetNo) {
			queryEquipmentInfo(proxy.$attrs.assetNo)
		}
	})
</script>

<style scoped>
	.container {
		padding: 10rpx;
	}

	.my-item-title {
		color: green;
	}

	/*******操作*********/

	.operate-container {
		margin-top: 15rpx;
	}

	.operate-Item .operate-btn {
		width: 100%;
	}

	/******文件按钮****/
	.btn-container {
		display: flex;
		justify-content: center;
	}

	.btn-container .btn-item {
		margin: 5rpx;
		padding: 15rpx 25rpx;
		font-size: 30rpx;
		color: white;
		background-color: darkgoldenrod;
		border-radius: 10rpx;
		border: 1rpx blue solid;
	}
</style>