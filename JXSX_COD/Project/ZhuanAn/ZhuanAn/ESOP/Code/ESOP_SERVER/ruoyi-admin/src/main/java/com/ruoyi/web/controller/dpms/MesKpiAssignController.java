package com.ruoyi.web.controller.dpms;

import com.ruoyi.common.annotation.Log;
import com.ruoyi.common.core.controller.BaseController;
import com.ruoyi.common.core.domain.AjaxResult;
import com.ruoyi.common.core.page.TableDataInfo;
import com.ruoyi.common.enums.BusinessType;
import com.ruoyi.common.utils.SecurityUtils;
import com.ruoyi.dpms.domain.MesKpiAssign;
import com.ruoyi.dpms.service.IMesKpiAssignService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.web.bind.annotation.*;

import java.util.List;

/**
 * KPI指派Controller
 * 
 * @author ruoyi
 * @date 2022-11-10
 */
@RestController
@RequestMapping("/dpms/kpiAssign")
public class MesKpiAssignController extends BaseController
{
    @Autowired
    private IMesKpiAssignService mesKpiAssignService;

    /**
     * 查询KPI指派列表
     */
    @PreAuthorize("@ss.hasPermi('dpms:kpiAssign:list')")
    @GetMapping("/list")
    public TableDataInfo list(MesKpiAssign mesKpiAssign)
    {
        startPage();
        mesKpiAssign.setDeptId(SecurityUtils.getDeptId());
        List<MesKpiAssign> list = mesKpiAssignService.selectMesKpiAssignList(mesKpiAssign);
        return getDataTable(list);
    }

    /**
     * 获取KPI指派详细信息
     */
    @PreAuthorize("@ss.hasPermi('dpms:kpiAssign:query')")
    @GetMapping(value = "/{id}")
    public AjaxResult getInfo(@PathVariable("id") Long id)
    {
        return AjaxResult.success(mesKpiAssignService.selectMesKpiAssignById(id));
    }

    /**
     * 新增KPI指派
     */
    @PreAuthorize("@ss.hasPermi('dpms:kpiAssign:add')")
    @Log(title = "KPI指派", businessType = BusinessType.INSERT)
    @PostMapping(value = "/add")
    public AjaxResult add(@RequestBody String mesKpiAssign)
    {
        return mesKpiAssignService.insertMesKpiAssign(mesKpiAssign);
    }

    /**
     * 修改KPI指派
     */
    @PreAuthorize("@ss.hasPermi('dpms:kpiAssign:edit')")
    @Log(title = "KPI指派", businessType = BusinessType.UPDATE)
    @PutMapping(value = "/edit")
    public AjaxResult edit(@RequestBody String mesKpiAssign)
    {
        return mesKpiAssignService.updateMesKpiAssign(mesKpiAssign);
    }

//    /**
//     * 删除KPI指派
//     */
//    @PreAuthorize("@ss.hasPermi('dpms:kpiAssign:remove')")
//    @Log(title = "KPI指派", businessType = BusinessType.DELETE)
//	@DeleteMapping("/remove/{ids}")
//    public AjaxResult remove(@PathVariable Long[] ids)
//    {
//        return toAjax(mesKpiAssignService.deleteMesKpiAssignByIds(ids));
//    }
}
