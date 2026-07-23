<template>
	<view class="container">
		<el-form :model="queryParams" label-width="auto" ref="queryRef">
			<el-form-item label="耗品" prop="consumableId">
				<el-select v-model="queryParams.consumableId" placeholder="请购料号,耗品名称,规格" clearable filterable remote
					:remote-method="handleQueryConsumable">
					<template #header>
						<span>请购料号 / 耗品名称 / 规格</span>
					</template>
					<el-option v-for="item in options.consumable_options" :key="item.dictValue" :label="item.dictLabel"
						:value="item.dictValue"></el-option>
				</el-select>
			</el-form-item>
			<el-form-item label="储位" prop="storageId">
				<el-cascader v-model="queryParams.storageId" clearable placeholder="请选择储位" class="my-cascader-full"
					popper-class="my-cascader-popper" :options="useBasicStore().getConsumableStorageTree"
					:props="{ checkStrictly: true, value: 'storageId', label: 'storageName', emitPath: false }">
					<template #default="{ node, data }">
						<span>{{ data.storageName }}</span>
					</template>
				</el-cascader>
			</el-form-item>
			<el-form-item>
				<div class="form-btn-item">
					<el-button type="primary" @click="handleQuery">搜索</el-button>
					<el-button @click="resetQuery">重置</el-button>
				</div>
			</el-form-item>
		</el-form>

		<el-table :data="dataList" v-loading="loading" ref="table" border header-cell-class-name="el-table-header-cell"
			highlight-current-row>
			<el-table-column label="耗品(请购料号/名称/规格)" min-width="200" :formatter="formatter" />
			<el-table-column prop="storageChangeType" label="变动类型" width="90" align="center">
				<template #default="scope">
					<dict-tag :options="options.storage_change_type" :value="scope.row.storageChangeType" />
				</template>
			</el-table-column>
			<el-table-column prop="changeQty" label="变更数量" width="90" align="center" />
			<el-table-column prop="createTime" label="创建时间" width="120" />
		</el-table>
		<el-pagination layout="prev, pager, next,total" :total="total" v-model:current-page="queryParams.pageNum"
			v-model:page-size="queryParams.pageSize" :pager-count="5" @change="getList" />
	</view>
</template>

<script setup>
	import { ref, reactive, getCurrentInstance, toRefs } from 'vue'
	import useBasicStore from '@/store/modules/basic.js'

	import {
		listConsumableStorageRecord,
	} from '@/api/consumable/consumableStorage.js'
	import { dictConsumableBase } from '@/api/consumable/consumableBase.js'

	const { proxy } = getCurrentInstance()
	let dictParams = [{ dictType: 'storage_change_type' }]
	proxy.getDicts(dictParams).then((response) => {
		response.data.forEach((element) => {
			state.options[element.dictType] = element.list
		})
	})

	const queryParams = reactive({
		pageNum: 1,
		pageSize: 10,
		sort: 'createtime',
		sortType: 'desc',
		storageId: null,
		consumableId: null,
	})

	const table = ref()
	const loading = ref(false)
	const total = ref(0)
	const dataList = ref([])

	//获取耗品列表信息
	function getList() {
		loading.value = true
		listConsumableStorageRecord(queryParams).then((res) => {
			const { code, data } = res
			if (code == 200) {
				dataList.value = data.result
				total.value = data.totalNum
				loading.value = false
			}
		})
	}

	//格式化单元格
	function formatter(row, cloumn) {
		return row.consumablePart + ' / ' + row.consumableName + ' / ' + row.spec
	}

	// ---------------------表单操作-----------------------
	const state = reactive({
		options: {
			// 变动类型 选项列表 格式 eg:{ dictLabel: '标签', dictValue: '0'}
			storage_change_type: [],
			// 耗品选择项
			consumable_options: [],
		}
	})

	const { options } = toRefs(state)


	//耗品查询
	function handleQueryConsumable(keyword, row) {
		if (keyword) {
			const query = {
				pageNum: 1,
				pageSize: 10,
				sort: '',
				sortType: 'asc',
				keyword: keyword
			}

			if (keyword.indexOf(',') >= 0) {
				const kv = keyword.split(',')
				query.consumablePart = kv[0]
				query.consumableName = kv[1]
				query.spec = kv[2]
				query.keyword = null
			}
			setTimeout(() => {
				dictConsumableBase(query).then((res) => {
					if (row) row.consumable_options = res.data.result
					else options.value.consumable_options = res.data.result
				})
			}, 200)
		}
	}


	// 查询
	function handleQuery() {
		queryParams.pageNum = 1
		getList()
	}

	// 重置查询操作
	function resetQuery() {
		proxy.$refs['queryRef'].resetFields()
		handleQuery()
	}

	getList()
</script>

<style scoped>
	.container {
		margin: 0rpx;
		width: auto;
		height: calc(100vh - 70px);
		padding: 15rpx;
		display: flex;
		flex-direction: column;
	}

	/* 使用 :deep() 穿透样式，强制修改宽度 */
	:deep(.my-cascader-full) {
		width: 100%;
	}

	/* 使用 :deep() 穿透样式 强制修改下拉菜单的宽度 */
	:deep(.my-cascader-popper) {
		max-width: 100%;
		overflow: auto;
	}

	.form-btn-item {
		width: 100%;
		display: flex;
		justify-content: flex-end;
	}
</style>