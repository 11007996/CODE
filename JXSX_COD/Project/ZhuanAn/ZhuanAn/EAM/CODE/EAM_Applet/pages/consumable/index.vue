<template>
	<view class="container">
		<el-form :model="queryParams" label-width="auto" ref="queryRef">
			<el-form-item label="储位" prop="storageId">
				<el-cascader v-model="queryParams.storageId" clearable placeholder="请选择储位" class="my-cascader-full"
					popper-class="my-cascader-popper" :options="useBasicStore().getConsumableStorageTree"
					:props="{ checkStrictly: true, value: 'storageId', label: 'storageName', emitPath: false }">
					<template #default="{ node, data }">
						<span>{{ data.storageName }}</span>
					</template>
				</el-cascader>
			</el-form-item>
			<el-form-item label="耗品名称" prop="consumableName">
				<el-input v-model="queryParams.consumableName" placeholder="请输入耗品名称" />
			</el-form-item>
			<el-form-item label="规格" prop="spec">
				<el-input v-model="queryParams.spec" placeholder="请输入规格" />
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
			<el-table-column label="耗品(请购料号/名称/规格)" min-width="140" :formatter="formatter" />
			<el-table-column prop="qty" label="数量" width="80" align="center" />
			<el-table-column label="操作" width="80" align="center" fixed="right">
				<template #default="scope">
					<div class="flex-col-ali-cen">
						<el-button type="primary" size="small" v-hasPermi="['consumable:storage:in']"
							@click="handleOperate(scope.row, 1)">入库</el-button>
						<el-button type="primary" size="small" v-hasPermi="['consumable:storage:out']"
							@click="handleOperate(scope.row, 2)">出库</el-button>
						<el-button type="primary" size="small" v-hasPermi="['consumable:storage:receive']"
							@click="handleOperate(scope.row, 4)">领用</el-button>
						<el-button type="primary" size="small" v-hasPermi="['consumable:storage:back']"
							@click="handleOperate(scope.row, 5)">归还</el-button>
						<el-button type="primary" size="small" v-hasPermi="['consumable:storage:scrapped']"
							@click="handleOperate(scope.row, 3)">报废</el-button>
						<el-button type="primary" size="small" v-hasPermi="['consumable:storage:transfer']"
							@click="handleOperate(scope.row, 6)">转移</el-button>
					</div>
				</template>
			</el-table-column>
		</el-table>
		<el-pagination layout="prev, pager, next,total" :total="total" v-model:current-page="queryParams.pageNum"
			v-model:page-size="queryParams.pageSize" :pager-count="5" @change="getList" />

		<div>
			<el-button type="primary" size="large" @click="handleShowStorageRecored" class="operate-btn">
				操作记录
			</el-button>
		</div>

		<!-- 添加或修改耗品存储表对话框 -->
		<el-dialog :title="title" :lock-scroll="false" v-model="open" width="98%">
			<el-form ref="formRef" :model="form" :rules="rules" label-width="100px">
				<el-row :gutter="20">
					<el-col :lg="24">
						<el-form-item label="耗品" prop="consumableId">
							<el-select v-model="form.consumableId" readonly placeholder="请购料号,耗品名称,规格" clearable
								filterable remote :remote-method="handleQueryConsumable" class="w100">
								<template #header>
									<span>请购料号 / 耗品名称 / 规格</span>
								</template>
								<el-option v-for="item in options.consumable_options" :key="item.dictValue"
									:label="item.dictLabel" :value="item.dictValue">
								</el-option>
							</el-select>
						</el-form-item>
					</el-col>

					<el-col :lg="24">
						<el-form-item label="储位" prop="storageId">
							<el-cascader class="w100" :options="useBasicStore().getConsumableStorageTree"
								:props="{ checkStrictly: true, value: 'storageId', label: 'storageName', emitPath: false }"
								:disabled="opertype != 1" placeholder="请选择储位" clearable v-model="form.storageId">
								<template #default="{ node, data }">
									<span>{{ data.storageName }}</span>
									<span v-if="!node.isLeaf"> ({{ data.children.length }}) </span>
								</template>
							</el-cascader>
						</el-form-item>
					</el-col>

					<el-col :lg="12">
						<el-form-item :label="title+'数量'" prop="changeQty">
							<el-input-number v-model.number="form.changeQty" :controls="true" controls-position="right"
								placeholder="请输入数量" />
						</el-form-item>
					</el-col>

					<!-- 目标储位（转移专用） -->
					<el-col :lg="24" v-if="opertype == 6">
						<el-form-item label="目标储位" prop="newStorageId">
							<el-cascader class="w100" :options="useBasicStore().getConsumableStorageTree"
								:props="{ checkStrictly: true, value: 'storageId', label: 'storageName', emitPath: false }"
								placeholder="请选择储位" clearable v-model="form.newStorageId">
								<template #default="{ node, data }">
									<span>{{ data.storageName }}</span>
									<span v-if="!node.isLeaf"> ({{ data.children.length }}) </span>
								</template>
							</el-cascader>
						</el-form-item>
					</el-col>

					<el-col :lg="12" v-if="opertype == 4 || opertype == 5">
						<el-form-item :label="opertype == 4 ? '领用人' : '归还人'" prop="relatedUser">
							<el-select v-model="form.relatedUser" placeholder="请选择人员" clearable filterable
								class="fullWidth">
								<el-option v-for="item in options.emp_options" :key="item.dictValue"
									:label="item.dictValue + ' - ' + item.dictLabel"
									:value="item.dictValue"></el-option>
							</el-select>
						</el-form-item>
					</el-col>

					<el-col :lg="12" v-if="opertype == 4">
						<el-form-item label="产线" prop="lineId">
							<el-select v-model="form.lineId" placeholder="请选择产线" clearable filterable class="fullWidth">
								<el-option v-for="item in useBasicStore().getLineDict" :key="item.dictValue"
									:label="item.dictLabel" :value="Number(item.dictValue)"></el-option>
							</el-select>
						</el-form-item>
					</el-col>

					<el-col :lg="24">
						<el-form-item label="备注">
							<el-input type="textarea" v-model="form.remark" :controls="true"
								controls-position="right" />
						</el-form-item>
					</el-col>
				</el-row>
			</el-form>
			<template #footer>
				<el-button text @click="cancel">取消</el-button>
				<el-button type="primary" @click="submitForm">提交</el-button>
			</template>
		</el-dialog>

	</view>
</template>

<script setup>
	import { ref, reactive, onMounted, getCurrentInstance, toRefs } from 'vue'
	import useBasicStore from '@/store/modules/basic.js'
	import useUserStore from '@/store/modules/user.js'
	import { waitForInit } from '@/utils/app-init'
	import {
		listConsumableStorage,
		getConsumableStorage,
		inConsumableStorage,
		outConsumableStorage,
		scrappedConsumableStorage,
		receiveConsumableStorage,
		backConsumableStorage,
		transferConsumableStorage
	} from '@/api/consumable/consumableStorage.js'

	const { proxy } = getCurrentInstance()
	const queryParams = reactive({
		pageNum: 1,
		pageSize: 10,
		sort: '',
		sortType: 'asc',
		storageId: null,
		consumableName: null,
		spec: null
	})

	const table = ref()
	const loading = ref(false)
	const total = ref(0)
	const dataList = ref([])

	//获取耗品列表信息
	function getList() {
		loading.value = true
		listConsumableStorage(queryParams).then((res) => {
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
	const formRef = ref()
	const title = ref('')
	// 操作类型 1.入库 2.出库 3.报废 4.领用 5.归还 6.转移 ,8、导入 9、操作导入
	const opertype = ref(0)
	const open = ref(false)
	const state = reactive({
		single: true,
		multiple: true,
		form: {},
		rules: {
			consumableId: [{ required: true, message: '耗品ID不能为空', trigger: 'blur' }],
			storageId: [{ required: true, message: '储位ID不能为空', trigger: 'blur' }],
			changeQty: [{ required: true, message: '变动数量不能为空', trigger: 'blur', type: 'number' }],
			relatedUser: [{ required: true, message: '领用/归还人不能为空', trigger: 'blur' }],
			newStorageId: [{ required: true, message: '目标储位不能为空', trigger: 'blur' }]
		},
		options: {
			// 变动类型 选项列表 格式 eg:{ dictLabel: '标签', dictValue: '0'}
			storage_change_type: [],
			// 耗品选择项
			consumable_options: [],
			// 员工
			emp_options: [],
			// 类别
			category_options: [],
		}
	})

	const { form, rules, options, single, multiple } = toRefs(state)

	// 关闭dialog
	function cancel() {
		open.value = false
		reset()
	}

	// 重置表单
	function reset() {
		form.value = {
			consumableId: null,
			storageId: null,
			changeQty: 0
		}
		proxy.$refs['formRef'].resetFields()
	}

	const operateTitle = ref(['', '入库', '出库', '报废', '领用', '归还', '转移'])
	// 操作 1.入库 2.出库 3.报废 4.领用 5.归还
	function handleOperate(row, operate) {
		//reset()
		title.value = operateTitle.value[operate]
		opertype.value = operate

		if (row) {
			options.value.consumable_options = [{ dictValue: row.consumableId, dictLabel: formatter(row) }]

			const params = {
				consumableId: row.consumableId,
				storageId: row.storageId
			}

			getConsumableStorage(params).then((res) => {
				const { code, data } = res
				if (code == 200) {
					open.value = true
					form.value = {
						...data
					}
				}
			})
		} else {
			//入库
			form.value.changeQty = 0
			open.value = true
		}
	}

	// 入库&出库&报废&领用 表单提交
	function submitForm() {
		proxy.$refs['formRef'].validate((valid) => {
			if (valid) {
				if (opertype.value === 1) {
					inConsumableStorage(form.value).then((res) => {

						proxy.$modal.msgSuccess('入库成功')
						open.value = false
						getList()
					})
				} else if (opertype.value === 2) {
					outConsumableStorage(form.value).then((res) => {
						proxy.$modal.msgSuccess('出库成功')
						open.value = false
						getList()
					})
				} else if (opertype.value === 3) {
					scrappedConsumableStorage(form.value).then((res) => {
						proxy.$modal.msgSuccess('报废成功')
						open.value = false
						getList()
					})
				} else if (opertype.value === 4) {
					receiveConsumableStorage(form.value).then((res) => {
						proxy.$modal.msgSuccess('领用成功')
						open.value = false
						getList()
					})
				} else if (opertype.value === 5) {
					backConsumableStorage(form.value).then((res) => {
						proxy.$modal.msgSuccess('归还成功')
						open.value = false
						getList()
					})
				} else if (opertype.value === 6) {
					transferConsumableStorage(form.value).then((res) => {
						proxy.$modal.msgSuccess('转移成功')
						open.value = false
						getList()
					})
				}
			}
		})
	}

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


	function initOptions() {
		options.value.emp_options = [{
			dictValue: useUserStore().userInfo.userName,
			dictLabel: useUserStore().userInfo.nickName
		}]
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

	//跳转到耗品变更记录
	function handleShowStorageRecored() {
		uni.navigateTo({
			url: '/pages/consumable/storageRecored'
		})
	}

	//挂载前等待
	onMounted(async () => {
		await waitForInit()
		initOptions()
		if (proxy.$attrs.storageId) {
			queryParams.storageId = parseInt(proxy.$attrs.storageId)
		}
		getList()
	})
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

	.flex-col-ali-cen {
		display: flex;
		flex-direction: column;
		align-items: center;
	}

	.el-table__row .el-button+.el-button {
		margin-left: 0rpx;
		margin-top: 10rpx;
	}

	.operate-btn {
		width: 100%;
		margin-top: 10rpx;
	}
</style>