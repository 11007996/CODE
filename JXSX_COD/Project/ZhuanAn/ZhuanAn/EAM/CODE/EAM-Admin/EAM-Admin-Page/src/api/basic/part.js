import request from '@/utils/request'

/**
 * 料号分页查询
 * @param {查询条件} data
 */
export function listPart(query) {
  return request({
    url: 'basic/part/list',
    method: 'get',
    params: query
  })
}

/**
 * 新增料号
 * @param data
 */
export function addPart(data) {
  return request({
    url: 'basic/part',
    method: 'post',
    data: data
  })
}

/**
 * 更新料号
 * @param data
 */
export function updatePart(data) {
  return request({
    url: 'basic/part',
    method: 'put',
    data: data
  })
}

/**
 * 获取料号详情
 * @param {Id}
 */
export function getPart(id) {
  return request({
    url: 'basic/part/' + id,
    method: 'get'
  })
}

/**
 * 删除料号
 * @param {主键} pid
 */
export function delPart(pid) {
  return request({
    url: 'basic/part/delete/' + pid,
    method: 'delete'
  })
}

/**
 * 料号字典信息
 */
export function dictPart(query) {
  return request({
    url: 'basic/part/dict',
    method: 'get',
    params: query
  })
}
