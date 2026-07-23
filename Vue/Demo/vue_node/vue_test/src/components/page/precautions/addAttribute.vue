<template>
	<div id="addd">
		<h2>1.增加响应式属性</h2>
		<h3 class="remark">说明：</h3>
		<ol class="remark">
			<li>初始化后增加的额外属性</li>
		</ol>
		<p>原始属性：{{text}}</p>
		//虽然可用，但浏览器会发布警告
		<p>后加属性：{{name}}</p>
		<button @click="textChange">响应</button>
	</div>
	<hr>
</template>

<script>
	export default{
		data(){
			return{
				text:"原始属性",
			}
		},
		methods:{
			textChange: function(){
				this.text = "响应属性";
				this.name = "后加属性"
			}
		}
	}
</script>

<style>
	.remark{
		 font-style:italic;
		 color:#aa0
	}
</style>