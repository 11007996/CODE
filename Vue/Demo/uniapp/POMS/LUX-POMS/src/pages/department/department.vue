<template>
    <view class="department">
        <uni-nav-bar left-icon="left" right-icon="plus" title="部门" @clickRight="onAddDepart" @clickLeft="onNavigator"/>

        <uni-table class="depart-table" border stripe emptyText="暂无更多数据" >
	        <uni-tr class="depart-tr">
	        	<uni-th width="250rpx" class="depart-th depart-code" align="center">部门代码</uni-th>
	        	<uni-th width="300rpx" class="depart-th depart-name" align="center">部门名称</uni-th>
	        	<uni-th width="100rpx" class="depart-th depart-modify" align="center" v-if="authority>2">修改</uni-th>
	        	<uni-th width="100rpx" class="depart-th depart-delete" align="center"  v-if="authority>3">删除</uni-th>
	        </uni-tr>
	        <uni-tr class="depart-tr" v-for="(item,index) in departData" :key=item.departId>
	        	<uni-td class="depart-th" align="center">{{item.departCode}}</uni-td>
	        	<uni-td class="depart-th" align="center">{{item.departName}}</uni-td>
	        	<uni-td class="depart-th" align="center" v-if="authority>2"><uni-icons type="gear" size="20" @click="onModify(index)"></uni-icons></uni-td>
	        	<uni-td class="depart-th" align="center" v-if="authority>3"><uni-icons type="trash" size="20" @click="onDelete(item.departId,index)"></uni-icons></uni-td>
	        </uni-tr>
        </uni-table>
        <uni-popup class="departPopup" ref="departPopup" type="center" :mask-click="false">
            <uni-popup-dialog
                mode="input"
                :title=popupTitle
                :before-close="true"
                @confirm="onDepartPopupConfirm(modifyDepart)"
                @close="onDepartPopupClose">
              <view>
                <text class="departPopup-text">部门代码：</text>
                <input class="departPopup-input" v-model="modifyDepart.departCode" placeholder="输入部门代码" />
                <text class="departPopup-text">部门名称：</text>
                <input class="departPopup-input" v-model="modifyDepart.departName" placeholder="输入部门名称" />
                <view class="departPopup-enabled">
                    <text class="departPopup-text">启用：</text>
                    <switch checked="item.departEnabled=='Y'?true:false" style="transform:scale(0.7)" @change="enabledChange" />
                </view>
              </view>
            </uni-popup-dialog>
        </uni-popup>
    </view>
</template>

<script setup>
    import {ref,reactive} from 'vue'
    import {GetDepartAll,ModifyDepartById,AddDepart,DateleDepart} from '../../api/basedata'
    import { onLoad } from '@dcloudio/uni-app';
    const departPopup = ref(null)
    const addOrModify = ref("");
    const popupTitle = ref("");
    //所有的部门
    //{"departId":10004,"departCode":"IT4","departName":"资讯开发部4","departEnabled":"Y"}
    const departData = reactive([])
    //要修改的部门
    const modifyDepart= reactive({
        "departId":0,
        "departCode":"",
        "departName":"",
        "departEnabled":""
    })
    const authority = ref(4);  //权限大小
    const modifyDepartIndex=ref(null)

    onLoad(async ()=>{
        const res = await GetDepartAll("Y");
        if(res.data.status===0)
        {
            const departments = res.data.data[0];
            departData.length =0;
            for(let i=0;i<departments.length;i++)
            {
                departData[i] = {
                    departId:departments[i].departId,
                    departCode:departments[i].departCode,
                    departName:departments[i].departName,
                    departEnabled:departments[i].enabled
                }
            }
        }
    })

    function onAddDepart()
    {
        addOrModify.value = "Add";
        popupTitle.value = "新增";
        departPopup.value.open('center');
    }
    async function onDelete(departid,index)
    {
        uni.showModal({
            titel:"提示",
            content:"确定删除此部门？",
            success:async function(res){
                try
                {
                    const res =await DateleDepart(departid);
                    if(res.data.status===0)
                    {
                        departData.splice(index, 1);
                        uni.showToast({
                            icon:'success',
	                        title: res.data.msg,
	                        duration: 1000
                        });
                    }
                    else
                    {
                        uni.showModal({
                                title:"错误",
                                content:res.data.msg,
                                showCancel:false
                            })
                    }
                }
                catch(err)
                {
                    uni.showModal({
                        title:"错误",
                        content:err,
                        showCancel:false
                    })
                }
            }
        })
    }
    function onModify(e)
    {
        modifyDepartIndex.value = e;
        addOrModify.value = "Modify";
        popupTitle.value = "修改"
        modifyDepart.departId = departData[e].departId;
        modifyDepart.departCode = departData[e].departCode;
        modifyDepart.departName = departData[e].departName;
        modifyDepart.departEnabled = departData[e].departEnabled;
        departPopup.value.open('center');
    }
    async function onDepartPopupConfirm(e)
    {
        if(addOrModify.value==="Modify")
        {
            const res =await ModifyDepartById({
                departId:e.departId,
                departCode:e.departCode,
                departName:e.departName,
                enabled:e.departEnabled,
                update_empid:uni.getStorageSync("userId")
            })
            if(res.data.status===0)
            {
                departData[modifyDepartIndex.value].departCode = e.departCode;
                departData[modifyDepartIndex.value].departName = e.departName;
                departData[modifyDepartIndex.value].departEnabled = e.departEnabled;
                uni.showToast({
                    icon:'success',
	                title: res.data.msg,
	                duration: 1000
                });
            }
            else
            {
                uni.showModal({
                    title:"错误",
                    content:res.data.msg,
                    showCancel:false
                })
            }
            
        }
        else if(addOrModify.value==="Add")
        {
            if(departData.find(x => x.departCode === e.departCode))
            {
                uni.showModal({
                    title:"错误",
                    content:"部门代码重复，请修改",
                    showCancel:false
                })
                return;
            }
            else if(departData.find(x => x.departName === e.departName))
            {
                uni.showModal({
                    title:"错误",
                    content:"部门名称重复，请修改",
                    showCancel:false
                })
                return;
            }
            const res = await AddDepart({
                departId:0,
                departCode:e.departCode,
                departName:e.departName,
                enabled:e.departEnabled===""?"Y":e.departEnabled,
                update_empid:uni.getStorageSync("userId")
            })
            if(res.data.status===0)
            {
                departData.push({"departId":res.data.data[0],"departCode":e.departCode,"departName":e.departName,"departEnabled":e.departEnabled});
                uni.showToast({
                    icon:'success',
	                title: res.data.msg,
	                duration: 1000
                });
            }
            else
            {
                uni.showModal({
                    title:"错误",
                    content:res.data.msg,
                    showCancel:false
                })
                return;
            }
        }
        departPopup.value.close();
    }
    function onDepartPopupClose()
    {
        departPopup.value.close();
    }
    function onNavigator()
    {
        uni.navigateBack()
    }
    function enabledChange()
    {
        if(modifyDepart.departEnabled === "Y")
        {
            modifyDepart.departEnabled = "N";
        }
        else if(modifyDepart.departEnabled === "N")
        {
            modifyDepart.departEnabled = "Y";
        }
        else if(modifyDepart.departEnabled === ""){
            modifyDepart.departEnabled = "N";
        }
    }
</script>

<style lang="scss" scoped>
@import '@/static/style/base-style.scss';
    .department{
        .depart-table{
            .depart-tr{
                width: auto;
                .depart-th{
                    width: auto;
                } 
                .depart-th.depart-code{
                    max-width: 250rpx;
                }
                .depart-th.depart-name{
                    max-width: 300rpx;
                }
                .depart-th.depart-modify{
                    max-width: 100rpx;
                }
                .depart-th.depart-delete{
                    max-width: 100rpx;
                }
                .depart-th.depart-enabled{
                    max-width: 100rpx;
                }
            }
        }
        .departPopup{
        .departPopup-text{
            margin-bottom: 30rpx;
        }
        .departPopup-input{
            border:1px solid $background-black-4;
            padding: 15rpx 15rpx 15rpx 15rpx;
            margin-top: 10rpx;
            margin-bottom: 10rpx;
            border-radius: 8rpx;
        }
        .departPopup-input:hover{
            border:1px solid $lux-primary-l;
        }
        .departPopup-enabled{
            margin-top: 10rpx;
            display:inline-block;
            align-items: center;
        }
    }
    }
    
</style>