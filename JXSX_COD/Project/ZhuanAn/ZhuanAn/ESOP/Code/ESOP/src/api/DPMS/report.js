import request from '@/utils/request'

// 查询KPI报表
export function getReportList(params) {
    return request({
        url: '/dpms/kpiReport/list',
        method: 'get',
        params
    })
}


