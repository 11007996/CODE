import { createRouter,createWebHashHistory } from "vue-router";
import Home from './Home.vue'
import About from './about.vue'



const routes = [
  {
    path:'/home',
    name:Home,
    component:Home
  },
  {
    path:'/about',
    name:About,
    component:About
  },
]



const router = createRouter({
  history:createWebHashHistory(),
  routes
})


export default router