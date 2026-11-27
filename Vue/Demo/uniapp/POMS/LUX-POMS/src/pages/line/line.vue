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
	        	<uni-td class="line-th" align="center" v-if="authority>3"><uni-icons type="trash" size="20" @click="onDelete(item.lineid,index)"></uni-icons></uni-td>
	        </uni-tr>
        </uni-table>
    </view>
</template>

<script setup>
import {ref,reactive} from 'vue'
import {DeleteLine,GetLineAll} from '../../api/basedata'
import { onLoad } from '@dcloudio/uni-app';
    const authority = ref(4)
	const lineData = reactive([])
	onLoad(async ()=>{
		const res = await GetLineAll({lineEnabled:"Y"});
		if(res.data.status===0)
		{
			// console.log(res.data.data[0]);
			let lines = res.data.data[0]
			lineData.length=0;
			for(let i =0;i<lines.length;i++)
			{
				lineData[i]={
					"lineid":lines[i].lineId,
					"linename":lines[i].lineName,
					"alias":lines[i].lineAlias,
					"lineDesc":lines[i].lineDesc
				}
			}
		}

	})


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
	async function onDelete(lineid,index)
	{
		await uni.showModal({
			title:"提示",
			content: "确定删除此线别？",
			success:async function(res)
			{
				if(res.confirm)
				{
					try
					{
						const res =await DeleteLine(lineid)
						if(res.data.status===0)
						{
							lineData.splice(index, 1);
						}
						else
						{
							uni.showModal({
								title:"错误",
								content: res.data.msg
							})
						}
					}
					catch(err)
					{
						uni.showModal({
							title:"错误",
							content: err
						})
					}
				}
			}
		})
		
		
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