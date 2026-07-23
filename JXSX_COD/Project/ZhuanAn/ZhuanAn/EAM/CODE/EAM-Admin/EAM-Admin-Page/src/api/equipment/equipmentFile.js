import request from '@/utils/request'

/**
 * 设备文件分页查询
 * @param {查询条件} data
 */
export function listEquipmentFile(query) {
  return request({
    url: 'equipment/EquipmentFile/list',
    method: 'get',
    params: query
  })
}

/**
 * 新增设备文件
 * @param data
 */
export function addEquipmentFile(data) {
  return request({
    url: 'equipment/EquipmentFile',
    method: 'post',
    data: data
  })
}

/**
 * 批量新增设备文件
 * @param data
 */
export function bacthAddEquipmentFile(data) {
  return request({
    url: 'equipment/EquipmentFile/batchAdd',
    method: 'post',
    data: data
  })
}

/**
 * 获取设备文件详情
 * @param {Id}
 */
export function getEquipmentFile(id) {
  return request({
    url: 'equipment/EquipmentFile/' + id,
    method: 'get'
  })
}

/**
 * 删除设备文件
 * @param {主键} pid
 */
export function delEquipmentFile(pid) {
  return request({
    url: 'equipment/EquipmentFile/delete/' + pid,
    method: 'delete'
  })
}

// ************************* 文件关联 ********************************
/**
 * 设备文件关联分页查询
 * @param {查询条件} data
 */
export function listEquipmentFileBind(query) {
  return request({
    url: 'equipment/EquipmentFile/bind/list',
    method: 'get',
    params: query
  })
}

/**
 * 设备文件绑定
 * @param data
 */
export function bindEquipmentFile(data) {
  return request({
    url: 'equipment/EquipmentFile/bind',
    method: 'post',
    data: data
  })
}

/**
 * 设备文件解绑
 * @param data
 */
export function unbindEquipmentFile(data) {
  return request({
    url: 'equipment/EquipmentFile/unbind',
    method: 'delete',
    data: data
  })
}
