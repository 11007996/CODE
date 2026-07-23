<template>
  <!-- 流程业务表单_上线通知单 -->
  <!-- 业务表单数据 -->
  <div>
    <el-form ref="formRef" :model="form" :rules="rules" :validate-on-rule-change="false" label-width="100px">
      <el-row :gutter="20">
        <el-col :lg="8" :sm="12" v-if="!fieldRules.partId?.hidden">
          <el-form-item label="料号" prop="partId">
            <el-select
              v-model="form.partId"
              placeholder="请选择料号"
              clearable
              filterable
              remote
              :remote-method="handleQueryPart"
              @change="handlePartChange"
              :readonly="!fieldRules.partId?.editable"
              class="fullWidth">
              <el-option v-for="item in options.part_options" :key="item.dictValue" :label="item.dictLabel" :value="item.dictValue"></el-option>
            </el-select>
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

      <!-- 设备清单 -->
      <el-row v-if="!fieldRules.equipmentList?.hidden">
        <el-divider content-position="center">上线通知单_设备需求清单信息</el-divider>
        <el-row :gutter="10" class="mb8" v-if="fieldRules.equipmentList?.editable">
          <el-col :span="1.5">
            <el-button type="primary" icon="Plus" @click="handleAddEquipment">添加</el-button>
          </el-col>
          <el-col :span="1.5">
            <el-button type="danger" icon="Delete" @click="handleDeleteEquipment">删除</el-button>
          </el-col>
        </el-row>
        <el-table
          max-height="500"
          :data="equipmentList"
          :row-class-name="rowEquipmentIndex"
          @selection-change="handleEquipmentSelectionChange"
          ref="EquipmentRef">
          <el-table-column type="selection" width="40" align="center" v-if="fieldRules.equipmentList?.editable" />
          <el-table-column label="设备名称" align="center" prop="equipmentName" min-width="200">
            <template #default="scope">
              <el-select
                v-model="scope.row.equipmentName"
                placeholder="请选择设备"
                clearable
                filterable
                allow-create
                remote
                :readonly="!fieldRules.equipmentList?.editable"
                :remote-method="handleQueryEquipment"
                class="fullWidth">
                <el-option v-for="item in options.equipment_options" :label="item.equipmentName" :value="item.equipmentName"></el-option>
              </el-select>
            </template>
          </el-table-column>
          <el-table-column label="需求数量" align="center" prop="needQty" width="90">
            <template #default="scope">
              <el-input-number v-model="scope.row.needQty" :controls="false" :readonly="!fieldRules.equipmentList?.editable" class="fullWidth" />
            </template>
          </el-table-column>
        </el-table>
      </el-row>

      <!-- 治具清单 -->
      <el-row v-if="!fieldRules.fixtureList?.hidden">
        <el-divider content-position="center">上线通知单_治具需求清单信息</el-divider>
        <el-row :gutter="10" class="mb8" v-if="fieldRules.fixtureList?.editable">
          <el-col :span="1.5">
            <el-button type="primary" icon="Plus" @click="handleAddFixture">添加</el-button>
          </el-col>
          <el-col :span="1.5">
            <el-button type="danger" icon="Delete" @click="handleDeleteFixture">删除</el-button>
          </el-col>
        </el-row>
        <el-table
          max-height="500"
          :data="fixtureList"
          :row-class-name="rowFixtureIndex"
          @selection-change="handleFixtureSelectionChange"
          ref="OnlineNoticeTicketFixtureRef">
          <el-table-column type="selection" width="40" align="center" v-if="fieldRules.fixtureList?.editable" />
          <el-table-column label="治具" align="center" prop="fixtureName" min-width="120">
            <template #default="scope">
              <el-select
                v-model="scope.row.fixtureName"
                placeholder="请输入治具名称"
                clearable
                filterable
                allow-create
                remote
                :readonly="!fieldRules.fixtureList?.editable"
                :remote-method="handleQueryFixture"
                class="fullWidth">
                <template #header>
                  <span>系列 / 名称</span>
                </template>
                <el-option v-for="item in options.fixture_options" :label="item.dictLabel" :value="item.dictLabel"></el-option>
              </el-select>
            </template>
          </el-table-column>
          <el-table-column label="需求数量" align="center" prop="needQty" width="90">
            <template #default="scope">
              <el-input-number v-model="scope.row.needQty" :controls="false" :readonly="!fieldRules.fixtureList?.editable" class="fullWidth" />
            </template>
          </el-table-column>
        </el-table>
      </el-row>
    </el-form>
  </div>
</template>

<script setup name="onlinenoticeticketform">
import { dictPart } from '@/api/basic/part.js'
import { idleEquipmentBase } from '@/api/equipment/equipmentBase.js'
import { dictFixtureBase, idleFixtureBase } from '@/api/fixture/fixtureBase.js'
import useBasicStore from '@/store/modules/basic.js'

const form = inject('form')
const fieldRules = inject('fieldRules')
const rules = inject('rules')

const { proxy } = getCurrentInstance()
const equipmentList = ref([])
const checkedEquipments = ref([])
const fixtureList = ref([])
const checkedfixtures = ref([])

const state = reactive({
  options: {
    // 料号选项
    part_options: [],
    // 设备选项
    equipment_options: [],
    // 治具选项
    fixture_options: []
  }
})
const { options } = toRefs(state)

//料号查询
function handleQueryPart(keyword) {
  if (keyword) {
    const queryPartParams = {
      pageNum: 1,
      pageSize: 10,
      sort: '',
      sortType: 'asc',
      partNo: keyword
    }
    setTimeout(() => {
      dictPart(queryPartParams).then((res) => {
        state.options.part_options = res.data.result
      })
    }, 200)
  }
}

//料号变更事件，将治具带出来
function handlePartChange(partId) {
  if (partId) {
    const query = {
      pageNum: 1,
      pageSize: 10,
      sort: '',
      sortType: 'asc',
      fixtureName: null,
      partId: partId
    }
    setTimeout(() => {
      idleFixtureBase(query).then((res) => {
        if (res.data) {
          const fixtures = []
          res.data.result.forEach((item) => {
            fixtures.push({
              fixtureName: item.fixtureName,
              needQty: item.defaultQty
            })
          })
          if (!fixtureList || fixtureList.value.length <= 0) fixtureList.value = fixtures
        }
      })
    }, 200)
  }
}

/******************************* 设备清单处理 *********************************** */
/** 上线通知单_治具需求清单治具查询 */
function handleQueryEquipment(keyword) {
  if (keyword) {
    const query = {
      equipmentName: keyword
    }
    setTimeout(() => {
      idleEquipmentBase(query).then((res) => {
        if (res.data) {
          state.options.equipment_options = res.data.result
        }
      })
    }, 200)
  }
}

function rowEquipmentIndex({ row, rowIndex }) {
  row.index = rowIndex + 1
}

/** 上线通知单_治具需求清单添加按钮操作 */
function handleAddEquipment() {
  let obj = {}
  equipmentList.value.push(obj)
}

/** 复选框选中数据 */
function handleEquipmentSelectionChange(selection) {
  checkedEquipments.value = selection.map((item) => item.index)
}

/** 上线通知单_治具需求清单删除按钮操作 */
function handleDeleteEquipment() {
  if (checkedEquipments.value.length == 0) {
    proxy.$modal.msgError('请先选择要删除的上线通知单_治具需求清单数据')
  } else {
    equipmentList.value = equipmentList.value.filter(function (item) {
      return checkedEquipments.value.indexOf(item.index) == -1
    })
  }
}

/******************************* 治具清单处理 *********************************** */
/** 上线通知单_治具需求清单治具查询 */
function handleQueryFixture(keyword) {
  if (keyword) {
    const query = {
      pageNum: 1,
      pageSize: 10,
      sort: '',
      sortType: 'asc',
      keyword: keyword
    }
    if (keyword.indexOf(',') >= 0) {
      const kv = keyword.split(',')
      query.series = kv[0]
      query.fixtureName = kv[1]
      query.keyword = null
    }
    setTimeout(() => {
      dictFixtureBase(query).then((res) => {
        if (res.data) {
          state.options.fixture_options = res.data.result
        }
      })
    }, 200)
  }
}

function rowFixtureIndex({ row, rowIndex }) {
  row.index = rowIndex + 1
}

/** 上线通知单_治具需求清单添加按钮操作 */
function handleAddFixture() {
  let obj = {}
  fixtureList.value.push(obj)
}

/** 复选框选中数据 */
function handleFixtureSelectionChange(selection) {
  checkedfixtures.value = selection.map((item) => item.index)
}

/** 上线通知单_治具需求清单删除按钮操作 */
function handleDeleteFixture() {
  if (checkedfixtures.value.length == 0) {
    proxy.$modal.msgError('请先选择要删除的上线通知单_治具需求清单数据')
  } else {
    fixtureList.value = fixtureList.value.filter(function (item) {
      return checkedfixtures.value.indexOf(item.index) == -1
    })
  }
}

//表单初始化
function initFormData() {
  //初始化表单数据
  //设备清单反序列化
  if (form.value.equipmentList) {
    equipmentList.value = JSON.parse(form.value.equipmentList)
  }
  //治具清单反序列化
  if (form.value.fixtureList) {
    fixtureList.value = JSON.parse(form.value.fixtureList)
  }
  // 料号
  if (form.value.partId) {
    const queryPartParams = {
      pageNum: 1,
      pageSize: 10,
      sort: '',
      sortType: 'asc',
      partId: form.value.partId
    }
    dictPart(queryPartParams).then((res) => {
      state.options.part_options = res.data.result
    })
  }
}

//验证表单
async function validFormData() {
  return await proxy.$refs['formRef']
    .validate((valid) => {
      if (valid) {
        //设备清单，序列化
        if (fieldRules.value.equipmentList?.editable) {
          form.value.equipmentList = JSON.stringify(equipmentList.value)
        }
        //治具清单，序列化
        if (fieldRules.value.fixtureList?.editable) {
          form.value.fixtureList = JSON.stringify(fixtureList.value)
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
