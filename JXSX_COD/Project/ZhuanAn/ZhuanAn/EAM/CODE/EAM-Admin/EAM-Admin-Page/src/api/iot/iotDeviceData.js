import request from '@/utils/request'

/**
 * 设备采集数据分页查询
 * @param {查询条件} data
 */
export function listIotDeviceData(query) {
  return request({
    url: 'iot/IotDeviceData/list',
    method: 'get',
    params: query
  })
}

/**
 * 新增设备采集数据
 * @param data
 */
export function addIotDeviceData(data) {
  return request({
    url: 'iot/IotDeviceData',
    method: 'post',
    data: data
  })
}
/**
 * 获取设备采集数据详情
 * @param {Id}
 */
export function getIotDeviceData(id) {
  return request({
    url: 'iot/IotDeviceData/' + id,
    method: 'get'
  })
}

/**
 * 删除设备采集数据
 * @param {主键} pid
 */
export function delIotDeviceData(pid) {
  return request({
    url: 'iot/IotDeviceData/delete/' + pid,
    method: 'delete'
  })
}
