<template>
  <div class="app-container">
    <!-- 查询表单 -->
    <el-form :model="queryParams" label-position="right" inline ref="queryRef" v-show="showSearch" @submit.prevent>
      <el-form-item label="设备" prop="equipmentId">
        <el-select
          v-model="queryParams.equipmentId"
          placeholder="资产编号/设备名称/资产名称/自定义机型"
          clearable
          filterable
          remote
          :remote-method="handleQueryEquipment">
          <el-option v-for="item in options.equipment_options" :key="item.dictValue" :label="item.dictLabel" :value="item.dictValue"></el-option>
        </el-select>
      </el-form-item>
      <el-form-item>
        <el-button icon="search" type="primary" @click="handleQuery">{{ $t('btn.search') }}</el-button>
        <el-button icon="refresh" @click="resetQuery">{{ $t('btn.reset') }}</el-button>
      </el-form-item>
    </el-form>

    <!-- 工具区域 -->
    <el-row :gutter="10" class="mb8">
      <el-col :span="1.5">
        <el-button type="primary" v-hasPermi="['equipment:file:bind']" plain icon="connection" @click="handleBind"> 关联 </el-button>
      </el-col>
      <right-toolbar v-model:showSearch="showSearch" @queryTable="getList"></right-toolbar>
    </el-row>

    <!-- 数据区域 -->
    <el-table :data="dataList" v-loading="loading" ref="table" border highlight-current-row>
      <el-table-column prop="equipmentId" label="设备ID" align="center" />
      <el-table-column prop="assetNo" label="资产编号" align="center" width="210" />
      <el-table-column prop="assetName" label="资产名称" min-width="180" />
      <el-table-column prop="fileId" label="文件id" align="center" width="180" />
      <el-table-column prop="fileName" label="文件名" align="left" min-width="180" :show-overflow-tooltip="true">
        <template #default="scope">
          <el-link type="primary" :href="scope.row.accessUrl" target="_blank">{{ scope.row.fileName }}</el-link>
        </template>
      </el-table-column>
      <el-table-column label="操作" fixed="right" align="center" width="130">
        <template #default="scope">
          <!-- <el-button text  icon="view" title="查看" @click="handleView(scope.row)"></el-button> -->
          <el-button text icon="download" title="下载" v-hasPermi="['equipment:file:query']" @click="handleDown(scope.row)"></el-button>
          <el-button class="copy-btn-main" icon="document-copy" title="复制" text @click="copyText(scope.row.accessUrl)"> </el-button>
          <el-button v-hasPermi="['equipment:file:unbind']" title="解绑" text icon="delete" @click="handleUnbind(scope.row)"> </el-button>
        </template>
      </el-table-column>
    </el-table>
    <pagination background :total="total" v-model:page="queryParams.pageNum" v-model:limit="queryParams.pageSize" @pagination="getList" />

    <!-- 文件关联弹窗 -->
    <el-dialog title="关联" :lock-scroll="false" v-model="open">
      <el-form :model="form" ref="formRef" label-position="right" :rules="rules" @submit.prevent>
        <el-form-item label="设备" prop="equipmentId">
          <el-select
            v-model="form.equipmentId"
            placeholder="资产编号/设备名称/资产名称/自定义机型"
            clearable
            filterable
            remote
            :remote-method="handleQueryEquipment"
            class="fullWidth">
            <el-option v-for="item in options.equipment_options" :key="item.dictValue" :label="item.dictLabel" :value="item.dictValue"></el-option>
          </el-select>
        </el-form-item>
      </el-form>
      <br />
      <el-form :model="queryParams2" label-position="right" inline @submit.prevent>
        <el-form-item label="文件名称" prop="realName">
          <el-input v-model="queryParams2.realName" placeholder="请输入文件名称" />
        </el-form-item>
        <el-form-item>
          <el-button icon="search" type="primary" @click="handleQuery2">{{ $t('btn.search') }}</el-button>
          <el-button icon="refresh" @click="resetQuery">{{ $t('btn.reset') }}</el-button>
        </el-form-item>
      </el-form>
      <!-- 数据区域 -->
      <el-table :data="dataList2" v-loading="loading" ref="tableRef" border highlight-current-row>
        <el-table-column type="selection" width="50" />
        <el-table-column prop="id" label="文件id" align="center" width="180" />
        <el-table-column prop="realName" label="文件名" align="left" min-width="180" :show-overflow-tooltip="true">
          <template #default="scope">
            <el-link type="primary" :href="scope.row.accessUrl" target="_blank">{{ scope.row.realName }}</el-link>
          </template>
        </el-table-column>
      </el-table>
      <pagination background :total="total2" v-model:page="queryParams2.pageNum" v-model:limit="queryParams2.pageSize" @pagination="getList2" />

      <template #footer>
        <el-button text @click="cancel">{{ $t('btn.cancel') }}</el-button>
        <el-button type="primary" @click="submitForm">{{ $t('btn.submit') }}</el-button>
      </template>
    </el-dialog>
  </div>
</template>
<script setup name="file">
import { getSysfile } from '@/api/tool/file.js'
import { listEquipmentFile, listEquipmentFileBind, bindEquipmentFile, unbindEquipmentFile } from '@/api/equipment/equipmentFile.js'
import { useClipboard } from '@vueuse/core'
import QRCode from 'qrcodejs2-fixes'
import { dictEquipmentBase } from '@/api/equipment/equipmentBase.js'

const props = defineProps({
  equipmentId: String
})

//显示搜索条件
const showSearch = ref(true)
if (props.equipmentId) showSearch.value = false
const queryRef = ref()
// 遮罩层
const loading = ref(true)
// 弹出层标题
const title = ref('')
// 是否显示弹出层
const open = ref(false)
const openView = ref(false)
// 表单
const formRef = ref(null)
const formView = ref({})
const uploadRef = ref(null)

// 数据列表
const dataList = ref([])
// 总记录数
const total = ref(0)

const state = reactive({
  form: {
    equipmentId: null
  },
  queryParams: {
    pageNum: 1,
    pageSize: 10,
    equipmentId: props.equipmentId,
    realName: undefined
  },
  options: {
    //设备选项
    equipment_options: []
  },
  rules: {
    equipmentId: [{ required: true, message: '设备不能为空', trigger: 'blur' }]
  }
})
const { queryParams, form, rules, options } = toRefs(state)
const { proxy } = getCurrentInstance()

// 查询数据
function getList() {
  loading.value = true
  listEquipmentFileBind(queryParams.value).then((res) => {
    if (res.code == 200) {
      dataList.value = res.data.result
      total.value = res.data.totalNum
      loading.value = false
    }
  })
}

// 重置查询操作
function resetQuery() {
  proxy.resetForm('queryRef')
  handleQuery()
}

/** 搜索按钮操作 */
function handleQuery() {
  queryParams.pageNum = 1
  getList()
}

// -------------------------表单操作---------------------------
const dataList2 = ref([])
const total2 = ref(0)
const tableRef = ref()
const queryParams2 = reactive({
  pageNum: 1,
  pageSize: 10,
  realName: undefined
})

function handleQuery2() {
  queryParams2.pageNum = 1
  getList2()
}

// 取消按钮
function cancel() {
  open.value = false
  reset()
}

// 查询所有的设备文件
function getList2() {
  listEquipmentFile(queryParams2).then((res) => {
    if (res.code == 200) {
      dataList2.value = res.data.result
      total2.value = res.data.totalNum
      loading.value = false
    }
  })
}

// 重置数据表单
function reset() {
  form.value = {
    equipmentId: null
  }
  proxy.resetForm('formRef')
}

/** 关联按钮操作 */
function handleBind() {
  reset()
  open.value = true
  title.value = '设备文件关联'
  form.value.equipmentId = props.equipmentId
  handleQuery2()
}

/** 解绑按钮操作 */
function handleUnbind(row) {
  const fileId = row.id
  let parm = {
    equipmentId: row.equipmentId,
    fileId: row.fileId
  }
  proxy
    .$confirm('是否确认解绑文件id为"' + parm.fileId + '"的数据项？')
    .then(function () {
      return unbindEquipmentFile(parm)
    })
    .then(() => {
      handleQuery()
      proxy.$modal.msgSuccess('删除成功')
    })
    .catch(() => {})
}

/** 查看按钮操作 */
function handleView(row) {
  getSysfile(row.id).then((res) => {
    const { code, data } = res
    if (code == 200) {
      openView.value = true
      formView.value = data
      proxy.$nextTick(() => {
        createQrCode(data.accessUrl)
      })
    }
  })
}

function createQrCode(url) {
  document.getElementById('imgContainer').innerHTML = ''
  new QRCode(document.getElementById('imgContainer'), {
    text: url,
    width: 130,
    height: 130
  })
}

//下载
async function handleDown(row) {
  await proxy.downFile('/common/downloadFile', { fileId: row.fileId })
}
const { copy, isSupported } = useClipboard()
const copyText = async (val) => {
  if (isSupported) {
    copy(val)
    proxy.$modal.msgSuccess('复制成功！')
  } else {
    proxy.$modal.msgError('当前浏览器不支持')
  }
}

//关联提交
function submitForm() {
  proxy.$refs['formRef'].validate((valid) => {
    if (valid) {
      //获取选中的文件ID
      let rows = tableRef.value.getSelectionRows()
      let data = []
      rows.forEach((item) => {
        data.push({ equipmentId: form.value.equipmentId, fileId: item.id })
      })
      //提交绑定请求
      bindEquipmentFile(data).then((res) => {
        const { code, data } = { ...res }
        if (code == 200) {
          proxy.$modal.msgSuccess('绑定成功！')
          open.value = false
          handleQuery()
        }
      })
    }
  })
}

// 查询设备
function handleQueryEquipment(keyword) {
  if (keyword) {
    const params = {
      pageNum: 1,
      pageSize: 10,
      sort: '',
      sortType: 'asc',
      keyword: keyword
    }
    setTimeout(() => {
      dictEquipmentBase(params).then((res) => {
        state.options.equipment_options = res.data.result
      })
    }, 200)
  }
}

//监听属性变化
watch(props, (val) => {
  queryParams.value.equipmentId = props.equipmentId
  handleQuery()
})

handleQuery()
</script>
