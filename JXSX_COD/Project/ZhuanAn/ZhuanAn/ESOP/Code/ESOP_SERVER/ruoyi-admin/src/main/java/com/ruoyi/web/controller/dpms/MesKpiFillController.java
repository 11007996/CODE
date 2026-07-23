package com.ruoyi.web.controller.dpms;

import com.ruoyi.common.annotation.Log;
import com.ruoyi.common.core.controller.BaseController;
import com.ruoyi.common.core.domain.AjaxResult;
import com.ruoyi.common.core.page.TableDataInfo;
import com.ruoyi.common.enums.BusinessType;
import com.ruoyi.common.utils.SecurityUtils;
import com.ruoyi.dpms.domain.MesKpiFill;
import com.ruoyi.dpms.service.IMesKpiFillService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.web.bind.annotation.*;

import java.util.List;

/**
 * KPI填报Controller
 *
 * @author ruoyi
 * @date 2022-11-16
 */
@RestController
@RequestMapping("/dpms/kpiFill")
public class MesKpiFillController extends BaseController
{
    @Autowired
    private IMesKpiFillService mesKpiFillService;

    /**
     * 查询KPI填报列表
     */
    @PreAuthorize("@ss.hasPermi('dpms:kpiFill:list')")
    @GetMapping("/list")
    public TableDataInfo list(MesKpiFill mesKpiFill)
    {
        startPage();
        mesKpiFill.setUserAccount(SecurityUtils.getUsername());
        mesKpiFill.setDeptId(SecurityUtils.getDeptId());
        List<MesKpiFill> list = mesKpiFillService.selectMesKpiFillList(mesKpiFill);

        return getDataTable(list);
    }

    /**
     * 修改KPI填报
     */
    @PreAuthorize("@ss.hasPermi('dpms:kpiFill:edit')")
    @Log(title = "KPI填报", businessType = BusinessType.UPDATE)
    @PutMapping(value = "/edit")
    public AjaxResult edit(@RequestBody String mesKpiFill)
    {
        return mesKpiFillService.updateMesKpiFill(mesKpiFill);
    }

}
