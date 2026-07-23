import request from '@/utils/request.js'

/**
 * 耗品储位信息tree查询
 * @param {查询条件} data
 */
export function treeConsumableStorageSpace(query) {
	return request({
		url: 'consumable/storageSpace/treelist',
		method: 'get',
		data: query
	})
}