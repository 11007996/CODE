## EAM-Admin

> 设备系统管理后台

EAM(web前后分离)  设备管理系统。

- EAM-Admin-vue-main            后台UI，部署在IIS 172.18.20.84:8887  的EAM_Admin_Page网站，前端非prod-api的路由重写到index首页。
- EAM-AdminNetCore-master   后台接口，部署在IIS 172.18.20.84:8888 的EAM_Admin_API网站。IIS安装了请求代理插件，将前端prod-api的路由请求url重写到后端接口。

- eam系统账号：

  用户：admin

  密码：admin2024_eam

  



## EMA-Listen项目

---- Listen(桌面应用)      设备数据上传监听程序，不同的厂区，部署在各自的DB服务器上，端口10409。



## EAM-Applet项目

> 设备系统（企业微信自建应用）

微信自建应用的前端项目，移动端应用(HBuilder开发工具，VUE3框架)，部署在172.18.20.81 IIS下的Asset网站的h5目录下。

配套的API接口项目在：（EAM-AdminNetCore-master 项目的EAM-Applet-APi项目模块后台接口），部署在172.18.20.81 IIS下的Asset网站。

- 本地企业微信调试问题，可信域名重定向问题：

本地调试时，要将项目启动端口配置为微信配置的端口号，并且需要在C:\Windows\System32\drivers\etc\hosts文件中配置dns域名解析，使微信配置的域名地址解析到 127.0.0.1，如：

```
127.0.0.1 sbgl.luxshare-ict.com
```

再通过企业微信访问：http://sbgl.luxshare-ict.com:8090/h5/#/

这样就可以在本地通过微信进入企业应用时，获取到code。并访问本地启动的项目。

- 本地获取微信接口访问token问题

内网：应该是已配置了，可以正常获取。

外网：因集团没有开放自建应用的可信服务器IP配置，无法在本地直接调用接口直拉获取到token。可以到企业微信提供的特定页面工具通过api获取token，工具地址（企业微信开发都中心>企业内部开发>服务端API>开发指南>获取access_token>调试工具）https://developer.work.weixin.qq.com/document/path/91039 ；注意，这样虽然可以获取到token，但调用他接口还是会报不是可信IP，也不方便。所以如果一定要在外网的情况调试，还是要在应用管理里配置可以的IP才方便调试。





## EAM-Dashboard项目

> 设备系统相关看板

所有厂区使用同一个服务器

部署在IIS 172.18.20.84:8080