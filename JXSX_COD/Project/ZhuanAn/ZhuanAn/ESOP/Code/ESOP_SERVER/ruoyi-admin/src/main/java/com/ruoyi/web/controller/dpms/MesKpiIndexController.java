package com.ruoyi.web.controller.dpms;

import com.ruoyi.common.annotation.Log;
import com.ruoyi.common.core.controller.BaseController;
import com.ruoyi.common.core.domain.AjaxResult;
import com.ruoyi.common.core.page.TableDataInfo;
import com.ruoyi.common.enums.BusinessType;
import com.ruoyi.dpms.domain.MesKpiIndex;
import com.ruoyi.dpms.service.IMesKpiIndexService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.web.bind.annotation.*;

import java.util.List;

/**
 * KPI指标Controller
 * 
 * @author ruoyi
 * @date 2022-11-10
 */
@RestController
@RequestMapping("/dpms/kpiIndex")
public class MesKpiIndexController extends BaseController
{
    @Autowired
    private IMesKpiIndexService mesKpiIndexService;

    /**
     * 查询KPI指标列表
     */
//    @PreAuthorize("@ss.hasPermi('dpms:kpiIndex:list')")
    @GetMapping("/list")
    public TableDataInfo list(MesKpiIndex mesKpiIndex)
    {
        startPage();
        List<MesKpiIndex> list = mesKpiIndexService.selectMesKpiIndexList(mesKpiIndex);
        return getDataTable(list);
    }

//    /**
//     * 导出KPI指标列表
//     */
//    @PreAuthorize("@ss.hasPermi('dpm:index:export')")
//    @Log(title = "KPI指标", businessType = BusinessType.EXPORT)
//    @PostMapping("/export")
//    public void export(HttpServletResponse response, MesKpiIndex mesKpiIndex)
//    {
//        List<MesKpiIndex> list = mesKpiIndexService.selectMesKpiIndexList(mesKpiIndex);
//        ExcelUtil<MesKpiIndex> util = new ExcelUtil<MesKpiIndex>(MesKpiIndex.class);
//        util.exportExcel(response, list, "KPI指标数据");
//    }

    /**
     * 获取KPI指标详细信息
     */
    @PreAuthorize("@ss.hasPermi('dpms:kpiIndex:query')")
    @GetMapping(value = "/{id}")
    public AjaxResult getInfo(@PathVariable("id") Long id)
    {
        return AjaxResult.success(mesKpiIndexService.selectMesKpiIndexById(id));
    }

    /**
     * 新增KPI指标
     */
    @PreAuthorize("@ss.hasPermi('dpms:kpiIndex:add')")
    @Log(title = "KPI指标", businessType = BusinessType.INSERT)
    @PostMapping(value = "/add")
    public AjaxResult add(@RequestBody MesKpiIndex mesKpiIndex)
    {
        return toAjax(mesKpiIndexService.insertMesKpiIndex(mesKpiIndex));
    }

    /**
     * 修改KPI指标
     */
    @PreAuthorize("@ss.hasPermi('dpms:kpiIndex:edit')")
    @Log(title = "KPI指标", businessType = BusinessType.UPDATE)
    @PutMapping(value = "/edit")
    public AjaxResult edit(@RequestBody MesKpiIndex mesKpiIndex)
    {
        return toAjax(mesKpiIndexService.updateMesKpiIndex(mesKpiIndex));
    }

    /**
     * 删除KPI指标
     */
    @PreAuthorize("@ss.hasPermi('dpms:kpiIndex:remove')")
    @Log(title = "KPI指标", businessType = BusinessType.DELETE)
	@DeleteMapping("/remove/{ids}")
    public AjaxResult remove(@PathVariable Long[] ids)
    {
        return toAjax(mesKpiIndexService.deleteMesKpiIndexByIds(ids));
    }
}
