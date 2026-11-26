<template>
    <view class="index-tasks" :style="{ backgroundColor: bgColor }" @click="onClickTask">
        <p class="title">{{ props.title }}</p>
        <p class="content">{{props.content}}</p>
        <view class="annex">
            <text >{{ props.publication_date }}</text>
			<uni-rate class="rate" :readonly="true" size="15" :value=level />
        </view>
    </view>
</template>

<script setup>
// import { SassString } from 'sass';
import {ref,reactive, computed} from 'vue'
    const props = defineProps({
        id:{
                    type:String,
                    default:"",
                    required:true
        },
        title:{
                    type:String,
                    default:"",
                    required:true
        },
        content:{
                type:String,
                default:"",
                required:true
        },
        publication_date:{
                type:String,
                default:null,
                required:true
        },
        level:{
                type:Number,
                default:"",
                required:true
        },
        timespan:{
            type:Number,
            default:"",
            required:true
        }
    })

    const bgColor = computed(() => {
        if (props.timespan <= 1) {
            return '#ffe6e6'  // 红色 - 已超时
        } else if (props.timespan <= 2) {
            return '#fff4e6' // 橙色 - 剩余不足 1 天
        } else {
            return '#f5f5f5' // 默认灰色
        }
    })


    function onClickTask()
    {
        uni.navigateTo({
            url:"../../pages/task-details/task-details"
        })
    }

</script>

<style lang="scss" scoped>
@import '@/static/style/base-style.scss';
    .index-tasks{
        height: 200rpx;
        border: 1rpx solid $lux-primary-l;
        background-color: $background-black-1;
        box-shadow: 0px 2px 5px 1px $shadow-black-1;
        border-radius: 10rpx;
        margin-bottom: 20rpx;
        position:relative;
        .title{
            font-weight: 500;
            font-size: large;
            margin:0rpx 25rpx 0rpx 25rpx;
        }
        .content{
            word-break: break-all;
            overflow: hidden;
            display: -webkit-box;
            -webkit-line-clamp: 2;
            -webkit-box-orient: vertical;
            margin:0rpx 25rpx 25rpx 25rpx;
        }
        .annex{
            display:flex;
            align-items: center;
            position:absolute;
            bottom: 10rpx;
            margin-left: 25rpx;
            text{
                margin-right:30rpx; 
                font-size:25rpx;
                color: gray;
            }
        }
    }
</style>