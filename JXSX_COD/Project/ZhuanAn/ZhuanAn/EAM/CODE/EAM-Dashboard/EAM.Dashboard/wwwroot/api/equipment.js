window.api = window.api || {};
(function () {
    /**
     * 获取设备的状态个数
     * @returns
     */
    api.getEquipmentStateStat = function () {
        return request({
            url: "/api/equipment/state",
            type: "GET",
        });
    }

    /**
     * 获取设备分布
     * @returns
     */
    api.getEquipmentDistribute = function () {
        return request({
            url: "/api/equipment/distribute",
            type: "GET",
        });
    }

    /**
     * 获取设备运行的记录（每个设备最后上报的6条记录）
     * @returns
     */
    api.getEquipmentRunRecord = function () {
        return request({
            url: "/api/equipment/record",
            type: "GET",
        });
    }

    /**
     * 获取能耗统计
     * @returns
     */
    api.getEquipmentEnertyStat = function () {
        return request({
            url: "/api/equipment/energy",
            type: "GET",
        });
    }

    /**
     * 获取设备当日OEE统计分析
     * @returns
     */
    api.getEquipmentOeeStat = function () {
        return request({
            url: "/api/equipment/oee",
            type: "GET",
        });
    }

    /**
     * 获取设备的运行状态统计
     * @returns
     */
    api.getEquipmentRunState = function () {
        return request({
            url: "/api/equipment/runState",
            type: "GET",
        });
    }

    /**
    * 获取设备的运行状态统计
    * @returns
    */
    api.getWarnEquipment = function () {
        return request({
            url: "/api/equipment/warnEquipment",
            type: "GET",
        });
    }

    /**
     * 获取产线设备产量占比
     * @returns
     */
    api.getEquipmentLineProductRate = function () {
        return request({
            url: "/api/equipment/lineProductRate",
            type: "GET",
        });
    }

    /**
     * 获取产线性能开动率
     * @returns
     */
    api.getEquipmentLinePerformanceRate = function () {
        return request({
            url: "/api/equipment/linePerformanceRate",
            type: "GET",
        });
    }

    /**
    * 设备故障时间排行
    * @returns
    */
    api.getEquipmentFaultTimeStat = function () {
        return request({
            url: "/api/equipment/faultTime",
            type: "GET",
        });
    }

    /**
  * 产线生技组长排行
  * @returns
  */
    api.getStatTopEmp = function () {
        return request({
            url: "/api/equipment/topEmp",
            type: "GET",
        });
    }
})();
