package com.ruoyi.web.controller.replace;

import com.ruoyi.common.core.controller.BaseController;
import com.ruoyi.common.core.domain.AjaxResult;
import com.ruoyi.product.domain.MesUploadTerminalPage;
import com.ruoyi.product.service.IMesUploadTerminalPageService;
import com.ruoyi.replace.service.ISopInfoService;
import org.mybatis.spring.annotation.MapperScan;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import java.util.List;

@RestController
@RequestMapping("/replace")
@MapperScan("com.ruoyi.replace.mapper")
public class ReplaceController extends BaseController {


    @Autowired
    private ISopInfoService sopInfoService;

    @Autowired
    private IMesUploadTerminalPageService mesUploadTerminalPageService;

    /**
     * 页面请求
     * @param cid
     * @return
     */

//    @GetMapping("/socket/{cid}")
//    public ModelAndView socket(@PathVariable String cid) {
//        ModelAndView mav=new ModelAndView("/socket");
//        mav.addObject("cid", cid);
//        return mav;
//    }
    /**
     * 推送数据接口
     * @param sopInfo
     * @return
     */

//    @PostMapping("/socket/push")
//    public AjaxResult pushToWeb(@RequestBody String sopInfo) {
//        Map<String,String> map = sopInfoService.selectSopInfo(sopInfo);
//        String msg = map.get("filePath");
//        String page = map.get("sopPage");
//        String cid = map.get("processId");
//        try{
//            WebSocketServer.sendInfo(msg, cid);
//        } catch (Exception e) {
//            return AjaxResult.error("error:"+cid+"#"+e.getMessage());
//        }
//    return AjaxResult.success("发送给工站"+cid+"的信息内容："+msg +"，文件页码："+ page);
//    @PostMapping("/socket/push")
//    public  AjaxResult pushToWeb(@RequestBody SopInfo sopInfo) {
//        List<SopInfo> list = sopInfoService.selectSopInfoList(sopInfo);
//        return (AjaxResult) list;
//    }

    /**
     * sop上传签核页面的详情信息
     * @param
     * @return
     */
    @GetMapping("/uploadInfo")
    public AjaxResult getInfo(Long sopId)
    {
        startPage();
        List<MesUploadTerminalPage> a=  mesUploadTerminalPageService.selectTerminalListBySopId(sopId);
        return AjaxResult.success(a);
    }
}