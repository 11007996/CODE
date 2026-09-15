import { defineConfig } from 'vite'
import uni from '@dcloudio/vite-plugin-uni'
// https://vitejs.dev/config/


//  module.exports = {
//    devServer: {
//      host: '0.0.0.0',
//      hot: true,
//      port: 6054,
//      open: true,
//      proxy: {
//        '/api': {
//          target: 'http://localhost:6054',  // 后端真实接口地址
//          changeOrigin: true,  // 开启换源，必须true
//          pathRewrite: {'^/api': '/api',},
//        },
//      },
//    },
//  };

export default defineConfig({
  plugins: [uni()],
  server: {
    host: '0.0.0.0',
      hot: true,
      port: 5173,  //前端端口
      open: true,
    proxy: {
      // 匹配所有 /api 开头的请求
      '/api': {
        target: 'http://localhost:6054', // 后端真实接口地址
        changeOrigin: true, // 开启换源，必须true
        // vite用 rewrite，不是webpack的pathRewrite！！踩坑重点
        // rewrite: (path) => path.replace(/^\/api/, ''),  //如果后端接口本身带 /api，**删掉 rewrite**
        // 如果后端是https，加上
        // secure: false
      }
    }
  }
})