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
            <el-form-item label="筛选：">
                <el-cascader
                    v-model="cascaderData"
                    :props="props"
                    placeholder="请选择线别/区段/制程"
                    clearable
                ></el-cascader>
            </el-form-item>
            <el-form-item>
                <el-button type="primary" icon="el-icon-search" size="mini" @click="handleQuery">搜索</el-button>
                <el-button icon="el-icon-refresh" size="mini" @click="resetQuery">重置</el-button>
            </el-form-item>
        </el-form>

        <el-row :gutter="10" class="mb8">
            <el-col :span="1.5">
                <el-button
                    type="primary"
                    plain
                    icon="el-icon-plus"
                    size="mini"
                    @click="handleAdd"
                    v-hasPermi="['product:terminal:add']"
                >新增</el-button>
            </el-col>

            <right-toolbar :showSearch.sync="showSearch" @queryTable="getList"></right-toolbar>
        </el-row>

        <el-table
            v-loading="loading"
            :data="terminalList"
            @selection-change="handleSelectionChange"
        >
            <el-table-column type="selection" width="55" align="center" />
            <el-table-column label="工站编号" align="center" prop="terminalId" />
            <el-table-column label="线别名称" align="center" prop="lineName" />
            <el-table-column label="区段名称" align="center" prop="stageName" />
            <el-table-column label="制程名称" align="center" prop="processName" />
            <el-table-column label="工站名称" align="center" prop="terminalName" />
            <el-table-column label="状态" align="center" prop="status">
                <template slot-scope="scope">
                    <dict-tag :options="dict.type.mes_enabled" :value="scope.row.status" />
                </template>
            </el-table-column>
            <!--      <el-table-column label="备注" align="center" prop="remark" />-->
            <!--      <el-table-column label="操作" align="center" class-name="small-padding fixed-width">-->
            <!--        <template slot-scope="scope">-->
            <!--          <el-button-->
            <!--            size="mini"-->
            <!--            type="text"-->
            <!--            icon="el-icon-edit"-->
            <!--            @click="handleUpdate(scope.row)"-->
            <!--            v-hasPermi="['product:terminal:edit']"-->
            <!--          >修改</el-button>-->
            <!--        </template>-->
            <!--      </el-table-column>-->
        </el-table>

        <pagination
            v-show="total>0"
            :total="total"
            :page.sync="queryParams.pageNum"
            :limit.sync="queryParams.pageSize"
            @pagination="getList"
        />

        <!-- 添加或修改工站对话框 -->
        <el-dialog :title="title" :visible.sync="open" width="500px" append-to-body>
            <el-form ref="form" :model="form" :rules="rules" label-width="80px">
                <el-form-item label="线别名称" prop="lineId">
                    <el-select v-model="form.lineId" placeholder="请选择线别名称">
                        <el-option
                            v-for="item in mesLineOptions"
                            :key="item.lineId"
                            :label="item.lineName"
                            :value="item.lineId"
                        ></el-option>
                    </el-select>
                </el-form-item>

                <el-form-item label="区段名称" prop="stageId">
                    <el-select
                        v-model="form.stageId"
                        placeholder="请选择区段名称"
                        @change="handleMesProcessOptionsByStageId"
                    >
                        <el-option
                            v-for="item in mesStageOptions"
                            :key="item.stageId"
                            :label="item.stageName"
                            :value="item.stageId"
                        ></el-option>
                    </el-select>
                </el-form-item>

                <el-form-item label="制程名称" prop="processId">
                    <el-select v-model="form.processId" placeholder="请选择制程名称">
                        <el-option
                            v-for="item in mesProcessOptions"
                            :key="item.processId"
                            :label="item.processName"
                            :value="item.processId"
                        ></el-option>
                    </el-select>
                </el-form-item>

                <el-form-item label="工站数量" prop="terminalNumber">
                    <el-input-number
                        v-model="form.terminalNum"
                        @change="handleTerminalSequenceChange"
                        :min="1"
                        :max="10"
                        label="描述文字"
                    ></el-input-number>
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
    listTerminal,
    getTerminal,
    delTerminal,
    addTerminal,
    updateTerminal,
    getMesLineOptions,
    getMesStageOptions,
    getMesProcessOptionsByStageId,
    getLineInTerminal,
    getStageInTerminal,
    getProcessInTerminal
} from "@/api/product/terminal";

let id = 0;
let lineId = "";
export default {
    name: "Terminal",
    dicts: ["mes_enabled"],
    data() {
        let that = this;
        return {
            terminalNum: 1,
            cascaderData: [],
            props: {
                lazy: true,
                lazyLoad(node, resolve) {
                    const { level, value } = node;
                    console.log(node);
                    if (level === 0) {
                        getLineInTerminal().then(res => {
                            var nodes = [];
                            res.rows.map(item => {
                                nodes.push({
                                    value: item.lineId,
                                    label: item.lineName
                                    //leaf: level >= 2
                                });
                            });
                            resolve(nodes);
                        });
                    }

                    if (level === 1) {
                        lineId = value;
                        getStageInTerminal(value).then(res => {
                            var nodes = [];
                            res.rows.map(item => {
                                nodes.push({
                                    value: item.stageId,
                                    label: item.stageName
                                    //leaf: level >= 2
                                });
                            });
                            resolve(nodes);
                        });
                    }
                    if (level === 2) {
                        getProcessInTerminal(lineId, value).then(res => {
                            var nodes = [];
                            res.rows.map(item => {
                                nodes.push({
                                    value: item.processId,
                                    label: item.processName,
                                    leaf: level >= 2
                                });
                            });
                            resolve(nodes);
                        });
                        
                    }
                    // setTimeout(() => {
                    //     const nodes = Array.from({ length: level + 1 }).map(
                    //         item => ({
                    //             value: ++id,
                    //             label: `选项${id}`,
                    //             leaf: level >= 2
                    //         })
                    //     );
                    //     // 通过调用resolve将子节点数据返回，通知组件数据加载完成

                    // }, 1000);
                }
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
            // 工站表格数据
            terminalList: [],

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
                status: null
            },
            // 表单参数
            form: {},
            // 表单校验
            rules: {
                lineId: [
                    {
                        required: true,
                        message: "线别名称不能为空",
                        trigger: "change"
                    }
                ],
                stageId: [
                    {
                        required: true,
                        message: "区段名称不能为空",
                        trigger: "change"
                    }
                ],
                processId: [
                    {
                        required: true,
                        message: "制程名称不能为空",
                        trigger: "change"
                    }
                ]
            }
        };
    },
    created() {
        this.getList();
    },
    methods: {

        /** 查询有效的 线别 MesLine */
        handleMesLineOptions() {
            getMesLineOptions().then(response => {
                this.mesLineOptions = response.rows;
            });
        },
        /** 查询有效的 区段 MesStage */
        handleMesStageOptions() {
            getMesStageOptions().then(response => {
                this.mesStageOptions = response.rows;
            });
        },
        /** 查询有效的 区段 MesProcess */
        handleMesProcessOptionsByStageId(val) {
            getMesProcessOptionsByStageId(val).then(response => {
                this.mesProcessOptions = response.rows;
            });
        },

        /** 查询工站列表 */
        getList() {
            this.loading = true;
            listTerminal(this.queryParams).then(response => {
                this.terminalList = response.rows;
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
                terminalNum: "1"
            };
            this.resetForm("form");
        },
        /** 搜索按钮操作 */
        handleQuery() {
            this.queryParams.pageNum = 1;
            this.queryParams.lineId = this.cascaderData[0];
            this.queryParams.stageId = this.cascaderData[1];
            this.queryParams.processId = this.cascaderData[2];
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
            this.ids = selection.map(item => item.terminalId);
            this.single = selection.length !== 1;
            this.multiple = !selection.length;
        },
        /** 新增按钮操作 */
        handleAdd() {
            this.reset();
            this.open = true;
            this.title = "添加工站";

            this.handleMesLineOptions();
            this.handleMesStageOptions();
        },
        /** 修改按钮操作 */
        handleUpdate(row) {
            this.reset();
            const terminalId = row.terminalId || this.ids;
            getTerminal(terminalId).then(response => {
                this.form = response.data;
                this.open = true;
                this.title = "修改工站";
            });
        },
        /** 提交按钮 */
        submitForm() {
            this.$refs["form"].validate(valid => {
                if (valid) {
                    if (this.form.terminalId != null) {
                        updateTerminal(this.form).then(response => {
                            this.$modal.msgSuccess("修改成功");
                            this.open = false;
                            this.getList();
                        });
                    } else {
                        addTerminal(this.form).then(response => {
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
            const terminalIds = row.terminalId || this.ids;
            this.$modal
                .confirm(
                    '是否确认删除工站编号为"' + terminalIds + '"的数据项？'
                )
                .then(function() {
                    return delTerminal(terminalIds);
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
                "product/terminal/export",
                {
                    ...this.queryParams
                },
                `terminal_${new Date().getTime()}.xlsx`
            );
        },

        //站点次序
        handleTerminalSequenceChange(value) {
            console.log(value);
        }
    }
};
</script>
