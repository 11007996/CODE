// 封装通用请求函数
function request(options = {}) {

    const token = getToken()
    const defaults = {
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        timeout: 30000,
        headers: {
            'X-Requested-With': 'XMLHttpRequest',
            // 动态获取 token 示例
            'Authorization': token ? 'Bearer ' + token : ''
        },
        success: function (res) {
            if (res.code === 401) {
                // 未授权，跳转登录
                removeToken()
                window.location.href = '/Account/Login';
            } else if (res.code === 403) {
                msgError('权限不足');
            } else if (res.code != 200) {
                msgError(res.msg);
            }
            if (options.success)
                options.success(res)
        },
        // 统一错误处理
        error: function (xhr, status, err) {
            console.error('请求失败:', options.url, status, err);
            if (xhr.status === 401) {
                // 未授权，跳转登录
                removeToken()
                window.location.href = '/Account/Login';
            } else if (xhr.status === 403) {
                msgError('权限不足');
            } else if (xhr.status >= 500) {
                msgError('服务器内部错误');
            } else {
                msgError('请求失败：' + (xhr.responseJSON?.message || xhr.statusText));
            }
        },
        // 成功回调不做默认处理，由调用方决定
        complete: function (xhr) {
            //响应头提示刷新token,
            let token = xhr.getResponseHeader("X-Refresh-Token")
            if (token) {
                setToken(token);
            }
        }
    };

    // 合并配置
    const settings = $.extend(true, {}, defaults, options);

    // 如果是 POST/PUT 请求且 data 是对象，转为 JSON 字符串
    if (settings.type &&
        typeof settings.data === 'object' &&
        settings.contentType === 'application/json; charset=utf-8') {
        settings.data = JSON.stringify(options.data);
    }
    return $.ajax(settings);
}


/**
 * 通用下载方法
 * @param {*} url 请求地址
 * @param {*} params 请求参数
 * @param {*} config 配置
 * @returns
 */
async function downFile(fileId, url, params, config) {

    return new Promise((resolve, reject) => {
        const token = getToken()
        const defaults = {
            url: url,
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded',  // 动态获取 token 示例
                'Authorization': token ? 'Bearer ' + token : ''
            },
            params: params,
            // 【关键步骤】设置响应类型为 blob，接收二进制文件流
            xhrFields: {
                responseType: 'blob'
            },
            success: function (blob, textStatus, xhr) {
                // 下载成功，将返回的 blob 对象保存到全局变量中
                var contentDisposition = decodeURI(xhr.getResponseHeader('content-disposition'))
                var patt = new RegExp('filename=([^;]+\\.[^\\.;]+);*')
                var result = patt.exec(contentDisposition)
                var fileName = result[1]
                fileName = fileName.replace(/\"/g, '')
                //全局缓存
                window.MultiFileCache.set(fileId, blob)
                resolve(blob)
            },
            error: function (xhr, status, error) {
                console.error('下载失败:', error);
                reject(error)
            }
        }

        // 合并配置
        const settings = $.extend(true, {}, defaults, config);
        $.ajax(settings);
    });
    
}
