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
                <el-form-item label="sop名称" prop="sopName">
                    <el-input
                        v-model="queryParams.sopName"
                        placeholder="请输入sop名称"
                        clearable
                        @keyup.enter.native="handleQuery"
                    />
                </el-form-item>
                <el-form-item label="版本" prop="version">
                    <el-input
                        v-model="queryParams.version"
                        placeholder="请输入版本"
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
                        >搜索</el-button
                    >
                    <el-button
                        icon="el-icon-refresh"
                        size="mini"
                        @click="resetQuery"
                        >重置</el-button
                    >
                </el-form-item>
            </el-form>
        </el-row>
        <el-row class="paddingRow">
            <el-table
                v-loading="loading"
                border
                :data="sopList"
                @selection-change="handleSelectionChange"
            >
                <!-- <el-table-column type="selection" width="55" align="center" /> -->
                <el-table-column label="sop编号" align="center" prop="sopId" />
                <el-table-column
                    label="sop名称"
                    align="center"
                    prop="sopName"
                    show-overflow-tooltip
                />
                <el-table-column label="版本" align="center" prop="version" />
                <!-- <el-table-column label="类型" align="center" prop="type" /> -->
                <el-table-column
                    label="文件路径"
                    align="center"
                    prop="filePath"
                    show-overflow-tooltip
                />
                <!-- <el-table-column label="服务器路径" align="center" prop="url" /> -->
                <el-table-column label="状态" align="center" prop="status">
                    <template slot-scope="scope">
                        <dict-tag
                            :options="dict.type.mes_enabled"
                            :value="scope.row.status"
                        />
                    </template>
                </el-table-column>
                <!-- <el-table-column label="备注" align="center" prop="remark" /> -->
                <el-table-column
                    label="操作"
                    align="center"
                    class-name="small-padding fixed-width"
                >
                    <template slot-scope="scope">
                        <el-button
                            size="mini"
                            type="text"
                            icon="el-icon-reading"
                            @click="handlePlay(scope.row)"
                            >展示</el-button
                        >
                    </template>
                </el-table-column>
            </el-table>

            <pagination
                v-show="total > 10"
                :total="total"
                :page.sync="queryParams.pageNum"
                :limit.sync="queryParams.pageSize"
                @pagination="getList"
            />
        </el-row>
        <!-- 添加或修改sop对话框 -->
        <el-dialog :title="title" :visible.sync="open" width="500px">
            <el-form ref="form" :model="form" :rules="rules" label-width="80px">
                <el-form-item label="sop名称" prop="sopName">
                    <el-input
                        v-model="form.sopName"
                        placeholder="请输入sop名称"
                    />
                </el-form-item>
                <el-form-item label="版本" prop="version">
                    <el-input v-model="form.version" placeholder="请输入版本" />
                </el-form-item>
                <el-form-item label="备注" prop="remark">
                    <el-input
                        v-model="form.remark"
                        type="textarea"
                        placeholder="请输入内容"
                    />
                </el-form-item>
            </el-form>
            <div slot="footer" class="dialog-footer">
                <el-button type="primary" @click="submitForm">确 定</el-button>
                <el-button @click="cancel">取 消</el-button>
            </div>
        </el-dialog>
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
import { listSop, getSop, delSop, addSop, updateSop } from "@/api/product/sop";
import {
    selectMesSopGroupBySopGroupId,
} from "@/api/product/sopConfig";
import VuePDF from "./pdf";
import VideoPlayer from "./video";
import { noDataMixins } from "@/mixins";
export default {
    name: "Sop",
    dicts: ["mes_enabled"],
    components: {
        VuePDF,
        VideoPlayer,
    },
    mixins: [noDataMixins],
    data() {
        return {
            isPdfDialog: false,
            PDF_title: "预览PDF",
            PDF_open: false,
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
            // sop表格数据
            sopList: [],
            // 弹出层标题
            title: "",
            // 是否显示弹出层
            open: false,

            url: "",

            pdfUrl: "",
            // 查询参数
            queryParams: {
                pageNum: 1,
                pageSize: 10,
                sopName: null,
                version: null,
                type: null,
                filePath: null,
                status: null,
            },
            // 表单参数
            form: {},
            // 表单校验
            rules: {
                sopName: [
                    {
                        required: true,
                        message: "sop名称不能为空",
                        trigger: "blur",
                    },
                ],
                version: [
                    {
                        required: true,
                        message: "版本不能为空",
                        trigger: "blur",
                    },
                ],
            },
        };
    },
    created() {
        this.getList();
    },
    methods: {
        opened() {
            console.log(this.pdfUrl);
        },
        //展示文件或视频
        handlePlay(row) {
            this.isPdfDialog = row.type === "0";
            this.showEsop(row);
        },
      async  showEsop(row) {
            if (this.isPdfDialog) {

                this.pdfPages =
                    row.sopPage === "" || !row.sopPage
                        ? []
                        : row.sopPage.split(",").map((str) => {
                              return str * 1;
                          });
                this.pdfPassword = this.jsEncrypt.decrypt(row.passWord);
                this.pdfInterval = row.sopInterval || "10";
                // let res= await selectMesSopGroupBySopGroupId(row.sopId)
                // this.pdfSizeList =res.rows.find(item=>item.type==='0').pdfSizeList
                this.pdfSizeList = row.pdfSizeList;
                this.pdfDialogVisible = true;
                this.$nextTick(() => {
                    this.pdfUrl = process.env.VUE_APP_BASE_API + row.filePath;
                });
            } else {
                this.$nextTick(() => {
                    this.videoUrl =
                        process.env.VUE_APP_BASE_API + item.filePath;
                    this.videoDialogVisible = true;
                });
            }
        },
        /** 查询sop列表 */
        getList() {
            this.loading = true;
            listSop(this.queryParams).then((response) => {
                this.sopList = response.rows;
                this.total = response.total;
                this.loading = false;
            });
        },
        // 取消按钮
        cancel() {
            this.open = false;
            this.reset();
        },
        // 表单重置
        reset() {
            this.form = {
                sopId: null,
                sopName: null,
                version: null,
                type: null,
                filePath: null,
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
            this.ids = selection.map((item) => item.sopId);
            this.single = selection.length !== 1;
            this.multiple = !selection.length;
        },
        /** 新增按钮操作 */
        handleAdd() {
            this.reset();
            this.open = true;
            this.title = "添加sop";
        },
        /** 修改按钮操作 */
        handleUpdate(row) {
            this.reset();
            const sopId = row.sopId || this.ids;
            getSop(sopId).then((response) => {
                this.form = response.data;
                this.open = true;
                this.title = "修改sop";
            });
        },
        /** 提交按钮 */
        submitForm() {
            this.$refs["form"].validate((valid) => {
                if (valid) {
                    if (this.form.sopId != null) {
                        updateSop(this.form).then((response) => {
                            this.$modal.msgSuccess("修改成功");
                            this.open = false;
                            this.getList();
                        });
                    } else {
                        addSop(this.form).then((response) => {
                            this.$modal.msgSuccess("新增成功");
                            this.open = false;
                            this.getList();
                        });
                    }
                }
            });
        },
        /** 删除按钮操作 */
        handleDelete(row) {
            const sopIds = row.sopId || this.ids;
            this.$modal
                .confirm('是否确认删除sop编号为"' + sopIds + '"的数据项？')
                .then(function () {
                    return delSop(sopIds);
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
                "product/sop/export",
                {
                    ...this.queryParams,
                },
                `sop_${new Date().getTime()}.xlsx`
            );
        },
    },
};
</script>
<style lang="scss" scoped>
::v-deep .el-dialog {
    height: 100vh;
    display: flex;
    flex-direction: column;
}
::v-deep .el-dialog__header {
    padding: 0 0 10px 0;
}
::v-deep .el-dialog__body {
    flex: 1;
    overflow-y: auto;
    width: 100%;
}
.el-dialog {
    margin-top: 0 !important;
}
::v-deep .el-dialog:not(.is-fullscreen) {
    margin-top: 0 !important;
}
</style>
