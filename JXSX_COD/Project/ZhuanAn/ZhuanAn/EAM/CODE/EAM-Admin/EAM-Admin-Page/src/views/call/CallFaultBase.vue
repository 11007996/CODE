<!--
 * @Descripttion: (故障记录/CALL_Fault_Base)
 * @Author: (admin)
 * @Date: (2025-07-30)
-->
<template>
  <div>
    <el-form :model="queryParams" label-position="right" inline ref="queryRef" v-show="showSearch" @submit.prevent>
      <el-form-item label="区域" prop="areaId">
        <el-select v-model="queryParams.areaId" placeholder="请选择区域" filterable clearable>
          <el-option
            v-for="item in options.call_area_options"
            :key="item.dictValue"
            :label="item.dictLabel"
            :value="Number(item.dictValue)"></el-option>
        </el-select>
      </el-form-item>
      <el-form-item label="产线" prop="lineId">
        <el-select v-model="queryParams.lineId" placeholder="请选择产线" filterable clearable>
          <el-option
            v-for="item in useBasicStore().getLineDict"
            :key="item.dictValue"
            :label="item.dictLabel"
            :value="Number(item.dictValue)"></el-option>
        </el-select>
      </el-form-item>
      <el-form-item label="呼叫目标" prop="callTargetType">
        <el-select v-model="queryParams.callTargetType" placeholder="请选择呼叫目标" clearable>
          <el-option v-for="item in options.call_target_type" :key="item.dictValue" :label="item.dictLabel" :value="item.dictValue"></el-option>
        </el-select>
      </el-form-item>
      <el-form-item label="点位类型" prop="callPointType">
        <el-select v-model="queryParams.callPointType" placeholder="请选择点位类型" clearable>
          <el-option v-for="item in options.call_point_type" :key="item.dictValue" :label="item.dictLabel" :value="item.dictValue"></el-option>
        </el-select>
      </el-form-item>
      <el-form-item label="设备类型" prop="equipmentType">
        <el-select
          v-model="queryParams.equipmentType"
          placeholder="请选择设备类型"
          clearable
          filterable
          remote
          :remote-method="handleQueryEquipmentType">
          <el-option
            v-for="item in options.equipment_type_options"
            :key="item.dictValue"
            :label="item.dictLabel"
            :value="item.dictValue"></el-option>
        </el-select>
      </el-form-item>

      <el-form-item label="呼叫时间">
        <el-date-picker
          v-model="dateRangeCreateTime"
          type="datetimerange"
          start-placeholder="开始日期"
          end-placeholder="结束日期"
          value-format="YYYY-MM-DD HH:mm:ss"
          :default-time="defaultTime"
          :shortcuts="dateOptions">
        </el-date-picker>
      </el-form-item>
      <el-form-item label="故障状态" prop="faultStatus">
        <el-select v-model="queryParams.faultStatus" clearable multiple placeholder="请输入故障状态">
          <el-option v-for="item in options.call_fault_status" :key="item.dictValue" :label="item.dictLabel" :value="item.dictValue"></el-option>
        </el-select>
      </el-form-item>
      <el-form-item label="处理人" prop="handlerNo">
        <el-select v-model="queryParams.handlerNo" placeholder="请输入姓名" clearable filterable remote :remote-method="handleQueryEmployee">
          <el-option
            v-for="item in options.emp_options"
            :key="item.dictValue"
            :label="item.dictValue + ' - ' + item.dictLabel"
            :value="item.dictValue"></el-option>
        </el-select>
      </el-form-item>
      <el-form-item label="支援人" prop="helperNo">
        <el-select v-model="queryParams.helperNo" placeholder="请输入姓名" clearable filterable remote :remote-method="handleQueryEmployee">
          <el-option
            v-for="item in options.emp_options"
            :key="item.dictValue"
            :label="item.dictValue + ' - ' + item.dictLabel"
            :value="item.dictValue"></el-option>
        </el-select>
      </el-form-item>
      <el-form-item label="解决人" prop="solverNo">
        <el-select v-model="queryParams.solverNo" placeholder="请输入姓名" clearable filterable remote :remote-method="handleQueryEmployee">
          <el-option
            v-for="item in options.emp_options"
            :key="item.dictValue"
            :label="item.dictValue + ' - ' + item.dictLabel"
            :value="item.dictValue"></el-option>
        </el-select>
      </el-form-item>

      <el-form-item>
        <el-button icon="search" type="primary" @click="handleQuery">{{ $t('btn.search') }}</el-button>
        <el-button icon="refresh" @click="resetQuery">{{ $t('btn.reset') }}</el-button>
      </el-form-item>
    </el-form>
    <!-- 工具区域 -->
    <el-row :gutter="15" class="mb10">
      <el-col :span="1.5">
        <el-button type="danger" v-hasPermi="['call:fault:operate:stop']" plain @click="handleStop"> 停止 </el-button>
      </el-col>
      <el-col :span="1.5">
        <el-button type="warning" plain icon="download" @click="handleExport" v-hasPermi="['call:fault:base:export']">
          {{ $t('btn.export') }}
        </el-button>
      </el-col>
      <right-toolbar v-model:showSearch="showSearch" @queryTable="getList" :columns="columns"></right-toolbar>
    </el-row>

    <el-table
      :data="dataList"
      v-loading="loading"
      ref="table"
      border
      header-cell-class-name="el-table-header-cell"
      highlight-current-row
      @sort-change="sortChange"
      @selection-change="handleSelectionChange">
      <el-table-column type="selection" width="50" align="center" />
      <el-table-column align="center" width="90" fixed>
        <template #default="scope">
          <el-button text @click="rowClick(scope.row)">{{ $t('btn.details') }}</el-button>
        </template>
      </el-table-column>
      <el-table-column prop="callId" label="呼叫ID" width="90" align="center" v-if="columns.showColumn('callId')" />
      <el-table-column prop="areaId" label="区域ID" width="90" align="center" v-if="columns.showColumn('areaId')" />
      <el-table-column prop="areaName" label="区域" width="100" align="center" v-if="columns.showColumn('areaName')" />
      <el-table-column prop="lineId" label="线别ID" width="90" align="center" v-if="columns.showColumn('lineId')" />
      <el-table-column prop="lineName" label="线别" width="100" align="center" v-if="columns.showColumn('lineName')" />
      <el-table-column prop="callReason" label="呼叫原因" width="90" align="center" v-if="columns.showColumn('callReason')">
        <template #default="scope">
          <dict-tag :options="options.call_reason" :value="scope.row.callReason" />
        </template>
      </el-table-column>
      <el-table-column prop="callTargetType" label="呼叫目标" width="90" align="center" v-if="columns.showColumn('callTargetType')">
        <template #default="scope">
          <dict-tag :options="options.call_target_type" :value="scope.row.callTargetType" />
        </template>
      </el-table-column>
      <el-table-column prop="faultStatus" label="故障状态" width="90" align="center" v-if="columns.showColumn('faultStatus')">
        <template #default="scope">
          <dict-tag :options="options.call_fault_status" :value="scope.row.faultStatus" />
        </template>
      </el-table-column>
      <el-table-column prop="callPointType" label="点位类型" width="90" align="center" v-if="columns.showColumn('callPointType')">
        <template #default="scope">
          <dict-tag :options="options.call_point_type" :value="scope.row.callPointType" />
        </template>
      </el-table-column>
      <el-table-column prop="equipment" label="设备" min-width="160" align="center" v-if="columns.showColumn('equipment')">
        <template #default="scope"> {{ scope.row.equipmentType }} {{ scope.row.equipmentNo }} </template>
      </el-table-column>
      <el-table-column prop="stationId" label="工站Id" width="90" align="center" v-if="columns.showColumn('stationId')" />
      <el-table-column prop="stationName" label="工站名称" min-width="150" align="center" v-if="columns.showColumn('stationName')" />
      <el-table-column prop="remark" label="备注" min-width="150" align="left" :show-overflow-tooltip="true" v-if="columns.showColumn('remark')" />
      <el-table-column prop="maxHandleTimes" label="最大处理时间（分钟）" width="90" align="center" v-if="columns.showColumn('maxHandleTimes')" />
      <el-table-column prop="maxHelpTimes" label="最大支援时间（分钟）" width="90" align="center" v-if="columns.showColumn('maxHelpTimes')" />
      <el-table-column prop="createTime" label="呼叫时间" width="160" v-if="columns.showColumn('createTime')" />
      <el-table-column prop="handleTime" label="处理时间" width="160" v-if="columns.showColumn('handleTime')" />
      <el-table-column prop="callHelpTime" label="呼叫支援时间" width="160" v-if="columns.showColumn('callHelpTime')" />
      <el-table-column prop="helpTime" label="支援时间" width="160" v-if="columns.showColumn('helpTime')" />
      <el-table-column prop="callHelpWay" label="支援方式" width="90" align="center" v-if="columns.showColumn('callHelpWay')">
        <template #default="scope">
          <dict-tag :options="options.call_help_way" :value="scope.row.callHelpWay" />
        </template>
      </el-table-column>
      <el-table-column prop="endTime" label="结束时间" width="160" v-if="columns.showColumn('endTime')" />
      <el-table-column prop="comeMinute" label="到场时长" width="90" v-if="columns.showColumn('comeMinute')" />
      <el-table-column prop="handleMinute" label="处理时长" width="90" v-if="columns.showColumn('handleMinute')" />
      <el-table-column prop="helpMinute" label="支援时长" width="90" v-if="columns.showColumn('helpMinute')" />
      <el-table-column prop="totalMinute" label="总计时长" width="90" v-if="columns.showColumn('totalMinute')" />
      <el-table-column prop="handlerNo" label="处理人工号" width="90" align="center" v-if="columns.showColumn('handlerNo')" />
      <el-table-column prop="handlerName" label="处理人" width="90" align="center" v-if="columns.showColumn('handlerName')" />
      <el-table-column prop="helperNo" label="支援人工号" width="90" align="center" v-if="columns.showColumn('helperNo')" />
      <el-table-column prop="helperName" label="支援人" width="90" align="center" v-if="columns.showColumn('helperName')" />
      <el-table-column prop="solverNo" label="解决人工号" width="90" align="center" v-if="columns.showColumn('solverNo')" />
      <el-table-column prop="solverName" label="解决人" width="90" align="center" v-if="columns.showColumn('solverName')" />
      <el-table-column prop="qcNo" label="品质工号" width="90" align="center" v-if="columns.showColumn('qcNo')" />
      <el-table-column prop="qcName" label="品质" width="90" align="center" v-if="columns.showColumn('qcName')" />
      <el-table-column prop="faultType" label="故障类型" min-width="120" align="center" v-if="columns.showColumn('faultType')" />
      <el-table-column
        prop="faultContent"
        label="故障内容"
        min-width="150"
        :show-overflow-tooltip="true"
        v-if="columns.showColumn('faultContent')" />
      <el-table-column
        prop="solutionContent"
        label="解决方案内容"
        min-width="150"
        :show-overflow-tooltip="true"
        v-if="columns.showColumn('solutionContent')" />
      <el-table-column prop="prodCount" label="制品跟踪数" width="90" align="center" v-if="columns.showColumn('prodCount')" />
      <el-table-column prop="passCount" label="良品数量" width="90" align="center" v-if="columns.showColumn('passCount')" />
      <el-table-column prop="ngCount" label="不良品数量" width="90" align="center" v-if="columns.showColumn('ngCount')" />
      <el-table-column prop="pcIp" label="呼叫电脑IP" width="140" align="center" :show-overflow-tooltip="true" v-if="columns.showColumn('pcIp')" />
      <el-table-column label="操作" width="120" fixed="right">
        <template #default="scope">
          <el-button type="primary" size="small" icon="view" title="详情" @click="handlePreview(scope.row)"></el-button>
          <el-button
            type="danger"
            size="small"
            icon="delete"
            title="删除"
            v-hasPermi="['call:fault:base:delete']"
            @click="handleDelete(scope.row)"></el-button>
        </template>
      </el-table-column>
    </el-table>
    <pagination :total="total" v-model:page="queryParams.pageNum" v-model:limit="queryParams.pageSize" @pagination="getList" />

    <el-drawer v-model="drawer" size="50%" direction="rtl">
      <el-table :data="callFaultOperateList" header-row-class-name="text-navy">
        <el-table-column label="序号" type="index" width="80" />
        <el-table-column prop="callId" label="呼叫Id" />
        <el-table-column prop="operaterNo" label="操作人工号" />
        <el-table-column prop="operaterName" label="操作人" />
        <el-table-column prop="faultStatus" label="故障状态">
          <template #default="scope">
            <dict-tag :options="options.call_fault_status" :value="scope.row.faultStatus" />
          </template>
        </el-table-column>
        <el-table-column prop="createTime" label="创建时间" />
      </el-table>
    </el-drawer>

    <!-- 添加或修改故障记录对话框 -->
    <el-dialog :title="title" :lock-scroll="false" v-model="open" :fullscreen="fullScreen">
      <el-form ref="formRef" :model="form" :rules="rules" label-width="100px">
        <el-row :gutter="20">
          <el-col :lg="12" v-if="opertype != 1">
            <el-form-item label="呼叫ID" prop="callId">
              <el-input-number v-model.number="form.callId" controls-position="right" placeholder="请输入呼叫记录ID" :disabled="true" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="线别ID" prop="lineId">
              <el-select v-model="form.lineId" placeholder="请选择线别ID">
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
              <el-select v-model="form.callReason" placeholder="请选择呼叫原因">
                <el-option v-for="item in options.call_reason" :key="item.dictValue" :label="item.dictLabel" :value="item.dictValue"></el-option>
              </el-select>
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="故障状态" prop="faultStatus">
              <el-select v-model="form.faultStatus" placeholder="请输入故障状态">
                <el-option
                  v-for="item in options.call_fault_status"
                  :key="item.dictValue"
                  :label="item.dictLabel"
                  :value="item.dictValue"></el-option>
              </el-select>
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="设备类型" prop="equipmentType">
              <el-select v-model="form.equipmentType" placeholder="请选择设备类型">
                <el-option
                  v-for="item in options.equipmentTypeOptions"
                  :key="item.dictValue"
                  :label="item.dictLabel"
                  :value="item.dictValue"></el-option>
              </el-select>
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="设备编号" prop="equipmentNo">
              <el-input v-model="form.equipmentNo" placeholder="请输入设备编号" />
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

          <el-col :lg="12">
            <el-form-item label="呼叫目标" prop="callTargetType">
              <el-select v-model="form.callTargetType" placeholder="请选择呼叫目标" clearable>
                <el-option
                  v-for="item in options.call_target_type"
                  :key="item.dictValue"
                  :label="item.dictLabel"
                  :value="item.dictValue"></el-option>
              </el-select>
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="最大处理时间" prop="maxHandleTimes">
              <el-input v-model.number="form.maxHandleTimes" placeholder="请输入最大处理时间（分钟）" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="最大支援时间" prop="maxHelpTimes">
              <el-input v-model.number="form.maxHelpTimes" placeholder="请输入最大支援时间（分钟）" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="呼叫时间" prop="createTime">
              <el-date-picker v-model="form.createTime" type="datetime" :teleported="false" placeholder="选择日期时间"></el-date-picker>
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="处理时间" prop="handleTime">
              <el-date-picker v-model="form.handleTime" type="datetime" :teleported="false" placeholder="选择日期时间"></el-date-picker>
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="呼叫支援时间" prop="callHelpTime">
              <el-date-picker v-model="form.callHelpTime" type="datetime" :teleported="false" placeholder="选择日期时间"></el-date-picker>
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="支援时间" prop="helpTime">
              <el-date-picker v-model="form.helpTime" type="datetime" :teleported="false" placeholder="选择日期时间"></el-date-picker>
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="结束时间" prop="endTime">
              <el-date-picker v-model="form.endTime" type="datetime" :teleported="false" placeholder="选择日期时间"></el-date-picker>
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="处理人" prop="handlerNo">
              <el-select v-model="form.handlerNo" placeholder="请输入姓名" clearable filterable remote :remote-method="handleQueryEmployee">
                <el-option
                  v-for="item in options.emp_options"
                  :key="item.dictValue"
                  :label="item.dictValue + ' - ' + item.dictLabel"
                  :value="item.dictValue"></el-option>
              </el-select>
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="支援人" prop="helperNo">
              <el-select v-model="form.helperNo" placeholder="请输入姓名" clearable filterable remote :remote-method="handleQueryEmployee">
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
              <el-select v-model="form.solverNo" placeholder="请输入姓名" clearable filterable remote :remote-method="handleQueryEmployee">
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
              <el-select v-model="form.qcNo" placeholder="请输入姓名" clearable filterable remote :remote-method="handleQueryEmployee">
                <el-option
                  v-for="item in options.emp_options"
                  :key="item.dictValue"
                  :label="item.dictValue + ' - ' + item.dictLabel"
                  :value="item.dictValue"></el-option>
              </el-select>
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="故障类型" prop="faultType">
              <el-select v-model="form.faultType" placeholder="请选择故障类型">
                <el-option
                  v-for="item in options.faultTypeOptions"
                  :key="item.dictValue"
                  :label="item.dictLabel"
                  :value="item.dictValue"></el-option>
              </el-select>
            </el-form-item>
          </el-col>

          <el-col :lg="24">
            <el-form-item label="故障内容" prop="faultContent">
              <el-input v-model="form.faultContent" placeholder="请输入故障内容" />
            </el-form-item>
          </el-col>

          <el-col :lg="24">
            <el-form-item label="解决方案内容" prop="solutionContent">
              <el-input v-model="form.solutionContent" placeholder="请输入解决方案内容" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="制品跟踪数" prop="prodCount">
              <el-input v-model.number="form.prodCount" placeholder="请输入制品跟踪数" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="良品数量" prop="passCount">
              <el-input v-model.number="form.passCount" placeholder="请输入良品数量" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="不良品数量" prop="ngCount">
              <el-input v-model.number="form.ngCount" placeholder="请输入不良品数量" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="支援方式" prop="callHelpWay">
              <el-select v-model="form.callHelpWay" placeholder="请选择支援方式">
                <el-option v-for="item in options.call_help_way" :key="item.dictValue" :label="item.dictLabel" :value="item.dictValue"></el-option>
              </el-select>
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="呼叫电脑IP" prop="pcIp">
              <el-input v-model="form.pcIp" placeholder="请输入呼叫电脑IP" />
            </el-form-item>
          </el-col>
        </el-row>
        <el-divider content-position="center">呼叫操作记录信息</el-divider>
        <el-row :gutter="10" class="mb8">
          <el-col :span="1.5">
            <el-button type="primary" icon="Plus" @click="handleAddCallFaultOperate">添加</el-button>
          </el-col>
          <el-col :span="1.5">
            <el-button type="danger" icon="Delete" @click="handleDeleteCallFaultOperate">删除</el-button>
          </el-col>
          <el-col :span="1.5">
            <el-button type="info" icon="FullScreen" @click="fullScreen = !fullScreen">{{ fullScreen ? '退出全屏' : '全屏' }}</el-button>
          </el-col>
        </el-row>
        <el-table
          :data="callFaultOperateList"
          :row-class-name="rowCallFaultOperateIndex"
          @selection-change="handleCallFaultOperateSelectionChange"
          ref="CallFaultOperateRef">
          <el-table-column type="selection" width="40" align="center" />
          <el-table-column label="序号" align="center" prop="index" width="50" />
          <el-table-column label="操作人" align="center" prop="operaterNo">
            <template #default="scope">
              <el-select v-model="scope.row.operaterNo" placeholder="请输入姓名" clearable filterable remote :remote-method="handleQueryEmployee">
                <el-option
                  v-for="item in options.emp_options"
                  :key="item.dictValue"
                  :label="item.dictValue + ' - ' + item.dictLabel"
                  :value="item.dictValue"></el-option>
              </el-select>
            </template>
          </el-table-column>
          <el-table-column label="故障状态" prop="faultStatus">
            <template #default="scope">
              <el-select v-model="scope.row.faultStatus" placeholder="请选择故障状态">
                <el-option
                  v-for="item in options.call_fault_status"
                  :key="item.dictValue"
                  :label="item.dictLabel"
                  :value="item.dictValue"></el-option>
              </el-select>
            </template>
          </el-table-column>
          <el-table-column label="创建时间" align="center" prop="createTime">
            <template #default="scope">
              <el-date-picker clearable v-model="scope.row.createTime" type="datetime" placeholder="选择日期时间"></el-date-picker>
            </template>
          </el-table-column>
        </el-table>
      </el-form>
      <template #footer v-if="opertype != 3">
        <el-button text @click="cancel">{{ $t('btn.cancel') }}</el-button>
        <el-button type="primary" @click="submitForm">{{ $t('btn.submit') }}</el-button>
      </template>
    </el-dialog>
  </div>
</template>

<script setup name="callfaultbase">
import {
  listCallFaultBase,
  addCallFaultBase,
  delCallFaultBase,
  updateCallFaultBase,
  getCallFaultBase,
  stopCallFaultBase
} from '@/api/call/callFaultBase.js'
import useBasicStore from '@/store/modules/basic.js'
import { dictEquipmentType } from '@/api/equipment/equipmentType.js'
import { dictCallArea } from '@/api/call/callArea.js'
import { dictEmployee } from '@/api/basic/employee.js'
const { proxy } = getCurrentInstance()
const ids = ref([])
const loading = ref(false)
const showSearch = ref(true)
const queryParams = reactive({
  pageNum: 1,
  pageSize: 10,
  sort: 'CreateTime',
  sortType: 'desc',
  areaId: undefined,
  lineId: undefined,
  callTargetType: undefined,
  callPointType: undefined,
  equipmentType: undefined,
  createTime: undefined,
  faultStatus: undefined,
  handlerNo: undefined,
  helperNo: undefined,
  solverNo: undefined
})
const columns = ref([
  { visible: false, prop: 'callId', label: '呼叫ID' },
  { visible: false, prop: 'areaId', label: '区域Id' },
  { visible: true, prop: 'areaName', label: '区域' },
  { visible: false, prop: 'lineId', label: '线别ID' },
  { visible: true, prop: 'lineName', label: '线别' },
  { visible: true, prop: 'callReason', label: '呼叫原因' },
  { visible: true, prop: 'callTargetType', label: '呼叫目标' },
  { visible: true, prop: 'faultStatus', label: '故障状态' },
  { visible: true, prop: 'callPointType', label: '点位类型' },
  { visible: true, prop: 'equipment', label: '设备' },
  { visible: false, prop: 'stationId', label: '工站Id' },
  { visible: true, prop: 'stationName', label: '工站名称' },
  { visible: true, prop: 'remark', label: '备注' },
  { visible: false, prop: 'maxHandleTimes', label: '最大处理时间' },
  { visible: false, prop: 'maxHelpTimes', label: '最大支援时间' },
  { visible: true, prop: 'createTime', label: '呼叫时间' },
  { visible: false, prop: 'handleTime', label: '处理时间' },
  { visible: false, prop: 'callHelpTime', label: '呼叫支援时间' },
  { visible: false, prop: 'helpTime', label: '支援时间' },
  { visible: false, prop: 'endTime', label: '结束时间' },
  { visible: true, prop: 'comeMinute', label: '到场时长' },
  { visible: true, prop: 'handleMinute', label: '处理时长' },
  { visible: false, prop: 'helpMinute', label: '支援时长' },
  { visible: false, prop: 'totalMinute', label: '总计时长' },
  { visible: false, prop: 'callHelpWay', label: '支援方式' },
  { visible: false, prop: 'handlerNo', label: '处理人工号' },
  { visible: true, prop: 'handlerName', label: '处理人' },
  { visible: false, prop: 'helperNo', label: '支援人工号' },
  { visible: true, prop: 'helperName', label: '支援人' },
  { visible: false, prop: 'solverNo', label: '解决人工号' },
  { visible: true, prop: 'solverName', label: '解决人' },
  { visible: false, prop: 'qcNo', label: '品质工号' },
  { visible: true, prop: 'qcName', label: '品质' },
  { visible: false, prop: 'faultType', label: '故障类型' },
  { visible: true, prop: 'faultContent', label: '故障内容' },
  { visible: true, prop: 'solutionContent', label: '解决方案' },
  { visible: false, prop: 'prodCount', label: '制品跟踪数' },
  { visible: false, prop: 'passCount', label: '良品数量' },
  { visible: false, prop: 'ngCount', label: '不良品数量' },
  { visible: false, prop: 'pcIp', label: '呼叫电脑IP' }
])
const total = ref(0)
const dataList = ref([])
const queryRef = ref()
const defaultTime = ref([new Date(2000, 1, 1, 0, 0, 0), new Date(2000, 2, 1, 23, 59, 59)])

// 开始呼叫时间时间范围
const dateRangeCreateTime = ref([])

var dictParams = [
  { dictType: 'call_help_way' },
  { dictType: 'call_reason' },
  { dictType: 'call_fault_status' },
  { dictType: 'sys_yes_no' },
  { dictType: 'call_target_type' },
  { dictType: 'call_point_type' }
]

proxy.getDicts(dictParams).then((response) => {
  response.data.forEach((element) => {
    state.options[element.dictType] = element.list
  })
})

function getList() {
  proxy.addDateRange(queryParams, dateRangeCreateTime.value, 'CreateTime')
  loading.value = true
  let params = { ...queryParams }
  if (params.faultStatus) params.faultStatus = params.faultStatus.join(',')
  listCallFaultBase(params).then((res) => {
    const { code, data } = res
    if (code == 200) {
      dataList.value = data.result
      total.value = data.totalNum
      loading.value = false
    }
  })
}

// 查询
function handleQuery() {
  queryParams.pageNum = 1
  getList()
}

// 重置查询操作
function resetQuery() {
  // 开始呼叫时间时间范围
  dateRangeCreateTime.value = []
  proxy.resetForm('queryRef')
  handleQuery()
}
// 自定义排序
function sortChange(column) {
  var sort = undefined
  var sortType = undefined

  if (column.prop != null && column.order != null) {
    sort = column.prop
    sortType = column.order
  }
  queryParams.sort = sort
  queryParams.sortType = sortType
  handleQuery()
}

// 导出按钮操作
function handleExport() {
  proxy
    .$confirm('是否确认导出呼叫记录信息数据项?', '警告', {
      confirmButtonText: '确定',
      cancelButtonText: '取消',
      type: 'warning'
    })
    .then(async () => {
      await proxy.downFile('/call/CallFaultBase/export', { ...queryParams })
    })
}

/*************** form操作 ***************/
const formRef = ref()
const title = ref('')
// 操作类型 1、add 2、edit 3、view
const opertype = ref(0)
const open = ref(false)
const state = reactive({
  single: true,
  multiple: true,
  form: {},
  rules: {
    callReason: [{ required: true, message: '呼叫原因不能为空', trigger: 'change' }],
    faultStatus: [{ required: true, message: '故障状态不能为空', trigger: 'blur' }]
  },
  options: {
    //区域 选项列表
    call_area_options: [],
    // 线别ID 选项列表 格式 eg:{ dictLabel: '标签', dictValue: '0'}
    //lineIdOptions: [],
    //呼叫支援方式
    call_help_way: [],
    //呼叫故障状态
    call_fault_status: [],
    //设备类型
    equipment_type_options: [],
    //人员选项
    emp_options: [],
    //呼叫目标类型
    call_target_type: [],
    //呼叫点位类型
    call_point_type: []
  }
})

const { form, rules, options, single, multiple } = toRefs(state)

// 关闭dialog
function cancel() {
  open.value = false
  reset()
}

// 重置表单
function reset() {
  form.value = {
    callId: null,
    areaId: null,
    lineId: null,
    callReason: null,
    callPointType: null,
    callTargetType: null,
    equipmentType: null,
    equipmentNo: null,
    stationId: null,
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
    qcNo: null,
    pcIp: null
  }
  callFaultOperateList.value = []
  proxy.resetForm('formRef')
}

// 多选框选中数据
function handleSelectionChange(selection) {
  ids.value = selection.map((item) => item.callId)
  single.value = selection.length != 1
  multiple.value = !selection.length
}

/**
 * 查看
 * @param {*} row
 */
function handlePreview(row) {
  reset()
  const id = row.callId
  getCallFaultBase(id).then((res) => {
    const { code, data } = res
    if (code == 200) {
      open.value = true
      title.value = '查看'
      opertype.value = 3
      form.value = {
        ...data
      }
      callFaultOperateList.value = res.data.callFaultOperateNav
    }
  })
}

// 添加按钮操作
function handleAdd() {
  reset()
  open.value = true
  title.value = '添加故障记录'
  opertype.value = 1
}
// 修改按钮操作
function handleUpdate(row) {
  reset()
  const id = row.callId || ids.value
  getCallFaultBase(id).then((res) => {
    const { code, data } = res
    if (code == 200) {
      open.value = true
      title.value = '修改故障记录'
      opertype.value = 2

      form.value = {
        ...data
      }

      callFaultOperateList.value = res.data.callFaultOperateNav
      let emps = []
      if (data.handlerNo) emps.push({ dictValue: data.handlerNo, dictLabel: data.handlerName })
      if (data.helperNo) emps.push({ dictValue: data.helperNo, dictLabel: data.helperName })
      if (data.solverNo) emps.push({ dictValue: data.solverNo, dictLabel: data.solverName })
      if (data.qcNo) emps.push({ dictValue: data.qcNo, dictLabel: data.qcName })

      options.value.emp_options = emps
    }
  })
}

// 添加&修改 表单提交
function submitForm() {
  proxy.$refs['formRef'].validate((valid) => {
    if (valid) {
      form.value.callFaultOperateNav = callFaultOperateList.value
      if (form.value.callId != undefined && opertype.value === 2) {
        updateCallFaultBase(form.value).then((res) => {
          proxy.$modal.msgSuccess('修改成功')
          open.value = false
          getList()
        })
      } else {
        addCallFaultBase(form.value).then((res) => {
          proxy.$modal.msgSuccess('新增成功')
          open.value = false
          getList()
        })
      }
    }
  })
}

// 停止按钮操作
function handleStop(row) {
  const Ids = row.callId || ids.value

  if (!Ids || Ids.length <= 0) {
    proxy.$modal.msgError('未选择需要停止的记录')
    return
  }

  proxy
    .$confirm('是否确认停止参数编号为"' + Ids + '"的数据项？', '警告', {
      confirmButtonText: proxy.$t('common.ok'),
      cancelButtonText: proxy.$t('common.cancel'),
      type: 'warning'
    })
    .then(function () {
      return stopCallFaultBase(Ids)
    })
    .then(() => {
      getList()
      proxy.$modal.msgSuccess('停止成功')
    })
}

// 删除按钮操作
function handleDelete(row) {
  const Ids = row.callId || ids.value

  proxy
    .$confirm('是否确认删除参数编号为"' + Ids + '"的数据项？', '警告', {
      confirmButtonText: proxy.$t('common.ok'),
      cancelButtonText: proxy.$t('common.cancel'),
      type: 'warning'
    })
    .then(function () {
      return delCallFaultBase(Ids)
    })
    .then(() => {
      getList()
      proxy.$modal.msgSuccess('删除成功')
    })
}

/*********************故障处理记录子表信息*************************/
const callFaultOperateList = ref([])
const checkedCallFaultOperate = ref([])
const fullScreen = ref(false)
const drawer = ref(false)

/** 故障处理记录序号 */
function rowCallFaultOperateIndex({ row, rowIndex }) {
  row.index = rowIndex + 1
}

/** 故障处理记录添加按钮操作 */
function handleAddCallFaultOperate() {
  let obj = {}
  //下面的代码自己设置默认值
  //obj.operaterNo = null;
  //obj.operaterName = null;
  //obj.faultStatus = null;
  //obj.createTime = null;
  callFaultOperateList.value.push(obj)
}

/** 复选框选中数据 */
function handleCallFaultOperateSelectionChange(selection) {
  checkedCallFaultOperate.value = selection.map((item) => item.index)
}

/** 故障处理记录删除按钮操作 */
function handleDeleteCallFaultOperate() {
  if (checkedCallFaultOperate.value.length == 0) {
    proxy.$modal.msgError('请先选择要删除的故障处理记录数据')
  } else {
    const CallFaultOperates = callFaultOperateList.value
    const checkedCallFaultOperates = checkedCallFaultOperate.value
    callFaultOperateList.value = CallFaultOperates.filter(function (item) {
      return checkedCallFaultOperates.indexOf(item.index) == -1
    })
  }
}

/** 故障处理记录详情 */
function rowClick(row) {
  const id = row.callId || ids.value
  getCallFaultBase(id).then((res) => {
    const { code, data } = res
    if (code == 200) {
      drawer.value = true
      callFaultOperateList.value = data.callFaultOperateNav
    }
  })
}

/**获取设备类型 */
function handleQueryEquipmentType(keyword) {
  if (keyword) {
    const params = {
      pageNum: 1,
      pageSize: 1000,
      sort: '',
      sortType: 'asc',
      equipmentTypeName: keyword
    }
    setTimeout(() => {
      dictEquipmentType(params).then((res) => {
        state.options.equipment_type_options = res.data.result
      })
    }, 200)
  }
}

//员工查询
function handleQueryEmployee(keyword) {
  if (keyword) {
    const query = {
      pageNum: 1,
      pageSize: 10,
      empName: keyword
    }
    setTimeout(() => {
      dictEmployee(query).then((res) => {
        options.value.emp_options = res.data.result
      })
    }, 200)
  }
}

//初始化区域选项
function initCallAreaOptions() {
  const query = {
    pageNum: 1,
    pageSize: 100,
    sort: '',
    sortType: 'asc'
  }
  dictCallArea(query).then((res) => {
    options.value.call_area_options = res.data.result
  })
}

initCallAreaOptions()
handleQuery()
</script>
