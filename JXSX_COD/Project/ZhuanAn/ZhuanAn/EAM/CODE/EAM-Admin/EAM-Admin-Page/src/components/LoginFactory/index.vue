<template>
  <div>
    <span class="title">
      {{ useUserStore().factoryName + ' [' + useUserStore().factoryId + ']' }}
    </span>
  </div>
</template>

<script setup name="index">
import useUserStore from '@/store/modules/user'
</script>
<style lang="scss" scoped>
.title {
  text-wrap: nowrap;
}
</style>
