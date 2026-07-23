import request from '@/utils/request'

// 查询KPI
export function getKpiIndexList(params) {
    return request({
        url: '/dpms/kpiIndex/list',
        method: 'get',
        params
    })
}

//新增kpi 
export function insertKpiIndex(data) {
    return request({
        url: '/dpms/kpiIndex/add',
        method: 'post',
        data
    })
}

//修改KPI 
export function updateKpiIndex(data) {
    return request({
        url: 'dpms/kpiIndex/edit',
        method: 'put',
        data
    })
}

//删除kpi 
export function removeKpiIndex(id) {
    return request({
        url: '/dpms/kpiIndex/remove/'+id,
        method: 'delete',
    })
}