import {
	apiHost,
	request
} from '@/common/http.js'
//--------------------基础------------------
//用户登入
export function userLoginApi(data) {
	return request({
		url: '/Home/Login',
		method: 'POST',
		data
	});
}



//--------------------资产------------------
//资产列表
export function assetListApi(data) {
	return request({
		url: '/Asset/AssetList',
		method: 'POST',
		data
	});
}

//资产详情
export function assetInfoApi(data) {
	return request({
		url: '/Asset/AssetInfo',
		method: 'POST',
		data
	});
}

//资产的保养项目
export function maintenanceItemsApi(data) {
	return request({
		url: "/Asset/MaintenanceItems",
		method: 'POST',
		data

	});
}

//更新保养记录
export function updateMaintenanceApi(data) {
	return request({
		url: "/Asset/UpdateMaintenance",
		method: 'POST',
		data
	});
}

//获取资产文件信息
export function getAssetFileInfo(data) {
	return request({
		url: "/Asset/AssetFileInfo",
		method: 'POST',
		data
	});
}

//获取缩略图文件信息
export function getPreviewFilesApi(data) {
	return request({
		url: "/File/GetPreviewFiles",
		method: 'POST',
		data
	});
}

export function downloadFile(data) {
	let hideLoading = false // 是否显示 loading
	let options = data;
	//显示加载弹窗
	if (!hideLoading) {
		uni.showLoading({
			title: '加载中...',
			mask: true
		});
	}

	//请求设置
	options.url = apiHost + options.url;
	console.log(options.url)
	// 判断本地是否存在token，如果存在则带上请求头
	if (uni.getStorageSync('token')) {
		options.header = {
			'Authorization': uni.getStorageSync('token') // 这里是token(可自行修改)
		};
	}
	options.method = 'POST';
	options.filePath = '/Temp File/';

	return new Promise((resolved, rejected) => {

		options.success = (res) => {
			// 如果请求回来的状态码不是200则执行以下操作
		//	console.log(res)
			if (res.statusCode !== 200) {
				uni.showToast({
					icon: 'none',
					duration: 3000,
					title: '文件下载失败'
				});
				rejected(res)
			} else {
				// 请求回来的状态码为200则返回内容
				resolved(res.tempFilePath)
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
		uni.downloadFile(options);
	});
}