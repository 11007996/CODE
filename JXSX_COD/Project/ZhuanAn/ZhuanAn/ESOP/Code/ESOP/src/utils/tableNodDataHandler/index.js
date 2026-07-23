export function tableNodDataHandler(tableData, height) {
    let top =
        (
            (height - document.getElementsByClassName("el-table__header")[0].offsetHeight - 120) / 2 +
            document.getElementsByClassName("el-table__header")[0].offsetHeight
        ) / 3;
    let emptyInner =
        `<img
            style='position:absolute;
            left: calc(50% - 150px);
            top: ${height ? top : "30%"};
            height:300px;'
            src=
        ` + require("@/assets/images/tableNoData.png") + ">" +
        `<div style='position:absolute;top:300px;left:calc(50% - 150px);font-size:18px; width:300px;text-align:center;'>暂无数据</div>`;
    let emptyDiv = document.createElement("div");
    emptyDiv.setAttribute("style", "text-align: center;height:240px;");
    emptyDiv.className = "no-data-big showTip";
    emptyDiv.innerHTML = emptyInner;
    if (tableData.length == 0) {
        if (document.getElementsByClassName("showTip").length) {
            document.getElementsByClassName("el-table__header-wrapper")[0].removeChild( document.getElementsByClassName("showTip")[0]);
        }
        document.getElementsByClassName("el-table__header-wrapper")[0].appendChild(emptyDiv);
    } else if (document.getElementsByClassName("showTip").length) {
        document.getElementsByClassName("el-table__header-wrapper")[0].removeChild(document.getElementsByClassName("showTip")[0]);
    }
}
