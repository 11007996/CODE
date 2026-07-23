<template>
  <el-form ref="formRef">
    <el-form-item prop="factoryId">
      <el-select v-model="form.factoryId" class="fullWidth" @change="handleSwitchFactory" filterable placeholder="厂区">
        <template #prefix>
          <svg-icon name="tree" class="input-icon" />
        </template>
        <el-option
          v-for="item in factory_options"
          :key="item.factoryId"
          :label="'[' + item.factoryId + '] ' + item.factoryName"
          :value="item.factoryId" />
      </el-select>
    </el-form-item>
  </el-form>
</template>

<script setup>
import { getUserFactorys } from '@/api/system/user'
import useUserStore from '@/store/modules/user'
import { reactive } from 'vue'
const form = reactive({ factoryId: null })
const factory_options = ref([])

function queryFactorys() {
  getUserFactorys().then((res) => {
    if (res.code == 200) {
      factory_options.value = res.data
    }
  })
}

function handleSwitchFactory() {
  const factoryId = form.factoryId
  useUserStore()
    .switchFacory(factoryId)
    .then((res) => {
      //切换成功，刷新整理页面
      location.reload()
    })
}

queryFactorys()
</script>
