<!-- <template>
  <div v-for="(item, i) in list" :ref="el => { if (el) divs[i] = el }">
    {{ item }}
  </div>
</template>

<script>
  import { ref, reactive, onBeforeUpdate } from 'vue'

  export default {
    setup() {
      const list = reactive([1, 2, 3])
      const divs = ref([])

      // 确保在每次更新之前重置ref
      onBeforeUpdate(() => {
        divs.value = []
      })

      return {
        list,
        divs
      }
    }
  }
</script> -->




<!-- <template>
    <div>
        <p>计数器实例: {{ count }}</p>
        <input @click="myFn" type="button" value="点我加 1">
    </div>
    <hr>
</template>

<script>
import {ref, onMounted} from 'vue';

export default {
    setup(){
        //定义初始值为0的变量，要使用ref方法赋值，直接赋值的话变量改变不会更新 UI
        let count = ref(0);

        // 定义点击事件 myFn
        function myFn(){
            console.log(count);
            count.value += 1;
        }
       
       // 组件被挂载时，我们用 onMounted 钩子记录一些消息
        onMounted(() => console.log('component mounted!'));

        // 外部使用组合API中定义的变量或方法，在模板中可用。
        return {count,myFn} // 返回的函数与方法的行为相同
    }
}
</script> -->



<!-- 同上 -->
<template>
    <div>
        <p>计数器实例: {{ count }}</p>
        <input @click="myFn" type="button" value="点我加 1">
    </div>
    <hr>
</template>

<script setup>
import {ref, onMounted} from 'vue';

    //定义初始值为0的变量，要使用ref方法赋值，直接赋值的话变量改变不会更新 UI
    let count = ref(0);

    // 定义点击事件 myFn
   function myFn(){
          console.log(count);
          count.value += 1;
      }
       
    // 组件被挂载时，我们用 onMounted 钩子记录一些消息
    onMounted(() => console.log('component mounted!'));


</script>