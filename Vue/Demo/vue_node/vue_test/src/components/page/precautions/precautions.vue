<template>
	<h1>VUE使用的注意事项</h1>
	<addAttribute />
	<listIndex></listIndex>
	<vmodel></vmodel>
	
</template>


<script>
	import addAttribute from './addAttribute.vue'
	import listIndex from './listIndex.vue'
	import vmodel from './v-model.vue'
	
	export default{
		components:{
			addAttribute,
			listIndex,
			vmodel
		}
	}
	
</script>


<style>
	
</style>