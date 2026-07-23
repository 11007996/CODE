import request from '@/utils/request.js'

//获取资产文件信息
export function getAssetFileInfo(data) {
	return request({
		url: "Asset/AssetFileInfo",
		method: 'POST',
		data
	});
}

//获取缩略图文件信息
export function getPreviewFilesApi(data) {
	return request({
		url: "File/GetPreviewFiles",
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
	options.url = options.url;
	// 判断本地是否存在token，如果存在则带上请求头
	if (uni.getStorageSync('token')) {
		options.header = {
			'Authorization': uni.getStorageSync('token') // 这里是token(可自行修改)
		};
	}
	options.method = 'POST';
	options.filePath = 'Temp File/';

	return new Promise((resolved, rejected) => {

		options.success = (res) => {
			// 如果请求回来的状态码不是200则执行以下操作
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