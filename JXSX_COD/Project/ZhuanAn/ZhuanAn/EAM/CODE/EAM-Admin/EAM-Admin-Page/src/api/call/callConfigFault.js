import request from '@/utils/request'

/**
* 故障配置分页查询
* @param {查询条件} data
*/
export function listCallConfigFault(query) {
  return request({
    url: 'call/CallConfigFault/list',
    method: 'get',
    params: query,
  })
}

/**
* 新增故障配置
* @param data
*/
export function addCallConfigFault(data) {
  return request({
    url: 'call/CallConfigFault',
    method: 'post',
    data: data,
  })
}
/**
* 修改故障配置
* @param data
*/
export function updateCallConfigFault(data) {
  return request({
    url: 'call/CallConfigFault',
    method: 'PUT',
    data: data,
  })
}
/**
* 获取故障配置详情
* @param {Id}
*/
export function getCallConfigFault(id) {
  return request({
    url: 'call/CallConfigFault/' + id,
    method: 'get'
  })
}

/**
* 删除故障配置
* @param {主键} pid
*/
export function delCallConfigFault(pid) {
  return request({
    url: 'call/CallConfigFault/delete/' + pid,
    method: 'delete'
  })
}
