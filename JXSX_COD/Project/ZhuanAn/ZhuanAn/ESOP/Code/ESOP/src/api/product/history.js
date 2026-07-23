import request from '@/utils/request'

// 查询线别信息列表
export function getSopHistory(params) {
    return request({
        url: '/system/mesUploadTerminalPage/HistoricalTemplateInfo',
        method: 'get',
        params
    })
}

// 修改SOP状态
export function changeSopStatus(data) {
    return request({
        url: '/system/mesUploadTerminalPage/changeSopStatus',
        method: 'put',
        data
    })
}
