<template>
	<h1>修改class属性改变样式</h1>
	<p :class="styleName">hello</p>
	<button @click="change">点击</button>

	<div class="color" :style="{backgroundColor:getCoor()}"></div>
	<div class="color1"></div>
	
	<!-- CSS module -->
	<p :class="$style.number">CSS module</p>
	<hr>
</template>
<script setup>
	import {ref } from 'vue'
	const styleName= ref("ss");
	function change(){
		if(styleName.value == "ss"){
			styleName.value = "aa";
		}
		else{
			styleName.value = "ss";
		}
	}
	function getCoor(n){
		return 'pink';
	}
</script>
<style scoped>
	.ss{
		color: red;
	}
	.aa{
		color: blue;
	}
	.color{
		z-index: 0;
		height: 50px;
		width: 500px;
	}
	.color1{
		margin-top: -20px;
		background-Color: blue;
		height: 100px;
		width: 50px;
	}
	
</style>

<!-- vue-loader -->
<style module>
.number{
		color: red;
	}
</style>