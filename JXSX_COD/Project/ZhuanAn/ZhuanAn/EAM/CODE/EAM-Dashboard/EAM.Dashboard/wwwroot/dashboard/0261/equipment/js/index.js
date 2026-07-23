(function () {
    // 1、页面一加载就要知道页面宽度计算
    let setFont = function () {
        // 因为要定义变量可能和别的变量相互冲突，污染，所有用自调用函数
        let html = document.documentElement;// 获取html
        // 获取宽度
        let width = html.clientWidth;
        // 设置html的基准值
        let fontSize = width / 80 + 'px';
        // 设置给html
        html.style.fontSize = fontSize;
    }
    setFont();

    //-----------------------------------设备状态-------------------------------------------
    //刷新设备状态统计
    function refreshEquipmentStateStat() {
        api.getEquipmentStateStat().then((res) => {
            let result = res.data
            if (result != null && result instanceof Array) {
                let html = '';
                for (let i = 0; i < 4; i++) {
                    let count = 0;
                    let stateName = "";
                    let color = "";
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
                        if (result[j].stateName == stateName) {
                            count = result[j].count;
                            break;
                        }
                    }
                    html += '<div class="item"><h4>' + count + '</h4><span><i class="icon-dot" style="color: ' + color + '"></i>' + stateName + '</span></div>';
                }
                $("#equipmentStateStat").html(html);
            }
        });
    }

    //----------------------------------产能占比--------------------------------------
    let productRateChart;
    function initProductRateChart() {
        productRateChart = echarts.init($('#productRateChart')[0]);
        let option = {
            // 控制提示
            tooltip: {
                trigger: 'axis',
            },
            legend: {
                selectedMode: false,
                itemWidth:15,
                textStyle: {
                    color: '#4c9bfd' // 图例文字颜色
                },
                right: '0',
                top: '0'
            },
            grid: {
                top: '50',
                bottom: 0,
                left: 0,
                right: 0
            },
            yAxis: {
                type: 'value',
                max: 100,
                //文字
                axisLabel: {
                    color: '#4c9bfd'
                },
            },
            xAxis: {
                type: 'category',
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
                            let preStr = value.substring(0, 5);
                            return preStr + '...';
                        }
                        return value;
                    }
                }
            }
        };

        productRateChart.setOption(option);
    }

    function refreshEquipmentProductRate() {
        api.getEquipmentLineProductRate().then((res) => {
            if (res.code == 200) {
                let rawData = res.data.data;
                const totalData = [];
                for (let i = 0; i < rawData[0].length; ++i) {
                    let sum = 0;
                    for (let j = 0; j < rawData.length; ++j) {
                        sum += rawData[j][i];
                    }
                    totalData.push(sum);
                }

                const series = res.data.yData.map((name, sid) => {
                    return {
                        name,
                        type: 'bar',
                        stack: 'total',
                        barWidth: '60%',
                        label: {
                            show: false,
                            formatter: (params) => params.value + '%'
                        },
                        data: rawData[sid].map((d, did) =>
                            totalData[did] <= 0 ? 0 : Math.round(d / totalData[did] * 1000) / 10
                        )
                    };
                });

                productRateChart.setOption({ series: series, xAxis: { data: res.data.xData } });
            }
        });
    }

    /**
    *------------------------------------平均OEE---------------------------------------
    */
    let avgOEEChart;
    function initAvgOEEChart() {
        avgOEEChart = echarts.init($('#avgOEEChart')[0]);
        let option = {
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
                    radius: ['10%', '60%'],
                    // 图表中心位置 left 50%  top 50% 距离图表DOM容器
                    center: ['50%', '50%'],
                    // 半径模式，另外一种是 area 面积模式
                    roseType: 'radius',
                    // 数据集 value 数据的值 name 数据的名称
                    data: [],
                    //文字调整
                    label: {
                        fontSize: 9
                    },
                    //引导线
                    labelLine: {
                        length: 5,
                        length2: 8
                    }
                }
            ],
            color: ['#006cff', '#ed8884', '#60cda0', '#ff9f7f', '#0096ff', '#9fe6b8', '#32c5e9', '#1d9dff']
        };
        avgOEEChart.setOption(option);
    }

    // ------------------------------------- 线别设备性能----------------------------------
    let linePerformanceRateChart;
    function initLinePerformanceRateChart() {
        linePerformanceRateChart = echarts.init($('#linePerformanceRateChart')[0]);
        // 中间省略的数据  准备三项
        let option = {
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
            legend: {
                selectedMode: false,
                textStyle: {
                    color: '#4c9bfd' // 图例文字颜色
                },
                right: '0',
                top: '0'
            },
            // 图表边界控制
            grid: {
                // 距离 上右下左 的距离
                left: '0',
                right: '3%',
                bottom: '3%',
                top: '20%',
                // 大小是否包含文本【类似于boxsizing】
                containLabel: true,
                //显示边框
                show: false,
                //边框颜色
                borderColor: 'rgba(0, 240, 255, 0.3)'
            },
            // 控制x轴
            xAxis: [
                {
                    // 使用类目，必须有data属性
                    type: 'category',
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
                        }
                    },
                }
            ],
            // 控制x轴
            series: [
                {
                    // 图表数据名称
                    name: '产线产能占比',
                    // 图表类型
                    type: 'line',
                    textStyle: {  //数值样式
                        color: 'inherit',   //字体颜色
                        fontSize: 10,  //字体大小
                    },
                    //平滑
                    //smooth: true,
                    //areaStyle: {
                    //    color: '#00fffb',
                    //    opacity: 0.2
                    //},
                }
            ],
            //dimensions: ['name', 'value'],
        };

        linePerformanceRateChart.setOption(option);
    }

    function refreshLinePerformanceRate() {
        api.getEquipmentLinePerformanceRate().then((res) => {
            result = res.data
            if (res.code == 200) {
                let rawData = res.data.data;
                const series = res.data.yData.map((name, sid) => {
                    return {
                        name,
                        type: 'line',
                        label: {
                            show: false,
                            formatter: (params) => params.value + '%',
                            textStyle: {  //数值样式
                                color: 'inherit',   //字体颜色
                                fontSize: 10,  //字体大小
                            },
                        },
                        data: rawData[sid]
                        //data: rawData[sid].map((d, did) =>
                        //    totalData[did] <= 0 ? 0 : Math.round(d / totalData[did] * 1000) / 10
                        //)
                    };
                });

                const xData = res.data.xData.map((it) => {
                    return new Date(it).format('MM-dd');
                })

                linePerformanceRateChart.setOption({ series: series, xAxis: { data: xData } })
            }
        });
    }

    // ---------------------------------------设备OEE-------------------------------
    let equipmentOEEChart;
    function initEquipmentOEEChart() {
        equipmentOEEChart = echarts.init($('#equipmentOEEChart')[0]);
        // 中间省略的数据  准备三项
        let option = {
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
            xAxis: {
                // 使用类目，必须有data属性
                type: 'category',
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
                            let preStr = value.substring(0, 5);
                            let lastStr = value.substring(value.indexOf('('), value.length);
                            return preStr + '...' + lastStr;
                        }
                        return value;
                    }
                }
            },
            // 控制y轴
            yAxis: {
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
            },
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
                            return Math.trunc(param.data.value);
                        }
                    },
                    // 图表数据名称
                    name: '设备实时OEE',
                    // 图表类型
                    type: 'bar',
                    // 柱子宽度
                    barWidth: '60%',
                }
            ],
            dimensions: ['name', 'value'],
        };

        equipmentOEEChart.setOption(option);
    }

    //-------------------------------------------能耗----------------------------------
    //数据源
    let energyData = {
        day90: { hour: '0', energy: '0' },
        day30: { hour: '0', energy: '0' },
        day1: { hour: '0', energy: '0' }
    }

    //事件
    function initEequipmentEnergy() {
        //点击事件
        $('.viewport-right-top').on('click', '.filter a', function () {
            //点击之后加类名
            $(this).addClass('active').siblings().removeClass('active');
            // 先获取点击a的 data-key自定义属性
            let key = $(this).attr('data-key');
            //获取自定义属性
            // data{}==>data.shuxing data['shuxing]
            key = energyData[key];//
            $('.viewport-right-top .item h4:eq(0)').text(key.hour);
            $('.viewport-right-top .item h4:eq(1)').text(key.energy);
        });
        //定时器
        let index = 0;
        let aclick = $('.viewport-right-top a');
        setInterval(function () {
            index++;
            if (index > 3) {
                index = 0;
            }
            //每san秒调用点击事件
            aclick.eq(index).click();
        }, 3000);
    }

    //刷新
    function refreshEquipmentEnertyStat() {
        api.getEquipmentEnertyStat().then((res) => {
            let result = res.data
            if (result != null) {
                energyData = result;
            }
        });
    }

    //-----------------------------------------今日之星----------------------------------------

    function refreshTopEmp() {
        api.getStatTopEmp().then(res => {
            if (res.code == 200) {
                let htmlStr = "";
                for (let i = 1; i <= res.data.length && i <= 3; i++) {
                    htmlStr += `<div class="item"><img src="${res.data[i - 1].avatar}" ><span>${i}</span></div>`;
                }
                $("#empRank").html(htmlStr);
            }
        })
    }

    //---------------------------------------------故障时长------------------------
    let faultTimeChart;
    function initFaultTimeChart() {
        faultTimeChart = echarts.init($('#faultTimeChart')[0]);
        let option = {
            //鼠标提示工具
            tooltip: {
                trigger: 'axis'
            },
            // 设置网格样式
            grid: {
                top: '0',
                left: '0',
                right: '0',
                bottom: '0',
            },
            xAxis: {
                // 类目类型
                type: 'category',
                //x轴刻度文字
                axisTick: {
                    show: false//去除刻度线
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
                            let idx = value.indexOf('\n')
                            let preStr = value.substring(0, idx);
                            let lastStr = value.substring(idx, value.length);
                            if (value.length - idx > 8) {
                                lastStr = value.substring(idx, idx + 5)
                            }
                            return preStr + lastStr + '..';
                        }
                        return value;
                    }
                },
                axisLine: {
                    show: true//去除轴线
                }
            },
            yAxis: [{
                name: "时长(min)",
                type: 'value',
                axisTick: {
                    show: false//去除刻度线
                },
                axisLabel: {
                    color: '#4c9bfd',//文本颜色
                    fontSize: 9,
                    show:false
                },
                axisLine: {
                    show: false//去除轴线
                },
                splitLine: {
                    show: false
                }
            }, {
                name: '时间稼动率',
                type: 'value',
                min: 0,
                max: 100,
                axisTick: {
                    show: false//去除刻度线
                },
                axisLabel: {
                    color: '#4c9bfd',//文本颜色
                    fontSize: 9,
                    show: false
                },
                axisLine: {
                    show: false//去除轴线
                },
                splitLine: {
                    show: false
                }
            }]
        };

        faultTimeChart.setOption(option);
    }

    function refreshEquipmentFualtTimeStat() {
        api.getEquipmentFaultTimeStat().then((res) => {
            if (res.code == 200) {
                let xData = res.data.xData
                const series = [{
                    name: res.data.yData[0],
                    type: 'bar',
                    barWidth: '60%',
                    yAxisIndex: 0,
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
                        position: 'inside', //数值在上方显示
                        textStyle: {  //数值样式
                            color: '#051505',   //字体颜色
                            fontSize: 10,  //字体大小
                        },
                        //verticalAlign:''
                    },
                    data: res.data.data[0]
                },
                {
                    name: res.data.yData[1],
                    type: 'line',
                    yAxisIndex: 1,
                    itemStyle: {
                        color: 'red'
                    },
                    label: {
                        show: true, //开启显示数值
                        position: 'top', //数值在上方显示
                        formatter: (params) => params.value.toFixed(1) + '%',
                        textStyle: {  //数值样式
                            color: 'red',   //字体颜色
                            fontSize: 10,  //字体大小
                        },
                    },
                    data: res.data.data[1]
                }];
                faultTimeChart.setOption({ xAxis: { data: xData }, series: series });
            }
        });
    }

    //---------------------------------设备故障监控------------------------------------------
    function initEquipmentWarn() {
        //事件委托
        $('.viewport-right-bottom').on('click', ' a', function () {
            //点击当前的a 加类名 active  他的兄弟删除类名
            $(this).addClass('active').siblings().removeClass('active');
            //获取一一对应的下标
            let index = $(this).index();
            //选取content 然后狗日对应下标的 显示   当前的兄弟.content隐藏
            $('.content').eq(index).show().siblings('.content').hide();
        });
        //滚动
        //原理：把marquee下面的子盒子都复制一遍 加入到marquee中
        //      然后动画向上滚动，滚动到一半重新开始滚动
        //因为选取的是两个marquee  所以要遍历
        $('.viewport-right-bottom .marquee').each(function (index, dom) {
            //将每个 的所有子级都复制一遍
            let rows = $(dom).children().clone();
            //再将新的到的加入原来的
            $(dom).append(rows);
        });
    }

    //刷新设备相关数据（24小时分析数据）
    function refreshEquipmentOeeStat() {
        api.getEquipmentOeeStat().then((res) => {
            let result = res.data
            if (result != null) {
                //平均OEE
                avgOEEChart.setOption({ series: [{ data: result.rate }] });
                $("#AvgOEE").text(result.oee + "%");
                $("#AvgOEECount").text(result.count);
                //设备OEE
                equipmentOEEChart.setOption({ dataset: { source: result.equipmentOEE } });
            }
        });
    }

    //-------------------------设备的运行状态----------------------------------
    function refreshEquipmentRunState() {
        api.getEquipmentRunState().then((res) => {
            result = res.data
            //设备实时状态
            let html = "";
            for (let i = 0; i < result.length; i++) {
                let item = result[i];
                html += '<div class="data"><h3><i class="icon-dot" style="color: ' + item.color + '"></i>' + item.stateName + '</h3><h3>' + item.count + '</h3><h3>' + item.rate + '%</h3></div>';
            }
            $("#equipmentCurrState").html(html);
        });
    }

    //-------------------------报警的设备----------------------------------
    function refreshWarnEquipment() {
        api.getWarnEquipment().then((res) => {
            result = res.data
            //报警设备
            let html = "";
            for (let i = 0; i < result.length; i++) {
                let item = result[i];
                html += '<div class="row"><span class="col">' + item.createTime + '</span><span class="col">' + item.lineName + "&nbsp;&nbsp;" + item.equipmentName + '</span><span class="col">' + item.warnCode + '</span><span class="icon-dot"></span></div>';
            }
            $("#equipmentWarnReports").html(html);
        });
    }

    //-------------------------------其他--------------------------------------------
    //窗口缩放调整图表大小
    window.addEventListener("resize", function () {
        setFont();
        avgOEEChart.resize();
        linePerformanceRateChart.resize();
        equipmentOEEChart.resize();
        faultTimeChart.resize();
        productRateChart.resize();
    });

    //初始加载
    initEquipmentWarn();
    initFaultTimeChart();
    initEequipmentEnergy();
    initEquipmentOEEChart();
    initLinePerformanceRateChart();
    initAvgOEEChart();
    initProductRateChart();

    //刷新数据
    refreshEquipmentStateStat();
    refreshEquipmentProductRate();
    refreshEquipmentOeeStat();
    refreshEquipmentRunState();
    refreshLinePerformanceRate();
    refreshEquipmentEnertyStat();
    refreshTopEmp();
    refreshEquipmentFualtTimeStat();
    refreshWarnEquipment();

    //定时器:刷新数据
    setInterval(function () {
        //设备状态
        refreshEquipmentStateStat();
        //产能占比
        refreshEquipmentProductRate();
        //平均OEE,设备OEE、设备状态、最后报警
        refreshEquipmentOeeStat();
        //设备运行状态数量
        refreshEquipmentRunState();
        //线别设备性能
        refreshLinePerformanceRate();
        //能耗
        refreshEquipmentEnertyStat();
        //今日之星
        refreshTopEmp();
        //故障时间排行
        refreshEquipmentFualtTimeStat();
        //报警设备
        refreshWarnEquipment();
    }, 1000 * 60);

    //定时器：时间显示
    setInterval(function () {
        $("#currTime").text(new Date().format("MM月dd hh:mm:ss"));
    }, 1000);
})();