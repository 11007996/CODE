<template>
  <div>
    <el-form ref="formRef" :model="form" :rules="rules" :validate-on-rule-change="false" label-width="100px">
      <el-row :gutter="20">
        <el-col :sm="8" :lg="8" v-if="!fieldRules.createMode?.hidden">
          <el-form-item label="创建模式" prop="createMode">
            <el-select v-model="form.createMode" :readonly="!fieldRules.createMode?.editable" class="fullWidth">
              <el-option
                v-for="item in options.measure_create_mode"
                :key="item.dictValue"
                :label="item.dictLabel"
                :value="item.dictValue"></el-option>
            </el-select>
          </el-form-item>
        </el-col>

        <el-col :lg="8" :sm="8" v-if="!fieldRules.partId?.hidden">
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
              <el-option
                v-for="item in options.part_options"
                :key="item.dictValue"
                :label="item.dictLabel"
                :value="Number(item.dictValue)"></el-option>
            </el-select>
          </el-form-item>
        </el-col>

        <el-col :sm="8" :lg="8" v-if="!fieldRules.fixtureId?.hidden">
          <el-form-item label="治具" prop="fixtureId">
            <el-select
              v-model="form.fixtureId"
              clearable
              filterable
              remote
              :readonly="!fieldRules.fixtureId?.editable"
              :remote-method="handleQueryFixture"
              placeholder="请输入治具名称"
              @change="handleFixtureChange"
              class="fullWidth">
              <template #header>
                <span>系列 / 名称</span>
              </template>
              <el-option v-for="item in options.fixture_options" :key="item.dictValue" :label="item.dictLabel" :value="item.dictValue"></el-option>
            </el-select>
          </el-form-item>
        </el-col>

        <el-col :sm="8" :lg="8" v-if="!fieldRules.drawingNo?.hidden">
          <el-form-item label="治具图号" prop="drawingNo">
            <el-input v-model="form.drawingNo" :readonly="!fieldRules.drawingNo?.editable" />
          </el-form-item>
        </el-col>

        <el-col :sm="8" :lg="8" v-if="!fieldRules.fixtureNoDesc?.hidden">
          <el-form-item label="治具编号" prop="fixtureNoDesc">
            <el-input v-model="form.fixtureNoDesc" :readonly="!fieldRules.fixtureNoDesc?.editable" />
          </el-form-item>
        </el-col>

        <el-col :sm="8" :lg="8" v-if="!fieldRules.version?.hidden">
          <el-form-item label="版本" prop="version">
            <el-input v-model="form.version" :readonly="!fieldRules.version?.editable" />
          </el-form-item>
        </el-col>

        <el-col :sm="24" :lg="24" v-if="!fieldRules.fileList?.hidden">
          <el-form-item label="治具图纸" prop="fileList">
            <el-link type="primary" class="fileName" v-for="item in fileList" :href="item.url" :download="item.name" target="_blank">{{
              item.name
            }}</el-link>
          </el-form-item>
        </el-col>
      </el-row>

      <!-- 尺寸测量评估 -->
      <el-divider content-position="center">治具尺寸量测单_尺寸测量评估</el-divider>
      <el-row :gutter="10" class="mb8" v-if="fieldRules.itemResult?.editable">
        <el-col :span="1.5">
          <el-button type="primary" icon="Plus" @click="handleAddSizeMeasureTicketItem">添加</el-button>
        </el-col>
        <el-col :span="1.5">
          <el-button type="danger" icon="Delete" @click="handleDeleteSizeMeasureTicketItem">删除</el-button>
        </el-col>
        <el-col :span="1.5">
          <el-button type="success" icon="edit" @click="handleItemDefine">测试项</el-button>
        </el-col>
      </el-row>
      <el-table
        :data="sizeMeasureTicketItemList"
        :row-class-name="rowSizeMeasureTicketItemIndex"
        @selection-change="handleSizeMeasureTicketItemSelectionChange"
        ref="SizeMeasureTicketItemRef">
        <el-table-column type="selection" width="40" align="center" v-if="fieldRules.itemResult?.editable" />
        <el-table-column label="序号" align="center" prop="index" width="50" />
        <el-table-column label="治具编号" align="center" prop="fixtureNo" width="90">
          <template #default="scope">
            <el-input v-model="scope.row.fixtureNo" :readonly="!fieldRules.itemResult?.editable" />
          </template>
        </el-table-column>
        <el-table-column
          v-for="(item, index) in sizeMeasureTicketItemDefineList"
          :label="item.itemName"
          align="center"
          :prop="item.index"
          width="90">
          <!-- 测试项目:自定义表头 -->
          <template #header="scope">
            <el-row v-if="item.itemName" class="itemName">
              <div>{{ item.itemName }}</div>
            </el-row>
            <el-row :gutter="1" type="flex" justify="center">
              <div class="st">
                <div>{{ item.standard }}</div>
              </div>
              <div class="pc">
                <div>+{{ item.positive }}</div>
                <div>-{{ item.caption }}</div>
              </div>
            </el-row>
          </template>
          <template #default="scope">
            <el-input-number
              :controls="false"
              v-model.number="scope.row[item.orderNo]"
              @change="(v) => handleItemValueChange(v, scope.row)"
              :readonly="!fieldRules.itemResult?.editable"
              class="fullWidth" />
          </template>
        </el-table-column>
        <el-table-column label="尺寸值判定" align="center" prop="result" width="150">
          <template #default="scope">
            <el-radio-group v-model="scope.row.result" :readonly="!fieldRules.itemResult?.editable">
              <el-radio v-for="item in options.judgment_result" :key="item.dictValue" :value="item.dictValue">
                {{ item.dictLabel }}
              </el-radio>
            </el-radio-group>
          </template>
        </el-table-column>
      </el-table>

      <!-- 外观、结构、使用效果评估 -->
      <el-divider content-position="center">治具尺寸量测单_外观、结构、使用效果评估</el-divider>
      <el-row :gutter="10" class="mb8" v-if="false">
        <el-col :span="1.5">
          <el-button type="primary" icon="Plus" @click="handleAddSizeMeasureTicketItemOther">添加</el-button>
        </el-col>
        <el-col :span="1.5">
          <el-button type="danger" icon="Delete" @click="handleDeleteSizeMeasureTicketItemOther">删除</el-button>
        </el-col>
      </el-row>
      <el-table
        :data="sizeMeasureTicketItemOtherList"
        :row-class-name="rowSizeMeasureTicketItemOtherIndex"
        @selection-change="handleSizeMeasureTicketItemOtherSelectionChange"
        ref="SizeMeasureTicketItemOtherRef">
        <el-table-column type="selection" width="40" align="center" v-if="false" />
        <el-table-column label="序号" align="center" prop="orderNo" width="50" />
        <el-table-column label="项目" align="center" prop="itemDesc" min-width="250" />
        <el-table-column label="自动化评估" align="center" prop="automationResult" min-width="160">
          <template #default="scope">
            <el-input v-model="scope.row.automationResult" :readonly="!fieldRules.engineerId?.editable" />
          </template>
        </el-table-column>
        <el-table-column label="品管评估" align="center" prop="qcResult" min-width="160">
          <template #default="scope">
            <el-input v-model="scope.row.qcResult" :readonly="!fieldRules.qcId?.editable" />
          </template>
        </el-table-column>
      </el-table>
    </el-form>

    <!-- 测试项目定义弹窗 -->
    <el-dialog :title="title" :lock-scroll="false" v-model="open">
      <el-form ref="itemDefineFormRef" :model="itemDefineForm" :rules="itemDefineFormRules" label-width="100px">
        <el-row :gutter="10" class="mb8" v-if="fieldRules.itemResult?.editable">
          <el-col :span="1.5">
            <el-button type="primary" icon="Plus" @click="handleAddItemDefine">添加</el-button>
          </el-col>
          <el-col :span="1.5">
            <el-button type="danger" icon="Delete" @click="handleDeleteItemDefine">删除</el-button>
          </el-col>
        </el-row>
        <el-table
          :data="itemDefineForm.itemDefineList"
          :row-class-name="rowItemDefineIndex"
          @selection-change="handleItemDefineSelectionChange"
          ref="ItemDefineRef">
          <el-table-column type="selection" width="40" align="center" v-if="fieldRules.itemResult?.editable" />
          <el-table-column label="序号" align="center" prop="orderNo" width="50" />
          <el-table-column label="项目名(非必填)" align="center" prop="itemName" min-width="120">
            <template #default="scope">
              <el-form-item label-width="1">
                <el-input v-model="scope.row.itemName" :readonly="!fieldRules.itemResult?.editable" />
              </el-form-item>
            </template>
          </el-table-column>
          <el-table-column label="标准值" align="center" prop="standard" width="90">
            <template #default="scope">
              <el-form-item :prop="'itemDefineList.' + scope.$index + '.standard'" :rules="itemDefineFormRules.standard" label-width="1">
                <el-input-number
                  v-model.number="scope.row.standard"
                  :controls="false"
                  :readonly="!fieldRules.itemResult?.editable"
                  class="fullWidth" />
              </el-form-item>
            </template>
          </el-table-column>
          <el-table-column label="正公差" align="center" prop="positive" width="90">
            <template #default="scope">
              <el-form-item :prop="'itemDefineList.' + scope.$index + '.positive'" :rules="itemDefineFormRules.positive" label-width="1">
                <el-input-number
                  v-model.number="scope.row.positive"
                  :controls="false"
                  :readonly="!fieldRules.itemResult?.editable"
                  class="fullWidth" />
              </el-form-item>
            </template>
          </el-table-column>
          <el-table-column label="负公差" align="center" prop="caption" width="90">
            <template #default="scope">
              <el-form-item :prop="'itemDefineList.' + scope.$index + '.caption'" :rules="itemDefineFormRules.caption" label-width="1">
                <el-input-number
                  v-model.number="scope.row.caption"
                  :controls="false"
                  :readonly="!fieldRules.itemResult?.editable"
                  class="fullWidth" />
              </el-form-item>
            </template>
          </el-table-column>
        </el-table>
      </el-form>
      <template #footer>
        <el-button text @click="cancel">{{ $t('btn.cancel') }}</el-button>
        <el-button type="primary" @click="submitForm">{{ $t('btn.submit') }}</el-button>
      </template>
    </el-dialog>
  </div>
</template>

<script setup>
import useUserStore from '@/store/modules/user.js'
import { dictFixtureBase } from '@/api/fixture/fixtureBase.js'
import { listFixtureFile } from '@/api/fixture/fixtureFile.js'
import { dictPart } from '@/api/basic/part.js'

const form = inject('form')
const fieldRules = inject('fieldRules')
const rules = inject('rules')
const { proxy } = getCurrentInstance()
const formRef = ref()
const fileList = ref([])

var dictParams = [{ dictType: 'measure_create_mode' }, { dictType: 'judgment_result' }]
//获取字典数据
proxy.getDicts(dictParams).then((response) => {
  response.data.forEach((element) => {
    state.options[element.dictType] = element.list
  })
})

const state = reactive({
  itemDefine: [],
  options: {
    //料号选项
    part_options: [],
    //器材类型
    measure_create_mode: [],
    //治具
    fixture_options: [],
    //判定结果
    judgment_result: []
  }
})
const { options, itemDefine } = toRefs(state)

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

// 料号变动
function handlePartChange(val) {
  form.value.fixtureId = ''
  form.value.fixtureName = ''
  state.options.fixture_options = []
  fileList.value = []
}

/** 治具尺寸量测单_测量记录治具查询 */
function handleQueryFixture(keyword) {
  if (!form.value.partId) {
    proxy.$modal.msgError('请先选择料号')
    form.value.fixtureName = null
    return
  }
  if (keyword) {
    const query = {
      pageNum: 1,
      pageSize: 10,
      sort: '',
      sortType: 'asc',
      partId: form.value.partId,
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

/** 治具尺寸量测单_测量记录治具变动事件 */
function handleFixtureChange(val) {
  form.value.fixtureName = ''
  fileList.value = []
  const dict = state.options.fixture_options.find((item) => item.dictValue == val)
  if (dict) {
    //治具名
    form.value.fixtureName = dict.dictLabel
    //查询治具文件
    const queryParams = {
      pageNum: 1,
      pageSize: 100,
      fixtureId: dict.dictValue
    }
    listFixtureFile(queryParams).then((res) => {
      if (res.code == 200 && res.data.result) {
        res.data.result.forEach((item) => {
          fileList.value.push({ fileId: item.id, name: item.realName, url: item.accessUrl })
        })
      }
    })
  }
}

/***********************治具尺寸量测单_ 尺寸评估***********/
const sizeMeasureTicketItemList = ref([])
const checkedSizeMeasureTicketItem = ref([])
function rowSizeMeasureTicketItemIndex({ row, rowIndex }) {
  row.index = rowIndex + 1
}

/** 治具尺寸量测单_测量记录添加按钮操作 */
function handleAddSizeMeasureTicketItem() {
  let obj = {}
  //下面的代码自己设置默认值
  //obj.needQty = null;
  sizeMeasureTicketItemList.value.push(obj)
}

/** 复选框选中数据 */
function handleSizeMeasureTicketItemSelectionChange(selection) {
  checkedSizeMeasureTicketItem.value = selection.map((item) => item.index)
}

/** 治具尺寸量测单_测量记录删除按钮操作 */
function handleDeleteSizeMeasureTicketItem() {
  if (checkedSizeMeasureTicketItem.value.length == 0) {
    proxy.$modal.msgError('请先选择要删除的治具尺寸量测单_测量记录数据')
  } else {
    const SizeMeasureTicketItems = sizeMeasureTicketItemList.value
    const checkedSizeMeasureTicketItems = checkedSizeMeasureTicketItem.value
    sizeMeasureTicketItemList.value = SizeMeasureTicketItems.filter(function (item) {
      return checkedSizeMeasureTicketItems.indexOf(item.index) == -1
    })
  }
}

function handleItemValueChange(v, row) {
  //判定结果
  var result = true
  var itemDefine = {}
  //遍历行数据对象的属性
  for (let key in row) {
    //当行属性为测试项目时
    if (key != 'index' && key != 'fixtureNo' && key != 'result') {
      itemDefine = sizeMeasureTicketItemDefineList.value.find((item) => item.orderNo == key)
      if (itemDefine) {
        //当前输入的数值 大于正公差或小于负公差 判定结果false:NG
        if (row[key] > itemDefine.standard + itemDefine.positive || row[key] < itemDefine.standard - itemDefine.caption) {
          result = false
          break
        }
      }
    }
  }
  row.result = result ? 'OK' : 'NG'
}

/***********************治具尺寸量测单_ 外观、结构、使用效果评估***********/
const sizeMeasureTicketItemOtherList = ref([
  { orderNo: 1, itemDesc: '治具子母板是否干涉、组装是否到位' },
  { orderNo: 2, itemDesc: '产品与治具适配是否到位，校角是否都有倒角，是否有损伤产品' },
  { orderNo: 3, itemDesc: '治具子母板是否防呆' },
  { orderNo: 4, itemDesc: '外观是否有毛刺、锐角、生绣等不良' },
  { orderNo: 5, itemDesc: '产品实际制作效果确认，试做良率(记录试做良品数和投入数)' },
  { orderNo: 6, itemDesc: '依实际产品制作确认各尺寸是否符合生产' }
])
const checkedSizeMeasureTicketItemOther = ref([])
const defaultOtherItems = ref(['', '', '', '', '', ''])
function rowSizeMeasureTicketItemOtherIndex({ row, rowIndex }) {
  row.index = rowIndex + 1
}

/** 治具尺寸量测单_测量记录添加按钮操作 */
function handleAddSizeMeasureTicketItemOther() {
  let obj = {}
  //下面的代码自己设置默认值
  //obj.needQty = null;
  sizeMeasureTicketItemOtherList.value.push(obj)
}

/** 复选框选中数据 */
function handleSizeMeasureTicketItemOtherSelectionChange(selection) {
  checkedSizeMeasureTicketItemOther.value = selection.map((item) => item.index)
}

/** 治具尺寸量测单_测量记录删除按钮操作 */
function handleDeleteSizeMeasureTicketItemOther() {
  if (checkedSizeMeasureTicketItem.value.length == 0) {
    proxy.$modal.msgError('请先选择要删除的治具尺寸量测单_测量记录数据')
  } else {
    const SizeMeasureTicketItemOthers = sizeMeasureTicketItemOtherList.value
    const checkedSizeMeasureTicketItemOthers = checkedSizeMeasureTicketItemOther.value
    sizeMeasureTicketItemOtherList.value = SizeMeasureTicketItemOthers.filter(function (item) {
      return checkedSizeMeasureTicketItemOthers.indexOf(item.index) == -1
    })
  }
}

/***********************治具尺寸量测单_尺寸项目定义弹窗(测试项目定义) ***********/
const open = ref(false)
const title = ref('')
const sizeMeasureTicketItemDefineList = ref([])
const checkedItemDefine = ref([])
const itemDefineFormRef = ref()
const itemDefineForm = reactive({
  itemDefineList: []
})

const itemDefineFormRules = ref({
  itemName: [{ required: true, message: '测试项目名称不能为空', trigger: 'blur' }],
  standard: [
    { required: true, message: '标准值不能为空', trigger: 'blur', type: 'number' },
    { pattern: /^(0|([1-9][0-9]*))(\.[\d]+)?$/, message: '标准值不能小于0', trigger: 'blur' }
  ],
  positive: [
    { required: true, message: '正公差不能为空', trigger: 'blur', type: 'number' },
    { pattern: /^(0|([1-9][0-9]*))(\.[\d]+)?$/, message: '正公差不能小于0', trigger: 'blur' }
  ],
  caption: [
    { required: true, message: '负公差不能为空', trigger: 'blur', type: 'number' },
    { pattern: /^(0|([1-9][0-9]*))(\.[\d]+)?$/, message: '负公差不能小于0', trigger: 'blur' }
  ]
})
/** 治具尺寸量测单_测量记录添加测试项目按钮操作 */
function handleItemDefine() {
  open.value = true
  title.value = '测量项目定义'
  itemDefineForm.itemDefineList = [...sizeMeasureTicketItemDefineList.value]
}

// 关闭dialog
function cancel() {
  open.value = false
  reset()
}

function reset() {
  itemDefineForm.itemDefineList = []
  proxy.resetForm('itemDefineFormRef')
}

//表单提交
function submitForm() {
  proxy.$refs['itemDefineFormRef'].validate((valid) => {
    if (valid) {
      itemDefineForm.itemDefineList.forEach((item) => {
        item.orderNo = item.orderNo + ''
      })
      sizeMeasureTicketItemDefineList.value = [...itemDefineForm.itemDefineList]
      open.value = false
    }
  })
}

function rowItemDefineIndex({ row, rowIndex }) {
  row.orderNo = rowIndex + 1
}

/** 治具尺寸量测单_测量记录测试项目添加按钮操作 */
function handleAddItemDefine() {
  let obj = {}
  //下面的代码自己设置默认值
  itemDefineForm.itemDefineList.push(obj)
}

/** 复选框选中数据 */
function handleItemDefineSelectionChange(selection) {
  checkedItemDefine.value = selection.map((item) => item.orderNo)
}

/** 治具尺寸量测单_测量记录删除按钮操作 */
function handleDeleteItemDefine() {
  if (checkedItemDefine.value.length == 0) {
    proxy.$modal.msgError('请先选择要删除的治具尺寸量测单_测量项目')
  } else {
    const itemsDefine = itemDefineForm.itemDefineList
    const checkedItemsDefine = checkedItemDefine.value
    itemDefineForm.itemDefineList = itemsDefine.filter(function (item) {
      return checkedItemsDefine.indexOf(item.orderNo) == -1
    })
  }
}

/**********************业务表单数据处理****************************** */
//表单初始化
function initFormData() {
  //初始化表单数据
  if (form.value.fixtureId) {
    options.value.fixture_options.push({ dictValue: form.value.fixtureId, dictLabel: form.value.fixtureName })
  }
  if (form.value.fileList) {
    fileList.value = JSON.parse(form.value.fileList)
  }
  if (form.value.itemResult) {
    sizeMeasureTicketItemList.value = JSON.parse(form.value.itemResult)
  }
  if (form.value.itemDefine) {
    sizeMeasureTicketItemDefineList.value = JSON.parse(form.value.itemDefine)
  }
  if (form.value.itemOther) {
    sizeMeasureTicketItemOtherList.value = JSON.parse(form.value.itemOther)
  }
}
//验证表单
async function validFormData() {
  return await proxy.$refs['formRef']
    .validate((valid) => {
      if (valid) {
        if (fieldRules.value.fileList?.editable) {
          form.value.fileList = JSON.stringify(fileList.value)
        }
        //测量项目定义
        if (fieldRules.value.itemResult?.editable) {
          form.value.itemResult = JSON.stringify(sizeMeasureTicketItemList.value)
        }
        //尺寸测量评估
        if (fieldRules.value.itemDefine?.editable) {
          form.value.itemDefine = JSON.stringify(sizeMeasureTicketItemDefineList.value)
        }
        //其他评估
        if (fieldRules.value.itemOther?.editable) {
          form.value.itemOther = JSON.stringify(sizeMeasureTicketItemOtherList.value)
        }

        //工程师审批
        if (fieldRules.value.engineerId?.editable) {
          form.value.engineerId = useUserStore().userName
        }
        //工程师主管
        if (fieldRules.value.engineerLeaderId?.editable) {
          form.value.engineerLeaderId = useUserStore().userName
        }
        //品管
        if (fieldRules.value.qcId?.editable) {
          form.value.qcId = useUserStore().userName
        }
        //品管主管
        if (fieldRules.value.qcLeaderId?.editable) {
          form.value.qcLeaderId = useUserStore().userName
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
//数据更新前
onBeforeUpdate(() => {
  initFormData()
})
</script>

<style lang="scss">
// 列头：测试项名称
.itemName div {
  margin: auto;
}
// 列头：标准值
.st div {
  font-size: 18px;
  line-height: 18px;
  text-align: right;
  text-wrap: nowrap;
}
// 列头：正负公差值
.pc div {
  font-size: 10px;
  line-height: 10px;
  text-align: left;
  text-wrap: nowrap;
}
.el-table--default .cell {
  padding: 0 1px;
}

.fileName {
  margin-right: 20px;
  background-color: bisque;
  padding: 0 5px;
}
</style>
