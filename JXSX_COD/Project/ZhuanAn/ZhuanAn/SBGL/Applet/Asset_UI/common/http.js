//export const apiHost = 'http://sbgl.luxshare-ict.com:8090';//正式服务器
//export const apiHost ='http://localhost:13792';//本地服务器
//export const apiHost ='http://localhost:5173';//本地UI
//根据环境配置接口的Host
export const apiHost = process.env.NODE_ENV === 'development'?'http://localhost:13792':'http://sbgl.luxshare-ict.com:8090';
export function request(options = {}) {
	let hideLoading = options.hideLoading || true // 是否显示 loading
	let hideMsg = options.hideMsg || false // 是否隐藏错误提示
	//显示加载弹窗
	if (!hideLoading) {
		uni.showLoading({
			title: '加载中...',
			mask: true
		});
	}

	//请求设置
	options.url = apiHost + options.url;
	// 判断本地是否存在token，如果存在则带上请求头
	if (uni.getStorageSync('token')) {
		options.header = {
			'Authorization': uni.getStorageSync('token') // 这里是token(可自行修改)
		};
	}
	options.method = 'POST';

	return new Promise((resolved, rejected) => {

		options.success = (res) => {
			// 如果请求回来的状态码不是200则执行以下操作
			if (res.statusCode !== 200) {
				// 认证失败
				if (res.statusCode == 401) {
					// 清除本地token
					uni.removeStorageSync('token')
					uni.removeStorageSync('userInfo')
					// 关闭所有页面返回到登录页
					uni.showToast({
						icon: 'none',
						duration: 3000,
						title: '登入凭证失效，请重新扫描或打开'
					});
				} else {
					// 非成功状态码弹窗
					if (!hideMsg) {
						uni.showToast({
							icon: 'none',
							duration: 3000,
							title: `${res.data.msgInfo}`
						});
					}
				}
				rejected(res)
			} else {
				// 请求回来的状态码为200则返回内容
				if (res.data.msgCode == '0')
					resolved(res.data.data)
				else
					uni.showToast({
						icon: 'none',
						duration: 2000,
						title: res.data.msgInfo
					});
			}
		};
		options.fail = (err) => {
			// 请求失败弹窗
			uni.showToast({
				icon: 'none',
				duration: 3000,
				title: '服务器错误,请稍后再试'
			});
			rejected(err);
		};
		options.complete = () => {
			if (!hideLoading) uni.hideLoading()
		}
		uni.request(options);
	});
};
