import request from '@/utils/request.js'
//--------------------基础------------------
//用户密码登入
export function login(username, password, code, uuid, clientId, factoryId, useOaAccount, wxCode) {
	const data = {
		username,
		password,
		code,
		uuid,
		clientId,
		factoryId,
		useOaAccount,
		wxCode
	}
	return request({
		url: 'login',
		method: 'POST',
		data: data,
		headers: {
			userName: username
		}
	})
}

//微信code登入
export function wxLogin(code, factoryId) {
	const data = {
		code,
		factoryId,
	}
	return request({
		url: 'wxLogin',
		method: 'POST',
		data: data
	})
}

//OA用户注册
export function oaUserRegister(username, password, wxCode) {
	const data = {
		username,
		password,
		wxCode
	}
	return request({
		url: 'register',
		method: 'POST',
		data: data
	})
}

//登出
export function logout() {
	return request({
		url: 'logout',
		method: 'POST',
	})
}


//获取当前登入用户的信息
export function getUserInfo() {
	return request({
		url: 'getInfo',
		method: 'GET',
	});
}

// 查询用户可用厂区
export function getUserFactorys() {
	return request({
		url: 'system/user/factorys',
		method: 'get'
	})
}

//切换登入厂区
export function switchFactory(factoryId) {
	return request({
		url: 'SwitchFactory/' + factoryId,
		method: 'POST'
	})
}


//获取服务器RSA公钥
export function getRSAPublicKey() {
	return request({
		url: 'RSAPublicKey',
		method: 'get'
	})
}