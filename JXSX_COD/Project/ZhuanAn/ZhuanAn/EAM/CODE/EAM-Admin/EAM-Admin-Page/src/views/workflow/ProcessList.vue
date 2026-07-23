<!--
 * @Descripttion: (流程定义/WF_Process_Define)
 * @Author: (admin)
 * @Date: (2024-06-04)
-->
<template>
  <div>
    <el-form :model="queryParams" label-position="right" inline ref="queryRef" v-show="showSearch" @submit.prevent>
      <el-form-item label="流程名称" prop="processName">
        <el-input v-model="queryParams.processName" placeholder="请输入流程名称" />
      </el-form-item>
      <el-form-item>
        <el-button icon="search" type="primary" @click="handleQuery">{{ $t('btn.search') }}</el-button>
        <el-button icon="refresh" @click="resetQuery">{{ $t('btn.reset') }}</el-button>
      </el-form-item>
    </el-form>
    <!-- 流程清单 -->
    <el-row :gutter="20">
      <el-col v-for="item in dataList" :sm="8" :lg="6">
        <el-button type="primary" @click="openPage(item.processId)" plain class="processItem">{{ item.processName }}</el-button>
      </el-col>
    </el-row>
  </div>
</template>

<script setup name="processdefine">
import { listProcessDefine } from '@/api/workflow/processDefine.js'

const { proxy } = getCurrentInstance()
const router = useRouter()
const loading = ref(false)
const showSearch = ref(true)
const queryParams = reactive({
  pageNum: 1,
  pageSize: 1000,
  sort: '',
  sortType: 'asc',
  processName: undefined,
  status: '0'
})

const total = ref(0)
const dataList = ref([])
const queryRef = ref()

function getList() {
  loading.value = true
  listProcessDefine(queryParams).then((res) => {
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

// 点击事件
const openPage = (processId) => {
  //使用resolve
  const url = router.resolve({
    path: '/process/' + processId
  })
  window.open(url.href, '_blank')
}

handleQuery()
</script>
<style scoped>
.processItem {
  width: 100%;
  height: 60px;
  margin: 10px 0px;
}
</style>
