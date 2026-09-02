<template>
    <view>
        <uni-icons type="arrow-right" size="30"></uni-icons>
        <button class="popupBtn" @click="oppup">打开</button>
        <button class="popupBtn" @click="onclose">关闭</button>
        <uni-icons type="arrow-left" size="30"></uni-icons>
        <uni-popup class="popupp" ref="popup" background-color="#fff">
			<view class="popup-content" >
                <text class="text">popup 内容</text>
                <view class="txt">
                    <p>详情：</p>
                    <p class="pp">1.相看我擤谚遥地刘竽斫原存档</p>
                    <p class="pp">2.相看我擤谚遥地刘竽斫原存档</p>
                    <p class="pp">3.相看我擤谚遥地刘竽斫原存档</p>
                    <p class="pp">4.相看我擤谚遥地刘竽斫原存档</p>
                </view>
          </view>
		</uni-popup>


        <uni-badge size="small" :text="40" absolute="rightTop" type="error" max-num="50">
	        <button type="default">右下</button>
        </uni-badge>

        <button class="calendar" @click="oncalendar">日历</button>
        <uni-calendar ref="calendar" :insert="calendarOpen" @change="confirm" lunar="true" range="true"/>
        
        <uni-card title="基础卡片" sub-title="副标题" extra="额外信息" thumbnail="https://qiniu-web-assets.dcloud.net.cn/unidoc/zh/unicloudlogo.png">
	        <text>这是一个带头像和双标题的基础卡片，此示例展示了一个完整的卡片。</text>
        </uni-card>
        <uni-section type="line" title="折叠框">
            <uni-combox label="所在城市" :candidates="candidates" placeholder="请选择所在城市" v-model="city" @input="comboxIn"></uni-combox>
        </uni-section>
        
        <uni-section title="手风琴效果（只会保留一个的打开状态）" type="line">
			<uni-collapse  accordion v-model="accordionVal" @change="change">
				<uni-collapse-item title="手风琴效果">
					<view class="content">
						<text class="text">手风琴效果同时只会保留一个组件的打开状态，其余组件会自动关闭。</text>
					</view>
                    <view class="content">
						<text class="text">手风琴效果同时只会保留一个组件的打开状态，其余组件会自动关闭。</text>
					</view>
				</uni-collapse-item>
				<uni-collapse-item title="手风琴效果">
					<view class="content">
						<text class="text">手风琴效果同时只会保留一个组件的打开状态，其余组件会自动关闭。</text>
					</view>
				</uni-collapse-item>
				<uni-collapse-item title="手风琴效果">
					<view class="content">
						<text class="text">手风琴效果同时只会保留一个组件的打开状态，其余组件会自动关闭。</text>
					</view>
				</uni-collapse-item>
			</uni-collapse>
		</uni-section>

        <uni-countdown :day="1" :hour="1" :minute="12" :second="40"></uni-countdown>
        <uni-countdown color="#FFFFFF" background-color="#00a265" border-color="#00a265" :day="1" :hour="2" :minute="30" :second="0"></uni-countdown>

        <view class="uni-px-5 uni-pb-5">
			<view class="text">多选选中：{{JSON.stringify(checkbox2)}}</view>
			<uni-data-checkbox mode="button" multiple v-model="checkbox2" :localdata="hobby"></uni-data-checkbox>
		</view>


            <uni-section title="本地数据" type="line" padding>
			<uni-data-picker placeholder="请选择班级" popup-title="请选择所在地区" :localdata="dataTree" v-model="classes"
				@change="pickeronchange" @nodeclick="pickeronnodeclick" @popupopened="pickeronpopupopened" @popupclosed="pickeronpopupclosed">
            </uni-data-picker>
		    </uni-section>


        <uni-data-select v-model="selectvalue" :localdata="selectrange" @change="selectchange"></uni-data-select>

        <uni-dateformat date="2021/10/20 22:23:24" :threshold="[0,0]" format="yyyy-MM-dd hh:mm:ss:SSS"></uni-dateformat>

        <uni-datetime-picker
				type="date"
				:value="single"
				start="2026-3-20"
				end="2027-6-20"
				@change="datetimechange"
			/>
 </view>
</template>

<script setup>
import {ref,reactive} from 'vue';
const popup = ref(null);
const calendarOpen = ref(false);
const calendar = ref(null);
const candidates = ref(["a","b","c","d","e"]);
const city = ref("");
const checkbox2 = ref([0]);
const hobby = reactive([{
					text: '足球',
					value: 0
				}, {
					text: '篮球',
					value: 1
				}, {
					text: '游泳',
					value: 2
				}]);
const dataTree= reactive([{
					text: "一年级",
					value: "1-0",
					children: [{
						text: "1.1班",
						value: "1-1"
					},
					{
						text: "1.2班",
						value: "1-2"
					}]
				},
				{
					text: "二年级",
					value: "2-0",
					children: [{
						text: "2.1班",
						value: "2-1"
					},
					{
						text: "2.2班",
						value: "2-2"
					}]
				},
				{
					text: "三年级",
					value: "3-0",
					disable: true
				}]);
const classes = ref('1-2');
const selectrange = reactive([{"value": 0,"text": "篮球"	},{"value": 1,"text": "足球"},{"value": 2,"text": "游泳"}]);
const selectvalue = ref(1);
const single = ref("");



function oppup()
{
    popup.value.open('bottom');
}
function onclose(){
    popup.value.close('bottom');
}

function confirm(e)
{
    console.log(e.fulldate);
}

function oncalendar()
{
    calendarOpen.value=!calendarOpen.value;
}

function comboxIn()
{
    console.log(city.value);
}

function pickeronchange()
{
    console.log(classes.value);
}
function pickeronnodeclick()
{
    console.log(classes.value);
}
function pickeronpopupopened()
{
    console.log("选择弹出时触发");
}
function pickeronpopupclosed()
{
    console.log("选择关闭时触发");
}
function selectchange(e)
{
    console.log(e);
}
function datetimechange(e)
{
    console.log(e);
}
</script>

<style lang="scss" scoped>
    .popupBtn{
        width: 200rpx;
        height: 100rpx;
        padding: 10rpx 20rpx;
        margin: 10rpx;
        display: inline;
    }
    .popupp{
        border-radius: 50rpx 50rpx 0 0;
        width: 90%;
        margin-left: 5%;
        position: absolute;
        background: rgba(180,180,180,0.8);
        bottom: 0;
        overflow: hidden;
        max-height: 50%;
        .popup-content{
            background: rgba(180,180,180,0.8);
            .text{
                display: flex;
                align-items: center;
                justify-content: center;
                color: white;
            }
            .txt{
                margin: 20rpx 50rpx;
                color: white;
                .pp{
                    margin-left: 1rem;
                }
            }
        }
    }

    .text {
		font-size: 12px;
		color: #666;
		margin-top: 5px;
	}
	.uni-px-5 {
	    padding-left: 10px;
	    padding-right: 10px;
	}
	.uni-pb-5 {
	    padding-bottom: 10px;
	}
</style>