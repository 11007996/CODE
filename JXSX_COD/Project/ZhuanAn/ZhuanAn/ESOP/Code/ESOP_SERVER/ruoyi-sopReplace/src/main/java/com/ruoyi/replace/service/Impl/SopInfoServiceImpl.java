package com.ruoyi.replace.service.Impl;

import com.alibaba.fastjson2.JSON;
import com.alibaba.fastjson2.JSONObject;
import com.ruoyi.replace.domain.SopInfo;
import com.ruoyi.replace.mapper.SopInfoMapper;
import com.ruoyi.replace.service.ISopInfoService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

/**
 * SopInfo业务层处理
 *
 * @author ruoyi
 * @date 2022-11-10
 */
@Service
public class SopInfoServiceImpl implements ISopInfoService {
    @Autowired
    private SopInfoMapper sopInfoMapper;



    /**
     * 通过modelId、lineId、stageId查询processId列表
     *
     * @param
     * @return
     */

//    public Map<String, String> selectSopInfo(String sopInfo) {
//        String msg = "";
//        Long sopId = 0L;
//        String filePath = "";
//        String cid = "";
//        String sopPage = "";
//        Map<String,String> map = new HashMap<String,String>();
//            //解析发送的报文   包括sopId、sopPage、modelId、lineId、stageId(、processId)
//            JSONObject jsonObject = JSONObject.parseObject(sopInfo);
//            //msg也就是filePath，要通过传的sopId在方法selectFilePathBySopId查询出来
//            sopId = jsonObject.getLong("sopId");
//            msg = sopInfoMapper.selectFilePathBySopId(sopId);
//            cid = jsonObject.getString("processId");
//            sopPage = jsonObject.getString("sopPage");
//            //当前proessId（工站/制程）的传值不为空，就推送消息给指定的这个proessId（工站/制程）
//            //返回给cid的信息只需要filePath、sopPage
//            if (cid != null) {
//                map.put("filePath",msg);
//                map.put("sopPage",sopPage);
//                map.put("processId",cid);
//
////                Long modelId = jsonObject.getLong("modelId");
////                Long lineId = jsonObject.getLong("lineId");
////                Long stageId = jsonObject.getLong("stageId");
////                Long processId = jsonObject.getLong("processId");
////                page = sopInfoMapper.selectPageByIds(modelId, lineId, stageId,processId);
////                map.put("sopPage", String.valueOf(page));
//                return map;
////                WebSocketServer.sendInfo(msg, cid);
//            } else {
//                Long modelId = jsonObject.getLong("modelId");
//                Long lineId = jsonObject.getLong("lineId");
//                Long stageId = jsonObject.getLong("stageId");
//                //pidList是查询出来的processId的list
//                List<Long> pidList = sopInfoMapper.selectProcessIdByIds(modelId, lineId, stageId);
//                for (int i = 0; i < pidList.size(); i++) {
//                    cid= String.valueOf(pidList.get(i));
//                    map.put("filePath",msg);
//                    map.put("sopPage",sopPage);
//                    map.put("processId",cid);
//
//                    return map;
////                    WebSocketServer.sendInfo(msg, cid);
//                }
//            }
//        return map;
//    }


    public void selectSopInfoList(SopInfo sopInfo){
        JSONObject jsonObject = JSON.parseObject(String.valueOf(sopInfo));
        Long modelId = jsonObject.getLong("modelId");
        Long lineId = jsonObject.getLong("lineId");
        Long stageId = jsonObject.getLong("stageId");
        Long cid = Long.valueOf(jsonObject.getString("processId"));
        JSONObject sopListObject = null;//存放发来的数据
        for (int i = 0; i < jsonObject.getJSONArray("sopList").size(); i++) {
            sopListObject = JSONObject.parseObject(jsonObject.getJSONArray("sopList").get(i).toString());
            Long sopId = sopListObject.getLong("sopId");

//            List ids = sopInfoMapper.sele
//            String sendMsg = sopInfoMapper.selectFilePathBySopId(sopId);//存放推送的数据
            String msg = sopInfoMapper.selectFilePathBySopId(sopId);
            String sopPage = sopListObject.getString("sopPage");
//            sendMsg.setFilePth();
//            sendMsg.set("sopPage",sopPage);
        }
    }

}
