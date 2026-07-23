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
            <el-form-item label="OA 单号" prop="oaId">
                <el-input
                    v-model="queryParams.oaId"
                    placeholder="请输入OA单号"
                    clearable
                    @keyup.enter.native="handleQuery"
                />
            </el-form-item>
            <el-form-item label="签核状态" prop="status">
                <el-select v-model="queryParams.status" placeholder="请选择OA签核状态" clearable>
                    <el-option
                        v-for="dict in dict.type.oa_status"
                        :key="dict.value"
                        :label="dict.label"
                        :value="dict.value"
                    ></el-option>
                </el-select>
            </el-form-item>
            <el-form-item label="sop名称" prop="sopName">
                <el-input
                    v-model="queryParams.sopName"
                    placeholder="请输入sop名称"
                    clearable
                    @keyup.enter.native="handleQuery"
                />
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
                    v-hasPermi="['product:oa:add']"
                >新增</el-button>
            </el-col>
            <right-toolbar :showSearch.sync="showSearch" @queryTable="getList"></right-toolbar>
        </el-row>
        <el-table v-loading="loading" :data="oaList" @selection-change="handleSelectionChange">
            <!-- <el-table-column type="selection" width="55" align="center" /> -->
            <el-table-column label="OA 单号" align="center" prop="oaId" />

            <el-table-column label="sop名称" align="center" prop="sopName" />
            <el-table-column label="版本" align="center" prop="version" />
            <el-table-column label="机种" align="center" prop="modelName" />
            <el-table-column label="绑定站点" align="center">
                <template
                    slot-scope="scope"
                >{{scope.row.lineName}}/{{scope.row.stageName}}/{{scope.row.processName}}</template>
            </el-table-column>
            <el-table-column label="签核状态" align="center" prop="status">
                <template slot-scope="scope">
                    <dict-tag :options="dict.type.oa_status" :value="scope.row.status" />
                </template>
            </el-table-column>
            <!-- <el-table-column label="备注" align="center" prop="remark" /> -->
            <el-table-column label="操作" align="center" class-name="small-padding fixed-width">
                <template slot-scope="scope">
                    <el-button
                        v-if="scope.row.status==0"
                        size="mini"
                        type="text"
                        icon="el-icon-edit"
                        @click="handleSendOa(scope.row)"
                    >OA送签</el-button>
                </template>
            </el-table-column>
        </el-table>
        <pagination
            v-show="total>0"
            :total="total"
            :page.sync="queryParams.pageNum"
            :limit.sync="queryParams.pageSize"
            @pagination="getList"
        />

        <!-- 添加或修改oa签核对话框 -->
        <el-dialog :title="title" :visible.sync="open" width="500px" append-to-body @open="setForm">
            <el-form ref="form" :model="form" :rules="rules" label-width="80px">
                
                <!-- 请选择签核人员 -->
                <el-form-item label="会签人员" prop="OACountersignUserList">
                    <el-select v-model="form.OACountersignUserList" multiple placeholder="请选择">
                        <el-option
                            v-for="item in allowCountersignUserOptions"
                            :key="item.userName"
                            :label="item.nickName"
                            :value="item.userName">
                        </el-option>
                    </el-select>
                </el-form-item>

                <el-form-item label="机种" prop="modelId">
                    <el-select v-model="form.modelId" placeholder="请选择机种">
                        <el-option
                            v-for="item in modelOptions"
                            :key="item.modelId"
                            :label="item.modelName"
                            :value="item.modelId"
                        ></el-option>
                    </el-select>
                </el-form-item>

                <el-form-item label="站点" prop="cascaderOptions">
                    <el-cascader
                        v-model="form.cascaderOptions"
                        :props="props"
                        placeholder="请选择线别/区段/制程"
                        clearable
                    ></el-cascader>
                </el-form-item>

                <el-form-item label="文件上传" prop="fileName">
                    <el-upload
                        accept=".pdf, .mp4"
                        action
                        :on-change="fileOnChange"
                        :before-upload="fileBeforeUpload"
                        :auto-upload="false"
                        :show-file-list="false"
                    >
                    <!-- <el-table>
                        <el-table-column><el-button type="success">点击上传</el-button></el-table-column>
                        <el-table-column><div class="el-upload__tip" slot="tip">文件:{{fileName}}</div></el-table-column>
                    </el-table> -->
                    <el-button type="success">点击上传</el-button>
                    <div class="el-upload__tip" slot="tip">文件:{{fileName}}</div>
                    </el-upload>
                </el-form-item>

                <el-form-item label="版本" prop="version">
                    <el-input v-model="form.version" placeholder="请输入版本" clearable />
                </el-form-item>
                <el-form-item label="备注" prop="remark">
                    <el-input v-model="form.remark" type="textarea" placeholder="请输入内容" />
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
    listOa,
    getOa,
    delOa,
    addOa,
    updateOa,
    getMesModelOptions,
    uploadFile,
    addOaEsopInfo,
    sendOa,getOACountersignUserList
} from "@/api/product/oa";

import {
    getLineInTerminal,
    getStageInTerminal,
    getProcessInTerminal
} from "@/api/product/terminal";
import { Loading } from "element-ui";

let lineId = "";
export default {
    name: "Oa",
    dicts: ["oa_status"],
    data() {
        return {
            // 遮罩层
            loading: true,
            // 选中数组
            ids: [],
            // 非单个禁用
            single: true,
            // 非多个禁用
            multiple: false,
            // 显示搜索条件
            showSearch: true,
            // 总条数
            total: 0,
            // oa签核表格数据
            oaList: [],
            // 弹出层标题
            title: "",
            // 是否显示弹出层
            open: false,

            // 机种List
            modelOptions: [],
            // 线别、区段、制程
            cascaderOptions: [],

            fileName: "",

            // 可签核人员列表签人员
            allowCountersignUserOptions:[],

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
                }
            },

            // 查询参数
            queryParams: {
                pageNum: 1,
                pageSize: 10,
                oaId: null,
                requestId: null,
                sopId: null,
                status: null
            },
            // 表单参数
            form: {
                file: "",
                fileName: ""
            },
            // 表单校验
            rules: {
                modelId: {
                    required: true,
                    message: "机种不能为空",
                    trigger: "change"
                },
                cascaderOptions: [
                    {
                        required: true,
                        message: "站点信息不能为空",
                        trigger: ["change", "blur"]
                    }
                ],

                version: {
                    required: true,
                    message: "版本不能为空",
                    trigger: "blur"
                },

                fileName: {
                    required: true,
                    message: "未上传文件",
                    trigger: "change"
                },
                OACountersignUserList:[
                    {
                        required: true,
                        message: "会签人员不能为空",
                        trigger:  "change"
                    }
                ]


            }
        };
    },
    created() {
        this.getList();
    },
    methods: {
        setForm(){
            this.form={
                file: "",
                fileName: ""
            }
        },
        fileOnChange(file) {
            console.log(file);
            this.form["fileName"] = file.name;
            this.form.file = file;
            this.$nextTick(() => {
                this.$refs.form.validateField("fileName");
            });
            this.fileName = this.form.file.name;
        },
        fileBeforeUpload(file) {},

        // 获取机种信息
        handleMesModelOptions() {
            getMesModelOptions().then(response => {
                this.modelOptions = response.rows;
            });
        },
        /** 查询oa签核列表 */
        getList() {
            this.loading = true;
            listOa(this.queryParams).then(response => {
                this.oaList = response.rows;
                this.total = response.total;
                this.loading = false;
            });
        },

        // 表单重置
        reset() {
            this.form = {};
            this.fileName = "";
            this.newFileName = "";
            this.originalFileName = "";
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
            this.ids = selection.map(item => item.oaId);
            this.single = selection.length !== 1;
            this.multiple = !selection.length;
        },
        /** 新增按钮操作 */
        handleAdd() {
            this.reset();
            this.open = true;
            this.title = "添加oa签核";
            this.cascaderOptions = null;
            this.handleMesModelOptions();
            this.handleSelectOACountersignUser();

        },

        /** 获取有OA签核权限的人员信息 */
        handleSelectOACountersignUser(){
            getOACountersignUserList().then(response => {
                this.allowCountersignUserOptions = response.rows;
                console.log("获取有OA签核权限的人员信息成功")
            });
        },

        /** 提交按钮 */
        submitForm() {
            this.$refs["form"].validate(valid => {
                if (valid) {
                    // const loadingInstance = this.$loading({
                    //     lock: true,
                    //     text: "Loading",
                    //     spinner: "el-icon-loading",
                    //     background: "rgba(0, 0, 0, 0.7)"
                    // });
                    if (this.form.oaId != null) {
                        updateOa(this.form).then(response => {
                            this.$modal.msgSuccess("修改成功");

                            this.open = false;
                            this.getList();
                        });
                    } else {
                        let formData = new FormData();
                        formData.append("file", this.form.file.raw);
                        formData.append("version", this.form.version);
                        formData.append("modelId", this.form.modelId);
                        formData.append("lineId", this.form.cascaderOptions[0]);
                        formData.append("stageId", this.form.cascaderOptions[1]);
                        formData.append("processId",this.form.cascaderOptions[2]);
                        formData.append("countersignUser",this.form.OACountersignUserList.toString())
                         //console.log(this.form.OACountersignUserList.toString())
                         //loadingInstance.close();
                        addOaEsopInfo(formData).then(response=> {
                            if (response.code === 200) {
                                this.$modal.msgSuccess(response.msg);
                                this.$modal.msgSuccess("新增成功");
                                //loadingInstance.close();
                                this.open = false;
                                this.getList();
                            } else {
                                this.$modal.msgError(response.msg);
                            }
                            // loadingInstance.close();
                        });
                    }
                }
            });
        },
        // 取消按钮
        cancel() {
            this.open = false;
            this.reset();
        },
        handleSendOa(row) {
            console.log(row.oaId);
            sendOa(row.oaId).then(response => {
                if (response.code === 200) {
                    this.$modal.msgSuccess(response.msg);
                } else {
                    this.$modal.msgError(response.msg);
                }
            });
            this.handleQuery();
        }
    }
};
</script>
