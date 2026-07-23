import request from '@/utils/request'

/**
 * 耗品字典分页查询
 * @param {查询条件} data
 */
export function dictConsumableBase(query) {
	return request({
		url: 'consumable/consumableBase/dict',
		method: 'get',
		data: query
	})
}

/**
 * 耗品类别字典分页查询
 * @param {查询条件} data
 */
export function dictConsumableCategory(query) {
	return request({
		url: 'consumable/consumableBase/dict/category',
		method: 'get',
		data: query
	})
}