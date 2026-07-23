import request from "@/utils/request";

// 查询sop配置列表
export function listSopConfig(query) {
    return request({
        url: "/product/sopConfig/list",
        method: "get",
        params: query,
    });
}

// 查询sop配置详细
export function getSopConfig(id) {
    return request({
        url: "/product/sopConfig/" + id,
        method: "get",
    });
}

// 新增sop配置
export function addSopConfig(data) {
    return request({
        url: "/product/sopConfig",
        method: "post",
        data: data,
    });
}

// 修改sop配置
export function updateSopConfig(data) {
    return request({
        url: "/product/sopConfig",
        method: "put",
        data: data,
    });
}

// 删除sop配置
export function delSopConfig(id) {
    return request({
        url: "/product/sopConfig/" + id,
        method: "delete",
    });
}

export function getSopConfigHt(modelId, lineId, stageId, processId) {
    const data = {
        modelId,
        lineId,
        stageId,
        processId,
    };
    return request({
        url: "/product/sopConfig/getSopConfigHt",
        method: "put",
        data: data,
    });
}

export function viewSopInfo(filePath) {
    const data = {
        filePath,
    };
    return request({
        url: "/product/sopConfig/view/",
        method: "post",
        data: data,
    });
}

//获取当前绑定SOP
export function selectMesSopGroupBySopGroupId(id) {
    return request({
        url: "/product/sopConfig/selectMesSopGroupBySopGroupId/" + id,
        method: "get",
    });
}

//获取工站SOP历史记录
export function getSignedSopList(params) {
    return request({
        url: "/product/sopConfig/getSignedSopList",
        method: "get",
        params,
    });
}

//根据工站配置sop
export function editSopConfig(data) {
    return request({
        url: "/product/sopConfig/editSopConfig",
        method: "post",
        data,
    });
}

//根据料号查询使用过的SOP
export function selectTemplateInfoByPartNo(params) {
    return request({
        url: "/system/mesUploadTerminalPage/templateInfo",
        method: "get",
        params,
    });
}

//根据料号更新SOP
export function updateSignedSopList(data) {
    return request({
        url: "/product/sopConfig/updateSignedSopList",
        method: "post",
        data,
    });
}

//获取线体下所有的工站
export function getTerminalListByLineId(params) {
    return request({
        url: "/product/line/getTerminalListByLineId/",
        method: "get",
        params,
    });
}

// 获取sop页码
export function getSopPage(params) {
    return request({
        url: "/product/sopConfig/getSopPage",
        method: "get",
        params,
    });
}
