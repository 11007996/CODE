import request from '@/utils/request'

/**
* 流程实例分页查询
* @param {查询条件} data
*/
export function listProcessInstance(query) {
  return request({
    url: 'workflow/ProcessInstance/list',
    method: 'get',
    params: query,
  })
}

/**
* 流程实例分页查询
* @param {查询条件} data
*/
export function listProcessInstanceByStatus(query) {
  return request({
    url: 'workflow/ProcessInstance/mylist',
    method: 'get',
    params: query,
  })
}

/**
* 新增流程实例
* @param data
*/
export function addProcessInstance(data) {
  return request({
    url: 'workflow/ProcessInstance',
    method: 'post',
    data: data,
  })
}
/**
* 修改流程实例
* @param data
*/
export function updateProcessInstance(data) {
  return request({
    url: 'workflow/ProcessInstance',
    method: 'PUT',
    data: data,
  })
}
/**
* 获取流程实例详情
* @param {Id}
*/
export function getProcessInstance(id) {
  return request({
    url: 'workflow/ProcessInstance/' + id,
    method: 'get'
  })
}

/**
* 删除流程实例
* @param {主键} pid
*/
export function delProcessInstance(pid) {
  return request({
    url: 'workflow/ProcessInstance/delete/' + pid,
    method: 'delete'
  })
}


/**
* 流程实例用户所处节点
* @param {查询条件} data
*/
export function userNodeForProcess(params) {
  return request({
    url: 'workflow/ProcessInstance/userNode',
    method: 'get',
    params: params,
  })
}


/**
* 获取流程实例详情
* @param {id} 流程ID(ProcessId)
*/
export function initProcessInstance(id) {
  return request({
    url: 'workflow/ProcessInstance/init/'+id,
    method: 'get',
  })
}


/**
* 获取当前用户待处理流程总数
* @param {id} 流程ID(ProcessId)
*/
export function PendingCountProcessInstance() {
  return request({
    url: 'workflow/ProcessInstance/count/Pending',
    method: 'get',
  })
}
