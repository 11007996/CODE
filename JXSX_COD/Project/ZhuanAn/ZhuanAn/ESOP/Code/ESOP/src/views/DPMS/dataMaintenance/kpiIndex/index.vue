<!-- kpi维护 -->
<template>
    <div class="app-container">
        <!-- 条件查询搜索框 -->
        <el-row class="searchRow paddingRow">
            <el-col>
                <el-form ref="searchForm" :model="searchForm" inline>
                    <!-- <el-form-item label="KPI类型" prop="kpiType">
                        <el-select
                            v-model="searchForm.kpiType"
                            clearable
                            size="mini"
                        >
                            <el-option
                                v-for="item in kpiTypeOptions"
                                :key="item.value"
                                :label="item.label"
                                :value="item.value"
                            >
                            </el-option>
                        </el-select>
                    </el-form-item> -->
                    <el-form-item label="KPI名称" prop="kpiIndex">
                        <el-input
                            v-model="searchForm.kpiIndex"
                            clearable
                            size="mini"
                            placeholder="请输入KPI名称"
                        />
                    </el-form-item>
                    <el-form-item label="单位" prop="units">
                        <el-input
                            v-model="searchForm.units"
                            placeholder="请输入KPI单位"
                            size="mini"
                            clearable
                        />
                    </el-form-item>
                    <el-form-item label="计算公式" prop="calculationFormula">
                        <el-input
                            v-model="searchForm.calculationFormula"
                            size="mini"
                            placeholder="请输入计算公式"
                            clearable
                        />
                    </el-form-item>
                    <el-form-item>
                        <el-button
                            plain
                            size="mini"
                            type="primary"
                            icon="el-icon-search"
                            @click="getData()"
                            >搜索</el-button
                        >
                        <el-button
                            plain
                            size="mini"
                            type="warning"
                            icon="el-icon-refresh"
                            @click="resetSearchForm()"
                            >重置</el-button
                        >
                        <el-button
                            plain
                            size="mini"
                            type="success"
                            icon="el-icon-plus"
                            @click="insertKpi()"
                            >新增</el-button
                        >
                    </el-form-item>
                </el-form>
            </el-col>
        </el-row>
        <el-row class="paddingRow">
            <el-col>
                <el-table :data="tableData" border>
                    <el-table-column label="ID" prop="id" min-width="40px" />
                    <el-table-column
                        label="名称"
                        prop="kpiIndex"
                        show-overflow-tooltip
                    />
                    <el-table-column label="单位" prop="units" />
                    <el-table-column
                        label="计算公式"
                        prop="calculationFormula"
                        show-overflow-tooltip
                    />
                    <el-table-column
                        label="创建人"
                        prop="createBy"
                        show-overflow-tooltip
                    />
                    <el-table-column
                        label="创建时间"
                        prop="createTime"
                        show-overflow-tooltip
                    />
                    <el-table-column
                        label="更新人"
                        prop="updateBy"
                        show-overflow-tooltip
                    />
                    <el-table-column
                        label="更新时间"
                        prop="updateTime"
                        show-overflow-tooltip
                    />
                    <el-table-column
                        label="操作"
                        fixed="right"
                        min-width="100px"
                    >
                        <template slot-scope="scope">
                            <el-button
                                type="text"
                                icon="el-icon-edit"
                                @click="updateKpiIndex(scope.row)"
                                :disabled="scope.row.status === '1'"
                            >
                                修改
                            </el-button>
                            <el-button
                                type="text"
                                icon="el-icon-delete"
                                class="dangerButton"
                                :disabled="scope.row.status === '1'"
                                @click="removeKpiIndex(scope.row)"
                            >
                                删除
                            </el-button>
                        </template>
                    </el-table-column>
                </el-table>
                <pagination
                    v-show="searchForm.total > 10"
                    :total="searchForm.total"
                    :page.sync="searchForm.pageNum"
                    :limit.sync="searchForm.pageSize"
                    @pagination="getData"
                />
            </el-col>
        </el-row>
        <!-- 新增/修改弹窗 -->
        <el-dialog
            center
            :close-on-click-modal="false"
            :title="dialogTitle"
            :visible.sync="dialogVisible"
            @open="dialogOpen"
            @close="dialogClose"
        >
            <el-form
                ref="dialogForm"
                :model="dialogForm"
                :rules="rules"
                label-width="80px"
            >
                <el-form-item label="名称" prop="kpiIndex">
                    <el-input
                        v-model="dialogForm.kpiIndex"
                        placeholder="请输入KPI名称"
                        clearable
                    />
                </el-form-item>
                <el-form-item label="单位" prop="units">
                    <el-input
                        v-model="dialogForm.units"
                        placeholder="请输入KPI单位"
                        clearable
                    />
                </el-form-item>
                <el-form-item label="计算公式" prop="calculationFormula">
                    <el-input
                        v-model="dialogForm.calculationFormula"
                        placeholder="请输入计算公式"
                        clearable
                    />
                </el-form-item>
            </el-form>
            <!-- 底部 button class/slot 名称固定  -->
            <span slot="footer" class="dialog-footer">
                <el-button type="success" @click="submitForm" size="mini" plain
                    >提交</el-button
                >
                <el-button type="danger" @click="cancelForm" size="mini" plain
                    >取消</el-button
                >
            </span>
        </el-dialog>
    </div>
</template>
<script>
import {
    getKpiIndexList,
    insertKpiIndex,
    removeKpiIndex,
    updateKpiIndex,
} from "@/api/DPMS/dataMaintenance/kpiIndex";
import { getKpiTypeList } from "@/api/DPMS/dataMaintenance/kpiType";
import Cookies from "js-cookie";
export default {
    data() {
        return {
            //
            searchForm: {
                total: 0,
                pageNum: 1,
                pageSize: 10,
            },
            //表格数据
            tableData: [],
            //弹窗的标题
            dialogTitle: "",
            //弹窗的显隐
            dialogVisible: false,
            //表单
            dialogForm: {},
            //校验规则
            rules: {
                kpiIndex: [
                    {
                        required: true,
                        message: "请输入KPI名称",
                        trigger: "blur",
                    },
                ],
            },
        };
    },
    methods: {
        getData() {
            getKpiIndexList(this.searchForm).then((res) => {
                if (res.rows.length > 0) {
                    this.tableData = res.rows;
                    this.searchForm.total = res.total;
                } else {
                    this.$modal.msgError("暂无数据");
                }
            });
        },
        // 重置搜索选项
        resetSearchForm() {
            this.$refs.searchForm.resetFields();
        },
        //新增KPI
        insertKpi() {
            this.dialogTitle = "新增KPI";
            this.dialogVisible = true;
        },
        //修改KPI
        updateKpiIndex(row) {
            this.dialogTitle = "修改KPI";
            this.dialogVisible = true;
            this.$nextTick(() => {
                this.dialogForm = JSON.parse(JSON.stringify(row));
            });
        },
        //删除KPI
        removeKpiIndex(row) {
            removeKpiIndex(row.id).then((res) => {
                if (res.code === 200) {
                    this.$modal.msgSuccess(res.msg);
                } else {
                    this.$modal.msgError(res.msg);
                }
                this.getData();
            });
        },
        //弹窗开启回调 初始化表单的值
        dialogOpen() {
            this.dialogForm = {
                kpiIndex: null,
                kpiType: null,
                units: null,
                calculationFormula: null,
            };
        },
        //弹窗关闭回调 重置表单
        dialogClose() {
            this.$refs.dialogForm.resetFields();
        },
        //提交表单
        submitForm() {
            this.$refs.dialogForm.validate((valid) => {
                if (valid) {
                    if (this.dialogTitle === "修改KPI") {
                        this.dialogForm["updateBy"] = Cookies.get("username");
                        this.dialogForm["updateTime"] = this.parseTime(
                            new Date()
                        );
                        updateKpiIndex(this.dialogForm).then((res) => {
                            if (res.code === 200) {
                                this.$modal.msgSuccess(res.msg);
                            } else {
                                this.$modal.msgError(res.msg);
                            }
                            this.dialogVisible = false;
                            this.getData();
                        });
                    } else {
                        this.dialogForm["createBy"] = Cookies.get("username");
                        this.dialogForm["createTime"] = this.parseTime(
                            new Date()
                        );
                        insertKpiIndex(this.dialogForm).then((res) => {
                            if (res.code === 200) {
                                this.$modal.msgSuccess(res.msg);
                            } else {
                                this.$modal.msgError(res.msg);
                            }
                            this.dialogVisible = false;
                            this.getData();
                        });
                    }
                } else {
                    this.$modal.msgError("请按提示填写指定内容！");
                }
            });
        },
        //取消
        cancelForm() {
            this.dialogVisible = false;
        },
        //获取KPI类别
        getKpiTypeList() {
            getKpiTypeList().then((res) => {
                this.kpiTypeOptions = res.rows;
            });
        },
    },
    mounted() {
        this.getData();
    },
};
</script>
<style lang="scss" scoped>

.el-row:last-child {
    margin-bottom: 0;
}
::v-deep .searchRow .el-form-item {
    margin-bottom: 0;
}
::v-deep .el-dialog {
    .el-form-item {
        display: flex;
        .el-form-item__content {
            flex: 1;
        }
    }
}
</style>