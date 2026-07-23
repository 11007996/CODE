import request from '@/utils/request'

/**
 * 设备保管分页查询
 * @param {查询条件} data
 */
export function listEquipmentStorage(query) {
  return request({
    url: 'equipment/EquipmentStorage/list',
    method: 'get',
    params: query
  })
}

/**
 * 新增设备保管
 * @param data
 */
export function addEquipmentStorage(data) {
  return request({
    url: 'equipment/EquipmentStorage',
    method: 'post',
    data: data
  })
}
/**
 * 修改设备保管
 * @param data
 */
export function updateEquipmentStorage(data) {
  return request({
    url: 'equipment/EquipmentStorage',
    method: 'PUT',
    data: data
  })
}
/**
 * 获取设备保管详情
 * @param {Id}
 */
export function getEquipmentStorage(id) {
  return request({
    url: 'equipment/EquipmentStorage/' + id,
    method: 'get'
  })
}

/**
 * 删除设备保管
 * @param {主键} pid
 */
export function delEquipmentStorage(pid) {
  return request({
    url: 'equipment/EquipmentStorage/delete/' + pid,
    method: 'delete'
  })
}
/**
 * 领用
 * @param data
 */
export function receiveEquipmentStorage(data) {
  return request({
    url: 'equipment/EquipmentStorage/receive',
    method: 'PUT',
    data: data
  })
}

/**
 * 设备批量领用
 * @param data
 */
export function batchReceiveEquipment(id, data) {
  return request({
    url: 'equipment/EquipmentStorage/receive/batch',
    method: 'POST',
    data: data
  })
}

/**
 * 归还
 * @param data
 */
export function backEquipmentStorage(data) {
  return request({
    url: 'equipment/EquipmentStorage/back',
    method: 'PUT',
    data: data
  })
}
/**
 * 设备保管记录分页查询
 * @param {查询条件} data
 */
export function listEquipmentStorageRecord(query) {
  return request({
    url: 'equipment/EquipmentStorage/record',
    method: 'get',
    params: query
  })
}
