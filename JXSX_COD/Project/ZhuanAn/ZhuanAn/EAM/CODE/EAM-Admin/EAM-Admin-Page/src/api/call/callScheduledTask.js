import request from '@/utils/request'

/**
* 广播定时任务分页查询
* @param {查询条件} data
*/
export function listCallScheduledTask(query) {
  return request({
    url: 'call/CallScheduledTask/list',
    method: 'get',
    params: query,
  })
}

/**
* 新增广播定时任务
* @param data
*/
export function addCallScheduledTask(data) {
  return request({
    url: 'call/CallScheduledTask',
    method: 'post',
    data: data,
  })
}
/**
* 修改广播定时任务
* @param data
*/
export function updateCallScheduledTask(data) {
  return request({
    url: 'call/CallScheduledTask',
    method: 'PUT',
    data: data,
  })
}
/**
* 获取广播定时任务详情
* @param {Id}
*/
export function getCallScheduledTask(id) {
  return request({
    url: 'call/CallScheduledTask/' + id,
    method: 'get'
  })
}

/**
* 删除广播定时任务
* @param {主键} pid
*/
export function delCallScheduledTask(pid) {
  return request({
    url: 'call/CallScheduledTask/delete/' + pid,
    method: 'delete'
  })
}
