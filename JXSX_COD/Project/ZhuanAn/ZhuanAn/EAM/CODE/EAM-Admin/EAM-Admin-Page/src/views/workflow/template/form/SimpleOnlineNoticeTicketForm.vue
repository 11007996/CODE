<template>
  <!-- 流程业务表单_上线通知单（简） -->
  <!-- 业务表单数据 -->
  <div>
    <el-form ref="formRef" :model="form" :rules="rules" :validate-on-rule-change="false" label-width="100px">
      <el-row :gutter="20">
        <el-col :lg="8" :sm="12" v-if="!fieldRules.newPartName?.hidden">
          <el-form-item label="新上线料号" prop="newPartName">
            <!-- <el-input v-model="form.newPartName" placeholder="请输入料号" :readonly="!fieldRules.newPartName?.editable"> </el-input> -->
            <el-select
              v-model="form.newPartName"
              placeholder="请输入料号"
              clearable
              filterable
              allow-create
              remote
              :readonly="!fieldRules.newPartName?.editable"
              :remote-method="handleQueryPartName"
              @change="handlePartChange">
              <el-option v-for="item in options.partName_options" :label="item.dictLabel" :value="item.dictLabel"></el-option>
            </el-select>
          </el-form-item>
        </el-col>

        <el-col :lg="8" :sm="12" v-if="!fieldRules.oldPartName?.hidden">
          <el-form-item label="在制料号" prop="oldPartName">
            <el-select
              v-model="form.oldPartName"
              placeholder="请输入料号"
              clearable
              filterable
              allow-create
              remote
              :readonly="!fieldRules.oldPartName?.editable"
              :remote-method="handleQueryPartName">
              <el-option v-for="item in options.partName_options" :label="item.dictLabel" :value="item.dictLabel"></el-option>
            </el-select>
            <!-- <el-input v-model="form.oldPartName" placeholder="请输入料号" :readonly="!fieldRules.oldPartName?.editable"> </el-input> -->
          </el-form-item>
        </el-col>

        <el-col :lg="8" :sm="12" v-if="!fieldRules.productQty?.hidden">
          <el-form-item label="产量" prop="productQty">
            <el-input-number
              v-model.number="form.productQty"
              :controls="false"
              placeholder="请输入产量"
              :readonly="!fieldRules.productQty?.editable"
              class="fullWidth" />
          </el-form-item>
        </el-col>

        <el-col :lg="8" :sm="12" v-if="!fieldRules.lineId?.hidden">
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

        <el-col :lg="8" :sm="12" v-if="!fieldRules.needTime?.hidden">
          <el-form-item label="需求时间" prop="needTime">
            <el-date-picker
              v-model="form.needTime"
              type="datetime"
              placeholder="选择日期时间"
              class="fullWidth"
              :readonly="!fieldRules.needTime?.editable"></el-date-picker>
          </el-form-item>
        </el-col>
      </el-row>

      <!-- 设备*治具清单 -->
      <el-row v-if="!fieldRules.itemList?.hidden">
        <el-divider content-position="center">上线通知单_需求清单</el-divider>
        <el-row :gutter="10" class="mb8" v-if="fieldRules.itemList?.editable">
          <el-col :span="1.5">
            <el-button type="primary" icon="Plus" size="small" @click="handleAddItem">添加</el-button>
          </el-col>
          <el-col :span="1.5">
            <el-button type="danger" icon="Delete" size="small" @click="handleDeleteItem">删除</el-button>
          </el-col>
        </el-row>
        <el-table max-height="500" :data="itemList" :row-class-name="rowItemIndex" @selection-change="handleItemSelectionChange" ref="EquipmentRef">
          <el-table-column type="selection" width="40" align="center" v-if="fieldRules.itemList?.editable" />
          <el-table-column label="设备*治具" align="center" prop="itemName" min-width="200">
            <template #default="scope">
              <el-select
                v-model="scope.row.itemName"
                placeholder="请输入设备*治具名称"
                clearable
                filterable
                allow-create
                remote
                :readonly="!fieldRules.itemList?.editable"
                :remote-method="handleQueryItem"
                class="fullWidth">
                <el-option v-for="item in options.item_options" :label="item.dictLabel" :value="item.dictLabel"></el-option>
              </el-select>
            </template>
          </el-table-column>
          <el-table-column label="需求数量" align="center" prop="needQty" width="90">
            <template #default="scope">
              <el-input-number v-model="scope.row.needQty" :controls="false" :readonly="!fieldRules.itemList?.editable" class="fullWidth" />
            </template>
          </el-table-column>
        </el-table>
      </el-row>
    </el-form>
  </div>
</template>

<script setup name="simpleOnlinenoticeticketform">
import useBasicStore from '@/store/modules/basic.js'
import {
  dictSimpleOnlineNoticeTicketItem,
  dictSimpleOnlineNoticeTicketPartName,
  listSimpleOnlineNoticeTicketItemsByPart
} from '@/api/business/simpleOnlineNoticeTicket.js'

const form = inject('form')
const fieldRules = inject('fieldRules')
const rules = inject('rules')

const { proxy } = getCurrentInstance()
const itemList = ref([])
const checkedItems = ref([])

const state = reactive({
  options: {
    item_options: [],
    partName_options: []
  }
})
const { options } = toRefs(state)

/******************************* 设备清单处理 *********************************** */

function rowItemIndex({ row, rowIndex }) {
  row.index = rowIndex + 1
}

/** 上线通知单_需求清单添加按钮操作 */
function handleAddItem() {
  let obj = {}
  itemList.value.push(obj)
}

/** 复选框选中数据 */
function handleItemSelectionChange(selection) {
  checkedItems.value = selection.map((item) => item.index)
}

/** 上线通知单_需求清单删除按钮操作 */
function handleDeleteItem() {
  if (checkedItems.value.length == 0) {
    proxy.$modal.msgError('请先选择要删除的上线通知单_需求清单数据')
  } else {
    itemList.value = itemList.value.filter(function (item) {
      return checkedItems.value.indexOf(item.index) == -1
    })
  }
}

//表单初始化
function initFormData() {
  //初始化表单数据
  //设备清单反序列化
  if (form.value.itemList) {
    itemList.value = JSON.parse(form.value.itemList)
  }
}

//验证表单
async function validFormData() {
  return await proxy.$refs['formRef']
    .validate((valid) => {
      if (valid) {
      }
      return valid
    })
    .then((res) => {
      if (res) {
        //设备清单，序列化
        if (fieldRules.value.itemList?.editable) {
          if (!itemList.value || itemList.value.length <= 0) {
            proxy.$modal.msgError('设备*治具最少需要一条记录')
            return false
          }
          itemList.value.forEach((item) => {
            if (!item.itemName || !item.needQty || item.needQty <= 0) {
              res = false
            }
          })
          if (res == false) {
            proxy.$modal.msgError('设备*治具名称不能为空，且数量要大于0')
            return false
          }
          //检查是否有相同名称
          if (hasDuplicate(itemList.value, 'itemName')) {
            proxy.$modal.msgError('设备*治具名称存在重复名称，请检查')
            return false
          }
          form.value.itemList = JSON.stringify(itemList.value)
        }
      }
      return res
    })
}

//数组对象指定属性是否有重复值
function hasDuplicate(arr, key) {
  // 提取属性值数组
  const values = arr.map((item) => item[key])
  // 利用 Set 去重，如果长度不一致，说明有重复
  return new Set(values).size !== arr.length
}

/** 上线通知单_治具需求清单治具查询 */
function handleQueryItem(keyword) {
  if (keyword) {
    const query = {
      pageNum: 1,
      pageSize: 10,
      sort: '',
      sortType: 'asc',
      keyword: keyword
    }
    setTimeout(() => {
      dictSimpleOnlineNoticeTicketItem(query).then((res) => {
        if (res.data) {
          state.options.item_options = res.data.result
        }
      })
    }, 200)
  }
}

/** 上线通知单_料号查询 */
function handleQueryPartName(keyword) {
  if (keyword) {
    const query = {
      pageNum: 1,
      pageSize: 10,
      sort: '',
      sortType: 'asc',
      keyword: keyword
    }
    setTimeout(() => {
      dictSimpleOnlineNoticeTicketPartName(query).then((res) => {
        if (res.data) {
          state.options.partName_options = res.data.result
        }
      })
    }, 200)
  }
}

//新上线料号值变更事件
function handlePartChange(value) {
  if (value) {
    const query = {
      partName: value
    }
    setTimeout(() => {
      listSimpleOnlineNoticeTicketItemsByPart(query).then((res) => {
        if (res.data) {
          let items = []
          res.data.forEach((it) => {
            items.push({ itemName: it.itemName, needQty: it.needQty })
          })
          itemList.value = items
        }
      })
    }, 200)
  } else {
    itemList.value = []
  }
}

defineExpose({ validFormData })
//组件挂载
onMounted(() => {
  initFormData()
})
</script>
