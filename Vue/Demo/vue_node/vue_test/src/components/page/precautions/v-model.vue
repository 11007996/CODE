<template>
	<div id="addd">
		<h2>3.数据双向绑定</h2>
		<h3 class="remark">说明：</h3>
		<ol class="remark">
			<li>v-model指令进行双向绑定</li>
		</ol>
		<p>{{context}}</p>
		<p>{{*context}}</p>  <!-- 只渲染一次 -->
		<input type="text" v-model="context"/>
		<p>单选框：{{boxcheck}}</p>
		<div>
			<label><input type="radio"  v-model="boxcheck" value=1>11</label>
			<label><input type="radio"  v-model="boxcheck" value=2>22</label>
			</div>
	</div>
	<hr>
</template>

<script>
	export default{
		data(){
			return{
				context:null,
				boxcheck:null,
			}
		}
	}
</script>
	
<style>
</style>