"use strict";
const common_vendor = require("../../common/vendor.js");
const common_api = require("../../common/api.js");
const common_dateUtil = require("../../common/dateUtil.js");
const common_assets = require("../../common/assets.js");
require("../../common/http.js");
const _sfc_main = {
  data() {
    let now = /* @__PURE__ */ new Date();
    let year = common_dateUtil.dateUtil.format(now, "yyyy");
    let month = common_dateUtil.dateUtil.format(now, "M");
    let maxDays = common_dateUtil.dateUtil.getMaxDaysInMonth(year, month);
    let days = [];
    for (let i = 0; i < maxDays; i++) {
      days.push(i + 1);
    }
    let dayIndex = now.getDate() - 1;
    let weeks = [];
    let firstWeekNum = common_dateUtil.dateUtil.getWeekNumber(year + "/" + month + "/1");
    let currWeekNum = common_dateUtil.dateUtil.getWeekNumber(year + "/" + month + "/" + now.getDate());
    let maxWeekNum = common_dateUtil.dateUtil.getWeekNumber(year + "/" + month + "/" + maxDays);
    let weekIndex = currWeekNum - firstWeekNum;
    for (let i = 0; i <= maxWeekNum - firstWeekNum; i++) {
      weeks.push(i + 1);
    }
    return {
      info: {
        assetNo: "",
        assetName: "",
        assetClass: "",
        model: "",
        entryDate: "",
        costCenter: "",
        durableYear: "",
        durableMonth: "",
        madeFactory: "",
        fileInfo: {}
      },
      fileClassText: ["", "操作手册", "保养周期表", "作业标准书"],
      year,
      month,
      operateItem: [{
        "timeMark": "D",
        "timeMarkName": "日",
        "timeMarkValues": days,
        "index": dayIndex
      }, {
        "timeMark": "W",
        "timeMarkName": "周",
        "timeMarkValues": weeks,
        "index": weekIndex
      }, {
        "timeMark": "M",
        "timeMarkName": "月",
        "timeMarkValues": [month],
        "index": 0
      }],
      userRight: ""
    };
  },
  onLoad(options) {
    this.info.assetNo = options.assetNo;
    this.getAssetInfo();
    let userInfo = common_vendor.index.getStorageSync("userInfo");
    if (userInfo) {
      this.userRight = userInfo.userRight;
    }
  },
  methods: {
    //获取资产详情
    getAssetInfo: function() {
      common_api.assetInfoApi({
        "assetNo": this.info.assetNo
      }).then((res) => {
        this.info = res;
      });
    },
    //picker修改事件
    bindPickerChange: function(e) {
      let itemIndex = e.currentTarget.dataset.itemIndex;
      this.operateItem[itemIndex].index = e.detail.value;
    },
    //打开保养操作页面
    maintenanceOperate: function(index) {
      let timeMark = this.operateItem[index].timeMark;
      let valIndex = this.operateItem[index].index;
      let timeMarkValue = this.operateItem[index].timeMarkValues[valIndex];
      let param = "assetNo=" + this.info.assetNo + "&year=" + this.year + "&month=" + this.month + "&timeMark=" + timeMark + "&timeMarkValue=" + timeMarkValue;
      common_vendor.index.navigateTo({
        url: "/pages/asset/maintenance?" + param
      });
    },
    //预览电子档图片
    openPreview: function(index) {
      let fileInfo = this.info.fileInfo[index];
      let param = `fileId=${fileInfo.fileId}&fileAliasName=${fileInfo.fileAliasName}`;
      common_vendor.index.navigateTo({
        url: "/pages/asset/showImg?" + param
      });
    }
  }
};
function _sfc_render(_ctx, _cache, $props, $setup, $data, $options) {
  return {
    a: common_vendor.t($data.info.assetNo),
    b: common_vendor.t($data.info.assetName),
    c: common_vendor.t($data.info.assetClass),
    d: common_vendor.t($data.info.model),
    e: common_vendor.t($data.info.entryDate.substring(0, 10)),
    f: common_vendor.t($data.info.costCenter),
    g: common_vendor.t($data.info.durableYear),
    h: common_vendor.t($data.info.durableMonth),
    i: common_vendor.t($data.info.madeFactory),
    j: common_vendor.t($data.year + "-" + $data.month),
    k: common_vendor.f($data.operateItem, (item, index, i0) => {
      return {
        a: common_vendor.t(item.timeMarkName),
        b: common_vendor.t(item.timeMarkValues[item.index]),
        c: common_vendor.t(item.timeMarkName),
        d: index,
        e: item.index,
        f: item.timeMarkValues,
        g: common_vendor.o(($event) => $options.maintenanceOperate(index))
      };
    }),
    l: $data.userRight != "A",
    m: common_vendor.o((...args) => $options.bindPickerChange && $options.bindPickerChange(...args)),
    n: common_assets._imports_0$1,
    o: common_vendor.f($data.info.fileInfo, (fileItem, index, i0) => {
      return {
        a: common_vendor.t($data.fileClassText[fileItem.fileClass]),
        b: common_vendor.o(($event) => $options.openPreview(index))
      };
    })
  };
}
const MiniProgramPage = /* @__PURE__ */ common_vendor._export_sfc(_sfc_main, [["render", _sfc_render], ["__scopeId", "data-v-3513d99f"], ["__file", "E:/Work Projects/设备系统/SBGL/Applet/Asset_UI/pages/asset/detail.vue"]]);
wx.createPage(MiniProgramPage);
