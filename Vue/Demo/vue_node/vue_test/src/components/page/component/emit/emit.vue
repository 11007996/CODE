<template>
	<h1>子组件触发父组件事件</h1>
    <emitt @emitOpen="sendData"></emitt>
	<hr>
</template>
<script setup>
	import emitt from './emit-1.vue'
	const sendData = (data)=>{
		console.log("数据：" + data);
	}
	// function sendData(data){
	// 	console.log("数据：" + data);
	// }
</script>