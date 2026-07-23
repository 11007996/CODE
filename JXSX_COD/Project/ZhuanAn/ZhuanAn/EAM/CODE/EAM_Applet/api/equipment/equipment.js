import request from '@/utils/request.js'

//查询设备列表
export function listEquipment(data) {
	return request({
		url: 'equipment/equipmentBase/list',
		method: 'GET',
		data
	});
}

//设备详情
export function getEquipment(id) {
	return request({
		url: 'equipment/equipmentBase/' + id,
		method: 'GET',
	});
}

//资产的保养项目
export function getMaintainDetail(data) {
	return request({
		url: "equipment/maintainRecord/detail",
		method: 'GET',
		data
	});
}

//添加保养记录
export function addMaintainRecord(data) {
	return request({
		url: "equipment/maintainRecord",
		method: 'POST',
		data
	});
}

//更新保养记录
export function updateMaintainRecord(data) {
	return request({
		url: "equipment/maintainRecord",
		method: 'PUT',
		data
	});
}

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