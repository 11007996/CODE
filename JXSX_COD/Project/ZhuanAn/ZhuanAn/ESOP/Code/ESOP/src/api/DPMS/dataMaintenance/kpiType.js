import request from '@/utils/request'

// 查询KPI类别
export function getKpiTypeList(params) {
    return request({
        url: '/dpms/kpiType/list',
        method: 'get',
        params
    })
}

//新增kpi类别
export function insertKpiType(data) {
    return request({
        url: '/dpms/kpiType/add',
        method: 'post',
        data
    })
}

//修改KPI类别
export function updateKpiType(data) {
    return request({
        url: 'dpms/kpiType/edit',
        method: 'put',
        data
    })
}

//删除kpi类别
export function removeKpiType(id) {
    return request({
        url: '/dpms/kpiType/remove/'+id,
        method: 'delete',
    })
}