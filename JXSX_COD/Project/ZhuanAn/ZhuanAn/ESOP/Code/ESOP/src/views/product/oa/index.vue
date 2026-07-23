<template>
    <div class="app-container">
        <el-row class="paddingRow headRow">
            <el-form :model="queryParams" ref="queryForm" size="small" :inline="true" v-show="showSearch"
                label-width="68px">
                <el-form-item label="单号" prop="oaId">
                    <el-input v-model="queryParams.oaId" placeholder="请输入单号" clearable @keyup.enter.native="handleQuery" />
                </el-form-item>
                <el-form-item label="签核状态" prop="status">
                    <el-select v-model="queryParams.status" placeholder="请选择签核状态" clearable>
                        <el-option v-for="dict in dict.type.oa_status" :key="dict.value" :label="dict.label"
                            :value="dict.value" />
                    </el-select>
                </el-form-item>
                <el-form-item label="sop名称" prop="sopName">
                    <el-input v-model="queryParams.sopName" placeholder="请输入sop名称" clearable
                        @keyup.enter.native="handleQuery" />
                </el-form-item>
                <el-form-item>
                    <el-button type="primary" icon="el-icon-search" size="mini" @click="handleQuery">搜索</el-button>
                    <el-button icon="el-icon-refresh" size="mini" @click="resetQuery">重置</el-button>
                </el-form-item>
                <el-form-item>
                    <el-button type="primary" plain icon="el-icon-plus" size="mini" @click="handleAdd"
                        v-hasPermi="['product:oa:add']">新增</el-button>
                </el-form-item>
            </el-form>
        </el-row>
        <el-row class="paddingRow">
            <el-tabs v-model="queryParams.operateStatus" @tab-click="handleClick" type="card">
                <el-tab-pane name="未处理">
                    <span slot="label">
                        <i class="el-icon-coordinate"></i> 待签核
                    </span>
                </el-tab-pane>
                <el-tab-pane name="已处理">
                    <span slot="label">
                        <i class="el-icon-circle-check"></i> 已处理
                    </span>
                </el-tab-pane>
                <el-tab-pane name="我的签核">
                    <span slot="label">
                        <i class="el-icon-circle-check"></i> 我的签核
                    </span>
                </el-tab-pane>
            </el-tabs>
            <el-table v-loading="loading" :data="tableData" stripe @selection-change="handleSelectionChange" border>
                <af-table-column label="单号" prop="oaId" show-overflow-tooltip fixed="left" min-width="180px">
                    <template slot-scope="scope">
                        <span @click="handleOaIdClick(scope.row)" class="clickable-oaid">{{ scope.row.oaId }}</span>
                    </template>
                </af-table-column>
                <af-table-column label="sop名称" prop="sopName" show-overflow-tooltip min-width="160px" />
                <af-table-column label="发起人" prop="createByName" show-overflow-tooltip />
                <af-table-column label="发起时间" prop="createTime" min-width="140px" show-overflow-tooltip />
                <af-table-column label="版本" prop="version" show-overflow-tooltip />
                <!-- <el-table-column
                    label="机种"
                    prop="modelName"
                    show-overflow-tooltip
                />
                <af-table-column
                    label="料号"
                    prop="materialName"
                    show-overflow-tooltip
                /> -->
                <af-table-column label="线体" prop="lineName" min-width="140px" show-overflow-tooltip />
                <af-table-column label="站位序号" prop="stageName" show-overflow-tooltip />
                <af-table-column label="站位名" prop="processName" min-width="160px" show-overflow-tooltip />
                <el-table-column label="签核状态" prop="status">
                    <template slot-scope="scope">
                        <dict-tag :options="dict.type.oa_status" :value="scope.row.status" />
                    </template>
                </el-table-column>
                <af-table-column label="退回原因" prop="remark" min-width="140px" v-if="queryParams.operateStatus != '未处理'" />
                <el-table-column label="操作" fixed="right" :min-width="queryParams.operateStatus != '已处理'
                    ? '180px'
                    : '100px'
                    ">
                    <template slot-scope="scope">
                        <el-button size="mini" type="text" icon="el-icon-coordinate"
                            v-if="queryParams.operateStatus == '未处理'" @click="changeStatus(scope.row, 'Y')"
                            :disabled="scope.row.status != '1'">签核</el-button>
                        <el-button size="mini" type="text" icon="el-icon-circle-close"
                            v-if="queryParams.operateStatus != '已处理'" class="dangerButton"
                            @click="changeStatus(scope.row, 'N')" :disabled="scope.row.status != '1'">退回</el-button>
                        <el-button size="mini" type="text" icon="el-icon-view" @click="handleView(scope.row)">查看</el-button>
                    </template>
                </el-table-column>
            </el-table>
            <pagination v-show="total > 0" :total="total" :page.sync="queryParams.pageNum"
                :limit.sync="queryParams.pageSize" @pagination="getList" />
        </el-row>
        <!-- 添加或修改oa签核对话框 -->
        <el-dialog :title="title" :visible.sync="open" width="90%" center @open="setForm" @close="dialogClose"
            :close-on-click-modal="false"  custom-class="addDialog">
            <el-form ref="form" :model="form" :rules="rules" label-width="80px">
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
                    <el-select v-model="form.lineId" clear filterable @change="changeLine" placeholder="请选择线别"
                        ref="lineSelect" size="mini">
                        <el-option v-for="(item, index) in lineOption" :key="index" :label="item.lineName"
                            :value="item.lineId" />
                    </el-select>
                </el-form-item>
                <el-form-item label="会签人员" prop="OACountersignUserList">
                    <el-select v-model="form.OACountersignUserList" multiple filterable placeholder="请选择" size="mini">
                        <el-option v-for="item in allowCountersignUserOptions" :key="item.userName" :label="item.nickName"
                            :value="item.userName" />
                    </el-select>
                </el-form-item>
                <el-form-item label="通知人员" prop="notifyUser">
                    <el-select v-model="form.notifyUser" multiple filterable placeholder="请选择" size="mini">
                        <el-option v-for="item in allowCountersignUserOptions" :key="item.userName" :label="item.nickName"
                            :value="item.userName" />
                    </el-select>
                </el-form-item>

                <el-form-item label="站位序号" prop="stageId">
                    <el-select v-model="form.stageId" clearable filterable @change="changeStage" placeholder="请选择站位序号"
                        ref="stageSelect" size="mini">
                        <el-option v-for="(item, index) in stageOption" :key="index" :label="item.stageName"
                            :value="item.stageId" />
                    </el-select>
                </el-form-item>
                <el-form-item label="站位名" prop="processId">
                    <el-select v-model="form.processId" clearable filterable placeholder="请选择站位名" @change="changeProcess"
                        size="mini" ref="processSelect">
                        <el-option v-for="(item, index) in processOption" :key="index" :label="item.processName"
                            :value="item.processId" />
                    </el-select>
                </el-form-item>
                <!-- <el-form-item label="工站" prop="macId">
                    <el-select
                        v-model="form.macId"
                        clearable
                        filterable
                        placeholder="请选择工站"

                        ref="terminalSelect"
                    >
                        <el-option
                            v-for="(item, index) in terminalOption"
                            :key="index"
                            :label="item.macName"
                            :value="item.macId"
                        />
                    </el-select>
                </el-form-item> -->
                <el-form-item label="版本" prop="version">
                    <el-input v-model="form.version" placeholder="请输入版本" size="mini" clearable />
                </el-form-item>
                <el-form-item label="文件上传" prop="fileName">
                    <el-upload accept=".pdf,.mp4,.MTS" action :on-change="fileOnChange" :before-upload="fileBeforeUpload"
                        :auto-upload="false" :show-file-list="false" size="mini">
                        <el-button type="success" size="mini" icon="el-icon-upload">点击上传</el-button>
                        <div class="el-upload__tip" slot="tip">
                            文件:{{ fileName }}
                        </div>
                    </el-upload>
                </el-form-item>
                <el-form-item label="备注" prop="remark">
                    <el-input v-model="form.remark" type="textarea" size="mini" placeholder="请输入内容" />
                </el-form-item>
                <el-form-item>
                    <el-row class="fixHeight">
                        <el-button class="addButton" type="success" size="mini" icon="el-icon-plus" plain
                            @click="addToTable">添加信息</el-button>
                        <el-button class="addButton" type="danger" size="mini" icon="el-icon-delete" plain
                            @click="removeInfoTableRow" :disabled="sopInfoTable.length === 0">删除信息</el-button>
                    </el-row>
                </el-form-item>
            </el-form>
            <el-table :data="sopInfoTable" border @selection-change="changeInfoTable">
                <el-table-column type="selection" min-width="55px" />
                <el-table-column label="线体" prop="lineName" />
                <el-table-column label="站位序号" prop="stageName" />
                <el-table-column label="站位名" prop="processName" />
                <el-table-column label="SOP页码">
                    <template slot-scope="scope">
                        <el-input v-model.trim="sopInfoTable[scope.$index].sopPage" size="mini"
                            placeholder="英文逗号隔开阿拉伯数字，如1,2,3" clearable>
                            <template slot="append">页</template>
                        </el-input>
                    </template>
                </el-table-column>
                <el-table-column label="翻页间隔">
                    <template slot-scope="scope">
                        <el-input v-model="sopInfoTable[scope.$index].sopInterval" size="mini" clearable>
                            <template slot="append">秒</template>
                        </el-input>
                    </template>
                </el-table-column>
            </el-table>
            <pagination v-show="total > 0" :total="total" :page.sync="form.pageNum" :limit.sync="form.pageSize"
                @pagination="getSopToCheckList" />
            <div slot="footer" class="dialog-footer">
                <el-button type="primary" @click="submitForm">确 定</el-button>
                <el-button @click="cancel">取 消</el-button>
            </div>
        </el-dialog>
        <!-- pdf mp4 -->
        <el-dialog class="pdfDialog" :modal="false" append-to-body fullscreen :visible.sync="pdfDialogVisible"
            style="height: 100vh">
            <VuePDF :pdfUrl="pdfUrl" ref="VuePdf" @close_PDF_dialog="close_PDF_dialog" @setPDF="setPDF"
                @splitScreen="splitScreen" :pdfPages="pdfPages" pdfInterval="5" :pdfPassword="pdfPassword" />
            <!-- <iframe ref="pdfIframe" :key="pdfUrl" :src="pdfUrl" frameborder="0" width="100%" height="100%" @load="iframeLoaded" name="pdfIframe"></iframe> -->
        </el-dialog>
        <el-dialog :modal="false" class="videoDialog" append-to-body :visible.sync="videoDialogVisible" v-el-drag-dialog
            width="70%" top="5vh">
            <VideoPlayer :key="videoUrl" :url="videoUrl" id="oaVideo" ref="videoPlayer" />
        </el-dialog>
        <!-- 查看oa签核详情 -->
        <el-dialog title="单号签核详情" :visible.sync="detailDialogVisible" width="50%" center>
            <el-table :data="detailDataList" border style="width: 100%">
                <el-table-column prop="lineName" label="线别"></el-table-column>
                <el-table-column prop="stageName" label="站位序号"></el-table-column>
                <el-table-column prop="processName" label="站位名"></el-table-column>
            </el-table>
            <span slot="footer" class="dialog-footer">
                <el-button @click="detailDialogVisible = false">关 闭</el-button>
            </span>
        </el-dialog>
    </div>
</template>

<script>
import {
    listOa,
    listOaDetail,
    updateOa,
    getMesModelOptions,
    addOaEsopInfo,
    sendLuxLink,
    getOACountersignUserList,
    updateEsop,
    uploadSopList,
    // getUserListByLineId,
    getSopToCheckList,
    // templateList,
} from "@/api/product/oa";
import {
    getStageInTerminal,
    getProcessInTerminal,
} from "@/api/product/terminal";
import { listLine } from "@/api/product/line";
// import { listTerminal } from "@/api/product/terminal";
// import Cookie from "js-cookie";
import { noDataMixins } from "@/mixins";
import JsEncrypt from "jsencrypt";
import VuePDF from "../sop/pdf";
import VideoPlayer from "../sop/video";
export default {
    name: "Oa",
    dicts: ["oa_status"],
    mixins: [noDataMixins],
    components: {
        VuePDF,
        VideoPlayer,
    },
    data() {
        return {
            pdfDialogVisible: false,
            videoDialogVisible: false,
            pdfUrl: "",
            pdfPages: [],
            videoUrl: "",
            activeName: "0",
            // 遮罩层
            loading: true,
            // 选中数组
            ids: [],
            // 非单个禁用
            single: true,
            // 非多个禁用
            multiple: false,
            // 显示搜索条件
            showSearch: true,
            // 总条数
            total: 0,
            // oa签核表格数据
            tableData: [],
            // 弹出层标题
            title: "",
            // 是否显示弹出层
            open: false,
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
            fileName: "",
            // 可签核人员列表签人员
            allowCountersignUserOptions: [],

            // 查询参数
            queryParams: {
                pageNum: 1,
                pageSize: 10,
                oaId: null,
                requestId: null,
                sopId: null,
                status: null,
                operateStatus: "未处理",
            },
            // 表单参数
            form: {
                file: "",
                fileName: "",
                pageNum: 1,
                pageSize: 10,
            },
            //站位名类型
            processTypeOptions: [
                {
                    label: "单站位名",
                    value: "0",
                },
                {
                    label: "多站位名",
                    value: "1",
                },
            ],
            // 表单校验
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
                // lineId: [
                //     {
                //         required: true,
                //         message: "线体不能为空",
                //         trigger:  "blur",
                //     },
                // ],
                // processId: [
                //     {
                //         required: true,
                //         message: "站位名不能为空",
                //         trigger:  "blur",
                //     },
                // ],
                // stageId: [
                //     {
                //         required: true,
                //         message: "站位序号不能为空",
                //         trigger:  "blur",
                //     },
                // ],
                // macId: [
                //     {
                //         required: true,
                //         message: "工站不能为空",
                //         trigger: ["change", "blur"],
                //     },
                // ],
                version: {
                    required: true,
                    message: "版本不能为空",
                    trigger: "blur",
                },
                fileName: {
                    required: true,
                    message: "未上传文件",
                    trigger: "change",
                },
                OACountersignUserList: [
                    {
                        required: true,
                        message: "会签人员不能为空",
                        trigger: "blur",
                    },
                ],
            },
            //文件列表
            sopInfoTable: [],
            //勾选的表格行
            infoTableRows: [],
            jsEncrypt: null,
            pdfPassword: null,
            detailDialogVisible: false, // 控制详情弹窗显示
            detailDataList: {} // 存储签核详情数据list
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
    created() {
        this.getList();
        this.getLine();
        this.jsEncrypt = new JsEncrypt();
        const privateKey = `-----BEGIN PRIVATE KEY-----
MIIEvQIBADANBgkqhkiG9w0BAQEFAASCBKcwggSjAgEAAoIBAQDKpekUQEnoZa2q
T70c4moKin7KdAB97JnbqOtR3TtMFtc9ANnug6dOOmjFJRbgL4iQsmX3BmAGOmTO
XblpKTybESZvmhNE99++VlowT2dVwMU9CTA44HtZiYZ81lwA9pwhpjqpS0wteDEO
QRcaYy5SO9zcsZ38VBYFusoP2Y91EMb0p23rOdiAttkau2XVIvGh1Ghul0FHz85Y
IThmbfkhP5SUISDwC746St2yLL3GN/dXc+VOs1slRvHv1CERXhqS784Y3qmgb2G9
GiwMD2WP63C0rBOCemoLhMAHAp+/pX0RxOjGaJnBWwG5tuApyhrRwm01auyl/Tjs
mhx3ZwwxAgMBAAECggEAIcZzSY/JgbVos4kkwOqvt+ALb9zTtCk6H5VQ200fM/he
mWlJ6WoB+ZTcn3cmD+l8PnmtavWiDYewA4E1hOR9mG7MVC9+5LDXlta3o3Ooim9d
sGWWpvQrOuokAyyLGxH/RdB52HuXT8DHlFOe8SP0tXoKvrHP3h15qizOvsOJGH6O
AhuqTRVnSbdO97r5peFT/kX0ChHiLHIIsMSk8gIWw73c+ejIWgboCsjFRlBuNqkT
pPWsA3j5scOYPvkXO0kueSi+GLwnr0PhT2k5xO6o1YEAFT0uhU4LCX8YD0L3Tvx2
19z+m8uLn1l2S5wlqk8lKk80UswDeufVJP1odcwTfQKBgQD9CGHhLrUDJONDj/WY
f84Fzo9TFujQ07dvrQaGAbQnKqeBjj52yyL+WWSGrmt3dgo8kcXPszlE+uT4+h38
nruDGyWqexN1gCCAdUu21kjIx5oXPxUoIAl/b9mX04y/6p1sBg8mXztaoxy8mhSG
JazL6RwRoztRpa2d2taMrPAeGwKBgQDNBkVOSIODI8BxltIM97PL1+leLvk4aI82
7JotHdorEuoWEzoB5V2yjNweva+XSQALTNUhdoRxFTxnMXGnwVl/e+7Py5VWB4Wv
ZnMA17zGQwNijBUn1wyfVwToKdPLrXm9LmZ2MQi+5eAz/VX29iaN/W36NvBL1WoQ
j+lCy9WzowKBgDDSPjh5j5l0s5jknOl4t2KtcUAB6pfoUbttchXHHGB2PW2k6W54
UV8sFlZaLwgUsXLwWW9y0Dj8A9P6RnDom5t3UHQtXRrNxveiKiK0A8UhphyYIlfk
npCFH0HJIp4hAZDHNoMb2tLpJ/FH9W/Qsx+A8daBXT+qrO4JPF5WO9pDAoGACWKY
GZVIL+CbFpgI1X8hQ9uGW0FbNzHSHHmINTiAnCgpfwkyRpPxThMUoHOebhZxYhMK
TpXWSjbmpPKmeT9okWVi8TAojd+aRwUxjoBRq+G1bfVron89nK2nE9mWUGSIhhhx
qEdmVxa+xKJ8JOnvqeBIAIQzS8VhLZDo5J3gEnECgYEAxBpef9O6iIBgDPJFfXRd
J93fkdr/GPKTr/33PqTl36DDeTjqmPDHFwXSn1wfMDtdsAq/cFai8wXBAqhDFHlg
/Rge8z0oUeXVpxUS4AQBsnIv9U/USwWcGlabdE4SgaStZa6eHfE3shmO6qvDVBLL
1Hgif0HlsQ0Q5tpaKGYhWXo=
-----END PRIVATE KEY-----`;
        this.jsEncrypt.setPrivateKey(privateKey);
    },
    methods: {
        setForm() {
            this.form = {
                OACountersignUserList: [],
                file: "",
                notifyUser: [],
                fileName: "",
                //modelId: "",
                //materialId: "",
                lineId: "",
                processId: "",
                stageId: "",
                //macId: "",
                version: "",
                remark: "",
            };
        },
        handleClick(val) {
            this.getList();
        },
        handleOaIdClick(row) {
            const oaId = row.oaId;
            listOaDetail({ oaId }).then(response => {
                console.log('完整响应:', response);
                if (response.code === 200 && response.rows.length > 0) {
                    this.detailDataList = response.rows; // 设置整个列表
                    this.detailDialogVisible = true;   // 打开弹窗
                } else {
                    this.$message.warning("未查询到该单号的详细信息");
                }
            }).catch(error => {
                console.error('接口调用失败:', error);
            });
        },
        //弹窗关闭的回调，重置表单与Sop信息的表格
        dialogClose() {
            this.$refs.form.resetFields();
            this.sopInfoTable = [];
        },
        fileOnChange(file) {
            this.form["fileName"] = file.name;
            this.form.file = file;
            this.$nextTick(() => {
                this.$refs.form.validateField("fileName");
            });
            this.fileName = this.form.file.name;
        },
        fileBeforeUpload(file) { },

        // // 获取机种信息
        // handleMesModelOptions() {
        //     getMesModelOptions().then((response) => {
        //         this.modelOptions = response.rows;
        //     });
        // },
        /** 查询oa签核列表 */
        getList() {
            this.loading = true;
            if (this.queryParams.operateStatus === "我的签核") {
                uploadSopList(this.queryForm).then((res) => {
                    this.tableData = res.rows;
                    this.total = res.total;
                    this.loading = false;
                });
            } else {
                listOa(this.queryParams).then((res) => {
                    this.tableData = res.rows;
                    this.total = res.total;
                    this.loading = false;
                });
            }
        },

        // 表单重置
        reset() {
            this.form = {
                pageNum: 1,
                pageSize: 10,
            };
            this.fileName = "";
            this.newFileName = "";
            this.originalFileName = "";
            this.resetForm("form");
        },
        /** 搜索按钮操作 */
        handleQuery() {
            this.queryParams.pageNum = 1;
            this.getList();
        },
        /** 重置按钮操作 */
        resetQuery() {
            this.resetForm("queryForm");
            this.handleQuery();
        },
        // 多选框选中数据
        handleSelectionChange(selection) {
            this.ids = selection.map((item) => item.oaId);
            this.single = selection.length !== 1;
            this.multiple = !selection.length;
        },
        /** 新增按钮操作 */
        handleAdd() {
            this.reset();
            this.open = true;
            this.title = "添加签核";
            this.getSopToCheckList()
            // this.handleMesModelOptions();
            this.handleSelectOACountersignUser();
        },

        /** 获取有OA签核权限的人员信息 */
        handleSelectOACountersignUser() {
            getOACountersignUserList().then((response) => {
                this.allowCountersignUserOptions = response.rows;
            });
        },

        /** 提交按钮 */
        submitForm() {
            let validArr = [];
            this.$refs.form.validateField(
                [
                    "OACountersignUserList",
                    // "modelId",
                    // "materialId",
                    "version",
                    "fileName",
                ],
                (valid) => {
                    validArr.push(valid);
                }
            );
            if (validArr.every((item) => item === "")) {
                if (this.form.oaId != null) {
                    const loading = this.$loading({
                        lock: true,
                        text: "修改中...",
                        spinner: "el-icon-loading",
                        background: "rgba(0, 0, 0, 0.7)",
                    });
                    updateOa(this.form)
                        .then((response) => {
                            this.$modal.msgSuccess("修改成功");
                            this.open = false;
                            this.getList();
                            loading.close();
                        })
                        .catch((err) => {
                            loading.close();
                        });
                } else {
                    const loading = this.$loading({
                        lock: true,
                        text: "发起签核中...",
                        spinner: "el-icon-loading",
                        background: "rgba(0, 0, 0, 0.7)",
                    });
                    let len = this.form.fileName.length;
                    let fileType = this.form.fileName.substring(len - 3, len);
                    //let noSopPage = false;
                    let terminalPageList = this.sopInfoTable.map((item) => {
                        item.sopPage =
                            item.sopPage === undefined ? "" : item.sopPage;
                        item.sopInterval =
                            item.sopInterval === undefined
                                ? ""
                                : item.sopInterval;
                        // if (item.sopPage == "") {
                        //     noSopPage = true;
                        // }
                        return {
                            lineId: item.lineId,
                            stageId: item.stageId,
                            processId: item.processId,
                            // macId: item.macId,
                            sopPage: item.sopPage,
                            sopInterval: item.sopInterval,
                        };
                    });
                    // if (noSopPage && fileType === "pdf") {
                    //     return this.$modal.msgError("请输入SOP的页码！");
                    // }
                    let formData = new FormData();
                    formData.append(
                        "applyAccount",
                        localStorage.getItem("userName")
                    );
                    formData.append("file", this.form.file.raw);
                    formData.append("remark", this.form.remark);
                    //formData.append("materialId", this.form.materialId);
                    formData.append("version", this.form.version);
                    // formData.append("modelId", this.form.modelId);
                    formData.append("type", fileType);
                    formData.append(
                        "countersignUser",
                        this.form.OACountersignUserList.toString()
                    );
                    formData.append(
                        "notifyUser",
                        this.form.notifyUser.toString()
                    );
                    terminalPageList.forEach((value, index) => {
                        formData.append(`terminalPageList[${index}].lineId`, value.lineId);
                        formData.append(`terminalPageList[${index}].stageId`, value.stageId);
                        formData.append(`terminalPageList[${index}].processId`, value.processId);
                        formData.append(`terminalPageList[${index}].sopPage`, value.sopPage);
                        formData.append(`terminalPageList[${index}].sopInterval`, value.sopInterval);
                    });

                    addOaEsopInfo(formData)
                        .then((response) => {
                            if (response.code === 200) {
                                this.$modal.msgSuccess(response.msg);
                                this.open = false;
                                this.getList();
                            } else {
                                this.$modal.msgError(response.msg);
                            }
                            this.open = false;
                            loading.close();
                        })
                        .catch((err) => {
                            loading.close();
                        });
                }
            } else {
                this.$modal.msgError("请按提示输入指定内容");
            }
        },
        // 取消按钮
        cancel() {
            this.open = false;
            this.reset();
        },
        handleSendLuxLink(row) {
            sendLuxLink(row.oaId).then((response) => {
                if (response.code === 200) {
                    this.$modal.msgSuccess(response.msg);
                } else {
                    this.$modal.msgError(response.msg);
                }
            });
            this.handleQuery();
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
        //     this.sopInfoTable = [];
        //     listLineByMaterialId(val).then((res) => {
        //         this.lineOption = res.rows;
        //     });
        //     // if (val) {
        //     //     templateList({ materialId: val }).then((res) => {
        //     //         this.sopInfoTable = res.rows;
        //     //     });
        //     // }
        // },
        //获取线体
        getLine() {
            listLine({ pageSize: 9999, pageNum: 1 }).then((res) => {
                this.lineOption = res.rows;
            });
        },
        //线别change查询站位序号
        changeLine(val) {
            this.form.stageId = null;
            this.form.processId = null;
            if (val) {
                // materialName
                getSopToCheckList({ lineId: val }).then((res) => {
                    this.sopInfoTable = res.rows;
                });
                getUserListByLineId(val).then((res) => {
                    this.form.OACountersignUserList = res.data.UserNameList;
                });
            }
            getStageInTerminal(val).then((res) => {
                this.stageOption = res.rows;
            });
        },
        getSopToCheckList() {
            const { pageNum, pageSize } = this.form;
            //现在的需求是默认获取所有的工站吗？对
            getSopToCheckList({ pageNum, pageSize }).then((res) => {
                this.total = res.total;
                getSopToCheckList({ pageNum, pageSize: this.total }).then((res) => {
                    this.total = res.total;
                    this.sopInfoTable = res.rows;
                    this.form.pageSize=this.total;
                });
            });
        },
        //站位序号change查询站位名
        changeStage(val) {
            this.form.processId = null;
            this.processOption = [];
            if (val) {
                getSopToCheckList({
                    lineId: this.form.lineId,
                    stageId: val
                }).then((res) => {
                    this.sopInfoTable = res.rows;
                });
            }
            getProcessInTerminal(this.form.lineId, val).then((res) => {
                this.processOption = res.rows;
            });
        },
        //站位名change查询工站
        changeProcess(val) {
            if (val) {
                getSopToCheckList({
                    lineId: this.form.lineId,
                    stageId: this.form.stageId,
                    processId: val
                }).then((res) => {
                    this.sopInfoTable = res.rows;
                });
            }
            // listTerminal({ processId: val }).then((res) => {
            //     this.terminalOption = res.rows;
            // });
        },
        addToTable() {
            let validArr = [];
            this.$refs.form.validateField(
                [
                    // "modelId",
                    // "materialId",
                    // "lineId",
                    "processId",
                    "stageId",
                    //"macId",
                ],
                (valid) => {
                    validArr.push(valid);
                }
            );
            // 如果验证通过
            if (validArr.every((item) => item === "")) {
                this.form["lineName"] = this.$refs.lineSelect.selected.label;
                this.form["stageName"] = this.$refs.stageSelect.selected.label;
                this.form["processName"] =
                    this.$refs.processSelect.selected.label;
                // this.form["macName"] =
                //     this.$refs.terminalSelect.selected.label;
                let sameFlag = false;
                this.sopInfoTable.map((item) => {
                    if (
                        // item["materialId"] === this.form.materialId &&
                        item["lineId"] === this.form.lineId &&
                        item["processId"] === this.form.processId &&
                        item["stageId"] === this.form.stageId
                    ) {
                        sameFlag = true;
                    }
                });
                if (!sameFlag) {
                    this.sopInfoTable.push(
                        JSON.parse(JSON.stringify(this.form))
                    );
                } else {
                    this.$modal.msgError("请勿添加重复项");
                }
                // 重置部分字段，保留机种料号
                let resetArr = ["lineId", "stageId", "processId"];
                resetArr.map((item) => {
                    this.$refs.form.fields
                        .find((f) => f.prop == item)
                        .resetField();
                });
            } else {
                this.$modal.msgError("请输入指定内容后再添加");
            }
        },
        changeInfoTable(rows) {
            this.infoTableRows = rows;
        },
        //删除表格行
        removeInfoTableRow() {
            if (this.infoTableRows.length === 0) {
                this.$modal.msgError("请勾选对应项次后再删除");
            } else {
                this.sopInfoTable = this.sopInfoTable.filter((item, index) => {
                    let arrList = this.infoTableRows;
                    let flag = true;
                    arrList.map((arr) => {
                        if (
                            arr.lineId === item.lineId &&
                            arr.stageId === item.stageId &&
                            // arr.macId === item.macId &&
                            arr.processId === item.processId
                        ) {
                            flag = false;
                        }
                    });
                    if (flag) {
                        return item;
                    }
                });
            }
        },
        //签核单据/退回单据
        changeStatus(row, status) {
            let action = status === "Y" ? "签核" : "退回";
            let that = this;
            this.$prompt(
                `是否要${action}单号为"${row.oaId}"的这项单据吗?`,
                "警告",
                {
                    confirmButtonText: "确定",
                    cancelButtonText: "取消",
                    type: "warning",
                    center: true,
                }
            )
                .then(({ value }) => {
                    updateEsop({ oaId: row.oaId, status, remark: value }).then(
                        (res) => {
                            if (res.code === 200) {
                                this.$modal.msgSuccess(res.msg);
                            } else {
                                this.$modal.msgError(res.msg);
                            }
                            that.getList();
                        }
                    );
                })
                .catch(() => {
                    this.$message({
                        type: "info",
                        message: `已取消${action}`,
                    });
                });
        },
        close_PDF_dialog() {
            this.pdfDialogVisible = false;
            this.pdfPages = [];
        },
        //查看sop
        handleView(row) {
            let sopNameLength = row.sopName.length;
            if (
                row.sopName.substring(sopNameLength - 3, sopNameLength) == "pdf"
            ) {
                this.pdfPassword = this.jsEncrypt.decrypt(row.passWord);
                this.$nextTick(() => {
                    this.pdfUrl = process.env.VUE_APP_BASE_API + row.filePath;
                });
                this.pdfDialogVisible = true;
            } else {
                this.$nextTick(() => {
                    this.videoUrl = process.env.VUE_APP_BASE_API + row.filePath;
                    this.videoDialogVisible = true;
                });
            }
        },
    },
};
</script>
<style lang="scss" scoped>
::v-deep .addDialog{
    height: 80%;
    display: flex;
    flex-direction: column;
    overflow: hidden;
    .el-dialog__body{
        flex: 1;
        overflow-y: auto;
        min-height: unset!important;
    }
}
::v-deep .headRow .el-form-item {
    margin-bottom: 0;
}

::v-deep .el-dialog {
    transform: inherit;

    // .el-select__tags {
    //     top: 14px;
    // }
    .el-select {
        width: 100%;
    }

    .el-cascader {
        width: 100%;
    }

    .el-form {
        display: flex;
        flex-wrap: wrap;

        .el-form-item {
            width: calc(25% - 10px);
            display: flex;
            margin-bottom: 15px;
        }

        .el-form-item__content {
            margin-left: 0 !important;
            flex: 1;
        }
    }
}

::v-deep .el-textarea__inner {
    min-height: 28px !important;
    line-height: 1;
}

.addButton {
    margin-left: 20px;
}

::v-deep .el-upload__tip {
    margin-top: 0;
}

::v-deep .el-form-item--medium .el-form-item__content {
    line-height: 20px;
}

.line {
    width: 100%;
    height: 30px;
}

::v-deep .el-dialog__body {
    padding: 10px;
    min-height: 400px;
    min-width: 300px;
}

.fixHeight {
    height: 28px;
}

::v-deep .el-table__fixed-right {
    height: 100% !important;
}

::v-deep .el-table__fixed {
    height: 100% !important;
}

.clickable-oaid {
    color: #0ea6fe; // 淡蓝色
    cursor: pointer;
    text-decoration: none; // 默认无下划线

    &:hover {
        text-decoration: underline; // 鼠标悬停时显示下划线
    }
}
</style>
<style lang="scss">
.el-textarea .el-textarea__inner {
    min-height: 28px !important;
}
</style>
