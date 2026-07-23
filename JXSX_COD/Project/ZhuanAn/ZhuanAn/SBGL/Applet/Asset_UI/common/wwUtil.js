import {
	apiHost
} from '@/common/http.js'
const CORPID = 'wxaf0083812bd83ebd'; //企业ID
const AGENTID = '1000136'; //应用ID

//企业微信相关
var wwUtil = {
	//获取用户授权Code
	getWxAuthCode: function(url, param) {
		let REDIRECT_URI = encodeURIComponent(apiHost + '/h5?from=wechat' + url) //回调路径
		let STATE = param; //其他参数
		let wxurl =
			`https://open.weixin.qq.com/connect/oauth2/authorize?appid=${CORPID}&redirect_uri=${REDIRECT_URI}&response_type=code&scope=snsapi_base&state=${STATE}&agentid=${AGENTID}#wechat_redirect`;
		//重定向到授权页面
		window.location.replace(wxurl);
	},

	//通过用户code，获取当前用户身份信息
	wwUserLogin: function(code) {
		if (!code) {
			uni.redirectTo({
				url: '/pages/error/wxopen?msg=未获取到授权code'
			});
			return;
		}
		let url = "/Home/WXAuthLogin?code=" + code;
		uni.request({
			url,
			success : (res) => {
				if (res.statusCode == 200) {
					if (res.data.msgCode == '0') {
						uni.setStorageSync("token", res.data.data.token);
						uni.setStorageSync("userInfo", res.data.data.userInfo);
					} else {
						uni.redirectTo({
							url: '/pages/error/wxopen?msg=' + res.data.msgInfo
						});
					}
				} else {
					uni.redirectTo({
						url: '/pages/error/wxopen?msg=求请错误，状态码：' + res.statusCode
					});
				}
			},
			fail: (err) => {
				uni.redirectTo({
					url: '/pages/error/wxopen?msg=请求异常：' + JSON.stringify(err) 
				});
			}
		});
	},
	getUserInfo : function(token) {
		if (token==undefined||token==null||token=='') {
			uni.redirectTo({
				url: '/pages/error/wxopen?msg=未知token:'+token+',无法请求用户信息'
			});
			return;
		}
		let url = "/Home/UserInfo?token=" + token;
		uni.request({
			url,
			success : (res) => {
				if (res.statusCode == 200) {
					if (res.data.msgCode == '0') {
						uni.setStorageSync("userInfo", res.data.data);
					} else {
						uni.redirectTo({
							url: '/pages/error/wxopen?msg=' + res.data.msgInfo
						});
					}
				} else {
					uni.redirectTo({
						url: '/pages/error/wxopen?msg=求请错误，状态码：' + res.statusCode
					});
				}
			},
			fail: (err) => {
				uni.redirectTo({
					url: '/pages/error/wxopen?msg=请求异常：' + JSON.stringify(err) 
				});
			}
		});
	},
}

export default wwUtil