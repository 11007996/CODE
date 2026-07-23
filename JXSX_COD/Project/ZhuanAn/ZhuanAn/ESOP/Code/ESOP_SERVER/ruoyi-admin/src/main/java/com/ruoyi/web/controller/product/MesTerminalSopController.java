package com.ruoyi.web.controller.product;

import com.alibaba.fastjson2.JSONObject;
import com.ruoyi.common.annotation.Log;
import com.ruoyi.common.core.controller.BaseController;
import com.ruoyi.common.core.domain.AjaxResult;
import com.ruoyi.common.core.page.TableDataInfo;
import com.ruoyi.common.enums.BusinessType;
import com.ruoyi.common.utils.ftp.FtpUploadFileUtils;
import com.ruoyi.common.utils.poi.ExcelUtil;
import com.ruoyi.common.exception.CustomException;
import com.ruoyi.product.domain.*;
import com.ruoyi.product.service.*;
import com.ruoyi.replace.component.WebSocketServer;
import com.ruoyi.replace.service.ISopInfoService;
import com.ruoyi.system.service.ISysConfigService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.transaction.annotation.Transactional;
import org.springframework.web.bind.annotation.*;

import javax.servlet.http.HttpServletResponse;
import java.io.ByteArrayOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.util.ArrayList;
import java.util.List;
import java.util.Map;

/**
 * sop配置Controller
 *
 * @author ruoyi
 * @date 2022-09-21
 */
@RestController
@RequestMapping("/product/sopConfig")
public class MesTerminalSopController extends BaseController
{
    @Autowired
    private IMesTerminalSopService mesTerminalSopService;

    @Autowired
    private IMesTerminalSopHtService mesTerminalSopHtService;
    @Autowired
    private IMesTerminalService mesTerminalService;
    @Autowired
    private ISysConfigService configService;
    @Autowired
    private IMesSopGroupService mesSopGroupService;

    @Autowired
    private ISopInfoService sopInfoService;

    @Autowired
    private IMesOaSopService oaSopService;

    @Autowired
    private IMesUploadTerminalPageService mesUploadTerminalPageService;
    @Autowired
    private IMesMacService mesMacService;



    /**
     * sop配置修改页面
     */
    @PreAuthorize("@ss.hasPermi('product:sopConfig:list')")
    @GetMapping("/getSopConfigHt")
    public TableDataInfo getSopConfigHt(@RequestBody MesUploadTerminalPage mesUploadTerminalPage)
    {
//        List<MesTerminalSopHt> list = mesTerminalSopHtService.selectMesTerminalSopHtList(mesTerminalSopHt);
        List<MesUploadTerminalPage> list = mesUploadTerminalPageService.selectPageInfoList(mesUploadTerminalPage);
        return getDataTable(list);
    }

    /**
     * sop一键推送-新增查询页面
     */
//    @PreAuthorize("@ss.hasPermi('product:sopConfig:list')")
    @GetMapping("/getSopPage")
    public TableDataInfo getSopPage(MesUploadTerminalPage mesUploadTerminalPage)
    {
        List<MesUploadTerminalPage> list = mesUploadTerminalPageService.selectPageInfoList1(mesUploadTerminalPage);
        return getDataTable(list);
    }


    /**
     * 查询sop配置列表
     */
    @PreAuthorize("@ss.hasPermi('product:sopConfig:list')")
    @GetMapping("/list")
    public TableDataInfo list(MesTerminalSop mesTerminalSop)
    {
        startPage();
//        List<MesTerminalSop> list = mesTerminalSopService.selectMesTerminalSopList(mesTerminalSop);
        List<MesTerminalSop> list = mesTerminalSopService.selectMesTerminalSopListPushed(mesTerminalSop);
        return getDataTable(list);
    }


    /**
     * 导出sop配置列表
     */
    @PreAuthorize("@ss.hasPermi('product:sopConfig:export')")
    @Log(title = "sop配置", businessType = BusinessType.EXPORT)
    @PostMapping("/export")
    public void export(HttpServletResponse response, MesTerminalSop mesTerminalSop)
    {
        List<MesTerminalSop> list = mesTerminalSopService.selectMesTerminalSopList(mesTerminalSop);
        ExcelUtil<MesTerminalSop> util = new ExcelUtil<MesTerminalSop>(MesTerminalSop.class);
        util.exportExcel(response, list, "sop配置数据");
    }

    /**
     * 获取sop配置详细信息
     */
    @PreAuthorize("@ss.hasPermi('product:sopConfig:query')")
    @GetMapping(value = "/{id}")
    public AjaxResult getInfo(@PathVariable("id") Long id)
    {
        return AjaxResult.success(mesTerminalSopService.selectMesTerminalSopById(id));
    }

    /**
     * 新增sop配置
     */
    @PreAuthorize("@ss.hasPermi('product:sopConfig:add')")
    @Log(title = "sop配置", businessType = BusinessType.INSERT)
    @PostMapping
    public AjaxResult add(@RequestBody MesTerminalSop mesTerminalSop)
    {
        return toAjax(mesTerminalSopService.insertMesTerminalSop(mesTerminalSop));
    }

//    /**
//     * 修改sop配置
//     */
//    @PreAuthorize("@ss.hasPermi('product:sopConfig:edit')")
//    @Log(title = "sop配置", businessType = BusinessType.UPDATE)
//    @PutMapping
//    public AjaxResult edit(@RequestBody MesTerminalSop mesTerminalSop)
//    {
//        return toAjax(mesTerminalSopService.updateMesTerminalSop(mesTerminalSop));
//    }

    @Log(title = "sop配置列表详情")
    @GetMapping("/selectMesSopGroupBySopGroupId/{sopGroupId}")
    public AjaxResult selectMesSopGroupBySopGroupId(@PathVariable("sopGroupId") String sopGroupId) {
        List<MesSopGroup> list =  mesSopGroupService.selectMesSopGroupBySopGroupId(sopGroupId);
        String sopName="";
        List<Map<String,Float>> pdfSizeList = new ArrayList<>();
        for (int i = 0; i < list.size(); i++) {
            if(list.get(i).getType().equals("0")){
                sopName = list.get(i).getSopName();
                list.get(i).setPdfSopName(sopName);
                logger.info(list.get(i).getFilePath());
                pdfSizeList = mesSopGroupService.pdfSize(list.get(i).getFilePath(),list.get(i).getPassWord());
//                pdfSizeList = mesSopGroupService.pdfSize1(list.get(i).getFilePath());
                list.get(i).setPdfSizeList(pdfSizeList);
            }else{
                list.get(i).setVideoSopName(list.get(i).getSopName());
            }
        }
        return AjaxResult.success(list);
    }


    /**
     * 按站点推送（如果一次性选多个站点，就推送到多个站点）
     */
    @PreAuthorize("@ss.hasPermi('product:sopConfig:edit')")
    @Log(title = "sop配置", businessType = BusinessType.UPDATE)
    @PostMapping("/editSopConfig")
    public AjaxResult editSopConfig(@RequestBody MesTerminalSop mesTerminalSop) throws IOException {
        //将从MesTerminalSop（List<MesSopGroup>）接收到的数据，更新到mes_sop_group表
        mesTerminalSopService.updateMesTerminalSop(mesTerminalSop);

        //查询群组表mes_sop_group的sop_group_id
        String sopGroupId = String.valueOf(mesTerminalSop.getId());
        List<MesSopGroup> list =  mesSopGroupService.selectMesSopGroupBySopGroupId(sopGroupId);
        //将mesTerminalSop里面的lineId、stageId、processId、terminalId用“_”拼接
        String sid = mesTerminalSop.getLineId()+"_"+mesTerminalSop.getProcessId()+"_"+mesTerminalSop.getTerminalId();
        String a = list.toString();
        String b = "{"+"\"sopInfoList\":"+a+"}";
        WebSocketServer.sendInfo(b,sid);
//        WebSocketServer.sendInfo(list.toString(), sid);
        return AjaxResult.success("OK");
    }

    /**
     * 删除sop配置
     */
    @PreAuthorize("@ss.hasPermi('product:sopConfig:remove')")
    @Log(title = "sop配置", businessType = BusinessType.DELETE)
	@DeleteMapping("/{ids}")
    public AjaxResult remove(@PathVariable Long[] ids)
    {
        return toAjax(mesTerminalSopService.deleteMesTerminalSopByIds(ids));
    }


    /**
     * 获取文件流
     * @param
     * @return
     */
    @PreAuthorize("@ss.hasPermi('product:sopConfig:query')")
    @PostMapping(value = "view")
    public void view( HttpServletResponse response, @RequestBody String jsonData) {
        Map<String, Object> map = JSONObject.parseObject(jsonData);
        String filePath = map.get("filePath").toString();
        try {
            FtpUploadFileUtils ftp = new FtpUploadFileUtils(configService.selectConfigByKey("product.sop.ftp"));
            InputStream in = ftp.view(filePath);
            if(in == null){
                System.out.println("读取文件失败");
                return;
            }

            ByteArrayOutputStream swapStream = new ByteArrayOutputStream();
            int ch;
            //循环将文件流写入ByteArrayOutputStream 中
            while ((ch = in.read()) != -1) {
                swapStream.write(ch);
            }
            //将ByteArrayOutputStream 转成byte[]
            byte[] bytes = swapStream.toByteArray();
            //通过输出流输出即可
            response.getOutputStream().write(bytes);


//            OutputStream outputStream = new BufferedOutputStream(response.getOutputStream());
//            //创建存放文件内容的数组
//            byte[] buff = new byte[1024];
//            //所读取的内容使用n来接收
//            int n;
//            //当没有读取完时,继续读取,循环
//            while ((n = in.read(buff)) != -1) {
//                //将字节数组的数据全部写入到输出流中
//                outputStream.write(buff, 0, n);
//            }
//            //强制将缓存区的数据进行输出
//            outputStream.flush();
//            //关流
//            outputStream.close();
            in.close();

        } catch (IOException e) {

        }
    }


    /**
     * 查询已签核sop列表
     */
//    @PreAuthorize("@ss.hasPermi('product:sopConfig:edit')")
    @GetMapping("/getSignedSopList")
    public TableDataInfo getSignedSopList(MesOaSop mesOaSop)
    {
        startPage();
        List<Long> list = oaSopService.selectSignedSopListByIds(mesOaSop);
        return getDataTable(list);
    }

    @Autowired
    private IMesSopService mesSopService;

//        /**
//         * 通过sopId+料号，更新数据（按料号推送）
//         */
//        @PostMapping("/updateSignedSopList")
//        public AjaxResult updateSignedSopList(@RequestBody MesBody mesBody) throws IOException {
//
//            List<MesSop> sopIdList = mesBody.getSopIdList();
//            List<MesTerminalSop>  terminalPageList = mesBody.getTerminalPageList();
//            Long sopId;
//            String type;
//            //1.更新mes_sop_group的数据
//            for (int i = 0; i < sopIdList.size(); i++) {
//                sopId = sopIdList.get(i).getSopId();
//                MesSop mesSop = mesSopService.selectMesSopBySopId(sopId);
//                type = mesSop.getType();
//                //1.1通过传来的站点信息（LineId、StageId、ProcessId、TerminalId）查询到表mes_terminal_sop的id
//                for (int j = 0; j <terminalPageList.size() ; j++) {
//                    MesTerminalSop page = terminalPageList.get(j);
//                    MesTerminalSop terminalSop = new MesTerminalSop();
//                    terminalSop.setLineId(page.getLineId());
//                    terminalSop.setStageId(page.getStageId());
//                    terminalSop.setProcessId(page.getProcessId());
//                    terminalSop.setTerminalId(page.getTerminalId());
//
//                    Long id = mesTerminalSopService.selectIdByTerminalSopInfo(terminalSop);
//                    //更新的mes_terminal_sop表的materialId
//                    MesTerminalSop terminalSop1 = new MesTerminalSop();
//                    terminalSop1.setModelId(mesBody.getModelId());
//                    terminalSop1.setMaterialId(mesBody.getMaterialId());
//                    terminalSop1.setId(id);
//
//                    if(id == null){
//                        //1.2 id 不存在，插入数据，获取id
//                        mesTerminalSopService.insertMesTerminalSop(terminalSop);
//                        Long id1 = mesTerminalSopService.selectIdByTerminalSopInfo(terminalSop);
//                        //根据 type 和 group_id（也是表mes_terminal_sop的id） 查询记录是否存在，如果存在，更新，如果不存在，新增
//                        MesSopGroup sopGroup = new MesSopGroup();
//                        sopGroup.setSopGroupId(id1.toString());
//                        sopGroup.setSopId(sopId);
//                        sopGroup.setType(type);
//                        sopGroup.setSopPage(page.getSopPage());
//                        Long l =  mesSopGroupService.selectMesSopGroupTypeNum(sopGroup);
//                        //根据mes_sop_group表的sop_group_id和type更新mes_sop_group表
//                        if(l > 0){
//                            mesSopGroupService.deleteMesSopGroupBySopGroupId(id1,type);
//                        }
//                        mesSopGroupService.insertMesSopGroup(sopGroup);
//                        mesTerminalSopService.updateMesTerminalSop1(terminalSop1);//更新的mes_terminal_sop表的materialId
//                    }
//                    else {
//                        //1.2 根据 type 和 group_id 记录是否存在，如果存在，更新，如果不存在，新增
//                        MesSopGroup sopGroup = new MesSopGroup();
//                        sopGroup.setSopGroupId(id.toString());
//                        sopGroup.setSopId(sopId);
//                        sopGroup.setType(type);
//                        sopGroup.setSopPage(page.getSopPage());
//                        Long l =  mesSopGroupService.selectMesSopGroupTypeNum(sopGroup);
//                        //根据mes_sop_group表的sop_group_id和type更新mes_sop_group表
//                        if(l > 0){
//                            mesSopGroupService.deleteMesSopGroupBySopGroupId(id,type);
//                        }
//                        mesSopGroupService.insertMesSopGroup(sopGroup);
//                        mesTerminalSopService.updateMesTerminalSop1(terminalSop1);//更新的mes_terminal_sop表的materialId
//                    }
//                }
//            }
//
//            //2.将group表更新完的数据发送给前端
//            for (int j = 0; j < terminalPageList.size() ; j++) {
//                MesTerminalSop page = (MesTerminalSop) terminalPageList.get(j);
//
//                MesTerminalSop terminalSop = new MesTerminalSop();
//                terminalSop.setLineId(page.getLineId());
//                terminalSop.setStageId(page.getStageId());
//                terminalSop.setProcessId(page.getProcessId());
//                terminalSop.setTerminalId(page.getTerminalId());
//                Long id = mesTerminalSopService.selectIdByTerminalSopInfo(terminalSop);
//
//                // 根据  group_id 记录是 list
//                String sid = terminalSop.getLineId()+"_"
//                            +terminalSop.getStageId()+"_"
//                            +terminalSop.getProcessId()+"_"
//                            +terminalSop.getTerminalId();
//                List<MesSopGroup> list1 =   mesSopGroupService.selectMesSopGroupBySopGroupId(id.toString());
//                String a =list1.toString();
//                String b = "{"+"\"sopInfoList\":"+a+"}";
//                WebSocketServer.sendInfo(b,sid);
//            }
//            return success("按工站推送成功");
//        }


    /**
     * 按线别推送
     */
    @PostMapping("/sendSopInfoByLine")
    public AjaxResult sendSopInfoByLine(@RequestBody MesBody mesBody) throws IOException {
        List<MesSop> sopIdList = mesBody.getSopIdList();
        List<MesTerminalSop>  terminalPageList = mesBody.getTerminalPageList();
        Long sopId;
        String type;

        for (int i = 0; i < terminalPageList.size(); i++) {
            Long lineId = terminalPageList.get(i).getLineId();
            String sopPage = terminalPageList.get(i).getSopPage();
            //根据线别id查询这个线别的所有站点的id
            List<MesTerminalSop> terminalIdByLineList = mesTerminalSopService.selectTerminalSopInfoByLineId(lineId);
            if (terminalIdByLineList.size() > 0){
                //1.更新mes_sop_group的数据
                for (int j = 0; j < sopIdList.size(); j++) {
                    sopId = sopIdList.get(j).getSopId();
                    MesSop mesSop = mesSopService.selectMesSopBySopId(sopId);
                    type = mesSop.getType();
                    //1.1通过传来的站点信息（ModelId、MaterialId、LineId、StageId、ProcessId、TerminalId）查询到表mes_terminal_sop的id
                    for (int k = 0; k < terminalIdByLineList.size(); k++) {
                        MesTerminalSop terminalSop = new MesTerminalSop();
                        terminalSop = terminalIdByLineList.get(k);
                        Long id = mesTerminalSopService.selectIdByTerminalSopInfo(terminalSop);
                        if (id == null) {
                            //1.2 mes_terminal_sop表的id 不存在，插入数据，获取id
                            mesTerminalSopService.insertMesTerminalSop(terminalSop);
                            Long id1 = mesTerminalSopService.selectIdByTerminalSopInfo(terminalSop);
                            //根据 type 和 group_id（也是表mes_terminal_sop的id） 查询记录是否存在，如果存在，更新，如果不存在，新增
                            MesSopGroup sopGroup = new MesSopGroup();
                            sopGroup.setSopGroupId(id1.toString());
                            sopGroup.setSopId(sopId);
                            sopGroup.setType(type);
                            sopGroup.setSopPage(sopPage);
                            Long l = mesSopGroupService.selectMesSopGroupTypeNum(sopGroup);
                            //根据mes_sop_group表的sop_group_id和type更新mes_sop_group表
                            if (l > 0) {
                                mesSopGroupService.deleteMesSopGroupBySopGroupId(id1, type);
                            }
                            mesSopGroupService.insertMesSopGroup(sopGroup);
                        } else {
                            //1.2 根据 type 和 group_id 记录是否存在，如果存在，更新，如果不存在，新增
                            MesSopGroup sopGroup = new MesSopGroup();
                            sopGroup.setSopGroupId(id.toString());
                            sopGroup.setSopId(sopId);
                            sopGroup.setType(type);
                            sopGroup.setSopPage(sopPage);
                            Long l = mesSopGroupService.selectMesSopGroupTypeNum(sopGroup);
                            //根据mes_sop_group表的sop_group_id和type更新mes_sop_group表
                            if (l > 0) {
                                mesSopGroupService.deleteMesSopGroupBySopGroupId(id, type);
                            }
                            mesSopGroupService.insertMesSopGroup(sopGroup);
                        }
                    }
                }
            }
            else {return AjaxResult.error("该线别没有对应的站点信息！");}
        }

        //参考上面的“按料号推送”，因为发送的数据中，terminalPageList内包含了LineId、StageId、ProcessId、TerminalId，所以只需要直接将四个id赋值给terminalSop
        //但是“按线别推送”，发送的数据只有LineId，所以要加一个for循环遍历terminalIdByLineList，从而查出上面四个id
        for (int n = 0; n < terminalPageList.size() ; n++) {
            Long lineId = terminalPageList.get(n).getLineId();
            //根据线别id查询这个线别的所有站点的id
            List<MesTerminalSop> terminalIdByLineList = mesTerminalSopService.selectTerminalSopInfoByLineId(lineId);
            MesTerminalSop page = (MesTerminalSop) terminalPageList.get(n);
            MesTerminalSop terminalSop = new MesTerminalSop();
            for (int m = 0; m < terminalIdByLineList.size(); m++) {
                terminalSop.setLineId(page.getLineId());
                terminalSop.setModelId(terminalIdByLineList.get(m).getModelId());
                terminalSop.setMaterialId(terminalIdByLineList.get(m).getMaterialId());
                terminalSop.setStageId(terminalIdByLineList.get(m).getStageId());
                terminalSop.setProcessId(terminalIdByLineList.get(m).getProcessId());
                terminalSop.setTerminalId(terminalIdByLineList.get(m).getTerminalId());
                Long id = mesTerminalSopService.selectIdByTerminalSopInfo(terminalSop);

                String sid = terminalSop.getModelId()+"_"
                        +terminalSop.getMaterialId()+"_"
                        +terminalSop.getLineId()+"_"
                        +terminalSop.getStageId()+"_"
                        +terminalSop.getProcessId()+"_"
                        +terminalSop.getTerminalId();
                // 根据  group_id 记录是 list
                List<MesSopGroup> list1 =   mesSopGroupService.selectMesSopGroupBySopGroupId(id.toString());
                String a =list1.toString();
                String b = "{"+"\"sopInfoList\":"+a+"}";
                WebSocketServer.sendInfo(b,sid);
            }
        }
        return success("按线别推送成功");
    }


    /**
     * 按制程推送（将某制程上的所有mac地址的sop都换掉）
     */
//    @PostMapping("/sendSopInfoByProcess")
//    public AjaxResult sendSopInfoByProcess(@RequestBody MesBody mesBody) throws IOException {
//        Long modelId = mesBody.getModelId();
//        Long materialId= mesBody.getMaterialId();
//        List<MesSop> sopIdList = mesBody.getSopIdList();
//        List<MesTerminalSop>  terminalPageList = mesBody.getTerminalPageList();
//        Long sopId;
//        String type;
//
//        for (int i = 0; i < terminalPageList.size(); i++) {
//            String sopPage = terminalPageList.get(i).getSopPage();
//            MesTerminal processIds = new MesTerminal();
//            processIds.setLineId(terminalPageList.get(i).getLineId());
//            processIds.setStageId(terminalPageList.get(i).getStageId());
//            processIds.setProcessId(terminalPageList.get(i).getProcessId());
//            processIds.setStatus("0");
//            //根据line+stage+process查询mes_terminal表里面该制程绑定了的所有terminalId(mac地址的Id)
//            List<MesTerminal> terminalIdByProcessList = mesTerminalService.selectMesTerminalList(processIds);
//            if (terminalIdByProcessList.size() > 0){
//                //1.更新mes_sop_group的数据
//                for (int j = 0; j < sopIdList.size(); j++) {
//                    sopId = sopIdList.get(j).getSopId();
//                    MesSop mesSop = mesSopService.selectMesSopBySopId(sopId);
//                    type = mesSop.getType();
//                    //1.1通过传来的站点信息（LineId、StageId、ProcessId、TerminalId）查询到表mes_terminal_sop的id
//                    for (int k = 0; k < terminalIdByProcessList.size(); k++) {
//                        MesTerminalSop terminalSop = new MesTerminalSop();
//                        terminalSop.setModelId(modelId);
//                        terminalSop.setLineId(materialId);
//                        terminalSop.setLineId(terminalIdByProcessList.get(k).getLineId());
//                        terminalSop.setStageId(terminalIdByProcessList.get(k).getStageId());
//                        terminalSop.setProcessId(terminalIdByProcessList.get(k).getProcessId());
//                        terminalSop.setTerminalId(terminalIdByProcessList.get(k).getTerminalId());
//                        Long id = mesTerminalSopService.selectIdByTerminalSopInfo(terminalSop);
//                        if (id == null) {
//                            //1.2 mes_terminal_sop表的id 不存在，插入数据，获取id
//                            mesTerminalSopService.insertMesTerminalSop(terminalSop);
//                            Long id1 = mesTerminalSopService.selectIdByTerminalSopInfo(terminalSop);
//                            //根据 type 和 group_id（也是表mes_terminal_sop的id） 查询记录是否存在，如果存在，更新，如果不存在，新增
//                            MesSopGroup sopGroup = new MesSopGroup();
//                            sopGroup.setSopGroupId(id1.toString());
//                            sopGroup.setSopId(sopId);
//                            sopGroup.setType(type);
//                            sopGroup.setSopPage(sopPage);
//                            Long l = mesSopGroupService.selectMesSopGroupTypeNum(sopGroup);
//                            //根据mes_sop_group表的sop_group_id和type更新mes_sop_group表
//                            if (l > 0) {
//                                mesSopGroupService.deleteMesSopGroupBySopGroupId(id1, type);
//                            }
//                            mesSopGroupService.insertMesSopGroup(sopGroup);
//                        } else {
//                            //1.2 根据 type 和 group_id 记录是否存在，如果存在，更新，如果不存在，新增
//                            MesSopGroup sopGroup = new MesSopGroup();
//                            sopGroup.setSopGroupId(id.toString());
//                            sopGroup.setSopId(sopId);
//                            sopGroup.setType(type);
//                            sopGroup.setSopPage(sopPage);
//                            Long l = mesSopGroupService.selectMesSopGroupTypeNum(sopGroup);
//                            //根据mes_sop_group表的sop_group_id和type更新mes_sop_group表
//                            if (l > 0) {
//                                mesSopGroupService.deleteMesSopGroupBySopGroupId(id, type);
//                            }
//                            mesSopGroupService.insertMesSopGroup(sopGroup);
//                        }
//                    }
//                }
//            }
//            else {return AjaxResult.error("该制程没有绑定的mac地址信息，请先绑定mac地址！");}
//        }
//
//        //参考上面的“按料号推送”，因为发送的数据中，terminalPageList内包含了LineId、StageId、ProcessId、TerminalId，所以只需要直接将四个id赋值给terminalSop
//        //但是“按线别推送”，发送的数据只有LineId，所以要加一个for循环遍历terminalIdByLineList，从而查出上面四个id
//        for (int n = 0; n < terminalPageList.size() ; n++) {
//            MesTerminal processIds = new MesTerminal();
//            processIds.setLineId(terminalPageList.get(n).getLineId());
//            processIds.setStageId(terminalPageList.get(n).getStageId());
//            processIds.setProcessId(terminalPageList.get(n).getProcessId());
//            processIds.setStatus("0");
//            //根据line+stage+process查询mes_terminal表里面该制程绑定了的所有terminalId(mac地址的Id)
//            List<MesTerminal> terminalIdByProcessList = mesTerminalService.selectMesTerminalList(processIds);
//            MesTerminalSop page = (MesTerminalSop) terminalPageList.get(n);
//            MesTerminalSop terminalSop = new MesTerminalSop();
//            for (int m = 0; m < terminalIdByProcessList.size(); m++) {
//                terminalSop.setLineId(page.getLineId());
//                terminalSop.setStageId(terminalIdByProcessList.get(m).getStageId());
//                terminalSop.setProcessId(terminalIdByProcessList.get(m).getProcessId());
//                terminalSop.setTerminalId(terminalIdByProcessList.get(m).getTerminalId());
//                Long id = mesTerminalSopService.selectIdByTerminalSopInfo(terminalSop);
//
//                String sid = terminalSop.getLineId()+"_"
//                            +terminalSop.getStageId()+"_"
//                            +terminalSop.getProcessId()+"_"
//                            +terminalSop.getTerminalId();
//                // 根据  group_id 记录是 list
//                List<MesSopGroup> list1 =   mesSopGroupService.selectMesSopGroupBySopGroupId(id.toString());
//                String a =list1.toString();
//                String b = "{"+"\"sopInfoList\":"+a+"}";
//                WebSocketServer.sendInfo(b,sid);
//            }
//        }
//        return success("按制程推送成功");
//    }


    /**
     * 按mac推送
     */
    @PostMapping("/updateSignedSopList")
    @Transactional(rollbackFor = Exception.class)
//    @PostMapping("/updateSignedSopListByMac")
    public AjaxResult updateSignedSopList(@RequestBody MesBody mesBody) throws IOException {

        List<MesSop> sopIdList = mesBody.getSopIdList();
        if(sopIdList.size()==0){ throw new CustomException("SOP文档/视频不能为空!");}
        List<MesTerminalSop>  terminalPageList = mesBody.getTerminalPageList();
        Long sopId;
        String type;
        //1.更新mes_sop_group的数据
        for (int i = 0; i < sopIdList.size(); i++) {
            sopId = sopIdList.get(i).getSopId();
            MesSop mesSop = mesSopService.selectMesSopBySopId(sopId);
            type = mesSop.getType();
            //1.1通过传来的站点信息（LineId、StageId、ProcessId、TerminalId）查询到表mes_terminal_sop的id
            //terminalPageList对应推送页面下方的的每一行sop信息
            for (int j = 0; j <terminalPageList.size() ; j++) {
                MesTerminalSop page = terminalPageList.get(j);
                MesTerminalSop terminalSop = new MesTerminalSop();
                //terminalSop.setModelId(mesBody.getModelId());
                //terminalSop.setMaterialId(mesBody.getMaterialId());
                terminalSop.setLineId(page.getLineId());
                terminalSop.setStageId(page.getStageId());
                terminalSop.setProcessId(page.getProcessId());
                terminalSop.setTerminalId(page.getMacId());


                MesUploadTerminalPage uploadedMacInfo = new MesUploadTerminalPage();
                uploadedMacInfo.setModelId(mesBody.getModelId());
                uploadedMacInfo.setMaterialId(mesBody.getMaterialId());
                uploadedMacInfo.setLineId(page.getLineId());
                uploadedMacInfo.setStageId(page.getStageId());
                uploadedMacInfo.setProcessId(page.getProcessId());
//                uploadedMacInfo.setTerminalId(page.getMacId());
                uploadedMacInfo.setTerminalId(null);
                //现在的逻辑：line+stage+process绑定页码，这process无论先后增删改多少mac，都可以直接根据process页码推送sop
                List<MesUploadTerminalPage> uploadedMaclist = mesUploadTerminalPageService.selectMesUploadTerminalPageList(uploadedMacInfo);

                Long id = mesTerminalSopService.selectIdByTerminalSopInfo(terminalSop);
//                //更新的mes_terminal_sop表的materialId
//                MesTerminalSop terminalSop1 = new MesTerminalSop();
//                terminalSop1.setModelId(mesBody.getModelId());
//                terminalSop1.setMaterialId(mesBody.getMaterialId());
//                terminalSop1.setId(id);

                if(id == null){
                    //1.2 id 不存在，插入数据，获取id
                    terminalSop.setModelId(mesBody.getModelId());
                    mesTerminalSopService.insertMesTerminalSop(terminalSop);
                    Long id1 = mesTerminalSopService.selectIdByTerminalSopInfo(terminalSop);
                    //根据 type 和 group_id（也是表mes_terminal_sop的id） 查询记录是否存在，如果存在，更新，如果不存在，新增
                    MesSopGroup sopGroup = new MesSopGroup();
                    sopGroup.setSopGroupId(id1.toString());
                    sopGroup.setSopId(sopId);
                    sopGroup.setType(type);
                    sopGroup.setPushStatus("1");

/*                    //根据line+stage+stage+terminal查出某个sop页码
                    MesUploadTerminalPage upload = new MesUploadTerminalPage();
//                    upload.setModelId(mesBody.getModelId());
//                    upload.setMaterialId(mesBody.getMaterialId());
                    upload.setLineId(page.getLineId());
                    upload.setStageId(page.getStageId());
                    upload.setProcessId(page.getProcessId());
                    //upload.setTerminalId(page.getMacId());
                    upload.setSopId(sopId);
                    MesUploadTerminalPage uploadPage = mesUploadTerminalPageService.selectUploadPageByIds(upload);
                    sopGroup.setSopPage(page.getSopPage());//无法从page获取到数据，直接先从upload表拿数据*/

                    Long l =  mesSopGroupService.selectMesSopGroupTypeNum(sopGroup);
                    //根据mes_sop_group表的sop_group_id和type更新mes_sop_group表
                    if(l > 0){
                        mesSopGroupService.deleteMesSopGroupBySopGroupId(id1,type);
                    }
                    sopGroup.setSopPage(page.getSopPage());
                    sopGroup.setSopInterval(page.getInterval());
                    mesSopGroupService.insertMesSopGroup(sopGroup);
//                    mesTerminalSopService.updateMesTerminalSop1(terminalSop1);//更新的mes_terminal_sop表的materialId
                }
                else {
                    terminalSop.setId(id);
                    terminalSop.setModelId(mesBody.getModelId());
                    mesTerminalSopService.updateMesTerminalSop1(terminalSop);
                    //1.2 根据 type 和 group_id 记录是否存在，如果存在，更新，如果不存在，新增
                    //只要走了当前这个推送接口，group表的pushStatus改为已推送状态（PushStatus = 1）
                    MesSopGroup sopGroup = new MesSopGroup();
                    sopGroup.setSopGroupId(id.toString());
                    sopGroup.setSopId(sopId);
                    sopGroup.setType(type);
                    sopGroup.setPushStatus("1");

/*                    //根据model+material+line+stage+stage+terminal查出某个sop页码
                    MesUploadTerminalPage upload = new MesUploadTerminalPage();
//                    upload.setModelId(mesBody.getModelId());
//                    upload.setMaterialId(mesBody.getMaterialId());
                    upload.setLineId(page.getLineId());
                    upload.setStageId(page.getStageId());
                    upload.setProcessId(page.getProcessId());
                    upload.setTerminalId(page.getMacId());
                    upload.setSopId(sopId);
                    MesUploadTerminalPage uploadPage = mesUploadTerminalPageService.selectUploadPageByIds(upload);*/
//                    if(null == uploadPage)
//                        throw new CustomException("请检查当前\'"+page.getMacName()+"\'是否上传签核了对应的SOP");
                    sopGroup.setSopPage(page.getSopPage());//无法从page获取到数据，直接先从upload表拿数据
                    sopGroup.setSopInterval(page.getInterval());

                    Long l =  mesSopGroupService.selectMesSopGroupTypeNum(sopGroup);
                    //根据mes_sop_group表的sop_group_id和type更新mes_sop_group表
                    if(l > 0){
                        mesSopGroupService.deleteMesSopGroupBySopGroupId(id,type);
                    }
                    sopGroup.setPushStatus("1");//0 未推送  1已推送
                    mesSopGroupService.insertMesSopGroup(sopGroup);
//                    mesTerminalSopService.updateMesTerminalSop1(terminalSop1);//更新的mes_terminal_sop表的materialId
                }
            }
        }

        //2.将group表更新完的数据发送给前端
        for (int j = 0; j < terminalPageList.size() ; j++) {
            MesTerminalSop page = (MesTerminalSop) terminalPageList.get(j);

            MesTerminalSop terminalSop = new MesTerminalSop();
            terminalSop.setLineId(page.getLineId());
            terminalSop.setStageId(page.getStageId());
            terminalSop.setProcessId(page.getProcessId());
            terminalSop.setTerminalId(page.getMacId());
//            terminalSop.setModelId(page.getModelId());
            Long id = mesTerminalSopService.selectIdByTerminalSopInfo(terminalSop);

            // 根据  group_id 记录是 list
            String sid = terminalSop.getLineId()+"_"
                    +terminalSop.getStageId()+"_"
                    +terminalSop.getProcessId()+"_"
                    +terminalSop.getTerminalId();
            List<MesSopGroup> list1 =   mesSopGroupService.selectMesSopGroupBySopGroupId(id.toString());
            String a =list1.toString();
            String b = "{"+"\"sopInfoList\":"+a+"}";
            WebSocketServer.sendInfo(b,sid);
        }
        return success("按mac地址推送成功");
    }

}
