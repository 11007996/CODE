import request from '@/utils/request'
import { downFile } from '@/utils/request'

/**
 * 设备资产信息分页查询
 * @param {查询条件} data
 */
export function listEquipmentBase(query) {
  return request({
    url: 'equipment/equipmentBase/list',
    method: 'get',
    params: query
  })
}

/**
 * 新增设备资产信息
 * @param data
 */
export function addEquipmentBase(data) {
  return request({
    url: 'equipment/equipmentBase',
    method: 'post',
    data: data
  })
}
/**
 * 修改设备资产信息
 * @param data
 */
export function updateEquipmentBase(data) {
  return request({
    url: 'equipment/equipmentBase',
    method: 'PUT',
    data: data
  })
}
/**
 * 获取设备资产信息详情
 * @param {Id}
 */
export function getEquipmentBase(id) {
  return request({
    url: 'equipment/equipmentBase/' + id,
    method: 'get'
  })
}

/**
 * 删除设备资产信息
 * @param {主键} pid
 */
export function delEquipmentBase(pid) {
  return request({
    url: 'equipment/equipmentBase/delete/' + pid,
    method: 'delete'
  })
}
// 导出设备资产信息
export async function exportEquipmentBase(query) {
  await downFile('equipment/equipmentBase/export', { ...query })
}
/**
 * 设备表字典
 * @param {查询条件} data
 */
export function dictEquipmentBase(query) {
  return request({
    url: 'equipment/equipmentBase/dict',
    method: 'get',
    params: query
  })
}

/**
 * 成本中心字典
 * @param {查询条件} data
 */
export function dictCostCenter(query) {
  return request({
    url: 'equipment/equipmentBase/dict/costCenter',
    method: 'get',
    params: query
  })
}

/**
 * 自定义机型字典
 * @param {查询条件} data
 */
export function dictCustomModel(query) {
  return request({
    url: 'equipment/equipmentBase/dict/customModel',
    method: 'get',
    params: query
  })
}

/**
 * 闲置设备
 * @param {查询条件} data
 */
export function idleEquipmentBase(query) {
  return request({
    url: 'equipment/equipmentBase/idle',
    method: 'get',
    params: query
  })
}
