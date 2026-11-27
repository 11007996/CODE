<template>
   <view class="index">
    <view class="notice">
      <headline class="headline" :head="'消息公告'" :annex="'详情'" @onMore="showNotice"></headline>
      <uni-popup class="notice-popup" ref="popup" type="message">
        <view class="popup-border">
          <view class="popup-title">消息公告</view>
        <view class="popup-content" v-html="popupNotice"></view>
        </view>
        
      </uni-popup>
      <view class="notice-contents">
      <swiper class="swiper" circular vertical="true" autoplay=true interval=5000
				duration=500>
				<swiper-item v-for="(item,index) in notices"  :key=index>
					<view class="swiper-item uni-bg-red">{{item.content}}</view>
				</swiper-item>
			</swiper>
    </view>
    </view>
    <view class="task">
      <headline class="headline" :head="'任务列表'" :annex=tasks.length @onMore="showTask"></headline>
      <view class="task-contents">
        <scroll-view :scroll-top="scrollTop" scroll-y="true" class="task-scroll-Y">
					<index-tasks v-for="item in tasks" id="tasks1" :key=item.id :id="item.id" :title=item.title :content=item.content :publication_date=item.publication_date :username=item.username :level=item.level :timespan=item.timespan></index-tasks>
				</scroll-view>
      </view>
    </view>
    <!-- <my-tabbar class="my-tabbar"></my-tabbar> -->

   </view>
</template>

<script setup>
import { onNavigationBarButtonTap,onLoad } from '@dcloudio/uni-app';
import {test} from '../../api/base'
import{ref,reactive,computed} from 'vue'
const popup = ref(null);

const notices = reactive(
  [{"id":1,"content":"你可能有很多次想要在一个组件的根元素上直接监听一个原生事件"},
    {"id":2,"content":"你可能有很多次想要在一个组件的根元素上直接监听一个原生事件"},
    {"id":3,"content":"你可能有很多次想要在一个组件的根元素上直接监听一个原生事件"},
    {"id":3,"content":"你可能有很多次想要在一个组件的根元素上直接监听一个原生事件"},
    {"id":3,"content":"你可能有很多次想要在一个组件的根元素上直接监听一个原生事件"},

  ]
);

const tasks = reactive(
  [{"id":1,"title":"巡检","content":"你可能有很多次想要在一个组件的根元素上直接监听一个原生事件你可能有很多次想要在一个组件的根元素上直接监听一个原生事件","publication_date":"2026-10-11","username":"郭明辉","level":3,"timespan":1},
    {"id":2,"title":"巡检","content":"你可能有很多次想要在一个组件的根元素上直接监听一个原生事件","publication_date":"2026-10-10","username":"郭明辉","level":2,"timespan":2},
    {"id":3,"title":"巡检","content":"你可能有很多次想要在一个组件的根元素上直接监听一个原生事件","publication_date":"2026-10-10","username":"郭明辉","level":2,"timespan":3},
    {"id":4,"title":"巡检","content":"你可能有很多次想要在一个组件的根元素上直接监听一个原生事件","publication_date":"2026-10-10","username":"郭明辉","level":2,"timespan":4},
    {"id":5,"title":"巡检","content":"你可能有很多次想要在一个组件的根元素上直接监听一个原生事件","publication_date":"2026-10-10","username":"郭明辉","level":2,"timespan":5},
    {"id":6,"title":"巡检","content":"你可能有很多次想要在一个组件的根元素上直接监听一个原生事件","publication_date":"2026-10-10","username":"郭明辉","level":3,"timespan":6},
  ]
);
  onLoad(()=>{
    //验证JWT
  })


  const popupNotice = computed(() => {
    let noticeStr="";
    for(var i =0;i<notices.length;i++)
    {
      noticeStr = noticeStr +"<p>"+ notices[i].content + "</p><br>";
    }
    return noticeStr;
  })

  function showNotice()
  {
    popup.value.open('center');
  }

</script>

<style lang="scss" scoped>
@import '@/static/style/base-style.scss';
  .index{
    .notice{
      margin:1rpx 25rpx 25rpx 25rpx;
      background-color: $background-black-1;
      box-shadow: 0px 2px 5px 1px $shadow-black-1;
      border-radius: 0 0 25rpx 25rpx;
      flex-shrink: 0;
      .notice-contents{
        border-width:0rpx 1rpx 1rpx 1rpx;
        margin-top: 0;
        border-radius: 0 0 20rpx 20rpx;
        border-style: solid;
        border-color: $lux-primary-l;
        overflow: hidden;
        max-height: 300rpx;
        .swiper{
          margin:10rpx 25rpx 25rpx 25rpx;
        }
      }
    .notice-popup{
      .popup-border{
        background-color: white;
        margin:25rpx;
        border-radius: 20rpx;
        overflow: hidden;
        .popup-title{
          display: flex;
          justify-content: center;
          align-items: center;
          height: 100rpx;
          width: 100%;
          border-bottom:1px solid gray;
          font-size: 40rpx;
          color:$lux-primary;
          background-color: $background-black-1;
        }
        .popup-content{
        padding: 50rpx;
        max-height: 500rpx;
        min-height: 100rpx;
        overflow:scroll;
        background-color: $background-black-2;
      }
      }
      
    }
    }
    .task{
      margin-top: 1rpx;
      margin-left: 25rpx;
      margin-right:25rpx;
      margin-bottom: 0rpx;
    }
  }
</style>
