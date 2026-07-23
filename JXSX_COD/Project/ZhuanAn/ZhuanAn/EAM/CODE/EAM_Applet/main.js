import App from './App'
import {
	createSSRApp
} from 'vue'
import * as Pinia from 'pinia';

import ElementPlus from 'element-plus'
import 'element-plus/dist/index.css'
import zhCn from 'element-plus/es/locale/lang/zh-cn' //element-plus国际化
import { getDicts } from '@/api/system/data.js'
import { createWebHashHistory, createRouter } from 'vue-router'
import plugins from './plugins'
// 字典标签组件
import DictTag from '@/components/DictTag'
import elementIcons from '@/components/SvgIcon/svgicon'

import directive from '@/directive/index'
import { initAppGuard } from '@/utils/app-init' // 引入程序启动初始化

export function createApp() {
	const app = createSSRApp(App)

	app.use(ElementPlus, { locale: zhCn })
	app.use(elementIcons)
	app.use(Pinia.createPinia())
	app.use(plugins)

	//全局方法挂载
	app.config.globalProperties.getDicts = getDicts
	//全局组件挂载
	app.component('DictTag', DictTag)
	initAppGuard()
	//自定义指令
	directive(app)

	return {
		app,
		Pinia
	}
}