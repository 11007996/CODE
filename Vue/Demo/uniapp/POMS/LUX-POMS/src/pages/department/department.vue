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
	        	<uni-td class="depart-th" align="center" v-if="authority>3"><uni-icons type="trash" size="20" @click="onDelete(index)"></uni-icons></uni-td>
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
              </view>
            </uni-popup-dialog>
        </uni-popup>
    </view>
</template>

<script setup>
    import {ref,reactive} from 'vue'
    const departPopup = ref(null)
    const addOrModify = ref("");
    const popupTitle = ref("");
    //所有的部门
    const departData = reactive([
        {"departId":10001,"departCode":"IT1","departName":"资讯开发部1"},
        {"departId":10002,"departCode":"IT2","departName":"资讯开发部2"},
        {"departId":10003,"departCode":"IT3","departName":"资讯开发部3"},
        {"departId":10004,"departCode":"IT4","departName":"资讯开发部4"}
    ])
    //要修改的部门
    const modifyDepart= reactive({
        "departCode":"",
        "departName":""
    })
    const authority = ref(4);  //权限大小
    const modifyDepartIndex=ref(null)

    function onAddDepart()
    {
        addOrModify.value = "Add";
        popupTitle.value = "新增";
        departPopup.value.open('center');
    }
    function onDelete(e)
    {
        departData.splice(e, 1);
    }
    function onModify(e)
    {
        modifyDepartIndex.value = e;
        addOrModify.value = "Modify";
        popupTitle.value = "修改"
        modifyDepart.departCode = departData[e].departCode;
        modifyDepart.departName = departData[e].departName;
        departPopup.value.open('center');
    }
    function onDepartPopupConfirm(e)
    {
        if(addOrModify.value==="Modify")
        {
            departData[modifyDepartIndex.value].departCode = e.departCode;
            departData[modifyDepartIndex.value].departName = e.departName;
        }
        else if(addOrModify.value==="Add")
        {
            if(departData.find(x => x.departCode === e.departCode))
            {
                showToast("部门代码重复，请修改");
                return;
            }
            else if(departData.find(x => x.departName === e.departName))
            {
                showToast("部门名称重复，请修改");
                return;
            }
            departData.push({"departId":10004,"departCode":e.departCode,"departName":e.departName})
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
    function showToast(str){
        uni.showToast({
            icon:'error',
	        title: str,
	        duration: 2000
        });
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
    }
    }
    
</style>