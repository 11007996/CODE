<template>
    <div class="app-container">
        <!-- <el-row class="boxRow">
            <el-form
                ref="queryForm"
                :model="queryForm"
                inline
                label-position="left"
            >
            </el-form>
        </el-row> -->
        <el-row class="boxRow">
            <el-table :data="tableData" border :span-method="spanMethod">
                <el-table-column
                    label="部门"
                    prop="appraisedPersonGroupArr"
                    min-width="200px"
                    show-overflow-tooltip
                />
                <el-table-column label="NO" prop="id" />
                <el-table-column
                    label="KPI指标"
                    prop="kpiIndex"
                    min-width="120px"
                    show-overflow-tooltip
                />
                <el-table-column
                    label="计算公式"
                    prop="calculationFormula"
                    show-overflow-tooltip
                    min-width="200px"
                />
                <el-table-column label="单位" prop="units" />
                <el-table-column
                    label="权重占比%"
                    prop="weightCoefficient"
                    min-width="100px"
                />
                <el-table-column label="统计周期" prop="statisticalCycle" />
                <el-table-column
                    label="统计部门"
                    min-width="200px"
                    prop="statisticalPersonGroupArr"
                    show-overflow-tooltip
                />
                <el-table-column
                    label="目标及实际达成值"
                    prop=""
                    min-width="130px"
                >
                    <template slot-scope="scope">
                        <div class="border_bottom">
                            目标 {{ scope.row.targetStandard }}
                        </div>
                        <div>实际</div>
                    </template>
                </el-table-column>
                <el-table-column label="Q1" prop="">
                    <el-table-column label="Jan" prop="">
                        <template slot-scope="scope">
                            <div class="border_bottom">
                                {{
                                    scope.row.statisticalCycle === "季度"
                                        ? scope.row.Q1_target
                                        : scope.row.Jan_target
                                }}
                            </div>
                            <div>
                                {{
                                    scope.row.statisticalCycle === "季度"
                                        ? scope.row.Q1_actual
                                        : scope.row.Jan_actual
                                }}
                            </div>
                        </template>
                    </el-table-column>
                    <el-table-column label="Feb" prop="">
                        <template slot-scope="scope">
                            <div class="border_bottom">
                                {{
                                    scope.row.statisticalCycle === "季度"
                                        ? scope.row.Q2_target
                                        : scope.row.Feb_target
                                }}
                            </div>
                            <div>
                                {{
                                    scope.row.statisticalCycle === "季度"
                                        ? scope.row.Q2_actual
                                        : scope.row.Feb_actual
                                }}
                            </div>
                        </template>
                    </el-table-column>
                    <el-table-column label="Mar" prop="">
                        <template slot-scope="scope">
                            <div class="border_bottom">
                                {{
                                    scope.row.statisticalCycle === "季度"
                                        ? scope.row.Q3_target
                                        : scope.row.Mar_target
                                }}
                            </div>
                            <div>
                                {{
                                    scope.row.statisticalCycle === "季度"
                                        ? scope.row.Q3_actual
                                        : scope.row.Mar_actual
                                }}
                            </div>
                        </template>
                    </el-table-column>
                </el-table-column>
                <el-table-column label="Q2" prop="">
                    <el-table-column label="Apr" prop="">
                        <template slot-scope="scope">
                            <div class="border_bottom">
                                {{
                                    scope.row.statisticalCycle === "季度"
                                        ? scope.row.Q4_target
                                        : scope.row.Apr_target
                                }}
                            </div>
                            <div>
                                {{
                                    scope.row.statisticalCycle === "季度"
                                        ? scope.row.Q4_actual
                                        : scope.row.Apr_actual
                                }}
                            </div>
                        </template>
                    </el-table-column>
                    <el-table-column label="May" prop="">
                        <template slot-scope="scope">
                            <div class="border_bottom">
                                {{ scope.row.May_target }}
                            </div>
                            <div>{{ scope.row.May_actual }}</div>
                        </template>
                    </el-table-column>
                    <el-table-column label="Jun" prop="">
                        <template slot-scope="scope">
                            <div class="border_bottom">
                                {{ scope.row.Jun_target }}
                            </div>
                            <div>{{ scope.row.Jun_actual }}</div>
                        </template>
                    </el-table-column>
                </el-table-column>
                <el-table-column label="Q3" prop="">
                    <el-table-column label="Jul" prop="">
                        <template slot-scope="scope">
                            <div class="border_bottom">
                                {{ scope.row.Jul_target }}
                            </div>
                            <div>{{ scope.row.Jul_actual }}</div>
                        </template>
                    </el-table-column>
                    <el-table-column label="Aug" prop="">
                        <template slot-scope="scope">
                            <div class="border_bottom">
                                {{ scope.row.Aug_target }}
                            </div>
                            <div>{{ scope.row.Aug_actual }}</div>
                        </template>
                    </el-table-column>
                    <el-table-column label="Sep" prop="">
                        <template slot-scope="scope">
                            <div class="border_bottom">
                                {{ scope.row.Sep_target }}
                            </div>
                            <div>{{ scope.row.Sep_actual }}</div>
                        </template>
                    </el-table-column>
                </el-table-column>
                <el-table-column label="Q4" prop="">
                    <el-table-column label="Oct" prop="">
                        <template slot-scope="scope">
                            <div class="border_bottom">
                                {{ scope.row.Oct_target }}
                            </div>
                            <div>{{ scope.row.Oct_actual }}</div>
                        </template>
                    </el-table-column>
                    <el-table-column label="Nov" prop="">
                        <template slot-scope="scope">
                            <div class="border_bottom">
                                {{ scope.row.Nov_target }}
                            </div>
                            <div>{{ scope.row.Nov_actual }}</div>
                        </template>
                    </el-table-column>
                    <el-table-column label="Dec" prop="">
                        <template slot-scope="scope">
                            <div class="border_bottom">
                                {{ scope.row.Dec_target }}
                            </div>
                            <div>{{ scope.row.Dec_actual }}</div>
                        </template>
                    </el-table-column>
                </el-table-column>
            </el-table>
        </el-row>
    </div>
</template>
<script>
import { getReportList } from "@/api/DPMS/report";
export default {
    data() {
        return {
            enMonth: [
                "Jan",
                "Feb",
                "Mar",
                "Apr",
                "May",
                "Jun",
                "Jul",
                "Aug",
                "Sep",
                "Oct",
                "Nov",
                "Dec",
            ],
            queryForm: {},
            tableData: [{}],
            resData: [],
        };
    },
    methods: {
        handleResData() {
            this.tableData = [];
            this.resData.map((item) => {
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
                let appraisedPersonGroupArr= personGroupArr;
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
                let statisticalPersonGroupArr= personGroupArr;
                item.appraisedPersonGroup.sort((a, b) => {
                    return a.deptName - b.deptName;
                });
                let tableDataObj = {
                    statisticalPersonGroupArr,
                    appraisedPersonGroupArr,
                    appraisedDepartmentName: "",
                    statisticalDepartmentName: "",
                    weightCoefficient: item.weightCoefficient,
                    statisticalCycle: item.statisticalCycle,
                    kpiIndex: item.kpiIndex,
                    units: item.units,
                    targetStandard: item.targetStandard,
                    calculationFormula: item.calculationFormula,
                    id: item.id,
                };
                item.targetList.map((target) => {
                    let splitKey;
                    let objKey;
                    if (tableDataObj.statisticalCycle === "月度") {
                        splitKey =
                            this.enMonth[target.label.split("月")[0] - 1];
                        objKey = splitKey + "_target";
                    } else {
                        objKey = target.label + "_target";
                    }
                    tableDataObj[objKey] = target.value;
                });
                item.actualList.map((actual) => {
                    let splitKey;
                    let objKey;
                    if (tableDataObj.statisticalCycle === "月度") {
                        splitKey =
                            this.enMonth[actual.label.split("月")[0] - 1];
                        objKey = splitKey + "_actual";
                    } else {
                        objKey = actual.label + "_actual";
                    }
                    tableDataObj[objKey] = actual.value;
                });
                item.appraisedPersonGroup.map((appraise) => {
                    tableDataObj.appraisedDepartmentName +=
                        appraise.deptName + "/";
                });
                item.statisticalPersonGroup.map((statistical) => {
                    tableDataObj.statisticalDepartmentName +=
                        statistical.deptName + "/";
                });
                tableDataObj.appraisedDepartmentName =
                    tableDataObj.appraisedDepartmentName.slice(0, -1);
                tableDataObj.statisticalDepartmentName =
                    tableDataObj.statisticalDepartmentName.slice(0, -1);
                this.tableData.push(tableDataObj);
            });
        },
        spanMethod({ row, column, rowIndex, columnIndex }) {
            if (row.statisticalCycle === "季度") {
                if (columnIndex >= 9 && columnIndex <= 12) {
                    return [1, 3];
                } else if (columnIndex >= 13) {
                    return [0, 0];
                }
            }
        },
        getData() {
            getReportList().then((res) => {
                this.resData = res.rows;
                this.handleResData();
            });
        },
    },
    mounted() {
        this.getData();
    },
};
</script>
<style lang="scss" scoped>
.border_bottom {
    border-bottom: 1px solid #dfe6ec;
}
.el-row {
    padding: 10px;
    background: #fff;
    margin-bottom: 10px;
}
</style>
