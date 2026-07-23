import request from '@/utils/request'

/**
 * 报表基本信息分页查询
 * @param {查询条件} data
 */
export function listReportBase(query) {
  return request({
    url: 'report/ReportBase/list',
    method: 'get',
    params: query
  })
}

/**
 * 新增报表基本信息
 * @param data
 */
export function addReportBase(data) {
  return request({
    url: 'report/ReportBase',
    method: 'post',
    data: data
  })
}
/**
 * 修改报表基本信息
 * @param data
 */
export function updateReportBase(data) {
  return request({
    url: 'report/ReportBase',
    method: 'PUT',
    data: data
  })
}
/**
 * 获取报表基本信息详情
 * @param {Id}
 */
export function getReportBase(id) {
  return request({
    url: 'report/ReportBase/' + id,
    method: 'get'
  })
}

/**
 * 删除报表基本信息
 * @param {主键} pid
 */
export function delReportBase(pid) {
  return request({
    url: 'report/ReportBase/delete/' + pid,
    method: 'delete'
  })
}

/**
 * 报表基本信息分页查询
 * @param {查询条件} data
 */
export function dictReportBase(query) {
  return request({
    url: 'report/ReportBase/dict',
    method: 'get',
    params: query
  })
}
