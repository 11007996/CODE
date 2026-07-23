<!-- 资产信息页面 -->
<template>
	<view class="container">
		<!-- 资产信息 -->
		<view class="info">
			<view class="item">
				<view class="title">资产编号</view>
				<view class="value">{{info.assetNo}}</view>
			</view>
			<view class="item">
				<view class="title">资产名称</view>
				<view class="value">{{info.assetName}}</view>
			</view>
			<view class="item">
				<view class="title">分类</view>
				<view class="value">{{info.assetClass}}</view>
			</view>
			<view class="item">
				<view class="title">型号规格</view>
				<view class="value">{{info.model}}</view>
			</view>
			<view class="item">
				<view class="title">购置日期</view>
				<view class="value">{{info.entryDate.substring(0,10)}}</view>
			</view>
			<view class="item">
				<view class="title">成本中心</view>
				<view class="value">{{info.costCenter}}</view>
			</view>
			<view class="item">
				<view class="title">耐用年限</view>
				<view class="value">{{info.durableYear}}</view>
			</view>
			<view class="item">
				<view class="title">耐用月数</view>
				<view class="value">{{info.durableMonth}}</view>
			</view>
			<view class="item">
				<view class="title">制造厂商</view>
				<view class="value">{{info.madeFactory}}</view>
			</view>
		</view>

		<!-- 操作 -->
		<view class="operate-container">
			<!--  当前月份 -->
			<view class="operate-Item">
				<view class="title">月份</view>
				<view class="item-right">{{year+'-'+month}}</view>
			</view>

			<!-- 操作 -->
			<view class="operate-Item" v-for="(item,index) in operateItem">
				<view class="title">{{item.timeMarkName}}保养</view>
				<picker :data-item-index="index" :disabled="userRight!='A'" mode="selector" :value="item.index"
					:range="item.timeMarkValues" @change="bindPickerChange">
					<view>
						第 {{item.timeMarkValues[item.index]}} {{item.timeMarkName}}
					</view>
				</picker>
				<view class="item-right" @click="maintenanceOperate(index)">
					<image src="@/static/right.png"></image>
				</view>
			</view>

			<!-- 电子档 -->
			<view class="btn-container">
				<view class="btn-item" v-for="(fileItem,index) in info.fileInfo">
					<view @click="openPreview(index)">{{fileClassText[fileItem.fileClass]}}</view>
				</view>
			</view>
		</view>
	</view>
</template>

<script>
	import {
		assetInfoApi
	} from '@/common/api.js';
	import dateUtil from '@/common/dateUtil.js';
	import wwUtil from '@/common/wwUtil.js';
	export default {
		data() {
			let now = new Date();
			let year = dateUtil.format(now, 'yyyy');
			let month = dateUtil.format(now, 'M');
			//日选择器处理
			let maxDays = dateUtil.getMaxDaysInMonth(year, month);
			let days = [];
			for (let i = 0; i < maxDays; i++) {
				days.push(i + 1);
			}
			let dayIndex = now.getDate() - 1;
			//周选择器处理
			let weeks = [];
			let firstWeekNum = dateUtil.getWeekNumber(year + '/' + month + '/' + 1);
			let currWeekNum = dateUtil.getWeekNumber(year + '/' + month + '/' + now.getDate());
			let maxWeekNum = dateUtil.getWeekNumber(year + '/' + month + '/' + maxDays);
			let weekIndex = currWeekNum - firstWeekNum;
			for (let i = 0; i <= maxWeekNum - firstWeekNum; i++) {
				weeks.push(i + 1);
			}
			return {
				info: {
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
				},
				fileClassText: ['', '操作手册', '保养周期表', '作业标准书'],
				year,
				month,
				operateItem: [{
					'timeMark': 'D',
					'timeMarkName': '日',
					'timeMarkValues': days,
					'index': dayIndex,
				}, {
					'timeMark': 'W',
					'timeMarkName': '周',
					'timeMarkValues': weeks,
					'index': weekIndex,
				}, {
					'timeMark': 'M',
					'timeMarkName': '月',
					'timeMarkValues': [month],
					'index': 0,
				}],
				userRight: ''
			}
		},
		onLoad(options) {
			this.info.assetNo = options.assetNo;
			this.getAssetInfo();
			let userInfo = uni.getStorageSync('userInfo');
			if (userInfo) {
				this.userRight = userInfo.userRight;
			}
		},
		methods: {
			//获取资产详情
			getAssetInfo: function() {
				assetInfoApi({
					'assetNo': this.info.assetNo
				}).then(res => {
					this.info = res;
				});
			},
			//picker修改事件
			bindPickerChange: function(e) {
				let itemIndex = e.currentTarget.dataset.itemIndex;
				this.operateItem[itemIndex].index = e.detail.value;
			},
			//打开保养操作页面
			maintenanceOperate: function(index) {
				let timeMark = this.operateItem[index].timeMark;
				let valIndex = this.operateItem[index].index;
				let timeMarkValue = this.operateItem[index].timeMarkValues[valIndex];
				let param = "assetNo=" + this.info.assetNo + "&year=" + this.year + "&month=" + this.month +
					"&timeMark=" + timeMark + "&timeMarkValue=" + timeMarkValue
				uni.navigateTo({
					url: "/pages/asset/maintenance?" + param,
				});
			},
			//预览电子档图片
			openPreview: function(index) {
				let fileInfo = this.info.fileInfo[index];
				let param = `fileId=${fileInfo.fileId}&fileAliasName=${fileInfo.fileAliasName}`
				uni.navigateTo({
					url: "/pages/asset/showImg?" + param
				});
			}
		}
	}
</script>

<style scoped>
	.container {
		padding: 10rpx;
	}

	.item {
		display: flex;
		justify-content: space-between;
		background-color: aliceblue;
		border-radius: 8rpx;
		overflow: hidden;
		margin: 5rpx 0rpx;
		height: 70rpx;
		align-items: center;
	}

	.item .title {
		display: inline-grid;
		width: 20%;
		height: inherit;
		color: #007aff;
		background-color: #d5d5d6;
		padding: 0 10rpx;
		text-align-last: justify;
		align-items: center;
	}

	.item .value {
		flex: 1;
		padding: 0 10rpx;
		color: gray;
	}

	/* 操作相关 */
	.operate-container {
		margin-top: 20rpx;
		display: flex;
		flex-direction: column;
	}

	.operate-Item {
		display: flex;
		justify-content: space-between;
		background-color: lavender;
		height: 80rpx;
		border-radius: 10rpx;
		overflow: hidden;
		border: 1px solid blue;
		margin-bottom: 15rpx;
	}


	.operate-Item .title {
		width: 18%;
		height: 100%;
		background-color: #007aff;
		color: white;
		display: flex;
		flex-direction: column;
		justify-content: center;
		text-align-last: justify;
		padding: 0rpx 20rpx;
	}

	.operate-Item picker {
		padding: 0 40rpx;
		height: 100%;
		flex: 1;
		background-color: white;
		display: flex;
		flex-direction: column;
		justify-content: center;
		text-align: center;
	}

	.item-right {
		height: 100%;
		display: flex;
		flex-direction: column;
		align-items: center;
		justify-content: center;
		padding-right: 15rpx;
	}

	.operate-Item image {
		width: 50rpx;
		height: 50rpx;
	}
	
	/******文件按钮****/
	.btn-container{
		display: flex;
		justify-content: center;
	}
	
	.btn-container .btn-item{
		margin: 5rpx;
		padding: 15rpx 25rpx;
		font-size:30rpx;
		color:white;
		background-color: darkgoldenrod;
		border-radius: 10rpx;
		border:1rpx blue solid;
	}
</style>