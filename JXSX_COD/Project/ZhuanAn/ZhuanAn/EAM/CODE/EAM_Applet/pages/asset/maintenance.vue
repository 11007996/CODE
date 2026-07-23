<!-- 保养操作页面 -->
<template>
	<view class="container">

		<!-- 表单 -->
		<el-form ref="queryRef" label-width="auto" :model="queryParams" :rules="rules">
			<el-form-item prop="assetNo" label="资产编号">
				{{queryParams.assetNo}}
			</el-form-item>
			<el-form-item prop="assetName" label="资产名称">
				{{form.assetName}}
			</el-form-item>

			<el-form-item prop="dateMark" label="保养类型">
				<el-select v-model="queryParams.dateMark" placeholder="选项保养类型">
					<el-option v-for="item in options.date_mark" :key="item.dictValue"
						:label="'[' + item.dictValue + '] ' + item.dictLabel" :value="item.dictValue" />
				</el-select>
			</el-form-item>

			<el-form-item prop="timeMark" label="时间类型" v-if="queryParams.dateMark=='D'">
				<el-select v-model="queryParams.timeMark" clearable placeholder="选择班次类型(为空表示整天)">
					<el-option v-for="item in options.time_mark" :key="item.dictValue"
						:label="'[' + item.dictValue + '] ' + item.dictLabel" :value="item.dictValue" />
				</el-select>
			</el-form-item>

			<el-form-item prop="maintainDate" label="保养日期">
				<el-date-picker type="date" format="YYYY/MM/DD" value-format="YYYY-MM-DD"
					v-model="queryParams.maintainDate"></el-date-picker>【{{form.dateMarkStamp}}】
			</el-form-item>
		</el-form>

		<view class="item" v-for="(item,index) in form.items" :key="item.itemId">
			<view class="title">{{item.itemName}}</view>
			<view class="item-value-cell">
				<el-switch v-if="item.type != 'input'" v-model="item.itemValue" class="ml-2" inline-prompt
					style="--el-switch-on-color: #13ce66; --el-switch-off-color: #ff4949" active-value="V"
					inactive-value="X" active-text="V" inactive-text="X" />
				<el-input v-else v-model="item.itemValue"></el-input>
				<el-link @click="handleTypeSwitch(item)">{{ (item.type == 'input' ? '勾选' : '输入') }}</el-link>
			</view>
		</view>

		<view class="item">
			<view class="title">保养人签名</view>
			<view>{{form.executorName}}</view>
		</view>

		<el-button type="success" size="large" class="submitBtn" @click="submit">保存</el-button>
	</view>
</template>

<script setup>
	import { ref, reactive, toRefs, onMounted, getCurrentInstance, watch } from 'vue'
	import { getMaintainDetail, addMaintainRecord, updateMaintainRecord } from '@/api/equipment/equipment.js';
	import dateUtil from '@/utils/dateUtil.js';

	const { proxy } = getCurrentInstance()

	let dictParams = [{ dictType: 'date_mark' }, { dictType: 'time_mark' }]
	proxy.getDicts(dictParams).then((response) => {
		response.data.forEach((element) => {
			state.options[element.dictType] = element.list
		})
	})


	const state = reactive({
		queryParams: {
			assetNo: '',
			dateMark: 'D',
			maintainDate: dateUtil.format(new Date(), 'yyyy-MM-dd')
		},
		form: {
			items: []
		},
		rules: {
			assetNo: [{ required: true, message: '设备资产编号不能为空', trigger: 'blur' }],
			dateMark: [{ required: true, message: '保养类型不能为空', trigger: 'blur' }],
			maintainDate: [{ required: true, message: '保养日期不能为空', trigger: 'blur' }],
		},
		options: {
			// 日期标记 选项列表
			date_mark: [],
			//时间标记
			time_mark: []
		}
	})
	const { queryParams, form, rules, options } = toRefs(state)


	//获取资产的保养项目
	function queryMaintainRecord() {
		proxy.$refs['queryRef'].validate((valid) => {
			if (valid) {
				const param = {
					...queryParams.value
				}
				getMaintainDetail(param).then(res => {
					form.value = res.data
					res.data.maintainRecordDetailNav.forEach(item => {
						item.type = item.itemValue == 'V' || item.itemValue == 'X' || item
							.itemValue == null ? 'switch' : 'input'
					})
					form.value.items = res.data.maintainRecordDetailNav;
				});
			}
		})
	}


	//保存保养记录
	function submit() {

		if (form.value.id > 0) {
			//修改
			updateMaintainRecord(form.value).then(res => {
				if (res.code == 200) {
					proxy.$modal.msgSuccess('保养成功')
					queryMaintainRecord()
				}
			})
		} else { //新增
			//新增
			addMaintainRecord(form.value).then(res => {
				if (res.code == 200) {
					proxy.$modal.msgSuccess('保养成功')
					queryMaintainRecord()
				}
			})
		}
	}

	//切换类型
	function handleTypeSwitch(item) {
		if (item.type === undefined) {
			if (item.itemValue === 'V' || item.itemValue === 'X' || !item.itemValue) {
				item.type = 'switch'
			} else {
				item.type = 'input'
			}
		}
		if (item.type == 'input') {
			item.type = 'switch'
		} else {
			item.type = 'input'
		}
	}

	//组件挂载
	onMounted(() => {
		if (proxy.$attrs.assetNo) {
			queryParams.value.assetNo = proxy.$attrs.assetNo
		}
	})

	watch(queryParams.value, (val, preVal) => {
		queryMaintainRecord()
	})
</script>

<style scoped>
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
		width: 96vw;
		margin-top: 30rpx;
	}

	.item-value-cell {
		width: 50%;
		display: flex;
		justify-content: flex-end;

	}

	.item-value-cell .el-input {
		flex: 1;
	}
</style>