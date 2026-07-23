<template>
	<h1>自定义事件</h1>
	<ol>
		<li>子组件触发父组件的事件</li>
	</ol>
	<p>{{message}}</p>
	<vue2emit1 @childEmit ="changee"></vue2emit1>
	<hr>
</template>
<script>
	import vue2emit1 from './vue2emit-1.vue'

	export default{
		
		data(){
			return{
				message : "hello"
			}
		},
		methods:{
			changee:function(data){
				this.message = data;
			}
		},
		components:{
			vue2emit1
		},
	}
</script>