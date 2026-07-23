"use strict";
const common_vendor = require("./vendor.js");
const common_http = require("./http.js");
function userLoginApi(data) {
  return common_http.request({
    url: "/Home/Login",
    method: "POST",
    data
  });
}
function assetListApi(data) {
  return common_http.request({
    url: "/Asset/AssetList",
    method: "POST",
    data
  });
}
function assetInfoApi(data) {
  return common_http.request({
    url: "/Asset/AssetInfo",
    method: "POST",
    data
  });
}
function maintenanceItemsApi(data) {
  return common_http.request({
    url: "/Asset/MaintenanceItems",
    method: "POST",
    data
  });
}
function updateMaintenanceApi(data) {
  return common_http.request({
    url: "/Asset/UpdateMaintenance",
    method: "POST",
    data
  });
}
function downloadFile(data) {
  let options = data;
  {
    common_vendor.index.showLoading({
      title: "加载中...",
      mask: true
    });
  }
  options.url = common_http.apiHost + options.url;
  if (common_vendor.index.getStorageSync("token")) {
    options.header = {
      "Authorization": common_vendor.index.getStorageSync("token")
      // 这里是token(可自行修改)
    };
  }
  options.method = "POST";
  options.filePath = "/Temp File/";
  return new Promise((resolved, rejected) => {
    options.success = (res) => {
      if (res.statusCode !== 200) {
        common_vendor.index.showToast({
          icon: "none",
          duration: 3e3,
          title: "文件下载失败"
        });
        rejected(res);
      } else {
        resolved(res.tempFilePath);
      }
    };
    options.fail = (err) => {
      common_vendor.index.showToast({
        icon: "none",
        duration: 3e3,
        title: "服务器错误,请稍后再试"
      });
      rejected(err);
    };
    options.complete = () => {
      common_vendor.index.hideLoading();
    };
    common_vendor.index.downloadFile(options);
  });
}
exports.assetInfoApi = assetInfoApi;
exports.assetListApi = assetListApi;
exports.downloadFile = downloadFile;
exports.maintenanceItemsApi = maintenanceItemsApi;
exports.updateMaintenanceApi = updateMaintenanceApi;
exports.userLoginApi = userLoginApi;
