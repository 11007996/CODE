// 确保全局api对象存在
window.api = window.api || {};
(function () {

    /**
     * 获取区域列表
     * @returns
     */
    api.getAreaList = function () {
        return request({
            url: "/api/callFaultBase/area/list",
            type: "GET",
        });
    }

    /**
     * 获取未结案的呼叫故障
     * @param {any} areaId
     * @returns
     */
    api.getUnsolvedCallFault = function (areaId) {
        return request({
            url: "/api/callFaultBase/unsolved/" + areaId,
            type: "GET",
        });
    }

    /**
     * 获取呼叫故障数量统计
     * @param {any} areaId
     * @returns
     */
    api.getCallFaultCountStat = function (areaId) {
        return request({
            url: "/api/callFaultBase/count/" + areaId,
            type: "GET",
        });
    }

    /**
     * 获取24小时内的每个小时的呼叫次数
     * @returns
     */
    api.getCallCountForHour = function () {
        return request({
            url: "/api/callFaultBase/count/hour/0",
            type: "GET",
        });
    }

    /**
     * 获取当周故障统计分析数据
     * @param {any} areaId
     * @returns
     */
    api.getCallFaultWeekStat = function (areaId) {
        return request({
            url: "/api/callFaultBase/week/" + areaId,
            type: "GET",
        });
    }

    /**
     * 获取呼叫定时任务
     * @param {any} areaId
     * @returns
     */
    api.getCallScheduledTask = function (areaId, second) {
        return request({
            url: "/api/callScheduledTask/" + areaId + "?second=" + second,
            type: "GET",
        });
    }
})();