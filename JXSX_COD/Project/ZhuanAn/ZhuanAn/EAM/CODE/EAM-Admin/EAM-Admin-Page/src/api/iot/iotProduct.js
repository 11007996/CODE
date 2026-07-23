import request from '@/utils/request'

/**
 * 产品表分页查询
 * @param {查询条件} data
 */
export function listIotProduct(query) {
  return request({
    url: 'iot/IotProduct/list',
    method: 'get',
    params: query
  })
}

/**
 * 新增产品表
 * @param data
 */
export function addIotProduct(data) {
  return request({
    url: 'iot/IotProduct',
    method: 'post',
    data: data
  })
}
/**
 * 修改产品表
 * @param data
 */
export function updateIotProduct(data) {
  return request({
    url: 'iot/IotProduct',
    method: 'PUT',
    data: data
  })
}
/**
 * 获取产品表详情
 * @param {Id}
 */
export function getIotProduct(id) {
  return request({
    url: 'iot/IotProduct/' + id,
    method: 'get'
  })
}

/**
 * 删除产品表
 * @param {主键} pid
 */
export function delIotProduct(pid) {
  return request({
    url: 'iot/IotProduct/delete/' + pid,
    method: 'delete'
  })
}

/**
 * 发布产品
 * @param {主键} pid
 */
export function releaseIotProduct(pid) {
  return request({
    url: 'iot/IotProduct/release/' + pid,
    method: 'PUT'
  })
}

/**
 * 产品字典分页查询
 * @param {查询条件} data
 */
export function dictIotProduct(query) {
  return request({
    url: 'iot/IotProduct/dict',
    method: 'get',
    params: query
  })
}
