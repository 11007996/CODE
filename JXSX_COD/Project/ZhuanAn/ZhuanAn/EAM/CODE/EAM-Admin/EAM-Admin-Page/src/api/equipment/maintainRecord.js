import request from '@/utils/request'
import { downFile } from '@/utils/request'

/**
 * 设备保养记录分页查询
 * @param {查询条件} data
 */
export function listMaintainRecord(query) {
  return request({
    url: 'equipment/MaintainRecord/list',
    method: 'get',
    params: query
  })
}

/**
 * 新增设备保养记录
 * @param data
 */
export function addMaintainRecord(data) {
  return request({
    url: 'equipment/MaintainRecord',
    method: 'post',
    data: data
  })
}
/**
 * 修改设备保养记录
 * @param data
 */
export function updateMaintainRecord(data) {
  return request({
    url: 'equipment/MaintainRecord',
    method: 'PUT',
    data: data
  })
}
/**
 * 获取设备保养记录详情
 * @param {Id}
 */
export function getMaintainRecord(id) {
  return request({
    url: 'equipment/MaintainRecord/' + id,
    method: 'get'
  })
}

/**
 * 删除设备保养记录
 * @param {主键} pid
 */
export function delMaintainRecord(pid) {
  return request({
    url: 'equipment/MaintainRecord/delete/' + pid,
    method: 'delete'
  })
}
// 导出设备保养记录
export async function exportMaintainRecord(query) {
  await downFile('equipment/MaintainRecord/export', { ...query })
}

/**
 * 详情
 * @param {查询条件} data
 */
export function detailMaintainRecord(query) {
  return request({
    url: 'equipment/MaintainRecord/detail',
    method: 'get',
    params: query
  })
}
/**
 * 全局保养
 * @param data
 */
export function globalMaintainRecord(data) {
  return request({
    url: 'equipment/MaintainRecord/globalMaintain',
    method: 'post',
    data: data,
    timeout: 5 * 60 * 1000 //5分钟
  })
}
