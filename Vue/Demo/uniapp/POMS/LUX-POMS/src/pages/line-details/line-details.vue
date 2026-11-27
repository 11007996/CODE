<template>
    <view class="line-details">
        <uni-nav-bar left-icon="left" title="线别设置"  @clickLeft="onCancel"/>
        <uni-forms class="lineFrom" ref="lineFrom" :modelValue="lineData" :rules="rules">
            <uni-forms-item name="linename" required label="线体">
                <uni-easyinput
                  class="inputControl"
                  v-model="lineData.linename"
                  placeholder=""
                  :disabled=onlyRead
                ></uni-easyinput>
            </uni-forms-item>
            <uni-forms-item name="linealias" label="别名">
                <uni-easyinput
                  class="inputControl"
                  type="text"
                  v-model="lineData.linealias"
                  placeholder=""
                  :disabled=onlyRead
                ></uni-easyinput>
            </uni-forms-item>
            <uni-forms-item name="linedesc" label="描述">
                <uni-easyinput
                  class="inputControl"
                  type="text"
                  v-model="lineData.linedesc"
                  placeholder=""
                  :disabled=onlyRead
                ></uni-easyinput>
            </uni-forms-item>
            <uni-forms-item name="remark" label="备注">
                <uni-easyinput
                  class="inputControl"
                  type="text"
                  v-model="lineData.remark"
                  placeholder=""
                  :disabled=onlyRead
                ></uni-easyinput>
            </uni-forms-item>
            <uni-forms-item name="shift" label="班别">
			    <uni-data-select class="inputControl" v-model="lineShift" :localdata="lineData.shift" :disabled=onlyRead @change="onShiftChange"></uni-data-select>
            </uni-forms-item>
            <uni-forms-item name="enabled" required label="启用">
                <uni-data-select class="inputControl" v-model="lineEnabled" :localdata="lineData.enabled" :disabled=onlyRead @change="onEnabledChange"></uni-data-select>
            </uni-forms-item>
            <uni-forms-item class="btn">
                <button v-if="!onlyRead" class="lineBtn" type="primary" :loading="loading" @click="onConfirm">确定</button>
                <button v-if="!onlyRead" class="lineBtn" @click="onCancel">取消</button>
            </uni-forms-item>
        </uni-forms>
    </view>
</template>

<script setup>
    import {ref,reactive,computed} from 'vue'
    import { getCurrentInstance} from 'vue'
    import { onLoad } from '@dcloudio/uni-app';
    import {CheckToken} from '../../api/base'
    import {GetLineDetailsById,GetShiftOverview,AddLine,ModifyLine} from '../../api/basedata'
    const loading = ref(false)
    const onlyRead= ref(false)   //只读，页面不可修改
    const openTyle = ref("")   //新增Add 修改Modify
    const lineid = ref("")   //线别ID
    const lineFrom = ref(null)
    const lineData=reactive({
        linename:'',
        linealias:'',
        linedesc:'',
        enabled:[{"value": "Y","text": "Y","disable": false},{"value": "N","text": "N","disable": false}],
        remark:'',
        shift:[]
    })
    
    const lineEnabled = ref("Y");   //有效 当前值
    const lineShift = ref("");    //班别  当前值
    const rules = reactive({
      linename:{
        rules: [
          { required: true, errorMessage: '线体不能为空' },
        //   {format: 'string',errorMessage: '年龄只能输入数字'}
          // { minLength: 8,maxLength:10, errorMessage: '账号长度8-10位' }
        ]},
        enabled: {
        rules: [
        { required: true, errorMessage: '必选' },
        // { minLength: 7,maxLength:12, errorMessage: '密码长度7-12位' }
      ]}
    })
    const instance = getCurrentInstance()

    onLoad(async ()=>{
      await CheckToken();
      const channel = instance.proxy.getOpenerEventChannel()
      channel.on('sendData', data=>{
        if(data.lineid)
        {
            lineid.value = data.lineid;
        }
        openTyle.value = data.opentyle;
        if(openTyle.value==='View')  //查看
        {
            onlyRead.value= true;
            GetShiftDetails();
            //获取线别详情
            GetLineDetails({"lineid":lineid.value});
        }
        else if(openTyle.value==='Modify'){   //修改
            onlyRead.value= false;
            GetShiftDetails();
            GetLineDetails({"lineid":lineid.value});
        }
        else if(openTyle.value==='Add'){    //新增
            onlyRead.value= false;
            GetShiftDetails();
        }
      })
    })

    //获取线别详情
    async function GetLineDetails(line)
    {
        const lineInfo = await GetLineDetailsById(line);
        if(lineInfo.data.status===0)
        {
            lineData.linename=lineInfo.data.data[0]?.lineName;
            lineData.linealias = lineInfo.data.data[0]?.lineAlias;
            lineData.linedesc = lineInfo.data.data[0]?.lineDesc;
            lineEnabled.value = lineInfo.data.data[0]?.enabled;
            lineData.remark= lineInfo.data.data[0]?.remark;
            lineShift.value = lineInfo.data.data[0]?.shiftid;
        }
        else
        {
            showModal('提示',lineInfo.data.msg)
        }
       return lineInfo;
    }

    //获取班别
    async function GetShiftDetails()
    {
        let shifts = await GetShiftOverview();
        if(shifts.data.status===0)
        {
            for(var s of shifts.data.data[0])
            {
                lineData.shift.push({"value": s.shiftid,"text": s.shiftName,"disable": false})
            }
        }
        else
        {
            showModal("提示",shifts.data.msg)
        }
    }

    //退出
    function onCancel()
    {
        uni.redirectTo({
            url:"../../pages/line/line"
        })
    }

    //确认
    async function onConfirm()
    {
        if(openTyle.value==='Add')
        {
            if (!lineFrom.value) return;
            try{
                await lineFrom.value.validate()
                const res = await AddLine({
                    lineId:0,
                    lineName:lineData.linename,
                    lineAlias:lineData.linealias,
                    lineDesc:lineData.linedesc,
                    empId:uni.getStorageSync('userId'),
                    enabled:lineEnabled.value,
                    remark:lineData.remark,
                    shiftid:lineShift.value,
                    shiftName:""
                })
                if(res.data.status===0)
                {
                    onlyRead.value = true;
                    uni.showToast({
                    title: '新增完成',
                    icon:'success',
	                duration: 1000
                    });
                }
                else
                {
                    showModal("提示",res.data.msg);
                }
            }
            catch(err)
            {
                uni.showToast({
                    title: err,
                    icon:'none',
	                duration: 3000
                });
            }
        }
        else if(openTyle.value==='Modify')
        {
            if (!lineFrom.value) return;
            try
            {
                await lineFrom.value.validate()
                const res = await ModifyLine({
                    lineId:lineid.value,
                    lineName:lineData.linename,
                    lineAlias:lineData.linealias,
                    lineDesc:lineData.linedesc,
                    empId:uni.getStorageSync('userId'),
                    enabled:lineEnabled.value,
                    remark:lineData.remark,
                    shiftid:lineShift.value,
                    shiftName:""
                })
                if(res.data.status===0)
                {
                    onlyRead.value = true;
                    uni.showToast({
                    title: '修改完成',
                    icon:'success',
	                duration: 1000
                    });
                }
                else
                {
                    showModal("提示",res.data.msg);
                }
            }
            catch(err)
            {
                uni.showToast({
                    title: err,
                    icon:'none',
	                duration: 3000
                });
            }
        }
    }


    //获取线别大概
    async function GetLineById(lineid)
    {
        const res = await GetLineDetailsById({
              lineid: lineid
            });
        if(res.data.status===0)
        {
            return res.data.data;
        }
        else
        {
            return null;
        }
    }

    function onShiftChange(e)
    {
        lineShift.value = e
    }
    function onEnabledChange(e)
    {
        lineEnabled.value = e;
    }

    function showModal(title,message)
    {
        uni.showModal({
            title: title,
	        content: message,
        })
    }

</script>

<style lang="scss" scoped>
    .line-details{

        .lineFrom{
            margin:50rpx 25rpx 25rpx 50rpx;
            .inputControl{
                margin-left: -50rpx;
                display: flex;
                justify-content: center;
                align-items: center;
            }
            .btn{
                margin-top:50rpx;
                .lineBtn{
                    width: 50%;
                    display:inline-flex;
                    justify-content: center;
                    align-items: center;
                }
            }
        }
    }
    
</style>