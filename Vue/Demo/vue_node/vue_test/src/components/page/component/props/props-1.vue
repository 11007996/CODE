<template>
	<h3>子组件传递</h3>
  <ol>
    <li>子组件传递数据</li>
    <li>slot槽，父组件将模板传递到子组件</li>
  </ol>
  <slot name ="temp1"></slot>
  <div>
    <p>{{ message }}</p>
  </div>
  <slot name ="temp2"></slot>
</template>
 
<script setup>
defineProps({
  message: {
    type: String,
    required: true,
    validator(value){   //验证函数
      return value.length!=100;
    }
  }
  // message : String
});
</script>