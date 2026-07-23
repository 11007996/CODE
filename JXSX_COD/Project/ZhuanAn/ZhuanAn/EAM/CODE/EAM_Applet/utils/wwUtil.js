const CORPID = 'wxaf0083812bd83ebd'; //企业ID
const AGENTID = '1000136';
//企业微信相关
var wwUtil = {
	//获取用户授权Code
	getWxAuthCode: function(url) {
		let REDIRECT_URI = encodeURIComponent(url);
		let STATE = 'wechat'; //其他参数
		let wxurl =
			`https://open.weixin.qq.com/connect/oauth2/authorize?appid=${CORPID}&redirect_uri=${REDIRECT_URI}&response_type=code&scope=snsapi_base&state=${STATE}&agentid=${AGENTID}#wechat_redirect`;
		//重定向到授权页面
		window.location.replace(wxurl);
	},
	//判断是否是微信环境
	isInWechat: function() {
		let ua = navigator.userAgent.toLowerCase();
		let r = false;
		//micromessenger
		if (ua.match(/MicroMessenger/i) == "micromessenger") {
			//在微信中
			r = true
		}
		return r;
	}
}

export default wwUtil