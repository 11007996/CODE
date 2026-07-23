<template>
	<h1>使用ref获取DOM元素</h1>
	<h4>说明：</h4>
	<ol>
		<li>可以获取子组件的元素，也可以调用子组件的方法</li>
	</ol>
	<p ref = "domObj">DOM元素{{count}}</p>
	<button @click="geet">点击</button>
	<hr>

</template>
<script setup>
	import {ref} from 'vue'
	const domObjj = ref(null);  //获取元素引用
	const count = ref(0);

	const gett = ()=>{
		count.value++
		console.log(domObj.value.textContent);
	}

	function geet(){
		count.value++
		console.log(domObj.value.textContent);
	}

</script>