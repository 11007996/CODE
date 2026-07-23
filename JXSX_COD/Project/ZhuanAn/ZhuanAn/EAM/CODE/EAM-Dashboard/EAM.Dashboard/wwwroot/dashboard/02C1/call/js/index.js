(function () {
    const myTTS = new TextToSpeech();
    const myAudioPlayer = new AudioPlayer();// 音频播放器
    let areaId;
    let speechStatus = false
    let speechInterval = 1

    let setFont = function () {
        // 因为要定义变量可能和别的变量相互冲突，污染，所有用自调用函数
        let html = document.documentElement;// 获取html
        // 获取宽度
        let width = html.clientWidth;

        // 判断
        //if (width < 1024) width = 1024
        //if (width > 1920) width = 1920
        // 设置html的基准值
        let fontSize = width / 22 + 'px';
        // 设置给html
        html.style.fontSize = fontSize;
    }
    setFont();

    // 启动监听呼叫权限
    activateSpeech();
    initSpeechInterval();
    initCallAreaList();

    //----------------------界面配置操作相关----------------------------------
    //初始化区域
    function initCallAreaList() {
        //事件绑定
        $("#AreaSelect").on("change", handleAreaChange);

        // tts绑定联动逻辑
        myTTS.on('start', () => {
            console.log("TTS 开始播放，背景音乐淡出...");
            // 在 300 毫秒内将背景音乐音量降到 0.2 (20%)
            myAudioPlayer.fadeVolume(0.2, 300);
        });

        myTTS.on('end', () => {
            console.log("TTS 播放结束，背景音乐淡入恢复...");
            // 在 500 毫秒内将背景音乐音量恢复到 1.0 (100%)
            myAudioPlayer.fadeVolume(1.0, 500);
        });

        api.getAreaList().then((res) => {
            result = res.data
            if (result && result.length > 0) {
                let htmlStr = '<option value="0">全局</option>';
                result.forEach((it) => {
                    htmlStr += `<option value="${it.areaId}">${it.areaName}</option>`;
                });
                $("#AreaSelect").html(htmlStr);
                areaId = window.localStorage.getItem("callAreaId") ?? 0;
                $("#AreaSelect").val(areaId);
                refreshCallCountData(areaId);
                refreshWeekDataAnalyse(areaId);
                refreshUnsolvedCallFault(areaId);

                clearTaskTimer();
                refreshCallScheduledTask(areaId, refreshInterval)
            }
        })
    }

    //区域切换
    function handleAreaChange() {
        areaId = $("#AreaSelect").val();
        window.localStorage.setItem("callAreaId", areaId);
        refreshCallCountData(areaId);
        refreshWeekDataAnalyse(areaId);
        refreshUnsolvedCallFault(areaId);

        clearTaskTimer();
        refreshCallScheduledTask(areaId, refreshInterval)
    }

    function initSpeechInterval() {
        $("#SpeechInterval").on("change", handleSpeechIntervalChange);
        speechInterval = window.localStorage.getItem("speechInterval") ?? 1;
        $("#SpeechInterval").val(speechInterval);
    }

    //修改广播时间间隔(分钟)
    function handleSpeechIntervalChange() {
        speechInterval = $("#SpeechInterval").val();
        window.localStorage.setItem("speechInterval", speechInterval);
    }

    //--------------------------异常获取数据刷新图表--------------------------
    //获取未解决的呼叫记录
    function refreshUnsolvedCallFault(areaId) {
        api.getUnsolvedCallFault(areaId).then((res) => {
            result = res.data
            let htmlStr = "";
            if (result != null) {
                result.forEach((item) => {
                    htmlStr += `<li><div class="call-item">
                                            <span>${item.lineName ?? ""}</span>
                                            <span>${item.callPointType == 'equipment' ? item.equipmentName : item.callPointType == 'station' ? item.stationName : ""}</span>
                                            <span>${item.callReason ?? ""}</span>
                                            <span>${item.callTargetTypeLabel ?? ""}</span>
                                            <span>${item.createTime ? item.createTime.substr(11, 5) : ""}</span>
                                            <span>${item.handleTime ? item.handleTime.substr(11, 5) : ""}</span>
                                            <span>${item.totalMinute ?? 0}分</span>
                                            <span>${item.handlerName ?? ""}</span>
                                            <span>${item.helperName ?? ""}</span>
                                            <span>${getFaultStatusLabel(item.faultStatus)}</span>
                                        </div>
                                   </li> `;
                });
            }
            $("#CallList ul").html(htmlStr);
            ttsPlayFaultContent(result);
        });
    }

    //根据状态返回标签内容
    function getFaultStatusLabel(faultStatus) {
        switch (faultStatus) {
            case 'Pending':
                return '<b style="color:#f56c6c;">待处理</b>';
                break;
            case 'Handling':
                return '<b style="color:#409eff;">处理中</b>';
                break;
            case 'WaitHelp':
                return '<b style="color:#f56c6c;">待支援</b>';
                break;
            case 'Helping':
                return '<b style="color:#e6a23c;">支援中</b>';
                break;
            case 'Completed':
                return '<b style="color:#67c23a;">已完成</b>';
                break;
            case 'Stop':
                return '<b style="color:#909399;">已中止</b>';
                break;
            default:
                return '';
        }
    }

    //月、周、月同比、周同比
    function refreshCallCountData(areaId) {
        api.getCallFaultCountStat(areaId).then((res) => {
            result = res.data
            if (result) {
                //月同比
                let monthContrastdata = [result.monthData[result.monthData.length - 3], result.monthData[result.monthData.length - 2]];
                let monthlegendData = [monthContrastdata[0].name, monthContrastdata[1].name];
                //monthContrastChart.clear();
                monthContrastChart.setOption({ legend: { data: monthlegendData }, series: [{ data: monthContrastdata }] });
                let monthRate = Math.ceil((monthContrastdata[1].value - monthContrastdata[0].value) / monthContrastdata[0].value * 100);//(上周总数-上上周总数)/上上周总数×100%

                if (monthRate == 'Infinity') {//除数据为0的特殊情况
                    $("#monthContrastArrow").html(`<div class="arrow-up"></div> <p>100%</p>`)
                }
                else if (monthRate <= 0) {
                    $("#monthContrastArrow").html(`<div class="arrow-down"></div> <p>${Math.abs(monthRate)}%</p>`)
                } else {
                    $("#monthContrastArrow").html(`<div class="arrow-up"></div> <p>${Math.abs(monthRate)}%</p>`)
                }
                //周同比
                let weekContrastdata = [result.weekData[result.weekData.length - 3], result.weekData[result.weekData.length - 2]];
                let weeklegendData = [weekContrastdata[0].name, weekContrastdata[1].name];
                //weekContrastChart.clear();
                weekContrastChart.setOption({ legend: { data: weeklegendData }, series: [{ data: weekContrastdata }] });
                let weekRate = Math.ceil((weekContrastdata[1].value - weekContrastdata[0].value) / weekContrastdata[0].value * 100);//(上周总数-上上周总数)/上上周总数×100%

                if (weekRate == 'Infinity') {//除数据为0的特殊情况
                    $("#weekContrastArrow").html(`<div class="arrow-up"></div> <p>100%</p>`)
                }
                else if (weekRate <= 0) {
                    $("#weekContrastArrow").html(`<div class="arrow-down"></div> <p>${Math.abs(weekRate)}%</p>`)
                } else {
                    $("#weekContrastArrow").html(`<div class="arrow-up"></div> <p>${Math.abs(weekRate)}%</p>`)
                }
                //月故障次数
                //monthStatChart.clear();
                monthStatChart.setOption({ dataset: { source: res.data.monthData } });
                //周故障次数
                //weekStatChart.clear();
                weekStatChart.setOption({ dataset: { source: res.data.weekData } });
            }
        });
    }

    //刷新本周人员时间、设备故障次数、设备故障类型排行的图表数据
    function refreshWeekDataAnalyse(areaId) {
        api.getCallFaultWeekStat(areaId).then((res) => {
            let result = res.data
            if (result) {
                //人员时长
                //empTimeChart.clear();
                empTimeChart.setOption({ series: [{ data: result.empTimeData }] });
                //设备故障次数
                //equipmentCountChart.clear();
                equipmentCountChart.setOption({ series: [{ data: result.equipmentData }] });

                //故障类型排行
                let yAxisData = result.faultTypeYData.reverse();//返回顺序，使故障最多的类型数据排在最上面
                result.faultTypeData.reverse();
                const rawData = [[], [], []]
                if (result.faultTypeData && result.faultTypeData) {
                    for (let row = 0; row < result.faultTypeYData.length; row++) {
                        let typeData = result.faultTypeData[row];
                        for (let col = 0; col < typeData.length; col++) {
                            rawData[col][row] = typeData[col]
                        }
                    }
                }
                const series = result.faultTypeYData.map((name, sid) => {
                    return {
                        name,
                        type: 'bar',
                        stack: 'all',
                        barWidth: '70%',
                        label: {
                            show: true,
                            formatter: (params) => {
                                return params.data.name;
                            }
                        },
                        data: rawData[sid]
                    };
                });
                faultTypeChart.setOption({ yAxis: { data: yAxisData }, series: series });
            }
        });
    }

    //-------------------定时广播任务【start】---------------------
    let taskTimerIds = []
    let refreshInterval = 60 * 5 //刷新间隔，单位秒

    //刷新定时任务
    function refreshCallScheduledTask(areaId, secord) {
        api.getCallScheduledTask(areaId, secord).then((res) => {
            if (res.code == 200 && res.data) {
                res.data.forEach(item => {
                    //设置一次性定时器
                    let timerId = setTimeout(function () { handleCallTask(item) }, item.executeWaitSeconds * 1000)
                    taskTimerIds.push(timerId);
                });
            }
        });
    }

    //定时广播任务处理
    function handleCallTask(task) {
        if (task) {
            if (task.playMedium == 'text') {
                for (i = 0; i < task.playCount; i++) {
                    myTTS.speak(task.textContent);
                }
            } else if (task.playMedium == 'file') {
                // 根据文件ID从全局缓存中取出对应的 Blob
                const targetBlob = window.MultiFileCache.get(task.fileId);
                // 判断是否存在，存在播放，不存在下载完成后播放
                if (targetBlob)
                    playSpecificAudio(targetBlob, task.playCount)
                else
                    api.downloadFile(task.fileId).then(res => {
                        playSpecificAudio(res, task.playCount)
                    });
            }
        }
    }

    //播放指定音频文件
    function playSpecificAudio(targetBlob, taskPlayCount) {
        if (targetBlob) {
            for (i = 0; i < taskPlayCount; i++) {
                const audioUrl = URL.createObjectURL(targetBlob);
                console.log("url:" + audioUrl)
                myAudioPlayer.enqueue(audioUrl)
            }
        } else {
            alert(`文件尚未下载或已被清除！`);
        }
    }

    //取消广播任务定时器
    function clearTaskTimer() {
        if (taskTimerIds) {
            taskTimerIds.forEach(item => {
                clearTimeout(item);
            });
            taskTimerIds = []
        }
    }

    //-------------------定时广播任务【end】---------------------

    //--------------------------------------播放呼叫内容-------------------------------
    // 监听任意用户交互，只执行一次,用于激活呼叫权限
    function activateSpeech() {
        ['click', 'keydown'].forEach(event => {
            document.addEventListener(event, enableSpeechOnce, { once: true, passive: true });
        });
    }

    //语音激活
    function enableSpeechOnce() {
        // 触发一次空朗读，激活语音上下文
        const utter = new SpeechSynthesisUtterance("");
        window.speechSynthesis.speak(utter);
        $("#SpeechStatus").removeClass("voice-close").addClass("voice-open");
        speechStatus = true;
    }

    //处理呼叫记录，判断是否播放语音
    function ttsPlayFaultContent(callList) {
        if (!speechStatus || !callList || callList.length <= 0) {
            return;
        }
        //呼叫ID集合
        let callIds = callList.map(it => it.callId);

        //格式 [{callId:0,faultStatus:'',lastCallTime:'yyyy-MM-dd hh:mm:ss'}]
        let tempSpeakList = window.localStorage.getItem("SpeakList");
        let localSpeakList = tempSpeakList ? JSON.parse(tempSpeakList) : [];//故障广播状态列表
        //清除不存在的数据
        localSpeakList = localSpeakList.filter(item => callIds.indexOf(item.callId) >= 0);

        for (let i = 0; i < callList.length; i++) {
            let callInfo = callList[i];

            //呼叫就绪、请求支援 播放广播
            if (callInfo.faultStatus == 'Pending' || callInfo.faultStatus == 'WaitHelp') {
                //呼叫文本语音内容
                let speakText = "";
                if (callInfo.faultStatus == 'WaitHelp') {//请求支援
                    speakText = callInfo.lineName + callInfo.equipmentName.replace("*", "") + ",设备故障，请高级工程师尽快到现场处理"
                } else {//故障呼叫
                    if (callInfo.callPointType == 'equipment')
                        speakText = callInfo.callReason + ",请" + callInfo.callTargetTypeLabel + "到" + callInfo.lineName + callInfo.equipmentName.replace("*", "") + "处理";
                    else if (callInfo.callPointType == 'station')
                        speakText = callInfo.callReason + ",请" + callInfo.callTargetTypeLabel + "到" + callInfo.lineName + callInfo.stationName.replace("*", "") + "处理";
                    else
                        speakText = callInfo.callReason + ",请" + callInfo.callTargetTypeLabel + "到" + callInfo.lineName + "处理";
                }

                //判断是否广播声音
                let speakInfo = localSpeakList.find(it => it.callId == callInfo.callId);
                if (!speakInfo) {//新的故障
                    localSpeakList.push({ callId: callInfo.callId, faultStatus: callInfo.faultStatus, lastCallTime: new Date() });
                    myTTS.speak(speakText);
                    myTTS.speak(speakText);
                } else if (callInfo.faultStatus == 'Pending' && callInfo.faultStatus == speakInfo.faultStatus && timeAgo(new Date(speakInfo.lastCallTime), new Date()) >= speechInterval * 60) {//待处理
                    localSpeakList.forEach((it) => {
                        if (it.callId == callInfo.callId) {
                            it.faultStatus = callInfo.faultStatus;
                            it.lastCallTime = new Date()
                        }
                    });
                    myTTS.speak(speakText);
                    myTTS.speak(speakText);
                } else if (callInfo.faultStatus == 'WaitHelp' && (callInfo.faultStatus != speakInfo.faultStatus || timeAgo(new Date(speakInfo.lastCallTime), new Date()) >= speechInterval * 60)) {//待支援，状态发生变化或超时，广播-次
                    localSpeakList.forEach((it) => {
                        if (it.callId == callInfo.callId) {
                            it.faultStatus = callInfo.faultStatus;
                            it.lastCallTime = new Date()
                        }
                    });
                    myTTS.speak(speakText);
                }
            }
        }
        window.localStorage.setItem("SpeakList", JSON.stringify(localSpeakList));//故障广播状态列表
    }

    //时间比较，返回相差秒数
    function timeAgo(startTime, endTime) {
        const diff = endTime - startTime
        const seconds = Math.floor(diff / 1000)
        return seconds;
    }

    //--------------------------月同比--------------------------
    let monthContrastChart;
    function initMonthContrastChart() {
        monthContrastChart = echarts.init($('#monthContrastChart')[0]);

        let option = {
            "animation": true,
            "tooltip": {
                "trigger": "item"
            },
            "legend": {
                "width": "100%",
                "orient": "vertical",
                "right": "0",
                "top": "0",
                "textStyle": {
                    "color": "#fff",
                    "fontSize": 12
                },
                "icon": "circle",
                "padding": 2,
                "itemGap": 5,
                // "data": ["上月", "本月"]
            },
            "series": [{
                "type": "pie",
                "center": ["50%", "65%"],
                "radius": ["40%", "90%"],
                "color": ["#FEE449", "#00FFFF"],
                "startAngle": 180,
                "endAngle": 360,
                "smooth": true,
                "labelLayout": function () {
                    return {
                        x: '10%',
                        y: monthContrastChart.getHeight() - 40,
                        moveOverlap: 'shiftY',
                        align: 'left',
                    };
                },
                labelLine: {
                    show: false
                },
                "label": {
                    "position": 'center',
                    "textMargin": 2,
                    "formatter": "{b|{b}:}  {c|{c}} ",
                    "borderRadius": 4,
                    "rich": {
                        "b": {
                            "color": "#b3e5ff",
                            "fontSize": '0.25rem',
                        },
                        "c": {
                            "color": "#eaffd0",
                            "fontSize": '0.25rem',
                        }
                    },
                },
                //"data": [
                //    { "name": "上月", "value": 80 },
                //    { "name": "本月", "value": 100 }
                //],
            }],
        }
        monthContrastChart.setOption(option);
    }

    //--------------------------周同比--------------------------
    let weekContrastChart;
    function initWeekContrastChart() {
        weekContrastChart = echarts.init($('#weekContrastChart')[0]);

        let option = {
            "animation": true,
            "tooltip": {
                "trigger": "item"
            },
            "legend": {
                "width": "100%",
                "orient": "vertical",
                "right": "0",
                "top": "0",
                "textStyle": {
                    "color": "#fff",
                    "fontSize": 12
                },
                "icon": "circle",
                "padding": 2,
                "itemGap": 5,
                //"data": ["上周", "本周"]
            },
            "series": [{
                "type": "pie",
                "center": ["50%", "65%"],
                "radius": ["40%", "90%"],
                "color": ["#FEE449", "#00FFFF"],
                "startAngle": 180,
                "endAngle": 360,
                "smooth": true,
                "labelLayout": function () {
                    return {
                        x: '10%',
                        y: weekContrastChart.getHeight() - 40,
                        moveOverlap: 'shiftY',
                        align: 'left',
                    };
                },
                labelLine: {
                    show: false
                },
                "label": {
                    "position": 'center',
                    "textMargin": 2,
                    "formatter": "{b|{b}:} {c|{c}} ",
                    "borderRadius": 4,
                    "rich": {
                        "b": {
                            "color": "#b3e5ff",
                            "fontSize": '0.25rem',
                        },
                        "c": {
                            "color": "#eaffd0",
                            "fontSize": '0.25rem',
                        }
                    },
                },
                //"data": [
                //    { "name": "上周", "value": 80 },
                //    { "name": "本周", "value": 100 }
                //],
            }],
        }
        weekContrastChart.setOption(option);
    }

    //--------------------------月故障次数统计--------------------------
    let monthStatChart;
    function initMonthStatChart() {
        monthStatChart = echarts.init($('#monthStatChart')[0]);

        let option = {
            tooltip: {
                trigger: 'axis',
                axisPointer: {
                    type: 'cross',
                    label: {
                        backgroundColor: '#6a7985'
                    }
                }
            },
            grid: {
                top: 0,
                left: 0,
                right: 0,
                bottom: 0
            },
            xAxis: {
                type: 'category',
                boundaryGap: false,
                axisLabel: {
                    "color": "#b3e5ff",
                },
            },
            yAxis: {
                type: 'value',
                axisLabel: {
                    "color": "#ffffff",
                },
                axisLine: {
                    show: true
                },
                splitLine: {
                    show: false
                }
            },
            series: [
                {
                    type: 'line',
                    stack: 'Total',
                    smooth: true,
                    lineStyle: {
                        width: 3
                    },
                    areaStyle: {
                        opacity: 0.8,
                        color: new echarts.graphic.LinearGradient(0, 0, 0, 1, [
                            {
                                offset: 0,
                                color: 'rgb(128, 255, 165)'
                            },
                            {
                                offset: 1,
                                color: 'rgb(1, 191, 236)'
                            }
                        ])
                    },
                }
            ],
            dimensions: ['name', 'value'],
            //dataset: {
            //    //source: [{ name: 'Mon', value: 140 }, { name: 'Tue', value: 232 }, { name: 'Wed', value: 101 }]
            //}
        };
        monthStatChart.setOption(option);
    }

    //--------------------------周故障数量统计--------------------------
    let weekStatChart;
    function initWeekStatChart() {
        weekStatChart = echarts.init($('#weekStatChart')[0]);

        let option = {
            tooltip: {
                trigger: 'axis',
                axisPointer: {
                    type: 'cross',
                    label: {
                        backgroundColor: '#6a7985'
                    }
                }
            },
            grid: {
                top: 0,
                left: 0,
                right: 0,
                bottom: 0
            },
            xAxis: {
                type: 'category',
                boundaryGap: false,
                axisLabel: {
                    "color": "#b3e5ff",
                },
            },
            yAxis: {
                type: 'value',
                axisLine: {
                    show: true
                },
                axisLabel: {
                    "color": "#ffffff",
                },
                splitLine: {
                    show: false
                }
            },
            series: [
                {
                    type: 'line',
                    stack: 'Total',
                    smooth: true,
                    lineStyle: {
                        width: 3
                    },
                    areaStyle: {
                        opacity: 0.8,
                        color: new echarts.graphic.LinearGradient(0, 0, 0, 1, [
                            {
                                offset: 0,
                                color: 'rgb(128, 255, 165)'
                            },
                            {
                                offset: 1,
                                color: 'rgb(1, 191, 236)'
                            }
                        ])
                    },
                }
            ],
            dimensions: ['name', 'value'],
            //dataset: {
            //    //source: [{ name: 'Mon', value: 140 }, { name: 'Tue', value: 232 }, { name: 'Wed', value: 101 }]
            //}
        };
        weekStatChart.setOption(option);
    }

    //--------------------------人员时长统计--------------------------
    let empTimeChart;
    function initEmpTimeChart() {
        empTimeChart = echarts.init($('#empTimeChart')[0]);

        let color = ["#8d7fec", "#5085f2", "#e75fc3", "#f87be2", "#f2719a", "#fca4bb", "#f59a8f", "#fdb301", "#57e7ec", "#cf9ef1"]

        let option = {
            color: color,
            series: [{
                type: 'pie',
                clockwise: false, //饼图的扇区是否是顺时针排布
                minAngle: 2, //最小的扇区角度（0 ~ 360）
                radius: ["20%", "60%"],
                itemStyle: { //图形样式
                    borderColor: '#ffffff',
                    borderWidth: 1,
                },
                label: {
                    show: true,
                    alignTo: 'edge',
                    edgeDistance: 10,
                    lineHeight: 15,
                    minMargin: 5,
                    //formatter: '{text|{b}}\n{c} ({d}%)',
                    formatter: '{text|{b}}\n{c}',
                    rich: {
                        text: {
                            color: "#fff",
                            fontSize: 10,
                        },
                        value: {
                            color: "#8693F3",
                            fontSize: 10,
                        },
                    },
                    emphasis: {
                        textStyle: {
                            fontSize: 10,
                        }
                    }
                },
                labelLine: {
                    length: 15,
                    length2: 0,
                    maxSurfaceAngle: 80
                },
                labelLayout: function (params) {
                    const isLeft = params.labelRect.x < empTimeChart.getWidth() / 2;
                    const points = params.labelLinePoints;
                    points[2][0] = isLeft
                        ? params.labelRect.x
                        : params.labelRect.x + params.labelRect.width;
                    return {
                        labelLinePoints: points
                    };
                },
                //data: [{name:'',value:''}]
            }]
        };
        empTimeChart.setOption(option);
    }

    //--------------------------设备次数统计--------------------------
    let equipmentCountChart;
    function initEquipmentCountChart() {
        equipmentCountChart = echarts.init($('#equipmentCountChart')[0]);

        let color = ["#8d7fec", "#5085f2", "#e75fc3", "#f87be2", "#f2719a", "#fca4bb", "#f59a8f", "#fdb301", "#57e7ec", "#cf9ef1"]

        let option = {
            color: color,
            series: [{
                type: 'pie',
                clockwise: false, //饼图的扇区是否是顺时针排布
                minAngle: 2, //最小的扇区角度（0 ~ 360）
                radius: ["20%", "60%"],
                //center: ["30%", "50%"],
                itemStyle: { //图形样式
                    borderColor: '#ffffff',
                    borderWidth: 1,
                },
                label: {
                    show: true,
                    alignTo: 'edge',
                    edgeDistance: 1,
                    lineHeight: 15,
                    minMargin: 5,
                    //formatter: '{text|{b}}\n{c} ({d}%)',
                    formatter: '{text|{b}}\n{c}',
                    rich: {
                        text: {
                            color: "#fff",
                            fontSize: 10,
                        },
                        value: {
                            color: "#8693F3",
                            fontSize: 10,
                        },
                    },
                    emphasis: {
                        textStyle: {
                            fontSize: 10,
                        }
                    }
                },
                labelLine: {
                    length: 15,
                    length2: 0,
                    maxSurfaceAngle: 80
                },
                labelLayout: function (params) {
                    const isLeft = params.labelRect.x < equipmentCountChart.getWidth() / 2;
                    const points = params.labelLinePoints;
                    points[2][0] = isLeft
                        ? params.labelRect.x
                        : params.labelRect.x + params.labelRect.width;
                    return {
                        labelLinePoints: points
                    };
                },
                //data: [{name:'',value:''}]
            }]
        };
        equipmentCountChart.setOption(option);
    }

    //--------------------------故障类型分析--------------------------
    let faultTypeChart;
    function initFaultTypeChart() {
        faultTypeChart = echarts.init($('#faultTypeChart')[0]);

        let option = {
            grid: {
                top: 0,
                left: 0,
                right: 0,
                bottom: 0
            },
            xAxis: {
                type: 'value',
                max: 1,
                axisLabel: {
                    show: false,
                    color: "#b3e5ff",
                },
                splitLine: {
                    show: false
                }
            },
            yAxis: {
                type: 'category',
                axisLabel: {
                    color: "#ffffff",
                    width: 100,
                    overflow: 'break'
                },
                //data: ['Direct', 'Mail Ad','ABC']
            },
            //series:series
        };
        faultTypeChart.setOption(option);
    }

    //窗口缩放
    window.addEventListener("resize", function () {
        setFont();
        faultTypeChart.resize();
        equipmentCountChart.resize();
        empTimeChart.resize();
        weekStatChart.resize();
        monthStatChart.resize();
        weekContrastChart.resize();
        monthContrastChart.resize();
    });

    //图表初始化
    initMonthContrastChart();
    initWeekContrastChart();
    initMonthStatChart();
    initWeekStatChart();
    initEquipmentCountChart();
    initEmpTimeChart();
    initFaultTypeChart();

    //--------------------定时任务--------------------
    // 呼叫记录刷新（10秒）
    const callListIntervalId = setInterval(() => {
        refreshUnsolvedCallFault(areaId);
    }, 10 * 1000);

    // 图表数据刷新（60秒）
    const chartIntervalId = setInterval(() => {
        refreshCallCountData(areaId);
        refreshWeekDataAnalyse(areaId);
    }, 60 * 1000);

    //定时广播任务
    const callTaskIntervalId = setInterval(() => {
        refreshCallScheduledTask(areaId, refreshInterval);
    }, refreshInterval * 1000);
})();