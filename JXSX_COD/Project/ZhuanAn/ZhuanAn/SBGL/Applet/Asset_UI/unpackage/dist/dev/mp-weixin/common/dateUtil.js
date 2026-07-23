"use strict";
var dateUtil = {
  format: function(date, fmt) {
    var o = {
      "M+": date.getMonth() + 1,
      //月份 
      "d+": date.getDate(),
      //日 
      "h+": date.getHours(),
      //小时 
      "m+": date.getMinutes(),
      //分 
      "s+": date.getSeconds(),
      //秒 
      "q+": Math.floor((date.getMonth() + 3) / 3),
      //季度 
      "S": date.getMilliseconds()
      //毫秒 
    };
    if (/(y+)/.test(fmt)) {
      fmt = fmt.replace(RegExp.$1, (date.getFullYear() + "").substr(4 - RegExp.$1.length));
    }
    for (var k in o) {
      if (new RegExp("(" + k + ")").test(fmt)) {
        fmt = fmt.replace(RegExp.$1, RegExp.$1.length == 1 ? o[k] : ("00" + o[k]).substr(("" + o[k]).length));
      }
    }
    return fmt;
  },
  //获取指定日期是当月的第几周
  getMonthWeek: function(date) {
    var w = date.getDay();
    var d = date.getDate();
    if (w == 0) {
      w = 7;
    }
    var config = {
      getMonth: date.getMonth() + 1,
      getYear: date.getFullYear(),
      getWeek: Math.ceil((d + 6 - w) / 7)
    };
    return config;
  },
  //获取日期在年中的第几周
  getWeekNumber: function(date) {
    var targetDate = new Date(date);
    var firstDayOfYear = new Date(targetDate.getFullYear(), 0, 1);
    var firstWeekTag = 0;
    while (firstDayOfYear.getDay() !== 1) {
      firstDayOfYear.setDate(firstDayOfYear.getDate() + 1);
      firstWeekTag = 1;
    }
    var diff = targetDate - firstDayOfYear;
    var weekNumber = Math.floor(diff / (7 * 24 * 60 * 60 * 1e3)) + 1 + firstWeekTag;
    return weekNumber;
  },
  //获取月份的最大天数
  getMaxDaysInMonth: function(year, month) {
    var nextMonth = new Date(year, month, 0);
    var maxDays = nextMonth.getDate();
    return maxDays;
  }
};
exports.dateUtil = dateUtil;
