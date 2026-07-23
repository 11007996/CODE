import request from "@/utils/request";

// 查询oa签核列表
export function listOa(params) {
    return request({
        url: "/esopApi/SignOff",
        method: "get",
        params,
    });
}
// 查询oa签核详情
export function listOaDetail(params) {
    return request({
        url: "/esopApi/SignOffDetail",
        method: "get",
        params,
    });
}

// // 查询oa签核详细
// export function getOa(oaId) {
//     return request({
//         url: "/product/oa/" + oaId,
//         method: "get",
//     });
// }

// // 新增oa签核
// export function addOa(data) {
//   return request({
//     url: '/product/oa',
//     method: 'post',
//     data: data
//   })
// }

// // 修改oa签核
// export function updateOa(data) {
//   return request({
//     url: '/product/oa',
//     method: 'put',
//     data: data
//   })
// }

// // 删除oa签核
// export function delOa(oaId) {
//   return request({
//     url: '/product/oa/' + oaId,
//     method: 'delete'
//   })
// }

export function getMesModelOptions() {
    return request({
        url: "/product/oa/getMesModelList",
        method: "get",
    });
}

export function uploadFile(data) {
    return request({
        url: "/product/oa/upload",
        method: "post",
        data: data,
    });
}

export function getSopToCheckList(params) {
    return request({
        url: "/product/mac/processList",
        method: "get",
        params,
    });
}

export function addOaEsopInfo(data) {
    return request({
        url: "/product/oa/addOaEsopInfo",
        method: "post",
        data: data,
    });
}

// 手动送签LuxLink
export function sendLuxLink(oaId) {
    return request({
        url: "/product/oa/sendLuxLink/" + oaId,
        method: "get",
    });
}

// 查获取有OA签核权限的人员信息
export function getOACountersignUserList() {
    return request({
        url: "/product/oa/selectOACountersignUserList",
        method: "get",
    });
}

//通过料号获取SOP模板
export function templateList(params) {
    return request({
        url: "/system/mesUploadTerminalPage/templateList",
        method: "get",
        params,
    });
}

// 签核单据/退回单据
export function updateEsop(data) {
    return request({
        url: "/esopApi/UpdateEsop",
        method: "post",
        data,
    });
}

// 查询用户上传的签核单
export function uploadSopList(params) {
    return request({
        url: "/esopApi/UploadSopList",
        method: "get",
        params,
    });
}

//根据线别获取会签人员信息
export function getUserListByLineId(id) {
    return request({
        url: "/product/line/getUserListByLineId/" + id,
        method: "get",
    });
}
