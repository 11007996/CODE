package com.ruoyi.web.controller.product;

import com.ruoyi.common.annotation.Log;
import com.ruoyi.common.constant.UserConstants;
import com.ruoyi.common.core.controller.BaseController;
import com.ruoyi.common.core.domain.AjaxResult;
import com.ruoyi.common.core.page.TableDataInfo;
import com.ruoyi.common.enums.BusinessType;
import com.ruoyi.common.utils.poi.ExcelUtil;
import com.ruoyi.product.domain.MesProcess;
import com.ruoyi.product.domain.MesStage;
import com.ruoyi.product.service.IMesProcessService;
import com.ruoyi.product.service.IMesStageService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.web.bind.annotation.*;

import javax.servlet.http.HttpServletResponse;
import java.util.List;

/**
 * 制程Controller
 *
 * @author ruoyi
 * @date 2022-09-14
 */
@RestController
@RequestMapping("/product/process")
public class MesProcessController extends BaseController
{
    @Autowired
    private IMesProcessService mesProcessService;

    @Autowired
    private IMesStageService mesStageService;


    /**
     * 查询制程列表
     */
//    @PreAuthorize("@ss.hasPermi('product:process:list')")
    @GetMapping("/list")
    public TableDataInfo list(MesProcess mesProcess)
    {
        startPage();
        List<MesProcess> list = mesProcessService.selectMesProcessList(mesProcess);
        return getDataTable(list);
    }

    /**
     * 导出制程列表
     */
    @PreAuthorize("@ss.hasPermi('product:process:export')")
    @Log(title = "制程", businessType = BusinessType.EXPORT)
    @PostMapping("/export")
    public void export(HttpServletResponse response, MesProcess mesProcess)
    {
        List<MesProcess> list = mesProcessService.selectMesProcessList(mesProcess);
        ExcelUtil<MesProcess> util = new ExcelUtil<MesProcess>(MesProcess.class);
        util.exportExcel(response, list, "制程数据");
    }

    /**
     * 获取制程详细信息
     */
    @PreAuthorize("@ss.hasPermi('product:process:query')")
    @GetMapping(value = "/{processId}")
    public AjaxResult getInfo(@PathVariable("processId") Long processId)
    {
        return AjaxResult.success(mesProcessService.selectMesProcessByProcessId(processId));
    }

    /**
     * 新增制程
     */
    @PreAuthorize("@ss.hasPermi('product:process:add')")
    @Log(title = "制程", businessType = BusinessType.INSERT)
    @PostMapping
    public AjaxResult add(@RequestBody MesProcess mesProcess)
    {
        if(UserConstants.NOT_UNIQUE.equals(mesProcessService.checkProcessNameUnique(mesProcess))){
            return AjaxResult.error("新增制程'" +mesProcess.getProcessName() + "失败，制程名称已存在");
        }
        mesProcess.setCreateBy(getUsername());
        return toAjax(mesProcessService.insertMesProcess(mesProcess));
    }

    /**
     * 修改制程
     */
    @PreAuthorize("@ss.hasPermi('product:process:edit')")
    @Log(title = "制程", businessType = BusinessType.UPDATE)
    @PutMapping
    public AjaxResult edit(@RequestBody MesProcess mesProcess)
    {
//        mesProcessService.checkProcessAllowed(mesProcess);
//        mesProcessService.checkProcessDataScope(mesProcess.getProcessId());
        if(UserConstants.NOT_UNIQUE.equals(mesProcessService.checkProcessNameUnique(mesProcess))){
            return AjaxResult.error("新增制程'" +mesProcess.getProcessName() + "失败，制程名称已存在");
        }
        mesProcess.setUpdateBy(getUsername());
        return toAjax(mesProcessService.updateMesProcess(mesProcess));
    }

    /**
     * 修改制程状态
     */
    @PreAuthorize("@ss.hasPermi('product:process:edit')")
    @Log(title = "制程", businessType = BusinessType.UPDATE)
    @PutMapping("/changeStatus")
    public AjaxResult changeStatus(@RequestBody MesProcess mesProcess)
    {
//        mesProcessService.checkProcessAllowed(mesProcess);
//        mesProcessService.checkProcessDataScope(mesProcess.getProcessId());
        mesProcess.setUpdateBy(getUsername());
        return toAjax(mesProcessService.updateMesProcessStatus(mesProcess));
    }

    /**
     * 删除制程
     */
    @PreAuthorize("@ss.hasPermi('product:process:remove')")
    @Log(title = "制程", businessType = BusinessType.DELETE)
	@DeleteMapping("/{processIds}")
    public AjaxResult remove(@PathVariable Long[] processIds)
    {
        return toAjax(mesProcessService.deleteMesProcessByProcessIds(processIds));
    }


    /**
     * 获取区段List
     */
//    @PreAuthorize("@ss.hasPermi('product:process:list')")
    @GetMapping("/stageList")
    public AjaxResult stageList(MesStage stage)
    {
        stage.setStatus("0");
        return AjaxResult.success(mesStageService.selectMesStageList(stage));
    }
}
