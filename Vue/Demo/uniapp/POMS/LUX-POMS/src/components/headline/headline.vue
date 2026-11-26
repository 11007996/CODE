<template>
    <view class="headline">
        <text>{{ props.head }}</text>
        <text class="more" @click="getMore">{{ props.annex }}</text>
    </view>
    <view>
    </view>
</template>

<script setup>
import {defineEmits} from 'vue'
const props = defineProps({
    head:{
                type:String,
                default:"",
                required:true
    },
    annex:{
            type:String,
            default:"",
            required:true
    }
})

const emit = defineEmits(['onMore'])
function getMore()
{
    emit('onMore')
}
</script>

<style lang="scss" scoped>
@import '@/static/style/base-style.scss';
    .headline
    {
        height: 70rpx;
        border: 1rpx solid $lux-primary-l;
        border-radius: 10rpx 10rpx 0rpx 0rpx;
        background-color: $lux-primary-ll;
        display:flex;
        align-items: center;
        justify-content: space-between;  /* 两端对齐 */
        padding: 0 25rpx;
        box-sizing: border-box;
        color: $lux-primary-d;
        .more{
            text-decoration: underline;
            text-underline-offset: 3px;
        }
    }
</style>