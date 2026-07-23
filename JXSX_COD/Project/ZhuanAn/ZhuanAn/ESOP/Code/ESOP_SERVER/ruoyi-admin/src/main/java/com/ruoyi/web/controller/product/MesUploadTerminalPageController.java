package com.ruoyi.web.controller.product;

import com.ruoyi.common.annotation.Log;
import com.ruoyi.common.core.controller.BaseController;
import com.ruoyi.common.core.domain.AjaxResult;
import com.ruoyi.common.core.page.TableDataInfo;
import com.ruoyi.common.enums.BusinessType;
import com.ruoyi.common.utils.poi.ExcelUtil;
import com.ruoyi.product.domain.MesSop;
import com.ruoyi.product.domain.MesSopGroup;
import com.ruoyi.product.domain.MesTerminal;
import com.ruoyi.product.domain.MesUploadTerminalPage;
import com.ruoyi.product.service.IMesSopGroupService;
import com.ruoyi.product.service.IMesSopService;
import com.ruoyi.product.service.IMesUploadTerminalPageService;
import com.ruoyi.replace.service.IMesMaterialRelateLineService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.web.bind.annotation.*;

import javax.servlet.http.HttpServletResponse;
import java.util.List;
import java.util.Map;

/**
 * 站点页码Controller
 *
 * @author ruoyi
 * @date 2023-01-06
 */
@RestController
@RequestMapping("/system/mesUploadTerminalPage")
public class MesUploadTerminalPageController extends BaseController
{
    @Autowired
    private IMesUploadTerminalPageService mesUploadTerminalPageService;

    @Autowired
    private IMesMaterialRelateLineService mesMaterialRelateLineService;

    @Autowired
    private IMesSopGroupService mesSopGroupService;
    @Autowired
    private IMesSopService mesSopService;

    /**
     * 查询站点页码列表
     */
    @PreAuthorize("@ss.hasPermi('system:mesUploadTerminalPage:list')")
    @GetMapping("/list")
    public TableDataInfo list(MesUploadTerminalPage mesUploadTerminalPage)
    {
        startPage();
        List<MesUploadTerminalPage> list = mesUploadTerminalPageService.selectMesUploadTerminalPageList(mesUploadTerminalPage);
        return getDataTable(list);
    }

    /**
     * 导出站点页码列表
     */
    @PreAuthorize("@ss.hasPermi('system:mesUploadTerminalPage:export')")
    @Log(title = "站点页码", businessType = BusinessType.EXPORT)
    @PostMapping("/export")
    public void export(HttpServletResponse response, MesUploadTerminalPage mesUploadTerminalPage)
    {
        List<MesUploadTerminalPage> list = mesUploadTerminalPageService.selectMesUploadTerminalPageList(mesUploadTerminalPage);
        ExcelUtil<MesUploadTerminalPage> util = new ExcelUtil<MesUploadTerminalPage>(MesUploadTerminalPage.class);
        util.exportExcel(response, list, "站点页码数据");
    }

    /**
     * 获取站点页码详细信息
     */
    @PreAuthorize("@ss.hasPermi('system:mesUploadTerminalPage:query')")
    @GetMapping(value = "/{id}")
    public AjaxResult getInfo(@PathVariable("oaId") String oaId)
    {
        return AjaxResult.success(mesUploadTerminalPageService.selectMesUploadTerminalPageById(oaId));
    }

    /**
     * 新增站点页码
     */
    @PreAuthorize("@ss.hasPermi('system:mesUploadTerminalPage:add')")
    @Log(title = "站点页码", businessType = BusinessType.INSERT)
    @PostMapping
    public AjaxResult add(@RequestBody MesUploadTerminalPage mesUploadTerminalPage)
    {
        return toAjax(mesUploadTerminalPageService.insertMesUploadTerminalPage(mesUploadTerminalPage));
    }

    /**
     * 修改站点页码
     */
    @PreAuthorize("@ss.hasPermi('system:mesUploadTerminalPage:edit')")
    @Log(title = "站点页码", businessType = BusinessType.UPDATE)
    @PutMapping
    public AjaxResult edit(@RequestBody MesUploadTerminalPage mesUploadTerminalPage)
    {
        return toAjax(mesUploadTerminalPageService.updateMesUploadTerminalPage(mesUploadTerminalPage));
    }

    /**
     * 删除站点页码
     */
    @PreAuthorize("@ss.hasPermi('system:mesUploadTerminalPage:remove')")
    @Log(title = "站点页码", businessType = BusinessType.DELETE)
    @DeleteMapping("/{ids}")
    public AjaxResult remove(@PathVariable Long[] ids)
    {
        return toAjax(mesUploadTerminalPageService.deleteMesUploadTerminalPageByIds(ids));
    }

    /**
     * 根据料号materialId+线别lineId,获取最新(createTime)的一条模板信息
     */
//    @GetMapping("/templateList")
//    public TableDataInfo templateList(@Param("materialId") Long materialId, @Param("lineId") Long lineId)
//    {
//        List<MesUploadTerminalPage> list = mesUploadTerminalPageService.selectTemplateByPartNo(materialId,lineId);
//        return getDataTable(list);
//    }

    /**
     * 根据料号material,获取最新(createTime)的一条模板信息
     */
    @GetMapping("/templateList")
    public TableDataInfo templateList(Long materialId)
    {
        List<MesUploadTerminalPage> list = mesUploadTerminalPageService.selectTemplateByPartNo(materialId);
        return getDataTable(list);
    }


//    public TableDataInfo templateList(Long modelId)
//    {
//        //1.根据机种选择料号
//        List<MesMaterialInfo> list1= mesMaterialInfoService.selectMaterialByModel(modelId);
//        //2.根据料号到mes_material_relate_line表查询到对应的线体信息
//        for (int i = 0; i < list1.size(); i++) {
//            List<MesMaterialRelateLine> list2 = mesMaterialRelateLineService.selectLineByMaterial(list1.get(i).getModelId());
//            for (int j = 0; j < list2.size(); j++) {
//                List<MesUploadTerminalPage> list3 = mesUploadTerminalPageService.selectMesUploadTerminalPageById()
////                List<MesUploadTerminalPage> list3 = mesUploadTerminalPageService.selectTemplateByPartNo(materialId);
//            }
//            //3.前端选择完某一条线体，再
//
//        }
//        return getDataTable(list);
//    }

//    /**
//     * 根据料号material,获取已签核的sop信息(弃用)
//     * @return 结果
//     */
//    @GetMapping("/templateInfo")
//    public TableDataInfo templateInfo(String materialId)
//    {
//        List<Map<String,String>> list = mesUploadTerminalPageService.selectTemplateInfoByPartNo(materialId);
//        return getDataTable(list);
//    }

//    /**
//     * 根据line+stage,获取已签核的sop信息（mulan在用）
//     * @return 结果
//     */
//    @GetMapping("/templateInfo")
//    public TableDataInfo templateInfo(String stageId)
//    {
//        List<Map<String,String>> list = mesUploadTerminalPageService.selectTemplateInfoByStage(stageId);
//        return getDataTable(list);
//    }
    /**
     * 根据modelId+lineId、stageId、processId逐阶带出数据,获取已签核的sop信息(BU17在用)
     * @return 结果
     */
    @GetMapping("/templateInfo")
    public TableDataInfo templateInfo(MesUploadTerminalPage mesUploadTerminalPage)
    {
        List<Map<String,String>> list = mesUploadTerminalPageService.selectTemplateInfoByPartNo(mesUploadTerminalPage);
        return getDataTable(list);
    }

    @PreAuthorize("@ss.hasPermi('system:mesUploadTerminalPage:History')")
    @GetMapping("/HistoricalTemplateInfo")
    public TableDataInfo templateInfoList(MesUploadTerminalPage mesUploadTerminalPage)
    {
        startPage();
        List<Map<String,String>> list = mesUploadTerminalPageService.selectHistoricalTemplateInfoByPartNo(mesUploadTerminalPage);
        return getDataTable(list);
    }
    /**
     * SOP版本管理-联动下拉框的查询
     * */
    @GetMapping("/VersionByIds")
    public TableDataInfo selectVersionByIds(MesUploadTerminalPage mesUploadTerminalPage)
    {
        List<Map<String,String>> list = mesUploadTerminalPageService.selectVersionByIds(mesUploadTerminalPage);
        return getDataTable(list);
    }

    /**
     * 修改sop版本状态（mulan在用）
     * 启用某版本（pdf、mp4）时，将同线别+站位序号的其他版本设为禁用
     */
    @PutMapping("/changeSopStatus")
    public AjaxResult changeSopStatus(@RequestBody MesUploadTerminalPage mesUploadTerminalPage)
    {
//        if (mesUploadTerminalPage.getVersionStatus().equals("0")){
////            Long terminalId = mesUploadTerminalPage.getTerminalId();
//            //注意区分文档和视频
//            List<MesUploadTerminalPage> infoList = mesUploadTerminalPageService.selectHistoricalVersionByProcess(mesUploadTerminalPage);
////            List<MesUploadTerminalPage> infoList = mesUploadTerminalPageService.selectHistoricalVersionByTerminalId(terminalId);
//            for (int i = 0; i < infoList.size(); i++) {
//                MesUploadTerminalPage sopVersionStatus = new MesUploadTerminalPage();
//                sopVersionStatus.setSopId(infoList.get(i).getSopId());
//                sopVersionStatus.setTerminalId(infoList.get(i).getTerminalId());
//                sopVersionStatus.setVersionStatus("1");
//                mesUploadTerminalPageService.updateMesUploadTerminalPageVersionStatus(sopVersionStatus);
//            }
//            mesUploadTerminalPageService.updateMesUploadTerminalPageVersionStatus(mesUploadTerminalPage);
//        }
//        else {
            mesUploadTerminalPageService.updateMesUploadTerminalPageVersionStatus(mesUploadTerminalPage);
//        }
        return AjaxResult.success();
    }

//    /**
//     * 修改sop版本状态（在用）
//     */
//    @PutMapping("/changeSopStatus")
//    public AjaxResult changeSopStatus(@RequestBody MesUploadTerminalPage mesUploadTerminalPage) {
//        //一个制程下可以存在多个版本的sop，如果想要启用某个sop版本，修改UploadTerminalPage表的versionStatus
//        //根据制程和sopId来更新表的数据
//        List<MesUploadTerminalPage> infoList = mesUploadTerminalPageService.selectHistoricalVersionByProcess(mesUploadTerminalPage);
//        for (int i = 0; i < infoList.size(); i++) {
//            MesUploadTerminalPage sopVersionStatus = new MesUploadTerminalPage();
//            sopVersionStatus.setSopId(infoList.get(i).getSopId());
//            sopVersionStatus.setLineId(infoList.get(i).getLineId());
//            sopVersionStatus.setStageId(infoList.get(i).getStageId());
//            sopVersionStatus.setProcessId(infoList.get(i).getProcessId());
//            sopVersionStatus.setTerminalId(infoList.get(i).getTerminalId());
//            sopVersionStatus.setVersionStatus(mesUploadTerminalPage.getVersionStatus());
//            mesUploadTerminalPageService.updateMesUploadTerminalPageVersionStatus(sopVersionStatus);
//
//        }
//
//        //如果某个sop的版本被停用（VersionStatus = 1）了，那sop_group表的推送状态改为未推送（status=0）
//        //根据sop_id、type来更新对应的多条数据数据
//        if (mesUploadTerminalPage.getVersionStatus().equals("1")){
//            MesSopGroup pushStatus = new MesSopGroup();
//            pushStatus.setSopId(mesUploadTerminalPage.getSopId());
//            pushStatus.setType(mesUploadTerminalPage.getType());
//            List<MesSopGroup> sopGroupList = mesSopGroupService.selectMesSopGroupList(pushStatus);
//            for (int j = 0; j < sopGroupList.size(); j++) {
//                pushStatus.setSopGroupId(sopGroupList.get(j).getSopGroupId());
//                pushStatus.setPushStatus("0");
//                mesSopGroupService.updateMesSopGroup(pushStatus);
//            }
//        }
//        return AjaxResult.success("修改成功");
//    }


    /**
     * 根据制程,获取已签核的sop信息(同一制程可以有多个版本的sop为启用状态)
     * @return 结果
     */
    @GetMapping("/sopVersionByProcess/{processId}")
    public TableDataInfo sopVersionByProcess(Long processId)
    {
        List<MesUploadTerminalPage> list = mesUploadTerminalPageService.selectSopVersionByProcess(processId);
        return getDataTable(list);
    }

}
