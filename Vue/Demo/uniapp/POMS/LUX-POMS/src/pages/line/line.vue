<template>
    <view class="line">
        <uni-nav-bar left-icon="left" right-icon="plus" title="线别" @clickRight="onAddLine" @clickLeft="onNavigator"/>

        <uni-table class="line-table" border stripe emptyText="暂无更多数据" >
	        <uni-tr class="line-tr">
	        	<uni-th width="250rpx" class="line-th line-name" align="center">线别名称</uni-th>
	        	<uni-th width="300rpx" class="line-th line-alias" align="center">别名</uni-th>
	        	<uni-th width="300rpx" class="line-th line-desc" align="center">描述</uni-th>
	        	<uni-th width="100rpx" class="line-th line-modify" align="center" v-if="authority>2">修改</uni-th>
	        	<uni-th width="100rpx" class="line-th line-delete" align="center"  v-if="authority>3">删除</uni-th>
	        </uni-tr>
	        <uni-tr class="line-tr" v-for="(item,index) in lineData" :key=item.lineid>
	        	<uni-td class="line-th line-underline" align="center" @click="onView(item.lineid)">
					{{item.linename}}
				</uni-td>
	        	<uni-td class="line-th" align="center">{{item.alias}}</uni-td>
	        	<uni-td class="line-th" align="center">{{item.lineDesc}}</uni-td>
	        	<uni-td class="line-th" align="center" v-if="authority>2"><uni-icons type="gear" size="20" @click="onModify(item.lineid)"></uni-icons></uni-td>
	        	<uni-td class="line-th" align="center" v-if="authority>3"><uni-icons type="trash" size="20" @click="onDelete(index)"></uni-icons></uni-td>
	        </uni-tr>
        </uni-table>
    </view>
</template>

<script setup>
import {ref,reactive} from 'vue'
    const authority = ref(4)
    const lineData = reactive([
        {"lineid":10001,"linename":"D01-1FT-01","alias":"L1","lineDesc":"生产1线","remark":"remark111"},
        {"lineid":10002,"linename":"2线","alias":"L2","lineDesc":"生产2线生产2线","remark":"remark111"},
        {"lineid":10003,"linename":"3线","alias":"L3","lineDesc":"生产3线","remark":"remark111"},
        {"lineid":10004,"linename":"4线","alias":"L4","lineDesc":"生产4线","remark":"remark111"},
    ])

	function onAddLine()
	{
		uni.navigateTo({
			url:"../../pages/line-details/line-details",
			success: (res) => {
    			res.eventChannel.emit('sendData', { opentyle: 'Add' })
  			}
		})
	}
	function onView(e)
	{
		uni.navigateTo({
			url:"../../pages/line-details/line-details",
			success: (res) => {
    			res.eventChannel.emit('sendData', {lineid:e, opentyle: 'View' })
  			}
		})
	}
	function onDelete(e)
	{
		
		lineData.splice(e, 1);
	}
	function onModify(e)
	{
		uni.navigateTo({
			url:"../../pages/line-details/line-details",
			success: (res) => {
    			res.eventChannel.emit('sendData', {lineid:e, opentyle: 'Modify' })
  			},
			fail(err){
				uni.showToast({
					title: err,
                    icon:'none',
	                duration: 3000
				})
			}
		})
	}
	function onNavigator()
	{
		uni.reLaunch({
			url:'../../pages/workbenches/workbenches'
		})
	}
</script>

<style lang="scss" scoped>
@import '@/static/style/base-style.scss';
	.line{
		.line-table{
            .line-tr{
				.line-th.line-name{
					max-width: 180rpx;
				}
				.line-th.line-alias{
					max-width: 100rpx;
				}
				.line-th.line-desc{
					max-width: 200rpx;
				}
				.line-th.line-modify,.line-th.line-delete{
					max-width: 80rpx;
				}
				.line-th.line-underline
				{
					text-decoration: underline;
					color: $lux-primary;
				}
            }
        }
	}
</style>