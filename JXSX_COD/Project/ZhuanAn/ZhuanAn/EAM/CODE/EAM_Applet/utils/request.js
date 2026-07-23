import { getToken, removeToken } from '@/utils/auth.js'
import { ElMessageBox, ElMessage, ElLoading } from 'element-plus'

//根据环境配置接口的Host
const apiHost = process.env.NODE_ENV === 'development' ? 'http://localhost:8091/' :
	'http://sbgl.luxshare-ict.com:8090/';

const request = function(options = {}) {
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
	if (getToken()) {
		options.header = {
			'Authorization': "Bearer " + getToken() // 这里是token(可自行修改)
		};
	}

	return new Promise((resolved, rejected) => {

		options.success = (res) => {
			// 如果请求回来的状态码不是200则执行以下操作
			if (res.statusCode !== 200) {
				// 认证失败
				if (res.statusCode == 401) {
					// 清除本地token
					removeToken()
					uni.navigateTo({
						url: "/pages/user/login"
					});
				} else if (res.statusCode == 403) {
					//没有权限
					ElMessage({
						message: '无权限，请联系管理员',
						type: 'error',
						duration: 3000,
						showClose: true,
						grouping: true
					});
				} else {
					// 非成功状态码弹窗
					if (!hideMsg) {
						ElMessage({
							message: res.data.msgInfo,
							type: 'error',
							duration: 3000,
							showClose: true,
							grouping: true
						});
					}
				}
				rejected(res)
			} else {

				if (res.data.code == 200)
					resolved(res.data)
				// 请求回来的状态码为200则返回内容
				else if (res.data.code == 401) {
					// 清除本地token
					removeToken()
					uni.navigateTo({
						url: "/pages/user/login"
					});
				} else {
					ElMessage({
						message: res.data.msg,
						type: 'error',
						duration: 3000,
						showClose: true,
						grouping: true
					});
				}
			}
		};
		options.fail = (err) => {
			// 请求失败弹窗
			ElMessage({
				message: '服务器错误,请稍后再试',
				type: 'error',
				duration: 3000,
				showClose: true,
				grouping: true
			});
			rejected(err);
		};
		options.complete = () => {
			if (!hideLoading) uni.hideLoading()
		}
		uni.request(options);
	});
};

export default request