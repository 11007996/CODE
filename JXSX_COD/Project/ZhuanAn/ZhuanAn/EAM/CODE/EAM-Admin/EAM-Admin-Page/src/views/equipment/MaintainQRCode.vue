<!--
 * @Descripttion: (设备保养二维码)
 * @Author: (admin)
 * @Date: (2024-10-21)
-->
<template>
  <div>
    <el-form :model="queryParams" label-position="right" inline ref="queryRef" v-show="showSearch" @submit.prevent>
      <el-form-item label="资产编号" prop="assetNo">
        <el-input v-model="queryParams.assetNo" placeholder="请输入资产编号" />
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
          @sort-change="sortChange">
          <el-table-column type="selection" width="40" align="center" />
          <el-table-column prop="equipmentId" label="设备ID" align="center" v-if="columns.showColumn('equipmentId')" />
          <el-table-column prop="assetNo" label="资产编号" align="center" width="210" v-if="columns.showColumn('assetNo')" />
          <el-table-column prop="assetName" label="资产名称" align="center" min-width="200" v-if="columns.showColumn('assetName')" />
          <el-table-column prop="equipmentName" label="设备名称" align="center" min-width="200" v-if="columns.showColumn('equipmentName')" />
        </el-table>
        <pagination :total="total" v-model:page="queryParams.pageNum" v-model:limit="queryParams.pageSize" @pagination="getList" />
      </el-col>

      <!-- 右侧区域 -->
      <el-col :lg="8">
        <div class="right-panel">
          <div class="preUrl-item"><label>资产URL</label> <el-input v-model="preURL" readonly /></div>
          <div class="canvas-item">
            <!-- 整个资产标签 -->
            <canvas ref="assetCanvasRef" :width="qrSize.width" :height="qrSize.height"> </canvas>
            <!-- 二维码 -->
            <canvas ref="qrCanvasRef" width="900" height="900" style="display: none"> </canvas>
          </div>
          <div>
            <el-button type="warning" plain @click="downloadQRCode(currAssetNo + '.png')">下载</el-button>
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

<script setup name="maintainqrcode">
import { listEquipmentBase } from '@/api/equipment/equipmentBase.js'
import QRCode from 'qrcode'
import useUserStore from '@/store/modules/user.js'

const { proxy } = getCurrentInstance()
const loading = ref(false)
const showSearch = ref(true)
const queryParams = reactive({
  pageNum: 1,
  pageSize: 10,
  sort: 'AssetNo',
  sortType: 'asc',
  equipmentId: undefined
})
const columns = ref([
  { visible: false, prop: 'equipmentId', label: '设备ID' },
  { visible: true, prop: 'assetNo', label: '资产编号' },
  { visible: true, prop: 'assetName', label: '资产名称' },
  { visible: true, prop: 'equipmentName', label: '设备名称' }
])
const total = ref(0)
const dataList = ref([])
const queryRef = ref()
const tableRef = ref()

const preURL = ref('')
const assetCanvasRef = ref() //资产画布
const qrCanvasRef = ref() //二维码画布
const currAssetNo = ref()

//获取系统配置的保养二维码地址
proxy.getConfigKey('sys.maintain.qrcode.url').then((response) => {
  preURL.value = response.data
})

function getList() {
  loading.value = true
  listEquipmentBase(queryParams).then((res) => {
    const { code, data } = res
    if (code == 200) {
      dataList.value = data.result
      total.value = data.totalNum
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

//行单击事件
function handleRowClick(row) {
  CreateAssetCanvas(row.assetNo, row.assetName)
  currAssetNo.value = row.assetNo
}

// 批量下载
function handleBatchDownload() {
  let rows = tableRef.value.getSelectionRows()
  if (rows && rows.length > 0) {
    rows.forEach((item) => {
      CreateAssetCanvas(item.assetNo, item.assetName)
      downloadQRCode(item.assetNo + '.png')
    })
    proxy.$modal.msgSuccess('下载完成')
  } else {
    proxy.$modal.msgError('未选中数据')
  }
}

const qrSize = ref({
  width: 300,
  height: 385
})
//生成 资产标签
function CreateAssetCanvas(assetNo, assetName) {
  let ctx = assetCanvasRef.value.getContext('2d')
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
  ctx.fillText('设备点检二维码', x, y, maxWidth)

  //----------------二维码-----------------------
  let text = preURL.value.replace('{assetNo}', assetNo)
  let qrCanvas = getQRCodeCanvas(text)
  //ctx.drawImage(qrCanvas, 0, 30, 300, 300)
  y += tilteHeight
  ctx.drawImage(qrCanvas, 0, y, width, width)
  //----------------资产名称-----------------------
  ctx.font = textHeight + 'px Arial'
  ctx.textBaseline = 'bottom'
  ctx.textAlign = 'left'
  x = 10
  y += 20 * hScale + width //加上二维码的高度
  ctx.fillText('名称：' + assetName, x, y, width - 20)

  //----------------资产编号-----------------------
  y += 10 * hScale + textHeight
  ctx.fillText('编号：' + assetNo, x, y, width - 20)
}

//生成二维码
function getQRCodeCanvas(text) {
  QRCode.toCanvas(qrCanvasRef.value, text, { errorCorrectionLevel: 'H', width: 900, height: 900, margin: 2 })
  return qrCanvasRef.value
}

//下载 资产标签 图片
function downloadQRCode(fileName) {
  let dataUrl = assetCanvasRef.value.toDataURL('image/jpeg')
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
