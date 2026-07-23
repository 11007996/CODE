import request from '@/utils/request'

/**
* 耗品领用单分页查询
* @param {查询条件} data
*/
export function listConsumableReceiveTicket(query) {
  return request({
    url: 'business/ConsumableReceiveTicket/list',
    method: 'get',
    params: query,
  })
}

/**
* 新增耗品领用单
* @param data
*/
export function addConsumableReceiveTicket(data) {
  return request({
    url: 'business/ConsumableReceiveTicket',
    method: 'post',
    data: data,
  })
}

/**
* 修改耗品领用单
* @param data
*/
export function updateConsumableReceiveTicket(data) {
  return request({
    url: 'business/ConsumableReceiveTicket',
    method: 'PUT',
    data: data,
  })
}

/**
* 获取耗品领用单详情
* @param {Id}
*/
export function getConsumableReceiveTicket(id) {
  return request({
    url: 'business/ConsumableReceiveTicket/' + id,
    method: 'get'
  })
}

/**
* 删除耗品领用单
* @param {主键} pid
*/
export function delConsumableReceiveTicket(pid) {
  return request({
    url: 'business/ConsumableReceiveTicket/delete/' + pid,
    method: 'delete'
  })
}

/**
* 获取耗品领用单_清单概要
* @param {Id}
*/
export function getConsumableReceiveTicketSummary(id) {
  return request({
    url: 'business/ConsumableReceiveTicket/' + id + '/summary',
    method: 'get'
  })
}

/**
* 获取耗品领用单_已领用清单
* @param {Id}
*/
export function getConsumableReceiveTicketReceive(id) {
  return request({
    url: 'business/ConsumableReceiveTicket/' + id + '/receive',
    method: 'get'
  })
}

/**
* 耗品领用单_耗品批量领用
* @param data
*/
export function batchReceiveConsumable(data) {
  return request({
    url: 'business/ConsumableReceiveTicket/receive',
    method: 'POST',
    data: data,
  })
}
