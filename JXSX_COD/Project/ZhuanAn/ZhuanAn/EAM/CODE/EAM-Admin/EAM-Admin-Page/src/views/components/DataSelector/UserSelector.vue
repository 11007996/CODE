<template>
  <DataSelectorDialog
    :visible="visible"
    title="选择用户"
    :load-data="loadUserData"
    :init-query-params="queryParams"
    :row-key="rowKey"
    :multiple="multiple"
    @confirm="$emit('confirm', $event)">
    <template #queryForm="{ queryParams }">
      <el-form-item label="用户名" prop="name">
        <el-input v-model="queryParams.name" placeholder="请输入用户名" clearable />
      </el-form-item>
      <el-form-item label="手机号" prop="phone">
        <el-input v-model="queryParams.phone" placeholder="请输入手机号" clearable />
      </el-form-item>
      <el-form-item label="状态" prop="status">
        <el-select v-model="queryParams.status" placeholder="请选择状态" clearable>
          <el-option label="启用" value="1" />
          <el-option label="禁用" value="0" />
        </el-select>
      </el-form-item>
    </template>

    <template #tableColumns>
      <el-table-column prop="id" label="ID" width="80" />
      <el-table-column prop="name" label="用户名" />
      <el-table-column prop="phone" label="手机号" />
      <el-table-column prop="email" label="邮箱" />
      <el-table-column prop="deptName" label="部门" />
      <el-table-column prop="status" label="状态">
        <template #default="{ row }">
          <el-tag :type="row.status === '1' ? 'success' : 'danger'">
            {{ row.status === '1' ? '启用' : '禁用' }}
          </el-tag>
        </template>
      </el-table-column>
    </template>
  </DataSelectorDialog>
</template>

<script setup="UserSelector">
import { ref } from 'vue'
import DataSelectorDialog from '@/components/DataSelectorDialog/index.vue'

const props = defineProps({
  visible: {
    type: Boolean,
    default: false
  },
  multiple: {
    type: Boolean,
    default: false
  },
  // 可以添加更多用户选择特有的props
  deptId: {
    type: [String, Number],
    default: ''
  }
})

const emit = defineEmits(['update:visible', 'confirm'])

const rowKey = 'id'
const queryParams = ref({
  pageNum: 1,
  pageSize: 10,
  name: '',
  phone: '',
  status: '',
  deptId: props.deptId // 使用传入的部门ID
})

// 加载用户数据 - 这里应该是调用API
const loadUserData = async (params) => {
  console.log('加载用户数据，参数:', params)
  // 实际项目中这里调用API接口
  // const res = await getUserList(params)

  // 模拟数据
  await new Promise((resolve) => setTimeout(resolve, 500))
  const mockData = [
    { id: 1, name: '张三', phone: '13800138001', email: 'zhangsan@example.com', deptName: '研发部', status: '1' },
    { id: 2, name: '李四', phone: '13800138002', email: 'lisi@example.com', deptName: '产品部', status: '1' },
    { id: 3, name: '王五', phone: '13800138003', email: 'wangwu@example.com', deptName: '市场部', status: '0' },
    { id: 4, name: '赵六', phone: '13800138004', email: 'zhaoliu@example.com', deptName: '研发部', status: '1' },
    { id: 5, name: '钱七', phone: '13800138005', email: 'qianqi@example.com', deptName: '人事部', status: '0' }
  ]

  // 模拟筛选
  let filteredData = [...mockData]
  if (params.name) {
    filteredData = filteredData.filter((item) => item.name.includes(params.name))
  }
  if (params.phone) {
    filteredData = filteredData.filter((item) => item.phone.includes(params.phone))
  }
  if (params.status) {
    filteredData = filteredData.filter((item) => item.status === params.status)
  }
  if (params.deptId) {
    filteredData = filteredData.filter((item) => item.deptName === (params.deptId === '1' ? '研发部' : '其他部门')) // 简单模拟
  }

  // 模拟分页
  const start = (params.pageNum - 1) * params.pageSize
  const end = start + params.pageSize
  const pageData = filteredData.slice(start, end)

  return {
    rows: pageData,
    total: filteredData.length
  }
}
</script>
