/* 站点SOP展示 */
<template>
    <div class="app-container viewBox">
        <el-row class="paddingRow" v-if="showSearchRow">
            <el-form
                ref="form"
                :model="form"
                inline
                label-width="80px"
                :rules="rules"
            >
                <!-- <el-form-item label="机种" prop="modelId">
                    <el-select
                        v-model="form.modelId"
                        placeholder="请选择机种"
                        filterable
                        clear
                        @change="changeModel"
                        ref="modelSelect"
                        size="mini"
                    >
                        <el-option
                            v-for="item in modelOptions"
                            :key="item.modelId"
                            :label="item.modelName"
                            :value="item.modelId"
                        />
                    </el-select>
                </el-form-item>
                <el-form-item label="料号" prop="materialId">
                    <el-select
                        v-model="form.materialId"
                        clearable
                        size="mini"
                        filterable
                        ref="materialSelect"
                        @change="changeMaterial"
                        placeholder="请选择料号"
                    >
                        <el-option
                            v-for="(item, index) in materialOption"
                            :key="index"
                            :label="item.materialName"
                            :value="item.id"
                        />
                    </el-select>
                </el-form-item> -->
                <el-form-item label="线别" prop="lineId">
                    <el-select
                        v-model="form.lineId"
                        clear
                        filterable
                        @change="changeLine"
                        placeholder="请选择线别"
                        ref="lineSelect"
                        size="mini"
                        :disabled="isPasswordFree"
                    >
                        <el-option
                            v-for="(item, index) in lineOption"
                            :key="index"
                            :label="item.lineName"
                            :value="item.lineId"
                        />
                    </el-select>
                </el-form-item>
                <el-form-item label="站位序号" prop="stageId">
                    <el-select
                        v-model="form.stageId"
                        clearable
                        filterable
                        @change="changeStage"
                        placeholder="请选择站位序号"
                        ref="stageSelect"
                        size="mini"
                        :disabled="isPasswordFree"
                    >
                        <el-option
                            v-for="(item, index) in stageOption"
                            :key="index"
                            :label="item.stageName"
                            :value="item.stageId"
                        />
                    </el-select>
                </el-form-item>
                <el-form-item label="站位名" prop="processId">
                    <el-select
                        v-model="form.processId"
                        clearable
                        filterable
                        placeholder="请选择站位名"
                        size="mini"
                        ref="processSelect"
                        :disabled="isPasswordFree"
                    >
                        <el-option
                            v-for="(item, index) in processOption"
                            :key="index"
                            :label="item.processName"
                            :value="item.processId"
                        />
                    </el-select>
                </el-form-item>
                <!-- <el-form-item label="工站" prop="terminalId">
                    <el-select
                        v-model="form.terminalId"
                        clearable
                        filterable
                        placeholder="请选择工站"
                        size="mini"
                        ref="terminalSelect"
                        :disabled="isPasswordFree"
                    >
                        <el-option
                            v-for="(item, index) in terminalOption"
                            :key="index"
                            :label="item.terminalName"
                            :value="item.terminalId"
                        />
                    </el-select>
                </el-form-item> -->
                <el-form-item>
                    <el-button
                        type="primary"
                        icon="el-icon-promotion"
                        plain
                        size="mini"
                        @click="submitForm"
                        >确认</el-button
                    >
                </el-form-item>
            </el-form>
        </el-row>
        <el-dialog
            class="pdfDialog"
            :modal="false"
            append-to-body
            fullscreen
            :visible.sync="pdfDialogVisible"
            :style="pdfStyle"
        >
            <VuePDF
                :pdfUrl="pdfUrl"
                ref="VuePdf"
                @close_PDF_dialog="close_PDF_dialog"
                @setPDF="setPDF"
                @splitScreen="splitScreen"
                :pdfPages="pdfPages"
                :pdfInterval="pdfInterval"
                :pdfPassword="pdfPassword"
                :pdfSizeList="pdfSizeList"
            />
        </el-dialog>
        <el-dialog
            :modal="false"
            class="videoDialog"
            append-to-body
            :visible.sync="videoDialogVisible"
            v-el-drag-dialog
            width="70%"
            top="5vh"
            :style="videoStyle"
        >
            <VideoPlayer
                :url="videoUrl"
                ref="videoPlayer"
                id="viewerVideo"
                :key="videoUrl"
            />
        </el-dialog>
    </div>
</template>
<script>
import { getMesModelOptions, templateList } from "@/api/product/oa";
import { getPartNoList } from "@/api/product/partNo";
import { listLine } from "@/api/product/line";
import {
    getStageInTerminal,
    getProcessInTerminal,
} from "@/api/product/terminal";
import VuePDF from "../product/sop/pdf";
import { noDataMixins } from "@/mixins";

import VideoPlayer from "../product/sop/video";
export default {
    components: {
        VuePDF,
        VideoPlayer,
    },
    mixins: [noDataMixins],
    data() {
        return {
            showSearchRow: true,
            form: {
                // modelId: "",
                // materialId: "",
                lineId: "",
                stageId: "",
                processId: "",
                terminalId: "",
            },
            rules: {
                // modelId: [
                //     {
                //         required: true,
                //         message: "机种不能为空",
                //         trigger: ["change", "blur"],
                //     },
                // ],
                // materialId: [
                //     {
                //         required: true,
                //         message: "料号不能为空",
                //         trigger: ["change", "blur"],
                //     },
                // ],
                lineId: [
                    {
                        required: true,
                        message: "线体不能为空",
                        trigger: ["change", "blur"],
                    },
                ],
                processId: [
                    {
                        required: true,
                        message: "站位名不能为空",
                        trigger: ["change", "blur"],
                    },
                ],
                stageId: [
                    {
                        required: true,
                        message: "站位序号不能为空",
                        trigger: ["change", "blur"],
                    },
                ],
                terminalId: [
                    {
                        required: true,
                        message: "工站不能为空",
                        trigger: ["change", "blur"],
                    },
                ],
            },
            //料号
            materialOption: [],
            // 机种List
            modelOptions: [],
            //线别
            lineOption: [],
            //站位序号
            stageOption: [],
            //站位名
            processOption: [],
            //工站
            terminalOption: [],
            //
            pdfInterval: "",
            pdfDialogVisible: false,
            videoDialogVisible: false,
            pdfUrl: "",
            videoUrl: "",
            pdfPages: "",
            $ws: null,
            isPasswordFree: false,
            mac: localStorage.getItem("mac"),

        };
    },
    watch: {
        videoDialogVisible(val) {
            if (!val) {
                this.$refs.videoPlayer && this.$refs.videoPlayer.pause();
            } else {
                this.$refs.videoPlayer && this.$refs.videoPlayer.play();
            }
        },
    },
    methods: {
        // 获取机种信息
        handleMesModelOptions() {
            getMesModelOptions().then((response) => {
                this.modelOptions = response.rows;
            });
        },
        // //机种change查询料号
        // changeModel(val) {
        //     getPartNoList({ modelId: val }).then((res) => {
        //         this.materialOption = res.rows;
        //         this.form.materialId = "";
        //     });
        // },
        // //料号change查询线体
        // changeMaterial(val) {
        //     listLine({ materialId: val }).then((res) => {
        //         this.lineOption = res.rows;
        //     });
        //     // if (val) {
        //     //     templateList({ materialId: val }).then((res) => {});
        //     // }
        // },
        getLine() {
            listLine().then((res) => {
                this.lineOption = res.rows;
            });
        },
        //线别change查询站位序号
        changeLine(val) {
            getStageInTerminal(val).then((res) => {
                this.stageOption = res.rows;
            });
        },
        //站位序号change查询站位名
        changeStage(val) {
            getProcessInTerminal(this.form.lineId, val).then((res) => {
                this.processOption = res.rows;
            });
        },
        //站位名change查询工站
        // changeProcess(val) {
        //     listTerminal({ processId: val }).then((res) => {
        //         this.terminalOption = res.rows;
        //     });
        // },
        //提交
        submitForm() {
            this.$refs.form.validate((valid) => {
                if (valid) {
                    // this.showSearchRow = false;
                    this.connectWs();
                } else {
                    this.$modal.msgError("请按提示输入指定内容");
                }
            });
        },
        //检查是是否非普通登录
        checkIsPasswordFree() {
            if (this.$store.state.user.stageInfo.lineId) {
                let { lineId, stageId, processId } =
                    this.$store.state.user.stageInfo;
                this.form.lineId = lineId;
                this.form.stageId = stageId;
                this.form.processId = processId;
                // this.form.terminalId = terminalId;
                this.changeLine(lineId);
                this.changeStage(stageId);
                this.isPasswordFree = true;
            } else {
                this.isPasswordFree = false;
            }
        },
        connectWs() {
            const loading = this.$loading({
                lock: true,
                text: "加载中...",
                spinner: "el-icon-loading",
                background: "rgba(0, 0, 0, 0.7)",
            });
            var that = this;
            let wsUrl =
                //`ws://${process.env.NODE_ENV === "development"?location.host:location.host+':8090'}/ws/` +
                `ws://${location.host}/ws/` +
                this.form.lineId +
                "_" +
                this.form.stageId +
                "_" +
                this.form.processId +
                "_" +
                this.mac;
                console.log(this.mac);
            var ws = new WebSocket(wsUrl);
            this.$ws = ws;
            //申请一个WebSocket对象，参数是服务端地址，同http协议使用http://开头一样，WebSocket协议的url使用ws://开头，另外安全的WebSocket协议使用wss://开头
            // ws.onopen = function () {
            //     //当WebSocket创建成功时，触发onopen事件
            //     ws.send("hello"); //将消息发送到服务端
            // };
            // ws.onmessage = function (e) {
            //     //当客户端收到服务端发来的消息时，触发onmessage事件，参数e.data包含server传递过来的数据
            //     console.log("msg");
            //     console.log(e.data);
            // };
            ws.onclose = function (e) {
                //当客户端收到服务端发送的关闭连接请求时，触发onclose事件
                console.log(e);
            };
            ws.onerror = function (e) {
                loading.close();
                //如果出现连接、处理、接收、发送数据失败的时候触发onerror事件
                console.log(e);
            };

            //心跳检测  .所谓的心跳检测，就是隔一段时间向服务器仅限一次数据访问，因为长时间不使用会导致ws自动断开，
            // 一般是间隔90秒内无操作会自动断开，因此，在间隔时间内进行一次数据访问，以防止ws断开即可，
            //这里选择30秒，倒计时30秒内无操作则进行一次访问，有操作则重置计时器
            //
            //封装为键值对的形式，成为js对象，与json很相似
            var heartCheck = {
                timeout: 30000, //30秒
                timeoutObj: null,
                reset: function () {
                    //接收成功一次推送，就将心跳检测的倒计时重置为30秒
                    clearTimeout(this.timeoutObj); //重置倒计时
                    this.start();
                },
                start: function () {
                    //启动心跳检测机制，设置倒计时30秒一次
                    this.timeoutObj = setTimeout(function () {
                        var message = {
                            type: "t10010",
                            service: "发送维持连接消息！",
                        };
                        console.log("发送维持连接消息！");
                        ws && ws.send(JSON.stringify(message)); //启动心跳
                    }, this.timeout);
                },
                //onopen连接上，就开始start及时，如果在定时时间范围内，onmessage获取到了服务端消息，
                // 就重置reset倒计时，距离上次从后端获取消息30秒后，执行心跳检测，看是不是断了。
            };
            // ---- ...
            // socket
            ws.onopen = function () {
                loading.close();
                //当WebSocket创建成功时，触发onopen事件
                ws.send("hello"); //将消息发送到服务端
                heartCheck.start(); //连接成功之后启动心跳检测机制
            };
            ws.onmessage = function (e) {
                //当客户端收到服务端发来的消息时，触发onmessage事件，参数e.data包含server传递过来的数据
                console.log(e);
                console.log(JSON.parse(e.data));
                let res = JSON.parse(e.data).sopInfoList;
                if (res.length === 0) {
                    that.$modal.msgError(
                        "该工站暂无可用SOP，请前往SOP签核页面维护，连接已关闭。"
                    );
                    if (ws && ws.close !== null) {
                        ws.close();
                        console.log("关闭连接");
                    }
                    ws = null;
                }
                res.map((item) => {
                    if (item.type === "0") {
                        that.pdfPages =
                            item.sopPage === "" || !item.sopPage
                                ? []
                                : item.sopPage.split(",").map((str) => {
                                      return str * 1;
                                });
                        that.pdfPassword = that.jsEncrypt.decrypt(
                            item.passWord
                        );
                        that.pdfInterval = item.sopInterval || "10";
                        that.pdfSizeList=item.pdfSizeList;
                        that.pdfDialogVisible = true;
                        that.$nextTick(() => {
                            that.pdfUrl =
                                process.env.VUE_APP_BASE_API + item.filePath;
                        });
                    }
                    if (item.type === "1") {
                        that.$nextTick(() => {
                            that.videoUrl =
                                process.env.VUE_APP_BASE_API + item.filePath;
                            that.videoDialogVisible = true;
                        });
                    }
                });
                //接收一次后台推送的消息，即进行一次心跳检测重置
                heartCheck.reset();
            };
        },
    },
    mounted() {
        this.getLine();
        this.checkIsPasswordFree();
    },
    destroyed() {
        this.$ws && this.$ws.close();
        clearTimeout(this.timeoutObj);
    },
};
</script>
<style lang="scss" scoped>
::v-deep .el-dialog__body {
    padding: 0;
}
::v-deep .el-dialog__headerbtn {
    top: 10px;
}
// 弹窗层元素不可穿透点击事件（不影响弹窗层元素的点击事件）
</style>
<style>
/* .viewBox .el-dialog__wrapper {
    pointer-events: none !important;
}

.viewBox .el-dialog__wrapper .el-dialog {
    pointer-events: auto !important;
} */

.videoDialog {
    pointer-events: none;
}
.videoDialog .el-dialog {
    pointer-events: auto;
}
</style>
