import request from '@/utils/request'

/**
* 设备配置分页查询
* @param {查询条件} data
*/
export function listIotDeviceConfig(query) {
  return request({
    url: 'iot/IotDeviceConfig/list',
    method: 'get',
    params: query,
  })
}

/**
* 新增设备配置
* @param data
*/
export function addIotDeviceConfig(data) {
  return request({
    url: 'iot/IotDeviceConfig',
    method: 'post',
    data: data,
  })
}
/**
* 修改设备配置
* @param data
*/
export function updateIotDeviceConfig(data) {
  return request({
    url: 'iot/IotDeviceConfig',
    method: 'PUT',
    data: data,
  })
}
/**
* 获取设备配置详情
* @param {Id}
*/
export function getIotDeviceConfig(id) {
  return request({
    url: 'iot/IotDeviceConfig/' + id,
    method: 'get'
  })
}

/**
* 删除设备配置
* @param {主键} pid
*/
export function delIotDeviceConfig(pid) {
  return request({
    url: 'iot/IotDeviceConfig/delete/' + pid,
    method: 'delete'
  })
}
