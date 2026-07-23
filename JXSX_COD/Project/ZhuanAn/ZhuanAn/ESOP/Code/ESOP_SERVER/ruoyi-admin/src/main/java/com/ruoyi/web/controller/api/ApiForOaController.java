package com.ruoyi.web.controller.api;

import com.alibaba.fastjson2.JSONObject;
import com.ruoyi.common.annotation.Log;
import com.ruoyi.common.core.domain.AjaxResult;
import com.ruoyi.common.enums.BusinessType;
import com.ruoyi.common.utils.DateUtils;
import com.ruoyi.product.domain.MesOaSop;
import com.ruoyi.product.domain.MesSop;
import com.ruoyi.product.domain.MesTerminalSop;
import com.ruoyi.product.service.IMesOaSopService;
import com.ruoyi.product.service.IMesSopService;
import com.ruoyi.product.service.IMesTerminalSopService;
import io.swagger.annotations.Api;
import io.swagger.annotations.ApiOperation;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import java.util.List;
import java.util.Map;

@Api("OA回写")
@RestController
@RequestMapping("/oai")
public class ApiForOaController {

    @Autowired
    private IMesOaSopService mesOaSopService;

    @Autowired
    private IMesTerminalSopService mesTerminalSopService;

    @Autowired
    private IMesSopService mesSopService;

    @ApiOperation("OA回写详细")
    @Log(title = "oa回写esop", businessType = BusinessType.UPDATE)
    @PostMapping("/UpdEsop")
    public AjaxResult UpdEsop(@RequestBody String  jsonData) {
        try {
            Map<String,Object> map = JSONObject.parseObject(jsonData);
            String rid = map.get("requestid").toString();
            String status = map.get("status").toString();
            MesOaSop mesOaSop =  mesOaSopService.selectMesOaSopByRequestId(rid);


            if( mesOaSop != null){

                if(status=="N"){
                    mesOaSop.setStatus("3");
                }else {
                    mesOaSop.setStatus("2");
                }
                mesOaSop.setUpdateTime(DateUtils.getNowDate());
                //mesOaSop.setUpdateBy("oa");
                Long j = mesOaSopService.updateMesOaSopByRequestId(mesOaSop);
                if(j == 0){
                    return new AjaxResult(500, "后台更新数据失败 ");
                }else {
                    try{
                        MesTerminalSop terminalSop = new MesTerminalSop();
                        terminalSop.setModelId(mesOaSop.getModelId());
                        terminalSop.setLineId(mesOaSop.getLineId());
                        terminalSop.setStageId(mesOaSop.getStageId());
                        terminalSop.setProcessId(mesOaSop.getProcessId());

                        List<MesTerminalSop> list = mesTerminalSopService.selectMesTerminalSopList(terminalSop);
                        if(list.size() == 0){
                            mesTerminalSopService.insertMesTerminalSop(terminalSop);
//                            mesTerminalSopService.insertHistory(terminalSop);
                        }else {
                            for (int i = 0; i < list.size(); i++) {
                                terminalSop.setId(list.get(i).getId());
                                mesTerminalSopService.updateMesTerminalSop(terminalSop);
//                                mesTerminalSopService.insertHistory(terminalSop);
                            }
                        }

                        MesSop sop = new MesSop();
                        sop.setSopId(mesOaSop.getSopId());
                        sop.setStatus("0");
                        mesSopService.updateMesSop(sop);

                    }
                    catch (Exception e){
                        return new AjaxResult(500, "后台更新数据失败 ");
                    }
                }

            }else {
                return new AjaxResult(500, "没有查询到此表单号");
            }
            return new AjaxResult(200, "操作成功");
        }catch (Exception e){
            return new AjaxResult(500, e.getMessage());
        }
    }
}
