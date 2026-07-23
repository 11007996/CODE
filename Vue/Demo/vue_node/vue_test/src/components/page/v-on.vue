<!-- <template>
	<h1>整个修饰符</h1>
	<p>{{count}}</p>
	//事件只触发一次
	<button @click.once="add">一次</button>
	<br>
	
	<hr>
</template> -->

<!-- // <script>
// 	export default{
// 		data() {
// 			return{
// 				count :1,
// 			}
// 		},
// 		methods:{
// 			add(){
// 				this.count++;
// 			}
// 		}
// 	}
// </script> -->










    <style>
        * {
            margin-top: 20px;
        }

        .demo1 {
            height: 80px;
            width: 240px;
            background-color: pink;
        }

        .box1 {
            padding: 5px;
            width: 400px;
            height: 400px;
            background-color: skyblue;
        }

        .box2 {
            padding: 5px;
            width: 200px;
            height: 200px;
            background-color: orange;
        }

        .list {
            width: 200px;
            height: 200px;
            background-color: peru;
            overflow: auto;
        }

        li {
            height: 100px;
        }
    </style>


<template>
    <div id="App">
        <h1>欢迎，{{name}}</h1>
        <!-- 阻止默认事件（常用） -->
        <a href="http://www.baidu.com" @click.prevent="showInfo">百度</a>

        <!-- 阻止事件冒泡（常用） -->
        <div class="demo1" @click="showInfo1">
            <button @click.stop="showInfo1">点我提示信息1</button>
            <!-- 修饰符可以连续写 -->
            <a href="http://www.baidu.com" @click.prevent.stop="showInfo3">百度哦</a>
        </div>

        <!-- 事件只触发一次（常用） -->
        <button @click.once="showInfo4">点我提示信息4</button>


        <!-- 使用事件的捕获模式 -->
        <div class="box1" @click.capture="showMsg(1)">
            div1
            <div class="box2" @click="showMsg(2)">
                div2
            </div>
        </div>

        <!-- 只有event.target是当前操作的元素时才触发事件； -->
        <div class="demo1" @click.self="showInfo">
            <button @click="showInfo">点我提示信息</button>
        </div>

        <!-- 事件的默认行为立即执行，无需等待事件回调执行完毕； -->
        <ul @wheel.passive="demo" class="list">
            <li>1</li>
            <li>2</li>
            <li>3</li>
            <li>4</li>
        </ul>

    </div>
</template>


<script>
        //创建Vue实例
        export default{
            data() {
				return{
					name: "ZYZ"
				}
                
            },
            methods: {
                showInfo:function() {
                    console.log("阻止页面跳转");
                },

                showInfo1:function() {
                    console.log("阻止冒泡");
                },
                showInfo3:function() {
                    console.log("阻止百度冒泡跳转");
                },
                showInfo4:function() {
                    console.log("只触发一次");
                },
                showMsg:function(msg) {
                    console.log(msg);
                },
                demo:function() {
                    for (let i = 0; i < 3; i++) {
                        console.log('#');
                    }
                    console.log('累坏了');
                }
            }
        }
    </script>
