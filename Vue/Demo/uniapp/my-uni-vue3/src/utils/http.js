// 拦截器不只是能拦截request请求，还可以拦截uni-app其它API
// 先拦截执行invoke，再执行uni-app API,再根据执行结果运行success或fail,再最后运行complete
// 常用于登录页面，判断是否已经登录，否则强制导航到登录页面
// uni.addInterceptor('request', {
//   invoke(args) {
//     // request 触发前拼接 url
//     // args.url = 'https://www.example.com/'+args.url
//     console.log("拦截器request前 " +args)
//   },
//   success(args) {
//     // 请求成功后，修改code值为1
//     // args.data.code = 1
//     console.log("拦截器request成功中")
//   },
//   fail(err) {
//     // console.log('interceptor-fail',err)
//     console.log("拦截器request失败中")
//   },
//   complete(res) {
//     //console.log('interceptor-complete',res)
//     console.log("拦截器request完成")
//   }
// })


// uni.addInterceptor('showModal',{
//     invoke(args) {
//     console.log("拦截器showModal " +args)
//   }
// })


export function http(reuqestOption)
{
    uni.request(reuqestOption);
    // uni.showModal({
    //     title: '拦截其它API',
    //     content: '拦截消息显示'
    // })
}