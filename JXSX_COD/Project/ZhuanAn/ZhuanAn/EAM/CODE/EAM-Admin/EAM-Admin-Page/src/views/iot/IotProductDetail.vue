<!--
 * @Descripttion: (Iot产品/详情)
 * @Author: (admin)
 * @Date: (2024-05-07)
-->
<template>
  <div>
    <el-form :model="form" label-position="right" inline ref="queryRef">
      <el-form-item label="产品名称" prop="productName">
        <el-input v-model="form.productName" placeholder="请输入产品名称" />
      </el-form-item>
    </el-form>

    <!-- tab -->
    <el-tabs type="card" v-model="activeTab">
      <el-tab-pane label="Topic类" name="topicFormat">
        <IotProductTopic :productId="currProductId" />
      </el-tab-pane>
      <el-tab-pane label="属性" name="thingProperty">
        <IotProductThingProperty :productId="currProductId" />
      </el-tab-pane>
      <el-tab-pane label="事件" name="thingEvent">
        <IotProductThingEvent :productId="currProductId" />
      </el-tab-pane>
      <!-- <el-tab-pane label="服务" name="thingService">
        <IotProductThingService :productId="currProductId" />
      </el-tab-pane> -->
      <el-tab-pane label="消息解析" name="parserScript">
        <IotProductParserScript :productId="currProductId" />
      </el-tab-pane>
    </el-tabs>
  </div>
</template>

<script setup name="iotproductdetail">
import IotProductTopic from './IotProductTopic.vue'
import IotProductThingProperty from './IotProductThingProperty.vue'
import IotProductThingEvent from './IotProductThingEvent.vue'
import IotProductThingService from './IotProductThingService.vue'
import IotProductParserScript from './IotProductParserScript.vue'
import { getIotProduct } from '@/api/iot/iotProduct.js'
const route = useRoute()
const activeTab = ref('topicFormat')
const currProductId = ref()
watch(
  () => route.query.productId,
  (newValue, oldValue) => {
    if (route.query.productId) {
      currProductId.value = Number(route.query.productId)
      handleQueryProduct()
    }
  },
  { immediate: true }
)

const state = reactive({
  form: {},
  options: {
    // 产品类型 选项列表 格式 eg:{ dictLabel: '标签', dictValue: '0'}
    iot_product_type: [],
    // 节点类型
    iot_product_node_type: [],
    // 产品数据格式
    iot_product_data_format: []
  }
})
const { form, options } = toRefs(state)

// 修改按钮操作
function handleQueryProduct() {
  if (!currProductId.value) return
  getIotProduct(currProductId.value).then((res) => {
    const { code, data } = res
    if (code == 200) {
      form.value = {
        ...data
      }
    }
  })
}

handleQueryProduct()
</script>
