<template>
    <div class="app-container">
        <el-timeline :reverse="true">
            <el-timeline-item
                v-for="(item, index) in options"
                :key="index"
                :timestamp="item.timestamp"
                placement="top"
            >
                <el-card>
                    <div class="h2">{{ item.version }}</div>
                    <div class="h3">Bug 修复</div>
                    <div
                        v-for="(info, infoIndex) in item.bugFix"
                        :key="info.title + infoIndex"
                        class="infoDiv"
                    >
                        <div class="h4">{{ info.title }}</div>
                        <div
                            v-for="(text, textIndex) in info.infos"
                            :key="'infoText_' + textIndex"
                            class="infoText"
                        >
                            - {{ text.text }}
                        </div>
                    </div>
                    <div class="h3">优化</div>
                    <div
                        v-for="(opt, optIndex) in item.optimization"
                        :key="optIndex"
                        class="infoDiv"
                    >
                        <div class="h4">{{ opt.title }}</div>
                        <div
                            v-for="(optText, optTextIndex) in opt.infos"
                            :key="optTextIndex"
                            class="infoText"
                        >
                            - {{ optText.text }}
                        </div>
                    </div>
                </el-card>
            </el-timeline-item>
        </el-timeline>
    </div>
</template>
<script>
export default {
    data() {
        return {
            options: [
                {
                    timestamp: "2023-01-12",
                    version: "2.0.1",
                    bugFix: [
                        {
                            title: "用户管理",
                            infos: [
                                {
                                    reportBy: "",
                                    text: "解决了搜索组件样式过暗的问题",
                                },
                                {
                                    reportBy: "",
                                    text: "解决了新增/修改用户信息时，部分字段描述错误的问题",
                                },
                            ],
                        },
                    ],
                    optimization: [
                        {
                            title: "版本追溯",
                            infos: [
                                {
                                    reportBy: "",
                                    text: "添加了更新日志模块，更好关注版本迭代，了解变更内容。",
                                },
                            ],
                        },
                    ],
                },
                {
                    timestamp: "2023-01-13",
                    version: "2.0.2",
                    bugFix: [
                        {
                            title: "SOP上传签核",
                            infos: [
                                {
                                    reportBy: "",
                                    text: "解决了手动送签时请求的URL错误导致送签失败的问题",
                                },
                            ],
                        },
                    ],
                    optimization: [
                        {
                            title: "sop展示",
                            infos: [
                                {
                                    reportBy: "",
                                    text: "选择好机种料号等信息，提交之后不会清空表单。",
                                },
                                {
                                    reportBy: "",
                                    text: "现在工站没有对应SOP时，会有相应提示，并主动关闭长链接。",
                                },
                            ],
                        },
                    ],
                },
                {
                    timestamp: "2023-02-08",
                    version: "2.0.3",
                    bugFix: [
                        {
                            title: "sop展示",
                            infos: [
                                {
                                    reportBy: "",
                                    text: "添加了加载弹窗，解决了sop文件异常时，点击确认按钮没有反应的问题。",
                                },
                            ],
                        },
                        {
                            title: "水印",
                            infos: [
                                {
                                    reportBy: "",
                                    text: "解决了水印的用户名称错误的显示为了undefined的问题",
                                },
                            ],
                        },
                        {
                            title: "copyright",
                            infos: [
                                {
                                    reportBy: "",
                                    text: "解决了当分辨率低时版权样式错乱的问题。",
                                },
                            ],
                        },
                    ],
                    optimization: [
                        {
                            title: "sop展示",
                            infos: [
                                {
                                    reportBy: "",
                                    text: "添加了PDF和视频的分屏展示。",
                                },
                                {
                                    reportBy: "",
                                    text: "添加了PDF的置顶。",
                                },
                                {
                                    reportBy: "",
                                    text: "添加了PDF的缩放。",
                                },
                            ],
                        },
                        {
                            title: "sop配置",
                            infos: [
                                {
                                    reportBy: "",
                                    text: "取消了一键推送时站位名工站站位序号的卡控。",
                                },
                                {
                                    reportBy: "",
                                    text: "现在可以按机种+料号+线体来一键推送",
                                },
                            ],
                        },
                    ],
                },
            ],
        };
    },
};
</script>
<style lang="scss" scoped>
.h2 {
    font-size: 25px;
    padding-bottom: 10px;
    font-weight: 700;
    border-bottom: 1px solid #ccc;
}
.h3 {
    padding: 10px 0;
    font-size: 20px;
    font-weight: 700;
    text-indent: 20px;
}
.h4 {
    font-size: 18px;
    padding: 5px 0;
}
.infoDiv {
    text-indent: 20px;
}
.infoText {
    line-height: 28px;
}
.app-container{
    height: calc(100vh - 144px);
    overflow-y: auto;
}
</style>
