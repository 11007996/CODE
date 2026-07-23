<!--
 * @Descripttion: (耗品储位二维码)
 * @Author: (admin)
 * @Date: (2024-10-21)
-->
<template>
  <div>
    <el-form :model="queryParams" label-position="right" inline ref="queryRef" v-show="showSearch" @submit.prevent>
      <el-form-item label="储位名称" prop="storageName">
        <el-input v-model="queryParams.storageName" placeholder="请输入储位名称" />
      </el-form-item>
      <el-form-item>
        <el-button icon="search" type="primary" @click="handleQuery">{{ $t('btn.search') }}</el-button>
        <el-button icon="refresh" @click="resetQuery">{{ $t('btn.reset') }}</el-button>
      </el-form-item>
    </el-form>

    <el-row :gutter="15" class="mb10" justify="center">
      <!-- 左侧区域 -->
      <el-col :lg="16">
        <!-- 工具区域 -->
        <el-row :gutter="15" class="mb10">
          <el-col :span="1.5">
            <el-button type="warning" plain icon="download" @click="handleBatchDownload"> 批量下载 </el-button>
          </el-col>
          <right-toolbar v-model:showSearch="showSearch" @queryTable="getList" :columns="columns"></right-toolbar>
        </el-row>

        <el-table
          :data="dataList"
          v-loading="loading"
          ref="tableRef"
          border
          header-cell-class-name="el-table-header-cell"
          highlight-current-row
          max-height="450"
          @row-click="handleRowClick"
          row-key="storageId"
          :tree-props="{ children: 'children', hasChildren: 'hasChildren' }">
          <el-table-column type="selection" width="40" align="center" />
          <el-table-column prop="storageId" label="储位ID" align="center" v-if="columns.showColumn('storageId')" />
          <el-table-column prop="storageName" label="储位名称" align="left" min-width="160" v-if="columns.showColumn('storageName')" />
          <el-table-column prop="storageFullName" label="储位全名" align="left" min-width="200" v-if="columns.showColumn('storageFullName')" />
          <el-table-column prop="storageType" label="储位类型" align="center" width="90" v-if="columns.showColumn('storageType')">
            <template #default="scope">
              <dict-tag :options="options.storage_type" :value="scope.row.storageType" />
            </template>
          </el-table-column>
        </el-table>
      </el-col>

      <!-- 右侧区域 -->
      <el-col :lg="8">
        <div class="right-panel">
          <div class="preUrl-item"><label>储位URL</label> <el-input v-model="preURL" readonly /></div>
          <div class="canvas-item">
            <!-- 整个画布 -->
            <canvas ref="canvasRef" :width="qrSize.width" :height="qrSize.height"> </canvas>
            <!-- 二维码 -->
            <canvas ref="qrCanvasRef" width="900" height="900" style="display: none"> </canvas>
          </div>
          <div>
            <el-button type="warning" plain @click="downloadQRCode(currStorageName + '.png')">下载</el-button>
            宽<el-input-number v-model="qrSize.width" size="small" :controls="false" style="width: 60px" /> 高<el-input-number
              v-model="qrSize.height"
              size="small"
              :controls="false"
              style="width: 60px" />
          </div>
        </div>
      </el-col>
    </el-row>
  </div>
</template>

<script setup name="cusstorageqrcode">
import { treeConsumableStorageSpace } from '@/api/consumable/consumableStorageSpace.js'
import QRCode from 'qrcode'

const { proxy } = getCurrentInstance()

var dictParams = [{ dictType: 'storage_type' }]
proxy.getDicts(dictParams).then((response) => {
  response.data.forEach((element) => {
    state.options[element.dictType] = element.list
  })
})

const loading = ref(false)
const showSearch = ref(true)
const queryParams = reactive({
  pageNum: 1,
  pageSize: 10,
  sort: '',
  sortType: 'asc',
  storageName: undefined
})
const columns = ref([
  { visible: false, prop: 'storageId', label: '储位ID' },
  { visible: true, prop: 'storageName', label: '储位名称' },
  { visible: false, prop: 'storageFullName', label: '储位全名' },
  { visible: true, prop: 'storageType', label: '储位类型' }
])
const total = ref(0)
const dataList = ref([])
const queryRef = ref()
const tableRef = ref()
const state = reactive({
  options: {
    // 储位类型 选项列表 格式 eg:{ dictLabel: '标签', dictValue: '0'}  //0:仓库，1:区域，2货架，3:储位
    storage_type: []
  }
})

const { options } = toRefs(state)

const preURL = ref('')
const canvasRef = ref() //画布
const qrCanvasRef = ref() //二维码画布
const currStorageName = ref()

//获取系统配置的耗品储位二维码地址
proxy.getConfigKey('sys.consumable.storage.qrcode.url').then((response) => {
  preURL.value = response.data
})

function getList() {
  loading.value = true
  treeConsumableStorageSpace(queryParams).then((res) => {
    const { code, data } = res
    if (code == 200) {
      dataList.value = res.data
      loading.value = false
    }
  })
}

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

//行单击事件
function handleRowClick(row) {
  CreateCanvas(row.storageId, row.storageName)
  currStorageName.value = row.storageName
}

// 批量下载
function handleBatchDownload() {
  let rows = tableRef.value.getSelectionRows()
  if (rows && rows.length > 0) {
    rows.forEach((item) => {
      CreateCanvas(item.storageId, item.storageName)
      downloadQRCode(item.storageName + '.png')
    })
    proxy.$modal.msgSuccess('下载完成')
  } else {
    proxy.$modal.msgError('未选中数据')
  }
}

const qrSize = ref({
  width: 300,
  height: 350
})
//生成 标签
function CreateCanvas(storageId, storageName) {
  let ctx = canvasRef.value.getContext('2d')
  let width = ctx.canvas.width
  let height = ctx.canvas.height
  let wScale = width / 300
  let hScale = height / 385
  let tilteHeight = 20 * hScale //标题字体高度
  let textHeight = 18 * hScale //文本字体高度
  ctx.fillStyle = '#fff'
  ctx.fillRect(0, 0, width, height) //清理画布
  //------------标题----------------------
  ctx.font = 'bold ' + tilteHeight + 'px Arial'
  ctx.fillStyle = '#000'
  // 设置文本基线为中间位置
  ctx.textBaseline = 'top'
  // 设置文本对齐方式为居中
  ctx.textAlign = 'center'
  // 获取 Canvas 宽度和高度的一半
  let x = width / 2
  let y = 10 * hScale
  let maxWidth = (width / 20) * 18
  ctx.fillText('耗品储位', x, y, maxWidth)

  //----------------二维码-----------------------
  let text = preURL.value.replace('{storageId}', storageId)
  let qrCanvas = getQRCodeCanvas(text)
  //ctx.drawImage(qrCanvas, 0, 30, 300, 300)
  y += tilteHeight
  ctx.drawImage(qrCanvas, 0, y, width, width)
  //----------------储位名称-----------------------
  ctx.font = textHeight + 'px Arial'
  ctx.textBaseline = 'bottom'
  ctx.textAlign = 'left'
  x = 10
  y += 20 * hScale + width //加上二维码的高度
  ctx.fillText('名称：' + storageName, x, y, width - 20)
}

//生成二维码
function getQRCodeCanvas(text) {
  QRCode.toCanvas(qrCanvasRef.value, text, { errorCorrectionLevel: 'H', width: 900, height: 900, margin: 2 })
  return qrCanvasRef.value
}

//下载 标签 图片
function downloadQRCode(fileName) {
  let dataUrl = canvasRef.value.toDataURL('image/jpeg')
  // 创建一个a标签
  var link = document.createElement('a')
  link.href = dataUrl
  link.download = fileName // 设置下载的文件名
  // 添加标签到文档中
  document.body.appendChild(link)
  // 单击连接
  link.click()
  // 删除标签
  document.body.removeChild(link)
}

handleQuery()
</script>

<style lang="scss">
.preUrl-item {
  display: flex;
  flex-direction: row;
  padding-bottom: 10px;
}
.right-panel {
  display: flex;
  flex-direction: column;
  max-width: 320px;
  padding: 0 10px;
}
.canvas-item {
  margin: auto;
  canvas {
    border: 1px solid lightgray;
  }
}
</style>
