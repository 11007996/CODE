package com.ruoyi.web.controller.product;

import com.ruoyi.common.annotation.Log;
import com.ruoyi.common.core.controller.BaseController;
import com.ruoyi.common.core.domain.AjaxResult;
import com.ruoyi.common.core.page.TableDataInfo;
import com.ruoyi.common.enums.BusinessType;
import com.ruoyi.replace.domain.MesMaterialInfo;
import com.ruoyi.replace.service.IMesMaterialInfoService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.web.bind.annotation.*;

import java.util.List;

/**
 * 物料基础信息Controller
 *
 * @author ruoyi
 * @date 2022-12-24
 */
@RestController
@RequestMapping("/replace/materialInfo")
public class MesMaterialInfoController extends BaseController
{
    @Autowired
    private IMesMaterialInfoService mesMaterialInfoService;

    /**
     * 查询物料基础信息列表
     */
    @PreAuthorize("@ss.hasPermi('replace:materialInfo:list')")
    @GetMapping("/list")
    public TableDataInfo list(MesMaterialInfo mesMaterialInfo)
    {
        startPage();
        List<MesMaterialInfo> list = mesMaterialInfoService.selectMesMaterialInfoList(mesMaterialInfo);
        return getDataTable(list);
    }


    /**
     * 获取物料基础信息详细信息
     */
    @PreAuthorize("@ss.hasPermi('replace:materialInfo:query')")
    @GetMapping(value = "/{id}")
    public AjaxResult getInfo(@PathVariable("id") Long id)
    {
        startPage();
        return AjaxResult.success(mesMaterialInfoService.selectMesMaterialInfoById(id));
    }

    /**
     * 新增物料基础信息
     */
    @PreAuthorize("@ss.hasPermi('replace:materialInfo:add')")
    @Log(title = "物料基础信息", businessType = BusinessType.INSERT)
    @PostMapping("/add")
    public AjaxResult add(@RequestBody MesMaterialInfo mesMaterialInfo)
    {
        mesMaterialInfo.setCreateBy(getUsername());
        return toAjax(mesMaterialInfoService.insertMesMaterialInfo(mesMaterialInfo));
    }

    /**
     * 修改物料基础信息
     */
    @PreAuthorize("@ss.hasPermi('replace:materialInfo:edit')")
    @Log(title = "物料基础信息", businessType = BusinessType.UPDATE)
    @PutMapping("/edit")
    public AjaxResult edit(@RequestBody MesMaterialInfo mesMaterialInfo)
    {
        return toAjax(mesMaterialInfoService.updateMesMaterialInfo(mesMaterialInfo));
    }

    /**
     * 根据机种查询料号
     */
    @GetMapping("/materialList")
    public TableDataInfo list(Long modelId)
    {
        List<MesMaterialInfo> list = mesMaterialInfoService.selectMaterialByModel(modelId);
        return getDataTable(list);
    }

}
