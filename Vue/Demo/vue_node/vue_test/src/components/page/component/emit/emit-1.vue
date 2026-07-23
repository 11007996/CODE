<template>
	<button @click="send">send</button>
</template>
<script setup>
	// 事件触发
	import {defineEmits} from 'vue'
	const emit = defineEmits['emitOpen'];  //指定meit对象可以触发的事件

	// const emit = defineEmits();   //所以事件都可以触发
	// const send = ()=>{
	// 	const data = "hello world";
	// 	emit('emitOpen',data);
	// }
	function send(){
		const data = "hello world";
		emit('emitOpen',data);    //将数据传递给父组件事件
	}
</script>