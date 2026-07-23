<template>
	<h1>过渡效果</h1>

		<transition name="fade">
			<div v-if="divVisible">
			    This content is sometimes hidden
		    </div>
		</transition>
		<button @click="showDiv">showDiv</button>

		<hr>
</template>

<script>
	export default{
		data(){
			return{
				divVisible:true
			}
		},
		methods:{
			showDiv(){
				this.divVisible = !this.divVisible;
			}
		}
	}
</script>

<style>
	/*.fade-enter-active,.fade-leave-active {
      transition: opacity 0.5s ease;
    }

    .fade-enter-from,.fade-leave-to {
      opacity: 0;
    }*/

	.fade-enter-active {
	  transition: all 0.3s ease-out;
	}

	.fade-leave-active {
	  transition: all 0.8s cubic-bezier(1, 0.5, 0.8, 1);
	}

	.fade-enter-from,
	.fade-leave-to {
	  transform: translateX(20px);
	  opacity: 0;
	}
</style>