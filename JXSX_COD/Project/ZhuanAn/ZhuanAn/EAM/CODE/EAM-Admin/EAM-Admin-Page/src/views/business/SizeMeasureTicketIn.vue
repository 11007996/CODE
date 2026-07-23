<!--
 * @Descripttion: (治具尺寸量测验收单_入库/BU_Size_Measure_Ticket_In)
 * @Author: (admin)
 * @Date: (2024-07-17)
-->
<template>
  <div>
    <!-- 治具尺寸量测验收单_入库_对话框 -->
    <el-form ref="formRef" :model="form" :rules="rules" label-width="100px">
      <el-row :gutter="20">
        <el-col :lg="12">
          <el-form-item label="业务编号" prop="ticketNo">
            <el-input v-model="form.ticketNo" placeholder="请输入业务编号" disabled />
          </el-form-item>
        </el-col>

        <el-col :lg="12">
          <el-form-item label="创建模式" prop="createMode">
            <el-select v-model="form.createMode" clearable placeholder="请选择创建模式" disabled class="fullWidth">
              <el-option
                v-for="item in options.measure_create_mode"
                :key="item.dictValue"
                :label="item.dictLabel"
                :value="item.dictValue"></el-option>
            </el-select>
          </el-form-item>
        </el-col>

        <el-col :lg="12">
          <el-form-item label="产品料号" prop="partId">
            <el-input v-model="form.partId" placeholder="请输入产品料号" disabled />
          </el-form-item>
        </el-col>

        <el-col :lg="12">
          <el-form-item label="治具名称" prop="fixtureName">
            <el-input v-model="form.fixtureName" placeholder="请输入治具名称" disabled />
          </el-form-item>
        </el-col>

        <el-col :lg="12">
          <el-form-item label="治具图号" prop="drawingNo">
            <el-input v-model="form.drawingNo" placeholder="请输入治具图号" disabled />
          </el-form-item>
        </el-col>
      </el-row>

      <el-divider content-position="center">治具尺寸量测验收单_治具入库</el-divider>
      <el-table
        border
        :data="sizeMeasureTicketItemList"
        :row-class-name="rowSizeMeasureTicketItemIndex"
        @selection-change="handleSizeMeasureTicketItemSelectionChange"
        ref="SizeMeasureTicketItemRef">
        <el-table-column type="selection" width="40" align="center" />
        <el-table-column label="治具编号" align="center" prop="fixtureNo" width="90" />
        <el-table-column label="尺寸判定" align="center" prop="sizeResult" width="90">
          <template #default="scope">
            <dict-tag :options="options.judgment_result" :value="scope.row.sizeResult" />
          </template>
        </el-table-column>
        <el-table-column label="入库" align="center" prop="inStorage" width="90">
          <template #default="scope">
            <dict-tag :options="options.sys_yes_no" :value="scope.row.inStorage" />
          </template>
        </el-table-column>
        <el-table-column label="入库储位" align="center" prop="storageId" min-width="250">
          <template #default="scope">
            <el-cascader
              class="w100"
              :options="useBasicStore().getFixtureStorageTree"
              :props="{ checkStrictly: true, value: 'storageId', label: 'storageName', emitPath: false }"
              placeholder="请选择储位"
              fittler
              clearable
              @change="changeStorageStore(scope.row.index)"
              v-model="scope.row.storageId">
              <template #default="{ node, data }">
                <span>{{ data.storageName }}</span>
                <span v-if="!node.isLeaf"> ({{ data.children.length }}) </span>
              </template>
            </el-cascader>
          </template>
        </el-table-column>
      </el-table>
    </el-form>
  </div>
</template>

<script setup name="sizemeasureticket">
import { getSizeMeasureTicket, sizeMeasureTicketInStorage } from '@/api/business/sizeMeasureTicket.js'
import useBasicStore from '@/store/modules/basic.js'
import { getFixtureBase } from '@/api/fixture/fixtureBase.js'

const props = defineProps({
  ticketNo: String
})
const { proxy } = getCurrentInstance()
const loading = ref(false)

var dictParams = [{ dictType: 'judgment_result' }, { dictType: 'measure_create_mode' }, { dictType: 'sys_yes_no' }]
//获取字典数据
proxy.getDicts(dictParams).then((response) => {
  response.data.forEach((element) => {
    state.options[element.dictType] = element.list
  })
})

/*************** form操作 ***************/
const formRef = ref()
const state = reactive({
  multiple: true,
  form: {},
  fixture: {},
  rules: {
    ticketNo: [{ required: true, message: '业务编号不能为空', trigger: 'blur' }],
    initiatorId: [{ required: true, message: '发起人不能为空', trigger: 'blur' }],
    partId: [{ required: true, message: '产品料号不能为空', trigger: 'blur' }],
    fixtureId: [{ required: true, message: '治具不能为空', trigger: 'blur' }]
  },
  options: {
    // 业务状态 选项列表 格式 eg:{ dictLabel: '标签', dictValue: '0'}
    status_options: [],
    //判定结果
    judgment_result: [],
    //测量创建模式
    measure_create_mode: [],
    //是否
    sys_yes_no: []
  }
})

const { form, fixture, rules, options } = toRefs(state)

// 重置表单
function reset() {
  form.value = {
    ticketNo: null,
    partId: null,
    fixtureId: null
  }
  sizeMeasureTicketItemDefineList.value = []
  sizeMeasureTicketItemList.value = []
  proxy.resetForm('formRef')
}

// 修改按钮操作
function handlePreview() {
  reset()
  const id = props.ticketNo
  getSizeMeasureTicket(id).then((res) => {
    const { code, data } = res
    if (code == 200) {
      form.value = {
        ...data
      }
      sizeMeasureTicketItemDefineList.value = res.data.sizeMeasureTicketItemDefineNav
      sizeMeasureTicketItemList.value = res.data.dynamicStatisticalReport
      getFixtureBase(form.value.fixtureId).then((res) => {
        if (res.code == 200) {
          fixture.value = res.data
        }
      })
    }
  })
}

// 添加&修改 表单提交
function submitForm() {
  proxy.$refs['formRef'].validate((valid) => {
    if (valid) {
      form.value.CheckedFixtureList = checkedSizeMeasureTicketItem.value
      if (form.value.ticketNo != undefined) {
        sizeMeasureTicketInStorage(form.value).then((res) => {
          proxy.$modal.msgSuccess('入库成功')
          handlePreview()
        })
      }
    }
  })
}

/*********************治具尺寸量测验收单_治具测量值子表信息*************************/
const sizeMeasureTicketItemList = ref([])
const sizeMeasureTicketItemDefineList = ref([])
const checkedSizeMeasureTicketItem = ref([])

/** 治具尺寸量测验收单_治具测量值序号 */
function rowSizeMeasureTicketItemIndex({ row, rowIndex }) {
  row.index = rowIndex + 1
}

/** 复选框选中数据 */
function handleSizeMeasureTicketItemSelectionChange(selection) {
  checkedSizeMeasureTicketItem.value = selection
}

/** 储位选择变动事件 */
function changeStorageStore(index) {
  //如果是第一行,将其他所有行都变为第一行的值
  if (index == 1) {
    const storageId = sizeMeasureTicketItemList.value[0].storageId
    sizeMeasureTicketItemList.value.forEach((item) => {
      item.storageId = storageId
    })
  }
}

defineExpose({ submitForm })
handlePreview()
</script>
