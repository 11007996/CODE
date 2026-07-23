// 使用 IIFE（立即执行函数）创建私有作用域，模拟“模块”
var userStore = (function () {

    // 私有变量：内存缓存用户信息
    var user = null;
    // 私有方法：从服务器获取用户信息
    function getUserInfoFromServer(callback, errorCallback) {
         api.getLoginUserInfo().then((res) => {
            if (res.code == 200) {
                user = res.data;
                callback(res.data)
            }
        }).catch((e) => {
            if (errorCallback) errorCallback(e);
        })
    }

    // 公共方法（暴露给外部）
    return {

        // 获取用户信息：优先内存 → localStorage → 请求服务器
        getUser: function (callback, errorCallback) {
            if (user) {
                callback(user);
                return;
            }

            // 从服务器获取
            getUserInfoFromServer(callback, errorCallback);
        },

        // 登录后设置用户信息
        setUser: function (userData) {
            user = userData;
        },

        // 登出时清除
        clearUser: function () {
            user = null;
        },

        // 判断是否已登录
        isLoggedIn: function () {
            return !!user;
        }
    };

})();