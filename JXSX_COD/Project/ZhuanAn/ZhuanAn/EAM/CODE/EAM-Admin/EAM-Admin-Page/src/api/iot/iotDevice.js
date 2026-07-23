import request from '@/utils/request'

/**
 * 产品设备表分页查询
 * @param {查询条件} data
 */
export function listIotDevice(query) {
  return request({
    url: 'iot/IotDevice/list',
    method: 'get',
    params: query
  })
}

/**
 * 新增产品设备表
 * @param data
 */
export function addIotDevice(data) {
  return request({
    url: 'iot/IotDevice',
    method: 'post',
    data: data
  })
}
/**
 * 修改产品设备表
 * @param data
 */
export function updateIotDevice(data) {
  return request({
    url: 'iot/IotDevice',
    method: 'PUT',
    data: data
  })
}
/**
 * 获取产品设备表详情
 * @param {Id}
 */
export function getIotDevice(id) {
  return request({
    url: 'iot/IotDevice/' + id,
    method: 'get'
  })
}

/**
 * 删除产品设备表
 * @param {主键} pid
 */
export function delIotDevice(pid) {
  return request({
    url: 'iot/IotDevice/delete/' + pid,
    method: 'delete'
  })
}

/**
 * 产品设备字典查询
 * @param {查询条件} data
 */
export function dictIotDevice(query) {
  return request({
    url: 'iot/IotDevice/dict',
    method: 'get',
    params: query
  })
}

/**
 * 产品设备绑定
 * @param data
 */
export function bindIotDevice(data) {
  return request({
    url: 'iot/IotDevice/bind',
    method: 'post',
    data: data
  })
}

/**
 * 产品设备解绑
 * @param {主键} pid
 */
export function unbindIotDevice(pid) {
  return request({
    url: 'iot/IotDevice/unbind/' + pid,
    method: 'delete'
  })
}
