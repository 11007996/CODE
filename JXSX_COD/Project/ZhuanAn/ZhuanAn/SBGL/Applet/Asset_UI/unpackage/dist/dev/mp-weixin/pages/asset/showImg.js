"use strict";
const common_vendor = require("../../common/vendor.js");
const common_api = require("../../common/api.js");
require("../../common/http.js");
var viewer;
const _sfc_main = {
  data() {
    return {
      imgArr: [
        { fileId: "", fileAliasName: "", url: "" }
      ]
    };
  },
  // 页面周期函数--监听页面加载
  onLoad(option) {
    this.imgArr[0].fileId = option.fileId;
    this.imgArr[0].fileAliasName = option.fileAliasName;
    this.downloadPreview();
  },
  mounted() {
    const ViewerDom = document.getElementById("img-container");
    viewer = new common_vendor.Viewer(ViewerDom, {});
  },
  methods: {
    //下载预览图片到本地
    downloadPreview: function() {
      common_api.downloadFile({
        url: `/File/DownloadPreview?fileId=${this.imgArr[0].fileId}&previewType=1`
      }).then((res) => {
        this.imgArr[0].url = res;
      });
    },
    //解决异步加载图片，导致viewer无法预览的总题。更新viewer的状态
    viewerUpdate: function() {
      viewer.update();
    }
  }
};
function _sfc_render(_ctx, _cache, $props, $setup, $data, $options) {
  return {
    a: common_vendor.f($data.imgArr, (item, index, i0) => {
      return {
        a: item.url,
        b: item.fileAliasName,
        c: common_vendor.o((...args) => $options.viewerUpdate && $options.viewerUpdate(...args), index),
        d: index
      };
    })
  };
}
const MiniProgramPage = /* @__PURE__ */ common_vendor._export_sfc(_sfc_main, [["render", _sfc_render], ["__file", "E:/Work Projects/设备系统/SBGL/Applet/Asset_UI/pages/asset/showImg.vue"]]);
wx.createPage(MiniProgramPage);
