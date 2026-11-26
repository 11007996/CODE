<template>
    <view class="task-details">
        <view class="title">
            <text class="title-text">{{ taskInfo.title }}</text>
            <text class="title-date">{{ taskInfo.publication_date }}</text>
        </view>
        <view class="task-content">{{ taskInfo.content }}</view>
        <view class="info">
            <view class="info-item">
                <view class="info-title">需求时间:</view>
                <view class="info-contend">{{taskInfo.estimated_time}}</view>
            </view>
            <view class="info-item">
                <view class="info-title">发布人员:</view>
                <view class="info-contend">{{taskInfo.publication_user}}</view>
            </view>
            <view class="info-item">
                <view class="info-title">优先等级:</view>
                <view class="info-contend">
                    <uni-rate class="rate" :readonly="true" size="20" :value=taskInfo.level />
                </view>
            </view>
            <view class="info-item">
                <view class="info-title">流程状态:</view>
                <view class="info-contend">{{taskInfo.status}}</view>
            </view>
        </view>
        <view class="task-complete">
            <uni-section class="uploadImg" title="上传图片" type="line">
				<uni-file-picker limit="2" sizeType="['compressed']" :image-styles="pickerStyle"></uni-file-picker>
		    </uni-section>
            <uni-section title="反馈" subTitle="" type="line" padding>
			    <uni-easyinput type="textarea" v-model="value" placeholder="请输入内容"></uni-easyinput>
		    </uni-section>

            <view class="btn-commit" @click="onCommit">提交</view>
        </view>
    </view>
</template>



<script setup>
import {ref,reactive} from 'vue'
const taskInfo=reactive({
        "id":1,
        "title":"巡检",
        "content":"你可能有很多次想要在一个组件的根元素上直接监听一个原生事件你可能有很多次;\n你可能有组件的根元素上直接监听一个原生事件你可能有很多次\n你可能有很多次想要在一个组件的根元素上直接监听一个原生事件你可能有很多次;\n你可能有组件的根元素上直接监听一个原生事件你可能有很多次",
        "publication_date":"2026-10-10",
        "estimated_time":"2026-10-12  08:00:20",
        "publication_user":"郭明辉",
        "status":"ongoing",
        "level":3
    })
const pickerStyle = reactive({
    "height": "200rpx",
	"width": "200rpx",
	"border":{
		"color":"#eee",		
		"width":"1px",		
		"style":"solid", 	
		"radius":"10rpx"
	}
})
</script>

<style lang="scss" scoped>
@import '../../static/style/base-style.scss';
    .task-details{
        margin:0rpx 25rpx 0rpx 25rpx;
        .title{
            // margin-top: 30rpx;
            margin-bottom: 20rpx;
            // background-color: $background-black-1;
            border-bottom: 2rpx solid $lux-primary-l;
            display:flex;
            align-items: end;
            justify-content: space-between;
            .title-text{
                font-size: 50rpx;
                padding:20rpx 25rpx 0rpx 25rpx;
                color:$lux-primary-l;
            }
            .title-date{
                font-size: 50rpx;
                padding:20rpx 25rpx 5rpx 25rpx;
                font-size:30rpx;
                color: gray;
            }
        }
        .task-content{
            white-space: pre-line;
            max-height: 400rpx;
            min-height: 200rpx;
            padding:25rpx;
            // border: 1rpx solid $lux-primary-l;
            border-radius: 25rpx;
            // background-color: $background-black-1;
            // box-shadow: 0px 2px 5px 1px $shadow-black-1;

        }
        .info{
            margin-top: 20rpx;
            .info-item{
                display:flex;
                align-items: center;
                height:70rpx;
                margin-left:30rpx;
                .info-title{
                    margin-right: 30rpx;
                    
                }
            }
        }
        .task-complete{
            .uploadImg{
                margin-top: 20rpx;
            }

            .btn-commit{
                margin:20rpx 20rpx 20rpx 20rpx;
                display:flex;
                justify-content: center;
                align-items: center;
                background-color: $lux-primary;
                height: 80rpx;
                border-radius: 10rpx;
                color:white;
            }
            
        }
    }
</style>