package com.ruoyi.web.controller.product;

import com.ruoyi.common.annotation.Log;
import com.ruoyi.common.core.controller.BaseController;
import com.ruoyi.common.core.domain.AjaxResult;
import com.ruoyi.common.enums.BusinessType;
import com.ruoyi.common.utils.DateUtils;
import com.ruoyi.common.utils.StringUtils;
import com.ruoyi.common.utils.file.FileUtils;
import com.ruoyi.product.domain.*;
import com.ruoyi.product.service.*;
import com.ruoyi.replace.domain.MesMaterialInfo;
import com.ruoyi.replace.service.IMesMaterialInfoService;
import com.ruoyi.system.service.ISysConfigService;
import com.ruoyi.system.service.ISysRoleService;
import com.ruoyi.system.service.ISysUserService;
import com.ruoyi.web.controller.product.Encrypt.EncryptPdfController;
import com.ruoyi.web.controller.product.Encrypt.EncryptVideoController;
import com.ruoyi.web.controller.product.JsonObjectLuxLink1.LuxLinkData;
import com.ruoyi.ws.WsClientActuator;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.web.bind.annotation.*;

import javax.annotation.Resource;
import java.io.*;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.List;
import java.util.Map;

import java.io.IOException;

/**
 * luxLink签核Controller
 *
 * @author ruoyi
 * @date 2024-05-07
 */

@CrossOrigin(origins = "http://localhost:8090", maxAge = 3600)
@RestController
@RequestMapping("/LuxLink")
public class MesLuxLinkController  extends BaseController {
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
    @Autowired
    private IMesUploadTerminalPageService mesUploadTerminalPageService;
    @Autowired
    private IMesProcessService mesProcessService;
    @Autowired
    private IMesMaterialInfoService mesMaterialInfoService;
    @Autowired
    private IMesLineService mesLineService;
    @Autowired
    private IMesTerminalService mesTerminalService;
    @Autowired
    private IMesMacService mesMacService;
    @Resource
    private WsClientActuator wsClientActuator;
    EncryptPdfController encryptPdfController;
    EncryptVideoController encryptVideoController;
//    FfmpegCmdHls4M3u8EncTest ffmpegCmdHls4M3u8EncTest;
    private static final Logger log = LoggerFactory.getLogger(MesOaSopController.class);

    /**
     * luxLink对sop签核盖章之后直接推数据过来
     * 保存到sop表、upload表
     */
//    @PreAuthorize("@ss.hasPermi('product:oa:add')")
    @Log(title = "LuxLink签核", businessType = BusinessType.INSERT)
    @PostMapping("/EsopInfo")
    @CrossOrigin
    public AjaxResult addluxLinkEsopInfo(@RequestBody LuxLinkData luxLinkData) {
        try {
            log.info("LuxLink签核接口传的数据 : " + luxLinkData);
            String modelName = luxLinkData.getModelName();
            String materialName = luxLinkData.getMaterialName();
            List<String> lineNameList = luxLinkData.getLineName();
            String stageName = luxLinkData.getStageName();
            String projectName = luxLinkData.getProjectName();
            List<String> processNameList = luxLinkData.getProcessName();
            List<String> sopPageList = luxLinkData.getSopPage();
//            List<LuxLinkData.FileDetail> fileDetail = luxLinkData.getFileDetail();
            for (int i = 0; i < lineNameList.size(); i++) {
                String lineName = lineNameList.get(i);
                for (LuxLinkData.FileDetail item : luxLinkData.getFileDetail()) {
                    String fileName = item.getFileName();
//                byte[] fc = fileToByte();
                    byte[] fc = item.getFileContent();
                    item.setFileContent(fc);

                    String s = fileName.substring(fileName.length() - 3);
                    s = s.toLowerCase();

                    Map<String, Object> map = new AjaxResult();
                    //文件服务器真实路径
                    String filePath = "";
                    //浏览器访问路径
                    String browserUrl = "";
                    //PDF密码
                    String pdfPassWord = "";
                    if (s.equals("mp4") || s.equals("mts")) {
                        map = EncryptVideoController.encryptVideo(fileName, item.getFileContent());

                    } else if (s.equals("pdf")) {
                        map = EncryptPdfController.encryptPdf(fileName, item.getFileContent());
                    }

                    filePath = map.get("filePath").toString();
                    browserUrl = map.get("browserUrl").toString();
                    if (s.equals("pdf")) {
                        pdfPassWord = map.get("pdfPassWord").toString();
                    }

                    String preNewFileName = FileUtils.getName(filePath);
                    String ee = preNewFileName.substring(0, preNewFileName.indexOf("."));
                    System.out.println(ee);
                    SimpleDateFormat sdf = new SimpleDateFormat("yyyyMMddHHmmSS");
                    String dd = sdf.format(new Date());
                    String newFileName = ee + "_" + dd + preNewFileName.substring(preNewFileName.indexOf("."));
                    System.out.println(newFileName);


                    log.info("===========【文件上传服务器成功】============= ");
                    // 保存到 SOP 表
                    MesSop sop = new MesSop();
                    sop.setOriginalName(fileName);
                    sop.setSopName(newFileName);
                    sop.setFilePath(browserUrl);
                    sop.setVersion(item.getFileVersion());
                    sop.setCreateBy("luxLink");
                    sop.setStatus(null);
                    sop.setPassWord(pdfPassWord);

                    if (s.equals("pdf")) {
                        sop.setType("0");
                        if (luxLinkData.getSopInterval() != null) {
                            sop.setSopInterval(luxLinkData.getSopInterval());
                        } else {
                            sop.setSopInterval("60");
                        }
                    }
                    else if (s.equals("mp4") || s.equals("mts") || s.equals("avi") || s.equals("wmv")) {
                        sop.setType("1");
                        sop.setSopInterval(null);
                    } else {
                        return AjaxResult.error("请上传pdf、mp4、mts格式的文件");
                    }
                    mesSopService.insertMesSop(sop);
                    // 获取Id
                    Long sopId = mesSopService.selectSopIdByName(newFileName).getSopId();
                    log.info("===========【LuxLink签核】已保存到SOP 表 ============= 通过新文件名称获取 sopid : " + sopId);

                    //保存到mes_upload_terminal_page表
                    MesUploadTerminalPage uploadInfo = new MesUploadTerminalPage();
                    //赋值sopId
                    uploadInfo.setSopId(sopId);
                    //赋值type
                    if (s.equals("pdf")) {
                        uploadInfo.setType("0");
                        if (luxLinkData.getSopInterval() != null) {
                            uploadInfo.setSopInterval(luxLinkData.getSopInterval());
                        } else {
                            uploadInfo.setSopInterval("60");
                        }
                    }
                    //赋值sopInterval
                    if (StringUtils.isNotEmpty(luxLinkData.getSopInterval())) {
                        uploadInfo.setSopInterval(luxLinkData.getSopInterval());
                    } else {
                        uploadInfo.setSopInterval("60");
                    }
                    //赋值ModelId
                    MesModel mesModel = new MesModel();
                    Long modelId = null;
                    MesModel modelInfoByName = mesModelService.modelInfoByName(modelName);
                    if (StringUtils.isNull(modelInfoByName)) {
                        mesModel.setModelName(modelName);
                        mesModelService.insertMesModel(mesModel);
                    }
                    modelId = mesModelService.modelInfoByName(modelName).getModelId();
                    uploadInfo.setModelId(modelId);
                    //赋值MaterialId
                    MesMaterialInfo mesMaterialInfo = new MesMaterialInfo();
                    Long materialId = null;
                    if (StringUtils.isNotEmpty(materialName)) {
                        MesMaterialInfo materialInfoByName = mesMaterialInfoService.mesMaterialInfoByName(materialName);
                        if (StringUtils.isNull(materialInfoByName)) {
                            mesMaterialInfo.setMaterialName(materialName);
                            mesMaterialInfo.setModelId(modelId);
                            mesMaterialInfoService.insertMesMaterialInfo(mesMaterialInfo);
                        }
                        materialId = mesMaterialInfoService.mesMaterialInfoByName(materialName).getId();
                    }
                    uploadInfo.setMaterialId(materialId);
                    //赋值LineId
                    MesLine mesLine = new MesLine();
                    Long lineId = null;
                    MesLine lineInfoByName = mesLineService.lineInfoByName(lineName);
                    if (StringUtils.isNull(lineInfoByName)) {
                        mesLine.setLineName(lineName);
                        mesLineService.insertMesLine(mesLine);
                    }
                    lineId = mesLineService.lineInfoByName(lineName).getLineId();
                    uploadInfo.setLineId(lineId);
                    //赋值stageId
                    MesStage mesStage = new MesStage();
                    Long stageId = null;
                    MesStage stageInfoByName = mesStageService.stageInfoByName(stageName);
                    if (StringUtils.isNull(stageInfoByName)) {
                        mesStage.setStageName(stageName);
                        mesStageService.insertMesStage(mesStage);
                    }
                    stageId = mesStageService.stageInfoByName(stageName).getStageId();
                    uploadInfo.setStageId(stageId);
                    //赋值processId,sopPage
                    for (int j = 0; j < processNameList.size(); j++) {
                        MesProcess mesProcess = new MesProcess();
                        Long processId = null;
                        MesProcess processInfoByName = mesProcessService.processInfoByName(processNameList.get(j));
                        if (StringUtils.isNull(processInfoByName)) {
                            mesProcess.setProcessName(processNameList.get(j));
                            mesProcess.setStageId(stageId);
                            mesProcessService.insertMesProcess(mesProcess);
                        }
                        processId = mesProcessService.processInfoByName(processNameList.get(j)).getProcessId();
                        uploadInfo.setProcessId(processId);
                        uploadInfo.setSopPage(sopPageList.get(j));
                        //luxLink数据插入mes_upload_terminal_page表
                        uploadInfo.setVersionStatus("0");
                        uploadInfo.setStatus("2");
                        uploadInfo.setProject(projectName);
                        uploadInfo.setCreateTime(DateUtils.parseDate(DateUtils.getTime()));
                        uploadInfo.setCreateBy("luxLink");
                        mesUploadTerminalPageService.insertMesUploadTerminalPage(uploadInfo);
                    }
                    log.info("===========【LuxLink签核数据】已保存到upload表============= sopId = " + sopId);
                }
            }
        }
        catch(Exception e){
            return AjaxResult.error();
        }
        return AjaxResult.success();
    }


    public byte[] fileToByte() {
        File file = new File("C:\\Users\\LiuYan\\Desktop\\SOP文件\\mp4\\1678257439219.mp4"); // 替换为你的文件路径
        byte[] fileContent = null;
        try (FileInputStream fis = new FileInputStream(file);
             ByteArrayOutputStream bos = new ByteArrayOutputStream()) {

            byte[] buffer = new byte[1024];
            int bytesRead;
            while ((bytesRead = fis.read(buffer)) != -1) {
                bos.write(buffer, 0, bytesRead);
            }

            fileContent = bos.toByteArray(); // 这里得到了文件的字节数组

        } catch (IOException e) {
            e.printStackTrace();
        }
        return fileContent;
        // 现在你可以使用 fileContent 这个字节数组了
    }
}
