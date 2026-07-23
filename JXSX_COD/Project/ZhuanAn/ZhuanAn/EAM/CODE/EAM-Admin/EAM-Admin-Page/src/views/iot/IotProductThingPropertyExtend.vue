<!--
 * @Descripttion: (属性扩展描述/IOT_Product_Thing_Property_Extend)
 * @Author: (admin)
 * @Date: (2026-02-27)
-->
<template>
  <div>
    <!-- 添加或修改属性扩展描述对话框 -->
    <el-dialog :title="title" :lock-scroll="false" v-model="open">
      <el-form ref="formRef" :model="form" :rules="rules" label-width="100px">
        <el-row :gutter="20">
          <!-- <el-col :lg="12">
          <el-form-item label="属性ID" prop="propertyId">
            <el-input v-model.number="form.propertyId" placeholder="请输入属性ID" :disabled="opertype != 1" />
          </el-form-item>
        </el-col> -->

          <el-col :lg="12">
            <el-form-item label="操作类型" prop="operateType">
              <el-select v-model="form.operateType" placeholder="请选择操作类型">
                <el-option
                  v-for="item in options.iot_register_type"
                  :key="item.dictValue"
                  :label="item.dictLabel"
                  :value="item.dictValue"></el-option>
              </el-select>
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="寄存器地址" prop="registerAddress">
              <el-input-number v-model.number="form.registerAddress" :controls="true" controls-position="right" placeholder="请输入寄存器地址" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="原始数据类型" prop="originalDataType">
              <el-select v-model="form.originalDataType" placeholder="请选择原始数据类型">
                <el-option
                  v-for="item in options.iot_original_data_type"
                  :key="item.dictValue"
                  :label="item.dictLabel"
                  :value="item.dictValue"></el-option>
              </el-select>
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="寄存器数据个数" prop="registerCount">
              <el-input-number v-model.number="form.registerCount" :controls="true" controls-position="right" placeholder="请输入寄存器数据个数" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="交换寄存器内高低序" prop="swap16">
              <el-switch
                v-model="form.swap16"
                inline-prompt
                active-text="ture"
                inactive-text="false"
                :active-value="true"
                :inactive-value="false" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="交换寄存器顺序" prop="reverseRegister">
              <el-switch
                v-model="form.reverseRegister"
                inline-prompt
                active-text="ture"
                inactive-text="false"
                :active-value="true"
                :inactive-value="false" />
            </el-form-item>
          </el-col>

          <el-col :lg="24">
            <el-form-item label="缩放因子" prop="scaling">
              <el-input-number v-model.number="form.scaling" :controls="true" controls-position="right" placeholder="请输入缩放因子" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="下限" prop="lowLimit">
              <el-input-number v-model.number="form.lowLimit" :controls="true" controls-position="right" placeholder="请输入下限" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="上限" prop="upperLimit">
              <el-input-number v-model.number="form.upperLimit" :controls="true" controls-position="right" placeholder="请输入上限" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="读写操作" prop="functionCode">
              <el-select v-model="form.functionCode" placeholder="请选择读写操作">
                <el-option
                  v-for="item in options.functionCodeOptions"
                  :key="item.dictValue"
                  :label="item.dictLabel"
                  :value="item.dictValue"></el-option>
              </el-select>
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="数据上报方式" prop="reportingMethod">
              <el-select v-model="form.reportingMethod" placeholder="请选择数据上报方式">
                <el-option
                  v-for="item in options.iot_reporting_method"
                  :key="item.dictValue"
                  :label="item.dictLabel"
                  :value="item.dictValue"></el-option>
              </el-select>
            </el-form-item>
          </el-col>
        </el-row>
      </el-form>
      <template #footer v-if="opertype != 3">
        <el-button text @click="cancel">{{ $t('btn.cancel') }}</el-button>
        <el-button type="danger" v-if="opertype == 2" @click="handleDelete">{{ $t('btn.delete') }}</el-button>
        <el-button type="primary" @click="submitForm">{{ $t('btn.submit') }}</el-button>
      </template>
    </el-dialog>
  </div>
</template>

<script setup name="iotproductthingpropertyextend">
import {
  addIotProductThingPropertyExtend,
  delIotProductThingPropertyExtend,
  updateIotProductThingPropertyExtend,
  getIotProductThingPropertyExtend
} from '@/api/iot/iotProductThingProperty.js'
const props = defineProps({
  propertyId: Number
})
const emit = defineEmits(['close-form'])
const { proxy } = getCurrentInstance()

var dictParams = [{ dictType: 'iot_original_data_type' }, { dictType: 'iot_register_type' }, { dictType: 'iot_reporting_method' }]

proxy.getDicts(dictParams).then((response) => {
  response.data.forEach((element) => {
    state.options[element.dictType] = element.list
  })
})

// 查询
function handleQuery() {
  handleUpdate()
}

/*************** form操作 ***************/
const formRef = ref()
const title = ref('')
// 操作类型 1、add 2、edit 3、view
const opertype = ref(0)
const open = ref(false)
const state = reactive({
  form: {},
  rules: {
    propertyId: [{ required: true, message: '属性ID不能为空', trigger: 'blur', type: 'number' }],
    operateType: [{ required: true, message: '操作类型不能为空', trigger: 'change' }],
    registerAddress: [{ required: true, message: '寄存器地址不能为空', trigger: 'blur', type: 'number' }],
    originalDataType: [{ required: true, message: '原始数据类型不能为空', trigger: 'change' }],
    swap16: [{ required: true, message: '交换寄存器内高低序不能为空', trigger: 'blur' }],
    reverseRegister: [{ required: true, message: '交换寄存器顺序不能为空', trigger: 'blur' }],
    scaling: [{ required: true, message: '缩放因子不能为空', trigger: 'blur' }],
    reportingMethod: [{ required: true, message: '数据上报方式不能为空', trigger: 'change' }]
  },
  options: {
    // 操作类型 选项列表 格式 eg:{ dictLabel: '标签', dictValue: '0'}
    operateTypeOptions: [],
    // 原始数据类型 选项列表 格式 eg:{ dictLabel: '标签', dictValue: '0'}
    iot_original_data_type: [],
    // 寄存器类型
    iot_register_type: [],
    // 上报方式
    iot_reporting_method: []
  }
})

const { form, rules, options } = toRefs(state)

// 关闭dialog
function cancel() {
  open.value = false
  reset()
}

// 重置表单
function reset() {
  form.value = {
    propertyId: null,
    operateType: null,
    registerAddress: null,
    originalDataType: null,
    registerCount: null,
    swap16: null,
    reverseRegister: null,
    scaling: null,
    lowLimit: null,
    upperLimit: null,
    functionCode: null,
    reportingMethod: null
  }
  proxy.resetForm('formRef')
}

// 添加按钮操作
function handleAdd() {
  reset()
  open.value = true
  title.value = '添加属性扩展描述'
  opertype.value = 1
}
// 修改按钮操作
function handleUpdate() {
  reset()
  const id = props.propertyId
  if (id > 0)
    getIotProductThingPropertyExtend(id).then((res) => {
      const { code, data } = res
      if (code == 200) {
        open.value = true
        title.value = '修改属性扩展描述'
        opertype.value = 2

        form.value = {
          ...data
        }
      } else {
        open.value = true
        title.value = '新增属性扩展描述'
        opertype.value = 1
      }
    })
}

// 添加&修改 表单提交
function submitForm() {
  proxy.$refs['formRef'].validate((valid) => {
    if (valid) {
      if (form.value.propertyId != undefined && opertype.value === 2) {
        updateIotProductThingPropertyExtend(form.value).then((res) => {
          proxy.$modal.msgSuccess('修改成功')
          open.value = false
        })
      } else {
        form.value.propertyId = props.propertyId
        addIotProductThingPropertyExtend(form.value).then((res) => {
          proxy.$modal.msgSuccess('新增成功')
          open.value = false
        })
      }
    }
  })
}

// 删除按钮操作
function handleDelete() {
  const Id = props.propertyId

  proxy
    .$confirm('是否确认删除参数编号为"' + Id + '"的数据项？', '警告', {
      confirmButtonText: proxy.$t('common.ok'),
      cancelButtonText: proxy.$t('common.cancel'),
      type: 'warning'
    })
    .then(function () {
      return delIotProductThingPropertyExtend(Id)
    })
    .then(() => {
      proxy.$modal.msgSuccess('删除成功')
      open.value = false
    })
}

watch(open, (val) => {
  handleQuery()
  if (open.value === false) emit('close-form')
})

watch(
  () => props.productId,
  (newValue, oldValue) => {
    handleQuery()
  }
)

handleQuery()
</script>

<style>
.el-form-item__label {
  align-items: center;
  line-height: 1.2 !important;
}
</style>
