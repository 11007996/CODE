"use strict";
const common_vendor = require("./vendor.js");
const apiHost = "http://localhost:13792";
function request(options = {}) {
  let hideLoading = options.hideLoading || true;
  let hideMsg = options.hideMsg || false;
  if (!hideLoading) {
    common_vendor.index.showLoading({
      title: "加载中...",
      mask: true
    });
  }
  options.url = apiHost + options.url;
  if (common_vendor.index.getStorageSync("token")) {
    options.header = {
      "Authorization": common_vendor.index.getStorageSync("token")
      // 这里是token(可自行修改)
    };
  }
  options.method = "POST";
  return new Promise((resolved, rejected) => {
    options.success = (res) => {
      if (res.statusCode !== 200) {
        if (res.statusCode == 401) {
          common_vendor.index.removeStorageSync("token");
          common_vendor.index.removeStorageSync("userInfo");
          common_vendor.index.showToast({
            icon: "none",
            duration: 3e3,
            title: "登入凭证失效，请重新扫描或打开"
          });
        } else {
          if (!hideMsg) {
            common_vendor.index.showToast({
              icon: "none",
              duration: 3e3,
              title: `${res.data.msgInfo}`
            });
          }
        }
        rejected(res);
      } else {
        if (res.data.msgCode == "0")
          resolved(res.data.data);
        else
          common_vendor.index.showToast({
            icon: "none",
            duration: 2e3,
            title: res.data.msgInfo
          });
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
      if (!hideLoading)
        common_vendor.index.hideLoading();
    };
    common_vendor.index.request(options);
  });
}
exports.apiHost = apiHost;
exports.request = request;
