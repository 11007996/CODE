<!-- 保养操作页面 -->
<template>
	<view class="container">
		<view>当前保养标志：第<text class="mark">{{timeMarkValue}}</text>{{timeMarkName}}
		</view>
		<view class="item" v-for="(item,index) in items">
			<view class="title">{{item.itemName}}</view>
			<!-- 判断是项目还是签名 -->
			<block v-if="item.itemName!='保养人签名'">
				<switch :data-index="index" :checked="item.itemValue=='V'" @change="changeItemValue" />
			</block>
			<block v-else>
				<input class="value" disabled :value="item.itemValue" />
			</block>
		</view>
		<button class="submitBtn" @click="submit"> 保存</button>
	</view>
</template>

<script>
	import {
		maintenanceItemsApi,
		updateMaintenanceApi
	} from '@/common/api.js';
	import wwUtil from '@/common/wwUtil.js';
	export default {
		data() {
			return {
				assetNo: '',
				year: '',
				month: '',
				timeMarkStamp: '',
				timeMarkValue: '',
				timeMarkName: '',
				items: []
			}
		},
		onLoad(options) {
			let timeMarkName = '日'
			switch (options.timeMark) {
				case 'D':
					timeMarkName = '日';
					break;
				case 'W':
					timeMarkName = '周';
					break;
				case 'M':
					timeMarkName = '月';
					break;
			}
			this.assetNo = options.assetNo;
			this.year = options.year;
			this.month = options.month;
			this.timeMark = options.timeMark;
			this.timeMarkValue = options.timeMarkValue;
			this.timeMarkName = timeMarkName;
			this.getMaintenanceItems();
		},
		methods: {
			//获取资产的保养项目
			getMaintenanceItems: function() {
				maintenanceItemsApi({
					assetNo: this.assetNo,
					year: this.year,
					month: this.month,
					timeMark: this.timeMark,
					timeMarkValue: this.timeMarkValue

				}).then(res => {
					this.items = res.items;
					this.timeMarkStamp = res.timeMarkStamp;
				});
			},
			//修改保养项目结果
			changeItemValue: function(e) {
				let index = e.currentTarget.dataset.index;
				let value = e.detail.value ? 'V' : 'X';
				this.items[index].itemValue = value;
			},
			//保存保养记录
			submit: function() {
				updateMaintenanceApi({
					AssetNo: this.assetNo,
					Year: this.year,
					TimeMark: this.timeMark,
					TimeMarkStamp: this.timeMarkStamp,
					ItemValueDic: this.items,
				}).then(res => {
					uni.showToast({
						icon: 'none',
						duration: 2000,
						title: '保存成功',
					});
				}).catch(err => {});
			}
		}
	}
</script>

<style>
	.container {
		padding: 10rpx;
	}

	.mark {
		color: red;
	}

	.item {
		height: 80rpx;
		background-color: aliceblue;
		display: flex;
		justify-content: space-between;
		border-radius: 10rpx;
		margin-bottom: 10rpx;
		justify-items: center;
		align-items: center;
	}

	.item .title {
		margin-left: 20rpx;
	}

	.item .value {
		margin-right: 20rpx;
		text-align: end;
	}

	.submitBtn {
		margin-top: 30rpx;
		background-color: green;
		color: white;
	}

	.submitBtn:active {
		background-color: bisque;
		color: white;
	}
</style>