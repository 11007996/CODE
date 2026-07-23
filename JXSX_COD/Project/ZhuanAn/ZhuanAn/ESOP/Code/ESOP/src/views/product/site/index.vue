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
                <el-form-item label="厂区名称" prop="siteName">
                    <el-input
                        v-model="queryParams.siteName"
                        placeholder="请输入厂区名称"
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
                    <el-button
                        type="primary"
                        plain
                        icon="el-icon-plus"
                        size="mini"
                        @click="handleAdd"
                        v-hasPermi="['product:site:add']"
                        >新增</el-button
                    >
                    <el-button
                        type="warning"
                        plain
                        icon="el-icon-download"
                        size="mini"
                        @click="handleExport"
                        v-hasPermi="['product:site:export']"
                        >导出</el-button
                    >
                </el-form-item>
            </el-form>
        </el-row>

        <el-row class="paddingRow">
            <el-table
                v-loading="loading"
                :data="siteList"
                border
                @selection-change="handleSelectionChange"
            >
                <!-- <el-table-column type="selection" width="55" /> -->
                <el-table-column label="厂区编号" prop="siteId" />
                <el-table-column label="厂区名称" prop="siteName" />
                <el-table-column label="备注" prop="remark" />
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
                            v-hasPermi="['product:site:edit']"
                            >修改</el-button
                        >
                        <!--          <el-button-->
                        <!--            size="mini"-->
                        <!--            type="text"-->
                        <!--            icon="el-icon-delete"-->
                        <!--            @click="handleDelete(scope.row)"-->
                        <!--            v-hasPermi="['product:site:remove']"-->
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
        <!-- 添加或修改厂区管理对话框 -->
        <el-dialog
            :title="title"
            :visible.sync="open"
            width="500px"
            append-to-body
        >
            <el-form ref="form" :model="form" :rules="rules" label-width="80px">
                <el-form-item label="厂区名称" prop="siteName" style="width: 100%;">
                    <el-input
                        v-model="form.siteName"
                        placeholder="请输入厂区名称"
                    />
                </el-form-item>
                <el-form-item label="备注" prop="remark" style="width: 100%;">
                    <el-input v-model="form.remark" placeholder="请输入备注" />
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
    listSite,
    getSite,
    delSite,
    addSite,
    updateSite,
    changeSiteStatus,
} from "@/api/product/site";
import { changeModelStatus } from "@/api/product/model";

export default {
    name: "Site",
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
            // 厂区管理表格数据
            siteList: [],
            // 弹出层标题
            title: "",
            // 是否显示弹出层
            open: false,
            // 查询参数
            queryParams: {
                pageNum: 1,
                pageSize: 10,
                siteName: null,
                status: null,
            },
            // 表单参数
            form: {},
            // 表单校验
            rules: {
                siteName: [
                    {
                        required: true,
                        message: "厂区名称不能为空",
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
        /** 查询厂区管理列表 */
        getList() {
            this.loading = true;
            listSite(this.queryParams).then((response) => {
                this.siteList = response.rows;
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
                siteId: null,
                siteName: null,
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
            this.ids = selection.map((item) => item.siteId);
            this.single = selection.length !== 1;
            this.multiple = !selection.length;
        },
        /** 新增按钮操作 */
        handleAdd() {
            this.reset();
            this.open = true;
            this.title = "添加厂区管理";
        },
        /** 修改按钮操作 */
        handleUpdate(row) {
            this.reset();
            const siteId = row.siteId || this.ids;
            getSite(siteId).then((response) => {
                this.form = response.data;
                this.open = true;
                this.title = "修改厂区管理";
            });
        },
        /** 厂区状态修改 */
        handleStatusChange(row) {
            let text = row.status === "0" ? "启用" : "停用";
            this.$modal
                .confirm('确认要"' + text + '""' + row.siteName + '"厂区吗？')
                .then(function () {
                    return changeSiteStatus(row.siteId, row.status);
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
                    if (this.form.siteId != null) {
                        updateSite(this.form).then((response) => {
                            this.$modal.msgSuccess("修改成功");
                            this.open = false;
                            this.getList();
                        });
                    } else {
                        addSite(this.form).then((response) => {
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
            const siteIds = row.siteId || this.ids;
            this.$modal
                .confirm(
                    '是否确认删除厂区管理编号为"' + siteIds + '"的数据项？'
                )
                .then(function () {
                    return delSite(siteIds);
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
                "product/site/export",
                {
                    ...this.queryParams,
                },
                `site_${new Date().getTime()}.xlsx`
            );
        },
    },
};
</script>
