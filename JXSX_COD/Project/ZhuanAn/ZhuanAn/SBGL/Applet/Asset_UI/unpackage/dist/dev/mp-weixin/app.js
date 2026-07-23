"use strict";
Object.defineProperty(exports, Symbol.toStringTag, { value: "Module" });
const common_vendor = require("./common/vendor.js");
if (!Math) {
  "./pages/index/index.js";
  "./pages/asset/detail.js";
  "./pages/asset/showImg.js";
  "./pages/asset/maintenance.js";
  "./pages/user/login.js";
  "./pages/error/wxopen.js";
}
const _sfc_main = {
  globalData: {},
  onLaunch: function() {
    {
      console.log("开发环境");
    }
  },
  onShow: function() {
  },
  onHide: function() {
  }
};
const App = /* @__PURE__ */ common_vendor._export_sfc(_sfc_main, [["__file", "E:/Work Projects/设备系统/SBGL/Applet/Asset_UI/App.vue"]]);
function createApp() {
  const app = common_vendor.createSSRApp(App);
  return {
    app
  };
}
createApp().app.mount("#app");
exports.createApp = createApp;
