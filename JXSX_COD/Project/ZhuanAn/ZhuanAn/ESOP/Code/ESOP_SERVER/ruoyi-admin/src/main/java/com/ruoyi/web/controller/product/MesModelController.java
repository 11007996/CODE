package com.ruoyi.web.controller.product;

import com.ruoyi.common.annotation.Log;
import com.ruoyi.common.constant.UserConstants;
import com.ruoyi.common.core.controller.BaseController;
import com.ruoyi.common.core.domain.AjaxResult;
import com.ruoyi.common.core.page.TableDataInfo;
import com.ruoyi.common.enums.BusinessType;
import com.ruoyi.common.utils.DateUtils;
import com.ruoyi.common.utils.poi.ExcelUtil;
import com.ruoyi.product.domain.MesModel;
import com.ruoyi.product.service.IMesModelService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.web.bind.annotation.*;

import javax.servlet.http.HttpServletResponse;
import java.util.List;

/**
 * 机种信息Controller
 *
 * @author ruoyi
 * @date 2022-09-14
 */
@RestController
@RequestMapping("/product/model")
public class MesModelController extends BaseController
{
    @Autowired
    private IMesModelService mesModelService;

    /**
     * 查询机种信息列表
     */
    @PreAuthorize("@ss.hasPermi('product:model:list')")
    @GetMapping("/list")
    public TableDataInfo list(MesModel mesModel)
    {
        startPage();
        List<MesModel> list = mesModelService.selectMesModelList(mesModel);
        return getDataTable(list);
    }

    /**
     * 导出机种信息列表
     */
    @PreAuthorize("@ss.hasPermi('product:model:export')")
    @Log(title = "机种信息", businessType = BusinessType.EXPORT)
    @PostMapping("/export")
    public void export(HttpServletResponse response, MesModel mesModel)
    {
        List<MesModel> list = mesModelService.selectMesModelList(mesModel);
        ExcelUtil<MesModel> util = new ExcelUtil<MesModel>(MesModel.class);
        util.exportExcel(response, list, "机种信息数据");
    }

    /**
     * 获取机种信息详细信息
     */
    @PreAuthorize("@ss.hasPermi('product:model:query')")
    @GetMapping(value = "/{modelId}")
    public AjaxResult getInfo(@PathVariable("modelId") Long modelId)
    {
        return AjaxResult.success(mesModelService.selectMesModelByModelId(modelId));
    }

    /**
     * 新增机种信息
     */
    @PreAuthorize("@ss.hasPermi('product:model:add')")
    @Log(title = "机种信息", businessType = BusinessType.INSERT)
    @PostMapping
    public AjaxResult add(@RequestBody MesModel mesModel)
    {
        if (UserConstants.NOT_UNIQUE.equals(mesModelService.checkModelNameUnique(mesModel)))
        {
            return AjaxResult.error("新增机种'" + mesModel.getModelName() + "'失败，机种名称已存在");
        }
        mesModel.setCreateBy(getUsername());
        return toAjax(mesModelService.insertMesModel(mesModel));
    }

    /**
     * 修改机种信息
     */
    @PreAuthorize("@ss.hasPermi('product:model:edit')")
    @Log(title = "机种信息", businessType = BusinessType.UPDATE)
    @PutMapping
    public AjaxResult edit(@RequestBody MesModel mesModel)
    {
        if(UserConstants.NOT_UNIQUE.equals(mesModelService.checkModelNameUnique(mesModel)))
        {
            return AjaxResult.error("修改机种'" + mesModel.getModelName() + "'失败，机种名称已存在");
        }
        mesModel.setUpdateBy(getUsername());
        return toAjax(mesModelService.updateMesModel(mesModel));
    }

    /**
     * 修改机种状态
     */
    @PreAuthorize("@ss.hasPermi('product:model:edit')")
    @Log(title = "机种管理", businessType = BusinessType.UPDATE)
    @PutMapping("/changeStatus")
    public AjaxResult changeStatus(@RequestBody MesModel model)
    {
        model.setUpdateBy(getUsername());
        model.setUpdateTime(DateUtils.getNowDate());
        return toAjax(mesModelService.updateModelStatus(model));
    }

    /**
     * 删除机种信息
     */
    @PreAuthorize("@ss.hasPermi('product:model:remove')")
    @Log(title = "机种信息", businessType = BusinessType.DELETE)
	@DeleteMapping("/{modelIds}")
    public AjaxResult remove(@PathVariable Long[] modelIds)
    {
        return toAjax(mesModelService.deleteMesModelByModelIds(modelIds));
    }

    /**
     * 根据机种名称查询机种详情信息
     */
    @PreAuthorize("@ss.hasPermi('product:model:detail')")
    @GetMapping("/modelInfo")
    public AjaxResult modelInfo(String modelName)
    {
        MesModel modelInfo = mesModelService.modelInfoByName(modelName);
        return AjaxResult.success(modelInfo);
    }
}
