<template>
  <div class="app-container">
    <el-form :model="queryParams" ref="queryForm" size="small" :inline="true" v-show="showSearch" label-width="68px">
      <el-form-item label="线别名称" prop="lineName">
        <el-input
          v-model="queryParams.lineName"
          placeholder="请输入线别名称"
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
          v-hasPermi="['product:line:add']"
        >新增</el-button>
      </el-col>
      <el-col :span="1.5">
        <el-button
          type="success"
          plain
          icon="el-icon-edit"
          size="mini"
          :disabled="single"
          @click="handleUpdate"
          v-hasPermi="['product:line:edit']"
        >修改</el-button>
      </el-col>
<!--      <el-col :span="1.5">-->
<!--        <el-button-->
<!--          type="danger"-->
<!--          plain-->
<!--          icon="el-icon-delete"-->
<!--          size="mini"-->
<!--          :disabled="multiple"-->
<!--          @click="handleDelete"-->
<!--          v-hasPermi="['product:line:remove']"-->
<!--        >删除</el-button>-->
<!--      </el-col>-->
      <el-col :span="1.5">
        <el-button
          type="warning"
          plain
          icon="el-icon-download"
          size="mini"
          @click="handleExport"
          v-hasPermi="['product:line:export']"
        >导出</el-button>
      </el-col>
      <right-toolbar :showSearch.sync="showSearch" @queryTable="getList"></right-toolbar>
    </el-row>

    <el-table v-loading="loading" :data="lineList" @selection-change="handleSelectionChange">
      <el-table-column type="selection" width="55" align="center" />
      <el-table-column label="线别编号" align="center" prop="lineId" />
      <el-table-column label="线别名称" align="center" prop="lineName" />
      <el-table-column label="线别类型" align="center" prop="lineType" >
        <template slot-scope="scope">
          <dict-tag :options="dict.type.mes_line_type" :value="scope.row.lineType"/>
        </template>
      </el-table-column>
      <el-table-column label="工作中心" align="center" prop="workCenter" >
        <template slot-scope="scope">
          <dict-tag :options="dict.type.mes_work_center" :value="scope.row.workCenter"/>
        </template>
      </el-table-column>
      <el-table-column label="厂区" align="center" prop="siteName" />
      <el-table-column label="状态" align="center" prop="status" >
        <template slot-scope="scope">
          <el-switch
            v-model="scope.row.status"
            active-value="0"
            inactive-value="1"
            @change="handleStatusChange(scope.row)"
          ></el-switch>
        </template>
      </el-table-column>
      <el-table-column label="备注" align="center" prop="remark" />
      <el-table-column label="操作" align="center" class-name="small-padding fixed-width">
        <template slot-scope="scope">
          <el-button
            size="mini"
            type="text"
            icon="el-icon-edit"
            @click="handleUpdate(scope.row)"
            v-hasPermi="['product:line:edit']"
          >修改</el-button>
<!--          <el-button-->
<!--            size="mini"-->
<!--            type="text"-->
<!--            icon="el-icon-delete"-->
<!--            @click="handleDelete(scope.row)"-->
<!--            v-hasPermi="['product:line:remove']"-->
<!--          >删除</el-button>-->
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

    <!-- 添加或修改线别信息对话框 -->
    <el-dialog :title="title" :visible.sync="open" width="500px" append-to-body>
      <el-form ref="form" :model="form" :rules="rules" label-width="80px">

       

        <el-form-item label="线别名称" prop="lineName">
          <el-input v-model="form.lineName" placeholder="请输入线别名称" />
        </el-form-item>

        <el-form-item label="线别类型" prop="lineType">
          <el-select v-model="form.lineType" placeholder="请选择线别类型" >
            <el-option
              v-for="dict in dict.type.mes_line_type"
              :key="dict.value"
              :label="dict.label"
              :value="dict.value"
              :disabled="dict.status == 1"
            />
          </el-select>
        </el-form-item>

        <el-form-item label="工作中心" prop="workCenter">
          <el-select v-model="form.workCenter" placeholder="请选择工作中心" >
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
          <el-select v-model="form.siteId" placeholder="请选择线别">
              <el-option
                  v-for="item in siteOptions"
                  :key="item.siteId"
                  :label="item.siteName"
                  :value="item.siteId"
              ></el-option>
          </el-select>
        </el-form-item>

        <el-form-item label="备注" prop="remark">
          <el-input v-model="form.remark"  type="textarea" placeholder="请输入备注" />
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
import { listLine, getLine, delLine, addLine, updateLine, changeLineStatus,  getMesSiteOptions } from '@/api/product/line'

export default {
  dicts: ['mes_work_center','mes_line_type'],
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

      siteOptions:[],
      // 查询参数
      queryParams: {
        pageNum: 1,
        pageSize: 10,
        lineName: null,
        lineType: null,
        workCenter: null,
        status: null,
      },
      // 表单参数
      form: {},
      // 表单校验
      rules: {
        siteId: {
                    required: true,
                    message: "厂区名称不能为空",
                    trigger: "shange"
                },
        lineName: [
          { required: true, message: "线别名称不能为空", trigger: "blur" }
        ],
      }
    };
  },
  created() {
    this.getList();
  },
  methods: {
    /** 查询线别信息列表 */
    getList() {
      this.loading = true;
      listLine(this.queryParams).then(response => {
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
        updateTime: null
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
      this.ids = selection.map(item => item.lineId)
      this.single = selection.length!==1
      this.multiple = !selection.length
    },
    /** 新增按钮操作 */
    handleAdd() {
      this.reset();
      this.open = true;
      this.title = "添加线别信息";
    },
    // 获取厂区信息
    handleMesSiteOptions() {
        getMesSiteOptions().then(response => {
            this.siteOptions = response.rows;
        });
    },

    /** 修改按钮操作 */
    handleUpdate(row) {
      this.reset();
      const lineId = row.lineId || this.ids
      getLine(lineId).then(response => {
        this.form = response.data;
        this.open = true;
        this.title = "修改线别信息";
      });
    },
    // 机种状态修改
    handleStatusChange(row) {
      let text = row.status === "0" ? "启用" : "停用";
      this.$modal.confirm('确认要"' + text + '""' + row.lineName + '"线别吗？').then(function() {
        return changeLineStatus(row.lineId, row.status);
      }).then(() => {
        this.$modal.msgSuccess(text + "成功");
      }).catch(function() {
        row.status = row.status === "0" ? "1" : "0";
      });
    },
    /** 提交按钮 */
    submitForm() {
      this.$refs["form"].validate(valid => {
        if (valid) {
          if (this.form.lineId != null) {
            updateLine(this.form).then(response => {
              this.$modal.msgSuccess("修改成功");
              this.open = false;
              this.getList();
            });
          } else {
            addLine(this.form).then(response => {
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
      this.$modal.confirm('是否确认删除线别信息编号为"' + lineIds + '"的数据项？').then(function() {
        return delLine(lineIds);
      }).then(() => {
        this.getList();
        this.$modal.msgSuccess("删除成功");
      }).catch(() => {});
    },
    /** 导出按钮操作 */
    handleExport() {
      this.download('product/line/export', {
        ...this.queryParams
      }, `line_${new Date().getTime()}.xlsx`)
    }
  }
};
</script>
