package com.ruoyi.web.controller.product;

import com.ruoyi.common.core.domain.AjaxResult;
import com.ruoyi.product.domain.MesMac;
import com.ruoyi.product.service.IMesMacService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.multipart.MultipartFile;

@RestController
@RequestMapping("/other/excel")
public class ImportMacController {

    @Autowired
    IMesMacService iMacService;

    /**
     * mac地址批量导入
     * @return
     */
//    @PostMapping("/importMacExcel")
//    @ResponseBody
//    public AjaxResult output(@RequestParam("file") MultipartFile file, MesMac mac){
//        try {
//            return iMacService.importMacExcel(file,mac);
//        } catch (Exception e) {
//            e.printStackTrace();
//            return AjaxResult.error(500, e.getMessage());
//        }
//    }

//    /**
//     * 料号信息模板下载
//     * @return
//     */
//    @PostMapping("/importTemplate")
//    public AjaxResult importTemplate(){
//        ExcelUtil<Partno> util = new ExcelUtil<Partno>(Partno.class);
//        return util.importTemplateExcel("料号模板");
//    }
}
