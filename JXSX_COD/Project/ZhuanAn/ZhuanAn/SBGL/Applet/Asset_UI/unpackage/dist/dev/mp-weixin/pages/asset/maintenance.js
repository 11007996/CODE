"use strict";
const common_vendor = require("../../common/vendor.js");
const common_api = require("../../common/api.js");
require("../../common/http.js");
const _sfc_main = {
  data() {
    return {
      assetNo: "",
      year: "",
      month: "",
      timeMarkStamp: "",
      timeMarkValue: "",
      timeMarkName: "",
      items: []
    };
  },
  onLoad(options) {
    let timeMarkName = "日";
    switch (options.timeMark) {
      case "D":
        timeMarkName = "日";
        break;
      case "W":
        timeMarkName = "周";
        break;
      case "M":
        timeMarkName = "月";
        break;
    }
    this.assetNo = options.assetNo;
    this.year = options.year;
    this.month = options.month;
    this.timeMark = options.timeMark;
    this.timeMarkValue = options.timeMarkValue;
    this.timeMarkName = timeMarkName;
    this.getMaintenanceItems();
  },
  methods: {
    //获取资产的保养项目
    getMaintenanceItems: function() {
      common_api.maintenanceItemsApi({
        assetNo: this.assetNo,
        year: this.year,
        month: this.month,
        timeMark: this.timeMark,
        timeMarkValue: this.timeMarkValue
      }).then((res) => {
        this.items = res.items;
        this.timeMarkStamp = res.timeMarkStamp;
      });
    },
    //修改保养项目结果
    changeItemValue: function(e) {
      let index = e.currentTarget.dataset.index;
      let value = e.detail.value ? "V" : "X";
      this.items[index].itemValue = value;
    },
    //保存保养记录
    submit: function() {
      common_api.updateMaintenanceApi({
        AssetNo: this.assetNo,
        Year: this.year,
        TimeMark: this.timeMark,
        TimeMarkStamp: this.timeMarkStamp,
        ItemValueDic: this.items
      }).then((res) => {
        common_vendor.index.showToast({
          icon: "none",
          duration: 2e3,
          title: "保存成功"
        });
      }).catch((err) => {
      });
    }
  }
};
function _sfc_render(_ctx, _cache, $props, $setup, $data, $options) {
  return {
    a: common_vendor.t($data.timeMarkValue),
    b: common_vendor.t($data.timeMarkName),
    c: common_vendor.f($data.items, (item, index, i0) => {
      return common_vendor.e({
        a: common_vendor.t(item.itemName),
        b: item.itemName != "保养人签名"
      }, item.itemName != "保养人签名" ? {
        c: index,
        d: item.itemValue == "V",
        e: common_vendor.o((...args) => $options.changeItemValue && $options.changeItemValue(...args))
      } : {
        f: item.itemValue
      });
    }),
    d: common_vendor.o((...args) => $options.submit && $options.submit(...args))
  };
}
const MiniProgramPage = /* @__PURE__ */ common_vendor._export_sfc(_sfc_main, [["render", _sfc_render], ["__file", "E:/Work Projects/设备系统/SBGL/Applet/Asset_UI/pages/asset/maintenance.vue"]]);
wx.createPage(MiniProgramPage);
