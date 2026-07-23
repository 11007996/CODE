import { defineStore } from 'pinia'
import { getToken, setToken, removeToken } from '@/utils/auth.js'
import { login, logout, oaUserRegister, getUserInfo, switchFactory, wxLogin } from '@/api/system/user.js'
import md5 from 'crypto-js/md5'
import { cusEncrypt } from '@/utils/jsencrypt'

export const useUserStore = defineStore('user', {
	state: () => ({
		loginType: 1,
		userInfo: '',
		token: getToken(),
		name: '',
		avatar: '',
		roles: [],
		permissions: [],
		userId: 0,
		authSource: '',
		userName: '',
		clientId: '',
		factoryId: '', //当前登入厂区ID
		factoryName: '', //当前登入厂区名称
		isLogin: false //是否获取用户信息完成
	}),
	actions: {
		setAuthSource(source) {
			this.authSource = source
		},
		// 账号密码登录
		login(userInfo) {
			const username = userInfo.username.trim()
			const password = userInfo.useOaAccount ? cusEncrypt(userInfo.password, userInfo
				.serverRsaPublicKey) : md5(userInfo.password).toString()
			const code = userInfo.code //验证码
			const uuid = userInfo.uuid
			const clientId = this.clientId
			const factoryId = userInfo.factoryId
			const useOaAccount = userInfo.useOaAccount
			const wxCode = sessionStorage.getItem('wxCode');

			return new Promise((resolve, reject) => {
				login(username, password, code, uuid, clientId, factoryId, useOaAccount, wxCode)
					.then((res) => {
						if (res.code == 200) {
							setToken(res.data)
							this.token = res.data
							resolve() //then处理
						} else {
							console.log('login error ', res)
							reject(res) //catch处理
						}
					})
					.catch((error) => {
						reject(error)
					})
			})
		},
		// 微信code登录
		wxLogin(code) {
			const factoryId = uni.getStorageSync('factoryId')

			return new Promise((resolve, reject) => {
				wxLogin(code, factoryId)
					.then((res) => {
						if (res.code == 200) {
							setToken(res.data)
							this.token = res.data
							resolve() //then处理
						} else {
							console.log('login error ', res)
							reject(res) //catch处理
						}
					})
					.catch((error) => {
						reject(error)
					})

			})
		},
		//OA用户注册
		oaRegister(userInfo) {
			const username = userInfo.username.trim()
			const password = cusEncrypt(userInfo.password, userInfo.serverRsaPublicKey)
			const wxCode = userInfo.wxCode

			return new Promise((resolve, reject) => {
				oaUserRegister(username, password, wxCode)
					.then((res) => {
						if (res.code == 200) {
							resolve() //then处理
						} else {
							console.log('register error ', res)
							reject(res) //catch处理
						}
					})
					.catch((error) => {
						reject(error)
					})
			})
		},
		// 获取用户信息
		getInfo() {
			return new Promise((resolve, reject) => {
				getUserInfo()
					.then((res) => {

						const data = res.data
						const avatar = data.user.avatar == '' ? defAva : data.user.avatar

						if (data.roles && data.roles.length > 0) {
							// 验证返回的roles是否是一个非空数组
							this.roles = data.roles
							this.permissions = data.permissions
						} else {
							this.roles = ['ROLE_DEFAULT']
						}
						//用户信息
						this.name = data.user.nickName
						this.avatar = avatar
						this.userInfo = data.user
						this.userId = data.user.userId
						this.userName = data.user.userName
						//登入厂区
						this.factoryId = data.factory.factoryId
						this.factoryName = data.factory.factoryName
						uni.setStorageSync("factoryId", data.factory.factoryId)
						this.isLogin = true
						resolve(res)
					})
					.catch((error) => {
						console.warn(error)
						reject('获取用户信息失败')
					})
			})
		},
		// 退出系统
		logOut() {
			return new Promise((resolve, reject) => {
				logout(this.token)
					.then((res) => {

						this.userInfo = ''
						this.token = ''
						this.name = ''
						this.avatar = ''
						this.roles = []
						this.permissions = []
						this.userId = 0
						this.authSource = ''
						this.userName = ''
						this.clientId = ''
						this.factoryId = '' //当前登入厂区ID
						this.factoryName = '' //当前登入厂区名称
						this.isLogin = false //是否获取用户信息完成	

						removeToken()
						//useTagsViewStore().visitedViews = []
						resolve(res)
					})
					.catch((error) => {
						reject(error)
					})
			})
		},
		//切换登入厂区
		switchFacory(factoryId) {
			return new Promise((resolve, reject) => {
				switchFactory(factoryId)
					.then((res) => {
						if (res.code == 200) {
							setToken(res.data)
							this.token = res.data
							resolve() //then处理
						} else {
							console.log('login error ', res)
							reject(res) //catch处理
						}
					})
					.catch((error) => {
						reject(error)
					})
			})
		}
	}
})

export default useUserStore