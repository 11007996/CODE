import request from '@/utils/request'

/**
* 流程定义分页查询
* @param {查询条件} data
*/
export function listProcessDefine(query) {
  return request({
    url: 'workflow/ProcessDefine/list',
    method: 'get',
    params: query,
  })
}

/**
 * 流程定义标签分页查询
 * @param {查询条件} query
 * @returns
 */
export function dictProcessDefine(query) {
  return request({
    url: 'workflow/ProcessDefine/dict',
    method: 'get',
    params: query,
  })
}


/**
* 新增流程定义
* @param data
*/
export function addProcessDefine(data) {
  return request({
    url: 'workflow/ProcessDefine',
    method: 'post',
    data: data,
  })
}
/**
* 修改流程定义
* @param data
*/
export function updateProcessDefine(data) {
  return request({
    url: 'workflow/ProcessDefine',
    method: 'PUT',
    data: data,
  })
}
/**
* 获取流程定义详情
* @param {Id}
*/
export function getProcessDefine(id) {
  return request({
    url: 'workflow/ProcessDefine/' + id,
    method: 'get'
  })
}

/**
* 删除流程定义
* @param {主键} pid
*/
export function delProcessDefine(pid) {
  return request({
    url: 'workflow/ProcessDefine/delete/' + pid,
    method: 'delete'
  })
}


/**
* 获取流程节点审批人列表
* @param {Id}
*/
export function getProcessNodeApprover(id) {
  return request({
    url: 'workflow/ProcessDefine/' + id + "/nodeApprover",
    method: 'get'
  })
}
