import './assets/main.css'

import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
// import VueResource from 'vue-resource'
// import axios from 'axios'
const app = createApp(App);
app.use(router);
// app.use(VueResource);
// app.use(axios);

app.mount('#app')
