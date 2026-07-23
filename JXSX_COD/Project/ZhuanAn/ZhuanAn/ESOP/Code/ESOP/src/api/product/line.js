import request from '@/utils/request'

// 查询线别信息列表
export function listLine(query) {
    return request({
        url: '/product/line/list',
        method: 'get',
        params: query
    })
}
export function listLineByMaterialId(id) {
    return request({
        url: '/product/line/list/'+id,
        method: 'get',
    })
}
// 查询线别信息详细
export function getLine(lineId) {
    return request({
        url: '/product/line/' + lineId,
        method: 'get'
    })
}

// 新增线别信息
export function addLine(data) {
    return request({
        url: '/product/line/add',
        method: 'post',
        data: data
    })
}

// 修改线别信息
export function updateLine(data) {
    return request({
        url: '/product/line/edit',
        method: 'put',
        data: data
    })
}

//修改线别状态
export function changeLineStatus(lineId, status) {
    const data = {
        lineId,
        status
    }
    return request({
        url: '/product/line/changeStatus',
        method: 'put',
        data: data
    })
}

// 删除线别信息
export function delLine(lineId) {
    return request({
        url: '/product/line/' + lineId,
        method: 'delete'
    })
}


export function getMesSiteOptions() {
    return request({
        url: '/product/site/getMesSiteList',
        method: 'get'
    })
}

//通过线体ID查询所绑定料号
export function getMaterialListByLineId(id) {
    return request({
        url: '/product/line/getMaterialListByLineId/' + id,
        method: 'get'
    })
}
