import request from '@/utils/request.js'

/**
 * 产线信息分页查询
 * @param {查询条件} data
 */
export function listLine(query) {
	return request({
		url: 'basic/Line/list',
		method: 'get',
		params: query,
	})
}

/**
 * 新增产线信息
 * @param data
 */
export function addLine(data) {
	return request({
		url: 'basic/Line',
		method: 'post',
		data: data,
	})
}
/**
 * 修改产线信息
 * @param data
 */
export function updateLine(data) {
	return request({
		url: 'basic/Line',
		method: 'PUT',
		data: data,
	})
}
/**
 * 获取产线信息详情
 * @param {Id}
 */
export function getLine(id) {
	return request({
		url: 'basic/Line/' + id,
		method: 'get'
	})
}

/**
 * 删除产线信息
 * @param {主键} pid
 */
export function delLine(pid) {
	return request({
		url: 'basic/Line/delete/' + pid,
		method: 'delete'
	})
}

/**
 * 产线字典信息
 */
export function dictLine(query) {
	return request({
		url: 'basic/Line/dict',
		method: 'get',
		data: query
	})
}