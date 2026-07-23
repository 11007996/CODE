import request from '@/utils/request'

/**
 * 故障记录分页查询
 * @param {查询条件} data
 */
export function listCallFaultBase(query) {
  return request({
    url: 'call/CallFaultBase/list',
    method: 'get',
    params: query
  })
}

/**
 * 修改故障记录
 * @param data
 */
export function updateCallFaultBase(data) {
  return request({
    url: 'call/CallFaultBase',
    method: 'PUT',
    data: data
  })
}
/**
 * 获取故障记录详情
 * @param {Id}
 */
export function getCallFaultBase(id) {
  return request({
    url: 'call/CallFaultBase/' + id,
    method: 'get'
  })
}

/**
 * 删除故障记录
 * @param {主键} pid
 */
export function delCallFaultBase(pid) {
  return request({
    url: 'call/CallFaultBase/delete/' + pid,
    method: 'delete'
  })
}

/***************************** 呼叫操作 Start ********************************* */
/**
 * 产线的设备、区域、工站等信息
 * @param {查询条件} data
 */
export function getCallSummaryByLine(lineId) {
  return request({
    url: 'call/CallFaultBase/line/' + lineId + '/summary',
    method: 'get'
  })
}

/**
 * 产线的设备、区域等信息
 * @param {查询条件} data
 */
export function getUnsolvedCallFaultBase(lineId) {
  return request({
    url: 'call/CallFaultBase/line/unsolved/' + lineId,
    method: 'get'
  })
}

/**
 * 呼叫故障
 * @param data
 */
export function addCallFaultBase(data) {
  return request({
    url: 'call/CallFaultBase',
    method: 'post',
    data: data
  })
}

/**
 * 处理签到
 * @param data
 */
export function handleCallFaultBase(data) {
  return request({
    url: 'call/CallFaultBase/handle',
    method: 'PUT',
    data: data
  })
}

/**
 * 请求支援
 * @param data
 */
export function requestHelpCallFaultBase(data) {
  return request({
    url: 'call/CallFaultBase/requestHelp',
    method: 'PUT',
    data: data
  })
}

/**
 * 支援签到
 * @param data
 */
export function helpCallFaultBase(data) {
  return request({
    url: 'call/CallFaultBase/help',
    method: 'PUT',
    data: data
  })
}

/**
 * 完成
 * @param data
 */
export function solveCallFaultBase(data) {
  return request({
    url: 'call/CallFaultBase/solve',
    method: 'PUT',
    data: data
  })
}

/**
 * 中止
 * @param data
 */
export function stopCallFaultBase(ids) {
  return request({
    url: 'call/CallFaultBase/stop/' + ids,
    method: 'PUT'
  })
}
