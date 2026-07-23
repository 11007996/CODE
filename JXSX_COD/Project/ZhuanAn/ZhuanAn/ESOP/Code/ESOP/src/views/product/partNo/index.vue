<!-- 料号管理 -->
<template>
    <div class="app-container">
        <!-- 查询 -->
        <el-row class="paddingRow searchRow">
            <el-form inline :model="searchForm" ref="searchForm">
                <el-form-item label="料号" prop="materialName">
                    <el-input
                        v-model="searchForm.materialName"
                        clearable
                        size="mini"
                        placeholder="请输入料号"
                    />
                </el-form-item>
                <el-form-item label="料号描述" prop="materialDesc">
                    <el-input
                        v-model="searchForm.materialDesc"
                        clearable
                        size="mini"
                        placeholder="请输入料号描述"
                    />
                </el-form-item>
                <el-form-item>
                    <el-button
                        icon="el-icon-search"
                        type="primary"
                        plain
                        size="mini"
                        @click="getData"
                        >查询</el-button
                    >
                    <el-button
                        icon="el-icon-refresh"
                        type="warning"
                        plain
                        size="mini"
                        @click="resetSearchForm"
                        >重置</el-button
                    >
                    <el-button
                        icon="el-icon-plus"
                        type="success"
                        plain
                        size="mini"
                        @click="handleInsert"
                        v-hasPermi="['replace:materialInfo:add']"
                        >新增</el-button
                    >
                </el-form-item>
            </el-form>
        </el-row>

        <!-- 表格 -->
        <el-row class="paddingRow">
            <el-table :data="tableData" border :loading="tableLoading" stripe >
                <el-table-column label="料号名称" prop="materialName" show-overflow-tooltip  min-width="140px"/>
                <el-table-column label="料号描述" prop="materialDesc" show-overflow-tooltip  min-width="140px"/>
                <el-table-column label="机种名称" prop="modelName" show-overflow-tooltip  min-width="140px"/>
                <el-table-column label="创建人" prop="creatorName" show-overflow-tooltip  />
                <el-table-column label="创建时间" prop="createTime" min-width="140px"/>
                <el-table-column label="更新人" prop="updaterName" />
                <el-table-column label="更新时间" prop="updateTime" min-width="140px"/>
                <el-table-column label="操作" fixed="right" min-width="150px">
                    <template slot-scope="scope">
                        <el-button
                            size="mini"
                            type="text"
                            icon="el-icon-edit"
                            @click="handleUpdate(scope.row)"
                            >修改</el-button
                        >
                        <el-button
                            size="mini"
                            type="text"
                            icon="el-icon-link"
                            @click="handleCopy(scope.row)"
                            >复制工艺</el-button
                        >
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
        </el-row>
        <!-- 新增/修改弹窗 -->
        <el-dialog
            center
            :visible.sync="dialogVisible"
            :title="dialogTitle"
            @open="dialogOpen"
            @close="dialogClose"
            width="500px"
        >
            <el-form
                inline
                :model="dialogForm"
                label-width="80px"
                :rules="rules"
                ref="dialogForm"
            >
                <el-form-item label="料号名称" prop="materialName" style="width: 100%;">
                    <el-input
                        v-model="dialogForm.materialName"
                        clearable
                        placeholder="请输入料号名称"
                    />
                </el-form-item>
                <el-form-item label="料号描述" prop="materialDesc" style="width: 100%;">
                    <el-input
                        v-model="dialogForm.materialDesc"
                        clearable
                        placeholder="请输入料号描述"
                    />
                </el-form-item>
                <el-form-item label="绑定机种" prop="modelId" style="width: 100%;">
                    <el-select
                        v-model="dialogForm.modelId"
                        placeholder="请选择绑定机种"
                        clearable
                        filterable
                    >
                        <el-option
                            v-for="(item, index) in modelOption"
                            :label="item.modelName"
                            :value="item.modelId"
                            :key="index"
                        >
                        </el-option>
                    </el-select>
                </el-form-item>
            </el-form>
            <span slot="footer" class="dialog-footer">
                <el-button type="success" @click="submitForm" size="mini" plain
                    >提 交</el-button
                >
                <el-button
                    type="danger"
                    @click="dialogVisible = false"
                    size="mini"
                    plain
                    >取 消</el-button
                >
            </span>
        </el-dialog>
        <!-- 复制工艺弹窗 -->
        <el-dialog
            center
            :visible.sync="copyDialogVisible"
            :title="copyDialogTitle"
            @open="copyDialogOpen"
            @close="copyDialogClose"
            destroy-on-close
            class="copyDialog"
        >
            <el-form
                inline
                :model="copyForm"
                :rules="copyFormRules"
                ref="copyForm"
            >
                <el-form-item label="母版料号" prop="parentName">
                    <el-input v-model="copyForm.parentName" disabled />
                </el-form-item>
                <el-form-item label="子版料号" prop="materialId">
                    <el-select
                        v-model="copyForm.materialId"
                        clearable
                        multiple
                        filterable
                    >
                        <el-option
                            v-for="item in partNoOption"
                            :key="item.id"
                            :label="item.materialName"
                            :value="item.id"
                        />
                    </el-select>
                </el-form-item>
            </el-form>
            <!-- <el-table
                :data="treeData"
                row-key="id"
                border
                default-expand-all
                :tree-props="{
                    children: 'children',
                    hasChildren: 'hasChildren',
                }"
            >
                <el-table-column label="" prop="" />
            </el-table> -->
            <el-divider content-position="left"
                >请选择需要复制的线体</el-divider
            >
            <el-input placeholder="输入关键字进行过滤" v-model="filterText">
            </el-input>
            <el-tree
                :data="treeData"
                :check-strictly="true"
                show-checkbox
                :props="partNoProps"
                :load="loadNode"
                lazy
                :filter-node-method="filterNode"
                ref="tree"
                :empty-text="'料号' + copyForm.parentName + '下暂无绑定线体'"
                v-loading="treeLoading"
                element-loading-text="拼命加载中"
                element-loading-spinner="el-icon-loading"
                element-loading-background="rgba(0, 0, 0, 0.8)"
            >
            </el-tree>
            <span slot="footer" class="dialog-footer">
                <el-button
                    type="success"
                    @click="submitCopyForm"
                    size="mini"
                    plain
                    >提 交</el-button
                >
                <el-button
                    type="danger"
                    @click="copyDialogVisible = false"
                    size="mini"
                    plain
                    >取 消</el-button
                >
            </span>
        </el-dialog>
    </div>
</template>
<script>
import {
    getPartNoList,
    insertPartNo,
    updatePartNo,
    copyTemplate,
} from "@/api/product/partNo";
import { listTerminal } from "@/api/product/terminal";
import { listLine } from "@/api/product/line";
import {
    getStageInTerminal,
    getProcessInTerminal,
} from "@/api/product/terminal";
import { listModel } from "@/api/product/model";
export default {
    data() {
        return {
            filterText: "",
            //查询表单
            searchForm: {
                pageSize: 10,
                pageNum: 1,
                total: 0,
                materialName: "",
                materialDesc: "",
            },
            searchForm1: {
                pageSize: 500,
                pageNum: 1,
                status: "0",
            },
            treeLoading: false,
            treeData: [],
            partNoProps: {
                children: "children",
                label: "label",
                isLeaf: "leaf",
            },
            //弹窗表单
            dialogForm: {},
            //表单验证规则
            rules: {
                materialName: [
                    {
                        required: true,
                        message: "请输入料号名称",
                        trigger: "blur",
                    },
                ],
                modelId: [
                    {
                        required: true,
                        message: "请选择绑定机种",
                        trigger: ["blur", "change"],
                    },
                ],
            },
            //表格数据
            tableData: [],
            //表格加载动画
            tableLoading: false,
            dialogVisible: false,
            //表格的标题
            dialogTitle: "",
            //机种
            modelOption: [],
            copyDialogVisible: false,
            copyDialogTitle: "",
            copyForm: {},
            copyFormRules: {
                parentName: [
                    {
                        required: true,
                    },
                ],
                materialId: [
                    {
                        required: true,
                        message: "请选择子版料号",
                        trigger: "change",
                    },
                ],
            },
            partNoOption: [],
            selectLineId: "",
        };
    },
    methods: {
        //查询，获取表格数据
        getData() {
            this.tableLoading = true;
            getPartNoList(this.searchForm).then((res) => {
                this.tableData = res.rows;
                this.searchForm.total = res.total;
                this.tableLoading = false;
            });
        },
        //重置搜索条件
        resetSearchForm() {
            this.$refs.searchForm.resetFields();
            this.getData();
        },
        //修改
        handleUpdate(row) {
            this.dialogVisible = true;
            this.dialogTitle = "修改料号信息";
            this.$nextTick(() => {
                this.dialogForm = JSON.parse(JSON.stringify(row));
            });
        },
        handleCopy(row) {
            this.copyDialogTitle = `复制料号：'${row.materialName}'的工艺`;
            this.copyDialogVisible = true;
            getPartNoList().then((res) => {
                this.partNoOption = res.rows.filter((item) => {
                    return item.id !== row.id;
                });
            });
            this.$nextTick(() => {
                this.copyForm.parent = row.id;
                this.treeLoading = true;
                listLine({ materialId: row.id }).then((res) => {
                    this.treeData = res.rows.map((item) => {
                        return {
                            label: item.lineName,
                            value: item.lineId,
                        };
                    });
                    this.treeLoading = false;
                });
                this.copyForm.parentName = row.materialName;
            });
        },
        filterNode(value, data) {
            if (!value) return true;
            return data.label.indexOf(value) !== -1;
        },
        loadNode(node, resolve) {
            let { data, level } = node;
            if (level === 1) {
                this.selectLineId = data.value;
                getStageInTerminal(data.value).then((res) => {
                    let nodes = res.rows.map((item) => {
                        return {
                            label: item.stageName,
                            value: item.stageId,
                            disabled: true,
                        };
                    });
                    return resolve(nodes);
                });
            }
            if (level === 2) {
                getProcessInTerminal(this.selectLineId, data.value).then(
                    (res) => {
                        let nodes = res.rows.map((item) => {
                            return {
                                label: item.processName,
                                value: item.processId,
                                disabled: true,
                            };
                        });
                        return resolve(nodes);
                    }
                );
            }
            if (level === 3) {
                listTerminal({ processId: data.value }).then((res) => {
                    let nodes = res.rows.map((item) => {
                        return {
                            label: item.terminalName,
                            value: item.terminalId,
                            leaf: true,
                            disabled: true,
                        };
                    });
                    return resolve(nodes);
                });
            }
        },
        copyDialogOpen() {
            this.copyForm = {
                materialId: [],
                parentName: "",
                parent: "",
            };
        },
        copyDialogClose() {
            this.$refs.copyForm.resetFields();
        },
        submitCopyForm() {
            this.$refs.copyForm.validate((valid) => {
                if (valid) {
                    let lineIds = this.$refs.tree
                        .getCheckedNodes()
                        .map((item) => {
                            return item.value;
                        });
                    let param = {
                        materialId: this.copyForm.materialId,
                        lineIds,
                    };
                    copyTemplate(param).then((res) => {
                        if (res.code === 200) {
                            this.$modal.msgSuccess(res.msg);
                        } else {
                            this.$modal.msgError(res.msg);
                        }
                        this.copyDialogVisible = false;
                    });
                }
            });
        },
        //新增
        handleInsert() {
            this.dialogVisible = true;
            this.dialogTitle = "新增料号";
        },
        //弹窗打开回调
        dialogOpen() {
            this.dialogForm = {
                materialName: "",
                materialDesc: "",
                modelId: "",
            };
        },
        //弹窗关闭回调
        dialogClose() {
            this.$refs.dialogForm.resetFields();
            // this.$refs.dialogForm.clearValidate()
        },
        //提交表单
        submitForm() {
            this.$refs.dialogForm.validate((valid) => {
                if (valid) {
                    if (this.dialogTitle === "新增料号") {
                        insertPartNo(this.dialogForm).then((res) => {
                            if (res.code === 200) {
                                this.$modal.msgSuccess(res.msg);
                            } else {
                                this.$modal.msgError(res.msg);
                            }
                            this.dialogVisible = false;
                            this.getData();
                        });
                    } else {
                        updatePartNo(this.dialogForm).then((res) => {
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
                    this.$modal.msgError("请按提示输入指定内容！");
                }
            });
        },
        listModel() {
            listModel(this.searchForm1).then((res) => {
                this.modelOption = res.rows;
            });
        },
    },
    mounted() {
        this.getData();
        this.listModel();
    },
    watch: {
        filterText(val) {
            this.$refs.tree.filter(val);
        },
    },
};
</script>
<style lang="scss" scoped>
::v-deep .copyDialog {
    .el-dialog {
        height: 80%;
        display: flex;
        flex-direction: column;
        overflow: hidden;
    }
    .el-dialog__body {
        flex: 1;
        overflow-y: auto;
    }
}
</style>
