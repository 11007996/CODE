import { ElNotification, ElMessageBox } from 'element-plus'
import useSocketStore from '@/store/modules/socket'
import useUserStore from '@/store/modules/user'
import { webNotify } from '@/utils/index'

export default {
  onMessage(connection) {
    //在线人数
    connection.on(MsgType.OnlineNum, (data) => {
      useSocketStore().setOnlineUsers(data)
    })

    //socket连接Id
    connection.on(MsgType.ConnId, (data) => {
      // useUserStore().saveConnId(data)
    })

    // 接收后台手动推送消息
    connection.on(MsgType.ReceiveNotice, (title, data) => {
      ElNotification({
        type: 'info',
        title: title,
        message: data,
        dangerouslyUseHTMLString: true,
        duration: 0
      })
      webNotify({ title: title, body: data })
    })

    // 接收系统通知/公告
    connection.on(MsgType.MoreNotice, (data) => {
      if (data.code == 200) {
        useSocketStore().setNoticeList(data.data)
      }
    })

    // 接收在线用户
    // connection.on(MsgType.OnlineUser, (data) => {
    //   useSocketStore().setOnlineUsers(data)
    // })

    // 接收强退通知
    connection.on(MsgType.ForceUser, (data) => {
      // connection.stop().then(() => {
      //   console.log('Connection stoped')
      // })
      // ElMessageBox.alert(`你的账号已被强退，原因：${data.reason || '无'}`, '提示', {
      //   confirmButtonText: '确定',
      //   callback: () => {

      //   }
      // })
      useSocketStore().setGlobalError({ code: 0, msg: `你的账号已被强退，原因：${data.reason || '无'}` })
      useUserStore()
        .logOut()
        .then(() => {
          location.href = import.meta.env.VITE_APP_ROUTER_PREFIX + 'error'
        })
    })

    // 接收聊天数据
    connection.on(MsgType.ReceiveChat, (data) => {
      const { fromUser, message } = data

      useSocketStore().setChat(data)

      if (data.userid != useUserStore().userId) {
        ElNotification({
          title: fromUser.nickName,
          message: message,
          type: 'success',
          duration: 3000
        })
      }
      webNotify({ title: fromUser.nickName, body: message })
    })

    //当前用户信息
    connection.on(MsgType.OnlineInfo, (data) => {
      useSocketStore().getOnlineInfo(data)
    })

    //登出
    connection.on(MsgType.LogOut, () => {
      useUserStore()
        .logOut()
        .then(() => {
          ElMessageBox.alert(`你的账号已在其他设备登录，如果不是你的操作请尽快修改密码`, '提示', {
            confirmButtonText: '确定',
            callback: () => {
              location.href = import.meta.env.VITE_APP_ROUTER_PREFIX + 'index'
            }
          })
        })
    })
  }
}

const MsgType = {
  ReceiveNotice: 'receiveNotice', // 接收后台手动推送消息
  ReceiveChat: 'receiveChat', // 接收聊天数据
  MoreNotice: 'moreNotice', // 接收系统通知/公告
  OnlineNum: 'onlineNum', //在线人数
  OnlineInfo: 'onlineInfo', //当前用户信息
  LockUser: 'lockUser', //用户锁定
  ForceUser: 'forceUser', //强退用户
  LogOut: 'logOut', //用户登出
  ConnId: 'connId' //socket连接ID
}
