<script>
	import wwUtil from '@/common/wwUtil.js';
	export default {
		globalData: {

		},
		onLaunch: function() {
			//判断启动环境
			if (process.env.NODE_ENV === 'development') {
				console.log('开发环境')
			} else { //生产环境
				// 获取启动应用的URL参数
				let urlParams = new URLSearchParams(window.location.search);
				// 通过get方法获取指定参数值
				let from = urlParams.get('from');
				//判断来源，如果是企业微信，则通过code登入，否则获取授权code
				let token =uni.getStorageSync('token');
				if (token==undefined||token==null||token=='') {
					if (from && from == 'wechat') {
						let code = urlParams.get('code');
						if (code) {
							wwUtil.wwUserLogin(code);
						} else {
							uni.redirectTo({
								url: '/pages/error/wxopen?msg=未获取到登入授权'
							})
						}
					} else {
						wwUtil.getWxAuthCode(this.$route.href, '');
					}
				} else{
					//判断是否有全局用户信息，没有在后台获取
					var userInfo =uni.getStorageSync('userInfo');
					if (userInfo == undefined || userInfo == null || userInfo == '')
						wwUtil.getUserInfo(token);
				}
			}
		},
		onShow: function() {},
		onHide: function() {}
	}
</script>

<style>
	/*每个页面公共css */
</style>