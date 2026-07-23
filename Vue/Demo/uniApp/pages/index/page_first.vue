<template>
	<h3 :class=className>第一个页面</h3>
	<button @click="show">点击</button>
</template>

<script>
	export default{
		data(){
			return {
				className:"testh",
			}
		},
		methods:{
			show:function()
			{
				if(this.className=="testt")
				{
					this.className="testh";
				}else{
					this.className="testt";
				}
				
				
			}
		}
	}
</script>

<style>
	button{
		width: 300rpx;
		height: 100rpx;
		line-height: 100rpx;
		font-family:sans-serif;
		font-size: 1.6rem;
		text-align: center;
	}
	.testh{
		margin: 50rpx;
		border-radius: 50rpx;
		text-align: center;
		background-color:olive;
		color: white;
	}
	.testt{
		margin: 50rpx;
		text-align: center;
		background-color:darkgray;
		color:white;
	}
</style>