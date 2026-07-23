(function () {

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


    //----------------------界面配置操作相关----------------------------------
    function init() {
        const ticketListEle = document.getElementById('ticketList');
        ticketListEle.addEventListener('click', function (event) {

            // 【关键步骤】找到被点击的 li 元素
            // event.target 可能是 li 本身，也可能是 li 内部的 label 或文字
            // .closest('li') 会向上查找直到找到最近的 li 标签
            const clickedLi = event.target.closest('li');

            // 校验：确保点击的是 ticketList 内部的 li，而不是其他地方
            if (clickedLi && ticketListEle.contains(clickedLi)) {
                // 获取 data-ticket-no 属性值
                const ticketNo = clickedLi.dataset.ticketNo;
                selectedTicketNo = ticketNo;
                highLightActiveItem(clickedLi);
                refreshSimpleOnlineNoticeInfo(ticketNo);
            }
        });
    }


    // 高亮逻辑 (移除其他 active，给当前添加 active)
    function highLightActiveItem(currentLi) {
        // 移除所有兄弟元素的 active 类
        const allItems = ticketList.querySelectorAll('.list-group-item-action');
        allItems.forEach(item => item.classList.remove('active'));
        // 给当前点击的添加 active
        currentLi.classList.add('active');
    }

    //--------------------------异步获取数据刷新图表--------------------------
    let selectedTicketNo = null;
    //获取未解决的呼叫记录
    function refreshSimpleOnlineNoticeList() {
        api.getSimpleOnlineNoticeTicketList().then((res) => {
            let result = res.data
            let htmlStr = "";
            let hasSelectedTicket = false;
            if (result != null) {
                result.forEach((item) => {
                    htmlStr += `<li id="ticket-item" class="list-group-item list-group-item-action ${selectedTicketNo==item.ticketNo? 'active':'' }"  data-ticket-no="${item.ticketNo ?? ""}">
                                    <label class="">编号:${item.ticketNo ?? ""}</label>
                                    <label class="">申请:${item.initiatorName ?? ""}&nbsp;/&nbsp;${item.lineName}</label>
                                </li>`;
                });

                hasSelectedTicket = result.some(item => item.ticketNo === selectedTicketNo);
            }
            $("#ticketList").html(htmlStr);
            if (!hasSelectedTicket) {
                loadInfoToPage(null);
            }
        });
    }

    //刷新单据信息
    function refreshSimpleOnlineNoticeInfo(ticketNo) {
        api.getSimpleOnlineNoticeTicketInfo(ticketNo).then((res) => {
            if(res.code==200)
                loadInfoToPage(res.data)
            else
                loadInfoToPage(null)
        });
    }

    function loadInfoToPage(result) {
        let htmlStr = "";
        let ticketNo = null;
        let initiatorName = null;
        let needTime = null;
        let newPartName = null;
        let oldPartName = null;
        let productQty = null;
        let lineName = null;

        if (result != null) {
            ticketNo = result.ticketNo;
            initiatorName = result.initiatorName;
            needTime = result.needTime;
            newPartName = result.newPartName;
            oldPartName = result.oldPartName;
            productQty = result.productQty;
            lineName = result.lineName;
            let a = 0;
            if (result.itemNav) {
                for (let i = 0; i < result.itemNav.length; i++) {
                    //  itemName1 = 
                    if (a == 0)
                        htmlStr += `<div class="row g-0">`;

                    htmlStr += `<label class="col-5 item-name ${result.itemNav[i].isReady? '':'item-not-ready'}">${result.itemNav[i].itemName}</label>
                                        <label class="col-1 item-qty">${result.itemNav[i].needQty}</label>`
                    a++;
                    if (a == 2 || i == result.itemNav.length - 1) {
                        htmlStr += `</div >`
                        a = 0;
                    }
                }
            }
        }
        $("#ticketNo").text(ticketNo);
        $("#initiatorName").text(initiatorName);
        $("#needTime").text(needTime);
        $("#newPartName").text(newPartName);
        $("#oldPartName").text(oldPartName);
        $("#productQty").text(productQty);
        $("#lineName").text(lineName);
        $("#itemNavList").html(htmlStr);
    }

    //窗口缩放
    window.addEventListener("resize", function () {
        setFont();
    });

    refreshSimpleOnlineNoticeList();
    //--------------------定时任务--------------------

    // 呼叫记录刷新（10秒）
    const callListIntervalId = setInterval(() => {
        refreshSimpleOnlineNoticeList();
    }, 10 * 1000);

    init();
})();

