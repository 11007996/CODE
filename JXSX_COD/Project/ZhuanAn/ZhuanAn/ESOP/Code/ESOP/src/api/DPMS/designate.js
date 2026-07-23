import request from '@/utils/request'

// 查询KPI指派
export function getKpiAssignList(params) {
    return request({
        url: '/dpms/kpiAssign/list',
        method: 'get',
        params
    })
}

//指派
export function designateKpi(data) {
    return request({
        url: '/dpms/kpiAssign/add',
        method: 'post',
        data
    })
}
