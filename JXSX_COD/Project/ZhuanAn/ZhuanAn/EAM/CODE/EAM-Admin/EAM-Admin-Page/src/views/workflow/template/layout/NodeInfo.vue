<!--
 * @Descripttion: (流程节点信息/审批人)
 * @Author: (admin)
 * @Date: (2024-06-13)
-->
<template>
  <div style="width: 100%">
    <el-table :data="dataList" ref="table" stripe header-cell-class-name="el-table-header-cell" highlight-current-row>
      <el-table-column prop="nodeId" label="节点ID" width="90" align="center" />
      <el-table-column prop="nodeName" label="节点名称" min-width="160" align="center" />
      <el-table-column prop="approverType" label="审批人类型" width="90" align="center">
        <template #default="scope">
          <dict-tag :options="options.process_approver_type" :value="scope.row.approverType" />
        </template>
      </el-table-column>
      <el-table-column prop="approverDesc" label="审批人描述" min-width="160" align="center" />
    </el-table>
  </div>
</template>

<script setup name="nodeIinfo">
import { getProcessNodeApprover } from '@/api/workflow/processDefine.js'
const { proxy } = getCurrentInstance()
const processInstance = inject('processInstance')
const dataList = ref([])

//字典数据初始化
const dictParams = [{ dictType: 'process_approver_type' }]
proxy.getDicts(dictParams).then((response) => {
  response.data.forEach((element) => {
    state.options[element.dictType] = element.list
  })
})

//数据仓库
const state = reactive({
  //数据字典
  options: {
    // 流程审批人类型
    process_approver_type: []
  }
})
const { options } = toRefs(state)

watch(processInstance, (newObj) => {
  getProcessNodeInfo()
})

function getProcessNodeInfo() {
  if (processInstance.value.processId) {
    getProcessNodeApprover(processInstance.value.processId).then((res) => {
      dataList.value = res.data
    })
  }
}

getProcessNodeInfo()
</script>
