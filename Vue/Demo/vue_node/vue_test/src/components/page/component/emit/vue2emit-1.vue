<template>
    <button @click="change">点击</button>
</template>
<script>
    export default{
            methods:{
                change:function(){
                    this.$emit('childEmit',"子组件传递数据");
                }
            }
    }
</script>