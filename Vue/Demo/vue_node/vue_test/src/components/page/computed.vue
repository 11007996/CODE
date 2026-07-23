<template>
	<h1>计算属性</h1>
	<ol>
		<li>计算属性介于data和方法之间，可以像访问data对象一样访问算计属性</li>
		<li>计算属性会被缓存，多次引用只计算一次，而方法是多次引用就计算多次，效率更高</li>
		<li>计算属性可以增加set/get对象，修改结果值</li>
	</ol>
	
	<div class="app">
		<!-- <p>数字的总和是：{{numberTatal}}</p> -->
		<p>数字的总和是：{{numberTatal2+=5}}</p>
		<p>数字的总和是：{{chartCat}}</p>
		<p>{{total}}</p>
		<button @click="num_sum">计算</button>
		<p v-for="n in numbers">{{n}}</p>
		
	</div>

</template>

<script>
	export default{
		data(){
			return{
				numbers:[3,5,2],
				chars:['h','e','l','l','o'],
				a:1,
				b:2,
				total:null
			}
		},
		methods:{
			num_sum(){
				this.total= this.a+this.b
			}
		},
		computed:{
			numberTatal(){
				return this.numbers.reduce((sum,val)=> sum + val);
			},
			numberTatal2:{
				get(){
					return this.numbers.reduce((sum,val)=> sum + val);
				},
				set(newValue){
					const oldValue = this.numberTatal2;
					const difference = newValue - oldValue;
					// this.numbers.push(difference);
					this.a = difference;
					
				}
				
			},
			chartCat(){
				return this.chars.reduce((sum,val)=> sum + val);
			}
		}
	}
</script>

<style>
</style>