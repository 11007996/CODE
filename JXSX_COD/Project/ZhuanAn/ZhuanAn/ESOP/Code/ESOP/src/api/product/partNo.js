import request from '@/utils/request'

// 查询料号列表
export function getPartNoList(params) {
    return request({
        url: '/replace/materialInfo/list',
        method: 'get',
        params
    })
}
//新增料号
export function insertPartNo(data) {
    return request({
        url: '/replace/materialInfo/add',
        method: 'post',
        data
    })
}

//修改料号
export function updatePartNo(data) {
    return request({
        url: '/replace/materialInfo/edit',
        method: 'PUT',
        data
    })
}

//复制工艺
export function copyTemplate(data) {
    return request({
        url: '/replace/relate/copyTemplate',
        method: 'PUT',
        data
    })
}



