import request from '@/utils/request'

/**
* 产品物模型事件分页查询
* @param {查询条件} data
*/
export function listIotProductThingEvent(query) {
  return request({
    url: 'iot/IotProductThingEvent/list',
    method: 'get',
    params: query,
  })
}

/**
* 新增产品物模型事件
* @param data
*/
export function addIotProductThingEvent(data) {
  return request({
    url: 'iot/IotProductThingEvent',
    method: 'post',
    data: data,
  })
}
/**
* 修改产品物模型事件
* @param data
*/
export function updateIotProductThingEvent(data) {
  return request({
    url: 'iot/IotProductThingEvent',
    method: 'PUT',
    data: data,
  })
}
/**
* 获取产品物模型事件详情
* @param {Id}
*/
export function getIotProductThingEvent(id) {
  return request({
    url: 'iot/IotProductThingEvent/' + id,
    method: 'get'
  })
}

/**
* 删除产品物模型事件
* @param {主键} pid
*/
export function delIotProductThingEvent(pid) {
  return request({
    url: 'iot/IotProductThingEvent/delete/' + pid,
    method: 'delete'
  })
}
