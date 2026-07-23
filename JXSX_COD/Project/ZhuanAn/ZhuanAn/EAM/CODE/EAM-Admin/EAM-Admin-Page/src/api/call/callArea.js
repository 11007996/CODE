import request from '@/utils/request'

/**
 * 广播区域分页查询
 * @param {查询条件} data
 */
export function listCallArea(query) {
  return request({
    url: 'call/CallArea/list',
    method: 'get',
    params: query
  })
}

/**
 * 新增广播区域
 * @param data
 */
export function addCallArea(data) {
  return request({
    url: 'call/CallArea',
    method: 'post',
    data: data
  })
}
/**
 * 修改广播区域
 * @param data
 */
export function updateCallArea(data) {
  return request({
    url: 'call/CallArea',
    method: 'PUT',
    data: data
  })
}
/**
 * 获取广播区域详情
 * @param {Id}
 */
export function getCallArea(id) {
  return request({
    url: 'call/CallArea/' + id,
    method: 'get'
  })
}

/**
 * 删除广播区域
 * @param {主键} pid
 */
export function delCallArea(pid) {
  return request({
    url: 'call/CallArea/delete/' + pid,
    method: 'delete'
  })
}

/**
 * 区域字典信息
 */
export function dictCallArea(query) {
  return request({
    url: 'call/CallArea/dict',
    method: 'get',
    params: query
  })
}
