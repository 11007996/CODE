import {createRouter,createWebHashHistory} from "vue-router";
import page1 from '../components/page/vueroute/page1.vue'
import page2 from '../components/page/vueroute/page2.vue'

const routes = [
	{
		path:'/components/page/vueroute/page1.vue',
		name:page1,
		component:page1
	},
	{
		path:'/components/page/vueroute/page2.vue',
		name:page2,
		component:page2
	},
]

const router = createRouter({
	history:createWebHashHistory(),
	routes
})

export default routes