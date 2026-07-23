import request from '@/utils/request'

/**
* 流程实例任务分页查询
* @param {查询条件} data
*/
export function listInstanceTask(query) {
  return request({
    url: 'workflow/InstanceTask/list',
    method: 'get',
    params: query,
  })
}

/**
* 新增流程实例任务
* @param data
*/
export function addInstanceTask(data) {
  return request({
    url: 'workflow/InstanceTask',
    method: 'post',
    data: data,
  })
}
/**
* 修改流程实例任务
* @param data
*/
export function updateInstanceTask(data) {
  return request({
    url: 'workflow/InstanceTask',
    method: 'PUT',
    data: data,
  })
}
/**
* 获取流程实例任务详情
* @param {Id}
*/
export function getInstanceTask(id) {
  return request({
    url: 'workflow/InstanceTask/' + id,
    method: 'get'
  })
}

/**
* 删除流程实例任务
* @param {主键} pid
*/
export function delInstanceTask(pid) {
  return request({
    url: 'workflow/InstanceTask/delete/' + pid,
    method: 'delete'
  })
}
