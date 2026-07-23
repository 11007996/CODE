<template >
	<h1>组件间数据传递</h1>
  <div>
    <ChildComponent :message="parentMessage">
      <template #temp1>
        <p>temp1模板</p>
      </template>
      <template #temp2>
        <p>temp2模板</p>
      </template>
    </ChildComponent>
  </div>
  <button @click="fun">修改</button>
  <hr>
</template>
 
<script setup>
import { ref } from 'vue';
import ChildComponent from './props-1.vue';
 
const parentMessage = ref('Hello from Parent');

function fun(){
	parentMessage.value = "hello";
}

</script>