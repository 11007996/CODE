<template>
        <view class="aa">
                <h1 class="bb">{{h1text}}</h1>
                <slot name="slotname"></slot>
                <p>{{ props.propMessage}} - {{ props.propEa }}</p>
        </view>
</template>

<script setup>
import {ref} from 'vue'
const h1text = ref("qqq");

const props = defineProps({
        propMessage:{
                type:String,
                default:"prop值",
                required:true
        },
	propEa: {
		type: Object,
		// 对象或数组默认值必须从一个工厂函数获取
		default: function() {
		  //return { message: 'hello' }
                  return "hello"
		}
	},
        // 自定义验证函数
	propF: {
		validator: function(value) {
		  // 这个值必须匹配下列字符串中的一个
		  return ['success', 'warning', 'danger'].indexOf(value) !== -1
		}
	},
})

</script>

<style lang="scss" scoped>
.aa{
        .bb{
                color: blueviolet;
        }
}
</style>