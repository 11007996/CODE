import request from '@/utils/request'

/**
 * 获取报表基本信息详情
 * @param {Id}
 */
export function getReportInfo(id) {
  return request({
    url: 'report/ReportExecute/reportInfo/' + id,
    method: 'get'
  })
}

/**
 * 获取报表基本信息详情
 * @param {Id}
 */
export function getReportParamOptions(query) {
  return request({
    url: 'report/ReportExecute/ReportParamOptions',
    method: 'get',
    params: query
  })
}

/**
 * 报表基本信息分页查询
 * @param {查询条件} data
 */
export function listReportExecute(data) {
  return request({
    url: 'report/ReportExecute/list',
    method: 'get',
    params: data
  })
}

// 导出设备资产信息
export async function exportReportExecute(query) {
  await downFile('report/ReportExecute/export', { ...query })
}
