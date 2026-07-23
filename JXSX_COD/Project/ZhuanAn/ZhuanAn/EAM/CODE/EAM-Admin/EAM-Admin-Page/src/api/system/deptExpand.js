import request from '@/utils/request'

/**
 * 系统部门扩展分页查询
 * @param {查询条件} data
 */
export function listSysDeptExpand(query) {
  return request({
    url: 'system/SysDeptExpand/list',
    method: 'get',
    params: query
  })
}

/**
 * 新增系统部门扩展
 * @param data
 */
export function addSysDeptExpand(data) {
  return request({
    url: 'system/SysDeptExpand',
    method: 'post',
    data: data
  })
}
/**
 * 修改系统部门扩展
 * @param data
 */
export function updateSysDeptExpand(data) {
  return request({
    url: 'system/SysDeptExpand',
    method: 'PUT',
    data: data
  })
}
/**
 * 获取系统部门扩展详情
 * @param {Id}
 */
export function getSysDeptExpand(id) {
  return request({
    url: 'system/SysDeptExpand/' + id,
    method: 'get'
  })
}

/**
 * 删除系统部门扩展
 * @param {主键} pid
 */
export function delSysDeptExpand(pid) {
  return request({
    url: 'system/SysDeptExpand/delete/' + pid,
    method: 'delete'
  })
}

/**
 * 同步系统部门名称
 * @param data
 */
export function SyncSysDeptExpand() {
  return request({
    url: 'system/SysDeptExpand/syncDeptName',
    method: 'PUT'
  })
}
