<template>
        
        <view class="aa">
                <h1 class="bb">{{h1text}}</h1>
                <slot name="slotname"></slot>
        </view>
        
</template>

<script setup>
import {ref} from 'vue'
const h1text = ref("qqq");

</script>

<style scoped>
.aa{
        .bb{
                color: blueviolet;
        }
}
</style>