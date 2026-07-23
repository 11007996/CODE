import request from '@/utils/request'

/**
* 报表数据列分页查询
* @param {查询条件} data
*/
export function listReportColumn(query) {
  return request({
    url: 'report/ReportColumn/list',
    method: 'get',
    params: query,
  })
}

/**
* 新增报表数据列
* @param data
*/
export function addReportColumn(data) {
  return request({
    url: 'report/ReportColumn',
    method: 'post',
    data: data,
  })
}
/**
* 修改报表数据列
* @param data
*/
export function updateReportColumn(data) {
  return request({
    url: 'report/ReportColumn',
    method: 'PUT',
    data: data,
  })
}
/**
* 获取报表数据列详情
* @param {Id}
*/
export function getReportColumn(id) {
  return request({
    url: 'report/ReportColumn/' + id,
    method: 'get'
  })
}

/**
* 删除报表数据列
* @param {主键} pid
*/
export function delReportColumn(pid) {
  return request({
    url: 'report/ReportColumn/delete/' + pid,
    method: 'delete'
  })
}
