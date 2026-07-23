<template>
  <el-dialog :title="title" :model-value="visible" :width="width" :top="top" @update:visible="$emit('update:visible', $event)" @close="handleClose">
    <!-- 查询条件表单 -->
    <el-form :model="queryParams" ref="queryFormRef" :inline="true" label-width="auto" class="selector-form">
      <slot name="queryForm" :queryParams="queryParams"></slot>
      <el-form-item>
        <el-button type="primary" @click="handleQuery">查询</el-button>
        <el-button @click="resetQuery">重置</el-button>
      </el-form-item>
    </el-form>

    <!-- 数据表格 -->
    <el-table
      :data="tableData"
      v-loading="loading"
      @selection-change="handleSelectionChange"
      @row-click="handleRowClick"
      :row-class-name="tableRowClassName"
      :row-key="rowKey"
      ref="tableRef">
      <el-table-column v-if="multiple" type="selection" width="55" :reserve-selection="true"></el-table-column>
      <slot name="tableColumns"></slot>
    </el-table>

    <!-- 分页 -->
    <div class="pagination-container">
      <el-pagination
        v-model:current-page="queryParams.pageNum"
        v-model:page-size="queryParams.pageSize"
        :total="total"
        :page-sizes="[10, 20, 30, 50]"
        layout="total, sizes, prev, pager, next, jumper"
        @size-change="handleSizeChange"
        @current-change="handleCurrentChange" />
    </div>

    <!-- 底部操作按钮 -->
    <template #footer>
      <div class="dialog-footer">
        <el-button @click="handleClose">取消</el-button>
        <el-button type="primary" @click="handleConfirm">确认</el-button>
      </div>
    </template>
  </el-dialog>
</template>

<script setup="DataSelectorDialog">
import { ref, watch } from 'vue'
const { proxy } = getCurrentInstance()
const props = defineProps({
  // 弹窗标题
  title: {
    type: String,
    default: '选择数据'
  },
  // 是否显示弹窗
  visible: {
    type: Boolean,
    default: false
  },
  // 弹窗宽度
  width: {
    type: String,
    default: '60%'
  },
  // 距离顶部距离
  top: {
    type: String,
    default: '5vh'
  },
  // 是否多选
  multiple: {
    type: Boolean,
    default: false
  },
  // 行数据的Key
  rowKey: {
    type: String,
    default: 'id'
  },
  // 初始查询参数
  initQueryParams: {
    type: Object,
    default: () => ({
      pageNum: 1,
      pageSize: 10
    })
  },
  // 加载数据的方法
  loadData: {
    type: Function,
    required: true
  }
})

const emit = defineEmits(['update:visible', 'confirm'])

// 查询表单引用
const queryFormRef = ref(null)
// 表格引用
const tableRef = ref(null)
// 查询参数
const queryParams = ref({ ...props.initQueryParams })
// 表格数据
const tableData = ref([])
// 加载状态
const loading = ref(false)
// 总条数
const total = ref(0)
// 选中的行
const selectedRows = ref([])
// 当前选中的行ID集合
const selectedRowIds = ref(new Set())

// 监听visible变化
watch(
  () => props.visible,
  (val) => {
    if (val) {
      getData()
      if (tableRef.value) {
        tableRef.value.clearSelection()
      }
      selectedRows.value = []
      selectedRowIds.value = new Set()
    }
  }
)

// 获取表格数据
const getData = async () => {
  try {
    loading.value = true
    const res = await props.loadData(queryParams.value)
    tableData.value = res.result || res.data || []
    total.value = res.totalNum || 0
  } catch (error) {
    tableData.value = []
    total.value = 0
  } finally {
    loading.value = false
  }
}

// 查询
const handleQuery = () => {
  queryParams.value.pageNum = 1
  getData()
}

// 重置查询
const resetQuery = () => {
  queryFormRef.value?.resetFields()
  queryParams.value = { ...props.initQueryParams }
  handleQuery()
}

// 分页大小变化
const handleSizeChange = (val) => {
  queryParams.value.pageSize = val
  getData()
}

// 当前页变化
const handleCurrentChange = (val) => {
  queryParams.value.pageNum = val
  getData()
}

// 选中行变化
const handleSelectionChange = (selection) => {
  selectedRows.value = selection
  // 更新选中行ID集合
  selectedRowIds.value = new Set(selection.map((item) => item[props.rowKey]))
}

// 处理行点击事件
const handleRowClick = (row) => {
  if (!props.multiple) {
    // 单选模式：清空之前的选择
    tableRef.value.clearSelection()
    selectedRowIds.value.clear()
  }
  // 切换当前行的选中状态
  tableRef.value.toggleRowSelection(row, undefined)
}

// 为行添加类名
const tableRowClassName = ({ row }) => {
  return selectedRowIds.value.has(row[props.rowKey]) ? 'selected-row' : ''
}

// 关闭弹窗
const handleClose = () => {
  emit('update:visible', false)
}

// 确认选择
const handleConfirm = () => {
  if (!props.multiple && selectedRows.value.length > 1) {
    proxy.$modal.msgError('请只选择一条数据')
    return
  }

  if (selectedRows.value.length === 0) {
    proxy.$modal.msgError('请至少选择一条数据')
    return
  }

  emit('confirm', props.multiple ? selectedRows.value : selectedRows.value[0])
  handleClose()
}
</script>

<style scoped>
.selector-form {
  margin-bottom: 20px;
}
.pagination-container {
  margin-top: 20px;
  text-align: right;
}
.dialog-footer {
  text-align: center;
}
/* 选中行样式 */
:deep(.el-table__body tr.selected-row) {
  background-color: #f0f7ff !important;
}
</style>
