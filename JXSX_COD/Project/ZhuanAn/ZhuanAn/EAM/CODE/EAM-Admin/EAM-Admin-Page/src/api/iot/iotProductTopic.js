import request from '@/utils/request'

/**
* 产品主题类表分页查询
* @param {查询条件} data
*/
export function listIotProductTopic(query) {
  return request({
    url: 'iot/IotProductTopic/list',
    method: 'get',
    params: query,
  })
}

/**
* 新增产品主题类表
* @param data
*/
export function addIotProductTopic(data) {
  return request({
    url: 'iot/IotProductTopic',
    method: 'post',
    data: data,
  })
}
/**
* 修改产品主题类表
* @param data
*/
export function updateIotProductTopic(data) {
  return request({
    url: 'iot/IotProductTopic',
    method: 'PUT',
    data: data,
  })
}
/**
* 获取产品主题类表详情
* @param {Id}
*/
export function getIotProductTopic(id) {
  return request({
    url: 'iot/IotProductTopic/' + id,
    method: 'get'
  })
}

/**
* 删除产品主题类表
* @param {主键} pid
*/
export function delIotProductTopic(pid) {
  return request({
    url: 'iot/IotProductTopic/delete/' + pid,
    method: 'delete'
  })
}
