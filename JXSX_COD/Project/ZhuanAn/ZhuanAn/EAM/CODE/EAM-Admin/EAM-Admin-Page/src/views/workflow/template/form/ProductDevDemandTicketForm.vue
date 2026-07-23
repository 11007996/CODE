<template>
  <div>
    <el-form ref="formRef" :model="form" :rules="rules" :validate-on-rule-change="false" label-width="100px">
      <el-row :gutter="20">
        <el-col :sm="8" :lg="8" v-if="!fieldRules.partId?.hidden">
          <el-form-item label="料号" prop="partId">
            <el-input v-model="form.partId" placeholder="请输入料号" :readonly="!fieldRules.partId?.editable" />
          </el-form-item>
        </el-col>

        <el-col :sm="8" :lg="8" v-if="!fieldRules.customId?.hidden">
          <el-form-item label="客户ID" prop="customId">
            <el-input v-model="form.customId" placeholder="请输入客户ID" :readonly="!fieldRules.customId?.editable" />
          </el-form-item>
        </el-col>

        <el-col :sm="8" :lg="8" v-if="!fieldRules.orderQty?.hidden">
          <el-form-item label="预订单量" prop="orderQty">
            <el-input-number
              v-model.number="form.orderQty"
              :controls="false"
              placeholder="请输入预订单量"
              :readonly="!fieldRules.orderQty?.editable"
              class="fullWidth" />
          </el-form-item>
        </el-col>

        <el-col :sm="8" :lg="8" v-if="!fieldRules.needDate?.hidden">
          <el-form-item label="需求时间" prop="needDate">
            <el-date-picker
              v-model="form.needDate"
              type="datetime"
              :teleported="false"
              placeholder="选择日期时间"
              :readonly="!fieldRules.needDate?.editable"
              class="fullWidth"></el-date-picker>
          </el-form-item>
        </el-col>
      </el-row>

      <el-divider content-position="center">产品开发需求单_需求清单信息</el-divider>
      <el-row :gutter="10" class="mb8" v-if="fieldRules.itemList?.editable">
        <el-col :span="1.5">
          <el-button type="primary" icon="Plus" @click="handleAddProductDevDemandTicketItem">添加</el-button>
        </el-col>
        <el-col :span="1.5">
          <el-button type="danger" icon="Delete" @click="handleDeleteProductDevDemandTicketItem">删除</el-button>
        </el-col>
      </el-row>
      <el-table
        :data="productDevDemandTicketItemList"
        :row-class-name="rowProductDevDemandTicketItemIndex"
        @selection-change="handleProductDevDemandTicketItemSelectionChange"
        ref="ProductDevDemandTicketItemRef">
        <el-table-column type="selection" width="40" align="center" v-if="fieldRules.itemList?.editable" />
        <el-table-column label="序号" align="center" prop="index" width="50" />
        <el-table-column label="制程名称" align="center" prop="processName" min-width="160">
          <template #default="scope">
            <el-input v-model="scope.row.processName" :readonly="!fieldRules.itemList?.editable" />
          </template>
        </el-table-column>
        <el-table-column label="标准规格" align="center" prop="standardSpec" min-width="160">
          <template #default="scope">
            <el-input v-model="scope.row.standardSpec" :readonly="!fieldRules.itemList?.editable" />
          </template>
        </el-table-column>
        <el-table-column label="器材类型" prop="equipmentType" width="90">
          <template #default="scope">
            <el-select v-model="scope.row.equipmentType" :readonly="!fieldRules.itemList?.editable" class="fullWidth">
              <el-option v-for="item in options.equipment_type" :key="item.dictValue" :label="item.dictLabel" :value="item.dictValue"></el-option>
            </el-select>
          </template>
        </el-table-column>
        <el-table-column label="说明" align="center" prop="caption" min-width="120">
          <template #default="scope">
            <el-input v-model="scope.row.caption" :readonly="!fieldRules.itemList?.editable" />
          </template>
        </el-table-column>
        <el-table-column label="数量" align="center" prop="qty" width="90">
          <template #default="scope">
            <el-input-number v-model.number="scope.row.qty" :controls="false" :readonly="!fieldRules.itemList?.editable" class="fullWidth" />
          </template>
        </el-table-column>
        <el-table-column label="开发方式" prop="devMode" width="90">
          <template #default="scope">
            <el-select v-model="scope.row.devMode" :readonly="!fieldRules.itemList?.editable" class="fullWidth">
              <el-option
                v-for="item in options.equipment_dev_mode"
                :key="item.dictValue"
                :label="item.dictLabel"
                :value="item.dictValue"></el-option>
            </el-select>
          </template>
        </el-table-column>
        <el-table-column label="延用目标" align="center" prop="extendTargetId" min-width="200">
          <template #default="scope">
            <el-select
              v-model="scope.row.extendTargetId"
              placeholder="过滤方式：料号,治具"
              clearable
              filterable
              remote
              :readonly="!fieldRules.itemList?.editable"
              :remote-method="handleQueryFixture"
              @change="
                (val) => {
                  handleFixtureChange(val, scope.row)
                }
              "
              class="fullWidth">
              <template #header>
                <span>系列 / 名称</span>
              </template>
              <el-option v-for="item in options.fixture_options" :key="item.dictValue" :label="item.dictLabel" :value="item.dictValue"></el-option>
            </el-select>
          </template>
        </el-table-column>
        <el-table-column label="附件" align="center" prop="fileList" min-width="250">
          <template #header="header">
            <span>
              <el-tooltip placement="top" content="只能上传jpg/png/pdf/zip文件，且单个文件不超过5M">
                <el-icon> <questionFilled /> </el-icon>
              </el-tooltip>
              附件
            </span>
          </template>
          <template #default="scope">
            <el-upload
              :action="uploadFileUrl"
              :headers="headers"
              :data="uploadData"
              :on-exceed="handleExceed"
              :before-upload="beforeUpload"
              :on-error="handleUploadError"
              :on-success="(res, file, fileList) => handleUploadSuccess(res, file, fileList, scope.row)"
              :on-preview="handlePreview"
              :before-remove="beforeRemove"
              :on-remove="handleRemove"
              multiple
              :limit="limitFileCount"
              :accept="limitFileType"
              :file-list="scope.row.fileList"
              :readonly="!fieldRules.itemList?.editable">
              <el-button v-if="fieldRules.itemList?.editable" type="primary">点击上传</el-button>
              <!-- <div slot="tip" class="el-upload__tip"></div> -->
            </el-upload>
          </template>
        </el-table-column>
      </el-table>
    </el-form>
  </div>
</template>

<script setup>
import { dictFixtureBase } from '@/api/fixture/fixtureBase.js'
import useUserStore from '@/store/modules/user.js'
import { getToken } from '@/utils/auth'

const form = inject('form')
const fieldRules = inject('fieldRules')
const rules = inject('rules')
const { proxy } = getCurrentInstance()
const productDevDemandTicketItemList = ref([])
const checkedProductDevDemandTicketItem = ref([])

var dictParams = [{ dictType: 'equipment_type' }, { dictType: 'equipment_dev_mode' }]
//获取字典数据
proxy.getDicts(dictParams).then((response) => {
  response.data.forEach((element) => {
    state.options[element.dictType] = element.list
  })
})

const state = reactive({
  options: {
    //料号选项
    part_options: [],
    //器材类型
    equipment_type: [],
    //开发方式
    equipment_dev_mode: [],
    //治具
    fixture_options: []
  }
})
const { options } = toRefs(state)

/** 产品开发需求单_治具需求清单治具查询 */
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

//***********************表格交互****************************************** */
/** 产品开发需求单_治具需求清单治具变动事件 */
function handleFixtureChange(val, row) {
  const dict = state.options.fixture_options.find((item) => item.dictValue == val)
  productDevDemandTicketItemList.value[row.index - 1].extendTargetDesc = dict.dictLabel
}

function rowProductDevDemandTicketItemIndex({ row, rowIndex }) {
  row.index = rowIndex + 1
}

/** 产品开发需求单_治具需求清单添加按钮操作 */
function handleAddProductDevDemandTicketItem() {
  let obj = {}
  //下面的代码自己设置默认值
  //obj.needQty = null;
  productDevDemandTicketItemList.value.push(obj)
}

/** 复选框选中数据 */
function handleProductDevDemandTicketItemSelectionChange(selection) {
  checkedProductDevDemandTicketItem.value = selection.map((item) => item.index)
}

/** 产品开发需求单_治具需求清单删除按钮操作 */
function handleDeleteProductDevDemandTicketItem() {
  if (checkedProductDevDemandTicketItem.value.length == 0) {
    proxy.$modal.msgError('请先选择要删除的产品开发需求单_治具需求清单数据')
  } else {
    const ProductDevDemandTicketItems = productDevDemandTicketItemList.value
    const checkedProductDevDemandTicketItems = checkedProductDevDemandTicketItem.value
    productDevDemandTicketItemList.value = ProductDevDemandTicketItems.filter(function (item) {
      return checkedProductDevDemandTicketItems.indexOf(item.index) == -1
    })
  }
}

//**************************文件上传**************************** */

//限制个数
const limitFileCount = ref(5)
//限制类型
const limitFileType = ref('image/jpeg,image/png,application/pdf,application/zip')
//限制大小（5M）
const limitFileSize = ref(5)
//上传接口的其他表单数据
const factoryId = useUserStore().factoryId
const uploadData = ref({ fileDir: 'Upload/' + factoryId + ' /Drawing' })

const uploadFileUrl = ref(import.meta.env.VITE_APP_BASE_API + import.meta.env.VITE_APP_UPLOAD_URL) // 上传的图片服务器地址
const headers = ref({ Authorization: 'Bearer ' + getToken() })
//多个同时上传时，存放已上传的数据
const uploadList = ref([])
//同一批次的数量
const number = ref(0)

//限制：文件超出个数限制时的钩子
function handleExceed(files, fileList) {
  proxy.$modal.msgError(
    `当前限制选择 ${limitFileCount.value} 个文件，本次选择了 ${files.length} 个文件，共选择了 ${files.length + fileList.length} 个文件`
  )
}
//检查：文件上转前的钩子
function beforeUpload(file, fileList) {
  let errorMsg = null
  if (limitFileType.value.indexOf(file.type) < 0) {
    errorMsg = '文件类型不合规'
  }
  if (file.size / 1024 / 1024 > limitFileSize) {
    errorMsg = '文件大小不能超过' + limitFileSize + 'M'
  }
  if (errorMsg) {
    proxy.$modal.msgError(errorMsg)
    return false
  }
  number.value++
  proxy.$modal.loading('正在上传文件，请稍候...')
  return true
}

//预览：点击文件列表中已上传的文件时的钩子
function handlePreview(file) {
  window.open(file.url, '_blank')
}

//删除：文件列表移除文件时的钩子
function handleRemove(file, fileList) {}

//删除前： 删除文件之前的钩子，参数为上传的文件和文件列表，若返回 false 或者返回 Promise 且被 reject，则停止删除。
function beforeRemove(file, fileList) {
  // return proxy.$modal.confirm(`确定移除 ${file.name}？`)
}

// 上传失败
function handleUploadError(err) {
  proxy.$modal.msgError('上传失败')
  proxy.$modal.closeLoading()
}

// 上传成功回调
function handleUploadSuccess(response, file, fileList, row) {
  if (response.code != 200) {
    fileList.value = []
    proxy.$modal.msgError(`上传失败，原因:${response.msg}!`)
    proxy.$modal.closeLoading()
    return
  }

  const { fileName, realName, url, fileId } = response.data
  const tempFile = { fileId: fileId, fileName: fileName, name: realName, url: url }
  uploadList.value.push(tempFile)
  if (!row.fileList) {
    row.fileList = []
  }
  if (number.value > 0 && uploadList.value.length === number.value) {
    row.fileList = row.fileList.filter((f) => f.url !== undefined).concat(uploadList.value)
    uploadList.value = []
    number.value = 0
    proxy.$modal.closeLoading()
  }
}

//***************************表单处理********************************* */

//表单初始化
function initFormData() {
  //初始化表单数据
  if (form.value.itemList) {
    productDevDemandTicketItemList.value = JSON.parse(form.value.itemList)
    const opt = []
    productDevDemandTicketItemList.value.forEach((item) => {
      opt.push({ dictValue: item.extendTargetId, dictLabel: item.extendTargetDesc })
      if (item.fileList) item.fileList = JSON.parse(item.fileList)
    })
    state.options.fixture_options = opt
  }
}

//验证表单
async function validFormData() {
  return await proxy.$refs['formRef']
    .validate((valid) => {
      if (valid) {
        //治具清单，序列化
        if (fieldRules.value.itemList?.editable) {
          productDevDemandTicketItemList.value.forEach((item) => {
            if (item.fileList) item.fileList = JSON.stringify(item.fileList)
          })
          form.value.itemList = JSON.stringify(productDevDemandTicketItemList.value)
        }
        //工程师审批
        if (fieldRules.value.engineerId?.editable) {
          form.value.engineerId = useUserStore().userName
        }
        //工程师主管
        if (fieldRules.value.engineerLeaderId?.editable) {
          form.value.engineerLeaderId = useUserStore().userName
        }
        //上级领导审批
        if (fieldRules.value.leaderId?.editable) {
          form.value.leaderId = useUserStore().userName
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

<style lang="scss"></style>
