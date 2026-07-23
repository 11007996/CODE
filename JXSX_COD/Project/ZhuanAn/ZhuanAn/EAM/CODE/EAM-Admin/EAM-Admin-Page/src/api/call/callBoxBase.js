import request from '@/utils/request'

/**
 * 呼叫盒信息分页查询
 * @param {查询条件} data
 */
export function listCallBoxBase(query) {
  return request({
    url: 'call/CallBoxBase/list',
    method: 'get',
    params: query
  })
}

/**
 * 新增呼叫盒信息
 * @param data
 */
export function addCallBoxBase(data) {
  return request({
    url: 'call/CallBoxBase',
    method: 'post',
    data: data
  })
}
/**
 * 修改呼叫盒信息
 * @param data
 */
export function updateCallBoxBase(data) {
  return request({
    url: 'call/CallBoxBase',
    method: 'PUT',
    data: data
  })
}
/**
 * 获取呼叫盒信息详情
 * @param {Id}
 */
export function getCallBoxBase(id) {
  return request({
    url: 'call/CallBoxBase/' + id,
    method: 'get'
  })
}

/**
 * 删除呼叫盒信息
 * @param {主键} pid
 */
export function delCallBoxBase(pid) {
  return request({
    url: 'call/CallBoxBase/delete/' + pid,
    method: 'delete'
  })
}

/**
 * 呼叫盒信息字典查询
 * @param {查询条件} data
 */
export function dictCallBoxBase(query) {
  return request({
    url: 'call/CallBoxBase/dict',
    method: 'get',
    params: query
  })
}
