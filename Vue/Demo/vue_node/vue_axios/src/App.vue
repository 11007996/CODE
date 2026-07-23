<script setup>
  import {ref,reactive} from 'vue'
  import axios  from 'axios'
  var con = ref("连接数据");
// let lists = reactive();

//POST表单数据
// let dataa = new URLSearchParams();   //表单数据
// dataa.append("aa","3");

// POST对象数据
const dataa={
  "resInt":3,
  "resInt2":1
};

//GET对象数据
const dataget = {
  a:2,
  b:3
}



async function connect () {
     //get请求
     // const res = await axios.get('http://hmajax.itheima.net/api/news')
     // con.value = res.data.message;
     // lists = res.data.data;

     //自定义get请求方式一
     // const res = await axios.get('http://localhost:53092/api/home?a=1&b=2')
     // con.value = res.data;

     //自定义get请求方式二
     // const res = await axios({
     //  url:'http://localhost:53092/api/home',
     //  method:'GET',
     //  headers: {
     //    // 'Content-Type': 'application/x-www-form-urlencoded'
     //    'Content-Type':'application/json'
     //  },
     //  params:dataget
     // })
     // con.value = res.data;

     //自定义POST请求
     // const res = await axios({
     //  url:'http://localhost:53092/api/home',
     //  method:'POST',
     //  headers: {
     //    'Content-Type': 'application/x-www-form-urlencoded'
     //  },
     //  data:dataa
     // });
     // con.value = res.data.resInt + res.data.resInt2;
  }
</script>

<template>

  <button @click="connect">连接</button>
  <div>
    <h2>{{con}}</h2>
    <!-- <ol>
      <li v-for="(item, i) in lists" :key="i">{{item.title}}</li>
    </ol> -->
  </div>
</template>

