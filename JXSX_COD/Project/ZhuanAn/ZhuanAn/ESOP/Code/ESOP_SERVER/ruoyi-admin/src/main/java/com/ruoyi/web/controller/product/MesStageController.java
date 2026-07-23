package com.ruoyi.web.controller.product;

import com.ruoyi.common.annotation.Log;
import com.ruoyi.common.constant.UserConstants;
import com.ruoyi.common.core.controller.BaseController;
import com.ruoyi.common.core.domain.AjaxResult;
import com.ruoyi.common.core.page.TableDataInfo;
import com.ruoyi.common.enums.BusinessType;
import com.ruoyi.common.utils.poi.ExcelUtil;
import com.ruoyi.product.domain.MesStage;
import com.ruoyi.product.service.IMesStageService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.web.bind.annotation.*;

import javax.servlet.http.HttpServletResponse;
import java.util.List;

/**
 * 区段管理Controller
 *
 * @author ruoyi
 * @date 2022-09-14
 */
@RestController
@RequestMapping("/product/stage")
public class MesStageController extends BaseController
{
    @Autowired
    private IMesStageService mesStageService;

    /**
     * 查询区段管理列表
     */
//    @PreAuthorize("@ss.hasPermi('product:stage:list')")
    @GetMapping("/list")
    public TableDataInfo list(MesStage mesStage)
    {
        startPage();
        List<MesStage> list = mesStageService.selectMesStageList(mesStage);
        return getDataTable(list);
    }

    /**
     * 导出区段管理列表
     */
    @PreAuthorize("@ss.hasPermi('product:stage:export')")
    @Log(title = "区段管理", businessType = BusinessType.EXPORT)
    @PostMapping("/export")
    public void export(HttpServletResponse response, MesStage mesStage)
    {
        List<MesStage> list = mesStageService.selectMesStageList(mesStage);
        ExcelUtil<MesStage> util = new ExcelUtil<MesStage>(MesStage.class);
        util.exportExcel(response, list, "区段管理数据");
    }

    /**
     * 获取区段管理详细信息
     */
    @PreAuthorize("@ss.hasPermi('product:stage:query')")
    @GetMapping(value = "/{stageId}")
    public AjaxResult getInfo(@PathVariable("stageId") Long stageId)
    {
        return AjaxResult.success(mesStageService.selectMesStageByStageId(stageId));
    }

    /**
     * 新增区段管理
     */
    @PreAuthorize("@ss.hasPermi('product:stage:add')")
    @Log(title = "区段管理", businessType = BusinessType.INSERT)
    @PostMapping
    public AjaxResult add(@RequestBody MesStage stage)
    {
        if (UserConstants.NOT_UNIQUE.equals(mesStageService.checkStageNameUnique(stage)))
        {
            return AjaxResult.error("新增区段'" + stage.getStageName() + "'失败，区段名称已存在");
        }
        stage.setCreateBy(getUsername());
        return toAjax(mesStageService.insertMesStage(stage));
    }

    /**
     * 修改区段管理
     */
    @PreAuthorize("@ss.hasPermi('product:stage:edit')")
    @Log(title = "区段管理", businessType = BusinessType.UPDATE)
    @PutMapping
    public AjaxResult edit(@RequestBody MesStage stage)
    {
        //mesStageService.checkStageAllowed(stage);
        //mesStageService.checkStageDataScope(stage.getStageId());
        if(UserConstants.NOT_UNIQUE.equals(mesStageService.checkStageNameUnique(stage)))
        {
            return AjaxResult.error("修改区段'" + stage.getStageName() + "'失败，区段名称已存在");
        }
        stage.setUpdateBy(getUsername());
        return toAjax(mesStageService.updateMesStage(stage));
    }

    /**
     * 修改区段状态
     */
    @PreAuthorize("@ss.hasPermi('product:stage:edit')")
    @Log(title = "区段管理", businessType = BusinessType.UPDATE)
    @PutMapping("/changeStatus")
    public AjaxResult changeStatus(@RequestBody MesStage stage)
    {
        //mesStageService.checkStageAllowed(stage);
        //mesStageService.checkStageDataScope(stage.getStageId());
        stage.setUpdateBy(getUsername());
        return toAjax(mesStageService.updateStageStatus(stage));
    }

    /**
     * 删除区段管理
     */
    @PreAuthorize("@ss.hasPermi('product:stage:remove')")
    @Log(title = "区段管理", businessType = BusinessType.DELETE)
	@DeleteMapping("/{stageIds}")
    public AjaxResult remove(@PathVariable Long[] stageIds)
    {
        return toAjax(mesStageService.deleteMesStageByStageIds(stageIds));
    }
}
