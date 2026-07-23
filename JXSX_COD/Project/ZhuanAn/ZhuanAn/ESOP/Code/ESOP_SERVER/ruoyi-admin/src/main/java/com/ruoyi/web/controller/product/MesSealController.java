package com.ruoyi.web.controller.product;

import java.io.IOException;
import java.util.List;
import javax.servlet.http.HttpServletResponse;

import com.ruoyi.common.utils.file.FileUtils;
import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;
import com.ruoyi.common.annotation.Log;
import com.ruoyi.common.core.controller.BaseController;
import com.ruoyi.common.core.domain.AjaxResult;
import com.ruoyi.common.enums.BusinessType;
import com.ruoyi.product.domain.MesSeal;
import com.ruoyi.product.service.IMesSealService;
import com.ruoyi.common.utils.poi.ExcelUtil;
import com.ruoyi.common.core.page.TableDataInfo;
import org.springframework.web.multipart.MultipartFile;

/**
 * 印章Controller
 *
 * @author ruoyi
 * @date 2024-04-18
 */
@RestController
@RequestMapping("/product/seal")
public class MesSealController extends BaseController
{
    @Autowired
    private IMesSealService mesSealService;

    /**
     * 查询印章列表
     */
    @PreAuthorize("@ss.hasPermi('product:seal:list')")
    @GetMapping("/list")
    public TableDataInfo list(MesSeal mesSeal)
    {
        startPage();
        List<MesSeal> list = mesSealService.selectMesSealList(mesSeal);
        return getDataTable(list);
    }

    /**
     * 导出印章列表
     */
    @PreAuthorize("@ss.hasPermi('product:seal:export')")
    @Log(title = "印章", businessType = BusinessType.EXPORT)
    @PostMapping("/export")
    public void export(HttpServletResponse response, MesSeal mesSeal)
    {
        List<MesSeal> list = mesSealService.selectMesSealList(mesSeal);
        ExcelUtil<MesSeal> util = new ExcelUtil<MesSeal>(MesSeal.class);
        util.exportExcel(response, list, "印章数据");
    }

    /**
     * 获取印章详细信息
     */
    @PreAuthorize("@ss.hasPermi('product:seal:query')")
    @GetMapping(value = "/{sealId}")
    public AjaxResult getInfo(@PathVariable("sealId") Long sealId)
    {
        return AjaxResult.success(mesSealService.selectMesSealBySealId(sealId));
    }

    /**
     * 新增印章(带图:10MB以内、透明底、png)
     */
    @PreAuthorize("@ss.hasPermi('product:seal:add')")
    @PostMapping("/addSeal")
    @ResponseBody
    public AjaxResult add(@RequestBody MesSeal mesSeal, @RequestParam("sealImageFile") MultipartFile multipartFile) throws IOException {
        return mesSealService.addMesSeal(mesSeal,multipartFile);
    }

    /**
     * 修改印章
     */
    @PreAuthorize("@ss.hasPermi('product:seal:edit')")
    @Log(title = "印章", businessType = BusinessType.UPDATE)
    @PutMapping
    public AjaxResult edit(@RequestBody MesSeal mesSeal)
    {
        return toAjax(mesSealService.updateMesSeal(mesSeal));
    }

    /**
     * 删除印章
     */
    @PreAuthorize("@ss.hasPermi('product:seal:remove')")
    @Log(title = "印章", businessType = BusinessType.DELETE)
    @DeleteMapping("/{sealIds}")
    public AjaxResult remove(@PathVariable Long[] sealIds)
    {
        return toAjax(mesSealService.deleteMesSealBySealIds(sealIds));
    }
}
