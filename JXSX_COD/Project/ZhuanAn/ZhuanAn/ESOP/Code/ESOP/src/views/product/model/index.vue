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
                @submit.native.prevent
            >
                <el-form-item label="机种名称" prop="modelName">
                    <el-input
                        v-model="queryParams.modelName"
                        placeholder="请输入机种名称"
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
                        size="mini"
                        plain
                        type="warning"
                        @click="resetQuery"
                        >重置</el-button
                    >
                    <el-button
                        type="success"
                        plain
                        icon="el-icon-plus"
                        size="mini"
                        @click="handleAdd"
                        v-hasPermi="['product:model:add']"
                        >新增</el-button
                    >
                    <el-button
                        type="info"
                        plain
                        icon="el-icon-download"
                        size="mini"
                        @click="handleExport"
                        v-hasPermi="['product:model:export']"
                        >导出</el-button
                    >
                </el-form-item>
            </el-form>
        </el-row>
        <el-row class="paddingRow">
            <el-table
                v-loading="loading"
                :data="modelList"
                @selection-change="handleSelectionChange"
                border
                stripe
            >
                <el-table-column type="selection" width="55" />
                <el-table-column label="机种编号" prop="modelId" />
                <el-table-column label="机种名称" prop="modelName" min-width="140px" />
                <el-table-column label="机种状态" prop="status">
                    <template slot-scope="scope">
                        <el-switch
                            v-model="scope.row.status"
                            active-value="0"
                            inactive-value="1"
                            @change="handleStatusChange(scope.row)"
                        />
                    </template>
                </el-table-column>
                <el-table-column label="备注" prop="remark" />
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
                            v-hasPermi="['product:model:edit']"
                            >修改</el-button
                        >
                        <!--          <el-button-->
                        <!--            size="mini"-->
                        <!--            type="text"-->
                        <!--            icon="el-icon-delete"-->
                        <!--            @click="handleDelete(scope.row)"-->
                        <!--            v-hasPermi="['product:model:remove']"-->
                        <!--          >删除</el-button>-->
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
        <!-- 添加或修改机种信息对话框 -->
        <el-dialog
            :title="title"
            :visible.sync="open"
            width="500px"
            append-to-body
        >
            <el-form ref="form" :model="form" :rules="rules" label-width="80px">
                <el-form-item label="机种名称" prop="modelName" style="width: 100%;">
                    <el-input
                        v-model="form.modelName"
                        placeholder="请输入机种名称"
                    />
                </el-form-item>
                <el-form-item label="备注" prop="remark" style="width: 100%;">
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
    listModel,
    getModel,
    delModel,
    addModel,
    updateModel,
    changeModelStatus,
} from "@/api/product/model";

export default {
    name: "Model",
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
            // 机种信息表格数据
            modelList: [],
            // 弹出层标题
            title: "",
            // 是否显示弹出层
            open: false,
            // 查询参数
            queryParams: {
                pageNum: 1,
                pageSize: 10,
                modelName: null,
                status: null,
            },
            // 表单参数
            form: {},
            // 表单校验
            rules: {
                modelName: [
                    {
                        required: true,
                        message: "机种名称不能为空",
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
        /** 查询机种信息列表 */
        getList() {
            this.loading = true;
            listModel(this.queryParams).then((response) => {
                this.modelList = response.rows;
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
                modelId: null,
                modelName: null,
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
            this.ids = selection.map((item) => item.modelId);
            this.single = selection.length !== 1;
            this.multiple = !selection.length;
        },
        /** 新增按钮操作 */
        handleAdd() {
            this.reset();
            this.open = true;
            this.title = "添加机种信息";
        },
        /** 修改按钮操作 */
        handleUpdate(row) {
            this.reset();
            const modelId = row.modelId || this.ids;
            getModel(modelId).then((response) => {
                this.form = response.data;
                this.open = true;
                this.title = "修改机种信息";
            });
        },
        /** 机种状态修改 */
        handleStatusChange(row) {
            let text = row.status === "0" ? "启用" : "停用";
            this.$modal
                .confirm('确认要"' + text + '""' + row.modelName + '"机种吗？')
                .then(function () {
                    return changeModelStatus(row.modelId, row.status);
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
                    if (this.form.modelId != null) {
                        updateModel(this.form).then((response) => {
                            this.$modal.msgSuccess("修改成功");
                            this.open = false;
                            this.getList();
                        });
                    } else {
                        addModel(this.form).then((response) => {
                            this.$modal.msgSuccess("新增成功");
                            this.open = false;
                            this.getList();
                        });
                    }
                }
            });
        },
        // /** 删除按钮操作 */
        // handleDelete(row) {
        //   const modelIds = row.modelId || this.ids;
        //   this.$modal.confirm('是否确认删除机种信息编号为"' + modelIds + '"的数据项？').then(function() {
        //     return delModel(modelIds);
        //   }).then(() => {
        //     this.getList();
        //     this.$modal.msgSuccess("删除成功");
        //   }).catch(() => {});
        // },
        /** 导出按钮操作 */
        handleExport() {
            this.download(
                "product/model/export",
                {
                    ...this.queryParams,
                },
                `model_${new Date().getTime()}.xlsx`
            );
        },
    },
};
</script>
