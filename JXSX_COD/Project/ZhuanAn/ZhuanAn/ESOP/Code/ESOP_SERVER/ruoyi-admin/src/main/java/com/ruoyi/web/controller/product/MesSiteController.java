package com.ruoyi.web.controller.product;

import com.ruoyi.common.annotation.Log;
import com.ruoyi.common.constant.UserConstants;
import com.ruoyi.common.core.controller.BaseController;
import com.ruoyi.common.core.domain.AjaxResult;
import com.ruoyi.common.core.page.TableDataInfo;
import com.ruoyi.common.enums.BusinessType;
import com.ruoyi.common.utils.DateUtils;
import com.ruoyi.common.utils.poi.ExcelUtil;
import com.ruoyi.product.domain.MesSite;
import com.ruoyi.product.service.IMesSiteService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.web.bind.annotation.*;

import javax.servlet.http.HttpServletResponse;
import java.util.List;

/**
 * 厂区管理Controller
 *
 * @author ruoyi
 * @date 2022-09-20
 */
@RestController
@RequestMapping("/product/site")
public class MesSiteController extends BaseController
{
    @Autowired
    private IMesSiteService mesSiteService;

    /**
     * 查询厂区管理列表
     */
    @PreAuthorize("@ss.hasPermi('product:site:list')")
    @GetMapping("/list")
    public TableDataInfo list(MesSite mesSite)
    {
        startPage();
        List<MesSite> list = mesSiteService.selectMesSiteList(mesSite);
        return getDataTable(list);
    }

    @PreAuthorize("@ss.hasPermi('product:site:list')")
    @GetMapping("/getMesSiteList")
    public TableDataInfo getMesSiteList(MesSite mesSite)
    {
        mesSite.setStatus("0");
        List<MesSite> list = mesSiteService.selectMesSiteList(mesSite);
        return getDataTable(list);
    }



    /**
     * 导出厂区管理列表
     */
    @PreAuthorize("@ss.hasPermi('product:site:export')")
    @Log(title = "厂区管理", businessType = BusinessType.EXPORT)
    @PostMapping("/export")
    public void export(HttpServletResponse response, MesSite mesSite)
    {
        List<MesSite> list = mesSiteService.selectMesSiteList(mesSite);
        ExcelUtil<MesSite> util = new ExcelUtil<MesSite>(MesSite.class);
        util.exportExcel(response, list, "厂区管理数据");
    }

    /**
     * 获取厂区管理详细信息
     */
    @PreAuthorize("@ss.hasPermi('product:site:query')")
    @GetMapping(value = "/{siteId}")
    public AjaxResult getInfo(@PathVariable("siteId") Long siteId)
    {
        return AjaxResult.success(mesSiteService.selectMesSiteBySiteId(siteId));
    }



    /**
     * 新增厂区管理
     */
    @PreAuthorize("@ss.hasPermi('product:site:add')")
    @Log(title = "厂区管理", businessType = BusinessType.INSERT)
    @PostMapping
    public AjaxResult add(@RequestBody MesSite mesSite)
    {
        if (UserConstants.NOT_UNIQUE.equals(mesSiteService.checkSiteNameUnique(mesSite)))
        {
            return AjaxResult.error("新增厂区'" + mesSite.getSiteName() + "'失败，厂区名称已存在");
        }
        mesSite.setCreateBy(getUsername());

        return toAjax(mesSiteService.insertMesSite(mesSite));
    }

    /**
     * 修改厂区管理
     */
    @PreAuthorize("@ss.hasPermi('product:site:edit')")
    @Log(title = "厂区管理", businessType = BusinessType.UPDATE)
    @PutMapping
    public AjaxResult edit(@RequestBody MesSite mesSite)
    {
//        mesSiteService.checkSiteAllowed(mesSite);
//        mesSiteService.checkSiteDataScope(mesSite.getSiteId());
        if(UserConstants.NOT_UNIQUE.equals(mesSiteService.checkSiteNameUnique(mesSite)))
        {
            return AjaxResult.error("修改厂区'" + mesSite.getSiteName() + "'失败，厂区名称已存在");
        }
        mesSite.setUpdateBy(getUsername());
        return toAjax(mesSiteService.updateMesSite(mesSite));
    }

    /**
     * 修改厂区状态
     */
    @PreAuthorize("@ss.hasPermi('product:site:edit')")
    @Log(title = "厂区管理", businessType = BusinessType.UPDATE)
    @PutMapping("/changeStatus")
    public AjaxResult changeStatus(@RequestBody MesSite mesSite)
    {
//        mesSiteService.checkSiteAllowed(mesSite);
//        mesSiteService.checkSiteDataScope(mesSite.getSiteId());
        mesSite.setUpdateBy(getUsername());
        mesSite.setUpdateTime(DateUtils.getNowDate());
        return toAjax(mesSiteService.updateSiteStatus(mesSite));
    }

    /**
     * 删除厂区管理
     */
    @PreAuthorize("@ss.hasPermi('product:site:remove')")
    @Log(title = "厂区管理", businessType = BusinessType.DELETE)
	@DeleteMapping("/{siteIds}")
    public AjaxResult remove(@PathVariable Long[] siteIds)
    {
        return toAjax(mesSiteService.deleteMesSiteBySiteIds(siteIds));
    }
}
