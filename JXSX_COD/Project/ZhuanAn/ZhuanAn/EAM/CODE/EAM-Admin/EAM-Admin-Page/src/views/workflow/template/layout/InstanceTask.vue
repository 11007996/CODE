<!--
 * @Descripttion: (流程实例任务/WF_Instance_Task)
 * @Author: (admin)
 * @Date: (2024-06-13)
-->
<template>
  <div style="width: 100%">
    <el-table :data="dataList" ref="table" stripe header-cell-class-name="table-header-cell" highlight-current-row>
      <el-table-column prop="taskId" label="任务ID" align="left" v-if="columns.showColumn('taskId')" />
      <el-table-column prop="processInstanceId" label="流程编号" align="left" v-if="columns.showColumn('processInstanceId')" />
      <el-table-column prop="nodeId" label="节点ID" align="left" v-if="columns.showColumn('nodeId')" />
      <el-table-column prop="nodeName" label="节点名称" min-width="160" align="left" v-if="columns.showColumn('nodeName')" />
      <el-table-column prop="assigneeId" label="操作人ID" align="left" v-if="columns.showColumn('assigneeId')" />
      <el-table-column prop="assigneeName" label="操作人" width="90" align="left" v-if="columns.showColumn('assigneeName')" />
      <el-table-column prop="status" label="状态" width="90" align="left" v-if="columns.showColumn('status')">
        <template #default="scope">
          <dict-tag :options="options.process_task_status" :value="scope.row.status" />
        </template>
      </el-table-column>
      <el-table-column
        prop="createTime"
        label="接收时间"
        width="160"
        align="left"
        :show-overflow-tooltip="true"
        v-if="columns.showColumn('createTime')" />
      <el-table-column
        prop="startTime"
        label="处理时间"
        width="160"
        align="left"
        :show-overflow-tooltip="true"
        v-if="columns.showColumn('startTime')" />
      <el-table-column
        prop="finishTime"
        label="完成时间"
        width="160"
        align="left"
        :show-overflow-tooltip="true"
        v-if="columns.showColumn('finishTime')" />
      <el-table-column
        prop="deadline"
        label="超时时间"
        width="160"
        align="left"
        :show-overflow-tooltip="true"
        v-if="columns.showColumn('deadline')" />
      <el-table-column
        prop="reminder"
        label="提醒时间"
        width="160"
        align="left"
        :show-overflow-tooltip="true"
        v-if="columns.showColumn('reminder')" />
    </el-table>
  </div>
</template>

<script setup name="instancetask">
const processInstance = inject('processInstance')
const { proxy } = getCurrentInstance()
//字典数据初始化
const dictParams = [{ dictType: 'process_task_status' }]
proxy.getDicts(dictParams).then((response) => {
  response.data.forEach((element) => {
    state.options[element.dictType] = element.list
  })
})

const dataList = ref([])
if (processInstance.value.instanceTaskNav) {
  dataList.value = [...processInstance.value.instanceTaskNav]
}

const columns = ref([
  { visible: false, prop: 'taskId', label: 'TaskId' },
  { visible: false, prop: 'processInstanceId', label: 'ProcessInstanceId' },
  { visible: false, prop: 'nodeId', label: '节点Id' },
  { visible: true, prop: 'nodeName', label: '节点名称' },
  { visible: false, prop: 'assigneeId', label: '操作人Id' },
  { visible: true, prop: 'assigneeName', label: '操作人名称' },
  { visible: true, prop: 'status', label: '状态' },
  { visible: true, prop: 'createTime', label: 'CreateTime' },
  { visible: true, prop: 'startTime', label: 'StartTime' },
  { visible: true, prop: 'finishTime', label: 'FinishTime' },
  { visible: false, prop: 'deadline', label: 'Deadline' },
  { visible: false, prop: 'reminder', label: 'Reminder' }
])

//数据仓库
const state = reactive({
  //数据字典
  options: {
    // 任务状态
    process_task_status: []
  }
})
const { options } = toRefs(state)

watch(processInstance, (newObj) => {
  if (processInstance.value.instanceTaskNav) {
    dataList.value = [...processInstance.value.instanceTaskNav]
  }
})
</script>
<style>
.table-header-cell {
  text-align: left !important;
}
</style>
