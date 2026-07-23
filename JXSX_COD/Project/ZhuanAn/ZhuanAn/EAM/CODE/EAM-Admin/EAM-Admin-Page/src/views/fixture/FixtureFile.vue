<template>
  <div class="app-container">
    <!-- 查询条件 -->
    <el-form :model="queryParams" label-position="right" inline ref="queryRef" v-show="showSearch" @submit.prevent>
      <el-form-item label="治具" prop="fixtureId">
        <el-select v-model="queryParams.fixtureId" placeholder="系列,名称" clearable filterable remote :remote-method="handleQueryFixture">
          <template #header>
            <span>系列 / 名称</span>
          </template>
          <el-option v-for="item in options.fixture_options" :key="item.dictValue" :label="item.dictLabel" :value="item.dictValue"></el-option>
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
        <el-button type="primary" v-hasPermi="['fixture:add']" plain icon="upload" @click="handleAdd">
          {{ $t('btn.upload') }}
        </el-button>
      </el-col>
      <right-toolbar v-model:showSearch="showSearch" @queryTable="getList"></right-toolbar>
    </el-row>

    <!-- 数据区域 -->
    <el-table :data="dataList" v-loading="loading" ref="table" border highlight-current-row>
      <el-table-column prop="id" label="文件id" align="center" width="180" />
      <el-table-column prop="realName" label="文件名" align="left" min-width="180" :show-overflow-tooltip="true">
        <template #default="scope">
          <el-link type="primary" :href="scope.row.accessUrl" target="_blank">{{ scope.row.realName }}</el-link>
        </template>
      </el-table-column>
      <el-table-column prop="fileSize" label="文件大小" min-width="90" align="center" :show-overflow-tooltip="true" />
      <el-table-column prop="fileExt" label="扩展名" align="center" :show-overflow-tooltip="true" width="80px" />
      <el-table-column prop="create_by" label="操作人" width="90" align="center" />
      <el-table-column prop="create_time" label="创建日期" align="center" width="160" />
      <el-table-column label="操作" fixed="right" align="center" width="130">
        <template #default="scope">
          <el-button
            text
            icon="download"
            title="下载"
            v-hasPermi="['fixture:query']"
            v-if="scope.row.storeType == 1"
            @click="handleDown(scope.row)"></el-button>
          <el-button class="copy-btn-main" icon="document-copy" title="复制" text @click="copyText(scope.row.accessUrl)"> </el-button>
          <el-button v-hasPermi="['fixture:delete']" title="删除" text icon="delete" @click="handleDelete(scope.row)"> </el-button>
        </template>
      </el-table-column>
    </el-table>
    <pagination background :total="total" v-model:page="queryParams.pageNum" v-model:limit="queryParams.pageSize" @pagination="getList" />

    <!-- 上传 -->
    <el-dialog :title="title" :lock-scroll="false" v-model="open" width="400px" draggable>
      <el-form ref="formRef" :model="form" :rules="rules" label-width="90px" label-position="left">
        <el-form-item label="治具" prop="fixtureId">
          <el-select v-model="form.fixtureId" placeholder="系列,名称" clearable filterable remote :remote-method="handleQueryFixture">
            <template #header>
              <span>系列 / 名称</span>
            </template>
            <el-option v-for="item in options.fixture_options" :key="item.dictValue" :label="item.dictLabel" :value="item.dictValue"></el-option>
          </el-select>
        </el-form-item>
        <FileUpload ref="uploadRef" @success="handleUploadSuccess" :autoUpload="false" :data="uploadData" />
      </el-form>
      <template #footer>
        <div class="dialog-footer">
          <el-button text @click="cancel">{{ $t('btn.cancel') }}</el-button>
          <el-button type="primary" @click="submitUpload">{{ $t('btn.submit') }}</el-button>
        </div>
      </template>
    </el-dialog>
  </div>
</template>
<script setup name="file">
import { dictFixtureBase } from '@/api/fixture/fixtureBase.js'
import { listFixtureFile, bacthAddFixtureFile, delFixtureFile } from '@/api/fixture/fixtureFile.js'
import { useClipboard } from '@vueuse/core'
import FileUpload from '@/components/FileUpload/index.vue'
import useUserStore from '@/store/modules/user.js'

const props = defineProps({
  fixtureId: Number
})
const showSearch = ref(true)
if (props.fixtureId) showSearch.value = false
// 遮罩层
const loading = ref(true)
// 弹出层标题
const title = ref('')
// 是否显示弹出层
const open = ref(false)
// 表单
const formRef = ref(null)
const uploadRef = ref(null)

// 数据列表
const dataList = ref([])
// 总记录数
const total = ref(0)

const state = reactive({
  form: {
    fixtureId: 0
  },
  rules: {
    fixtureId: [{ required: true, message: '治具不能为空', trigger: 'blur' }]
  },
  queryParams: {
    pageNum: 1,
    pageSize: 20,
    fixtureId: props.fixtureId,
    fileId: undefined
  },
  options: {
    //治具选项
    fixture_options: []
  }
})
const { queryParams, form, rules, options } = toRefs(state)
const { proxy } = getCurrentInstance()
const factoryId = useUserStore().factoryId
const uploadData = ref({ fileDir: 'Upload/Fixture/' + factoryId })

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

// 查询数据
function getList() {
  loading.value = true
  listFixtureFile(queryParams.value).then((res) => {
    if (res.code == 200) {
      dataList.value = res.data.result
      total.value = res.data.totalNum
      loading.value = false
    }
  })
}

// 取消按钮
function cancel() {
  open.value = false
  reset()
}

// 重置数据表单
function reset() {
  form.value = {
    fixtureId: ''
  }
  proxy.resetForm('formRef')
}

/** 新增按钮操作 */
function handleAdd() {
  reset()
  open.value = true
  title.value = '上传文件'
}

/** 删除按钮操作 */
function handleDelete(row) {
  const delData = {
    fixtureId: row.fixtureId,
    fileId: row.id
  }
  proxy
    .$confirm('是否确认删除参数编号为"' + row.id + '"的数据项？')
    .then(function () {
      return delFixtureFile(delData)
    })
    .then(() => {
      handleQuery()
      proxy.$modal.msgSuccess('删除成功')
    })
    .catch(() => {})
}

// 上传成功方法
function handleUploadSuccess(fileUrlStr, filelist) {
  filelist = JSON.parse(JSON.stringify(filelist.value))
  open.value = false
  if (filelist) {
    const files = []
    filelist.forEach((item) => {
      files.push({ fixtureId: form.value.fixtureId, fileId: item.fileId })
    })
    bacthAddFixtureFile(files).then((res) => {
      const { code, data } = { ...res }
      if (code == 200) {
        getList()
      }
    })
  }
}

// 手动上传
function submitUpload() {
  proxy.$refs['formRef'].validate((valid) => {
    if (valid) {
      proxy.$refs.uploadRef.submitUpload()
    }
  })
}

//下载
async function handleDown(item) {
  await proxy.downFile('/common/downloadFile', { fileId: item.id })
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

//治具查询
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
        options.value.fixture_options = res.data.result
      })
    }, 200)
  }
}

//监听属性变化
watch(props, (val) => {
  queryParams.value.fixtureId = props.fixtureId
  handleQuery()
})

handleQuery()
</script>
