import request from '@/utils/request'

// 查询本机mac
export function getLocalMac(params) {
    return request({
        url: '/product/mac/getMac',
        method: 'get',
        params
    })
}

//查询mac信息列表
export function getMacList(params) {
    return request({
        url: '/product/mac/list',
        method: 'get',
        params,
        headers: {
            isToken: false
        },
    })
}

//获取mac地址详细信息
export function getMacInfo(id) {
    return request({
        url: '/product/mac/' + id,
        method: 'get',
    })
}

//新增mac
export function insertMac(data) {
    return request({
        url: '/product/mac',
        method: 'post',
        data
    })
}

//修改mac地址
export function updateMac(data) {
    return request({
        url: '/product/mac',
        method: 'put',
        data
    })
}

//删除mac地址
export function removeMac(id) {
    return request({
        url: '/product/mac/' + id,
        method: 'DELETE',
    })
}

//更改MAC状态
export function changeStatus(data) {
    return request({
        url: '/product/mac/changeStatus',
        method: 'put',
        data
    })
}

// 导入模板
export function importTemplate() {
    return request({
        url: '/product/mac/downloadTemplate',
        method: 'get',
    })
}
