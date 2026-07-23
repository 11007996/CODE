import request from '@/utils/request'

/**
* 报表参数分页查询
* @param {查询条件} data
*/
export function listReportParam(query) {
  return request({
    url: 'report/ReportParam/list',
    method: 'get',
    params: query,
  })
}

/**
* 新增报表参数
* @param data
*/
export function addReportParam(data) {
  return request({
    url: 'report/ReportParam',
    method: 'post',
    data: data,
  })
}
/**
* 修改报表参数
* @param data
*/
export function updateReportParam(data) {
  return request({
    url: 'report/ReportParam',
    method: 'PUT',
    data: data,
  })
}
/**
* 获取报表参数详情
* @param {Id}
*/
export function getReportParam(id) {
  return request({
    url: 'report/ReportParam/' + id,
    method: 'get'
  })
}

/**
* 删除报表参数
* @param {主键} pid
*/
export function delReportParam(pid) {
  return request({
    url: 'report/ReportParam/delete/' + pid,
    method: 'delete'
  })
}
