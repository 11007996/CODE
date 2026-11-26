<template>
    <view class="mine-info">
        <view class="items1">
            <view class="mine-item-pwd" @click="toPwd">修改密码</view>
        </view>
    </view>
</template>

<script setup>
    function toPwd(){
        console.log("safe");
        uni.navigateTo({
            "url":"../../pages/mine-change-pwd/mine-change-pwd"
        })
    }
</script>

<style lang="scss" scoped>
@import '@/static/style/base-style.scss';
    .mine-info{
        margin: 25rpx 0rpx 25rpx 0rpx;
        background-color: $background-black-1;
        .items1{
            .mine-item-pwd{
                padding: 20rpx;
                border-bottom: 1px solid $background-black-3;
            }
            .mine-item-pwd:last-child{
            border-bottom:none;
        }
        }
    }
</style>