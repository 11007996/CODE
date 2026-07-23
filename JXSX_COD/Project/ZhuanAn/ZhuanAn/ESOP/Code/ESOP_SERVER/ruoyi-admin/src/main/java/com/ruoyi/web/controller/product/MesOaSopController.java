package com.ruoyi.web.controller.product;

import com.alibaba.fastjson2.JSONObject;
import com.ruoyi.common.annotation.Log;
import com.ruoyi.common.constant.HttpStatus;
import com.ruoyi.common.core.controller.BaseController;
import com.ruoyi.common.core.domain.AjaxResult;
import com.ruoyi.common.core.domain.entity.SysRole;
import com.ruoyi.common.core.domain.entity.SysUser;
import com.ruoyi.common.core.page.TableDataInfo;
import com.ruoyi.common.enums.BusinessType;
import com.ruoyi.common.exception.CustomException;
import com.ruoyi.common.utils.DateUtils;
import com.ruoyi.common.utils.SecurityUtils;
import com.ruoyi.common.utils.file.FileUtils;
import com.ruoyi.common.utils.http.HttpUtils;
import com.ruoyi.common.utils.poi.ExcelUtil;
import com.ruoyi.common.utils.sign.Md5Utils;
import com.ruoyi.product.domain.*;
import com.ruoyi.product.service.*;
import com.ruoyi.replace.service.IMesMaterialInfoService;
import com.ruoyi.system.service.ISysConfigService;
import com.ruoyi.system.service.ISysRoleService;
import com.ruoyi.system.service.ISysUserService;
import com.ruoyi.web.controller.product.Encrypt.EncryptPdfController;
import com.ruoyi.web.controller.product.Encrypt.EncryptVideoController;
import com.ruoyi.web.controller.product.JsonObjectLuxlink.JsonObjectLuxLinkBody;
import com.ruoyi.web.controller.product.JsonObjectLuxlink.JsonObjectLuxLinkBodyMaster;
import com.ruoyi.web.controller.product.JsonObjectLuxlink.JsonObjectLuxlinkBodyDetail;
import com.ruoyi.ws.WsClientActuator;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.multipart.MultipartFile;

import javax.annotation.Resource;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.List;
import java.util.Map;

/**
 * oa签核Controller
 *
 * @author ruoyi
 * @date 2022-09-21
 */

//@CrossOrigin(origins = "http://10.33.22.28:8090", maxAge = 3600)
@CrossOrigin(origins = "http://localhost:8090", maxAge = 3600)
@RestController
@RequestMapping("/product/oa")
public class MesOaSopController extends BaseController {
    @Autowired
    private IMesOaSopService mesOaSopService;

    @Autowired
    private IMesModelService mesModelService;

    @Autowired
    private IMesSopService mesSopService;

    @Autowired
    private ISysRoleService sysRoleService;

    @Autowired
    private ISysUserService userService;

    @Autowired
    private IMesStageService mesStageService;

    @Autowired
    private ISysConfigService configService;

    @Resource
    private WsClientActuator wsClientActuator;

    EncryptPdfController encryptPdfController;
    EncryptVideoController encryptVideoController;
//    FfmpegCmdHls4M3u8EncTest ffmpegCmdHls4M3u8EncTest;


    @Autowired
    private IMesUploadTerminalPageService mesUploadTerminalPageService;
    @Autowired
    private IMesProcessService mesProcessService;
    @Autowired
    private IMesMaterialInfoService mesMaterialInfoService;
    @Autowired
    private IMesTerminalService mesTerminalService;
    @Autowired
    private IMesMacService mesMacService;

    private static final Logger log = LoggerFactory.getLogger(MesOaSopController.class);

    //    private static final String ip = "172.19.129.224";
    private static final String ip = "10.33.22.28";
//    private static final String ip = "10.16.204.143";

    /**
     * 查询oa签核列表
     */
    @PreAuthorize("@ss.hasPermi('product:oa:list')")
    @GetMapping("/list")
    public TableDataInfo list(MesOaSop mesOaSop) {
        startPage();
        List<MesOaSop> list = mesOaSopService.selectMesOaSopList(mesOaSop);
        return getDataTable(list);
    }

    /**
     * 查询oa签核人员列表
     */
    @PreAuthorize("@ss.hasPermi('product:oa:list')")
    @GetMapping("/selectOACountersignUserList")
    public TableDataInfo selectOACountersignUserList() {
        SysRole role = sysRoleService.SelectRoleByRolekey("OACountersignUser");
        SysUser user = new SysUser();
        user.setRoleId(role.getRoleId());
        List<SysUser> list = userService.selectAllocatedList(user);
        return getDataTable(list);
    }

    /**
     * 导出oa签核列表
     */
    @PreAuthorize("@ss.hasPermi('product:oa:export')")
    @Log(title = "oa签核", businessType = BusinessType.EXPORT)
    @PostMapping("/export")
    public void export(HttpServletResponse response, MesOaSop mesOaSop) {
        List<MesOaSop> list = mesOaSopService.selectMesOaSopList(mesOaSop);
        ExcelUtil<MesOaSop> util = new ExcelUtil<MesOaSop>(MesOaSop.class);
        util.exportExcel(response, list, "oa签核数据");
    }

    /**
     * 获取oa签核详细信息
     */
    @PreAuthorize("@ss.hasPermi('product:oa:query')")
    @GetMapping(value = "/{oaId}")
    public AjaxResult getInfo(@PathVariable("oaId") String oaId) {
        return AjaxResult.success(mesOaSopService.selectMesOaSopByOaId(oaId));
    }

    /**
     * 修改oa签核
     */
    @PreAuthorize("@ss.hasPermi('product:oa:edit')")
    @Log(title = "oa签核", businessType = BusinessType.UPDATE)
    @PutMapping
    public AjaxResult edit(@RequestBody MesOaSop mesOaSop) {
        return toAjax(mesOaSopService.updateMesOaSop(mesOaSop));
    }

    /**
     * 删除oa签核
     */
    @PreAuthorize("@ss.hasPermi('product:oa:remove')")
    @Log(title = "oa签核", businessType = BusinessType.DELETE)
    @DeleteMapping("/{oaIds}")
    public AjaxResult remove(@PathVariable String[] oaIds) {
        return toAjax(mesOaSopService.deleteMesOaSopByOaIds(oaIds));
    }


    /**
     * 获取 机种信息
     */
    @PreAuthorize("@ss.hasPermi('product:oa:list')")
    @GetMapping("/getMesModelList")
    public TableDataInfo list(MesModel model) {
        model.setStatus("0");
        List<MesModel> list = mesModelService.selectMesModelList(model);
        return getDataTable(list);
    }

    /**
     * 新增oa签核
     */
    @PreAuthorize("@ss.hasPermi('product:oa:add')")
    @Log(title = "oa签核", businessType = BusinessType.INSERT)
    @PostMapping("/addOaEsopInfo")
    @CrossOrigin
    public AjaxResult addOaEsopInfo(@RequestParam(value = "file") MultipartFile file, MesOaSop mesOaSop) throws Exception {
        String a= SecurityUtils.getUsername();
        if(!a.equals("admin")){
            try {
                if(mesOaSop.getTerminalPageList() == null){
                    return AjaxResult.error("请点击添加信息");
                }
/*                log.info("===========【OA签核】开始检查版本============= ");
                // by站点检查版本唯一
                MesOaSop oasop = new MesOaSop();
//                oasop.setModelId(mesOaSop.getModelId());
//                oasop.setMaterialId(mesOaSop.getMaterialId());
                oasop.setLineId(mesOaSop.getTerminalPageList().get(0).getLineId());
                oasop.setStageId(mesOaSop.getTerminalPageList().get(0).getStageId());
                oasop.setProcessId(mesOaSop.getProcessId());
                oasop.setVersion(mesOaSop.getVersion());*/
                String s1 = file.getOriginalFilename().substring(file.getOriginalFilename().length()-3);
                s1 = s1.toLowerCase();

/*                List<MesOaSop> list = null;
                //检查版本唯一性：已签核的版本号不可以重复，若同时上传多个相同版本号的，签核完第一个，其他都退回（status=3） status：1签核中 2签核成功 3 签核失败
                //检查文档(0:pdf)版本
                if(s1.equals("pdf") ){
                    oasop.setType("0");
                    list = mesOaSopService.selectMesOaSopVersionNum(oasop);
                    if(list.size() > 0){
//                        String m = mesMaterialInfoService.selectMesMaterialInfoById(oasop.getMaterialId()).getMaterialName();
                        return AjaxResult.error("该站位序号的文档版本:"+oasop.getVersion()+"已存在");
                    }
                }
                //检查视频(1:mp4、mts、avi)版本
                else if (mesOaSop.getTerminalPageList().size() >= 1) {
                    for (int i = 0; i < mesOaSop.getTerminalPageList().size(); i++) {
                        oasop.setType("1");
                        oasop.setProcessId(mesOaSop.getTerminalPageList().get(i).getProcessId());
                        list = mesOaSopService.selectMesOaSopVersionNum(oasop);
                        if(list.size() > 0){
//                                String p = mesProcessService.selectMesProcessByProcessId(oasop.getProcessId()).getProcessName();
                            return AjaxResult.error("该站位序号的视频版本:"+oasop.getVersion()+"已存在");
                        }
                    }
                }

                log.info("===========【OA签核】检查版本成功============= ");*/
                //上传并返回加密的文件url和文件名称
                String fileName = file.getOriginalFilename();
                byte[] fileContent = file.getBytes();
                Map<String,Object> map = new AjaxResult();
                //文件服务器真实路径
                String filePath ="";
                //浏览器访问路径
                String browserUrl ="";
                //PDF密码
                String pdfPassWord ="";
                if(s1.equals("mp4") || s1.equals("mts")){
                    map = EncryptVideoController.encryptVideo(fileName,fileContent);

                } else if (s1.equals("pdf")) {
                    map = EncryptPdfController.encryptPdf(fileName,fileContent);
                }
                int resultCode = (int) map.get("code");
                if(resultCode == 200){
                    filePath = map.get("filePath").toString();
                    browserUrl = map.get("browserUrl").toString();
                    if (s1.equals("pdf")){
                        pdfPassWord = map.get("pdfPassWord").toString();
                    }
                }else {
                    return AjaxResult.error(HttpStatus.ERROR,map.get("msg").toString());
                }

                String preNewFileName = FileUtils.getName(filePath) ;
                String ee = preNewFileName.substring(0,preNewFileName.indexOf("."));
                System.out.println(ee);
                SimpleDateFormat sdf = new SimpleDateFormat("yyyyMMddHHmmSS");
                String dd = sdf.format(new Date());
                String newFileName = ee+"_"+dd+preNewFileName.substring(preNewFileName.indexOf("."));
                System.out.println(newFileName);


                log.info("===========【OA签核】文件上传成功============= ");
                // 保存到 SOP 表
                MesSop sop = new MesSop();
                sop.setOriginalName(file.getOriginalFilename());
                sop.setSopName(newFileName);
                sop.setFilePath(browserUrl);
                sop.setVersion(mesOaSop.getVersion());
                sop.setCreateBy(getUsername());
                sop.setStatus("1");
                sop.setPassWord(pdfPassWord);

                String url = "http://" + ip + ":8090" + filePath;
                sop.setUrl(url);
                String s = fileName.substring(file.getOriginalFilename().length()-3);
                s=s.toLowerCase();
                if (s.equals("pdf")) {
                    sop.setType("0");
                    if (mesOaSop.getSopInterval() != null){
                        sop.setSopInterval(mesOaSop.getSopInterval());
                    }
                    else {
                        sop.setSopInterval("180");
                    }
                }
//                else if(s.equals("mp4") || s.equals("mts") || s.equals("avi") || s.equals("wmv")){
                else if(s.equals("mp4") || s.equals("mts")){
                    sop.setType("1");
                    sop.setSopInterval(null);
                }
                else {
//                    return AjaxResult.error("请上传pdf、mp4、mts、avi、wmv格式的文件");}
                    return AjaxResult.error("请上传pdf、mp4、mts格式的文件");}
                mesSopService.insertMesSop(sop);
                // 获取Id
                Long sopId = mesSopService.selectSopIdByName(newFileName).getSopId();
                log.info("===========【OA签核】已保存到SOP 表 ============= 通过新文件名称获取 sopid : " + sopId);

                // 保存到OA签核表
//                MesModel model =  mesModelService.selectMesModelByModelId(mesOaSop.getModelId());
                MesStage stage = mesStageService.selectMesStageByStageId(mesOaSop.getTerminalPageList().get(0).getStageId());
                mesOaSop.setOaId(stage.getStageName() + sop.getSopName().substring(sop.getOriginalName().length() - 4, sop.getSopName().length() - 4 ));
                mesOaSop.setSopId(sopId);
                mesOaSop.setRemark(mesOaSop.getRemark());
                mesOaSop.setCreateBy(getUsername());
                mesOaSop.setCountersignUser(mesOaSop.getCountersignUser());
                mesOaSop.setNotifyUser(mesOaSop.getNotifyUser());
                mesOaSopService.insertMesOaSop(mesOaSop);

                //保存到mes_upload_terminal_page表
                MesUploadTerminalPage uploadInfo = new MesUploadTerminalPage();
                uploadInfo.setSopId(sopId);
                uploadInfo.setModelId(mesOaSop.getModelId());
                uploadInfo.setMaterialId(mesOaSop.getMaterialId());
                uploadInfo.setOaId(mesOaSop.getOaId());//把luxId也就是oaId保存到upload_terminal_page表
                if (s.equals("pdf")) {
                    uploadInfo.setType("0");
                } else if(s.equals("mp4")||s.equals("mts")){
                    uploadInfo.setType("1");
                }
                else {
//                    return AjaxResult.error("请上传pdf格式的文档或者mp4、mts、avi、wmv格式的视频");}
                    return AjaxResult.error("请上传pdf格式的文档或者mp4、mts格式的视频");}
                //terminalPageList不需要传terminalId的值了，sop、sop页码、sop间隔只绑定到line+stage+process
                List<MesUploadTerminalPage> list2 = mesOaSop.getTerminalPageList();
                Long lineId,stageId,processId,terminalId;
                String sopPage,sopInterval;
                // 循环遍历terminalPageList（不带macId玩了）
                for (int i = 0; i < list2.size(); i++) {
                    lineId = list2.get(i).getLineId();
                    stageId = list2.get(i).getStageId();
                    processId = list2.get(i).getProcessId();
                    sopPage = list2.get(i).getSopPage();
                    sopInterval = list2.get(i).getSopInterval();
                    uploadInfo.setLineId(lineId);
                    uploadInfo.setStageId(stageId);
                    uploadInfo.setProcessId(processId);
//                    uploadInfo.setTerminalId(list2.get(i).getMacId());
                    uploadInfo.setSopPage(sopPage);
                    uploadInfo.setSopInterval(sopInterval);
                    uploadInfo.setCreateTime(DateUtils.parseDate(DateUtils.getTime()));
                    uploadInfo.setCreateBy(getUsername());
                    mesUploadTerminalPageService.insertMesUploadTerminalPage(uploadInfo);
                }
                log.info("===========【OA签核】已保存到OA签核表============= OaId = " + mesOaSop.getOaId());
                //发送POST 请求
//                sendLuxLink(mesOaSop.getOaId());
            }catch (IOException ex){
                log.error(ex.getMessage());
                return AjaxResult.error(ex.getMessage());
            }
        }
        else {
            return AjaxResult.error("admin账号无法进行SOP新增上传");
        }
        return toAjax(1);
    }



    @PreAuthorize("@ss.hasPermi('product:oa:edit')")
    @Log(title = "luxLink签核", businessType = BusinessType.INSERT)
    @GetMapping(value = "/sendLuxLink/{luxId}")
    public AjaxResult sendLuxLink(@PathVariable("luxId") String luxId)
    {
        log.info("===========【LuxLink签核】开始发送签核命令============= ");

        // LuxLink 参数
        JsonObjectLuxLinkBody luxJsonBody = new JsonObjectLuxLinkBody();
        luxJsonBody.setAccount("ESOP-BPM");
        luxJsonBody.setPassword(Md5Utils.hash("ESOP-BPMesop20221108lux"));
        luxJsonBody.setProcessDefId("obj_05867128905245069239b2dca31ebe59");
        luxJsonBody.setCreateuser(getUsername());

        MesOaSop oaSop = mesOaSopService.selectInfoByOaId(luxId);
        //LuxLink  Body参数 master
        JsonObjectLuxLinkBodyMaster luxJsonBodyMaster = new JsonObjectLuxLinkBodyMaster();
        luxJsonBodyMaster.setBoName("BO_EU_MFG_ESOP");
        luxJsonBodyMaster.setLuxid(oaSop.getOaId());
        luxJsonBodyMaster.setCountersignPerson(oaSop.getCountersignUser());
        luxJsonBodyMaster.setNotifyPerson(oaSop.getNotifyUser());
        luxJsonBodyMaster.setWriteBackUrl("http://localhost:8090/luxlink/UpdateEsop");//localhost
//        luxJsonBody.setMaster((String) JSON.toJSON(luxJsonBodyMaster.toJsonString()));
        luxJsonBody.setMaster(luxJsonBodyMaster.toJsonString());

        // LuxLink 参数 --detail[{},{}...]
        JsonObjectLuxlinkBodyDetail detailJson = new JsonObjectLuxlinkBodyDetail();
        detailJson.setBoName("BO_EU_MFG_ESOP_S1");
//        JSONObject detailJson = new JSONObject();
//        detailJson.put("boName","BO_EU_MFG_ESOP_S1");

//        JsonObjectLuxLinkDetailBoItem luxJsonDetailBoItem = new JsonObjectLuxLinkDetailBoItem();
        List<MesUploadTerminalPage> mesUploadTerminalPageList= mesUploadTerminalPageService.selectMesUploadTerminalPageListById(oaSop.getSopId());
        String str ="[";
        for (int i = 0; i < mesUploadTerminalPageList.size(); i++) {
            MesUploadTerminalPage mul = mesUploadTerminalPageList.get(i);
            str+= mul.toJSONString()+",";
        }
        if(str.length() >1){
            str= str.substring(0,str.length()-1);
        }
        str += "]";

//        String str1 = mesUploadTerminalPageList.toString();
//        String str= JSON.toJSON(mesUploadTerminalPageList).toString();//list转json（用阿里jar包）,json.toJSON格式的key和value自带双引号（"key":"value"）
        detailJson.setBoItem(str);//给boItem赋值str
//        detailJson.put("boItem",str);//给boItem赋值str
//        String luxJson =   "{" + luxJsonBody.toJsonString() + ",\"detail\":[" + detailJson.toString() + "]}";
//        luxJsonBody.setDetail(detailJson.toJSONString());
        luxJsonBody.setDetail(detailJson.toJsonString());
//        luxJsonBody.setDetail((String) JSON.toJSON(detailJson.toJsonString()));
        String luxJson =  luxJsonBody.toJsonString();
        String luxJson1 =luxJson;

        //这个url是用来给httpUtils发送请求，看能否获取RESULT值
        String url = configService.selectConfigByKey("product.luxlink.url");
        String json = luxJson1;
        Map<String, Object> map = JSONObject.parseObject(HttpUtils.sendJsonPost(url,json));
        boolean ok = (boolean) map.get("RESULT");
        if (!ok) {
            log.info("===========【LuxLink签核】发送签核失败============= ");
            return AjaxResult.error(map.get("msg").toString());
        }else {
            //RESULT为true的时候(发送LuxLink签核信息成功)，将数据添加到mes_oa_sop表
            String rid =  map.get("REQUESTID").toString();
            MesOaSop mesOaSop = new MesOaSop();
            mesOaSop.setOaId(oaSop.getOaId());
            mesOaSop.setRequestId(rid);
            mesOaSop.setStatus("1");
            mesOaSopService.updateMesOaSop(mesOaSop);
            log.info("===========【LuxLink签核】发送签核完成============= ");
        }
        return toAjax(1);
    }
}


