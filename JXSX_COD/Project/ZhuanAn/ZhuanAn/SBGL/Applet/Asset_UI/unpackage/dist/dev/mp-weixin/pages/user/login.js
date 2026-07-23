"use strict";
const common_vendor = require("../../common/vendor.js");
const common_api = require("../../common/api.js");
const common_assets = require("../../common/assets.js");
require("../../common/http.js");
const _sfc_main = {
  data() {
    return {
      form: {
        workCode: "",
        password: ""
      }
    };
  },
  methods: {
    //登入
    login: function() {
      common_api.userLoginApi(
        this.form
      ).then((res) => {
        common_vendor.index.setStorageSync("token", res.token);
        common_vendor.index.navigateBack({
          delta: 1
        });
      });
    }
  }
};
function _sfc_render(_ctx, _cache, $props, $setup, $data, $options) {
  return {
    a: common_assets._imports_0$2,
    b: $data.form.workCode,
    c: common_vendor.o(($event) => $data.form.workCode = $event.detail.value),
    d: $data.form.password,
    e: common_vendor.o(($event) => $data.form.password = $event.detail.value),
    f: common_vendor.o((...args) => $options.login && $options.login(...args))
  };
}
const MiniProgramPage = /* @__PURE__ */ common_vendor._export_sfc(_sfc_main, [["render", _sfc_render], ["__file", "E:/Work Projects/设备系统/SBGL/Applet/Asset_UI/pages/user/login.vue"]]);
wx.createPage(MiniProgramPage);
