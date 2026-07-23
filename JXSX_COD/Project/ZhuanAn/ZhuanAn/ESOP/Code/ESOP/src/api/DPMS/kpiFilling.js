import request from '@/utils/request'

// 查询待填报列表
export function getKpiFillList(params) {
    return request({
        url: '/dpms/kpiFill/list',
        method: 'get',
        params
    })
}


//填报
export function fillKpi(data) {
    return request({
        url: '/dpms/kpiFill/edit',
        method: 'post',
        data
    })
}