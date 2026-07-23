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
                <el-form-item label="线别名称" prop="lineId">
                    <el-select
                        v-model="queryParams.lineId"
                        placeholder="请选择线别名称"
                        clearable
                        filterable
                        size="mini"
                        @change="changeLine"
                    >
                        <el-option
                            v-for="item in lineOptions"
                            :key="item.lineId"
                            :label="item.lineName"
                            :value="item.lineId"
                        />
                    </el-select>
                </el-form-item>
                <el-form-item label="站位序号" prop="stageId">
                    <el-select
                        v-model="queryParams.stageId"
                        placeholder="请选择站位序号"
                        @change="changeStage"
                        clearable
                        filterable
                        size="mini"
                    >
                        <el-option
                            v-for="item in stageOptions"
                            :key="item.stageId"
                            :label="item.stageName"
                            :value="item.stageId"
                        />
                    </el-select>
                </el-form-item>
                <el-form-item label="站位名" prop="processId">
                    <el-select
                        v-model="queryParams.processId"
                        placeholder="请选择站位名"
                        clearable
                        filterable
                        size="mini"
                    >
                        <el-option
                            v-for="item in processOptions"
                            :key="item.processId"
                            :label="item.processName"
                            :value="item.processId"
                        />
                    </el-select>
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
            <el-row class="mt20">
                <el-button
                    type="primary"
                    plain
                    icon="el-icon-plus"
                    size="mini"
                    @click="handleAdd"
                    v-hasPermi="['product:mac:add']"
                    >新增</el-button
                >
                <el-button
                    type="success"
                    plain
                    icon="el-icon-upload"
                    size="mini"
                    @click="handleImport"
                    >导入</el-button
                >
                <el-button
                    type="danger"
                    plain
                    icon="el-icon-delete"
                    size="mini"
                    v-hasPermi="['product:mac:remove']"
                    @click="handleDelete"
                    :disabled="!ids.length"
                    >删除</el-button
                >
            </el-row>
        </el-row>
        <el-row class="paddingRow">
            <el-table
                v-loading="loading"
                :data="terminalList"
                @selection-change="handleSelectionChange"
                border
                stripe
            >
                <el-table-column type="selection"></el-table-column>
                <el-table-column label="ID" prop="macId" />
                <el-table-column
                    label="线别名称"
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
                    label="MAC地址"
                    prop="macName"
                    min-width="180px"
                    show-overflow-tooltip
                />
                <el-table-column
                    label="MAC描述"
                    prop="macArea"
                    show-overflow-tooltip
                />
                <el-table-column label="状态" prop="status">
                    <template slot-scope="scope">
                        <el-switch
                            v-model="scope.row.status"
                            active-value="0"
                            inactive-value="1"
                            @change="handleStatusChange(scope.row)"
                        />
                    </template>
                </el-table-column>
                <el-table-column
                    label="操作"
                    class-name="small-padding fixed-width"
                >
                    <template slot-scope="scope">
                        <el-button
                            size="mini"
                            type="text"
                            icon="el-icon-edit"
                            @click="handleUpdate(scope.row)"
                            v-hasPermi="['product:mac:edit']"
                            >修改</el-button
                        >
                        <el-button
                            type="text"
                            plain
                            icon="el-icon-delete"
                            size="mini"
                            v-hasPermi="['product:mac:remove']"
                            @click="handleDelete(scope.row)"
                            >删除</el-button
                        >
                    </template>
                </el-table-column>
            </el-table>
            <pagination
                :total="total"
                :page.sync="queryParams.pageNum"
                :limit.sync="queryParams.pageSize"
                @pagination="getList"
            />
        </el-row>
        <!-- 添加或修改MAC对话框 -->
        <el-dialog
            :title="title"
            :visible.sync="open"
            append-to-body
            center
            @open="dialogOpen"
            @close="dialogClose"
        >
            <el-form ref="form" :model="form" :rules="rules" label-width="80px">
                <el-form-item label="线别名称" prop="lineId">
                    <el-select
                        v-model="form.lineId"
                        placeholder="请选择线别名称"
                        clearable
                        filterable
                        size="mini"
                    >
                        <el-option
                            v-for="item in mesLineOptions"
                            :key="item.lineId"
                            :label="item.lineName"
                            :value="item.lineId"
                        />
                    </el-select>
                </el-form-item>
                <el-form-item label="站位序号" prop="stageId">
                    <el-select
                        v-model="form.stageId"
                        placeholder="请选择站位序号"
                        @change="handleMesProcessOptionsByStageId"
                        clearable
                        filterable
                        size="mini"
                    >
                        <el-option
                            v-for="item in mesStageOptions"
                            :key="item.stageId"
                            :label="item.stageName"
                            :value="item.stageId"
                        />
                    </el-select>
                </el-form-item>
                <el-form-item label="站位名" prop="processId">
                    <el-select
                        v-model="form.processId"
                        placeholder="请选择站位名"
                        clearable
                        filterable
                        size="mini"
                    >
                        <el-option
                            v-for="item in mesProcessOptions"
                            :key="item.processId"
                            :label="item.processName"
                            :value="item.processId"
                        />
                    </el-select>
                </el-form-item>

                <el-form-item label="MAC" prop="macName" style="width: 100%">
                    <el-input
                        v-model="form.macName"
                        :rows="10"
                        placeholder="请输入mac地址"
                        clearable
                    />
                </el-form-item>
                <el-form-item
                    label="MAC描述"
                    prop="macArea"
                    style="width: 100%"
                >
                    <el-input
                        type="textarea"
                        v-model="form.macArea"
                        placeholder="请输入mac所在区域,例如：X栋X层X台"
                        clearable
                        autosize
                    />
                </el-form-item>
            </el-form>
            <div slot="footer" class="dialog-footer">
                <el-button type="primary" @click="submitForm" size="mini"
                    >确 定</el-button
                >
                <el-button @click="cancel" size="mini">取 消</el-button>
            </div>
        </el-dialog>
        <!-- 用户导入对话框 -->
        <el-dialog
            :title="upload.title"
            :visible.sync="upload.open"
            append-to-body
        >
            <el-form :model="upload.data" :rules="uploadRules" ref="uploadForm">
                <el-form-item label="线别名称" prop="lineId">
                    <el-select
                        v-model="upload.data.lineId"
                        placeholder="请选择线别名称"
                        clearable
                        filterable
                        size="mini"
                    >
                        <el-option
                            v-for="item in mesLineOptions"
                            :key="item.lineId"
                            :label="item.lineName"
                            :value="item.lineId"
                        />
                    </el-select>
                </el-form-item>

                <el-form-item label="站位序号" prop="stageId">
                    <el-select
                        v-model="upload.data.stageId"
                        placeholder="请选择站位序号"
                        @change="handleMesProcessOptionsByStageId"
                        clearable
                        filterable
                        size="mini"
                    >
                        <el-option
                            v-for="item in mesStageOptions"
                            :key="item.stageId"
                            :label="item.stageName"
                            :value="item.stageId"
                        />
                    </el-select>
                </el-form-item>

                <el-form-item label="站位名" prop="processId">
                    <el-select
                        v-model="upload.data.processId"
                        placeholder="请选择站位名"
                        clearable
                        filterable
                        size="mini"
                    >
                        <el-option
                            v-for="item in mesProcessOptions"
                            :key="item.processId"
                            :label="item.processName"
                            :value="item.processId"
                        />
                    </el-select>
                </el-form-item>
            </el-form>
            <el-upload
                ref="upload"
                :limit="1"
                accept=".xlsx, .xls"
                :headers="upload.headers"
                :action="upload.url + '?updateSupport=' + upload.updateSupport"
                :disabled="upload.isUploading"
                :on-progress="handleFileUploadProgress"
                :on-success="handleFileSuccess"
                :auto-upload="false"
                :data="upload.data"
                drag
            >
                <i class="el-icon-upload"></i>
                <div class="el-upload__text">
                    将文件拖到此处，或
                    <em>点击上传</em>
                </div>
                <div class="el-upload__tip" slot="tip">
                    <!-- <el-checkbox
                        v-model.trim="upload.updateSupport"
                    />是否更新已经存在的用户数据 -->
                    <el-link
                        type="info"
                        style="font-size: 12px"
                        @click="importTemplate"
                        >下载模板</el-link
                    >
                </div>
                <div class="el-upload__tip" style="color: red" slot="tip">
                    提示：仅允许导入“xls”或“xlsx”格式文件！
                </div>
            </el-upload>
            <div slot="footer" class="dialog-footer">
                <el-button type="primary" @click="submitFileForm"
                    >确 定</el-button
                >
                <el-button @click="upload.open = false">取 消</el-button>
            </div>
        </el-dialog>
    </div>
</template>

<script>
import {
    getMesLineOptions,
    getMesStageOptions,
    getMesProcessOptionsByStageId,
    getLineInTerminal,
    getStageInTerminal,
    getProcessInTerminal,
} from "@/api/product/terminal";
import { listProcess } from "@/api/product/process";
import { getToken } from "@/utils/auth";
import {
    getMacList,
    getMacInfo,
    insertMac,
    updateMac,
    removeMac,
    changeStatus,
    importTemplate,
} from "@/api/product/mac";
let lineId = "";
export default {
    name: "Terminal",
    dicts: ["mes_enabled"],
    data() {
        return {
            terminalNum: 1,
            cascaderData: [],
            props: {
                lazy: true,
                lazyLoad(node, resolve) {
                    const { level, value } = node;
                    console.log(node);
                    if (level === 0) {
                        getLineInTerminal().then((res) => {
                            var nodes = [];
                            res.rows.map((item) => {
                                nodes.push({
                                    value: item.lineId,
                                    label: item.lineName,
                                    //leaf: level >= 2
                                });
                            });
                            resolve(nodes);
                        });
                    }

                    if (level === 1) {
                        lineId = value;
                        getStageInTerminal(value).then((res) => {
                            var nodes = [];
                            res.rows.map((item) => {
                                nodes.push({
                                    value: item.stageId,
                                    label: item.stageName,
                                    //leaf: level >= 2
                                });
                            });
                            resolve(nodes);
                        });
                    }
                    if (level === 2) {
                        getProcessInTerminal(lineId, value).then((res) => {
                            var nodes = [];
                            res.rows.map((item) => {
                                nodes.push({
                                    value: item.processId,
                                    label: item.processName,
                                    leaf: level >= 2,
                                });
                            });
                            resolve(nodes);
                        });
                    }
                },
            },
            // 用户导入参数
            upload: {
                data: {},
                // 是否显示弹出层（用户导入）
                open: false,
                // 弹出层标题（用户导入）
                title: "",
                // 是否禁用上传
                isUploading: false,
                // 是否更新已经存在的用户数据
                updateSupport: 0,
                // 设置上传的请求头部
                headers: { Authorization: "Bearer " + getToken() },
                // 上传的地址
                url:
                    process.env.VUE_APP_BASE_API +
                    "/product/mac/importMacExcel",
            },
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
            // MAC表格数据
            terminalList: [],
            lineOptions: [],
            stageOptions: [],
            processOptions: [],
            mesLineOptions: [],
            mesStageOptions: [],
            mesProcessOptions: [],

            // 弹出层标题
            title: "",
            // 是否显示弹出层
            open: false,
            // 查询参数
            queryParams: {
                pageNum: 1,
                pageSize: 10,
                terminalName: null,
                lineId: null,
                stageId: null,
                processId: null,
                status: null,
            },
            // 表单参数
            form: {},
            // 表单校验
            rules: {
                lineId: [
                    {
                        required: true,
                        message: "线别名称不能为空",
                        trigger: "change",
                    },
                ],
                stageId: [
                    {
                        required: true,
                        message: "站位序号不能为空",
                        trigger: "change",
                    },
                ],
                processId: [
                    {
                        required: true,
                        message: "站位名不能为空",
                        trigger: "change",
                    },
                ],
                macName: [
                    {
                        required: true,
                        message: "请输入mac地址",
                        trigger: "blur",
                    },
                    {
                        pattern: /^([0-9A-Fa-f]{2}[:-]){5}([0-9A-Fa-f]{2})$/,
                        message: "请输入正确的MAC地址",
                    },
                ],
            },
            uploadRules: {
                lineId: [
                    {
                        required: true,
                        message: "线别不能为空",
                        trigger: "change",
                    },
                ],
                stageId: [
                    {
                        required: true,
                        message: "站位序号不能为空",
                        trigger: "change",
                    },
                ],
                processId: [
                    {
                        required: true,
                        message: "站位名不能为空",
                        trigger: "change",
                    },
                ],
            },
        };
    },
    created() {
        this.getList();
        this.handleMesLineOptions();
        this.handleMesStageOptions();
        this.listProcess();
    },
    methods: {
        dialogOpen() {
            this.form = {
                lineId: null,
                stageId: null,
                processId: null,
                macName: null,
                macArea: null,
            };
        },
        dialogClose(){
            this.$refs.form.resetFields();
        },
        // 状态修改
        handleStatusChange(row) {
            let action = row.status === "0" ? "启用" : "停用";
            let that = this;
            this.$confirm(
                "确认要" + action + '"' + row.macName + '"吗?',
                "警告",
                {
                    confirmButtonText: "确定",
                    cancelButtonText: "取消",
                    type: "warning",
                    center: true,
                }
            )
                .then(() => {
                    changeStatus({
                        macId: row.macId,
                        status: row.status,
                    }).then((res) => {
                        this.getList();
                        this.$modal.msgSuccess(action + "成功");
                    });
                })
                .catch(() => {
                    this.$message({
                        type: "info",
                        message: `已取消${action}`,
                    });
                    row.status = row.status === "0" ? "1" : "0";
                });
        },
        /** 查询有效的 线别 MesLine */
        handleMesLineOptions() {
            getMesLineOptions().then((res) => {
                this.mesLineOptions = res.rows;
                this.lineOptions = res.rows;
            });
        },
        /** 查询有效的 站位序号 MesStage */
        handleMesStageOptions() {
            getMesStageOptions().then((response) => {
                this.mesStageOptions = response.rows;
                this.stageOptions = response.rows;
            });
        },
        listProcess() {
            listProcess({ pageSize: 999, pageNum: 1 }).then((res) => {
                this.processOptions = res.rows;
            });
        },
        /** 查询有效的 站位名 MesProcess */
        handleMesProcessOptionsByStageId(val, flag) {
            if (!flag) {
                this.form.processId = null;
                this.upload.data.processId = null;
            }
            if (val) {
                getMesProcessOptionsByStageId(val).then((response) => {
                    this.mesProcessOptions = response.rows;
                });
            }
        },
        changeLine(val) {
            this.queryParams.stageId = null;
            this.queryParams.processId = null;
            if (val) {
                getStageInTerminal(val).then((res) => {
                    this.stageOptions = res.rows;
                });
            }
        },
        changeStage(val) {
            this.queryParams.processId = null;
            if (val) {
                getProcessInTerminal(this.queryParams.lineId, val).then(
                    (response) => {
                        this.processOptions = response.rows;
                    }
                );
            } else {
                listProcess({ pageSize: 999, pageNum: 1 }).then((res) => {
                    this.processOptions = res.rows;
                });
            }
        },
        /** 查询MAC列表 */
        getList() {
            this.loading = true;
            getMacList(this.queryParams)
                .then((response) => {
                    this.terminalList = response.rows;
                    this.total = response.total;
                    this.loading = false;
                })
                .catch((err) => {
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
            this.form = {};
            this.resetForm("form");
        },
        /** 搜索按钮操作 */
        handleQuery() {
            this.queryParams.pageNum = 1;
            // this.queryParams.lineId = this.cascaderData[0];
            // this.queryParams.stageId = this.cascaderData[1];
            // this.queryParams.processId = this.cascaderData[2];
            this.getList();
        },
        /** 重置按钮操作 */
        resetQuery() {
            this.resetForm("queryForm");
            this.cascaderData = [];
            this.handleQuery();
        },

        // 多选框选中数据
        handleSelectionChange(selection) {
            this.ids = selection.map((item) => item.macId);
            this.single = selection.length !== 1;
            this.multiple = !selection.length;
        },
        /** 新增按钮操作 */
        handleAdd() {
            this.reset();
            this.open = true;
            this.title = "添加MAC";
        },
        /** 修改按钮操作 */
        handleUpdate(row) {
            this.reset();
            const macId = row.macId || this.ids;
            getMacInfo(macId).then((response) => {
                this.open = true;
                this.$nextTick(() => {
                    this.form = response.data;
                });
                this.handleMesProcessOptionsByStageId(
                    response.data.stageId,
                    true
                );
                this.title = "修改MAC";
            });
        },
        /** 提交按钮 */
        submitForm() {
            this.$refs["form"].validate((valid) => {
                if (valid) {
                    if (this.form.macId != null) {
                        updateMac(this.form).then((response) => {
                            this.$modal.msgSuccess("修改成功");
                            this.open = false;
                            this.getList();
                        });
                    } else {
                        insertMac(this.form).then((response) => {
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
            const macIds = row.macId || this.ids;
            this.$modal
                .confirm('是否确认删除MAC编号为"' + macIds + '"的数据项？')
                .then(() => {
                    removeMac(macIds)
                        .then(() => {
                            this.getList();
                            this.$modal.msgSuccess("删除成功");
                        })
                        .catch(() => {});
                });
        },
        /** 导出按钮操作 */
        handleExport() {
            this.download(
                "product/terminal/export",
                {
                    ...this.queryParams,
                },
                `terminal_${new Date().getTime()}.xlsx`
            );
        },
        // 导入
        handleImport() {
            this.upload.open = true;
        },
        // 文件上传中处理
        handleFileUploadProgress(event, file, fileList) {
            this.upload.isUploading = true;
        },
        // 文件上传成功处理
        handleFileSuccess(response, file, fileList) {
            this.upload.open = false;
            this.upload.isUploading = false;
            this.$refs.upload.clearFiles();
            this.$alert(response.msg, "导入结果", {
                dangerouslyUseHTMLString: true,
            });
            this.getList();
        },
        importTemplate() {
            this.download(
                "product/mac/downloadTemplate",
                {},
                `template_${new Date().getTime()}.xlsx`
            );
        },
        // 提交上传文件
        submitFileForm() {
            this.$refs.uploadForm.validate((valid) => {
                if (valid) {
                    this.$refs.upload.submit();
                } else {
                    this.$modal.msgError("请按提示输入指定内容");
                }
            });
        },
        //站点次序
        handleTerminalSequenceChange(value) {
            console.log(value);
        },
    },
};
</script>
<style lang="scss" scoped>
.mt20 {
    margin-top: 10px;
}
</style>
