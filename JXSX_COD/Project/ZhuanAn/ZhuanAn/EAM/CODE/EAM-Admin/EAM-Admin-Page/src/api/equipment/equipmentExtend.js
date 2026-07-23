import request from '@/utils/request'

/**
 * 设备扩展信息分页查询
 * @param {查询条件} data
 */
export function listEquipmentExtend(query) {
  return request({
    url: 'equipment/EquipmentExtend/list',
    method: 'get',
    params: query
  })
}

/**
 * 新增设备扩展信息
 * @param data
 */
export function addEquipmentExtend(data) {
  return request({
    url: 'equipment/EquipmentExtend',
    method: 'post',
    data: data
  })
}
/**
 * 修改设备扩展信息
 * @param data
 */
export function updateEquipmentExtend(data) {
  return request({
    url: 'equipment/EquipmentExtend',
    method: 'PUT',
    data: data
  })
}
/**
 * 获取设备扩展信息详情
 * @param {Id}
 */
export function getEquipmentExtend(id) {
  return request({
    url: 'equipment/EquipmentExtend/' + id,
    method: 'get'
  })
}

/**
 * 删除设备扩展信息
 * @param {主键} pid
 */
export function delEquipmentExtend(pid) {
  return request({
    url: 'equipment/EquipmentExtend/delete/' + pid,
    method: 'delete'
  })
}
