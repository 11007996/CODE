// 可以在项目的公共 JS 文件中定义
window.MultiFileCache = {
    // 用一个普通对象作为字典来存储多个文件的 Blob
    fileMap: {},

    // 存入单个文件的 Blob (传入文件ID和对应的Blob)
    set: function (fileId, blob) {
        this.fileMap[fileId] = blob;
    },

    // 根据文件ID获取对应的 Blob
    get: function (fileId) {
        return this.fileMap[fileId];
    },

    // 移除指定文件的缓存
    remove: function (fileId) {
        delete this.fileMap[fileId];
        console.log(`文件 ${fileId} 缓存已清除`);
    },

    // 清空所有文件的缓存（建议在页面销毁或彻底不需要时调用）
    clearAll: function () {
        // 在清空前，最好先释放掉所有由 createObjectURL 生成的链接
        this.fileMap = {};
        console.log('所有文件缓存已清空');
    }
};