<template>
  <h1>局部自定义指令</h1>
  <ol>
    <li>指令前加v-</li>
    <li>生命周期钩子不能访问DATA数据</li>
  </ol>
	<p id="idd" v-blink>自定义闪烁指令</p>
  <hr>
</template>
<script>
	export default {
  directives: {
    'blink': {
      mounted:function(el,binding,vnode) {
        let isVisible = false;
        let num =0;
      	setInterval(()=>{	
          isVisible = !isVisible;
        el.style.visibility = isVisible ? 'visible' :'hidden';
        el.style.color = num % 4 == 0 ? 'black' : 'blue';
        num = num+1;
    },500);
      }
    }
  }
};
</script>
