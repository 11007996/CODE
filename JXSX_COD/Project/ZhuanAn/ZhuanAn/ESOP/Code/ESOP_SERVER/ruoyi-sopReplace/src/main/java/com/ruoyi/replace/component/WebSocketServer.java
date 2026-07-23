package com.ruoyi.replace.component;

import com.ruoyi.common.exception.ServiceException;
import com.ruoyi.common.utils.StringUtils;
import com.ruoyi.product.domain.MesMac;
import com.ruoyi.product.mapper.MesMacMapper;
import com.ruoyi.product.service.IMesMacService;
import com.ruoyi.product.service.IMesSopGroupService;
import com.ruoyi.replace.domain.JsonDecoder;
import com.ruoyi.replace.domain.JsonEncoder;
import com.ruoyi.replace.domain.JsonObj;
import com.ruoyi.replace.domain.SopInfo;
import com.ruoyi.replace.mapper.SopInfoMapper;
import lombok.extern.slf4j.Slf4j;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.context.ApplicationContext;
import org.springframework.stereotype.Component;

import javax.websocket.*;
import javax.websocket.server.PathParam;
import javax.websocket.server.ServerEndpoint;
import java.io.IOException;
import java.util.ArrayList;
import java.util.Enumeration;
import java.util.List;
import java.util.Map;
import java.util.concurrent.ConcurrentHashMap;

@Component
@ServerEndpoint(value="/ws/{sid}",decoders = JsonDecoder.class , encoders = JsonEncoder.class)
@Slf4j
public class WebSocketServer {

//    private SopInfoServiceImpl sopInfoService = new SopInfoServiceImpl() ;

    private static ApplicationContext applicationContext;

    private SopInfoMapper sopInfoMapper;

    private MesMacMapper mesMacMapper;

    //解决无法注入service问题
    //这里使用静态，让service属于类
    private static IMesSopGroupService mesSopGroupService;
    //注入的时候，给类的service注入
    @Autowired
    public void WebSocketServer(IMesSopGroupService mesSopGroupService) {
        WebSocketServer.mesSopGroupService = mesSopGroupService;
    }

    //解决无法注入mapper问题
    public static void setApplicationContext(ApplicationContext applicationContext) {
        WebSocketServer.applicationContext = applicationContext;
    }


    /**
     * 静态变量，用来记录当前在线连接数。应该把它设计成线程安全的。
     */
    private static int onlineCount = 0;
    /**
     * concurrent包的线程安全Set，用来存放每个客户端对应的MyWebSocket对象。
     */
//    private static CopyOnWriteArraySet<WebSocketServer> webSocketSet = new CopyOnWriteArraySet<>();

    private static ConcurrentHashMap<String,WebSocketServer> webSocketMap = new ConcurrentHashMap<>();
    /**
     * 与某个客户端的连接会话，需要通过它来给客户端发送数据
     */
    private Session session;
    /**
     * 接收sid
     */
    private String sid="";

    /**
     * 连接建立成功调用的方法
     **/
    @OnOpen
    public void onOpen(Session session,@PathParam("sid") String sid) {
        try{
            String sidtemp="";
            for (int i = 0; i < 10000; i++) {
                if(i>9999){
//                    sendMessage("error");
                    sendMessage("当前在线人数过多，请刷新重试");
                    return;
                }
                sidtemp = sid + "__" + i;
                this.session = session;
                this.sid=sidtemp;
                if(webSocketMap.get(sidtemp)!=null){
//                    webSocketMap.remove(sid);
//                    //加入set中
//                    webSocketMap.put(sid,this);
                }else{
                    //加入set中
                    webSocketMap.put(sidtemp,this);
                    //在线数加1
                    addOnlineCount();
                    break;
                }
            }

//            this.session = session;
//            this.sid=sid;
//            if(webSocketMap.contains(sid)){
//                webSocketMap.remove(sid);
//                //加入set中
//                webSocketMap.put(sid,this);
//            }else{
//                //加入set中
//                webSocketMap.put(sid,this);
//                //在线数加1
//                addOnlineCount();
//            }
            log.info("有新窗口开始监听:"+sid+",当前在线人数为" + getOnlineCount());

            String[] mlsp= sid.split("_");
            Long lineId = Long.valueOf(mlsp[0]);
            Long stageId = Long.valueOf(mlsp[1]);
            Long processId = Long.valueOf(mlsp[2]);
            System.out.println(mlsp[3]);
            if(mlsp[3] == "null" || mlsp[3].equals("null")){
                throw new ServiceException("获取不到当前MAC的信息，请尝试从GetMac进入！");
            }
//            String macName = mlsp[3];
            MesMac mesMac = new MesMac();
            mesMac.setMacName(mlsp[3]);
            mesMac.setLineId(lineId);
            mesMac.setStageId(stageId);
            mesMac.setProcessId(processId);
            //实例化bean
            mesMacMapper = applicationContext.getBean(MesMacMapper.class);
            List<MesMac> macList = mesMacMapper.selectMesMacList(mesMac);
            if(macList.size()>1){
                throw new IOException("MAC地址不唯一");
            }
            Long terminalId = macList.get(0).getMacId();

            //实例化bean
            sopInfoMapper = applicationContext.getBean(SopInfoMapper.class);
            List <SopInfo> list =  sopInfoMapper.selectSopInfoByIds(lineId,stageId,processId,terminalId);
            //新增获取pdf文件宽高代码
            List<Map<String,Float>> pdfSizeList = new ArrayList<>();
             String filePath = null,passWord = null;
            for (SopInfo sopItem:list) {
                if(sopItem.getType().equals("0")){
                    filePath = sopItem.getFilePath();
                    passWord = sopItem.getPassWord();
                    pdfSizeList = mesSopGroupService.pdfSize(filePath,passWord);
                    sopItem.setPdfSizeList(pdfSizeList);
                }
            }
            JsonObj a = new JsonObj();
            a.setSopInfoList(list);
            //解析发送的报文
//            sendMessage("连接成功!"+ JSON.toJSON(list));
            System.out.println("发送信息给前端："+a);
            sendObjectMessage(a);

        } catch (IOException e) {
            log.error("websocket IO异常" );
        } catch (EncodeException e) {
            throw new RuntimeException(e);
        }
    }
    /**
     * 连接关闭调用的方法
     */
    @OnClose
    public void onClose() {
        //从set中删除
        webSocketMap.remove(sid);
        //在线数减1
        subOnlineCount();
//        System.out.println("有一连接关闭！当前在线人数为" + getOnlineCount());
        log.info("有一连接关闭！当前在线人数为" + getOnlineCount());
    }
    /**
     * 收到客户端消息后调用的方法
     * @param message 客户端发送过来的消息
     **/
    @OnMessage
    public void onMessage(String message, Session session) {
//        System.out.println("收到来自窗口"+sid+"的信息:"+message);
        log.info("收到来自窗口"+sid+"的信息:"+message);
        //可以群发消息
        //消息保存到数据库、redis
//            if(StringUtils.isNotBlank(message)){
//            try {
//                //解析发送的报文
//                JSONObject jsonObject = JSON.parseObject(message);
//                //当前客户端sid发送的消息message
//                String msg=jsonObject.getString("message");
//                //传送给对应toSid用户的websocket
//                if(webSocketMap.contains(sid)){
//                    log.info("窗口"+sid+"发送给服务器的消息："+msg);
////                    webSocketMap.get(sid).sendMessage(msg);
//                }else {
//                    //否则不在这个服务器上，发送到mysql或者redis
//                    log.error("请求的userId:"+sid+"不在该服务器上");
//                }
//            }catch (Exception e){
//                e.printStackTrace();
//            }
//        }
    }
    /**
     * @param session
     * @param error
     */
    @OnError
    public void onError(Session session, Throwable error) {
        log.error("发生错误");
        error.printStackTrace();
    }
    /**
     * 实现服务器主动推送
     */
    public void sendMessage(String message) throws IOException {
        this.session.getBasicRemote().sendText(message);
    }
    public void sendObjectMessage(JsonObj message) throws EncodeException, IOException {
        this.session.getBasicRemote().sendObject(message);
    }
    /**
     * 群发自定义消息
     * */
    public static void sendInfo(String message,@PathParam("sid") String sid) throws IOException {
        log.info("推送消息到窗口"+sid+"，推送内容:"+message);
        if(StringUtils.isNotBlank(sid) ){
            if(webSocketMap.containsKey(sid)){
                webSocketMap.get(sid).sendMessage(message);
            }else{
//                List<String> list = (List<String>) webSocketMap.keys();
                List<String> list = new ArrayList<>();
                Enumeration enu =  webSocketMap.keys();
                while (enu.hasMoreElements()) {
                    list.add(enu.nextElement().toString());
                }
                for (int i = 0; i < list.size(); i++) {
                    for (int j = 0; j < 10; j++) {
                        String sidtemp = sid + "__" + j;
                        if(list.get(i).contains(sidtemp)){
                            webSocketMap.get(list.get(i)).sendMessage(message);
                        }
                    }

                }
            }

        }else{
            log.error("用户"+sid+",不在线！");
        }
    }

    public static synchronized int getOnlineCount() {
        return onlineCount;
    }
    public static synchronized void addOnlineCount() {
        WebSocketServer.onlineCount++;
    }
    public static synchronized void subOnlineCount() {
        WebSocketServer.onlineCount--;
    }

}

