const rootUri = "http://" + document.location.host;

/*大屏*/
//自调用函数
(function () {
    // 1、页面一加载就要知道页面宽度计算
    var setFont = function () {
        // 因为要定义变量可能和别的变量相互冲突，污染，所有用自调用函数
        var html = document.documentElement;// 获取html
        // 获取宽度
        var width = html.clientWidth;

        // 判断
        if (width < 1024) width = 1024
        if (width > 1920) width = 1920
        // 设置html的基准值
        var fontSize = width / 80 + 'px';
        // 设置给html
        html.style.fontSize = fontSize;
    }
    setFont();
    // 2、页面改变的时候也需要设置
    // 尺寸改变事件
    window.onresize = function () {
        setFont();
    }
})();


//-----------------------------------设备状态-------------------------------------------
//刷新设备状态统计
function RefreshMachineStateStat() {
    $.ajax({
        type: "get",
        async: false, //必须设置为false，才能实现data的全局变量赋值
        url: rootUri + "/Machine/MachinesState",
        dataType: "json",
        success: function (result) {
            if (result != null && result instanceof Array) {
                var html = '';
                for (let i = 0; i < 4; i++) {
                    var count = 0;
                    var stateName = "";
                    var color = "";
                    switch (i) {
                        case 0:
                            stateName = "设备总数";
                            color = "blue";
                            break;
                        case 1:
                            stateName = "正常设备";
                            color = "green";
                            break;
                        case 2:
                            stateName = "闲置设备";
                            color = "gray";
                            break;
                        case 3:
                            stateName = "故障设备";
                            color = "red";
                            break;
                    }
                    for (let j = 0; j < result.length; j++) {
                        if (result[j].StateName == stateName) {
                            count = result[j].Count;
                            break;
                        }
                    }
                    html += '<div class="item"><h4>' + count + '</h4><span><i class="icon-dot" style="color: ' + color + '"></i>' + stateName + '</span></div>';
                }
                $("#MachineStateStat").html(html);
            }
        }
    });
}


/*
* -----------------------------------------------------设备上报状态时间--------------------------------------
*/
function RefreshMachineReports() {
    $.ajax({
        type: "get",
        async: false, //必须设置为false，才能实现data的全局变量赋值
        url: rootUri + "/Machine/MachineCurrLastReports",
        dataType: "json",
        success: function (result) {
            if (result != null) {
                machineReportData = result;
                LoadMachineReportPage();
            }
        }
    });
}
//加载分页设备上报记录
var pageNo = 0;
var pageSize = 10;
function LoadMachineReportPage() {
    //刷新主项dom
    let html = '';
    for (let i = pageNo * pageSize; i < machineReportData.length && i < (pageNo + 1) * pageSize; i++) {
        let color = "green";
        if (machineReportData[i].RunState == "1") {
            color = "red";
            if (machineReportData[i].WarnState == "2") {
                color = "yellow";
            }
        }
        html += '<li><span>' + machineReportData[i].Line + "&nbsp;&nbsp;" + machineReportData[i].MachineName + '</span><span>' + machineReportData[i].ProductCount + '<s class="icon-dot" style="color: ' + color + '"></s></span></li>';
    }
    $('#MachineReports').html(html);
}
var machineReportData = [];
//设备:滚动事件
var index = 0;
(function () {
    //鼠标进入事件
    $('.inner').on('mouseenter', '.sup li', function () {
        if (machineReportData.length <= 0) return;
        $(this).addClass('active').siblings().removeClass('active');
        var reportsData = machineReportData[pageNo * pageSize + index].Reports;
        var html = '';
        reportsData.forEach(function (item) {
            let color = "green";
            if (item.RunState == "1") {
                color = "red";
                if (item.WarnState == "2") {
                    color = "yellow";
                }
            }
            html += '<li><span></span>' + '<span>' + item.CreateTime + ' <s class="icon-dot" style="color:' + color + '"></s></span></li>';
        });
        //渲染
        $('.sub').html(html);
    });
    //初始进入事件
    $('.province .sup li').eq(0).mouseenter();
    //定时器
    var timer = setInterval(function () {
        index++;
        if (index % pageSize == 0) {
            //重置当前索引
            index = 0;
            pageNo++;
            if (pageNo * pageSize >= machineReportData.length) {
                pageNo = 0;
            }
            //刷新主项上报
            LoadMachineReportPage();
        }
        $('.sup li').eq(index).mouseenter();
    }, 2000);
})();



/**
*------------------------------------平均OEE---------------------------------------
*/
var avgOEE_Echart;
(function () {
    avgOEE_Echart = echarts.init($('.pie')[0]);
    option = {
        // 控制提示
        tooltip: {
            // 非轴图形，使用item的意思是放到数据对应图形上触发提示
            trigger: 'item',
            // 格式化提示内容：
            // a 代表图表名称 b 代表数据名称 c 代表数据  d代表  当前数据/总数据的比例
            formatter: "{a} <br/>{b} : {c} ({d}%)"
        },
        // 控制图表
        series: [
            {
                // 图表名称
                name: 'OEE',
                // 图表类型
                type: 'pie',
                // 南丁格尔玫瑰图 有两个圆  内圆半径10%  外圆半径70%
                // 百分比基于  图表DOM容器的半径
                radius: ['10%', '70%'],
                // 图表中心位置 left 50%  top 50% 距离图表DOM容器
                center: ['50%', '50%'],
                // 半径模式，另外一种是 area 面积模式
                roseType: 'radius',
                // 数据集 value 数据的值 name 数据的名称
                data: [],
                //文字调整
                label: {
                    fontSize: 15
                },
                //引导线
                labelLine: {
                    length: 8,
                    length2: 10
                }
            }
        ],
        color: ['#006cff', '#ed8884', '#60cda0', '#ff9f7f', '#0096ff', '#9fe6b8', '#32c5e9', '#1d9dff']
    };
    avgOEE_Echart.setOption(option);
})();


// -------------------------------------24小时呼叫统计----------------------------------
function RefreshMachineCallOneDay() {
    $.ajax({
        type: "get",
        async: false, //必须设置为false，才能实现data的全局变量赋值
        url: rootUri + "/Machine/MachineCallOneDay",
        dataType: "json",
        success: function (result) {
            if (result != null) {
                var option = { xAxis: [{ data: result.XData }], series: [{ data: result.YData }] };
                CallOneDay_Echart.setOption(option);
            }
        }
    });
}
var CallOneDay_Echart;
(function () {
    // 中间省略的数据  准备三项
    option = {
        // 工具提示
        tooltip: {
            // 触发类型  经过轴触发axis  经过轴触发item
            trigger: 'item',
            // 轴触发提示才有效
            axisPointer: {
                // 默认为直线，可选为：'line' 线效果 | 'shadow' 阴影效果       
                type: 'shadow'
            }
        },
        // 图表边界控制
        grid: {
            // 距离 上右下左 的距离
            left: '0',
            right: '3%',
            bottom: '3%',
            top: '5%',
            // 大小是否包含文本【类似于boxsizing】
            containLabel: true,
            //显示边框
            show: true,
            //边框颜色
            borderColor: 'rgba(0, 240, 255, 0.3)'
        },
        // 控制x轴
        xAxis: [
            {
                // 使用类目，必须有data属性
                type: 'category',
                // 使用 data 中的数据设为刻度文字
                data: [],
                // 刻度设置
                axisTick: {
                    // true意思：图形在刻度中间
                    // false意思：图形在刻度之间
                    alignWithLabel: false,
                    show: false
                },
                //文字
                axisLabel: {
                    //  X 坐标轴标签相关设置
                    color: '#4c9bfd',
                },
            }
        ],
        // 控制y轴
        yAxis: [
            {
                // 使用数据的值设为刻度文字
                type: 'value',
                axisTick: {
                    // true意思：图形在刻度中间
                    // false意思：图形在刻度之间
                    alignWithLabel: false,
                    show: false
                },
                //文字
                axisLabel: {
                    color: '#4c9bfd'
                },
                splitLine: {
                    lineStyle: {
                        color: 'rgba(0, 240, 255, 0.3)'
                    }
                },
            }
        ],
        // 控制x轴
        series: [
            {
                // series配置
                // 颜色
                itemStyle: {
                    // 提供的工具函数生成渐变颜色
                    color: new echarts.graphic.LinearGradient(
                        // (x1,y2) 点到点 (x2,y2) 之间进行渐变
                        0, 0, 0, 1,
                        [
                            { offset: 0, color: '#00fffb' }, // 0 起始颜色
                            { offset: 1, color: '#0061ce' }  // 1 结束颜色
                        ]
                    )
                },
                // 图表数据名称
                name: '设备实时故障数',
                // 图表类型
                type: 'line',
                //平滑
                smooth: true,
                areaStyle: {
                    color: '#00fffb',
                    opacity: 0.2
                },
                // 数据
                data: []
            }
        ]
    };
    CallOneDay_Echart = echarts.init($('.lineCallStat .bar')[0]);
    CallOneDay_Echart.setOption(option);
})();



// ---------------------------------------设备OEE-------------------------------
var machineOEE_Echart;
(function () {
    // 中间省略的数据  准备三项
    option = {
        // 工具提示
        tooltip: {
            // 触发类型  经过轴触发axis  经过轴触发item
            trigger: 'item',
            // 轴触发提示才有效
            axisPointer: {
                // 默认为直线，可选为：'line' 线效果 | 'shadow' 阴影效果       
                type: 'shadow'
            }
        },
        // 图表边界控制
        grid: {
            // 距离 上右下左 的距离
            left: '0',
            right: '3%',
            bottom: '3%',
            top: '5%',
            // 大小是否包含文本【类似于boxsizing】
            containLabel: true,
            //显示边框
            show: true,
            //边框颜色
            borderColor: 'rgba(0, 240, 255, 0.3)'
        },
        // 控制x轴
        xAxis: [
            {
                // 使用类目，必须有data属性
                type: 'category',
                // 使用 data 中的数据设为刻度文字
                data: [],
                // 刻度设置
                axisTick: {
                    // true意思：图形在刻度中间
                    // false意思：图形在刻度之间
                    alignWithLabel: false,
                    show: false
                },
                //文字
                axisLabel: {
                    color: '#4c9bfd',
                    fontSize: 9,
                    interval: 0,
                    rotate: '60',
                    padding: [0, 0, 0, 0],
                    formatter: function (value)  //X轴的内容
                    {
                        if (value && value.length > 8) {
                            var preStr = value.substring(0, 5);
                            var lastStr = value.substring(value.indexOf('('), value.length);
                            return preStr + '...' + lastStr;
                        }
                        return value;
                        //var ret = ""; //拼接加\n返回的类目项
                        //var max = 2;  //每行显示的文字字数
                        //var val = value.length;  //X轴内容的文字字数
                        //var rowN = Math.ceil(val / max);  //需要换的行数
                        //if(rowN > 1)  //判断 如果字数大于2就换行
                        //{
                        //    for(var i = 0; i<rowN;i++){
                        //        var temp = "";  //每次截取的字符串
                        //        var start = i * max;  //开始截取的位置
                        //        var end = start + max;  //结束截取的位置
                        //        temp = value.substring(start,end)+ "\n";
                        //        ret += temp;  //最终的字符串
                        //    }
                        //    return ret;
                        //}
                        //else {return value}
                    }
                }
            }
        ],
        // 控制y轴
        yAxis: [
            {
                // 使用数据的值设为刻度文字
                type: 'value',
                max: 100,
                axisTick: {
                    // true意思：图形在刻度中间
                    // false意思：图形在刻度之间
                    alignWithLabel: false,
                    show: false
                },
                //文字
                axisLabel: {
                    color: '#4c9bfd'
                },
                splitLine: {
                    lineStyle: {
                        color: 'rgba(0, 240, 255, 0.3)'
                    },
                    show: false
                },
            }
        ],
        // 控制x轴
        series: [
            {
                // series配置
                // 颜色
                itemStyle: {
                    // 提供的工具函数生成渐变颜色
                    color: new echarts.graphic.LinearGradient(
                        // (x1,y2) 点到点 (x2,y2) 之间进行渐变
                        0, 0, 0, 1,
                        [
                            { offset: 0, color: '#00fffb' }, // 0 起始颜色
                            { offset: 1, color: '#0061ce' }  // 1 结束颜色
                        ]
                    )
                },
                label: {
                    show: true, //开启显示数值
                    position: 'top', //数值在上方显示
                    textStyle: {  //数值样式
                        color: '#00fffb',   //字体颜色
                        fontSize: 10,  //字体大小
                    },
                    formatter: function (param) {
                        return Math.trunc(param.value);
                    }
                },
                // 图表数据名称
                name: '设备实时OEE',
                // 图表类型
                type: 'bar',
                // 柱子宽度
                barWidth: '60%',
                // 数据
                data: []
            }
        ]
    };
    machineOEE_Echart = echarts.init($('.machineOEE .bar')[0]);
    machineOEE_Echart.setOption(option);
})();

//-------------------------------------------能耗----------------------------------
//数据源
var energyData = {
    day90: { Hour: '0', Energy: '0' },
    day30: { Hour: '0', Energy: '0' },
    day1: { Hour: '0', Energy: '0' }
}
//刷新
function RefreshMachineEnertyStat() {
    $.ajax({
        type: "get",
        async: false, //必须设置为false，才能实现data的全局变量赋值
        url: rootUri + "/Machine/MachineEnergyStat",
        dataType: "json",
        success: function (result) {
            if (result != null) {
                energyData = result;
            }
        }
    });
}
//事件
(function () {
    //点击事件
    $('.order').on('click', '.filter a', function () {
        //点击之后加类名
        $(this).addClass('active').siblings().removeClass('active');
        // 先获取点击a的 data-key自定义属性
        var key = $(this).attr('data-key');
        //获取自定义属性
        // data{}==>data.shuxing data['shuxing]
        key = energyData[key];//
        $('.order .item h4:eq(0)').text(key.Hour);
        $('.order .item h4:eq(1)').text(key.Energy);
    });
    //定时器
    var index = 0;
    var aclick = $('.order a');
    setInterval(function () {
        index++;
        if (index > 3) {
            index = 0;
        }
        //每san秒调用点击事件
        aclick.eq(index).click();
    }, 3000);
})();

//---------------------------------------------呼叫历史统计（月、周）------------------------
var callData = {
    "month":
    {
        "XData": [],
        "YData": []
    },
    "week": {
        "XData": [],
        "YData": []
    }
};

function RefreshCallStat() {
    $.ajax({
        type: "get",
        async: false, //必须设置为false，才能实现data的全局变量赋值
        url: rootUri + "/Machine/CallStat",
        dataType: "json",
        success: function (result) {
            if (result != null) {
                callData = result;
            }
        }
    });
}

(function () {
    var myechart = echarts.init($('.line')[0]);
    var option = {
        //鼠标提示工具
        tooltip: {
            trigger: 'axis'
        },
        xAxis: {
            // 类目类型                                  
            type: 'category',
            // x轴刻度文字                                  
            data: callData.month.XData,
            axisTick: {
                show: false//去除刻度线
            },
            axisLabel: {
                color: '#4c9bfd'//文本颜色
            },
            axisLine: {
                show: false//去除轴线  
            },
            boundaryGap: false//去除轴内间距
        },
        yAxis: {
            // 数据作为刻度文字                                  
            type: 'value',
            axisTick: {
                show: false//去除刻度线
            },
            axisLabel: {
                color: '#4c9bfd'//文本颜色
            },
            axisLine: {
                show: false//去除轴线  
            },
            boundaryGap: false//去除轴内间距
        },
        //图例组件
        legend: {
            textStyle: {
                color: '#4c9bfd' // 图例文字颜色

            },
            right: '10%'//距离右边10%
        },
        // 设置网格样式
        grid: {
            show: true,// 显示边框
            top: '20%',
            left: '3%',
            right: '4%',
            bottom: '3%',
            borderColor: '#012f4a',// 边框颜色
            containLabel: true // 包含刻度文字在内
        },
        series: [{
            name: '呼叫次数',
            // 数据                                  
            data: callData.month.YData,
            // 图表类型                                  
            type: 'line',
            // 圆滑连接                                  
            smooth: true,
            itemStyle: {
                color: 'red'  // 线颜色
            }
        }]
    };
    myechart.setOption(option);

    //点击效果
    $('.sales ').on('click', '.caption a', function () {
        $(this).addClass('active').siblings('a').removeClass('active');
        //option series   data
        //获取自定义属性值
        var key = $(this).attr('data-type');
        //取出对应的值
        if (key == "month") {
            myechart.setOption({
                xAxis: {
                    data: callData.month.XData
                },
                series: [
                    {
                        data: callData.month.YData
                    }
                ]
            });
        } else if (key == "week") {
            myechart.setOption({
                xAxis: {
                    data: callData.week.XData
                },
                series: [
                    {
                        data: callData.week.YData
                    }
                ]
            });
        }
    });
    //定时器(切换，月、周)
    var index = 0;
    var timer = setInterval(function () {
        index++;
        if (index > 2) {
            index = 0;
        };
        $('.sales .caption a').eq(index).click();
    }, 1000 * 5);
})();

//-----------------------------------------呼叫统计：周月分析----------------------------------------
function RefreshCallPreDayAnalyse() {
    $.ajax({
        type: "get",
        async: false, //必须设置为false，才能实现data的全局变量赋值
        url: rootUri + "/Machine/CallWeekAndMonthCountStat",
        dataType: "json",
        success: function (result) {
            if (result != null) {
                var currWeekCount = result.CurrWeekCount;//本周故障数量
                var preOneWeekCount = result.PreOneWeekCount;//上周故障数
                var preTwoWeekCount = result.PreTwoWeekCount;//上上周故障数
                var preWeekRate = result.PreWeekRate;//上周故障同比增长
                var preMonthRate = result.PreMonthRate;//上月故障同比增长
                var absCount = Math.abs(preOneWeekCount - preTwoWeekCount);
                //var color = preOneWeekCount >= preTwoWeekCount ? 'red' : 'limit';
                var option = {
                    series: [{
                        data: [
                            { value: absCount, itemStyle: { color: '#00e1f2' } },
                            { value: preTwoWeekCount - absCount, itemStyle: { color: '#12274d' } }, // 颜色#12274d
                            { value: preTwoWeekCount, itemStyle: { color: 'transparent' } }// 透明隐藏第三块区域
                        ]
                    }]
                };
                currWeekCall_Echart.setOption(option);

                $("#currWeekCount").text(currWeekCount);
                $("#preMonthRate").html(preMonthRate + '%');
                $("#preWeekRate").text(preWeekRate + '%');
            }
        }
    });
}

//-----------------------------------------设备分布----------------------------------------
function RefreshMachineDistribute() {
    $.ajax({
        type: "get",
        async: false, //必须设置为false，才能实现data的全局变量赋值
        url: rootUri + "/Machine/MachinesDistribute",
        dataType: "json",
        success: function (result) {
            if (result != null && result instanceof Array) {
                var html = '<h3>设备分布</h3>';
                html += '<div class="data">';
                for (let i = 0; i < 4; i++) {
                    if (i % 2 == 0 && i > 0) {
                        html += '</div><div class="data">';
                    }
                    html += '<div class="item"><h4>' + result[i].Count + '</h4><span>' + result[i].PointName + '</span></div>';
                }
                html += " </div>";
                $('#MachineDistribute .inner').html(html);
            }
        }
    });
}

//---------------------------------------昨天故障分析------------------------------------------
var currWeekCall_Echart;
(function () {
    currWeekCall_Echart = echarts.init($('.gauge')[0]);
    var option = {
        series: [
            {
                type: 'pie',
                radius: ['130%', '150%'],  // 放大图形
                center: ['50%', '80%'],    // 往下移动  套住75%文字
                label: {
                    show: false,
                },
                startAngle: 180,
                hoverOffset: 0,  // 鼠标经过不变大
                data: [
                    {
                        value: 100,
                        itemStyle: { // 颜色渐变#00c9e0->#005fc1
                            color: {
                                type: 'linear',
                                x: 0,
                                y: 0,
                                x2: 0,
                                y2: 1,
                                colorStops: [
                                    { offset: 0, color: '#00c9e0' },
                                    { offset: 1, color: '#005fc1' }
                                ]
                            }
                        }
                    },
                    { value: 100, itemStyle: { color: '#12274d' } }, // 颜色#12274d

                    { value: 200, itemStyle: { color: 'transparent' } }// 透明隐藏第三块区域
                ]
            }
        ]
    };
    currWeekCall_Echart.setOption(option);
})();

//---------------------------------设备故障监控------------------------------------------
(function () {
    //事件委托
    $('.monitor').on('click', ' a', function () {
        //点击当前的a 加类名 active  他的兄弟删除类名
        $(this).addClass('active').siblings().removeClass('active');
        //获取一一对应的下标 
        var index = $(this).index();
        //选取content 然后狗日对应下标的 显示   当前的兄弟.content隐藏
        $('.content').eq(index).show().siblings('.content').hide();
    });
    //滚动
    //原理：把marquee下面的子盒子都复制一遍 加入到marquee中
    //      然后动画向上滚动，滚动到一半重新开始滚动
    //因为选取的是两个marquee  所以要遍历
    $('.monitor .marquee').each(function (index, dom) {
        //将每个 的所有子级都复制一遍
        var rows = $(dom).children().clone();
        //再将新的到的加入原来的
        $(dom).append(rows);
    });

})();


//刷新设备相关数据（24小时分析数据）
function RefreshMachineStatOneDay() {
    $.ajax({
        type: "get",
        async: false, //必须设置为false，才能实现data的全局变量赋值
        url: rootUri + "/Machine/MachineStatOneDay",
        dataType: "json",
        success: function (result) {
            if (result != null) {
                //平均OEE
                var option1 = { series: [{ data: result.MachineAvgOEE.Rate }] };
                avgOEE_Echart.setOption(option1);
                $("#AvgOEE").text(result.MachineAvgOEE.OEE + "%");
                $("#AvgOEECount").text(result.MachineAvgOEE.Count);
                //设备OEE
                var option2 = {
                    xAxis: [{ data: result.MachineOEE.XData }],
                    series: [{ data: result.MachineOEE.YData }]
                };
                machineOEE_Echart.setOption(option2);
                //设备实时状态
                var html = "";
                for (var i = 0; i < result.MachineCurrState.length; i++) {
                    var item = result.MachineCurrState[i];
                    html += '<div class="data"><h3><i class="icon-dot" style="color: ' + item.Color + '"></i>' + item.StateName + '</h3><h3>' + item.Count + '</h3><h3>' + item.Rate + '%</h3></div>';
                }
                $("#MachineCurrState").html(html);
                //报警设备
                var html2 = "";
                for (var i = 0; i < result.CurrWarnMachines.length; i++) {
                    var item = result.CurrWarnMachines[i];
                    html2 += '<div class="row"><span class="col">' + item.CreateTime + '</span><span class="col">' + item.Line + "&nbsp;&nbsp;" + item.MachineName + '</span><span class="col">' + item.WarnCode + '</span><span class="icon-dot"></span></div>';
                }
                $("#MachineWarnReports").html(html2);
            }
        }
    });
}


//-------------------------初始化及定时任务------------------------------------------
//定时异步刷新设备状态与设备分布
(function () {

    //初始加载
    RefreshMachineReports();
    RefreshMachineStateStat();
    RefreshMachineDistribute();
    RefreshMachineStatOneDay();
    RefreshMachineCallOneDay();
    RefreshCallStat();
    RefreshCallPreDayAnalyse();
    RefreshMachineEnertyStat();

    ////定时器
    setInterval(function () {
        //上报记录
        RefreshMachineReports();
        //设备状态
        RefreshMachineStateStat();
        //设备分布
        RefreshMachineDistribute();
        //平均OEE,设备OEE、设备状态、最后报警
        RefreshMachineStatOneDay();
        //24小时呼叫次数
        RefreshMachineCallOneDay();
        //月、周呼叫次数统计
        RefreshCallStat();
        //当日分析
        RefreshCallPreDayAnalyse();
        //能耗
        RefreshMachineEnertyStat();
    }, 1000 * 30);

    setInterval(function () {
        $(".header .currTime").text(new Date().format("MM月dd hh:mm:ss"));
    }, 1000);
})();


