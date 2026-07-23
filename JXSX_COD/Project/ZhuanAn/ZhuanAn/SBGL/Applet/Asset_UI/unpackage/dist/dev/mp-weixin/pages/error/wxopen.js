"use strict";
const common_vendor = require("../../common/vendor.js");
const _sfc_main = {
  data() {
    return {
      msg: ""
    };
  },
  onLoad(options) {
    this.msg = options.msg;
  },
  methods: {}
};
function _sfc_render(_ctx, _cache, $props, $setup, $data, $options) {
  return {
    a: common_vendor.t($data.msg)
  };
}
const MiniProgramPage = /* @__PURE__ */ common_vendor._export_sfc(_sfc_main, [["render", _sfc_render], ["__scopeId", "data-v-d78385f5"], ["__file", "E:/Work Projects/设备系统/SBGL/Applet/Asset_UI/pages/error/wxopen.vue"]]);
wx.createPage(MiniProgramPage);
