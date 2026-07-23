import request from '@/utils/request'

/**
 * 报表分组分页查询
 * @param {查询条件} data
 */
export function listReportGroup(query) {
  return request({
    url: 'report/ReportGroup/list',
    method: 'get',
    params: query
  })
}

/**
 * 新增报表分组
 * @param data
 */
export function addReportGroup(data) {
  return request({
    url: 'report/ReportGroup',
    method: 'post',
    data: data
  })
}
/**
 * 修改报表分组
 * @param data
 */
export function updateReportGroup(data) {
  return request({
    url: 'report/ReportGroup',
    method: 'PUT',
    data: data
  })
}
/**
 * 获取报表分组详情
 * @param {Id}
 */
export function getReportGroup(id) {
  return request({
    url: 'report/ReportGroup/' + id,
    method: 'get'
  })
}

/**
 * 删除报表分组
 * @param {主键} pid
 */
export function delReportGroup(pid) {
  return request({
    url: 'report/ReportGroup/delete/' + pid,
    method: 'delete'
  })
}

/**
 * 报表分组字典查询
 * @param {查询条件} data
 */
export function dictReportGroup(query) {
  return request({
    url: 'report/ReportGroup/dict',
    method: 'get',
    params: query
  })
}
