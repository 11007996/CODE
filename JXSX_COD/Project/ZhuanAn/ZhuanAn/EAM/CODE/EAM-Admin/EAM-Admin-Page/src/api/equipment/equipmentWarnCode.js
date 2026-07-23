import request from '@/utils/request'

/**
 * 设备报警代码分页查询
 * @param {查询条件} data
 */
export function listEquipmentWarnCode(query) {
  return request({
    url: 'equipment/EquipmentWarnCode/list',
    method: 'get',
    params: query
  })
}

/**
 * 新增设备报警代码
 * @param data
 */
export function addEquipmentWarnCode(data) {
  return request({
    url: 'equipment/EquipmentWarnCode',
    method: 'post',
    data: data
  })
}
/**
 * 修改设备报警代码
 * @param data
 */
export function updateEquipmentWarnCode(data) {
  return request({
    url: 'equipment/EquipmentWarnCode',
    method: 'PUT',
    data: data
  })
}
/**
 * 获取设备报警代码详情
 * @param {Id}
 */
export function getEquipmentWarnCode(query) {
  return request({
    url: 'equipment/EquipmentWarnCode/info',
    method: 'get',
    params: query
  })
}

/**
 * 删除设备报警代码
 * @param {主键} pid
 */
export function delEquipmentWarnCode(data) {
  return request({
    url: 'equipment/EquipmentWarnCode/delete',
    method: 'delete',
    data: data
  })
}
