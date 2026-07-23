<!--
 * @Descripttion: (呼叫面板/CALL_Panel)
 * @Author: (admin)
 * @Date: (2025-07-30)
-->
<template>
  <div>
    <el-form :model="queryParams" label-position="right" inline ref="queryRef" v-show="showSearch" @submit.prevent>
      <el-form-item label="产线" prop="lineId">
        <el-select v-model="queryParams.lineId" placeholder="请选择产线" filterable>
          <el-option
            v-for="item in useBasicStore().getLineDict"
            :key="item.dictValue"
            :label="item.dictLabel"
            :value="Number(item.dictValue)"></el-option>
        </el-select>
      </el-form-item>
    </el-form>
    <!-- 工具区域 -->
    <el-row :gutter="15" class="mb10">
      <el-col :span="1.5">
        <el-button type="primary" v-hasPermi="['call:fault:operate:call']" :disabled="!btnStatus.call" plain @click="openForm(1)"> 呼叫 </el-button>
        <el-button type="warning" v-hasPermi="['call:fault:operate:handle']" :disabled="!btnStatus.handle" plain @click="openForm(2)">
          处理签到
        </el-button>
        <el-button type="warning" v-hasPermi="['call:fault:operate:handle']" :disabled="!btnStatus.requestHelp" plain @click="openForm(3)">
          请求支援
        </el-button>
        <el-button type="danger" v-hasPermi="['call:fault:operate:help']" :disabled="!btnStatus.help" plain @click="openForm(4)">
          支援签到
        </el-button>
        <el-button type="success" v-hasPermi="['call:fault:operate:solve']" :disabled="!btnStatus.solve" plain @click="openForm(5)">
          完成
        </el-button>
        <el-button type="info" plain @click="reset"> 重置 </el-button>
      </el-col>
      <right-toolbar v-model:showSearch="showSearch" @queryTable="getList"></right-toolbar>
    </el-row>

    <el-row :gutter="15" class="mb10">
      <!-- 操作面板 -->
      <el-col :lg="15">
        <div class="call-panel">
          <el-form ref="formRef" :model="form" label-width="100px">
            <el-row>
              <el-col :sm="8">
                <el-form-item label="点位类型" prop="callPointType">
                  <el-select v-model="form.callPointType" placeholder="请选择呼叫点位类型">
                    <el-option
                      v-for="item in options.call_point_type"
                      :key="item.dictValue"
                      :label="item.dictLabel"
                      :value="item.dictValue"></el-option>
                  </el-select>
                </el-form-item>
                <el-table
                  v-show="form.callPointType == 'equipment'"
                  ref="equipmentListRef"
                  :data="equipmentList"
                  @row-click="equipmentRowClick"
                  height="380"
                  header-cell-class-name="el-table-header-cell"
                  highlight-current-row>
                  <el-table-column prop="equipment" label="设备" min-width="160">
                    <template #default="scope">
                      {{ scope.row.equipmentNo ? scope.row.equipmentType + ' * ' + scope.row.equipmentNo : scope.row.equipmentType }}
                    </template>
                  </el-table-column>
                </el-table>
                <el-table
                  v-show="form.callPointType == 'station'"
                  ref="stationListRef"
                  :data="stationList"
                  @row-click="stationRowClick"
                  height="380"
                  header-cell-class-name="el-table-header-cell"
                  highlight-current-row>
                  <el-table-column prop="stationName" label="工站" min-width="160" align="center" />
                </el-table>
              </el-col>
              <el-col :sm="16">
                <!-- 表单数据 -->
                <el-row>
                  <el-col :sm="12">
                    <el-form-item label="产线" prop="lineId">
                      <el-select v-model="form.lineId" disabled placeholder="请选择产线">
                        <el-option
                          v-for="item in useBasicStore().getLineDict"
                          :key="item.dictValue"
                          :label="item.dictLabel"
                          :value="Number(item.dictValue)"></el-option>
                      </el-select>
                    </el-form-item>
                  </el-col>
                  <el-col :sm="12">
                    <el-form-item label="广播区域">
                      <el-input :value="areaLineInfo?.areaName" disabled />
                    </el-form-item>
                  </el-col>
                  <el-col :sm="12">
                    <el-form-item label="呼叫原因" prop="callReason">
                      <el-select v-model="form.callReason" placeholder="请选择呼叫原因">
                        <el-option
                          v-for="item in options.call_reason"
                          :key="item.dictValue"
                          :label="item.dictLabel"
                          :value="item.dictValue"></el-option>
                      </el-select>
                    </el-form-item>
                  </el-col>
                  <el-col :sm="12">
                    <el-form-item label="呼叫目标" prop="callTargetType">
                      <el-select v-model="form.callTargetType" clearable placeholder="请选择呼叫目标">
                        <el-option
                          v-for="item in options.call_target_type"
                          :key="item.dictValue"
                          :label="item.dictLabel"
                          :value="item.dictValue"></el-option>
                      </el-select>
                    </el-form-item>
                  </el-col>
                </el-row>
                <!-- 描述数据 -->
                <el-descriptions title="呼叫信息" :column="2" border label-width="70" style="padding: 15px">
                  <el-descriptions-item label="呼叫时间" width="100" label-align="right" align="center">{{ form.createTime }}</el-descriptions-item>
                  <el-descriptions-item label="到场时间" width="100" label-align="right" align="center">{{ form.handleTime }}</el-descriptions-item>
                  <el-descriptions-item label="到场时长" label-align="right" align="center">
                    {{ form.comeMinute }}
                  </el-descriptions-item>
                  <el-descriptions-item label="处理时长" label-align="right" align="center"> {{ form.handleMinute }} </el-descriptions-item>
                  <el-descriptions-item label="支援时长" label-align="right" align="center">{{ form.helpMinute }}</el-descriptions-item>
                  <el-descriptions-item label="总计时长" label-align="right" align="center">{{ form.totalMinute }} </el-descriptions-item>
                  <el-descriptions-item label="状态" label-align="right" align="center">
                    <dict-tag :options="options.call_fault_status" :value="form.faultStatus" />
                  </el-descriptions-item>
                  <el-descriptions-item label="限定时长" label-align="right" align="center">
                    {{ form.remainMinute < 0 ? '[超时]' + form.remainMinute : form.remainMinute }}
                  </el-descriptions-item>
                  <el-descriptions-item label="处理人" label-align="right" align="center">{{ form.handlerName }}</el-descriptions-item>
                  <el-descriptions-item label="支援人" label-align="right" align="center">{{ form.helperName }}</el-descriptions-item>
                  <el-descriptions-item label="备注" label-align="right" align="left" :span="2">{{ form.remark }}</el-descriptions-item>
                </el-descriptions>
              </el-col>
            </el-row>
          </el-form>
        </div>
      </el-col>

      <!-- 故障记录 -->
      <el-col :lg="9">
        <el-table
          :data="dataList"
          @row-click="callRowClick"
          v-loading="loading"
          ref="callBaseRef"
          border
          header-cell-class-name="el-table-header-cell"
          highlight-current-row>
          <el-table-column prop="equipment" label="设备 / 工站" min-width="160" align="center">
            <template #default="scope">
              {{ scope.row.equipmentNo ? scope.row.equipmentType + ' * ' + scope.row.equipmentNo : scope.row.equipmentType }}
              {{ scope.row.stationName }}
            </template>
          </el-table-column>
          <el-table-column prop="faultStatus" label="状态" width="80" align="center">
            <template #default="scope">
              <dict-tag :options="options.call_fault_status" :value="scope.row.faultStatus" />
            </template>
          </el-table-column>
          <el-table-column prop="handlerName" label="处理人" width="80" align="center" />
          <el-table-column prop="helperName" label="支援人" width="80" align="center" />
        </el-table>
      </el-col>
    </el-row>

    <!-- 呼叫操作对话框 -->
    <el-dialog :title="title" :lock-scroll="false" v-model="open" :width="dialogWidth">
      <!-- 呼叫表单 -->
      <el-form v-if="opertype == 1" ref="formRef" :model="form" :rules="rules" label-width="100px">
        <el-row :gutter="20">
          <el-col :lg="12">
            <el-form-item label="线别" prop="lineId">
              <el-select v-model="form.lineId" placeholder="">
                <el-option
                  v-for="item in useBasicStore().getLineDict"
                  :key="item.dictValue"
                  :label="item.dictLabel"
                  :value="parseInt(item.dictValue)"></el-option>
              </el-select>
            </el-form-item>
          </el-col>
          <el-col :lg="12">
            <el-form-item label="呼叫原因" prop="callReason">
              <el-select v-model="form.callReason" placeholder="">
                <el-option v-for="item in options.call_reason" :key="item.dictValue" :label="item.dictLabel" :value="item.dictValue"></el-option>
              </el-select>
            </el-form-item>
          </el-col>
          <el-col :lg="12">
            <el-form-item label="呼叫目标" prop="callTargetType">
              <el-select v-model="form.callTargetType" clearable placeholder="请选择呼叫目标">
                <el-option
                  v-for="item in options.call_target_type"
                  :key="item.dictValue"
                  :label="item.dictLabel"
                  :value="item.dictValue"></el-option>
              </el-select>
            </el-form-item>
          </el-col>
          <el-col :lg="12">
            <el-form-item label="点位类型" prop="callPointType">
              <el-select v-model="form.callPointType" placeholder="请选择呼叫点位类型">
                <el-option
                  v-for="item in options.call_point_type"
                  :key="item.dictValue"
                  :label="item.dictLabel"
                  :value="item.dictValue"></el-option>
              </el-select>
            </el-form-item>
          </el-col>

          <el-col :lg="12" v-if="form.callPointType == 'equipment'">
            <el-form-item label="设备类型" prop="equipmentType">
              <el-select v-model="form.equipmentType" clearable placeholder="">
                <el-option
                  v-for="item in equipmentList"
                  :key="item.equipmentType"
                  :label="item.equipmentType"
                  :value="item.equipmentType"></el-option>
              </el-select>
            </el-form-item>
          </el-col>
          <el-col :lg="12" v-if="form.callPointType == 'equipment'">
            <el-form-item label="设备编号" prop="equipmentNo">
              <el-input v-model="form.equipmentNo" style="width: 200px" />
            </el-form-item>
          </el-col>

          <el-col :lg="12" v-if="form.callPointType == 'station'">
            <el-form-item label="工站" prop="stationId">
              <el-select v-model="form.stationId" placeholder="" clearable>
                <el-option v-for="item in stationList" :key="item.stationId" :label="item.stationName" :value="item.stationId"></el-option>
              </el-select>
            </el-form-item>
          </el-col>

          <el-col :lg="24">
            <el-form-item label="备注" prop="remark">
              <el-input v-model="form.remark" type="textarea" />
            </el-form-item>
          </el-col>
        </el-row>
      </el-form>

      <!-- 处理签到 -->
      <el-form v-if="opertype == 2" ref="formRef" :model="form" :rules="rules" label-width="100px">
        <el-form-item label="处理人" prop="handlerNo">
          <el-select v-model="form.handlerNo" placeholder="请输入工号或姓名" clearable filterable remote :remote-method="handleQueryEmployee">
            <el-option
              v-for="item in options.emp_options"
              :key="item.dictValue"
              :label="item.dictValue + ' - ' + item.dictLabel"
              :value="item.dictValue"></el-option>
          </el-select>
        </el-form-item>
      </el-form>

      <!-- 请求支援 -->
      <el-form v-if="opertype == 3" disabled ref="formRef" :model="form" :rules="rules" label-width="100px">
        <el-form-item label="当前处理人" prop="handlerNo">
          <el-select v-model="form.handlerNo" placeholder="请输入工号或姓名" clearable filterable remote :remote-method="handleQueryEmployee">
            <el-option
              v-for="item in options.emp_options"
              :key="item.dictValue"
              :label="item.dictValue + ' - ' + item.dictLabel"
              :value="item.dictValue"></el-option>
          </el-select>
        </el-form-item>
        确定需要呼叫支援吗？
      </el-form>

      <!-- 支援签到 -->
      <el-form v-if="opertype == 4" ref="formRef" :model="form" :rules="rules" label-width="100px">
        <el-form-item label="支援人" prop="helperNo">
          <el-select v-model="form.helperNo" placeholder="请输入工号或姓名" clearable filterable remote :remote-method="handleQueryEmployee">
            <el-option
              v-for="item in options.emp_options"
              :key="item.dictValue"
              :label="item.dictValue + ' - ' + item.dictLabel"
              :value="item.dictValue"></el-option>
          </el-select>
        </el-form-item>
      </el-form>

      <!-- 完成 -->
      <el-form v-if="opertype == 5" ref="formRef" :model="form" :rules="rules" label-width="100px">
        <el-row :gutter="20">
          <el-col :lg="12" v-if="form.callPointType == 'equipment'">
            <el-form-item label="设备">
              <el-input :value="form.equipmentNo ? form.equipmentType + ' * ' + form.equipmentNo : form.equipmentType" disabled />
            </el-form-item>
          </el-col>
          <el-col :lg="12" v-if="form.callPointType == 'station'">
            <el-form-item label="工站">
              <el-select v-model="form.stationId" placeholder="请选择工站" clearable>
                <el-option
                  v-for="item in stationList"
                  :key="item.stationId"
                  :label="'[' + item.stationCode + ']' + item.stationName"
                  :value="item.stationId"></el-option>
              </el-select>
            </el-form-item>
          </el-col>
          <el-col :lg="12">
            <el-form-item label="解决人" prop="solverNo">
              <el-select v-model="form.solverNo" placeholder="请输入工号或姓名" clearable filterable remote :remote-method="handleQueryEmployee">
                <el-option
                  v-for="item in options.emp_options"
                  :key="item.dictValue"
                  :label="item.dictValue + ' - ' + item.dictLabel"
                  :value="item.dictValue"></el-option>
              </el-select>
            </el-form-item>
          </el-col>
          <el-col :lg="12">
            <el-form-item label="品质" prop="qcNo">
              <el-select v-model="form.qcNo" placeholder="请输入工号或姓名" clearable filterable remote :remote-method="handleQueryEmployee">
                <el-option
                  v-for="item in options.emp_options"
                  :key="item.dictValue"
                  :label="item.dictValue + ' - ' + item.dictLabel"
                  :value="item.dictValue"></el-option>
              </el-select>
            </el-form-item>
          </el-col>
          <el-col :lg="12">
            <el-form-item label="制品跟踪数" prop="prodCount">
              <el-input-number v-model.number="form.prodCount" :controls="true" controls-position="right" />
            </el-form-item>
          </el-col>
          <el-col :lg="12">
            <el-form-item label="良品数量" prop="passCount">
              <el-input-number v-model.number="form.passCount" :controls="true" controls-position="right" />
            </el-form-item>
          </el-col>
          <el-col :lg="12">
            <el-form-item label="不良数量" prop="ngCount">
              <el-input-number v-model.number="form.ngCount" :controls="true" controls-position="right" />
            </el-form-item>
          </el-col>
          <el-col :lg="12" v-if="form.callPointType == 'equipment'">
            <el-form-item label="故障类型" prop="faultType">
              <el-select v-model="form.faultType" filterable @change="faultTypeChange" placeholder="请选择故障类型">
                <el-option
                  v-for="item in options.fault_type_options"
                  :key="item.faultConfigId"
                  :label="item.equipmentType"
                  :value="item.equipmentType"></el-option>
              </el-select>
            </el-form-item>
          </el-col>
          <el-col :lg="12">
            <el-form-item label="呼叫原因" prop="callReason">
              <el-select v-model="form.callReason" disabled placeholder="请选择呼叫原因">
                <el-option v-for="item in options.call_reason" :key="item.dictValue" :label="item.dictLabel" :value="item.dictValue"></el-option>
              </el-select>
            </el-form-item>
          </el-col>
          <el-col :lg="24">
            <el-form-item label="故障内容" prop="faultContent">
              <el-select
                v-model="form.faultContent"
                filterable
                allow-create
                clearable
                placeholder="请输入故障内容"
                @change="handleFaultContentChange"
                class="fullWidth">
                <el-option
                  v-for="item in options.fault_solution_options"
                  :key="item.faultContent"
                  :label="item.faultContent"
                  :value="item.faultContent"></el-option>
              </el-select>
            </el-form-item>
          </el-col>
          <el-col :lg="24">
            <el-form-item label="解决方案" prop="solutionContent">
              <el-select v-model="form.solutionContent" filterable allow-create clearable placeholder="请输入故障内容" class="fullWidth">
                <el-option
                  v-for="item in options.fault_solution_options"
                  :key="item.solutionContent"
                  :label="item.solutionContent"
                  :value="item.solutionContent"></el-option>
              </el-select>
            </el-form-item>
          </el-col>
        </el-row>
      </el-form>

      <!-- 完成(指定人员) -->
      <!-- <el-form v-if="opertype == 5 && form.targetHandler" ref="formRef" :model="form" :rules="rules" label-width="100px">
        <el-row :gutter="20">
          <el-col :lg="12">
            <el-form-item label="指定人" prop="targetHandler">
              <el-select v-model="form.targetHandler" disabled>
                <el-option
                  v-for="item in options.emp_options"
                  :key="item.dictValue"
                  :label="item.dictValue + ' - ' + item.dictLabel"
                  :value="item.dictValue"></el-option>
              </el-select>
            </el-form-item>
          </el-col>
          <el-col :lg="12">
            <el-form-item label="解决人" prop="solverNo">
              <el-select v-model="form.solverNo" placeholder="请输入工号或姓名" clearable filterable remote :remote-method="handleQueryEmployee">
                <el-option
                  v-for="item in options.emp_options"
                  :key="item.dictValue"
                  :label="item.dictValue + ' - ' + item.dictLabel"
                  :value="item.dictValue"></el-option>
              </el-select>
            </el-form-item>
          </el-col>
          <el-col :lg="24">
            <el-form-item label="故障内容" prop="faultContent">
              <el-input v-model="form.faultContent" type="textarea" placeholder="请输入异常描述" />
            </el-form-item>
          </el-col>
          <el-col :lg="24">
            <el-form-item label="解决方案" prop="solutionContent">
              <el-input v-model="form.solutionContent" type="textarea" placeholder="请输入解决方案" />
            </el-form-item>
          </el-col>
        </el-row>
      </el-form> -->

      <template #footer>
        <el-button text @click="cancel">{{ $t('btn.cancel') }}</el-button>
        <el-button type="primary" @click="submitForm">{{ $t('btn.submit') }}</el-button>
      </template>
    </el-dialog>
  </div>
</template>

<script setup name="callpanel">
import {
  getUnsolvedCallFaultBase,
  getCallSummaryByLine,
  getCallFaultBase,
  addCallFaultBase,
  handleCallFaultBase,
  requestHelpCallFaultBase,
  helpCallFaultBase,
  solveCallFaultBase
} from '@/api/call/callFaultBase.js'
import useBasicStore from '@/store/modules/basic.js'
import { dictEmployee } from '@/api/basic/employee.js'
import { listCallConfigFault, getCallConfigFault } from '@/api/call/callConfigFault.js'
const { proxy } = getCurrentInstance()
const loading = ref(false)
const showSearch = ref(false)
const queryParams = reactive({
  // pageNum: 1,
  // pageSize: 100,
  // sort: 'createTime',
  // sortType: 'desc',
  lineId: 0
})

const dataList = ref([])
const equipmentList = ref([])
const stationList = ref([])
const areaLineInfo = ref({})
const queryRef = ref()
const btnStatus = ref({
  call: true,
  handle: false,
  requestHelp: false,
  help: false,
  solve: false
})

var dictParams = [{ dictType: 'call_reason' }, { dictType: 'call_fault_status' }, { dictType: 'call_target_type' }, { dictType: 'call_point_type' }]

proxy.getDicts(dictParams).then((response) => {
  response.data.forEach((element) => {
    state.options[element.dictType] = element.list
  })
})

//获取未结案呼叫列表
function getList() {
  if (!queryParams.lineId || queryParams.lineId === 0) return
  loading.value = true
  getUnsolvedCallFaultBase(queryParams.lineId).then((res) => {
    const { code, data } = res
    if (code == 200) {
      dataList.value = data
      loading.value = false
    }
  })
}

//获取呼叫信息详情
function getCallFaultBaseInfo(callId) {
  if (!callId) return //没有选择呼叫记录或打开了表单窗口，则不更新当前操作的呼叫信息
  getCallFaultBase(callId).then((res) => {
    const { code, data } = res
    if (code == 200) {
      form.value = {
        ...data
      }

      //设备名称
      if (form.value.equipmentNo) form.value.equipmentName = form.value.equipmentType + '*' + form.value.equipmentNo
      else form.value.equipmentName = form.value.equipmentType

      if (form.value.callPointType == 'equipment') {
        //设置选中的设备
        if (form.value.equipmentType) {
          let equipment = equipmentList.value.find((it) => it.equipmentType == form.value.equipmentType && it.equipmentNo == form.value.equipmentNo)
          if (equipment) proxy.$refs['equipmentListRef'].setCurrentRow(equipment, true)
        }
      } else if (form.value.callPointType == 'station') {
        //设置选中的工站
        if (form.value.stationId) {
          let station = stationList.value.find((it) => it.stationId == form.value.stationId)
          if (station) proxy.$refs['stationListRef'].setCurrentRow(station, true)
        }
      }

      //设置人员选项
      let emps = []
      if (data.handlerNo) emps.push({ dictValue: data.handlerNo, dictLabel: data.handlerName })
      if (data.helperNo) emps.push({ dictValue: data.helperNo, dictLabel: data.helperName })
      if (data.solverNo) emps.push({ dictValue: data.solverNo, dictLabel: data.solverName })
      if (data.qcNo) emps.push({ dictValue: data.qcNo, dictLabel: data.qcName })
      options.value.emp_options = emps

      //解决方案
      if (!form.value.faultType && form.value.equipmentType) {
        form.value.faultType = form.value.equipmentType
        faultTypeChange(form.value.faultType)
      }
    }
  })
}

function initLineInfo() {
  if (!queryParams.lineId || queryParams.lineId === 0) return
  getCallSummaryByLine(queryParams.lineId).then((res) => {
    const { code, data } = res
    if (code == 200) {
      equipmentList.value = data.lineEquipmentList
      stationList.value = data.lineStationList
      areaLineInfo.value = data.callArea
    }
  })
}

/** 设备列表 行单击事件 */
function equipmentRowClick(row) {
  form.value.equipmentType = row.equipmentType
  form.value.equipmentNo = row.equipmentNo
}

/** 工站列表 行单击事件 */
function stationRowClick(row) {
  form.value.stationId = row.stationId
}

/** 呼叫列表 行单击事件 */
function callRowClick(row) {
  getCallFaultBaseInfo(row.callId)
}

//故障类型变更事件
function faultTypeChange(value) {
  //const id = row.faultConfigId
  let faultCofnig = options.value.fault_type_options.find((it) => it.equipmentType == value)
  getCallConfigFault(faultCofnig.faultConfigId).then((res) => {
    const { code, data } = res
    if (code == 200) {
      options.value.fault_solution_options = data.callConfigFaultSolutionNav
    }
  })
}

/*************** form操作 ***************/
const formRef = ref()
const title = ref('')
// 操作类型 1、呼叫 2、处理签到 3、请求支援 4、支援签到 5、完成
const opertype = ref(0)
const open = ref(false)
const dialogWidth = ref('50%')

const state = reactive({
  form: {
    callPointType: 'equipment',
    callTargetType: 'ET',
    callReason: '机台故障'
  },
  rules: {
    callReason: [{ required: true, message: '呼叫原因不能为空', trigger: 'change' }],
    callTargetType: [{ required: true, message: '呼叫目标不能为空', trigger: 'blur' }],
    callPointType: [{ required: true, message: '呼叫点位不能为空', trigger: 'blur' }],
    lineId: [{ required: true, message: '产线不能为空', trigger: 'blur' }],
    handlerNo: [{ required: true, message: '处理人不能为空', trigger: 'blur' }],
    helperNo: [{ required: true, message: '支援人不能为空', trigger: 'blur' }],
    solverNo: [{ required: true, message: '解决人不能为空', trigger: 'blur' }],
    prodCount: [{ required: true, message: '制品跟踪数量不能为空', trigger: 'blur' }],
    passCount: [{ required: true, message: '良品数量不能为空', trigger: 'blur' }],
    ngCount: [{ required: true, message: '不良数量不能为空', trigger: 'blur' }],
    faultContent: [{ required: true, message: '故障内容不能为空', trigger: 'blur' }],
    solutionContent: [{ required: true, message: '解决方案不能为空', trigger: 'blur' }]
  },
  options: {
    //呼叫故障状态
    call_fault_status: [],
    //故障类型
    fault_type_options: [],
    //故障项目选项
    fault_solution_options: [],
    //人员选项
    emp_options: [],
    //呼叫目标选项
    call_target_type: [],
    //呼叫位置类型：
    call_point_type: []
  }
})

const { form, rules, options } = toRefs(state)

// 关闭dialog
function cancel() {
  open.value = false
}

// 重置表单
function reset() {
  form.value = {
    callId: null,
    lineId: queryParams.lineId,
    callReason: '机台故障',
    callTargetType: 'ET',
    callPointType: 'equipment',
    stationId: null,
    equipmentType: null,
    equipmentNo: null,
    remark: null,
    maxHandleTimes: null,
    maxHelpTimes: null,
    handleTime: null,
    callHelpTime: null,
    helpTime: null,
    callHelpWay: null,
    endTime: null,
    faultStatus: null,
    faultType: null,
    faultContent: null,
    solutionContent: null,
    prodCount: null,
    passCount: null,
    ngCount: null,
    handlerNo: null,
    helperNo: null,
    solverNo: null,
    qcNo: null
  }
  proxy.$refs['equipmentListRef'].setCurrentRow()
  proxy.$refs['stationListRef'].setCurrentRow()
  proxy.resetForm('formRef')
}

//打开表单弹窗
function openForm(type) {
  open.value = true
  opertype.value = type
  let titleList = ['', '呼叫确认', '处理签到', '请求支援', '支援签到', '确认完成']
  let widthList = ['50%', '50%', '300', '300', '300', '50%']
  title.value = titleList[opertype.value]
  dialogWidth.value = widthList[opertype.value]

  if (opertype.value === 5 && !form.value.solverNo) {
    //结案操作
    form.value.solverNo = form.value.handlerNo
  }
}

// 添加&修改 表单提交
function submitForm() {
  proxy.$refs['formRef'].validate((valid) => {
    if (valid) {
      //呼叫
      if (opertype.value === 1) {
        let param = {
          lineId: form.value.lineId,
          callReason: form.value.callReason,
          callTargetType: form.value.callTargetType,
          callPointType: form.value.callPointType,
          equipmentType: form.value.equipmentType,
          equipmentNo: form.value.equipmentNo,
          stationId: form.value.stationId,
          remark: form.value.remark
        }
        addCallFaultBase(param).then((res) => {
          if (res.code == 200) {
            proxy.$modal.msgSuccess('呼叫成功')
            open.value = false
            getList()
            getCallFaultBaseInfo(res.data.callId)
          }
        })
      }

      //处理签到
      if (opertype.value == 2) {
        let param = {
          callId: form.value.callId,
          operatorNo: form.value.handlerNo
        }
        handleCallFaultBase(param).then((res) => {
          if (res.code == 200) {
            proxy.$modal.msgSuccess('签到成功')
            open.value = false
            getList()
            getCallFaultBaseInfo(form.value.callId)
          }
        })
      }

      //请求支援
      if (opertype.value == 3) {
        let param = {
          callId: form.value.callId,
          operatorNo: form.value.handlerNo,
          callHelpWay: '1' //呼叫支援方式，0:被动，1主动
        }
        requestHelpCallFaultBase(param).then((res) => {
          if (res.code == 200) {
            proxy.$modal.msgSuccess('请求支援成功')
            open.value = false
            getList()
            getCallFaultBaseInfo(form.value.callId)
          }
        })
      }

      //支援签到
      if (opertype.value == 4) {
        let param = {
          callId: form.value.callId,
          operatorNo: form.value.helperNo
        }
        helpCallFaultBase(param).then((res) => {
          if (res.code == 200) {
            proxy.$modal.msgSuccess('签到成功')
            open.value = false
            getList()
            getCallFaultBaseInfo(form.value.callId)
          }
        })
      }

      //完成
      if (opertype.value == 5) {
        let param = {
          callId: form.value.callId,
          operatorNo: form.value.solverNo,
          faultType: form.value.faultType,
          faultContent: form.value.faultContent,
          solutionContent: form.value.solutionContent,
          prodCount: form.value.prodCount,
          passCount: form.value.passCount,
          ngCount: form.value.ngCount,
          qcNo: form.value.qcNo
        }
        solveCallFaultBase(param).then((res) => {
          if (res.code == 200) {
            proxy.$modal.msgSuccess('完成成功')
            open.value = false
            getList()
            reset()
          }
        })
      }
    }
  })
}

//处理故障内容变化事件
function handleFaultContentChange(val) {
  form.value.solutionContent = null
  if (options.value.fault_solution_options && options.value.fault_solution_options.length > 0) {
    options.value.fault_solution_options.forEach((it) => {
      if (it.faultContent == val) form.value.solutionContent = it.solutionContent
    })
  }
}

/***************************** 其他 ***************************** */
//产线变化事件
watch(
  () => [queryParams.lineId],
  (newValue, oldValue) => {
    localStorage.setItem('call-line', Number(queryParams.lineId))
    form.value.lineId = queryParams.lineId
    initLineInfo()
    getList()
  }
)

//监听故障状态，设置按钮的状态
watch(
  () => [form.value.faultStatus],
  (newValue, oldValue) => {
    switch (form.value.faultStatus) {
      case 'Pending':
        btnStatus.value = { call: false, handle: true, requestHelp: false, help: false, solve: false }
        break
      case 'Handling':
        btnStatus.value = { call: false, handle: false, requestHelp: true, help: false, solve: true }
        break
      case 'WaitHelp':
        btnStatus.value = { call: false, handle: false, requestHelp: false, help: true, solve: true }
        break
      case 'Helping':
        btnStatus.value = { call: false, handle: false, requestHelp: false, help: false, solve: true }
        break
      case 'Completed':
      case 'Stop':
        btnStatus.value = { call: false, handle: false, requestHelp: false, help: false, solve: false }
        break
      default:
        btnStatus.value = { call: true, handle: false, requestHelp: false, help: false, solve: false }
    }
  }
)

const timer = ref(0)
onMounted(() => {
  //组件挂载时的生命周期执行的方法
  timer.value = window.setInterval(function logname() {
    // 其他定时执行的方法
    getList()
    if (open.value) return //表单窗口打开时，不自动更新表单信息，防止输入内容被覆盖
    getCallFaultBaseInfo(form.value.callId)
  }, 60 * 1000)
})

onBeforeUnmount(() => {
  clearInterval(timer.value)
})

//员工查询
function handleQueryEmployee(keyword) {
  if (keyword) {
    const query = {
      pageNum: 1,
      pageSize: 10,
      keyword: keyword
    }
    setTimeout(() => {
      dictEmployee(query).then((res) => {
        options.value.emp_options = res.data.result
      })
    }, 200)
  }
}

/** 初始化产线 */
function initLine() {
  queryParams.lineId = Number(localStorage.getItem('call-line'))
}

/**初始化故障类型选项配置 */
function initFaultTypeOptions() {
  let params = {
    pageNum: 1,
    pageSize: 1000,
    sort: '',
    sortType: 'asc',
    equipmentType: null
  }
  listCallConfigFault(params).then((res) => {
    if (res.code == 200) {
      options.value.fault_type_options = res.data.result
    }
  })
}

initLine()
initFaultTypeOptions()
</script>

<style lang="scss" scoped>
.call-panel {
  width: 100%;
  height: 460px;
  background-color: var(--el-color-success-light-9);
  border: solid 1px var(--el-color-success-light-3);
  border-radius: 10px;
  padding: 5px;
  overflow: auto;
}
</style>
