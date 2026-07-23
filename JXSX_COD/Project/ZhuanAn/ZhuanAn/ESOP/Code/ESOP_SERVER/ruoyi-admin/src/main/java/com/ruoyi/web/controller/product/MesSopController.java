package com.ruoyi.web.controller.product;

import com.ruoyi.common.annotation.Log;
import com.ruoyi.common.core.controller.BaseController;
import com.ruoyi.common.core.domain.AjaxResult;
import com.ruoyi.common.core.page.TableDataInfo;
import com.ruoyi.common.enums.BusinessType;
import com.ruoyi.common.utils.DateUtils;
import com.ruoyi.common.utils.poi.ExcelUtil;
import com.ruoyi.product.domain.MesSop;
import com.ruoyi.product.service.IMesSopService;
import com.ruoyi.product.service.IMesUploadTerminalPageService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.web.bind.annotation.*;

import javax.servlet.http.HttpServletResponse;
import java.io.*;
import java.util.List;

/**
 * sopController
 *
 * @author ruoyi
 * @date 2022-09-23
 */
@RestController
@RequestMapping("/product/sop")
public class MesSopController extends BaseController
{
    @Autowired
    private IMesSopService mesSopService;

    @Autowired
    private IMesUploadTerminalPageService mesUploadTerminalPageService;

    /**
     * 查询sop列表
     */
    @PreAuthorize("@ss.hasPermi('product:sop:list')")
    @GetMapping("/list")
    public TableDataInfo list(MesSop mesSop)
    {
        startPage();
        List<MesSop> list = mesSopService.selectMesSopList(mesSop);
        return getDataTable(list);
    }

    /**
     * 导出sop列表
     */
    @PreAuthorize("@ss.hasPermi('product:sop:export')")
    @Log(title = "sop", businessType = BusinessType.EXPORT)
    @PostMapping("/export")
    public void export(HttpServletResponse response, MesSop mesSop)
    {
        List<MesSop> list = mesSopService.selectMesSopList(mesSop);
        ExcelUtil<MesSop> util = new ExcelUtil<MesSop>(MesSop.class);
        util.exportExcel(response, list, "sop数据");
    }

    /**
     * 获取sop详细信息
     */
    @PreAuthorize("@ss.hasPermi('product:sop:query')")
    @GetMapping(value = "/{sopId}")
    public AjaxResult getInfo(@PathVariable("sopId") Long sopId)
    {
        return AjaxResult.success(mesSopService.selectMesSopBySopId(sopId));
    }

    /**
     * 新增sop
     */
    @PreAuthorize("@ss.hasPermi('product:sop:add')")
    @Log(title = "sop", businessType = BusinessType.INSERT)
    @PostMapping("/add")
    public AjaxResult add(@RequestBody MesSop mesSop)
    {
        return toAjax(mesSopService.insertMesSop(mesSop));
    }

    /**
     * 修改sop
     */
    @PreAuthorize("@ss.hasPermi('product:sop:edit')")
    @Log(title = "sop", businessType = BusinessType.UPDATE)
    @PutMapping
    public AjaxResult edit(@RequestBody MesSop mesSop)
    {
        return toAjax(mesSopService.updateMesSop(mesSop));
    }

//    /**
//     * 修改sop版本状态
//     */
//    @PutMapping("/changeSopStatus")
//    public AjaxResult changeSopStatus(@RequestBody MesSop mesSop)
//    {
//        //如果想要启用某个sop版本，先通过mac地址查询到这个mac地址的其他sop版本，把其他版本都停用
//        if (mesSop.getStatus().equals("0")){
//            Long terminalId = mesSop.getTerminalId();
//            //注意区分文档和视频
//            List<MesUploadTerminalPage> infoList = mesUploadTerminalPageService.selectHistoricalVersionByTerminalId(terminalId);
//            for (int i = 0; i < infoList.size(); i++) {
//                Long sopId = infoList.get(i).getSopId();
//                MesSop sopStatus = mesSopService.selectMesSopBySopId(sopId);
//                sopStatus.setStatus("1");
//                String a = sopStatus.getStatus();
//                String b = "";
//                mesSopService.updateMesSop(sopStatus);
//            }
//            mesSopService.updateMesSop(mesSop);
//        }
//        else {
//            mesSopService.updateMesSop(mesSop);
//        }
//        return AjaxResult.success();
//    }



    /**
     * 删除sop
     */
    @PreAuthorize("@ss.hasPermi('product:sop:remove')")
    @Log(title = "sop", businessType = BusinessType.DELETE)
    @DeleteMapping("/{sopIds}")
    public AjaxResult remove(@PathVariable Long[] sopIds)
    {
        return toAjax(mesSopService.deleteMesSopBySopIds(sopIds));
    }




    /**
     * 返回文件流
     *
     * @param response
     */
    @PostMapping("/getFile/{id}")
    public void getFileInputStream( HttpServletResponse response, @PathVariable Long id) {

        MesSop mesSop = mesSopService.selectMesSopBySopId(id);
        //读取指定路径下面的文件
        try {
            InputStream in = new FileInputStream(mesSop.getFilePath());
            if(in.equals("") || in == null){

            }
            OutputStream outputStream = new BufferedOutputStream(response.getOutputStream());
            //创建存放文件内容的数组
            byte[] buff = new byte[1024];
            //所读取的内容使用n来接收
            int n;
            //当没有读取完时,继续读取,循环
            while ((n = in.read(buff)) != -1) {
                //将字节数组的数据全部写入到输出流中
                outputStream.write(buff, 0, n);
            }
            //强制将缓存区的数据进行输出
            outputStream.flush();
            //关流
            outputStream.close();
            in.close();
        }
        catch (IOException e) {

        }

    }
}
