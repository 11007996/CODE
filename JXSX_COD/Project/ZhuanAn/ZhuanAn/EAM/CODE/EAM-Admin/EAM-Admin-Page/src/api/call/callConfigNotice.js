import request from '@/utils/request'

/**
* 消息接收配置表分页查询
* @param {查询条件} data
*/
export function listCallConfigNotice(query) {
  return request({
    url: 'call/CallConfigNotice/list',
    method: 'get',
    params: query,
  })
}

/**
* 新增消息接收配置表
* @param data
*/
export function addCallConfigNotice(data) {
  return request({
    url: 'call/CallConfigNotice',
    method: 'post',
    data: data,
  })
}
/**
* 修改消息接收配置表
* @param data
*/
export function updateCallConfigNotice(data) {
  return request({
    url: 'call/CallConfigNotice',
    method: 'PUT',
    data: data,
  })
}
/**
* 获取消息接收配置表详情
* @param {Id}
*/
export function getCallConfigNotice(id) {
  return request({
    url: 'call/CallConfigNotice/' + id,
    method: 'get'
  })
}

/**
* 删除消息接收配置表
* @param {主键} pid
*/
export function delCallConfigNotice(pid) {
  return request({
    url: 'call/CallConfigNotice/delete/' + pid,
    method: 'delete'
  })
}
