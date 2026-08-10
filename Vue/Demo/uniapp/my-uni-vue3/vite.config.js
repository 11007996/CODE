import { defineConfig } from 'vite'
import uni from '@dcloudio/vite-plugin-uni'
// https://vitejs.dev/config/
export default defineConfig({
  plugins: [
    uni(),
  ],
})

/*
module.exports = {
  devServer: {
    host: '0.0.0.0',
    hot: true,
    port: 80,
    open: true,
    proxy: {
      '/bobcat': {
		target: 'http://172.18.32.206',   //本地
        changeOrigin: true,
        pathRewrite: {
          '^/bobcat': '/bobcat',
        },
      },
    },
  },
}
  */