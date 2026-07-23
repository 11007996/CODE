<template>
  <!-- 流程业务表单_耗品领用单 -->
  <!-- 业务表单数据 -->
  <div>
    <el-form ref="formRef" :model="form" :rules="rules" :validate-on-rule-change="false" label-width="100px">
      <el-row :gutter="20">
        <el-col :lg="8" :sm="8" v-if="!fieldRules.lineId?.hidden">
          <el-form-item label="产线" prop="lineId">
            <el-select
              v-model="form.lineId"
              placeholder="请选择产线"
              clearable
              filterable
              :readonly="!fieldRules.lineId?.editable"
              class="fullWidth">
              <el-option
                v-for="item in useBasicStore().getLineDict"
                :key="item.dictValue"
                :label="item.dictLabel"
                :value="item.dictValue"></el-option>
            </el-select>
          </el-form-item>
        </el-col>

        <el-col :lg="8" :sm="8" v-if="!fieldRules.needDate?.hidden">
          <el-form-item label="需求时间" prop="needDate">
            <el-date-picker
              v-model="form.needDate"
              type="datetime"
              placeholder="选择日期时间"
              :readonly="!fieldRules.needDate?.editable"
              class="fullWidth"></el-date-picker>
          </el-form-item>
        </el-col>

        <el-col :lg="8" :sm="8" v-if="!fieldRules.purpose?.hidden">
          <el-form-item label="用途" prop="purpose">
            <el-input v-model="form.purpose" placeholder="请输入用途" :readonly="!fieldRules.purpose?.editable" />
          </el-form-item>
        </el-col>
      </el-row>
      <!-- 耗品清单 -->
      <el-row v-if="!fieldRules.consumableList?.hidden">
        <el-divider content-position="center">耗品领用单_耗品需求清单信息</el-divider>
        <el-row :gutter="10" class="mb8" v-if="fieldRules.consumableList?.editable">
          <el-col :span="1.5">
            <el-button type="primary" icon="Plus" @click="handleAddConsumable">添加</el-button>
          </el-col>
          <el-col :span="1.5">
            <el-button type="danger" icon="Delete" @click="handleDeleteConsumable">删除</el-button>
          </el-col>
        </el-row>
        <el-table
          :data="consumableList"
          :row-class-name="rowConsumableIndex"
          @selection-change="handleConsumableSelectionChange"
          ref="ConsumableRef">
          <el-table-column type="selection" width="40" align="center" v-if="fieldRules.consumableList?.editable" />
          <el-table-column label="耗品名称" align="center" prop="consumableId" min-width="120">
            <template #default="scope">
              <el-input
                v-model="scope.row.consumableName"
                clearable
                placeholder="请选择耗品"
                @focus="handleQueryConsumable(scope.row)"
                :readonly="!fieldRules.consumableList?.editable" />
            </template>
          </el-table-column>
          <el-table-column label="料号" prop="consumablePart" min-width="120" />
          <el-table-column label="规格" prop="spec" min-width="120" />
          <el-table-column label="单价" prop="price" width="90" />
          <el-table-column label="需求数量" align="center" prop="needQty" width="90">
            <template #default="scope">
              <el-input-number v-model="scope.row.needQty" :controls="false" :readonly="!fieldRules.consumableList?.editable" class="fullWidth" />
            </template>
          </el-table-column>
        </el-table>
      </el-row>
    </el-form>

    <!-- 耗品选择弹窗 -->
    <ConsumableSelectorForIdle v-model:visible="consumableSelectorVisible" @confirm="handleConsumableSelect"></ConsumableSelectorForIdle>
  </div>
</template>

<script setup name="consumablereceiveticketform">
import useBasicStore from '@/store/modules/basic.js'
import ConsumableSelectorForIdle from '@/views/components/DataSelector/ConsumableSelectorForIdle.vue'

const form = inject('form')
const fieldRules = inject('fieldRules')
const rules = inject('rules')
const { proxy } = getCurrentInstance()
const consumableList = ref([])
const checkedConsumable = ref([])

const state = reactive({
  options: {
    //料号选项
    part_options: []
  }
})
const { options, tableRules } = toRefs(state)

const consumableSelectorVisible = ref(false)
const consumableSelectorRowIndex = ref(0)
/** 上线通知单_治具需求清单治具查询 */
function handleQueryConsumable(row) {
  consumableSelectorRowIndex.value = row.index
  consumableSelectorVisible.value = true
}

//选择治具
const handleConsumableSelect = (consumable) => {
  consumableList.value[consumableSelectorRowIndex.value - 1].consumableId = consumable.consumableId
  consumableList.value[consumableSelectorRowIndex.value - 1].consumableName = consumable.consumableName
  consumableList.value[consumableSelectorRowIndex.value - 1].consumablePart = consumable.consumablePart
  consumableList.value[consumableSelectorRowIndex.value - 1].spec = consumable.spec
  consumableList.value[consumableSelectorRowIndex.value - 1].price = consumable.price
  consumableList.value[consumableSelectorRowIndex.value - 1].needQty = 1
}

function rowConsumableIndex({ row, rowIndex }) {
  row.index = rowIndex + 1
}

/** 耗品领用单_耗品需求清单添加按钮操作 */
function handleAddConsumable() {
  let obj = {}
  //下面的代码自己设置默认值
  //obj.needQty = null;
  consumableList.value.push(obj)
}

/** 复选框选中数据 */
function handleConsumableSelectionChange(selection) {
  checkedConsumable.value = selection.map((item) => item.index)
}

/** 耗品领用单_耗品需求清单删除按钮操作 */
function handleDeleteConsumable() {
  if (checkedConsumable.value.length == 0) {
    proxy.$modal.msgError('请先选择要删除的耗品领用单_耗品需求清单数据')
  } else {
    consumableList.value = consumableList.value.filter(function (item) {
      return checkedConsumable.value.indexOf(item.index) == -1
    })
  }
}

//表单初始化
function initFormData() {
  //初始化表单数据
  if (form.value.consumableList) {
    consumableList.value = JSON.parse(form.value.consumableList)
  }
}
//验证表单
async function validFormData() {
  //检查【耗品需求清单】表的数据
  if (!consumableList.value || consumableList.value.length <= 0) {
    proxy.$modal.msgError('耗品需求清单不能为空')
    return false
  }

  let errorMsg = []
  consumableList.value.forEach((item, index) => {
    if (!item.consumableId) {
      errorMsg.push(`第${index + 1}行耗品不能为空`)
    }
    if (item.needQty == null || item.needQty == undefined || item.needQty <= 0) {
      errorMsg.push(`第${index + 1}行需求数量不能为空`)
    }
  })
  if (errorMsg.length > 0) {
    proxy.$modal.msgError(errorMsg.join(' | '))
    return false
  }

  //检查其他表单项目的数据
  return await proxy.$refs['formRef']
    .validate((valid) => {
      if (valid) {
        //耗品清单，序列化
        if (fieldRules.value.consumableList?.editable) {
          form.value.consumableList = JSON.stringify(consumableList.value)
        }
      }
      return valid
    })
    .then((res) => {
      return res
    })
}
defineExpose({ validFormData })
//组件挂载
onMounted(() => {
  initFormData()
})
</script>
