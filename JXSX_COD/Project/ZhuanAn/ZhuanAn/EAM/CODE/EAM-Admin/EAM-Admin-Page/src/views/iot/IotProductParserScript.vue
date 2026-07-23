<!--
 * @Descripttion: (数据解析脚本/IOT_Product_Parser_Script)
 * @Author: (admin)
 * @Date: (2026-01-07)
-->
<template>
  <div>
    <el-form :model="queryParams" label-position="right" inline ref="queryRef" v-show="showSearch" @submit.prevent>
      <!-- <el-form-item label="产品ID" prop="productId">
        <el-select clearable v-model="queryParams.productId" placeholder="请选择产品ID">
          <el-option v-for="item in options.productIdOptions" :key="item.dictValue" :label="item.dictLabel" :value="item.dictValue">
            <span class="fl">{{ item.dictLabel }}</span>
            <span class="fr" style="color: var(--el-text-color-secondary)">{{ item.dictValue }}</span>
          </el-option>
        </el-select>
      </el-form-item>
      <el-form-item>
        <el-button icon="search" type="primary" @click="handleQuery">{{ $t('btn.search') }}</el-button>
        <el-button icon="refresh" @click="resetQuery">{{ $t('btn.reset') }}</el-button>
      </el-form-item>-->
    </el-form>
    <!-- 工具区域 -->
    <el-row :gutter="15" class="mb10">
      <el-col :span="1.5">
        <el-button type="primary" v-hasPermi="['iot:product:parser:script:add']" plain icon="plus" @click="handleAdd">
          {{ $t('btn.add') }}
        </el-button>
        <el-button
          type="success"
          icon="edit"
          title="编辑"
          v-hasPermi="['iot:product:parser:script:edit']"
          @click="handleUpdate(props.productId)"></el-button>
        <el-button
          type="danger"
          icon="delete"
          title="删除"
          v-hasPermi="['iot:product:parser:script:delete']"
          @click="handleDelete(props.productId)"></el-button>
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
      @sort-change="sortChange">
      <el-table-column prop="productId" label="产品ID" align="center" width="90" v-if="columns.showColumn('productId')" />
      <el-table-column prop="scriptCode" label="脚本代码" v-if="columns.showColumn('scriptCode')">
        <template #default="scope">
          <MdPreview show-code-row-number editorId="id1" :modelValue="'```JavaScript\n' + scope.row.scriptCode + '\n```'" />
        </template>
      </el-table-column>
      <el-table-column prop="enabled" label="是否可用" align="center" width="90" v-if="columns.showColumn('enabled')">
        <template #default="scope">
          <el-switch v-model="scope.row.enabled" active-text="是" inactive-text="否" inline-prompt disabled />
        </template>
      </el-table-column>
      <el-table-column prop="createBy" label="创建人" align="center" width="90" v-if="columns.showColumn('createBy')" />
      <el-table-column prop="createTime" label="创建时间" width="160" v-if="columns.showColumn('createTime')" />
      <el-table-column prop="updateBy" label="更新人" align="center" width="90" v-if="columns.showColumn('updateBy')" />
      <el-table-column prop="updateTime" label="更新时间" width="160" v-if="columns.showColumn('updateTime')" />
      <!-- <el-table-column label="操作" width="120">
        <template #default="scope"> </template>
      </el-table-column> -->
    </el-table>
    <!-- <pagination :total="total" v-model:page="queryParams.pageNum" v-model:limit="queryParams.pageSize" @pagination="getList" /> -->

    <!-- 添加或修改数据解析脚本对话框 -->
    <el-dialog :title="title" :lock-scroll="false" v-model="open" width="90%">
      <el-form ref="formRef" :model="form" :rules="rules" label-width="100px">
        <el-row :gutter="20">
          <!-- <el-col :lg="12">
            <el-form-item label="产品ID" prop="productId">
              <el-input v-model.number="form.productId" placeholder="请输入产品ID" :disabled="opertype != 1" />
            </el-form-item>
          </el-col> -->

          <el-col :lg="12">
            <el-form-item label="是否可用" prop="enabled">
              <el-switch v-model="form.enabled" active-text="是" inactive-text="否" inline-prompt />
            </el-form-item>
          </el-col>

          <!-- <el-col :lg="24">
            <el-form-item label="脚本代码" prop="scriptCode">
               <el-input type="textarea" v-model="form.scriptCode" placeholder="请输入脚本代码" height="800" />
            </el-form-item>
          </el-col> -->
          <el-col :lg="24">
            <MdEditor editorId="id2" v-model="form.scriptCode" :toolbars="[]" :preview="false" :footers="[]" style="overflow-y: scroll" />
          </el-col>
        </el-row>
      </el-form>

      <template #footer v-if="opertype != 3">
        <el-button text @click="cancel">{{ $t('btn.cancel') }}</el-button>
        <el-button type="primary" @click="submitForm">{{ $t('btn.submit') }}</el-button>
      </template>
    </el-dialog>
  </div>
</template>

<script setup name="iotproductparserscript">
import {
  listIotProductParserScript,
  addIotProductParserScript,
  delIotProductParserScript,
  updateIotProductParserScript,
  getIotProductParserScript
} from '@/api/iot/iotProductParserScript.js'
import { MdPreview, MdEditor } from 'md-editor-v3'
import 'md-editor-v3/lib/preview.css'
const props = defineProps({
  productId: Number
})
const { proxy } = getCurrentInstance()
const ids = ref([])
const loading = ref(false)
const showSearch = ref(false)
const queryParams = reactive({
  pageNum: 1,
  pageSize: 10,
  sort: '',
  sortType: 'asc',
  productId: undefined
})
const columns = ref([
  { visible: false, prop: 'productId', label: '产品ID' },
  { visible: true, prop: 'scriptCode', label: '脚本代码' },
  { visible: false, prop: 'enabled', label: '是否可用' },
  { visible: false, prop: 'createBy', label: '创建人' },
  { visible: false, prop: 'createTime', label: '创建时间' },
  { visible: false, prop: 'updateBy', label: '更新人' },
  { visible: false, prop: 'updateTime', label: '更新时间' }
])
const total = ref(0)
const dataList = ref([])
const queryRef = ref()
const defaultTime = ref([new Date(2000, 1, 1, 0, 0, 0), new Date(2000, 2, 1, 23, 59, 59)])

function getList() {
  loading.value = true
  queryParams.productId = props.productId
  listIotProductParserScript(queryParams).then((res) => {
    const { code, data } = res
    if (code == 200) {
      dataList.value = data.result
      total.value = data.totalNum
      loading.value = false
    }
  })
}

//监听属性变化
watch(props, (val) => {
  handleQuery()
})

// 查询
function handleQuery() {
  queryParams.pageNum = 1
  getList()
}

// 重置查询操作
function resetQuery() {
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
    productId: [{ required: true, message: '产品ID不能为空', trigger: 'change', type: 'number' }],
    scriptCode: [{ required: true, message: '脚本代码不能为空', trigger: 'blur' }]
  },
  options: {
    // 产品ID 选项列表 格式 eg:{ dictLabel: '标签', dictValue: '0'}
    productIdOptions: []
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
    productId: null,
    scriptCode: null,
    enabled: null,
    createBy: null,
    createTime: null,
    updateBy: null,
    updateTime: null
  }
  proxy.resetForm('formRef')
}

// 添加按钮操作
function handleAdd() {
  reset()
  form.value.scriptCode = '```JavaScript\n' + codeTemplate + '\n```'
  open.value = true
  title.value = '添加数据解析脚本'
  opertype.value = 1
}
// 修改按钮操作
function handleUpdate(productId) {
  reset()
  //const id = row.productId || ids.value
  getIotProductParserScript(productId).then((res) => {
    const { code, data } = res
    if (code == 200) {
      open.value = true
      title.value = '修改数据解析脚本'
      opertype.value = 2
      data.scriptCode = '```JavaScript\n' + data.scriptCode + '\n```'

      form.value = {
        ...data
      }
    }
  })
}

// 添加&修改 表单提交
function submitForm() {
  proxy.$refs['formRef'].validate((valid) => {
    if (valid) {
      const formData = { ...form.value }
      formData.scriptCode = removeScriptLang(formData.scriptCode)
      if (form.value.productId != undefined && opertype.value === 2) {
        updateIotProductParserScript(formData).then((res) => {
          proxy.$modal.msgSuccess('修改成功')
          open.value = false
          getList()
        })
      } else {
        formData.productId = props.productId
        addIotProductParserScript(formData).then((res) => {
          proxy.$modal.msgSuccess('新增成功')
          open.value = false
          getList()
        })
      }
    }
  })
}

// 删除按钮操作
function handleDelete(productId) {
  //const Ids = row.productId || ids.value

  proxy
    .$confirm('是否确认删除参数编号为"' + productId + '"的数据项？', '警告', {
      confirmButtonText: proxy.$t('common.ok'),
      cancelButtonText: proxy.$t('common.cancel'),
      type: 'warning'
    })
    .then(function () {
      return delIotProductParserScript(productId)
    })
    .then(() => {
      getList()
      proxy.$modal.msgSuccess('删除成功')
    })
}

//移除特定字符的行
function removeScriptLang(str) {
  // 清除前后空行
  str = str.trim()
  // 将字符串按行分割
  let lines = str.split('\n')
  const maxLineIdx = lines.length - 1
  // 检查 n 是否有效（1-based）
  if (maxLineIdx <= 0) {
    return str // 超出范围，原样返回
  }

  //删除结尾的“```”行
  if (lines[maxLineIdx] == '```') lines.splice(maxLineIdx, 1)
  //删除第一行“```JavaScript”
  if (lines[0] == '```JavaScript') lines.splice(0, 1)

  // 重新拼接为字符串
  return lines.join('\n')
}
const codeTemplate = `var PROP_POST_METHOD = 'property.post'; //属性上报。

/*
调用时机：非MQTT协议的数据上传(自定义协议)的设备上传数据时，会调用此方法
rawDataToProtocol：原始数据转为指定格式数据
示例数据：
设备上报属性数据：
传入参数：
    字节数组
输出结果：
    json对象，最少要返回一个method属性，说明此数据的作用，如 { "method":"property.post"}
*/
function rawDataToProtocol(bytes) {
    var jsonMap = new Object();
	//解析数据到jsonMap中
    for (var i = 0; i < bytes.length; i++) {

    }

    return jsonMap;
}
/*
调用时机：非MQTT协议时，需要返回数据给设备，数据为非标准的json内容。输入参数会包含一个method属性来区分，当前数据的来源
protocolToRawData：协议对象转字节数组
示例数据：设备上报的返回结果
传入参数：
    标准josn对象
输出结果：
    字节数组
*/
function protocolToRawData(json) {
    var method = json['method'];
    var payloadArray = [];
	//根据不同method,处理json参数
    if (method == PROP_POST_METHOD) //属性设置。
    {

    }
    return payloadArray;
}

/*
  调用时机：MQTT协议，自定义topic
  注意：topic最后需要有字符串'?parser=default',才会走此方法
  transformPayload:转为标准格式
  示例数据
  自定义Topic内容转为平台数据格式
  输入参数：
     topic:主题
     bytes:内容
  输出参数：
	 平台格式
 */
function transformPayload(topic, bytes) {

    var jsonMap = {};
	//解析数据到jsonMap
    for (var i = 0; i < bytes.length; i++) {

    }

    return jsonMap;
}
`

handleQuery()
</script>
