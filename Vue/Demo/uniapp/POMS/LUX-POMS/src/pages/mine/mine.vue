<template>
    <view class="mine">
        <view class="business-card">
            <view class="username">{{businessCard.userName}}</view>
            <view class="usernodepart">
                <view class="userno">{{businessCard.userNo}}</view>
                <view class="userdepart">{{businessCard.department}}</view>
            </view>
            <view class="useremail">{{businessCard.email}}</view>
        </view>
        <view class="Navigation-tab">
            <view class="tab-item tab-item-click" @click="componentName='task'">我的任务</view>
            <view class="tab-item tab-item-click" @click="componentName='publish'">我的发布</view>
            <view class="tab-item tab-item-click" @click="componentName='info'">我的资料</view>
        </view>
        <view>
            <view v-if="componentName=='task'">
                <mine-task></mine-task>
            </view>
            <view v-else-if="componentName=='publish'">
                <mine-publish></mine-publish>
            </view>
            <view v-else-if="componentName=='info'">
                <mine-info></mine-info>
            </view>
        </view>
        
    </view>
</template>

<script setup>
    import{ref,reactive} from 'vue'
    const businessCard = reactive({
        "userNo":"11007996",
        "userName":"郭明辉",
        "department":"资讯开发部",
        "email":"Minghui.Guo2@luxshare-ict.com"
    })
    const componentName=ref("task")

</script>

<style lang="scss" scoped>
@import '@/static/style/base-style.scss';
    .mine{
        margin:25rpx 25rpx 25rpx 25rpx;
        .business-card{
            height:200rpx;
            background-color: $background-black-1;
            padding: 35rpx;
            border-radius: 20rpx 20rpx 0rpx 0rpx;
            .username{
                font-size:50rpx;
            }
            .usernodepart{
                display:flex;
                align-items: center;
                margin-top: 20rpx;
                .userno{
                    margin-right: 50rpx;
                }
            }
        }
        .Navigation-tab{
            display:flex;
            align-items: center;
             justify-content: space-between;
             .tab-item{
                display: flex;
                align-items: center;
                justify-content: center;
                width:calc(100%/3);
                height:70rpx;
                margin-top: 10rpx;
             }
             .tab-item:hover{
                border-bottom: 2px solid $lux-primary;
                background-color: $background-black-1;
             }

        }
    }
</style>