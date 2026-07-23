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
                <el-form-item label="站位序号" prop="stageId">
                    <el-select
                        v-model="queryParams.stageId"
                        placeholder="请选择站位序号"
                    >
                        <el-option
                            v-for="item in stageOptions"
                            :key="item.stageId"
                            :label="item.stageName"
                            :value="item.stageId"
                        ></el-option>
                    </el-select>
                </el-form-item>

                <el-form-item label="站位名" prop="processName">
                    <el-input
                        v-model="queryParams.processName"
                        placeholder="请输入站位名"
                        clearable
                        @keyup.enter.native="handleQuery"
                    />
                </el-form-item>
                <!--      <el-form-item label="站位序号id" prop="stageId">-->
                <!--        <el-input-->
                <!--          v-model="queryParams.stageId"-->
                <!--          placeholder="请输入站位序号id"-->
                <!--          clearable-->
                <!--          @keyup.enter.native="handleQuery"-->
                <!--        />-->
                <!--      </el-form-item>-->
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
                    <el-button
                        type="primary"
                        plain
                        icon="el-icon-plus"
                        size="mini"
                        @click="handleAdd"
                        v-hasPermi="['product:process:add']"
                        >新增</el-button
                    >
                    <el-button
                        type="warning"
                        plain
                        icon="el-icon-download"
                        size="mini"
                        @click="handleExport"
                        v-hasPermi="['product:process:export']"
                        >导出</el-button
                    >
                </el-form-item>
            </el-form>
        </el-row>

        <el-row class="paddingRow">
            <el-table
                v-loading="loading"
                :data="processList"
                @selection-change="handleSelectionChange"
                stripe
                border
            >
                <el-table-column type="selection" width="55" />
                <el-table-column label="站位名编号" prop="processId" />
                <el-table-column
                    label="站位名"
                    prop="processName"
                    min-width="150px"
                    show-overflow-tooltip
                />
                <el-table-column label="站位名类型" prop="processType">
                    <template slot-scope="scope">
                        <dict-tag
                            :options="dict.type.mes_process_operate_type"
                            :value="scope.row.processType"
                        />
                    </template>
                </el-table-column>

                <el-table-column label="状态" prop="status">
                    <template slot-scope="scope">
                        <el-switch
                            v-model="scope.row.status"
                            active-value="0"
                            inactive-value="1"
                            @change="handleStatusChange(scope.row)"
                        ></el-switch>
                    </template>
                </el-table-column>
                <el-table-column label="备注" prop="remark" />
                <el-table-column label="站位序号" prop="stageName" />
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
                            v-hasPermi="['product:process:edit']"
                            >修改</el-button
                        >
                        <el-button
                            size="mini"
                            type="text"
                            icon="el-icon-delete"
                            @click="handleDelete(scope.row)"
                            v-hasPermi="['product:process:remove']"
                            >删除</el-button
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
        <!-- 添加或修改站位名对话框 -->
        <el-dialog
            :title="title"
            :visible.sync="open"
            width="500px"
            append-to-body
        >
            <el-form ref="form" :model="form" :rules="rules" label-width="80px">
                <el-form-item
                    label="站位序号"
                    prop="stageId"
                    style="width: 100%"
                >
                    <el-select
                        v-model="form.stageId"
                        placeholder="请选择站位序号"
                    >
                        <el-option
                            v-for="item in stageOptions"
                            :key="item.stageId"
                            :label="item.stageName"
                            :value="item.stageId"
                        ></el-option>
                    </el-select>
                </el-form-item>

                <el-form-item
                    label="站位名"
                    prop="processName"
                    style="width: 100%"
                >
                    <el-input
                        v-model="form.processName"
                        placeholder="请输入站位名"
                    />
                </el-form-item>

                <el-form-item
                    label="站位名类型"
                    prop="processType"
                    style="width: 100%"
                >
                    <el-select
                        v-model="form.processType"
                        placeholder="请选择站位名类型"
                    >
                        <el-option
                            v-for="dict in dict.type.mes_process_operate_type"
                            :key="dict.value"
                            :label="dict.label"
                            :value="dict.value"
                            :disabled="dict.status == 1"
                        />
                    </el-select>
                </el-form-item>

                <el-form-item label="备注" prop="remark" style="width: 100%">
                    <el-input
                        v-model="form.remark"
                        type="textarea"
                        placeholder="请输入备注"
                    />
                </el-form-item>
            </el-form>
            <div slot="footer" class="dialog-footer">
                <el-button type="primary" @click="submitForm">确 定</el-button>
                <el-button @click="cancel">取 消</el-button>
            </div>
        </el-dialog>
    </div>
</template>

<script>
import {
    listProcess,
    getProcess,
    delProcess,
    addProcess,
    updateProcess,
    stageList,
    changeProcessStatus,
} from "@/api/product/process";

export default {
    dicts: ["mes_process_operate_type"],
    name: "Process",
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
            // 站位名表格数据
            processList: [],
            // 站位序号选项
            stageOptions: [],
            // 弹出层标题
            title: "",
            // 是否显示弹出层
            open: false,
            // 查询参数
            queryParams: {
                pageNum: 1,
                pageSize: 10,
                processName: null,
                processType: null,
                stageId: null,
                stageName: null,
                status: null,
            },
            // 表单参数
            form: {},
            // 表单校验
            rules: {
                stageId: [
                    {
                        required: true,
                        message: "站位序号不能为空",
                        trigger: "change",
                    },
                ],
                processName: [
                    {
                        required: true,
                        message: "站位名不能为空",
                        trigger: "blur",
                    },
                ],
                processType: [
                    {
                        required: true,
                        message: "站位名类型不能为空",
                        trigger: "change",
                    },
                ],
            },
        };
    },
    created() {
        this.getList();
        this.getStageOptions();
    },
    methods: {
        /** 查询站位名列表 */
        getList() {
            this.loading = true;
            listProcess(this.queryParams).then((response) => {
                this.processList = response.rows;
                this.total = response.total;
                this.loading = false;
            });
        },

        /** 获取站位序号列表 */
        getStageOptions() {
            stageList().then((response) => {
                this.stageOptions = response.data;
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
            this.getList();
        },
        /** 重置按钮操作 */
        resetQuery() {
            this.queryParams = {};
            this.handleQuery();
        },
        // 多选框选中数据
        handleSelectionChange(selection) {
            this.ids = selection.map((item) => item.processId);
            this.single = selection.length !== 1;
            this.multiple = !selection.length;
        },
        /** 新增按钮操作 */
        handleAdd() {
            this.reset();
            this.open = true;
            this.title = "添加站位名";
        },
        /** 修改按钮操作 */
        handleUpdate(row) {
            this.reset();
            const processId = row.processId || this.ids;
            getProcess(processId).then((response) => {
                this.form = response.data;
                this.open = true;
                this.title = "修改站位名";
            });
        },
        /** 状态修改 */
        handleStatusChange(row) {
            let text = row.status === "0" ? "启用" : "禁用";
            this.$modal
                .confirm(
                    '确认要"' + text + '""' + row.processName + '"站位名吗？'
                )
                .then(function () {
                    return changeProcessStatus(row.processId, row.status);
                })
                .then(() => {
                    this.$modal.msgSuccess(text + "成功");
                })
                .catch(function () {
                    row.status = row.status === "0" ? "1" : "0";
                });
        },

        /** 提交按钮 */
        submitForm() {
            this.$refs["form"].validate((valid) => {
                if (valid) {
                    if (this.form.processId != null) {
                        updateProcess(this.form).then((response) => {
                            this.$modal.msgSuccess("修改成功");
                            this.open = false;
                            this.getList();
                        });
                    } else {
                        addProcess(this.form).then((response) => {
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
            const processIds = row.processId || this.ids;
            this.$modal
                .confirm(
                    '是否确认删除站位名编号为"' + processIds + '"的数据项？'
                )
                .then(function () {
                    return delProcess(processIds);
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
                "product/process/export",
                {
                    ...this.queryParams,
                },
                `process_${new Date().getTime()}.xlsx`
            );
        },
    },
};
</script>
<style lang="scss" scoped>
::v-deep .el-select {
    width: 100%;
}
</style>
