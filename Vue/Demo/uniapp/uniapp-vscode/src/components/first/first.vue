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
        
        <!-- <uni-section title="手风琴效果（只会保留一个的打开状态）" type="line">
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
		</uni-section> -->

        <uni-countdown :day="1" :hour="1" :minute="12" :second="40"></uni-countdown>
        <uni-countdown color="#FFFFFF" background-color="#00a265" border-color="#00a265" :day="1" :hour="2" :minute="30" :second="0"></uni-countdown>

        <view class="uni-px-5 uni-pb-5">
			<view class="text">多选选中：{{JSON.stringify(checkbox2)}}</view>
			<uni-data-checkbox mode="button" multiple v-model="checkbox2" :localdata="hobby"></uni-data-checkbox>
		</view>


            <!-- <uni-section title="本地数据" type="line" padding>
			<uni-data-picker placeholder="请选择班级" popup-title="请选择所在地区" :localdata="dataTree" v-model="classes"
				@change="pickeronchange" @nodeclick="pickeronnodeclick" @popupopened="pickeronpopupopened" @popupclosed="pickeronpopupclosed">
            </uni-data-picker>
		    </uni-section> -->


        <uni-data-select v-model="selectvalue" :localdata="selectrange" @change="selectchange"></uni-data-select>

        <uni-dateformat date="2021/10/20 22:23:24" :threshold="[0,0]" format="yyyy-MM-dd hh:mm:ss:SSS"></uni-dateformat>

        <uni-datetime-picker
				type="date"
				:value="single"
				start="2026-3-20"
				end="2027-6-20"
				@change="datetimechange"
			/>

	<!-- <view>
		<button @click="showDrawer" type="primary">右侧弹出 显示Drawer</button>
		<uni-drawer ref="showRight" mode="right" :mask-click="false">
			<scroll-view style="height: 100%;" scroll-y="true">
				<button @click="closeDrawer" type="primary">关闭Drawer</button>
				<view v-for="item in 60" :key="item">可滚动内容 {{ item }}</view>
			</scroll-view>
		</uni-drawer>
	</view> -->

	<uni-section title="图标" subTitle="使用 prefixIcon / suffixIcon 属性 ,可以自定义输入框左右侧图标" type="line" padding>
			<uni-easyinput prefixIcon="search" v-model="easyinputvalue" placeholder="左侧图标" @iconClick="easyinputClick"></uni-easyinput>
	</uni-section>

	<!-- <uni-fab
			:pattern="fabpattern"
			:content="fabcontent"
			:horizontal="horizontal"
			:vertical="vertical"
			:direction="direction"
			@trigger="fabtrigger"
			@fabClick="fabClick"
		></uni-fab> -->
	

	<uni-fav :checked="favchecked" class="favBtn" circle="false" bgColor="#dd524d" bgColorChecked="#007aff" @click="favonClick"/>


	<!-- <uni-file-picker 
	v-model="imageValue" 
	fileMediatype="image" 
	mode="grid" 
	@select="fileselect" 
	@progress="fileprogress" 
	@success="filesuccess" 
	@fail="filefail" /> -->


	<!-- <view class="">
		<uni-forms ref="formRef" :modelValue="formData" :rules="formRules" label-width="120rpx">
			<uni-forms-item label="姓名" name="name">
				<uni-easyinput type="text" v-model="formData.name" placeholder="请输入姓名" />
			</uni-forms-item>
			<uni-forms-item label="年龄" name="age">
				<input type="text" v-model="formData.age" placeholder="请输入年龄" />
			</uni-forms-item>
			<uni-forms-item required name="hobbyy" label="兴趣爱好">
				<uni-data-checkbox multiple v-model="formData.hobbyy" :localdata="hobby"/>
			</uni-forms-item>
		</uni-forms>
		<button @click="submitForm">Submit</button>
		<button @click="resetForm">Exit</button>
	</view> -->

	<!-- <uni-goods-nav :fill="true"  :options="options" :buttonGroup="buttonGroup"  @click="navonClick" @buttonClick="navbuttonClick" /> -->

	<!-- <uni-grid :column="4">
		<uni-grid-item>
			<text class="text">textgrid1</text>
		</uni-grid-item>
		<uni-grid-item>
			<text class="text">textgrid1</text>
		</uni-grid-item>
		<uni-grid-item>
			<text class="text">textgrid1</text>
		</uni-grid-item>
		<uni-grid-item>
			<text class="text">textgrid1</text>
		</uni-grid-item>
	</uni-grid> -->

	<!-- <uni-group title="分组1" top="20">
    	<view>分组1 的内容</view>
    	<view>分组1 的内容</view>
	</uni-group>
	<uni-group title="分组2">
    	<view>分组2 的内容</view>
    	<view>分组2 的内容</view>
	</uni-group> -->

		<!-- <view class="indexed">
			<uni-indexed-list :options="indexedlist" :show-select="true" @click="indexedbindClick"></uni-indexed-list>
		</view> -->

		<!-- <uni-link href="https://uniapp.dcloud.io/" text="https://uniapp.dcloud.io/" fontSize="20"></uni-link> -->


	<!-- <uni-list>
		<uni-list-item title="列表文字" note="列表描述信息" :show-badge="true" badge-text="12"></uni-list-item>
		<uni-list-item :disabled="true" title="列表文字" note="列表禁用状态"></uni-list-item>
		<uni-list-item title="列表左侧带略缩图" note="列表描述信息" thumb="https://qiniu-web-assets.dcloud.net.cn/unidoc/zh/unicloudlogo.png" thumb-size="lg" rightText="右侧文字"></uni-list-item>
 		<uni-list-item :show-extra-icon="true" :extra-icon="extraIcon1" title="列表左侧带扩展图标" ></uni-list-item>
	</uni-list> -->

	<view>
		<uni-load-more status="no-more"></uni-load-more>
		<uni-load-more status="more" iconType="auto"></uni-load-more>
	</view>
	
	<!-- <view class="box-bg">
        <uni-nav-bar
		  dark
          shadow
          left-icon="left"
          title="开启阴影"
          @clickLeft="navback"
        />
		</view> -->

		<!-- <uni-notice-bar text="[多行] 这是 NoticeBar 通告栏，这是 NoticeBar 通告栏，这是 NoticeBar 通告栏，这是 NoticeBar 通告栏"></uni-notice-bar> -->
		<!-- <uni-notice-bar scrollable single text="[单行] 这是 NoticeBar 通告栏，这是 NoticeBar 通告栏，这是 NoticeBar 通告栏"></uni-notice-bar> -->

		<uni-number-box v-model="value" :min="0" :max="9"></uni-number-box>

		<uni-pagination title="标题文字" show-icon="true" total="50" current="2"></uni-pagination>

		<uni-section title="设置评分数" subTitle="设置 max 属性控制组件最大星星数量" type="line" padding>
			<uni-rate :max="10" :value="5" />
		</uni-section>

		<uni-section title="响应式布局" subTitle="共五个响应尺寸：xs、sm、md、lg 和 xl" type="line">
			<view class="example-body">
				<uni-row class="demo-uni-row" :gutter="gutter">
					<uni-col :xs="8" :sm="6" :md="4" :lg="3" :xl="1">
						<view class="demo-uni-col dark"></view>
					</uni-col>
					<uni-col :xs="4" :sm="6" :md="8" :lg="9" :xl="11">
						<view class="demo-uni-col light"></view>
					</uni-col>
					<uni-col :xs="4" :sm="6" :md="8" :lg="9" :xl="11">
						<view class="demo-uni-col dark"></view>
					</uni-col>
					<uni-col :xs="8" :sm="6" :md="4" :lg="3" :xl="1">
						<view class="demo-uni-col light"></view>
					</uni-col>
				</uni-row>
			</view>
		</uni-section>

		<uni-search-bar placeholder="自定placeholder" @confirm="search"></uni-search-bar>
		<uni-search-bar :radius="100" @confirm="search" clearButton="auto" cancelButton="none"></uni-search-bar>

		<uni-segmented-control :current="segmentedcurrent" :values="segmenteditems" @clickItem="onClickItem" styleType="button" activeColor="#4cd964"></uni-segmented-control>
		<uni-segmented-control :current="segmentedcurrent" :values="segmenteditems" @clickItem="onClickItem" styleType="text" activeColor="#4cd964"></uni-segmented-control>

		<uni-steps :options="stepsoptions" :active="1"></uni-steps>
		<uni-steps :options="stepsoptions" direction="column" :active="2"></uni-steps>

		<uni-swipe-action>
		<!-- 基础用法 -->
			<uni-swipe-action-item :right-options="swipeaction" :left-options="swipeaction" @click="onClick" @change="change">
				<view>SwipeAction 基础使用场景</view>
			</uni-swipe-action-item>
		</uni-swipe-action>


		<uni-swiper-dot :info="swiperinfo" :current="swipercurrent" field="content" mode="round">
			<swiper class="swiper-box" @change="swiperchange">
				<swiper-item v-for="(item ,index) in swiperinfo" :key="index">
					<view class="swiper-item">
						{{item.content}}
					</view>
				</swiper-item>
			</swiper>
		</uni-swiper-dot>


		<uni-table border stripe emptyText="暂无更多数据" type="selection">
			<!-- 表头行 -->
			<uni-tr>
				<uni-th align="center">日期</uni-th>
				<uni-th align="center">姓名</uni-th>
				<uni-th align="left">地址</uni-th>
			</uni-tr>
			<!-- 表格数据行 -->
			<uni-tr>
				<uni-td>2020-10-20</uni-td>
				<uni-td>Jeson</uni-td>
				<uni-td>北京市海淀区</uni-td>
			</uni-tr>
			<uni-tr>
				<uni-td>2020-10-21</uni-td>
				<uni-td>HanMeiMei</uni-td>
				<uni-td>北京市海淀区</uni-td>
			</uni-tr>
		</uni-table>

		<uni-tag text="标签"></uni-tag>
		<uni-tag text="标签" type="error" :circle="true"></uni-tag>
		<uni-tag text="标签" @click="bindClick" inverted></uni-tag>

		<uni-title type="h1" title="h1 一级标题" color="#027fff"></uni-title>
		<uni-title type="h2" title="h2 标题居中" align="center"></uni-title>

		<view style="padding:120px 20px;">
    		<uni-tooltip content="上方提示文字" placement="top">
    		  <view style="padding:10px;background:#eee;">点击我(小程序/App)/hover(H5)</view>
    		</uni-tooltip>
  		</view>

			<view>
		<button type="primary" @click="transitionopen">fade</button>
		<uni-transition mode-class="fade" :styles="{'width':'100px','height':'100px','backgroundColor':'red'}" :show="transitionshow" @change="transitionchange" />
	</view>
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
const pattern = reactive({color: '#7A7E83',
					backgroundColor: '#fff',
					selectedColor: '#007AFF',
					buttonColor: '#007AFF',
					iconColor: '#fff'});

const segmenteditems = reactive(['选项1', '选项2', '选项3'])
const segmentedcurrent = ref(null);

const stepsoptions = reactive([{title: '事件一'}, {title: '事件二'}, {title: '事件三'}, {title: '事件四'}])


const transitionshow = ref(false)
function transitionopen()
{
	transitionshow.value=!transitionshow.value
}
function transitionchange()
{
	console.log("动画完成")
}

const swipeaction = reactive([{
				text: '取消',
				style: {
					backgroundColor: '#007aff'
				}
			}, {
				text: '确认',
				style: {
					backgroundColor: '#dd524d'
				}
			}])

const swiperinfo = reactive([{
				content: '内容 A'
			}, {
				content: '内容 B'
			}, {
				content: '内容 C'
			}])
const swipercurrent = ref(0)
function swiperchange(e){
	swipercurrent.value = e.detail.current;
}

const imageValue = ref(null);
function fileselect()
{
	console.log(imageValue.value);
}

function navback()
{
	console.log("回退页面");
}

const indexedlist = ref([{
	"letter": "A",
	"data": [
		"阿克苏机场",
		"阿拉山口机场"
	]
}, {
	"letter": "B",
	"data": [
		"保山机场",
		"包头机场",
		"北海福成机场"
	]
}])
function indexedbindClick(e){
	console.log(e);
}


const options = reactive(
	[{
			icon: 'headphones',
			text: '客服'
		}, {
			icon: 'shop',
			text: '店铺',
			info: 2,
			infoBackgroundColor:'#007aff',
			infoColor:"red"
		}, {
			icon: 'cart',
			text: '购物车',
			info: 2
		}]
)
const buttonGroup = reactive([{
	      text: '加入购物车',
	      backgroundColor: '#ff0000',
	      color: '#fff'
	    },
	    {
	      text: '立即购买',
	      backgroundColor: '#ffa200',
	      color: '#fff'
	    }
	    ])
function navonClick(e)
{
	uni.showToast({
		title: `点击${e.content.text}`,
	    icon: 'none'
		});
}
function navbuttonClick(e)
{
	console.log(e);
}


const formRef = ref(null);
const formData = reactive({
	hobbyy:[],
	name:"",
	age:0
});
const formRules = {
	name:{rules:[
		{ required: true, message: '请输入姓名', trigger: 'blur' },
		{ min: 2, max: 10, message: '长度2‑10个字符', trigger: 'blur' }
	]},
	age: {rules:[
    	{ required: true, message: '请输入年龄', trigger: 'blur' },
		{ type:Number, message: '输入整数类型', trigger: 'blur' }
  	]},
	hobbyy: {rules:[
    	{ required: true, message: '请选择班级', trigger: 'change' }
  	]
}};
const submitForm = async () => {
  const valid = await formRef.value.validate()
  if (valid) {
    console.log('校验通过，表单数据：', formData);
  } else {
    console.log('校验不通过');
  }
}

// 重置表单
// const resetForm = () => {
//   formRef.value.resetFields();
// }
const resetForm = ()=>{
  console.log('formRef.value ===>', formRef.value)
  if(!formRef.value) return
  formRef.value.resetFields()
}


const checked = ref(false);
function favonClick()
{
	checked.value=!checked.value;
}


const fabcontent = reactive([{
						iconPath: '/static/logo.png',
						selectedIconPath: '/static/logo.png',
						text: '相册',
						active: false
					},
					{
						iconPath: '/static/logo.png',
						selectedIconPath: '/static/logo.png',
						text: '首页',
						active: false
					},
					{
						iconPath: '/static/logo.png',
						selectedIconPath: '/static/logo.png',
						text: '收藏',
						active: false
					}]);



function fabtrigger(e)
{
	console.log(e.item.text);
}
function fabClick()
{
	console.log("fabClick");
}


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

const showRight = ref(null);
function showDrawer()
{
	showRight.value.open();
}
function closeDrawer()
{
	showRight.value.close();
}

const easyinputvalue = ref("");
function easyinputClick()
{
	easyinputvalue.value="输入框信息";
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

    
	.demo-uni-row {
		margin-bottom: 10px;

		/* #ifdef MP-TOUTIAO || MP-QQ || MP-BAIDU */
		display: block;
		/* #endif */
	}

	// 支付宝小程序没有 demo-uni-row 层级
	// 微信小程序使用了虚拟化节点，没有 demo-uni-row 层级
	/* #ifdef MP-ALIPAY || MP-WEIXIN */
	::v-deep .uni-row {
		margin-bottom: 10px;
	}

	/* #endif */

	.demo-uni-col {
		height: 36px;
		border-radius: 5px;
	}

	.dark_deep {
		background-color: #99a9bf;
	}

	.dark {
		background-color: #d3dce6;
	}

	.light {
		background-color: #e5e9f2;
	}

	.example-body {
		/* #ifndef APP-NVUE */
		display: block;
		/* #endif */
		padding: 5rpx 10rpx 0;
		overflow: hidden;
	}



</style>