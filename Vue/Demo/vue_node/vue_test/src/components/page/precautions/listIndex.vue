<template>
	<div id="addd">
		<h2>2.不能用索引来设置数组元素</h2>
		<h3 class="remark">说明：</h3>
		<ol class="remark">
			<li>数组元素要用set方法进行赋值</li>
		</ol>
		<p>数组元素</p>
		<ul>
			<li v-for="dog in dogs">{{dog}}</li>
		</ul>
		<button @click="setList">响应</button>
	</div>
	
	<hr>
</template>

<script>
	export default{
			data(){
				return{
					dogs:['Rex','Rover','Henrietta','Alan']
			}
		},
		methods:{
			setList:function(){
				//splice方法删除元素后再添加
				this.dogs.splice(2,1,'Bob');
			}
		}
	}
</script>

<style>
</style>