<template>
	<h1>组件挂载</h1>
	<p style="color: red">{{ msg }}</p>
	<button @click="show">{{count}}</button>
	<!-- <p :id=pid class=pid">{{count}}</p> -->
	<p>{{count}}</p>
	
	<br />
	<hr />
</template>

<script>
	export default{
		data(){
			return{
				msg:"hello world",
				colors:"red",
				pid:"ppid",
				but:"sumbmit",
				count:0,
			}
		},
		methods:{
			show: function(){
				    this.count++;
			        console.log("hello world")
			    }
		}
	}
</script>

<style>
</style>