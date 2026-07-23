import request from '@/utils/request'
import { downFile } from '@/utils/request'

/**
 * 设备运行数据分页查询
 * @param {查询条件} data
 */
export function listEquipmentRuningRecord(query) {
  return request({
    url: 'equipment/EquipmentRuningRecord/list',
    method: 'get',
    params: query
  })
}

/**
 * 新增设备运行数据
 * @param data
 */
export function addEquipmentRuningRecord(data) {
  return request({
    url: 'equipment/EquipmentRuningRecord',
    method: 'post',
    data: data
  })
}

/**
 * 删除设备运行数据
 * @param {主键} pid
 */
export function delEquipmentRuningRecord(pid) {
  return request({
    url: 'equipment/EquipmentRuningRecord/delete/' + pid,
    method: 'delete'
  })
}
// 导出设备运行数据
export async function exportEquipmentRuningRecord(query) {
  await downFile('equipment/EquipmentRuningRecord/export', { ...query })
}

/**
 * 设备实时运行数据
 * @param {查询条件} data
 */
export function listEquipmentRuningWatch(query) {
  return request({
    url: 'equipment/EquipmentRuningRecord/watch',
    method: 'get',
    params: query
  })
}

/**
 * 设备实时运行详情
 * @param {*} query
 * @returns
 */
export function detailEquipmentRuningWatch(query) {
  return request({
    url: 'equipment/EquipmentRuningRecord/watch/detail',
    method: 'get',
    params: query
  })
}
