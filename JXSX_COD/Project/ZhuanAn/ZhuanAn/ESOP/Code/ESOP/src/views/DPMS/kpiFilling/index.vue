<!-- kpi填报 -->
<template>
    <div class="app-container">
        <!-- 条件查询搜索框 -->
        <el-row class="searchRow">
            <el-col>
                <el-form ref="searchForm" :model="searchForm" inline>
                    <el-form-item prop="kpiYear" label="年度">
                        <el-input
                            v-model="searchForm.kpiYear"
                            clearable
                            size="mini"
                            placeholder="请输入KPI年度"
                        />
                    </el-form-item>

                    <el-form-item label="统计周期" prop="statisticalCycle">
                        <el-select
                            v-model="searchForm.statisticalCycle"
                            clearable
                            size="mini"
                            placeholder="请选择KPI统计周期"
                        >
                            <el-option
                                v-for="item in statisticalCycleOptions"
                                :key="item.value"
                                :label="item.label"
                                :value="item.value"
                            >
                            </el-option>
                        </el-select>
                    </el-form-item>
                    <el-form-item label="KPI名称" prop="kpiIndex">
                        <el-input
                            v-model="searchForm.kpiIndex"
                            clearable
                            size="mini"
                            placeholder="请输入KPI名称"
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
                        <!-- <el-button
                            plain
                            size="mini"
                            type="success"
                            icon="el-icon-plus"
                            @click="insertKpi()"
                            >新增指派</el-button
                        > -->
                    </el-form-item>
                </el-form>
            </el-col>
        </el-row>
        <!-- 表格 -->
        <el-row>
            <el-col>
                <el-table
                    :data="tableData"
                    border
                    stripe
                    v-loading="tableDataLoading"
                    element-loading-text="拼命加载中"
                    element-loading-spinner="el-icon-loading"
                    element-loading-background="rgba(0, 0, 0, 0.8)"
                >
                    <el-table-column label="ID" prop="id" min-width="50px" />
                    <el-table-column
                        label="KPI名称"
                        prop="kpiIndex"
                        show-overflow-tooltip
                        min-width="150px"
                    />
                    <el-table-column
                        label="年度"
                        prop="kpiYear"
                        show-overflow-tooltip
                    />
                    <el-table-column
                        label="统计周期"
                        prop="statisticalCycle"
                        show-overflow-tooltip
                    />
                    <el-table-column label="单位" prop="units" />
                    <el-table-column
                        label="计算公式"
                        prop="calculationFormula"
                        show-overflow-tooltip
                        min-width="150px"
                    />
                    <el-table-column
                        label="考核部门"
                        prop="appraisedPersonGroupArr"
                        show-overflow-tooltip
                        min-width="150px"
                    />
                    <el-table-column
                        label="统计部门"
                        prop="statisticalPersonGroupArr"
                        show-overflow-tooltip
                        min-width="150px"
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
                        min-width="150px"
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
                        min-width="150px"
                    />
                    <el-table-column
                        label="操作"
                        fixed="right"
                        min-width="140px"
                    >
                        <template slot-scope="scope">
                            <el-button
                                type="text"
                                icon="el-icon-finished"
                                class="dangerButton"
                                @click="fillKpi(scope.row)"
                            >
                                填报
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
        <!-- 填报弹窗 -->
        <el-dialog
            center
            width="95%"
            :close-on-click-modal="false"
            :title="designateDialogTitle"
            :visible.sync="designateDialogVisible"
            @open="designateDialogOpen"
            @close="designateDialogClose"
        >
            <!-- 指派选项 -->
            <el-row>
                <el-form
                    inline
                    ref="designateForm"
                    :model="designateForm"
                    label-width="120px"
                    :rules="designateRules"
                >
                    <el-row class="noMarginRow formFlexRow" v-if="showKpiItem">
                        <el-divider content-position="left">KPI</el-divider>
                        <el-form-item label="名称" prop="kpiIndex">
                            <el-input
                                disabled
                                v-model="designateForm.kpiIndex"
                            />
                        </el-form-item>
                        <el-form-item label="单位" prop="units">
                            <el-input disabled v-model="designateForm.units" />
                        </el-form-item>
                        <el-form-item
                            label="计算公式"
                            prop="calculationFormula"
                        >
                            <el-input
                                disabled
                                v-model="designateForm.calculationFormula"
                            />
                        </el-form-item>
                    </el-row>
                    <el-row class="noMarginRow formFlexRow">
                        <el-divider content-position="left"
                            >指派内容</el-divider
                        >
                        <el-form-item label="年度" prop="kpiYear">
                            <el-input
                                v-model="designateForm.kpiYear"
                                placeholder="请输入KPI考核年度"
                                type="number"
                                disabled
                                min="2000"
                            />
                        </el-form-item>
                        <el-form-item label="权重" prop="weightCoefficient">
                            <el-input
                                v-model="designateForm.weightCoefficient"
                                type="number"
                                max="100"
                                placeholder="请输入KPI权重"
                                disabled
                            >
                                <template slot="append">%</template>
                            </el-input>
                        </el-form-item>
                        <el-form-item label="周期" prop="statisticalCycle">
                            <el-select
                                v-model="designateForm.statisticalCycle"
                                clearable
                                placeholder="请选择KPI统计周期"
                                disabled
                                @change="changeCycle"
                            >
                                <el-option
                                    v-for="item in statisticalCycleOptions"
                                    :key="item.value"
                                    :label="item.label"
                                    :value="item.value"
                                >
                                </el-option>
                            </el-select>
                        </el-form-item>
                        <el-form-item label="目标值标准" prop="targetStandard">
                            <el-select
                                v-model="designateForm.targetStandard"
                                clearable
                                disabled
                            >
                                <el-option
                                    v-for="(
                                        item, index
                                    ) in targetStandardOptions"
                                    :key="index"
                                    :label="item.label"
                                    :value="item.value"
                                >
                                </el-option>
                            </el-select>
                        </el-form-item>
                    </el-row>
                </el-form>
                <el-divider content-position="left">目标值设定</el-divider>
                <el-row class="targetRow" v-if="showInputTarget">
                    <el-col
                        v-for="(item, index) in targetOption"
                        :key="index"
                        :span="24 / targetOption.length"
                        class="targetCol"
                    >
                        <el-row class="row_th">
                            {{ item.label }}
                        </el-row>
                        <el-row class="row_tb">
                            <el-input
                                :placeholder="item.label + '目标'"
                                disabled
                                v-model="designateForm.targetList[index].value"
                                @change="changeTargetInput()"
                            />
                        </el-row>
                    </el-col>
                </el-row>
                <el-divider content-position="left"
                    >填报当前周期实际值</el-divider
                >
                <el-row class="targetRow" v-if="showInputTarget">
                    <el-col
                        v-for="(item, index) in targetOption"
                        :key="index"
                        :span="24 / targetOption.length"
                        class="targetCol"
                    >
                        <el-row class="row_th">
                            {{ item.label }}
                        </el-row>
                        <el-row class="row_tb">
                            <el-input
                                :placeholder="item.label + '实际值'"
                                v-model="designateForm.actualList[index].value"
                                :disabled="nowMonth > index"
                                @change="changeTargetInput()"
                            />
                        </el-row>
                    </el-col>
                </el-row>
            </el-row>
            <span slot="footer" class="dialog-footer">
                <el-button
                    type="success"
                    @click="submitFillKpi"
                    size="mini"
                    plain
                    >提交</el-button
                >
                <el-button
                    type="danger"
                    @click="cancelDesignate"
                    size="mini"
                    plain
                    >取消</el-button
                >
            </span>
        </el-dialog>
    </div>
</template>
<script>
import { getKpiFillList, fillKpi } from "@/api/DPMS/kpiFilling";
import { deptTreeSelect, listUser } from "@/api/system/user";
import { throttle } from "@/utils/throttle";
import Cookies from "js-cookie";
export default {
    data() {
        return {
            nowMonth: new Date().getMonth(),
            searchForm: {
                total: 0,
                pageNum: 1,
                pageSize: 10,
            },
            //kpi统计周期
            statisticalCycleOptions: [
                {
                    label: "月度",
                    value: "月",
                },
                {
                    label: "季度",
                    value: "季",
                },
                {
                    label: "年度",
                    value: "年",
                },
            ],
            //目标值标准
            targetStandardOptions: [
                {
                    label: "大于",
                    value: 0,
                },
                {
                    label: "小于",
                    value: 1,
                },
                {
                    label: "等于",
                    value: 2,
                },
                {
                    label: "不等于",
                    value: 3,
                },
                {
                    label: "大于等于",
                    value: 4,
                },
                {
                    label: "小于等于",
                    value: 5,
                },
            ],
            //表格数据
            tableData: [],
            //表格加载动画
            tableDataLoading: false,
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
            // 指派kpi弹窗显隐
            designateDialogVisible: false,
            //指派kpi弹窗标题
            designateDialogTitle: "",
            //指派表单校验规则
            designateRules: {},
            //步骤
            step: null,
            //当前步骤状态	wait / process / finish / error / success
            stepStatus: "",
            //可用kpi列表
            kpiTableData: [],
            //
            //分页
            kpiTablePage: {
                total: 0,
                pageNum: 1,
                pageSize: 10,
                status: "0",
            },
            showKpiItem: false,
            //填写目标
            showInputTarget: false,
            //指派的表单
            designateForm: {},
            //目标值配置
            targetOption: [],
            //部门
            deptOptions: [],
            //考核人员
            appraisedList: [],
            //统计人员
            statisticalList: [],
            //部门级联选择器配置
            depProps: {
                emitPath: false,
                multiple: true,
                value: "id",
                label: "label",
                lazy: true,
                lazyLoad(node, resolve) {
                    const { level, value } = node;
                    if (level === 3) {
                        listUser({ deptId: value }).then((res) => {
                            let nodes = [];
                            res.rows.map((item) => {
                                nodes.push({
                                    label: item.nickName,
                                    id: item.userName,
                                    leaf: true,
                                });
                            });
                            resolve(nodes);
                        });
                    } else {
                        resolve(node);
                    }
                },
            },
        };
    },
    methods: {
        getData() {
            this.tableDataLoading = true;
            getKpiFillList(this.searchForm).then((res) => {
                if (res.rows.length > 0) {
                    res.rows.map((item) => {
                        let oldDeptList = [];
                        // 排序，取字符串，去重，拼接
                        item.appraisedPersonGroup
                            .sort(function (a, b) {
                                if (a.deptName > b.deptName) {
                                    return 1;
                                }
                                if (a.deptName < b.deptName) {
                                    return -1;
                                }
                                return 0;
                            })
                            .map((group) => {
                                oldDeptList.push(group.deptName);
                            });
                        let personGroupArr = oldDeptList
                            .filter((groupItem, index) => {
                                return oldDeptList.indexOf(groupItem) === index;
                            })
                            .join("&");
                        item["appraisedPersonGroupArr"] = personGroupArr;
                        oldDeptList = [];
                        // 排序，取字符串，去重，拼接
                        item.statisticalPersonGroup
                            .sort(function (a, b) {
                                if (a.deptName > b.deptName) {
                                    return 1;
                                }
                                if (a.deptName < b.deptName) {
                                    return -1;
                                }
                                return 0;
                            })
                            .map((group) => {
                                oldDeptList.push(group.deptName);
                            });
                        personGroupArr = oldDeptList
                            .filter((groupItem, index) => {
                                return oldDeptList.indexOf(groupItem) === index;
                            })
                            .join("&");
                        item["statisticalPersonGroupArr"] = personGroupArr;
                    });
                    this.tableData = res.rows;
                    this.searchForm.total = res.total;
                } else {
                    this.$modal.msgError("暂无数据");
                }
                this.tableDataLoading = false;
            });
        },
        // 重置搜索选项
        resetSearchForm() {
            this.$refs.searchForm.resetFields();
        },
        //填报Kpi
        fillKpi(row) {
            row['year']=row.kpiYear;
            this.designateDialogVisible = true;
            this.designateDialogTitle = "填报Kpi";
            this.showKpiItem = true;
            this.$nextTick(() => {
                this.appraisedList = [];
                deptTreeSelect().then((res) => {
                    this.deptOptions = res.data;
                    this.designateForm = JSON.parse(JSON.stringify(row));
                    this.changeCycle(this.designateForm.statisticalCycle, true);
                    // this.designateForm.appraisedPersonGroup.map((item) => {
                    //     this.appraisedList.push([item.deptId]);
                    // });
                });
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
        //指派kpi弹窗打开的回调
        designateDialogOpen() {
            this.designateForm = {
                kpiYear: "",
                units: "",
                calculationFormula: "",
                kpiIndex: "",
                weightCoefficient: "",
                targetStandard: "",
                statisticalCycle: "",
                targetList: [],
                appraisedPersonGroup: [],
                statisticalPersonGroup: [],
            };
        },
        //指派KPI弹窗关闭的回调
        designateDialogClose() {
            this.showKpiItem = false;
            this.showInputTarget = false;
            this.appraisedList = [];
            this.statisticalList = [];
            this.$refs.designateForm.resetFields();
            // 重置步骤
            this.step = null;
        },
        //选择KPI
        selectKpi(row) {
            this.step = 1;
            this.showKpiItem = true;
            let { kpiIndex, units, calculationFormula, id } = row;
            this.$nextTick(() => {
                this.designateForm["kpiIndex"] = kpiIndex;
                this.designateForm["units"] = units;
                this.designateForm["calculationFormula"] = calculationFormula;
                this.designateForm["kpiId"] = id;
                this.$forceUpdate();
                this.step = 2;
                this.stepStatus = "process";
            });
        },
        //提交指派
        submitFillKpi() {
            this.$refs.designateForm.validate((valid) => {
                if (valid) {
                    let { id, actualList } = this.designateForm;
                    let params = {
                        id,
                        actualList,
                    };
                    fillKpi(params).then((res) => {
                        if (res.code === 200) {
                            this.$modal.msgSuccess(res.msg);
                        } else {
                            this.$modal.msgError(res.msg);
                        }
                        this.designateDialogVisible = false;
                        this.getData();
                    });
                } else {
                    this.$modal.msgError("请按提示填写指定内容！");
                }
            });
        },
        //取消指派
        cancelDesignate() {
            this.designateDialogVisible = false;
        },
        //改变周期
        changeCycle(val, isView) {
            if (!val) {
                this.showInputTarget = false;
            } else {
                this.targetOption = [];
                if (!isView) {
                    this.designateForm.targetList = [];
                }
                this.showInputTarget = true;
                switch (val) {
                    case "月":
                        for (var i = 1; i < 13; i++) {
                            this.targetOption.push({
                                label: i + "月",
                                value: i,
                            });
                            if (!isView) {
                                this.designateForm.targetList.push({
                                    label: i + "月",
                                    value: "",
                                });
                            }
                        }
                        break;
                    case "季":
                        for (var i = 1; i < 5; i++) {
                            this.targetOption.push({
                                label: "Q" + i,
                                value: i,
                            });
                            if (!isView) {
                                this.designateForm.targetList.push({
                                    label: "Q" + i,
                                    value: "",
                                });
                            }
                        }
                        break;
                    default:
                        this.targetOption.push({
                            label: this.designateForm.year + "年",
                            value: this.designateForm.year,
                        });
                        if (!isView) {
                            this.designateForm.targetList.push({
                                label: this.designateForm.year + "年",
                                value: "",
                            });
                        }
                }
            }
        },
        changeTargetInput: throttle(
            function () {
                this.$forceUpdate();
            },
            300,
            true
        ),
    },
    mounted() {
        this.getData();
    },
};
</script>
<style lang="scss" scoped>
.el-row {
    padding: 10px;
    background: #fff;
    margin-bottom: 10px;
}
.el-row:last-child {
    margin-bottom: 0;
}
.noMarginRow {
    margin-bottom: 0;
    padding: 0;
}
::v-deep .searchRow .el-form-item {
    margin-bottom: 0;
}
::v-deep .el-dialog {
    .el-form {
        display: flex;
        flex-direction: column;
    }
    .targetRow {
        text-align: center;
    }
    .formFlexRow {
        display: flex;
        flex-wrap: wrap;
    }
    .targetCol {
        .el-row {
            border: 1px solid #dfe6ec;
            margin-top: -1px;
            margin-left: -1px;
            margin-bottom: 0;
        }
        .row_th {
            background-color: #f8f8f9;
            font-size: 13px;
            color: #000;
            font-weight: 700;
        }
        .row_tb {
            padding: 5px;
        }
    }
    @media screen and (max-width: 1400px) {
        .el-form-item {
            width: calc(25% - 10px);
        }
    }
    @media screen and (min-width: 1401px) {
        .el-form-item {
            width: calc(20% - 10px);
        }
    }
    .el-form-item {
        display: flex;
        margin-bottom: 15px !important;
        .el-form-item__content {
            flex: 1;
        }
    }
}

::v-deep .el-divider--horizontal {
    margin: 10px 0;
}
</style>
