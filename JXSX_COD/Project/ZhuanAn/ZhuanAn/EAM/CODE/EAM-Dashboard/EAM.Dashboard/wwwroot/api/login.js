// 确保全局api对象存在
window.api = window.api || {};

(function () {
    /**
     * 获取RSA加密公钥
     * @returns
     */
    api.getRSAPublicKey = function () {
        return request({
            url: '/RSAPublicKey',
            type: "GET",
        });
    }

    /**
     * 登入
     * @param {any} data
     * @returns
     */
    api.login = function (data) {
        return request({
            url: '/login',
            type: "POST",
            data: data,
        });
    }

    /**
     * 登出
     * @returns
     */
    api.logout = function () {
        return request({
            url: '/logout',
            type: "POST",
        });
    }
})();