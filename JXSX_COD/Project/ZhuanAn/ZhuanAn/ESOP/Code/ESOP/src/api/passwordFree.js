
import request from '@/utils/request'

// 获取线体
export function selectLineByMac(params) {
    return request({
        url: '/product/mac/selectLineByMac',
        headers: {
            isToken: false
        },
        method: 'GET',
        params
    })
}

//获取站位
export function selectStageByMac(params) {
    return request({
        url: '/product/mac/selectStageByMac',
        headers: {
            isToken: false
        },
        method: 'GET',
        params
    })
}

export function selectProcessByMac(params) {
    return request({
        url: '/product/mac/selectProcessByMac',
        headers: {
            isToken: false
        },
        method: 'GET',
        params
    })
}
//
