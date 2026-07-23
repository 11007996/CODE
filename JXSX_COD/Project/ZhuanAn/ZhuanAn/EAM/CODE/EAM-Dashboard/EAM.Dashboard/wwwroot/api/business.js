// 确保全局api对象存在
window.api = window.api || {};
(function () {

    /**
     * 获取上线通知单(简)的列表
     * @returns
     */
    api.getSimpleOnlineNoticeTicketList = function () {
        return request({
            url: "/api/business/simpleOnlineNoticeTicket/list",
            type: "GET",
        });
    }

    /**
     * 获取上线通知单(简)的详细信息
     * @param {any} ticketNo
     * @returns
     */
    api.getSimpleOnlineNoticeTicketInfo = function (ticketNo) {
        return request({
            url: "/api/business/simpleOnlineNoticeTicket/"+ticketNo,
            type: "GET",
        });
    }
  
})();