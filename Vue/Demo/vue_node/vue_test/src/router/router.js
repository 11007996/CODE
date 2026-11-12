import {createRouter,createWebHashHistory} from "vue-router";
import page1 from '../components/page/vueroute/page1.vue'
import page2 from '../components/page/vueroute/page2.vue'

const routes = [
	{
		path:'/page1',
		component:page1
	},
	{
		path:'/page2',
		component:page2
	},
]

const router = createRouter({
	history:createWebHashHistory(),
	routes
})

export default routes