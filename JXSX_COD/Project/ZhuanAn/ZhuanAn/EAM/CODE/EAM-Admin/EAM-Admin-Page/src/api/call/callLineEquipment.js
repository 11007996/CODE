import request from '@/utils/request'

/**
* 产线设备分页查询
* @param {查询条件} data
*/
export function listCallLineEquipment(query) {
  return request({
    url: 'call/CallLineEquipment/list',
    method: 'get',
    params: query,
  })
}

/**
* 新增产线设备
* @param data
*/
export function addCallLineEquipment(data) {
  return request({
    url: 'call/CallLineEquipment',
    method: 'post',
    data: data,
  })
}
/**
* 修改产线设备
* @param data
*/
export function updateCallLineEquipment(data) {
  return request({
    url: 'call/CallLineEquipment',
    method: 'PUT',
    data: data,
  })
}
/**
* 获取产线设备详情
* @param {Id}
*/
export function getCallLineEquipment(id) {
  return request({
    url: 'call/CallLineEquipment/' + id,
    method: 'get'
  })
}

/**
* 删除产线设备
* @param {主键} pid
*/
export function delCallLineEquipment(pid) {
  return request({
    url: 'call/CallLineEquipment/delete/' + pid,
    method: 'delete'
  })
}
