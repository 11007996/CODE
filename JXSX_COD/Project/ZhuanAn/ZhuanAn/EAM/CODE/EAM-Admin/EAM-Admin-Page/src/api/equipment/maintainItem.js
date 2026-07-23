import request from '@/utils/request'
import { downFile } from '@/utils/request'

/**
 * 设备保养项目分页查询
 * @param {查询条件} data
 */
export function listMaintainItem(query) {
  return request({
    url: 'equipment/MaintainItem/list',
    method: 'get',
    params: query
  })
}

/**
 * 新增设备保养项目
 * @param data
 */
export function addMaintainItem(data) {
  return request({
    url: 'equipment/MaintainItem',
    method: 'post',
    data: data
  })
}
/**
 * 修改设备保养项目
 * @param data
 */
export function updateMaintainItem(data) {
  return request({
    url: 'equipment/MaintainItem',
    method: 'PUT',
    data: data
  })
}
/**
 * 获取设备保养项目详情
 * @param {Id}
 */
export function getMaintainItem(id) {
  return request({
    url: 'equipment/MaintainItem/' + id,
    method: 'get'
  })
}

/**
 * 删除设备保养项目
 * @param {主键} pid
 */
export function delMaintainItem(pid) {
  return request({
    url: 'equipment/MaintainItem/delete/' + pid,
    method: 'delete'
  })
}
// 导出设备保养项目
export async function exportMaintainItem(query) {
  await downFile('equipment/MaintainItem/export', { ...query })
}
/**
 * 克隆设备保养项目
 * @param data
 */
export function cloneMaintainItem(data) {
  return request({
    url: 'equipment/MaintainItem/clone',
    method: 'post',
    data: data
  })
}
