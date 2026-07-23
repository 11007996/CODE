import request from '@/utils/request'

/**
 * 机台类型分页查询
 * @param {查询条件} data
 */
export function listEquipmentType(query) {
  return request({
    url: 'equipment/EquipmentType/list',
    method: 'get',
    params: query
  })
}

/**
 * 新增机台类型
 * @param data
 */
export function addEquipmentType(data) {
  return request({
    url: 'equipment/EquipmentType',
    method: 'post',
    data: data
  })
}
/**
 * 修改机台类型
 * @param data
 */
export function updateEquipmentType(data) {
  return request({
    url: 'equipment/EquipmentType',
    method: 'PUT',
    data: data
  })
}
/**
 * 获取机台类型详情
 * @param {Id}
 */
export function getEquipmentType(id) {
  return request({
    url: 'equipment/EquipmentType/' + id,
    method: 'get'
  })
}

/**
 * 删除机台类型
 * @param {主键} pid
 */
export function delEquipmentType(pid) {
  return request({
    url: 'equipment/EquipmentType/delete/' + pid,
    method: 'delete'
  })
}

/**
 * 机台类型字典分页查询
 * @param {查询条件} data
 */
export function dictEquipmentType(query) {
  return request({
    url: 'equipment/EquipmentType/dict',
    method: 'get',
    params: query
  })
}
