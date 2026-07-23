import request from '@/utils/request'

/**
* 设备日志分页查询
* @param {查询条件} data
*/
export function listIotDeviceLog(query) {
  return request({
    url: 'iot/IotDeviceLog/list',
    method: 'get',
    params: query,
  })
}

/**
* 新增设备日志
* @param data
*/
export function addIotDeviceLog(data) {
  return request({
    url: 'iot/IotDeviceLog',
    method: 'post',
    data: data,
  })
}
/**
* 获取设备日志详情
* @param {Id}
*/
export function getIotDeviceLog(id) {
  return request({
    url: 'iot/IotDeviceLog/' + id,
    method: 'get'
  })
}

