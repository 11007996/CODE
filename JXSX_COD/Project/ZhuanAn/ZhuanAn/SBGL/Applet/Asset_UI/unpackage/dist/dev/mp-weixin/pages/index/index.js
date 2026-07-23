"use strict";
const common_vendor = require("../../common/vendor.js");
const common_api = require("../../common/api.js");
const common_assets = require("../../common/assets.js");
require("../../common/http.js");
const _sfc_main = {
  data() {
    return {
      keywords: "",
      assetList: []
    };
  },
  onLoad(options) {
  },
  methods: {
    //搜索资产信息
    searchAssetList: function() {
      common_api.assetListApi({
        "keywords": this.keywords
      }).then((res) => {
        this.assetList = res;
      });
    },
    //查看资产详情
    getAssetDetail: function(assetNo) {
      common_vendor.index.navigateTo({
        url: "/pages/asset/detail?assetNo=" + assetNo
      });
    },
    //打开摄像头
    scanQRCode: function() {
      let that = this;
      common_vendor.index.scanCode({
        scanType: "qrCode",
        success: function(res) {
          that.getAssetDetail(res.result);
        }
      });
    }
  }
};
function _sfc_render(_ctx, _cache, $props, $setup, $data, $options) {
  return {
    a: $data.keywords,
    b: common_vendor.o(($event) => $data.keywords = $event.detail.value),
    c: common_assets._imports_0,
    d: common_vendor.o((...args) => $options.searchAssetList && $options.searchAssetList(...args)),
    e: common_vendor.t($data.assetList.length),
    f: common_vendor.f($data.assetList, (item, k0, i0) => {
      return {
        a: common_vendor.t(item.assetNo),
        b: common_vendor.t(item.assetName),
        c: common_vendor.o(($event) => $options.getAssetDetail(item.assetNo))
      };
    })
  };
}
const MiniProgramPage = /* @__PURE__ */ common_vendor._export_sfc(_sfc_main, [["render", _sfc_render], ["__scopeId", "data-v-1cf27b2a"], ["__file", "E:/Work Projects/设备系统/SBGL/Applet/Asset_UI/pages/index/index.vue"]]);
wx.createPage(MiniProgramPage);
