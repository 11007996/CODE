//此命名空间可以导出用于其它模块，声明前都在加上export
export var exportTs;
(function (exportTs) {
    function show() {
        console.log("exportTs function show");
    }
    exportTs.show = show;
})(exportTs || (exportTs = {}));
export var decal;
(function (decal) {
    class CC {
        show(name) {
            console.log(name);
        }
    }
    decal.CC = CC;
})(decal || (decal = {}));
