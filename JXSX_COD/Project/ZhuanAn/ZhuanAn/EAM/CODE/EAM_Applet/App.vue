<script>
	import wwUtil from '@/utils/wwUtil.js';
	import {
		getToken
	} from '@/utils/auth.js'
	import useUserStore from '@/store/modules/user.js'

	export default {
		globalData: {},
		onLaunch: function() {
			console.log('onLaunch')

			let token = getToken()
			if (token) {
				useUserStore().getInfo()
			} else {

				//判断启动环境(是否在微信启动)
				if (!wwUtil.isInWechat()) {
					console.log('浏览器环境')
				} else {
					console.log('微信环境')
					// 获取启动应用的URL参数
					let urlParams = new URLSearchParams(window.location.search);
					//判断是否是为微信code回调
					if (window.location.href.indexOf('state=wechat') >= 0) { //判断来源，如果是企业微信，则通过code登入，否则获取授权code

						//检查hash模式下微信返回的url是否正确,不正确调整,重启启动
						const href = window.location.href; //这个href是源始启动的href
						//alert(href)
						const index1 = href.indexOf('?')
						const index2 = href.indexOf('#')
						let newHref;
						if (index1 < index2) { //路由地址异常，调整
							const host = href.split("?")[0]
							const wxparm = href.substring(index1 + 1, index2)
							const url = href.split("#")[1]
							newHref = url + '&' + wxparm
						}

						let code = urlParams.get('code');
						if (code) {
							sessionStorage.setItem('wxCode', code); //临时缓存code
							//使用code登入
							useUserStore().wxLogin(code).then(res => {
								useUserStore().getInfo().then(res => {
									if (newHref) {
										setTimeout(() => {
											uni.reLaunch({
												url: newHref
											})
										}, 0)
									}
								})
							}).catch((error) => {
								uni.showToast({
									duration: 3000,
									title: error.msg
								})
								uni.switchTab({
									url: '/pages/index'
								})
							});
						}
					} else {
						wwUtil.getWxAuthCode(window.location.href);
					}
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