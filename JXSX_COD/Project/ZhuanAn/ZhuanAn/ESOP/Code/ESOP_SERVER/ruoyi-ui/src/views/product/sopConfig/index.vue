<template>
    <div class="app-container">
        <el-form
            :model="queryParams"
            ref="queryForm"
            size="small"
            :inline="true"
            v-show="showSearch"
            label-width="68px"
        >
            <el-form-item label="机种" prop="modelName">
                <el-input
                    v-model="queryParams.modelName"
                    placeholder="请输入机种名称"
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
                <el-button type="primary" icon="el-icon-search" size="mini" @click="handleQuery">搜索</el-button>
                <el-button icon="el-icon-refresh" size="mini" @click="resetQuery">重置</el-button>
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

        <el-table
            v-loading="loading"
            :data="sopConfigList"
            @selection-change="handleSelectionChange"
        >
            <el-table-column type="selection" width="55" align="center" />
            <!-- <el-table-column label="id" align="center" prop="id" /> -->
            <el-table-column label="机种名称" align="center" prop="modelName" />
            <el-table-column label="线别名称" align="center" prop="lineName" />
            <el-table-column label="区段名称" align="center" prop="stageName" />
            <el-table-column label="制程名称" align="center" prop="processName" />
            <!-- <el-table-column label="工站 id" align="center" prop="terminalId" /> -->
            <el-table-column label="SOP名称" align="center" prop="sopName" />
            <el-table-column label="版本" align="center" prop="version" />
            <!-- <el-table-column label="备注" align="center" prop="remark" /> -->
            <el-table-column label="操作" align="center" class-name="small-padding fixed-width">
                <template slot-scope="scope">
                    <el-button
                        size="mini"
                        type="text"
                        icon="el-icon-reading"
                        @click="handlePlay(scope.row)"
                    >展示</el-button>

                    <el-button
                        size="mini"
                        type="text"
                        icon="el-icon-edit"
                        @click="handleUpdate(scope.row)"
                        v-hasPermi="['product:sopConfig:edit']"
                    >修改</el-button>
                </template>
            </el-table-column>
        </el-table>

        <pagination
            v-show="total>0"
            :total="total"
            :page.sync="queryParams.pageNum"
            :limit.sync="queryParams.pageSize"
            @pagination="getList"
        />

        <!-- 添加或修改sop配置对话框 -->
        <el-dialog
            :title="title"
            :visible.sync="open"
            width="500px"
            append-to-body
            class="formDialog"
        >
            <el-form ref="form" :model="form" :rules="rules" label-width="80px">
                <el-form-item label="机种" prop="modelName">
                    <el-input v-model="form.modelName" :disabled="true" />
                </el-form-item>
                <el-form-item label="线别名称" prop="lineName">
                    <el-input v-model="form.lineName" :disabled="true" />
                </el-form-item>
                <el-form-item label="区段名称" prop="stageName">
                    <el-input v-model="form.stageName" :disabled="true" />
                </el-form-item>
                <el-form-item label="制程名称" prop="processName">
                    <el-input v-model="form.processName" :disabled="true" />
                </el-form-item>
                <el-form-item label="SOP版本" prop="sopId">
                    <el-select v-model="form.sopId" placeholder="请选择SOP版本">
                        <el-option
                            v-for="item in sopOptions"
                            :key="item.sopId"
                            :label="item.version"
                            :value="item.sopId"
                        ></el-option>
                    </el-select>
                </el-form-item>
            </el-form>
            <div slot="footer" class="dialog-footer">
                <el-button type="primary" @click="submitForm">确 定</el-button>
                <el-button @click="cancel">取 消</el-button>
            </div>
        </el-dialog>

        <el-dialog
            :visible.sync="PDF_open"
            fullscreen
            @opened="opened"
            append-to-body
        >
            <VuePDF :pdfUrl="pdfUrl" ref="VuePdf" v-if="isPdfDialog" />
            <VideoPlayer :url="pdfUrl" v-if="!isPdfDialog" />
        </el-dialog>
    </div>
</template>

<script>
import {
    listSopConfig,
    getSopConfig,
    delSopConfig,
    addSopConfig,
    updateSopConfig,
    getSopConfigHt,viewSopInfo
} from "@/api/product/sopConfig";
import VuePDF from "../sop/pdf";
import VideoPlayer from "../sop/video";

export default {
    name: "SopConfig",
    components: {
        VuePDF,
        VideoPlayer
    },
    data() {
        return {
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
            lineOptions: undefined,
            pdfUrl: "",

            // 查询参数
            queryParams: {
                pageNum: 1,
                pageSize: 10,
                modelId: null,
                lineId: null,
                stageId: null,
                processId: null,
                terminalId: null,
                sopId: null,
                status: null
            },
            // 表单参数
            form: {},
            // 表单校验
            rules: {
                sopId: [
                    {
                        required: true,
                        message: "sop id不能为空",
                        trigger: "blur"
                    }
                ]
            }
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

            console.log(row.filePath)
            let viewfile
            viewSopInfo(row.filePath).then(response =>{
                 viewfile = response.data;
            });

            let sopNameLength = row.sopName.length;
            if ( row.sopName.substring(sopNameLength - 3, sopNameLength) == "pdf" )
            {
                this.isPdfDialog = true;
                //this.showEsop(row.url);
                this.showEsop(viewfile);
            } 
            else 
            {
                this.isPdfDialog = false;
                //this.showEsop(row.url);
                this.showEsop(viewfile);
            }
            //this.urlPath = row.url;
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
            listSopConfig(this.queryParams).then(response => {
                this.sopConfigList = response.rows;
                this.total = response.total;
                this.loading = false;
            });
        },

        /** 查询部门下拉树结构 */
        getStationTree() {
            stationTreeSelect().then(response => {
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
                terminalId: null,
                sopId: null,
                status: "0",
                remark: null,
                createBy: null,
                createTime: null,
                updateBy: null,
                updateTime: null
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
            this.ids = selection.map(item => item.id);
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
            this.reset();
            this.handleSopConfigHt(
                row.modelId,
                row.lineId,
                row.stageId,
                row.processId
            );
            const id = row.id || this.ids;
            getSopConfig(id).then(response => {
                this.form = response.data;
                this.open = true;
                this.title = "修改sop配置";
            });
        },
        /** 获取历史配置 */
        handleSopConfigHt(val1, val2, val3, val4) {
            getSopConfigHt(val1, val2, val3, val4).then(response => {
                this.sopOptions = response.rows;
            });
        },
        /** 提交按钮 */
        submitForm() {
            this.$refs["form"].validate(valid => {
                if (valid) {
                    if (this.form.id != null) {
                        updateSopConfig(this.form,).then(response => {
                            this.$modal.msgSuccess("修改成功");
                            this.open = false;
                            this.getList();
                        });
                    } else {
                        addSopConfig(this.form).then(response => {
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
            const ids = row.id || this.ids;
            this.$modal
                .confirm('是否确认删除sop配置编号为"' + ids + '"的数据项？')
                .then(function() {
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
                    ...this.queryParams
                },
                `sopConfig_${new Date().getTime()}.xlsx`
            );
        }
    }
};
</script>


<style lang="scss" scoped>
 ::v-deep {
    
 }
.pdfDialog {
    .el-dialog {
        height: 100vh;
        display: flex;
        flex-direction: column;
        margin-top: 0 !important;
    }
  
}

::v-deep .el-dialog__header {
    padding: 0 0 10px 0;
}
::v-deep .el-dialog__body {
    flex: 1;
    overflow-y: auto;
    width: 100%;
}

</style>
