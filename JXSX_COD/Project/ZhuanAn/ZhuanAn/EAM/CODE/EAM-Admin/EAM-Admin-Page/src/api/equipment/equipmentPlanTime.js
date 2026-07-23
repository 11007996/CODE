import request from '@/utils/request'

/**
 * 设备计划停机时间分页查询
 * @param {查询条件} data
 */
export function listEquipmentPlanTime(query) {
  return request({
    url: 'equipment/EquipmentPlanTime/list',
    method: 'get',
    params: query
  })
}

/**
 * 新增设备计划停机时间
 * @param data
 */
export function addEquipmentPlanTime(data) {
  return request({
    url: 'equipment/EquipmentPlanTime',
    method: 'post',
    data: data
  })
}
/**
 * 修改设备计划停机时间
 * @param data
 */
export function updateEquipmentPlanTime(data) {
  return request({
    url: 'equipment/EquipmentPlanTime',
    method: 'PUT',
    data: data
  })
}
/**
 * 获取设备计划停机时间详情
 * @param {Id}
 */
export function getEquipmentPlanTime(id) {
  return request({
    url: 'equipment/EquipmentPlanTime/' + id,
    method: 'get'
  })
}

/**
 * 删除设备计划停机时间
 * @param {主键} pid
 */
export function delEquipmentPlanTime(pid) {
  return request({
    url: 'equipment/EquipmentPlanTime/delete/' + pid,
    method: 'delete'
  })
}
