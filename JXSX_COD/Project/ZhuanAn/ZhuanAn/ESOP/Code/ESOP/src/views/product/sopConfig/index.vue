<template>
    <div class="app-container">
        <el-row class="paddingRow searchRow">
            <el-form
                :model="queryParams"
                ref="queryForm"
                size="small"
                :inline="true"
                v-show="showSearch"
                label-width="68px"
            >
                <!-- <el-form-item label="机种" prop="modelName">
                    <el-input
                        v-model="queryParams.modelName"
                        placeholder="请输入机种"
                        clearable
                        @keyup.enter.native="handleQuery"
                    />
                </el-form-item> -->
                <!-- <el-form-item label="料号" prop="materialName">
                    <el-input
                        v-model="queryParams.materialName"
                        placeholder="请输入料号"
                        clearable
                        @keyup.enter.native="handleQuery"
                    />
                </el-form-item> -->
                <el-form-item label="线别" prop="lineName">
                    <el-input
                        v-model="queryParams.lineName"
                        placeholder="请输入线别"
                        clearable
                        @keyup.enter.native="handleQuery"
                    />
                </el-form-item>
                <el-form-item label="SOP" prop="sopName">
                    <el-input
                        v-model="queryParams.sopName"
                        placeholder="请输入SOP名称"
                        clearable
                        @keyup.enter.native="handleQuery"
                    />
                </el-form-item>
                <el-form-item>
                    <el-button
                        type="primary"
                        icon="el-icon-search"
                        size="mini"
                        @click="handleQuery"
                        plain
                        >搜索</el-button
                    >
                    <el-button
                        icon="el-icon-refresh"
                        type="warning"
                        plain
                        size="mini"
                        @click="resetQuery"
                        >重置</el-button
                    >
                    <el-button
                        icon="el-icon-magic-stick"
                        size="mini"
                        type="success"
                        plain
                        @click="massReplace"
                        >一键推送</el-button
                    >
                </el-form-item>
            </el-form>

            <!-- <el-row :gutter="10" class="mb8">
                    <el-col :span="1.5">
                        <el-button
                            type="primary"
                            plain
                            icon="el-icon-plus"
                            size="mini"
                            @click="handleAdd"
                            v-hasPermi="['product:sopConfig:add']"
                        >新增</el-button>
                    </el-col>
                    <el-col :span="1.5">
                        <el-button
                            type="success"
                            plain
                            icon="el-icon-edit"
                            size="mini"
                            :disabled="single"
                            @click="handleUpdate"
                            v-hasPermi="['product:sopConfig:edit']"
                        >修改</el-button>
                    </el-col>
                    <right-toolbar :showSearch.sync="showSearch" @queryTable="getList"></right-toolbar>
        </el-row>-->
        </el-row>
        <el-row class="paddingRow">
            <el-table
                v-loading="loading"
                :data="sopConfigList"
                @selection-change="handleSelectionChange"
                border
            >
                <el-table-column type="selection" width="55" />
                <!-- <el-table-column label="id"  prop="id" /> -->
                <!-- <el-table-column
                    label="机种"
                    prop="modelName"
                    show-overflow-tooltip
                /> -->
                <!-- <el-table-column
                    label="料号"
                    prop="materialName"
                    show-overflow-tooltip
                /> -->
                <af-table-column
                    label="线别"
                    prop="lineName"
                    show-overflow-tooltip
                />
                <!-- <af-table-column label="站位序号" prop="stageName" />
                <af-table-column label="站位名" prop="processName" /> -->
                <af-table-column
                    label="MAC"
                    prop="macName"
                    show-overflow-tooltip
                />
                <af-table-column
                    label="SOP名称"
                    prop="sopName"
                    show-overflow-tooltip
                />
                <af-table-column label="版本" prop="version" />
                <af-table-column label="页码" prop="sopPage" />
                <el-table-column label="操作" min-width="120px" fixed="right">
                    <template slot-scope="scope">
                        <el-button
                            size="mini"
                            type="text"
                            icon="el-icon-reading"
                            @click="handlePlay(scope.row)"
                            >展示</el-button
                        >

                        <!-- <el-button
                            size="mini"
                            type="text"
                            icon="el-icon-s-promotion"
                            @click="handleUpdate(scope.row)"
                            v-hasPermi="['product:sopConfig:edit']"
                            >推送新SOP</el-button
                        > -->
                    </template>
                </el-table-column>
            </el-table>

            <pagination
                v-show="total > 0"
                :total="total"
                :page.sync="queryParams.pageNum"
                :limit.sync="queryParams.pageSize"
                @pagination="getList"
            />
        </el-row>
        <!-- 添加或修改sop配置对话框 -->
        <el-dialog
            :title="title"
            :visible.sync="open"
            width="500px"
            append-to-body
            class="formDialog"
            center
        >
            <el-form ref="form" :model="form" :rules="rules" label-width="80px">
                <!-- <el-form-item label="机种" prop="modelName">
                    <el-input
                        v-model="form.modelName"
                        :disabled="true"
                        size="mini"
                    />
                </el-form-item> -->
                <el-form-item label="线别名称" prop="lineName">
                    <el-input
                        v-model="form.lineName"
                        :disabled="true"
                        size="mini"
                    />
                </el-form-item>
                <el-form-item label="站位序号" prop="stageName">
                    <el-input
                        v-model="form.stageName"
                        :disabled="true"
                        size="mini"
                    />
                </el-form-item>
                <el-form-item label="站位名" prop="processName">
                    <el-input
                        v-model="form.processName"
                        :disabled="true"
                        size="mini"
                    />
                </el-form-item>
                <!-- <el-form-item label="SOP版本" prop="sopId">
                    <el-select v-model="form.sopId" placeholder="请选择SOP版本">
                        <el-option
                            v-for="item in sopOptions"
                            :key="item.sopId"
                            :label="item.version"
                            :value="item.sopId"
                        />
                    </el-select>
                </el-form-item> -->
                <el-form-item label="SOP文档" prop="pdfSopId">
                    <el-select
                        v-model="form.pdfSopId"
                        size="mini"
                        placeholder="请选择SOP文档"
                        filterable
                    >
                        <el-option
                            v-for="item in formPdfSopOptions"
                            :key="item.value"
                            :label="item.label"
                            :value="item.value"
                        />
                    </el-select>
                </el-form-item>
                <el-form-item label="SOP视频" prop="videoSopId">
                    <el-select
                        v-model="form.videoSopId"
                        size="mini"
                        placeholder="请选择SOP视频"
                        filterables
                    >
                        <el-option
                            v-for="item in formVideoSopOptions"
                            :key="item.value"
                            :label="item.label"
                            :value="item.value"
                        />
                    </el-select>
                </el-form-item>
                <el-form-item label="页码" prop="sopPage">
                    <el-input
                        v-model="form.sopPage"
                        size="mini"
                        clearable
                        placeholder="请输入页码"
                    />
                </el-form-item>
            </el-form>
            <div slot="footer" class="dialog-footer">
                <el-button type="primary" @click="submitForm">确 定</el-button>
                <el-button @click="cancel">取 消</el-button>
            </div>
        </el-dialog>
        <!-- 批量更换SOP -->
        <el-dialog
            width="80%"
            center
            title="一键推送"
            :visible.sync="massDialogVisible"
            @open="massDialogOpen"
            @close="massDialogClose"
        >
            <el-form
                ref="massForm"
                :model="massForm"
                inline
                label-width="80px"
                :rules="massFormRules"
            >
                <!-- <el-form-item label="机种" prop="modelId">
                    <el-select
                        v-model="massForm.modelId"
                        placeholder="请选择机种"
                        filterable
                        clear
                        size="mini"
                        @change="changeModel"
                        ref="modelSelect"
                    >
                        <el-option
                            v-for="item in modelOptions"
                            :key="item.modelId"
                            :label="item.modelName"
                            :value="item.modelId"
                        />

                    </el-select>
                </el-form-item> -->
                <!-- <el-form-item label="料号" prop="materialId">
                    <el-select
                        v-model="massForm.materialId"
                        clearable
                        filterable
                        ref="materialSelect"
                        @change="changeMaterial"
                        placeholder="请选择料号"
                        size="mini"
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
                        v-model="massForm.lineId"
                        clearable
                        filterable
                        @change="changeLine"
                        placeholder="请选择线别"
                        ref="lineSelect"
                        size="mini"
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
                        v-model="massForm.stageId"
                        clearable
                        filterable
                        @change="changeStage"
                        placeholder="请选择站位序号"
                        ref="stageSelect"
                        size="mini"
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
                        v-model="massForm.processId"
                        clearable
                        filterable
                        placeholder="请选择站位名"
                        @change="changeProcess"
                        size="mini"
                        ref="processSelect"
                    >
                        <el-option
                            v-for="(item, index) in processOption"
                            :key="index"
                            :label="item.processName"
                            :value="item.processId"
                        />
                    </el-select>
                </el-form-item>
                <el-form-item label="MAC" prop="macId">
                    <el-select
                        v-model="massForm.macId"
                        clearable
                        filterable
                        placeholder="请选择MAC"
                        @change="changeMac"
                        size="mini"
                        ref="macSelect"
                    >
                        <el-option
                            v-for="(item, index) in macOption"
                            :key="index"
                            :label="item.macName"
                            :value="item.macId"
                        />
                    </el-select>
                </el-form-item>
                <el-form-item label="SOP文档" prop="pdfSopId">
                    <el-select
                        clearable
                        size="mini"
                        v-model="massForm.pdfSopId"
                        placeholder="请选择SOP文档"
                        @change="changeSop"
                        filterable
                    >
                        <el-option
                            v-for="item in pdfSopOption"
                            :key="item.value"
                            :label="item.label"
                            :value="item.value"
                        />
                    </el-select>
                </el-form-item>
                <el-form-item label="SOP视频" prop="videoSopId">
                    <el-select
                        clearable
                        size="mini"
                        v-model="massForm.videoSopId"
                        placeholder="请选择SOP视频"
                        filterable
                    >
                        <el-option
                            v-for="item in videoSopOption"
                            :key="item.value"
                            :label="item.label"
                            :value="item.value"
                        />
                    </el-select>
                </el-form-item>
                <el-form-item>
                    <el-button
                        class="addButton"
                        type="success"
                        size="mini"
                        icon="el-icon-plus"
                        plain
                        @click="addToTable"
                        >添加SOP信息</el-button
                    >
                    <el-button
                        class="addButton"
                        type="danger"
                        size="mini"
                        icon="el-icon-delete"
                        plain
                        @click="removeInfoTableRow"
                        :disabled="materialTable.length === 0"
                        >删除SOP信息</el-button
                    >
                </el-form-item>
            </el-form>
            <el-table
                :data="materialTable"
                border
                @selection-change="changeInfoTable"
            >
                <el-table-column
                    type="selection"
                    min-width="55px"
                    show-overflow-tooltip
                />
                <!-- <el-table-column
                    label="料号"
                    prop="materialName"
                    show-overflow-tooltip
                /> -->
                <el-table-column
                    label="线体"
                    prop="lineName"
                    show-overflow-tooltip
                />
                <el-table-column
                    label="站位序号"
                    prop="stageName"
                    show-overflow-tooltip
                />
                <el-table-column
                    label="站位名"
                    prop="processName"
                    show-overflow-tooltip
                />
                <el-table-column
                    label="MAC"
                    prop="macName"
                    show-overflow-tooltip
                />
                <el-table-column label="SOP页码">
                    <template slot-scope="scope">
                        <el-input
                            v-model="materialTable[scope.$index].sopPage"
                            size="mini"
                            clearable
                        >
                            <template slot="append">页</template>
                        </el-input>
                    </template>
                </el-table-column>
                <el-table-column label="翻页间隔">
                    <template slot-scope="scope">
                        <el-input
                            v-model="materialTable[scope.$index].interval"
                            size="mini"
                            clearable
                        >
                            <template slot="append">秒</template>
                        </el-input>
                    </template>
                </el-table-column>
            </el-table>
            <div slot="footer" class="dialog-footer">
                <el-button type="primary" @click="submitMassForm"
                    >确 定</el-button
                >
                <el-button @click="massDialogVisible = false">取 消</el-button>
            </div>
        </el-dialog>
        <el-dialog
            class="pdfDialog"
            :modal="false"
            append-to-body
            fullscreen
            :visible.sync="pdfDialogVisible"
            style="height: 100vh"
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
            <!-- <iframe ref="pdfIframe" :key="pdfUrl" :src="pdfUrl" frameborder="0" width="100%" height="100%" @load="iframeLoaded" name="pdfIframe"></iframe> -->
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
                :key="videoKey"
                :url="videoUrl"
                id="configVideo"
                ref="videoPlayer"
            />
        </el-dialog>
    </div>
</template>

<script>
import {
    listSopConfig,
    delSopConfig,
    getSopConfigHt,
    getSignedSopList,
    updateSignedSopList,
    editSopConfig,
    selectMesSopGroupBySopGroupId,
    selectTemplateInfoByPartNo,
    getTerminalListByLineId,
    getSopPage
} from "@/api/product/sopConfig";
import { listLine } from "@/api/product/line";
import { listTerminal } from "@/api/product/terminal";
import {
    getStageInTerminal,
    getProcessInTerminal,
} from "@/api/product/terminal";
import VuePDF from "../sop/pdf";
import VideoPlayer from "../sop/video";
import { getPartNoList } from "@/api/product/partNo";
import { getMesModelOptions, templateList } from "@/api/product/oa";
import { noDataMixins } from "@/mixins";
import JsEncrypt from "jsencrypt";
import { getMacList } from "@/api/product/mac";
export default {
    name: "SopConfig",
    components: {
        VuePDF,
        VideoPlayer,
    },
    mixins: [noDataMixins],
    watch: {
        videoDialogVisible(val) {
            if (!val) {
                this.$refs.videoPlayer && this.$refs.videoPlayer.pause();
            } else {
                this.$refs.videoPlayer && this.$refs.videoPlayer.play();
            }
        },
        materialTable(val) {

        }
    },
    data() {
        return {
            notByAdd: true,
            pdfPages: [],
            pdfInterval: "10",
            // 遮罩层
            loading: true,
            // 选中数组
            ids: [],
            // 非单个禁用
            single: true,
            // 非多个禁用
            multiple: true,
            // 显示搜索条件
            showSearch: true,
            // 总条数
            total: 0,
            // sop配置表格数据
            sopConfigList: [],
            // 弹出层标题
            title: "",
            // 是否显示弹出层
            open: false,

            sopOptions: [],

            isPdfDialog: false,
            PDF_title: "预览PDF",
            PDF_open: false,
            lineOption: [],
            stageOption: [],
            processOption: [],
            macOption: [],
            pdfDialogVisible: false,
            videoDialogVisible: false,
            pdfUrl: "",
            videoUrl: "",
            // 查询参数
            queryParams: {
                pageNum: 1,
                pageSize: 10,
                modelId: null,
                materialId: null,
                lineId: null,
                stageId: null,
                processId: null,
                macId: null,
                sopId: null,
                status: null,
            },
            // 表单参数
            form: {},
            // 表单校验
            rules: {},
            massFormRules: {
                modelId: [
                    {
                        required: true,
                        message: "机种不能为空",
                        trigger: ["change", "blur"],
                    },
                ],
                // materialId: [
                //     {
                //         required: true,
                //         message: "料号不能为空",
                //         trigger: ["change", "blur"],
                //     },
                // ],
                // lineId: [
                //     {
                //         required: false,
                //         message: "线体不能为空",
                //         trigger: ["change", "blur"],
                //     },
                // ],
                // processId: [
                //     {
                //         required: true,
                //         message: "站位名不能为空",
                //         trigger: ["change", "blur"],
                //     },
                // ],
                // stageId: [
                //     {
                //         required: true,
                //         message: "站位序号不能为空",
                //         trigger: ["change", "blur"],
                //     },
                // ],
                // macId: [
                //     {
                //         required: true,
                //         message: "MAC不能为空",
                //         trigger: ["change", "blur"],
                //     },
                // ],
            },
            modelOptions: [],
            materialOption: [],
            massDialogVisible: false,
            massForm: {},
            materialTable: [],
            pdfSopOption: [],
            videoSopOption: [],
            formPdfSopOptions: [],
            formVideoSopOptions: [],
            infoTableRows: [],
            activeName: "first",
            jsEncrypt: null,
            pdfPassword: null,
            sopInfo: null,
            videoKey:"",

        };
    },
    created() {
        this.handleMesModelOptions();
        this.getList();
        this.getLine()
        this.jsEncrypt = new JsEncrypt();
        const prikey = `-----BEGIN PRIVATE KEY-----
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
        this.jsEncrypt.setPrivateKey(prikey);
    },
    methods: {
        opened() {},
        //展示文件或视频
        handlePlay(row) {
            selectMesSopGroupBySopGroupId(row.id).then((res) => {
                res.data.map((item) => {
                    if (item.type === "0") {

                        this.pdfPages =item.sopPage===''||!item.sopPage?[]:item.sopPage.split(",").map((str) => {
                            return str * 1;
                        });
                        this.pdfInterval = item.sopInterval
                            ? item.sopInterval+''
                            : "5";
                        this.pdfSizeList=item.pdfSizeList;
                        this.pdfPassword = this.jsEncrypt.decrypt(
                            item.passWord
                        );
                        this.$nextTick(() => {
                            this.pdfUrl =  process.env.VUE_APP_BASE_API + item.filePath;
                        });
                        this.pdfDialogVisible = true;
                    }
                    if (item.type === "1") {
                        this.$nextTick(() => {
                            this.videoKey="videoKey"+new Date().getDate()
                            this.videoUrl =  process.env.VUE_APP_BASE_API + item.filePath;
                            this.videoDialogVisible = true;
                        });
                    }
                });
            });
            //row.sopName.substring(sopNameLength - 3, sopNameLength) == "pdf"
        },
        showEsop(val) {
            this.PDF_open = true;
            this.$nextTick(() => {
                this.pdfUrl = val;
            });
        },

        /** 查询sop配置列表 */
        getList() {
            this.loading = true;
            listSopConfig(this.queryParams).then((response) => {
                this.sopConfigList = response.rows;
                this.total = response.total;
                this.loading = false;
            });
        },

        /** 查询部门下拉树结构 */
        getStationTree() {
            stationTreeSelect().then((response) => {
                this.deptOptions = response.data;
            });
        },
        // 筛选节点
        filterNode(value, data) {
            if (!value) return true;
            return data.label.indexOf(value) !== -1;
        },
        // 节点单击事件
        handleNodeClick(data) {
            this.queryParams.deptId = data.id;
            this.handleQuery();
        },

        // 取消按钮
        cancel() {
            this.open = false;
            this.reset();
        },
        // 表单重置
        reset() {
            this.form = {
                id: null,
                modelId: null,
                lineId: null,
                stageId: null,
                processId: null,
                macId: null,
                sopId: null,
                status: "0",
                remark: null,
                createBy: null,
                createTime: null,
                updateBy: null,
                updateTime: null,
            };
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
            this.ids = selection.map((item) => item.id);
            this.single = selection.length !== 1;
            this.multiple = !selection.length;
        },
        /** 新增按钮操作 */
        handleAdd() {
            this.reset();
            this.open = true;
            this.title = "添加sop配置";
        },
        /** 修改按钮操作 */
        handleUpdate(row) {
            this.open = true;
            this.title = "修改sop配置";
            this.form = JSON.parse(JSON.stringify(row));
            // selectTemplateInfoByPartNo({ stageId: row.stageId }).then(
            //     (res) => {
            //         this.sortOptions(
            //             res.rows,
            //             "formPdfSopOptions",
            //             "formVideoSopOptions"
            //         );
            //     }
            // );
        },
        /** 获取历史配置 */
        handleSopConfigHt(val1, val2, val3, val4) {
            getSopConfigHt(val1, val2, val3, val4).then((response) => {
                this.sopOptions = response.rows;
            });
        },
        /** 提交按钮 */
        submitForm() {
            this.$refs["form"].validate((valid) => {
                if (valid) {
                    if (this.form.id != null) {
                        this.form["sopGroupList"] = [
                            {
                                sopId: this.form.pdfSopId,
                                sopPage: this.form.sopPage,
                                type: "0",
                            },
                            {
                                sopId: this.form.videoSopId,
                                sopPage: "0",
                                type: "1",
                            },
                        ];
                        editSopConfig(this.form).then((response) => {
                            this.$modal.msgSuccess("修改成功");
                            this.open = false;
                            this.getList();
                        });
                    }
                }
            });
        },
        /** 删除按钮操作 */
        handleDelete(row) {
            const ids = row.id || this.ids;
            this.$modal
                .confirm('是否确认删除sop配置编号为"' + ids + '"的数据项？')
                .then(function () {
                    return delSopConfig(ids);
                })
                .then(() => {
                    this.getList();
                    this.$modal.msgSuccess("删除成功");
                })
                .catch(() => {});
        },
        /** 导出按钮操作 */
        handleExport() {
            this.download(
                "product/sopConfig/export",
                {
                    ...this.queryParams,
                },
                `sopConfig_${new Date().getTime()}.xlsx`
            );
        },
        //批量更换
        massReplace() {
            this.massDialogVisible = true;
        },
        // 获取机种信息
        handleMesModelOptions() {
            getMesModelOptions().then((response) => {
                this.modelOptions = response.rows;
            });
        },
        //机种change查询料号
        changeModel(val) {
            getPartNoList({ modelId: val }).then((res) => {
                this.materialOption = res.rows;
                // this.massForm.materialId = '';
            });
        },
        // //料号change查询线体
        // changeMaterial(val) {
        //     listLine({ materialId: val }).then((res) => {
        //         this.lineOption = res.rows;
        //     });
        //     if (val) {
        //         this.materialTable = [];
        //         // templateList({ materialId: val }).then((res) => {
        //         //     this.notByAdd=true;
        //         //     this.materialTable = res.rows;
        //         // });
        //         selectTemplateInfoByPartNo({ materialId: val }).then((res) => {
        //             this.sortOptions(
        //                 res.rows,
        //                 "pdfSopOption",
        //                 "videoSopOption"
        //             );
        //         });
        //     }
        // },
        //获取线体
        getLine() {
            listLine({ pageSize: 9999, pageNum: 1 }).then((res) => {
                this.lineOption = res.rows;
            });
        },
        //线别change查询站位序号
        changeLine(val) {
            this.materialTable = [];
            this.massForm.stageId = '';
            this.stageOption = [];
            this.massForm.processId = '';
            this.processOption = [];
            this.massForm.macId = '';
            this.macOption = [];
            if (val) {
                getStageInTerminal(val).then((res) => {
                    this.stageOption = res.rows;
                });
                selectTemplateInfoByPartNo({
                    lineId: val,
                }).then(
                    (res) => {
                        this.sortOptions(
                            res.rows,
                            "pdfSopOption",
                            "videoSopOption"
                        );
                    }
                );
                getMacList({ lineId: val }).then((res) => {
                    this.notByAdd = true;
                    this.materialTable = res.rows.map(item => {
                        if (this.sopInfo ) {
                            item.sopPage = this.sopInfo.sopPage
                            item.interval = this.sopInfo.sopInterval
                        }else {
                            item.sopPage = null
                            item.interval = null
                        }
                        return item
                    })
                });
            }else{
                this.massForm.stageId=null;
            }
        },
        //站位序号change查询站位名
        changeStage(val) {
            this.massForm.processId = '';
            this.processOption = [];
            this.massForm.macId = '';
            this.macOption = [];
            this.pdfSopOption = [];
            this.sopInfo = null
            this.videoSopOption = [];
            // if (val) {
                getProcessInTerminal(this.massForm.lineId, val).then((res) => {
                    this.processOption = res.rows;
                });
                getMacList({
                    lineId: this.massForm.lineId,
                    stageId: val
                }).then((res) => {
                    this.notByAdd = true;
                    this.materialTable = res.rows.map(item => {
                        if (this.sopInfo ) {
                            item.sopPage = this.sopInfo.sopPage
                            item.interval = this.sopInfo.sopInterval
                        }else {
                            item.sopPage = null
                            item.interval = null
                        }
                        return item
                    })
                });
            if (val) {
                // selectTemplateInfoByPartNo({
                //     stageId: val,
                //     lineId: this.massForm.lineId,
                //     modelId: this.massForm.modelId,
                //     materialId: this.massForm.materialId,
                // }).then(
                //     (res) => {
                //         this.sortOptions(
                //             res.rows,
                //             "pdfSopOption",
                //             "videoSopOption"
                //         );
                //     }
                // );
            }else{
                this.massForm.pdfSopId=null;
                this.massForm.videoSopId=null;
            }
        },
        //站位名change查询MAC
        changeProcess(val) {
            this.massForm.macId = '';
            this.macOption = [];
            // if (val) {
                // selectTemplateInfoByPartNo({
                //     lineId: this.massForm.lineId,
                //     stageId: this.massForm.stageId,
                //     processId: val,
                //     modelId: this.massForm.modelId,
                //     materialId: this.massForm.materialId,
                // }).then(
                //     (res) => {
                //         this.sortOptions(
                //             res.rows,
                //             "pdfSopOption",
                //             "videoSopOption"
                //         );
                //     }
                // );
                getMacList({
                    lineId: this.massForm.lineId,
                    stageId: this.massForm.stageId,
                    processId: val,
                }).then((res) => {
                    this.notByAdd = true;
                    this.materialTable = res.rows.map(item => {
                        if (this.sopInfo ) {
                            item.sopPage = this.sopInfo.sopPage
                            item.interval = this.sopInfo.sopInterval
                        }else {
                            item.sopPage = null
                            item.interval = null
                        }
                        return item
                    })
                    this.macOption = res.rows;
                });
            // }
        },
        // MAC change查询table
        changeMac(val) {
            // if (val) {
                getMacList({
                    lineId: this.massForm.lineId,
                    stageId: this.massForm.stageId,
                    processId: this.massForm.processId,
                    macId: val,
                }).then((res) => {
                    this.notByAdd = true;
                    this.materialTable = res.rows.map(item => {
                        if (this.sopInfo ) {
                            item.sopPage = this.sopInfo.sopPage
                            item.interval = this.sopInfo.sopInterval
                        }else {
                            item.sopPage = null
                            item.interval = null
                        }
                        return item
                    })
                });
            // }
        },
        async changeSop(val) {
            if (val) {
                const res = await getSopPage({processId: Number(this.massForm.processId), sopId: val})
                this.sopInfo = res?.rows?.[0]
                if (this.sopInfo) {
                    this.materialTable = this.materialTable.map(item => {
                        item.sopPage = this.sopInfo.sopPage
                        item.interval = this.sopInfo.sopInterval
                        return item
                    })
                }
            }
        },
        addToTable() {
            let validArr = [];
            this.$refs.massForm.validateField(
                [ "lineId"],
                (valid) => {
                    validArr.push(valid);
                    console.log(validArr);
                }
            );
            // 如果验证通过
            if (validArr.every((item) => item === "")) {
                if (
                    this.materialTable.filter((item) => {
                        return (
                            // item.modelId === this.massForm.modelId &&
                            // item.materialId === this.massForm.materialId &&
                            item.lineId === this.massForm.lineId &&
                            item.processId === this.massForm.processId &&
                            item.stageId === this.massForm.stageId &&
                            item.macId === this.massForm.macId
                        );
                    }).length != 0
                ) {
                    this.$modal.msgError("已有该项，请勿重复添加");
                    return false;
                } else {
                    // this.massForm["materialName"] =
                    //     this.$refs.materialSelect.selected.label;
                    this.massForm["lineName"] =
                        this.$refs.lineSelect.selected.label;
                    this.massForm["stageName"] =
                        this.$refs.stageSelect.selected.label;
                    this.massForm["processName"] =
                        this.$refs.processSelect.selected.label;
                    this.massForm["macName"] =
                        this.$refs.macSelect.selected.label;
                    this.materialTable.push(
                        JSON.parse(JSON.stringify(this.massForm))
                    );
                    this.notByAdd = false;
                    // 重置部分字段，保留机种料号
                    // let resetArr = ["lineId", "stageId", "processId", "macId"];
                    // resetArr.map((item) => {
                    //     this.$refs.massForm.fields
                    //         .find((f) => f.prop == item)
                    //         .resetField();
                    // });
                }
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
                this.materialTable = this.materialTable.filter(
                    (item, index) => {
                        let arrList = this.infoTableRows;
                        let flag = true;
                        arrList.map((arr) => {
                            if (
                                arr.lineId === item.lineId &&
                                arr.stageId === item.stageId &&
                                arr.macId === item.macId &&
                                arr.processId === item.processId
                            ) {
                                flag = false;
                            }
                        });
                        if (flag) {
                            return item;
                        }
                    }
                );
            }
        },
        massDialogOpen() {
            this.massForm = {
                // modelId: "",
                // materialId: "",
                lineId: "",
                processId: "",
                stageId: "",
                macId: "",
                pdfSopId: "",
                videoSopId: "",
            };
        },
        massDialogClose() {
            this.$refs.massForm.resetFields();
        },
        submitMassForm() {
            this.$refs.massForm.validate((valid) => {
                if (valid) {
                    this.$confirm(
                        `确定更换站位序号为${this.$refs.stageSelect.selectedLabel}的SOP吗?`,
                        "提示",
                        {
                            confirmButtonText: "确定",
                            cancelButtonText: "取消",
                            type: "warning",
                        }
                    ).then((res) => {
                        if (res === "confirm") {
                            let param = {
                                modelId: this.massForm.modelId,
                                materialId: this.massForm.materialId,
                                sopIdList: [],
                                terminalPageList: this.materialTable,
                            };
                            if (this.massForm.pdfSopId) {
                                param.sopIdList.push({
                                    sopId: this.massForm.pdfSopId,
                                    type: "0",
                                });
                            }
                            if (this.massForm.videoSopId) {
                                param.sopIdList.push({
                                    sopId: this.massForm.videoSopId,
                                    type: "1",
                                });
                            }
                            updateSignedSopList(param).then((res) => {
                                if (res.code === 200) {
                                    this.$modal.msgSuccess(res.msg);
                                } else {
                                    his.$modal.msgError(res.msg);
                                }
                                this.massDialogVisible = false;
                                this.getList();
                            });
                        } else {
                            this.$modal.msgError("已取消更换SOP");
                        }
                    });
                } else {
                    this.$modal.msgError("请按提示输入指定内容");
                }
            });
        },
        close_PDF_dialog() {
            this.pdfDialogVisible = false;
            this.pdfPages = [];
        },
        sortOptions(list, pdfOption, videoOption) {
            this[pdfOption] = [];
            this[videoOption] = [];
            if (list.length != 0) {
                list.map((item) => {
                    if (item.type === "0") {
                        this[pdfOption].push({
                            label: item.sopName,
                            value: item.sopId,
                        });
                    } else {
                        this[videoOption].push({
                            label: item.sopName,
                            value: item.sopId,
                        });
                    }
                });
            }
        },
        iframeLoaded(ev) {
            // console.log(ev.currentTarget.contentDocument);
            // const iframeDocument = this.$refs.pdfIframe.contentDocment || this.$refs.pdfIframe.contentWindow.document
            // console.log(iframeDocument,44);
            // console.log( this.$refs.pdfIframe.contentWindow,66)
        },
    },
};
</script>

<style lang="scss" scoped>
.pdfDialog ::v-deep {
    .el-dialog {
        height: 100vh;
        display: flex;
        flex-direction: column;
        margin-top: 0 !important;
        .el-dialog__body {
            height: calc(100vh - 150px);
        }
    }
}

::v-deep .el-dialog__header {
    padding: 10px 0;
}
::v-deep .el-dialog__body {
    flex: 1;
    overflow-y: auto;
    width: 100%;
}
::v-deep .el-select {
    width: 100%;
}
</style>
<style>
.videoDialog {
    pointer-events: none;
}
.videoDialog .el-dialog {
    pointer-events: auto;
}
</style>
