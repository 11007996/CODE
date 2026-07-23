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
                <el-form-item label="线别名称" prop="lineName">
                    <el-input
                        v-model="queryParams.lineName"
                        placeholder="请输入线别名称"
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
                        @click="resetQuery"
                        >重置</el-button
                    >
                    <el-button
                        type="success"
                        plain
                        icon="el-icon-plus"
                        size="mini"
                        @click="handleAdd"
                        v-hasPermi="['product:line:add']"
                        >新增</el-button
                    >
                    <el-button
                        type="danger"
                        plain
                        icon="el-icon-download"
                        size="mini"
                        @click="handleExport"
                        v-hasPermi="['product:line:export']"
                        >导出</el-button
                    >
                    <el-button
                        size="mini"
                        type="danger"
                        icon="el-icon-delete"
                        @click="handleDelete"
                        v-hasPermi="['product:line:remove']"
                        >删除</el-button
                    >
                </el-form-item>
            </el-form>
        </el-row>
        <el-row class="paddingRow">
            <el-table
                v-loading="loading"
                :data="lineList"
                border
                @selection-change="handleSelectionChange"
                stripe
            >
                <!-- <el-table-column type="selection" width="55" /> -->
                <el-table-column label="线别编号" prop="lineId" />
                <el-table-column label="线别名称" prop="lineName" />
                <el-table-column label="线别类型" prop="lineType">
                    <template slot-scope="scope">
                        <dict-tag
                            :options="dict.type.mes_line_type"
                            :value="scope.row.lineType"
                        />
                    </template>
                </el-table-column>
                <!-- <el-table-column label="绑定料号" prop="materialNameList" >
                    <template slot-scope="scope">
                        {{scope.row.materialNameList.join('/')}}
                    </template>
                </el-table-column> -->
                <el-table-column label="工作中心" prop="workCenter">
                    <template slot-scope="scope">
                        <dict-tag
                            :options="dict.type.mes_work_center"
                            :value="scope.row.workCenter"
                        />
                    </template>
                </el-table-column>
                <el-table-column label="厂区" prop="siteName" />
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
                            v-hasPermi="['product:line:edit']"
                            >修改</el-button
                        >
                        <el-button
                            size="mini"
                            type="text"
                            icon="el-icon-delete"
                            @click="handleDelete(scope.row)"
                            v-hasPermi="['product:line:remove']"
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
        <!-- 添加或修改线别信息对话框 -->
        <el-dialog
            :title="title"
            :visible.sync="open"
            width="60%"
            append-to-body
            center
            @open="dialogOpen"
        >
            <el-form ref="form" :model="form" :rules="rules" label-width="80px">
                <el-form-item label="线别名称" prop="lineName">
                    <el-input
                        v-model="form.lineName"
                        placeholder="请输入线别名称"
                    />
                </el-form-item>

                <el-form-item label="线别类型" prop="lineType">
                    <el-select
                        v-model="form.lineType"
                        placeholder="请选择线别类型"
                    >
                        <el-option
                            v-for="dict in dict.type.mes_line_type"
                            :key="dict.value"
                            :label="dict.label"
                            :value="dict.value"
                            :disabled="dict.status == 1"
                        />
                    </el-select>
                </el-form-item>
                <el-form-item label="会签人员" prop="userIdList">
                    <el-select
                        v-model="form.userIdList"
                        multiple
                        filterable
                        placeholder="请选择"
                        size="mini"
                    >
                        <el-option
                            v-for="item in allowCountersignUserOptions"
                            :key="item.userId"
                            :label="item.nickName"
                            :value="item.userId"
                        />
                    </el-select>
                </el-form-item>

                <el-form-item label="工作中心" prop="workCenter">
                    <el-select
                        v-model="form.workCenter"
                        placeholder="请选择工作中心"
                    >
                        <el-option
                            v-for="dict in dict.type.mes_work_center"
                            :key="dict.value"
                            :label="dict.label"
                            :value="dict.value"
                            :disabled="dict.status == 1"
                        />
                    </el-select>
                </el-form-item>

                <el-form-item label="厂区名称" prop="siteId">
                    <el-select
                        v-model="form.siteId"
                        placeholder="请选择厂区名称"
                    >
                        <el-option
                            v-for="item in siteOptions"
                            :key="item.siteId"
                            :label="item.siteName"
                            :value="item.siteId"
                        ></el-option>
                    </el-select>
                </el-form-item>

                <el-form-item label="备注" prop="remark">
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
    listLine,
    getLine,
    delLine,
    addLine,
    updateLine,
    changeLineStatus,
    getMesSiteOptions,
    getMaterialListByLineId,
} from "@/api/product/line";
import { getOACountersignUserList ,getUserListByLineId} from "@/api/product/oa";
import { getPartNoList } from "@/api/product/partNo";
export default {
    dicts: ["mes_work_center", "mes_line_type"],
    name: "ProductLine",
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
            // 线别信息表格数据
            lineList: [],
            // 弹出层标题
            title: "",
            // 是否显示弹出层
            open: false,
            materialOption: [],
            siteOptions: [],
            // 查询参数
            queryParams: {
                pageNum: 1,
                pageSize: 10,
                lineName: null,
                lineType: null,
                workCenter: null,
                status: null,
                //materialIdList: "",
            },
            // 表单参数
            form: {},
            // 表单校验
            rules: {
                siteId: {
                    required: true,
                    message: "厂区名称不能为空",
                    trigger: "shange",
                },
                lineName: [
                    {
                        required: true,
                        message: "线别名称不能为空",
                        trigger: "blur",
                    },
                ],
                userIdList: [
                    {
                        required: true,
                        message: "请选择会签人员",
                        trigger: ["blur", "change"],
                    },
                ],
            },
            allowCountersignUserOptions: [],
        };
    },
    created() {
        //this.getPartNoOption();
        this.getList();
    },
    methods: {
        /** 查询线别信息列表 */
        getList() {
            this.loading = true;
            listLine(this.queryParams).then((response) => {
                this.lineList = response.rows;
                this.total = response.total;
                this.loading = false;
            });
            this.handleMesSiteOptions();
        },
        // 取消按钮
        cancel() {
            this.open = false;
            this.reset();
        },
        // 表单重置
        reset() {
            this.form = {
                lineId: null,
                lineName: null,
                lineType: "0",
                workCenter: "0",
                status: "0",
                remark: null,
                createBy: null,
                createTime: null,
                updateBy: null,
                updateTime: null,
                userIdList:[],
                //materialIdList: [],
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
            this.ids = selection.map((item) => item.lineId);
            this.single = selection.length !== 1;
            this.multiple = !selection.length;
        },
        /** 新增按钮操作 */
        handleAdd() {
            this.reset();
            this.open = true;
            this.title = "添加线别信息";
        },
        // 获取厂区信息
        handleMesSiteOptions() {
            getMesSiteOptions().then((response) => {
                this.siteOptions = response.rows;
            });
        },

        /** 修改按钮操作 */
        handleUpdate(row) {
            this.reset();
            this.open = true;
            this.title = "修改线别信息";
            // getMaterialListByLineId(row.lineId).then((res) => {
            //     this.form["materiaIldList"] = res.data.MaterialIdList;
            // });
            getUserListByLineId(row.lineId).then(res=>{
                this.form["userIdList"] = res.data.UserIdList
            })
            this.$nextTick(() => {
                this.form = JSON.parse(JSON.stringify(row));
            });
        },
        // 机种状态修改
        handleStatusChange(row) {
            let text = row.status === "0" ? "启用" : "停用";
            this.$modal
                .confirm('确认要"' + text + '""' + row.lineName + '"线别吗？')
                .then(function () {
                    return changeLineStatus(row.lineId, row.status);
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
                    if (this.form.lineId != null) {
                        updateLine(this.form).then((response) => {
                            this.$modal.msgSuccess("修改成功");
                            this.open = false;
                            this.getList();
                        });
                    } else {
                        addLine(this.form).then((response) => {
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
            const lineIds = row.lineId || this.ids;
            this.$modal
                .confirm(
                    '是否确认删除线别信息编号为"' + lineIds + '"的数据项？'
                )
                .then(function () {
                    return delLine(lineIds);
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
                "product/line/export",
                {
                    ...this.queryParams,
                },
                `line_${new Date().getTime()}.xlsx`
            );
        },
        dialogOpen() {
            getOACountersignUserList().then((res) => {
                this.allowCountersignUserOptions = res.rows;
            });
        },
        //根据机种获取料号
        // getPartNoOption() {
        //     getPartNoList().then((res) => {
        //         this.materialOption = res.rows;
        //     });
        // },
    },
};
</script>
<style lang="scss" scoped>
::v-deep .el-select {
    width: 100%;
}
</style>
