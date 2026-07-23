<template>
    <div class="app-container">
        <el-row class="paddingRow searchRow">
            <el-form inline label-width="80px" :model="searchForm">
                <!-- <el-form-item label="机种" prop="modelId">
                    <el-select
                        v-model="searchForm.modelId"
                        placeholder="请选择机种"
                        filterable
                        clear
                        @change="changeModel"
                        ref="modelSelect"
                        size="mini"
                    >
                        <el-option
                            v-for="item in modelOptions"
                            :key="item.modelId"
                            :label="item.modelName"
                            :value="item.modelId"
                        />
                    </el-select>
                </el-form-item>
                <el-form-item label="料号" prop="materialId">
                    <el-select
                        v-model="searchForm.materialId"
                        clearable
                        size="mini"
                        filterable
                        ref="materialSelect"
                        placeholder="请选择料号"
                    >
                        <el-option
                            v-for="(item, index) in materialOption"
                            :key="index"
                            :label="item.materialName"
                            :value="item.id"
                        />
                    </el-select>
                </el-form-item> -->
                <el-form-item label="线别名称" prop="lineId">
                    <el-select
                        v-model="searchForm.lineId"
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
                        v-model="searchForm.stageId"
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
                        v-model="searchForm.processId"
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
                        plain
                        icon="el-icon-search"
                        @click="getData"
                        size="mini"
                        >查询</el-button
                    >
                </el-form-item>
            </el-form>
        </el-row>
        <el-row class="paddingRow">
            <el-table
                border
                stripe
                :data="tableData"
                v-loading="tableLoading"
                element-loading-text="拼命加载中"
                element-loading-spinner="el-icon-loading"
                element-loading-background="rgba(0, 0, 0, 0.8)"
            >
                <af-table-column
                    label="文件名称"
                    prop="sopName"
                    min-width="200px"
                    show-overflow-tooltip
                />
                <af-table-column label="文件类型" prop="fileType" />
                <af-table-column
                    label="线体"
                    prop="lineName"
                    show-overflow-tooltip
                />
                <af-table-column
                    label="站位序号"
                    prop="stageName"
                    show-overflow-tooltip
                />
                <af-table-column
                    label="站位名"
                    prop="processName"
                    show-overflow-tooltip
                />
                <!-- <af-table-column label="MAC地址" prop="macName" show-overflow-tooltip />
                <af-table-column label="MAC描述" prop="macArea" show-overflow-tooltip /> -->
                <af-table-column label="版本" prop="version" />
                <el-table-column label="状态" prop="versionStatus">
                    <template slot-scope="scope">
                        <el-switch
                            @change="
                                (val) => {
                                    changeStatus(val, scope.row);
                                }
                            "
                            v-model="tableData[scope.$index].versionStatus"
                            active-value="0"
                            inactive-value="1"
                        />
                    </template>
                </el-table-column>
                <af-table-column
                    label="创建时间"
                    prop="createTime"
                    :formatter="parseTableTime"
                    min-width="140px"
                />
                <!-- <el-table-column label="创建人" prop="createBy" /> -->
            </el-table>
            <pagination
                v-show="searchForm.total > 0"
                :total="searchForm.total"
                :page.sync="searchForm.pageNum"
                :limit.sync="searchForm.pageSize"
                @pagination="getData"
            />
        </el-row>
    </div>
</template>
<script>
import { getSopHistory, changeSopStatus } from "@/api/product/history";
import { getMesModelOptions } from "@/api/product/oa";
import { getPartNoList } from "@/api/product/partNo";
import { noDataMixins } from "@/mixins";
import {
    getMesLineOptions,
    getMesStageOptions,
    getMesProcessOptionsByStageId,
    getStageInTerminal,
    getProcessInTerminal,
} from "@/api/product/terminal";
import { listProcess } from "@/api/product/process";
export default {
    mixins: [noDataMixins],
    data() {
        return {
            searchForm: {
                pageSize: 10,
                pageNum: 1,
                total: 0,
                lineId: null,
                stageId: null,
                processId: null,
                // modelId: "",
                // materialId: "",
            },
            lineOptions: [],
            stageOptions: [],
            processOptions: [],
            tableData: [],
            tableLoading: false,
            modelOptions: [],
            materialOption: [],
        };
    },
    methods: {
        getData() {
            this.tableLoading = true;
            getSopHistory(this.searchForm).then((res) => {
                this.tableData = res.rows.map((item) => {
                    item["fileType"] = item.type === "0" ? "pdf文档" : "视频";
                    item["statusName"] =
                        item.versionStatus === "0" ? "正常" : "停用";
                    item.createTime = this.parseTableTime(
                        "",
                        "",
                        item.createTime
                    );
                    return item;
                });
                this.searchForm.total = res.total;
                this.tableLoading = false;
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
                this.stageOptions = response.rows;
            });
        },
        listProcess() {
            listProcess({ pageSize: 999, pageNum: 1 }).then((res) => {
                this.processOptions = res.rows;
            });
        },
        /** 查询有效的 站位序号 MesProcess */
        handleMesProcessOptionsByStageId(val) {
            getMesProcessOptionsByStageId(val).then((response) => {
                this.mesProcessOptions = response.rows;
            });
        },
        changeLine(val) {
            this.searchForm.stageId = null;
            this.changeStage(false);
            if (val) {
                getStageInTerminal(val).then((res) => {
                    this.stageOptions = res.rows;
                });
            } else {
                getMesStageOptions().then((response) => {
                    this.stageOptions = response.rows;
                });
            }
        },
        changeStage(val) {
            this.searchForm.processId = null;
            if (val) {
                getProcessInTerminal(this.searchForm.lineId, val).then(
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
        // // 获取机种信息
        // handleMesModelOptions() {
        //     getMesModelOptions().then((res) => {
        //         this.modelOptions = res.rows;
        //     });
        // },
        // //机种change查询料号
        // changeModel(val) {
        //     getPartNoList({ modelId: val }).then((res) => {
        //         this.materialOption = res.rows;
        //         this.searchForm.materialId = "";
        //     });
        // },
        changeStatus(val, row) {
            let action = val === "0" ? "启用" : "禁用";
            this.$confirm(
                `是否要${action}文件名称为"${row.sopName}"的这项吗?`,
                "警告",
                {
                    confirmButtonText: "确定",
                    cancelButtonText: "取消",
                    center: true,
                    type: "warning",
                }
            )
                .then(() => {
                    changeSopStatus(row).then((res) => {
                        if (res.code === 200) {
                            this.$modal.msgSuccess(res.msg);
                        } else {
                            this.$modal.msgError(res.msg);
                        }
                        // this.getData();
                    });
                })
                .catch(() => {
                    this.$message({
                        type: "info",
                        message: `已取消${action}`,
                    });
                    row.versionStatus = val === "0" ? "1" : "0";
                });
        },
        close_PDF_dialog() {
            this.pdfDialogVisible = false;
            this.pdfPages = [];
        },
    },
    mounted() {
        this.getData();
        this.handleMesLineOptions();
        this.handleMesStageOptions();
        this.listProcess();
        // this.handleMesModelOptions();
    },
};
</script>
