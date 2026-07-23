import request from '@/utils/request'
import QS from 'qs'

/**
* 流程节点定义分页查询
* @param {查询条件} data
*/
export function listNodeDefine(query) {
  return request({
    url: 'workflow/NodeDefine/list',
    method: 'get',
    params: query,
    paramsSerializer: function (params) {
      return QS.stringify(params, { indices: false })
    }
  })
}

/**
 * 节点详情分页查询
 * @param {查询条件} query
 * @returns
 */
export function detailListNodeDefine(query) {
  return request({
    url: 'workflow/NodeDefine/detailList',
    method: 'get',
    params: query,
    paramsSerializer: function (params) {
      return QS.stringify(params, { indices: false })
    }
  })
}

/**
 * 节点详情分页查询
 * @param {查询条件} query
 * @returns
 */
export function dictNodeDefine(query) {
  return request({
    url: 'workflow/NodeDefine/dict',
    method: 'get',
    params: query,
    paramsSerializer: function (params) {
      return QS.stringify(params, { indices: false })
    }
  })
}


/**
* 新增流程节点定义
* @param data
*/
export function addNodeDefine(data) {
  return request({
    url: 'workflow/NodeDefine',
    method: 'post',
    data: data,
  })
}
/**
* 修改流程节点定义
* @param data
*/
export function updateNodeDefine(data) {
  return request({
    url: 'workflow/NodeDefine',
    method: 'PUT',
    data: data,
  })
}
/**
* 获取流程节点定义详情
* @param {Id}
*/
export function getNodeDefine(id) {
  return request({
    url: 'workflow/NodeDefine/' + id,
    method: 'get'
  })
}

/**
* 删除流程节点定义
* @param {主键} pid
*/
export function delNodeDefine(pid) {
  return request({
    url: 'workflow/NodeDefine/delete/' + pid,
    method: 'delete'
  })
}



// ---------------------节点流向--------------------------------

/**
* 节点流向分页查询
* @param {查询条件} data
*/
export function listNodeFlow(query) {
  return request({
    url: 'workflow/NodeDefine/flow/list',
    method: 'get',
    params: query,
  })
}

/**
* 新增节点流向
* @param data
*/
export function addNodeFlow(data) {
  return request({
    url: 'workflow/NodeDefine/flow',
    method: 'post',
    data: data,
  })
}
/**
* 修改节点流向
* @param data
*/
export function updateNodeFlow(data) {
  return request({
    url: 'workflow/NodeDefine/flow',
    method: 'PUT',
    data: data,
  })
}
/**
* 获取节点流向详情
* @param {Id}
*/
export function getNodeFlow(id) {
  return request({
    url: 'workflow/NodeDefine/flow/' + id,
    method: 'get'
  })
}

/**
* 删除节点流向
* @param {主键} pid
*/
export function delNodeFlow(pid) {
  return request({
    url: 'workflow/NodeDefine/flow/delete/' + pid,
    method: 'delete'
  })
}

//--------------------节点字段控制----------------------------

/**
* 节点字段控件配置分页查询
* @param {查询条件} data
*/
export function listNodeFieldControl(query) {
  return request({
    url: 'workflow/NodeDefine/field/list',
    method: 'get',
    params: query,
  })
}

/**
* 节点字段控件配置分页查询
* @param {查询条件} data
*/
export function detailListNodeFieldControl(query) {
  return request({
    url: 'workflow/NodeDefine/field/detailList',
    method: 'get',
    params: query,
  })
}


/**
* 新增节点字段控件配置
* @param data
*/
export function addNodeFieldControl(data) {
  return request({
    url: 'workflow/NodeDefine/field',
    method: 'post',
    data: data,
  })
}
/**
* 修改节点字段控件配置
* @param data
*/
export function updateNodeFieldControl(data) {
  return request({
    url: 'workflow/NodeDefine/field',
    method: 'PUT',
    data: data,
  })
}

/**
* 批量修改节点字段控件配置
* @param data
*/
export function batchUpdateNodeFieldControl(data) {
  return request({
    url: 'workflow/NodeDefine/field/batch',
    method: 'PUT',
    data: data,
  })
}
/**
* 获取节点字段控件配置详情
* @param {Id}
*/
export function getNodeFieldControl(id) {
  return request({
    url: 'workflow/NodeDefine/field/' + id,
    method: 'get'
  })
}

/**
* 删除节点字段控件配置
* @param {主键} pid
*/
export function delNodeFieldControl(pid) {
  return request({
    url: 'workflow/NodeDefine/field/delete/' + pid,
    method: 'delete'
  })
}




//-------------------节点审批人------------------------------

/**
* 节点审批人配置分页查询
* @param {查询条件} data
*/
export function listNodeApprover(query) {
  return request({
    url: 'workflow/NodeDefine/approver/list',
    method: 'get',
    params: query,
  })
}

/**
* 节点审批人配置分页查询
* @param {查询条件} data
*/
export function dictNodeApprover(query) {
  return request({
    url: 'workflow/NodeDefine/approver/dict',
    method: 'get',
    params: query,
  })
}


/**
* 新增节点审批人配置
* @param data
*/
export function addNodeApprover(data) {
  return request({
    url: 'workflow/NodeDefine/approver',
    method: 'post',
    data: data,
  })
}
/**
* 修改节点审批人配置
* @param data
*/
export function updateNodeApprover(data) {
  return request({
    url: 'workflow/NodeDefine/approver',
    method: 'PUT',
    data: data,
  })
}
/**
* 获取节点审批人配置详情
* @param {Id}
*/
export function getNodeApprover(id) {
  return request({
    url: 'workflow/NodeDefine/approver/' + id,
    method: 'get'
  })
}

/**
* 删除节点审批人配置
* @param {主键} pid
*/
export function delNodeApprover(pid) {
  return request({
    url: 'workflow/NodeDefine/approver/delete/' + pid,
    method: 'delete'
  })
}
