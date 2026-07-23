package com.ruoyi.web.controller.replace;

import com.ruoyi.common.annotation.Log;
import com.ruoyi.common.core.controller.BaseController;
import com.ruoyi.common.core.domain.AjaxResult;
import com.ruoyi.common.core.page.TableDataInfo;
import com.ruoyi.common.enums.BusinessType;
import com.ruoyi.common.utils.DateUtils;
import com.ruoyi.replace.domain.CopyTemplateObj;
import com.ruoyi.replace.domain.MesMaterialRelateLine;
import com.ruoyi.replace.service.IMesMaterialInfoService;
import com.ruoyi.replace.service.IMesMaterialRelateLineService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.web.bind.annotation.*;

import java.util.ArrayList;
import java.util.List;

/**
 * 线料关系Controller
 *
 * @author ruoyi
 * @date 2022-12-24
 */
@RestController
@RequestMapping("/replace/relate")
public class MesMaterialRelateLineController extends BaseController {
    @Autowired
    private IMesMaterialRelateLineService mesMaterialRelateLineService;
    @Autowired
    private IMesMaterialInfoService mesMaterialInfoService;


    /**
     * 查询线料关系列表
     */
    @PreAuthorize("@ss.hasPermi('replace:relate:list')")
    @GetMapping("/list")
    public TableDataInfo list(MesMaterialRelateLine mesMaterialRelateLine) {
        startPage();
        List<MesMaterialRelateLine> list = mesMaterialRelateLineService.selectMesMaterialRelateLineList(mesMaterialRelateLine);
        return getDataTable(list);
    }


    /**
     * 获取线料关系详细信息
     */
    @PreAuthorize("@ss.hasPermi('replace:relate:query')")
    @GetMapping(value = "/{id}")
    public AjaxResult getInfo(@PathVariable("id") Long id) {
        return AjaxResult.success(mesMaterialRelateLineService.selectMesMaterialRelateLineById(id));
    }

    /**
     * 新增线料关系
     */
    @PreAuthorize("@ss.hasPermi('replace:relate:add')")
    @Log(title = "线料关系", businessType = BusinessType.INSERT)
    @PostMapping
    public AjaxResult add(@RequestBody MesMaterialRelateLine mesMaterialRelateLine) {
        return toAjax(mesMaterialRelateLineService.insertMesMaterialRelateLine(mesMaterialRelateLine));
    }


    /**
     * 删除线料关系
     */
    @PreAuthorize("@ss.hasPermi('replace:relate:remove')")
    @Log(title = "线料关系", businessType = BusinessType.DELETE)
    @DeleteMapping("/{ids}")
    public AjaxResult remove(@PathVariable Long[] ids) {
        return toAjax(mesMaterialRelateLineService.deleteMesMaterialRelateLineByIds(ids));
    }

//    /**
//     * 根据料号查询线别
//     */
//    @GetMapping("/lineList")
//    public TableDataInfo list(Long materialId)
//    {
//        List<MesMaterialRelateLine> list = mesMaterialRelateLineService.selectLineByMaterial(materialId);
//        return getDataTable(list);
//    }


    /**
     * SOP工艺路线复制(一个母版，多个子版)
     */
//    @Log(title = "工艺路线复制", businessType = BusinessType.UPDATE)
//    @PutMapping("/copyTemplate")
//    public AjaxResult copyTemplate(@RequestBody CopyTemplateObj copyTemplateObj){
//        Long m1 = copyTemplateObj.getParent();
//        String m1Name = mesMaterialInfoService.selectMesMaterialInfoById(m1).getMaterialName();
//        if(mesMaterialRelateLineService.selectLineByMaterial(m1).size() == 0){
//            return AjaxResult.error("料号"+m1Name+"未绑定线体!");
//        };
//        Long[] Child = copyTemplateObj.getChild();
//        for (int i = 0; i < Child.length; i++) {
//            Long m2 = Child[i];
//            mesMaterialRelateLineService.deleteMesMaterialRelateLineByMId(m2);
//            List<MesMaterialRelateLine> mmrl = mesMaterialRelateLineService.selectLineByMaterial(m1);
//            MesMaterialRelateLine mmrlNew = new MesMaterialRelateLine();
//            for (int j = 0; j < mmrl.size(); j++) {
//                mmrlNew.setMaterialId(m2);
//                mmrlNew.setLineId(mmrl.get(j).getLineId());
//                mmrlNew.setUpdateBy(getUsername());
//                mmrlNew.setUpdateTime(DateUtils.getNowDate());
//                mesMaterialRelateLineService.insertMesMaterialRelateLine(mmrlNew);
//            }
//        }
//        return AjaxResult.success("成功复制料号"+m1Name+"的工艺路线");
//    }

//    @Log(title = "工艺路线复制", businessType = BusinessType.UPDATE)
//    @PutMapping("/copyTemplate")
//    public AjaxResult copyTemplate(@RequestBody CopyTemplateObj copyTemplateObj) {
//        Long[] lineIds = copyTemplateObj.getLineIds();
//        Long materialId = copyTemplateObj.getMaterialId();
//        List<MesMaterialRelateLine> mmrl = mesMaterialRelateLineService.selectLineByMaterial(materialId);
//        MesMaterialRelateLine mmrlNew = new MesMaterialRelateLine();//插入到线料关联表的数据——mmrlNew
//        if (mmrl.equals(null)) {
//            for (int i = 0; i < lineIds.length; i++) {
//                Long lineId = lineIds[i];
//                mmrlNew.setMaterialId(materialId);
//                mmrlNew.setLineId(lineId);
//                mmrlNew.setUpdateBy(getUsername());
//                mmrlNew.setUpdateTime(DateUtils.getNowDate());
//                mesMaterialRelateLineService.insertMesMaterialRelateLine(mmrlNew);
//            }
//        } else {
//            //从mmrl取出只包含lineId的list——lindIds1（先初始化，再加数据）
//            List<Long> lineIds1 = new ArrayList<>();
//            for (MesMaterialRelateLine item:mmrl) {
//                lineIds1.add(item.getLineId());
//            }
//            //将lineIds里面与lindIds1不同的数据插入到lindIds1
//            for (Long item:lineIds){
//                if (lineIds1.contains(item)){
//                    continue;
//                }
//                lineIds1.add(item);
//            }
//            //先删掉关联表里面materialId查询到的所有数据，再把lineIds1都加进去
//            mesMaterialRelateLineService.deleteMesMaterialRelateLineByMId(materialId);
//            for (int i = 0; i < lineIds1.size(); i++) {
//                Long lineId = lineIds1.get(i);
//                mmrlNew.setMaterialId(materialId);
//                mmrlNew.setLineId(lineId);
//                mmrlNew.setUpdateBy(getUsername());
//                mmrlNew.setUpdateTime(DateUtils.getNowDate());
//                mesMaterialRelateLineService.insertMesMaterialRelateLine(mmrlNew);
//            }
//        }
//        return AjaxResult.success("操作成功");
//    }

    @Log(title = "工艺路线复制", businessType = BusinessType.UPDATE)
    @PutMapping("/copyTemplate")
    public AjaxResult copyTemplate(@RequestBody CopyTemplateObj copyTemplateObj) {
        Long[] lineIds = copyTemplateObj.getLineIds();
        Long[] materialIds = copyTemplateObj.getMaterialId();

        for (int m = 0; m < materialIds.length; m++) {
            Long materialId = materialIds[m];
            List<MesMaterialRelateLine> mmrl = mesMaterialRelateLineService.selectLineByMaterial(materialId);
            MesMaterialRelateLine mmrlNew = new MesMaterialRelateLine();//插入到线料关联表的数据——mmrlNew
            if (mmrl == null || mmrl.size() == 0) {
                for (int i = 0; i < lineIds.length; i++) {
                    Long lineId = lineIds[i];
                    mmrlNew.setMaterialId(materialId);
                    mmrlNew.setLineId(lineId);
                    mmrlNew.setUpdateBy(getUsername());
                    mmrlNew.setUpdateTime(DateUtils.getNowDate());
                    mesMaterialRelateLineService.insertMesMaterialRelateLine(mmrlNew);
                }
            } else {
                //从mmrl取出只包含lineId的list——lindIds1（先初始化，再加数据）
                List<Long> lineIds1 = new ArrayList<>();
                for (MesMaterialRelateLine item : mmrl) {
                    lineIds1.add(item.getLineId());
                }
                //将lineIds里面与lindIds1不同的数据插入到lindIds1
                for (Long item : lineIds) {
                    if (lineIds1.contains(item)) {
                        continue;
                    }
                    lineIds1.add(item);
                }
                //先删掉关联表里面materialId查询到的所有数据，再把lineIds1都加进去
                mesMaterialRelateLineService.deleteMesMaterialRelateLineByMId(materialId);
                for (int i = 0; i < lineIds1.size(); i++) {
                    Long lineId = lineIds1.get(i);
                    mmrlNew.setMaterialId(materialId);
                    mmrlNew.setLineId(lineId);
                    mmrlNew.setUpdateBy(getUsername());
                    mmrlNew.setUpdateTime(DateUtils.getNowDate());
                    mesMaterialRelateLineService.insertMesMaterialRelateLine(mmrlNew);
                }
            }
        }
        return AjaxResult.success("操作成功");
    }
}
