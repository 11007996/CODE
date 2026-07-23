<template>
  <div class="layout">
    <div class="main-container">
      <!-- 头部信息 -->
      <div :class="{ 'fixed-header': fixedHeader }">
        <div>
          流程：{{ processInstance.processId }}-{{ processInstance.processName }}-{{
            nodeInfo ? nodeInfo.nodeName : processInstance.currentNodeName
          }}
          {{ processInstance.processInstanceId }}
          <dict-tag :options="options.process_status" :value="processInstance.status" />
        </div>
        <el-menu :default-active="activeIndex" mode="horizontal" @select="handleSelect">
          <el-menu-item index="processForm">流程表单</el-menu-item>
          <el-menu-item index="processStatus">流程状态</el-menu-item>
          <el-menu-item index="nodeInfo">节点信息</el-menu-item>
        </el-menu>
      </div>

      <!-- 流程表单菜单面板 -->
      <div v-if="activeIndex == 'processForm'" class="processFormTab">
        <!-- 基础表单数据 -->
        <div>
          <el-row type="flex" justify="center" :gutter="20">
            <el-col :sm="8"> </el-col>
            <el-col :sm="8">
              <h3 style="text-align: center">{{ processInstance.processName }}</h3>
            </el-col>
            <el-col :sm="8"> 实例编号：{{ processInstance.processInstanceId }} </el-col>
          </el-row>

          <h4>基本信息</h4>
          <el-form ref="formRef" :model="processInstance" label-width="100px">
            <el-row :gutter="20">
              <el-col :sm="12">
                <el-form-item label="标题" prop="title">
                  <el-input v-model="processInstance.title" placeholder="请输入流程标题" :disabled="processInstance.processInstanceId != null" />
                </el-form-item>
              </el-col>

              <el-col :sm="6">
                <el-form-item label="发起人" prop="InitiatorName">
                  <el-input v-model="processInstance.initiatorName" disabled />
                </el-form-item>
              </el-col>

              <el-col :sm="6">
                <el-form-item label="部门" prop="deptName">
                  <el-input v-model="processInstance.deptName" disabled />
                </el-form-item>
              </el-col>
            </el-row>
          </el-form>

          <!-- 下个节点审批类型为【表单】的人员选择 -->
          <el-form :model="form" :rules="rules" :validate-on-rule-change="false" :inline="true" label-width="100px">
            <el-row :gutter="20">
              <template v-for="formItem in fieldControls">
                <el-col v-if="formItem.fieldName == nextNodeFormApprover">
                  <el-form-item :label="formItem.fieldDesc" :prop="formItem.fieldName" v-if="!fieldRules[formItem.fieldName]?.hidden">
                    <el-select
                      v-model="form[formItem.fieldName]"
                      placeholder="请选择人员"
                      clearable
                      filterable
                      remote
                      :remote-method="handleQueryEmployee"
                      :disabled="!fieldRules[formItem.fieldName]?.editable">
                      <el-option
                        v-for="item in options.emp_options"
                        :key="item.dictValue"
                        :label="item.dictValue + ' - ' + item.dictLabel"
                        :value="item.dictValue"></el-option>
                    </el-select>
                  </el-form-item>
                </el-col>
              </template>
            </el-row>
          </el-form>
        </div>

        <!-- 业务表单数据 -->
        <h4>业务信息</h4>
        <!-- 非活跃的组件将会被缓存！ -->
        <component :is="BusinessForms[processInstance.processTemplate]" ref="businessFormRef"></component>

        <!-- 审批意见 -->
        <div style="margin-top: 30px">
          <el-form label-width="100px">
            <el-row>
              <el-col :lg="24">
                <el-form-item label="审批意见" prop="description">
                  <el-input type="textarea" v-model="opinion"></el-input>
                </el-form-item>
              </el-col>
            </el-row>
          </el-form>
        </div>

        <!-- 审批记录 -->
        <h4>审批流转意见</h4>
        <el-divider></el-divider>
        <div v-if="processInstance.instanceApprovalNav">
          <el-row type="flex" justify="center" v-for="item in processInstance.instanceApprovalNav" class="approval-item">
            <el-col :sm="4" class="enabled">
              {{ item.approverName }}
            </el-col>
            <el-col :sm="16">
              {{ item.opinion }}
            </el-col>
            <el-col :sm="4" class="disabled">
              {{ item.actionTime }}
            </el-col>
            <el-col :sm="4" class="disabled">
              {{ item.deptName }}
            </el-col>
            <el-col :sm="16" class="disabled"> 接收人:{{ item.receiver }} </el-col>
            <el-col :sm="4" class="enabled">
              <template #default="scope">
                [{{ item.nodeName }}/
                <dict-tag :options="options.process_node_action" :value="item.actionType" />
                ]
              </template>
            </el-col>
          </el-row>
        </div>

        <!-- 用户可执行操作 -->
        <el-row v-if="allowedActions" type="flex" justify="center" class="action-div">
          <!-- 可操作按钮 -->
          <template v-for="item in options.process_node_action">
            <el-button
              type="primary"
              @click="handleAction(item.dictValue)"
              :value="item.dictValue"
              v-if="allowedActions.indexOf(item.dictValue) >= 0"
              >{{ item.dictLabel }}
            </el-button>
          </template>
        </el-row>
      </div>

      <!-- 流程任务状态 -->
      <div v-if="activeIndex == 'processStatus'">
        <div class="instanceTaskTab">
          <InstanceTask></InstanceTask>
        </div>
      </div>

      <!-- 流程节点信息 -->
      <div v-if="activeIndex == 'nodeInfo'">
        <div class="instanceTaskTab">
          <NodeInfo></NodeInfo>
        </div>
      </div>
    </div>

    <!-- 【传阅】操作对话框 -->
    <el-dialog :title="title" :lock-scroll="false" v-model="open">
      <el-form ref="acceptorFormRef" :model="acceptorForm" :rules="rules" label-width="100px">
        <el-row :gutter="20">
          <el-col :lg="24">
            <el-form-item label="接收人" prop="acceptorId">
              <el-select
                v-model="acceptorForm.acceptorId"
                placeholder="请选择接收人"
                clearable
                filterable
                remote
                :multiple="actionType == 'Circulate'"
                :remote-method="handleQueryEmployee"
                class="fullWidth">
                <el-option
                  v-for="item in options.emp_options"
                  :key="item.dictValue"
                  :label="item.dictValue + ' - ' + item.dictLabel"
                  :value="item.dictValue"></el-option>
              </el-select>
            </el-form-item>
          </el-col>
        </el-row>
      </el-form>
      <template #footer>
        <el-button text @click="cancel">{{ $t('btn.cancel') }}</el-button>
        <el-button type="primary" @click="submitAcceptorForm">{{ $t('btn.submit') }}</el-button>
      </template>
    </el-dialog>
  </div>
</template>

<script setup name="processlayout">
import InstanceTask from './InstanceTask'
import NodeInfo from './NodeInfo'
import OnlineNoticeTicketForm from '../form/OnlineNoticeTicketForm'
import SimpleOnlineNoticeTicketForm from '../form/SimpleOnlineNoticeTicketForm'
import ConsumableReceiveTicketForm from '../form/ConsumableReceiveTicketForm'
import ProductDevDemandTicketForm from '../form/ProductDevDemandTicketForm'
import SizeMeasureTicketForm from '../form/SizeMeasureTicketForm'
import ProdMeasureTicketForm from '../form/ProdMeasureTicketForm'
import {
  getProcessInstance,
  initProcessInstance,
  addProcessInstance,
  updateProcessInstance,
  userNodeForProcess
} from '@/api/workflow/processInstance.js'
import { dictEmployee } from '@/api/basic/employee.js'

const BusinessForms = {
  OnlineNoticeTicket: OnlineNoticeTicketForm,
  SimpleOnlineNoticeTicket: SimpleOnlineNoticeTicketForm,
  ConsumableReceiveTicket: ConsumableReceiveTicketForm,
  ProductDevDemandTicket: ProductDevDemandTicketForm,
  SizeMeasureTicket: SizeMeasureTicketForm,
  ProdMeasureTicket: ProdMeasureTicketForm
}
const route = useRoute()
const { proxy } = getCurrentInstance()
const fixedHeader = ref(true) //头部固定显示
const activeIndex = ref('processForm') //活动Tab索引
const businessFormRef = ref()

//字典数据初始化
const dictParams = [{ dictType: 'process_node_type' }, { dictType: 'process_node_action' }, { dictType: 'process_status' }]
proxy.getDicts(dictParams).then((response) => {
  response.data.forEach((element) => {
    state.options[element.dictType] = element.list
  })
})

//数据仓库
const state = reactive({
  //流程信息
  processInstance: {
    processInstanceId: route.query.instanceId,
    processId: route.params.processId,
    processName: null,
    title: null,
    initiatorId: null,
    initiatorName: null,
    acceptorId: null,
    deptId: null,
    deptName: null,
    nodeId: null,
    nodeName: null,
    processTemplate: null,
    instanceApprovalNav: [], //审批流转记录
    instanceTaskNav: [] //流程状态
  },
  //当前节点信息
  nodeInfo: {},
  //用户可用操作
  allowedActions: [],
  //节点字段控制
  fieldControls: [],
  //业务表单信息
  form: {},
  //审批意见
  opinion: null,
  //业务表单(必填)
  rules: {},
  //业务表单字段规则(显示、编辑)
  fieldRules: {},
  //下个节点的表单审批人
  nextNodeFormApprover: null,
  //数据字典
  options: {
    // 节点类型 选项列表 格式 eg:{ dictLabel: '标签', dictValue: '0'}
    process_node_type: [],
    // 允许的操作 选项列表 格式 eg:{ dictLabel: '标签', dictValue: '0'}
    process_node_action: [],
    // 流程状态
    process_status: [],
    // 员工
    emp_options: []
  }
})
const { processInstance, nodeInfo, allowedActions, fieldControls, rules, fieldRules, form, opinion, nextNodeFormApprover, options } = toRefs(state)

//提供【表单】、【组件规则】给业务表单
provide('form', form)
provide('rules', rules)
provide('fieldRules', fieldRules)
provide('processInstance', processInstance)

//切换选项卡
function handleSelect(index) {
  activeIndex.value = index
}

//查询流程信息
function queryInstanceInfo() {
  if (processInstance.value.processInstanceId) {
    //根据实例ID，查询流程信息
    const id = processInstance.value.processInstanceId
    return getProcessInstance(id).then((res) => {
      processInstance.value = { ...res.data }
      //表单数据特殊处理
      if (processInstance.value.instanceFormDataNav) {
        processInstance.value.instanceFormDataNav.forEach((item) => {
          form.value[item.fieldName] = item.fieldValue
        })
      }
    })
  } else {
    //根据流程编号初始化
    const id = processInstance.value.processId
    return initProcessInstance(id).then((res) => {
      processInstance.value = { ...res.data }
    })
  }
}

//查询用户所在节点信息(节点字段控制的信息)
function queryUserNode() {
  const queryParams = {
    processId: processInstance.value.processId,
    processInstanceId: processInstance.value.processInstanceId
  }
  return userNodeForProcess(queryParams).then((res) => {
    nodeInfo.value = res.data.nodeInfo
    allowedActions.value = res.data.allowedActions
    fieldControls.value = res.data.fieldControls
    rules.value = {}
    fieldRules.value = {}
    nextNodeFormApprover.value = res.data.nextNodeFormApprover
    if (fieldControls.value) {
      fieldControls.value.forEach((item) => {
        rules.value[item.fieldName] = [{ required: item.required, message: item.fieldDesc + '不能为空', trigger: 'change' }]
        fieldRules.value[item.fieldName] = { hidden: item.hidden, editable: item.editable }
      })
    }
  })
}

//员工查询
function handleQueryEmployee(keyword) {
  if (keyword) {
    const query = {
      pageNum: 1,
      pageSize: 10,
      sort: '',
      sortType: 'asc',
      empName: keyword
    }
    setTimeout(() => {
      dictEmployee(query).then((res) => {
        state.options.emp_options = res.data.result
      })
    }, 200)
  }
}

/**
 * 用户操作的动作
 * @param {*} action
 */
async function handleAction(action) {
  //【传阅】、【转发】操作
  if (action == 'Circulate' || action == 'Forward') {
    openAcceptorForm(action)
    return
  }

  const valid = await businessFormRef.value.validFormData()
  console.log(valid)
  if (!valid) return

  //将表单数据,转为[{key:'',value:''}]的对象数组
  const formData = Object.entries(form.value).map(([key, value]) => ({ key, value }))
  //return
  const data = {
    processInstanceId: processInstance.value.processInstanceId,
    title: processInstance.value.title,
    processId: processInstance.value.processId,
    deptId: processInstance.value.dept,
    deptName: processInstance.value.deptName,
    acceptorId: processInstance.value.acceptorId,
    actionNodeId: nodeInfo.value.nodeId,
    opinion: opinion.value,
    actionType: action,
    formData: formData
  }
  if (processInstance.value.processInstanceId) {
    //更新
    updateProcessInstance(data).then((res) => {
      proxy.$modal.msgSuccess('更新成功')
      init()
    })
  } else {
    //新增
    addProcessInstance(data).then((res) => {
      proxy.$modal.msgSuccess('新增成功')
      let url = window.location.href + '?instanceId=' + res.data.processInstanceId
      window.location.href = url
    })
  }
}

//---------------[转发]、[传阅]表单--------------
const title = ref()
const open = ref(false)
const actionType = ref()
const acceptorFormRef = ref()
const acceptorForm = ref({
  acceptorId: null
})

//打开接收人表单
function openAcceptorForm(at) {
  actionType.value = at
  if (at == 'Circulate') {
    title.value = '传阅流程单'
  } else if (at == 'Forward') {
    title.value = '转发流程单'
  }
  open.value = true
}

// 关闭dialog
function cancel() {
  open.value = false
  title.value = null
  actionType.value = null
  acceptorForm.value.acceptorId = null
}

//提交【接收人表单】
function submitAcceptorForm() {
  let acceptorId = acceptorForm.value.acceptorId
  if (acceptorId instanceof Array) {
    acceptorId = acceptorId.join()
  }

  //return
  const data = {
    processInstanceId: processInstance.value.processInstanceId,
    title: processInstance.value.title,
    processId: processInstance.value.processId,
    deptId: processInstance.value.dept,
    deptName: processInstance.value.deptName,
    acceptorId: acceptorId,
    actionNodeId: nodeInfo.value.nodeId,
    opinion: opinion.value,
    actionType: actionType.value
  }
  if (processInstance.value.processInstanceId) {
    //更新
    updateProcessInstance(data).then((res) => {
      proxy.$modal.msgSuccess('更新成功')
      open.value = false
      init()
    })
  }
}

async function init() {
  await queryInstanceInfo()
  await queryUserNode()
}

init()
</script>

<style scoped lang="scss">
.layout {
  position: relative;
  height: 100%;
  width: 100%;
  display: flex;
  flex-direction: row;
  flex: 1;
}

.main-container {
  min-height: 100%;
  width: 90%;
  flex-direction: column;
  position: relative;
  margin: 10px auto;
  margin-bottom: 20px;
  padding-bottom: 50px;
}

// 固定header
.fixed-header {
  position: sticky;
  position: -webkit-sticky;
  z-index: 9;
}

.mobile .fixed-header {
  width: 100%;
}
.processFormTab {
  /* 50= navbar  50  */
  //min-height: calc(100vh - 50px);
  width: 100%;
  position: relative;
  margin: 0px auto;
  max-width: 900px;
}

.hasTagsView {
  // .app-main {
  //   min-height: calc(100vh - 84px - var(--base-footer-height));
  // }
  .el-header {
    --el-header-height: var(--el-header-height) + var(--el-tags-height) !important;
  }
}

.approval-item {
  border-bottom: 1px solid rgb(236, 235, 235);
  padding: 10px 0px;
  .disabled {
    color: rgb(199, 196, 196);
  }
  .enabled {
    color: blue;
  }
}

.action-div {
  margin-top: 20px;
  padding-bottom: 20px;
}

.instanceTaskTab {
  width: 100%;
  position: relative;
  margin: 0px auto;
  max-width: 1400px;
}
</style>
