<template>
    <view class="">
		<uni-forms class="formC" ref="formRef" :model-value="formData" :rules="rules">
			<uni-forms-item label="姓名" name="name">
				<uni-easyinput class="inputName" type="text" v-model="formData.name" placeholder="请输入姓名" />
			</uni-forms-item>
			<uni-forms-item label="年龄" name="age">
				<input type="text" class="inputAge" v-model="formData.age" placeholder="请输入年龄" />
			</uni-forms-item>
			<uni-forms-item class="checkitem" required name="hobby" label="兴趣爱好"  label-width="150rpx"> 
				<uni-data-checkbox class="checkHobby" multiple v-model="formData.hobby" :localdata="genderList"/>
			</uni-forms-item>
		</uni-forms>
		<button @click="handleSubmit">Submit</button>
	</view>

  <!-- insert表示是否插入日历  lunar表示是否显示家历 start-date表示开始有效日期-->
  <uni-calendar 
	:insert=isopen
	:lunar="true" 
  :range="false"
  :selected="datelist"
  :start-date="'2026-8-2'"
	:end-date="'2026-9-20'"
	@change="change"
	 />
   <button @click="opencalendar">打开日历</button>
</template>

<script setup>
import {ref,reactive} from 'vue'
const isopen = ref(false)
const formRef = ref(null)
const formData=ref({
    name:"",
    age:"",
    hobby:""
})
const genderList=reactive([{text:"aaa",value:"a"},{text:"bbb",value:"b"},{text:"ccc",value:"c"}])
const rules = ref({
    name:[{ required: true, message: '姓名必填' },
          { type: String, message: '必须输入字符串' },
          { min:2, max:5, message:'长度2‑5位', trigger:'blur'}
    ],
    age:[{ required: true, message: '姓名必填' },
          { type: Number, message: '必须输入字符串' },
          { min:2, max:5, message:'长度2‑5位', trigger:'blur'}
    ],
    hobby:[
          {
            validator(rule, value, callback) {
              if (!value || value.length === 0) {
                callback(new Error('请选择爱好'))
              } else {
                callback()
              }
            },
            trigger: 'change'
            }
        ]
})

const objd = reactive({date:"",info:""});
const datelist = reactive([])
const change=((e)=>{
  let isadd=true;
  let i=0;
  for (; i < datelist.length; i++) {
    if(datelist[i].date==e.fulldate)
    {
      isadd = false;
      break;
    }
    
  }
  console.log(isadd)
  if(isadd)
  {
    objd.value={date:e.fulldate,info:"勾选"}   //reactive对象不能解构，必须整个赋值对象，不能单个赋值对象的字段
    datelist.push(objd.value);
  }
  else
  {
    datelist.splice(i, 1)
  }

})
function opencalendar()
{
  isopen.value= !isopen.value;
}

async function handleSubmit(){
  try{
    await formRef.value.validate()
    console.log('校验通过', formData.value)
  }catch(err){
    console.log('校验失败',err)
  }
}
</script>

<style lang="scss" scoped>
.formC{
    .inputName{
        width: 300rpx;
        height: 50rpx;
    }
    .inputAge{
        width: 300rpx;
        height: 60rpx;
        border: 1rpx solid rgba(200,200,200,0.5);
        border-radius: 10rpx;
        padding: 0 20rpx;
    }
    .inputAge:hover{
        border: 2rpx solid rgba(50,90,200,0.7);
    }
    .checkitem{
        width: 60%;
        // display: flex;
        // align-content: center
        // justify-content: center;
        // white-space: nowrap;
        .checkHobby{
            margin-left: 10rpx;
            width: 500rpx;
        }
    }
    //deep修改组件内部的样式
    // .uni-forms-item                整个item容器
    // .uni-forms-item__label         label外层容器
    // .uni-forms-item__label-text    label文字
    // .uni-forms-item__content       右侧输入内容区域
    // .uni-forms-item__error         校验错误提示文字
    :deep(.uni-forms-item__label) {
        color: rgb(255, 100, 100);
        white-space: nowrap;
    }
}

</style>