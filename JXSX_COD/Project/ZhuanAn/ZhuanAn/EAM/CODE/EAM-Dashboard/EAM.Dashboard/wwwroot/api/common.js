// 确保全局api对象存在
window.api = window.api || {};
(function () {

    /**
     * 下载文件
     * @param fileId 文件id
     * @returns
     */
    api.downloadFile = function (fileId) {
        return downFile(fileId, "/api/common/downloadFile?fileId=" + fileId, null, { type: "GET" });
    }
})();