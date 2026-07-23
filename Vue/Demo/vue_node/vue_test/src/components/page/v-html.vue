<template>
	<h1>动态html</h1>
	<div v-html="html_str"></div>
	
	<input type="text" @input="conChange" v-model="htmeContext">
	<hr />
	
</template>

<script>
	export default{
		data(){
			return{
				htmeContext:null,
				html_str:null,
			}
		},
		methods:{
			conChange:function(){
				this.html_str = "<h1 style=\"color:red\">"+this.htmeContext+"</h1>";
			}
		}
	}
</script>

<style>
</style>