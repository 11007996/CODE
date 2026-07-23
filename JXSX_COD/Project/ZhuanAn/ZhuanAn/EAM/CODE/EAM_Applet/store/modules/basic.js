import { defineStore } from 'pinia'
import { dictLine } from '@/api/basic/line.js'
import { treeConsumableStorageSpace } from '@/api/consumable/consumableStorageSpace.js'

const useBasicStore = defineStore('basic', {
	state: () => ({
		isInitLine: false,
		lineDict: [],
		isInitConsumableStorage: false,
		consumableStorageTree: [],
	}),
	getters: {
		getLineDict: (store) => {
			if (!store.isInitLine) store.initLine()
			return store.lineDict
		},
		getConsumableStorageTree: (store) => {
			if (!store.isInitConsumableStorage) store.initConsumableStorge()
			return store.consumableStorageTree
		}
	},
	actions: {
		//初始产线信息
		async initLine() {
			await this.updateLine()
			this.isInitLine = true
		},
		//更新产线信息
		updateLine() {
			const query = {
				pageNum: 1,
				pageSize: 1000,
				sort: '',
				sortType: 'asc'
			}
			return dictLine(query)
				.then((res) => {
					const { code, data } = res
					if (code == 200) {
						this.lineDict = data.result
					}
				})
				.catch(() => {})
		},
		//初始化耗品储位信息
		async initConsumableStorge() {
			await this.updateConsumableStorage()
			this.isInitConsumableStorage = true
		},
		//更新耗品储位信息
		async updateConsumableStorage() {
			await treeConsumableStorageSpace()
				.then((res) => {
					const { code, data } = res
					if (code == 200) {
						this.consumableStorageTree = data
					}
				})
				.catch(() => {})
		},

	}
})

export default useBasicStore