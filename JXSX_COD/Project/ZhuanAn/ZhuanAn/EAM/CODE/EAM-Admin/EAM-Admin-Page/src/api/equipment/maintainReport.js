import request from '@/utils/request'

/**
 * 资产保养报表分页查询
 * @param {查询条件} data
 */
export function listMaintainReport(query) {
  return request({
    url: 'equipment/MaintainReport/list',
    method: 'get',
    params: query
  })
}

/**
 * 新增资产保养报表
 * @param data
 */
export function addMaintainReport(data) {
  return request({
    url: 'equipment/MaintainReport',
    method: 'post',
    data: data
  })
}
/**
 * 批量新增资产保养报表
 * @param data
 */
export function batchAddMaintainReport(data) {
  return request({
    url: 'equipment/MaintainReport/batch',
    method: 'post',
    data: data
  })
}
/**
 * 修改资产保养报表
 * @param data
 */
export function updateMaintainReport(data) {
  return request({
    url: 'equipment/MaintainReport',
    method: 'PUT',
    data: data
  })
}
/**
 * 获取资产保养报表详情
 * @param {Id}
 */
export function getMaintainReport(id) {
  return request({
    url: 'equipment/MaintainReport/' + id,
    method: 'get'
  })
}

/**
 * 删除资产保养报表
 * @param {主键} pid
 */
export function delMaintainReport(pid) {
  return request({
    url: 'equipment/MaintainReport/delete/' + pid,
    method: 'delete'
  })
}

/**
 * 资产保养报表单页Sheet查询
 * @param {查询条件} data
 */
export function sheetMaintainReport(query) {
  return request({
    url: 'equipment/MaintainReport/sheet',
    method: 'get',
    params: query
  })
}

/**
 * 资产保养报表概况查询
 * @param {查询条件} data
 */
export function overviewMaintainReport(query) {
  return request({
    url: 'equipment/MaintainReport/overview',
    method: 'get',
    params: query
  })
}
