import request from '@/utils/request'

/**
* 产品物模型服务分页查询
* @param {查询条件} data
*/
export function listIotProductThingService(query) {
  return request({
    url: 'iot/IotProductThingService/list',
    method: 'get',
    params: query,
  })
}

/**
* 新增产品物模型服务
* @param data
*/
export function addIotProductThingService(data) {
  return request({
    url: 'iot/IotProductThingService',
    method: 'post',
    data: data,
  })
}
/**
* 修改产品物模型服务
* @param data
*/
export function updateIotProductThingService(data) {
  return request({
    url: 'iot/IotProductThingService',
    method: 'PUT',
    data: data,
  })
}
/**
* 获取产品物模型服务详情
* @param {Id}
*/
export function getIotProductThingService(id) {
  return request({
    url: 'iot/IotProductThingService/' + id,
    method: 'get'
  })
}

/**
* 删除产品物模型服务
* @param {主键} pid
*/
export function delIotProductThingService(pid) {
  return request({
    url: 'iot/IotProductThingService/delete/' + pid,
    method: 'delete'
  })
}
