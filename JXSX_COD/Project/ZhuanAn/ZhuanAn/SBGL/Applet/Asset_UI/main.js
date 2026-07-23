import App from './App'
//import * as ww from '@wecom/jssdk' //企业微信js-sdk

// #ifndef VUE3
import Vue from 'vue'
import './uni.promisify.adaptor'
Vue.config.productionTip = false;
App.mpType = 'app'
const app = new Vue({
	...App
})
app.$mount()
// #endif

// #ifdef VUE3
import {
	createSSRApp
} from 'vue'
import AppVue from './App';
export function createApp() {
	const app = createSSRApp(App)
	return {
		app
	}
}
// #endif

