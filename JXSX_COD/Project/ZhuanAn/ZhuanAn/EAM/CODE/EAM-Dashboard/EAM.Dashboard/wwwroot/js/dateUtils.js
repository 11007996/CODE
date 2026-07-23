Date.prototype.format = function (fmt) {
    let o = {
        "M+": this.getMonth() + 1,                 //月份
        "d+": this.getDate(),                    //日
        "h+": this.getHours(),                   //小时
        "m+": this.getMinutes(),                 //分
        "s+": this.getSeconds(),                 //秒
        "q+": Math.floor((this.getMonth() + 3) / 3), //季度
        "S": this.getMilliseconds()             //毫秒
    };
    if (/(y+)/.test(fmt)) {
        fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    }
    for (let k in o) {
        if (new RegExp("(" + k + ")").test(fmt)) {
            fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
        }
    }
    return fmt;
}

//顶部时间
function getTime() {
    let myDate = new Date();
    let myYear = myDate.getFullYear(); //获取完整的年份(4位,1970-????)
    let myMonth = myDate.getMonth() + 1; //获取当前月份(0-11,0代表1月)
    let myToday = myDate.getDate(); //获取当前日(1-31)
    let myDay = myDate.getDay(); //获取当前星期X(0-6,0代表星期天)
    let myHour = myDate.getHours(); //获取当前小时数(0-23)
    let myMinute = myDate.getMinutes(); //获取当前分钟数(0-59)
    let mySecond = myDate.getSeconds(); //获取当前秒数(0-59)
    let week = ['星期日', '星期一', '星期二', '星期三', '星期四', '星期五', '星期六'];
    return nowTime = myYear + '-' + fillZero(myMonth) + '-' + fillZero(myToday) + '&nbsp;&nbsp;' + fillZero(myHour) + ':' + fillZero(myMinute) + ':' + fillZero(mySecond) + '&nbsp;&nbsp;' + week[myDay] + '&nbsp;&nbsp;';
};

function fillZero(str) {
    let realNum;
    if (str < 10) {
        realNum = '0' + str;
    } else {
        realNum = str;
    }
    return realNum;
}