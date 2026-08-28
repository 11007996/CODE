<template>
    <view class="text">easycom自动导入组件</view>
    <button @click="toast">弹窗提示</button>
    <view>
        <uni-forms class="uniform" ref="form" :rules="rules" :model="formData" name="userform">
        <uni-forms-item label="姓名" name="name">
            <input type="text" name="name" v-model="formData.name" placeholder="请输入姓名" />
        </uni-forms-item>
        <uni-forms-item label="年龄" name="age">
            <uni-easyinput type="text" name="age" v-model="formData.age" placeholder="请输入年龄" />
        </uni-forms-item>
        <button @click="toast">提交</button>
        <button @click="toast">重置</button>
    </uni-forms>
    </view>

    
    
    <button @click="$emit('emitE','emit参数1')">emits事件</button>
    <!-- <button @click="emitsEvent">emits事件</button> -->
</template>

<script setup>
    import { errorMessages } from '@vue/compiler-core';
    import {ref,defineExpose} from 'vue';
    const formData= ref({
        name:"",
        age:""
    });

    const emits = defineEmits(["emitE"]);
    defineExpose({
        name:"nameExpose",
        callbackFun,
    })

    function emitsEvent()
    {
        emits("emitE","emit参数");
    }

    function toast(){
        /*   //提示框
        uni.showToast({
            icon:"none",   //设为none可以显示更多文字，否则会被截断
			title: "纵向丰才得手十刘纵向丰才得手十刘纵向丰才得手十刘纵向丰才得手十刘纵向丰才得手十刘纵向丰才得手十刘纵向丰才得手十刘纵向丰才得手十刘",
            duration:2000,
            showCancel: false,
            mask:true
        });
        // 不设置延时，弹出框会一闪而过
        setTimeout(() => {
            uni.hideLoading();
        }, 2000);
        */

        /*  //加载转圈弹窗
        uni.showLoading({
            title:"加载中...",
            success:callbackFun
        });
        setTimeout(() => {
            uni.hideLoading();
        }, 2000);
        */

        /*   //回调函数
        uni.showModal({
            title:"提示",
            content:"文字内容",
            showCancel:true,  //显示取消按钮
            cancelText:"取消",
            cancelColor:"#a66",
            confirmText:"是",
            confirmColor:"FFF",
            editable:true,   //显示输入框
            placeholderText:"请输入文字",
            success:callbackFun   //回调函数
        })
        */

        /*   //从底部向上弹出操作菜单
        uni.showActionSheet({
            title:"点击",
            itemList:["AAAAA","BBBBBB","CCCCC","DDDDD"],
            itemColor:"#a66",
            success:function(res){console.log(res)}
        })*/

        //设置导航栏标题
        uni.setNavigationBarTitle({
            title:"第一页"
        })
        /*//设置导航标题颜色
        uni.setNavigationBarColor({
            frontColor:"#000000",   //仅支持 #ffffff 和 #000000
            backgroundColor:"#e4e"
        })
        */
        /*//设置导航标题转圈
        uni.showNavigationBarLoading();
        setInterval(() => {
            uni.hideNavigationBarLoading()
        }, 2000);*/

        /*//滚动页面
        uni.pageScrollTo({
	        scrollTop: 0,   //滚动到页面顶部，单位px
	        duration: 300,
            //selector:".text",  //元素选择器，滚动到指定位置
            success:callbackFun
        });
        */

        //监听窗口大小
        // uni.onWindowResize(resize);

        /*//获取文件信息
        uni.getFileInfo({
            filePath:"../../src/static/logo.png",
            success:(res)=>{
                console.log(res);
            }
        });
        */
    }

    function resize(res)
    {
        console.log('变化后的窗口宽度=' + res.size.windowWidth);
        console.log('变化后的窗口宽度=' + res.size.windowHeight);
    }

    function callbackFun()
    {
        console.log("回调成功");
        /*
        uni.showToast({
            icon:"success",
            title:"回调成功"
        })
            */
    }
</script>

<style lang="scss" scoped>
    .text{
        color:cadetblue
    }
    .uniform{
        input{
            widows: 300rpx;
            height: 50rpx;
        }
    }
</style>