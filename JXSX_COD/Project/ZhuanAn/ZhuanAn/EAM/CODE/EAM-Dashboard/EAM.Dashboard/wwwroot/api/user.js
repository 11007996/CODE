// 确保全局api对象存在
window.api = window.api || {};
(function () {
    /**
     * 获取当前登入用户的信息
     * @returns
     */
    api.getLoginUserInfo = function () {
        return request({
            url: '/getInfo',
            type: 'GET',
        });
    }

    /**
     * 查询用户可用厂区
     * @returns
     */
    api.getUserFactorys = function () {
        return request({
            url: '/system/user/factorys',
            method: 'GET'
        })
    }

    /**
     * 切换登入的厂区
     * @param {any} factoryId
     * @returns
     */
    api.switchLoginFactory = function (factoryId) {
        return request({
            url: 'SwitchFactory/' + factoryId,
            type: 'POST'
        });
    }
})();