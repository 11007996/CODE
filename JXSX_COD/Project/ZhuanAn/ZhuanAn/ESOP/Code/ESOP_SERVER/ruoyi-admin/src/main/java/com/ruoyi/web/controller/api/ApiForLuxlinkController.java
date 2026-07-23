package com.ruoyi.web.controller.api;

import com.alibaba.fastjson2.JSONObject;
import com.ruoyi.common.core.domain.AjaxResult;
import com.ruoyi.common.utils.DateUtils;
import com.ruoyi.product.domain.*;
import com.ruoyi.product.service.*;
import io.swagger.annotations.Api;
import io.swagger.annotations.ApiOperation;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import java.util.List;

@Api("luxlink 接口")
@RestController
@RequestMapping("/luxlink")
public class ApiForLuxlinkController {

    @Autowired
    private IMesOaSopService mesOaSopService;

    @Autowired
    private IMesTerminalSopService mesTerminalSopService;

    @Autowired
    private IMesSopGroupService mesSopGroupService;

    @Autowired
    private IMesSopService mesSopService;

    @Autowired
    private IMesUploadTerminalPageService mesUploadTerminalPageService;

    private static final Logger log = LoggerFactory.getLogger(ApiForLuxlinkController.class);

    @ApiOperation("luxlink回写接口")
    @PostMapping("/UpdateEsop")
    public AjaxResult updateEsop(@RequestBody String jsonData) {
        try {
            log.info("luxlink回写接口数据 : " + jsonData);
            JSONObject entity=  JSONObject.parseObject(jsonData);
            String rid = entity.getString("requestid");
            String status = entity.getString("status");

            MesOaSop mesOaSop =  mesOaSopService.selectMesOaSopByRequestId(rid);

            MesOaSop mesOaSop1 = new MesOaSop();
            Long materialId = mesOaSop.getMaterialId();
            String type = mesOaSop.getType();
            mesOaSop1.setMaterialId(materialId);
            mesOaSop1.setType(type);
            List<Long> sopIdList = mesOaSopService.selectSopIdsByPartNo(mesOaSop1);

            if( mesOaSop != null){

                if(status.equals("N")){
                    mesOaSop.setStatus("3");
                }else {
                    mesOaSop.setStatus("2");
                    for (int i = 0; i < sopIdList.size(); i++) {
//                        Long sopId = sopIdList.get(i);
                        MesSop mesSop = mesSopService.selectMesSopBySopId(sopIdList.get(i));
//                        mesSop.setUpdateTime(DateUtils.getNowDate());
                        mesSop.setStatus("1");
                        mesSop.setSopId(sopIdList.get(i));
                        mesSopService.updateMesSop(mesSop);
                    }
                }
                mesOaSop.setUpdateTime(DateUtils.getNowDate());
                //更新mes_oa_sop表的数据（status、upDateTime）
                mesOaSopService.updateMesOaSopByRequestId(mesOaSop);

                //更新mes_upload_terminal_page表数据（status）
                MesUploadTerminalPage uploadInfo = new MesUploadTerminalPage();
                uploadInfo.setStatus(mesOaSop.getStatus());
                uploadInfo.setSopId(mesOaSop.getSopId());
//                uploadInfo.setVersionStatus("1");//签核成功之后的sop状态都改为停用状态（versionStatus=1）
//                mesUploadTerminalPageService.updateMesUploadTerminalPage(uploadInfo);


                if(mesOaSop.getStatus() == "2"){
                    try{
                        List<MesUploadTerminalPage> mesUploadTerminalPageList = mesUploadTerminalPageService.selectMesUploadTerminalPageById(mesOaSop.getOaId());
                        for (int i = 0; i < mesUploadTerminalPageList.size(); i++) {
                            MesTerminalSop terminalSop = new MesTerminalSop();
                            MesUploadTerminalPage multp = mesUploadTerminalPageList.get(i);
                            terminalSop.setModelId(multp.getModelId());
                            terminalSop.setModelName(multp.getModelName());
                            terminalSop.setMaterialId(multp.getMaterialId());
                            terminalSop.setLineId(multp.getLineId());
                            terminalSop.setStageId(multp.getStageId());
                            terminalSop.setProcessId(multp.getProcessId());
                            terminalSop.setTerminalId(multp.getTerminalId());

                            MesSopGroup sopGroup = new MesSopGroup();
//                            sopGroup.setSopGroupId(terminalSop.getId().toString());
                            sopGroup.setSopId(mesOaSop.getSopId());
                            sopGroup.setSopPage(multp.getSopPage());
                            //判断传来的type是文档还是视频  mes_sop_group表的文档：类型全设为0  视频：类型全设为1
                            //目前有4个类型 —— 文档:0:pdf 视频: 1:mp4、mts、avi
                            if(!multp.getType().equals("0")){
                                sopGroup.setType("1");
                            }
                            else {
                                sopGroup.setType("0");
                            }
                            //根据4个id查询到工站的信息list
                            List<MesTerminalSop> list = mesTerminalSopService.selectMesTerminalSopList1(terminalSop);
                            //如果，查询结果是当前工站在mes_terminal_sop表一条sop数据也没有，直接把数据插入到mes_terminal_sop表
                            if(list.size() == 0){
                                //在表mes_terminal_sop中插入数据
                                mesTerminalSopService.insertMesTerminalSop(terminalSop);
                                List<MesTerminalSop> list1 = mesTerminalSopService.selectMesTerminalSopList(terminalSop);
                                String id = list1.get(0).getId().toString();
                                sopGroup.setSopGroupId(id);
                                //在表mes_sop_group中插入数据
                                mesSopGroupService.insertMesSopGroup(sopGroup);
                            }else {
                                //如果，查询结果是当前工站在mes_terminal_sop表有数据
                                // 先判断mes_sop_group表是否有同样type的数据，有就先按sopGroupId和type先删后增数据，没有就直接插入数据
                                for (int k = 0; k < list.size(); k++) {
                                    terminalSop.setId(list.get(k).getId());
                                    sopGroup.setSopGroupId(list.get(k).getId().toString());
//                                mesTerminalSopService.updateMesTerminalSop(terminalSop);
                                    //在mes_sop_group表修改数据 先判断文件type，再修改(先删后增)
                                    Long typeNum = mesSopGroupService.selectMesSopGroupTypeNum(sopGroup);
                                    if(typeNum > 0){
                                        mesSopGroupService.deleteMesSopGroupBySopGroupId(terminalSop.getId(),sopGroup.getType());
                                        mesSopGroupService.insertMesSopGroup(sopGroup);
                                    }else {
                                        mesSopGroupService.insertMesSopGroup(sopGroup);
                                    }
                                }
                            }
                            //更新mes_upload_terminal_page表数据（status）
                            MesUploadTerminalPage versionStatus = new MesUploadTerminalPage();
                            versionStatus.setSopId(mesOaSop.getSopId());
                            versionStatus.setVersionStatus("0");//签核成功之后的sop版本状态都改为停用状态（versionStatus=1）
                            versionStatus.setStatus("2");//签核成功之后的sop签核状态都改为签核成功状态（status=2）
                            mesUploadTerminalPageService.updateMesUploadTerminalPage(versionStatus);
                            //版本状态versionStatus加到upload表了，不需要修改sop表的status了
//                            MesSop sop = new MesSop();
//                            sop.setSopId(mesOaSop.getSopId());
//                            sop.setStatus("0");
//                            mesSopService.updateMesSop(sop);
                        }
                    }
                    catch (Exception e){
                        log.info(e.getMessage());
                        return new AjaxResult(500, "后台更新数据失败 ");
                    }
                }
                else {
                    return new AjaxResult(200, "操作成功");
                }

            }else {
                return new AjaxResult(500, "没有查询到此表单号");
            }
            return new AjaxResult(200, "操作成功");
        }catch (Exception e){
            log.info(e.getMessage());
            return new AjaxResult(500, e.getMessage());
        }
    }

}
