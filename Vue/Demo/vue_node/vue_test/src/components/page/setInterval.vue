<template>
	<h1>定时器</h1>
	<!-- <button @click="starttime" :disabled="enabledOpen">开启</button> -->
	<!-- <button @click="endtime" :disabled="enabledStop">停止</button> -->
	
	<button v-if="yesno" @click="starttime">开启</button>
	<button v-else @click="endtime">停止</button>
	
	<p>{{count}}</p>
	
	<br />
	<hr />
</template>

<script>
	export default{
		
		data(){
			return{
				timer: null,
				count:0,
				// enabledOpen:false,
				// enabledStop:true,
				yesno:true,
			}
		},
		//生命周期钩子
		// created() {
		// 	setInterval(()=>{
		// 		this.count++;
		// 	},1000)
		// },
		methods:{
			
			starttime: function(){
				    this.timer =setInterval(()=>{
				    		this.count++;
				    	},1000);
					this.changeYesNo();
					// this.enabledOpen = true;
					// this.enabledStop = false;
			},
			endtime: function()
			{
				clearInterval(this.timer);
				this.timer = null;
				this.count=0;
				this.changeYesNo();
				// this.enabledOpen = false;
				// this.enabledStop = true;
			},
			changeYesNo: function()
			{
				this.yesno = !this.yesno;
			}
		}
	}
</script>

<style>
</style>