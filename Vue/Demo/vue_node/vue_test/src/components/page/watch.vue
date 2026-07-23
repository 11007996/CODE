<template>
	<h1>监听器</h1>
	<ol>
		<li>当数据发生变化时自动调用</li>
	</ol>
	
	<p>{{number}}</p>
	<button id="but1" @click="numberChange">number</button>
	<button id="but2" @click="ageChange">age</button>
	<!-- <button id="but3" @click="nameChange">name</button> -->
	<hr>
</template>

<script>
	export default{
		data() {
			return{
				number:0,
				user:{
					name:"GG",
					age:19
				}
			}
		},
		methods:{
			numberChange:function(){
				this.number ++;
			},
			ageChange:function(){
				this.user.age ++;
			},
			nameChange:function(){
				this.user.name = this.user.name+this.user.name;
			}
		},
		watch:{
			number(){
				// console.log("number发生变化："+ this.number);
				
				//休息2秒
				setTimeout(()=>{
					console.log("number发生变化："+ this.number);
				},2000)
			},
			// //监听对象的某个属性
			// 'user.age'(){
			// 	console.log("user.age发生变化："+ this.user.age);
			// },
			// //监听回传两个值
			// 'user.name'(newValue,oldValue){
			// 	console.log("user.name发生变化："+ oldValue + "-" + newValue);
			// },
			//监听整个对象需要deep深度监听
			user:{
				handler(newValue,oldValue){
					console.log(newValue,oldValue);
				},
				deep:true
			}
		}
	}
</script>

<style>
	#but1{
			margin-right:10px;
	}
	#but2{
		    margin-left: 5px;
	}
	#but3{
		    margin-left: 5px;
	}
</style>