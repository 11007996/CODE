package com.ruoyi.web.controller.product;

import com.ruoyi.common.annotation.Log;
import com.ruoyi.common.core.controller.BaseController;
import com.ruoyi.common.core.domain.AjaxResult;
import com.ruoyi.common.core.page.TableDataInfo;
import com.ruoyi.common.enums.BusinessType;
import com.ruoyi.common.utils.poi.ExcelUtil;
import com.ruoyi.product.domain.MesLine;
import com.ruoyi.product.domain.MesProcess;
import com.ruoyi.product.domain.MesStage;
import com.ruoyi.product.domain.MesTerminal;
import com.ruoyi.product.service.IMesLineService;
import com.ruoyi.product.service.IMesProcessService;
import com.ruoyi.product.service.IMesStageService;
import com.ruoyi.product.service.IMesTerminalService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.web.bind.annotation.*;

import javax.servlet.http.HttpServletResponse;
import java.util.List;

/**
 * 工站Controller
 *
 * @author ruoyi
 * @date 2022-09-14
 */
@RestController
@RequestMapping("/product/terminal")
public class MesTerminalController extends BaseController
{
    @Autowired
    private IMesTerminalService mesTerminalService;
    @Autowired
    private IMesProcessService mesProcessService;
    @Autowired
    private IMesStageService mesStageService;
    @Autowired
    private IMesLineService mesLineService;

    /**
     * 查询工站列表
     */
//    @PreAuthorize("@ss.hasPermi('product:terminal:list')")  //去掉权限管控（process =》 terminal）
    @GetMapping("/list")
    public TableDataInfo list(MesTerminal mesTerminal)
    {
        startPage();
        List<MesTerminal> list = mesTerminalService.selectMesTerminalList(mesTerminal);
        return getDataTable(list);
    }



    /**
     * 导出工站列表
     */
    @PreAuthorize("@ss.hasPermi('product:terminal:export')")
    @Log(title = "工站", businessType = BusinessType.EXPORT)
    @PostMapping("/export")
    public void export(HttpServletResponse response, MesTerminal mesTerminal)
    {
        List<MesTerminal> list = mesTerminalService.selectMesTerminalList(mesTerminal);
        ExcelUtil<MesTerminal> util = new ExcelUtil<MesTerminal>(MesTerminal.class);
        util.exportExcel(response, list, "工站数据");
    }

    /**
     * 获取工站详细信息
     */
//    @PreAuthorize("@ss.hasPermi('product:terminal:query')")
    @GetMapping(value = "/{terminalId}")
    public AjaxResult getInfo(@PathVariable("terminalId") Long terminalId)
    {
        return AjaxResult.success(mesTerminalService.selectMesTerminalByTerminalId(terminalId));
    }

    /**
     * 新增工站
     */
//    @PreAuthorize("@ss.hasPermi('product:terminal:add')")
    @Log(title = "工站", businessType = BusinessType.INSERT)
    @PostMapping
//    public AjaxResult add(@RequestBody MesTerminal mesTerminal)
//    {
//        Long lineId = mesTerminal.getLineId();
//        MesLine mesline = mesLineService.selectMesLineByLineId(lineId);
//        String siteName = mesline.getSiteName();
//        String lineName = mesline.getLineName();
//
//        String processName = mesProcessService.selectMesProcessByProcessId(mesTerminal.getProcessId()).getProcessName();
//        Integer maxSeq = mesTerminalService.getMaxSequenceByProcessId(mesTerminal);
//        int max = maxSeq == null ? 0 : maxSeq;
//        int num = mesTerminal.getTerminalNum();
//        for ( int i = max + 1 ; i <=  max + num; i++) {
//            MesTerminal terminal = new MesTerminal();
//            terminal.setLineId(mesTerminal.getLineId());
//            terminal.setStageId(mesTerminal.getStageId());
//            terminal.setProcessId(mesTerminal.getProcessId());
//            terminal.setCreateBy(getUsername());
//            terminal.setSequence(i);
//            terminal.setTerminalName(siteName+"_"+lineName+"_"+i+"_"+processName);
//            mesTerminalService.insertMesTerminal(terminal);
//        }
//        return toAjax(1);
//    }

    /**
     * 新增MAC地址绑定line+stage+process
     */
    public AjaxResult add(@RequestBody MesTerminal mesTerminal)
    {
//        Long lineId = mesTerminal.getLineId();
//        MesLine mesline = mesLineService.selectMesLineByLineId(lineId);
//        String lineName = mesline.getLineName();
//        String processName = mesProcessService.selectMesProcessByProcessId(mesTerminal.getProcessId()).getProcessName();
        Integer maxSeq = mesTerminalService.getMaxSequenceByProcessId(mesTerminal);
        int max = maxSeq == null ? 0 : maxSeq;
        String[] macList = mesTerminal.getMacName();
        for (int i = 0; i < macList.length; i++) {
            MesTerminal terminal = new MesTerminal();
            terminal.setLineId(mesTerminal.getLineId());
            terminal.setStageId(mesTerminal.getStageId());
            terminal.setProcessId(mesTerminal.getProcessId());
//            terminal.setCreateBy(getUsername());
            terminal.setSequence(max+(i+1));
            terminal.setTerminalName(macList[i]);
            //查询当前mac是否已经有绑定了line+stage+process
            List<MesTerminal> macRelatedList= mesTerminalService.selectMesTerminalList(terminal);
            if (macRelatedList.size()>0){
                mesTerminalService.updateMesTerminal(terminal);
            }
                mesTerminalService.insertMesTerminal(terminal);
        }
        return AjaxResult.success();
    }

    /**
     * 修改工站
     */
    @PreAuthorize("@ss.hasPermi('product:terminal:edit')")
    @Log(title = "工站", businessType = BusinessType.UPDATE)
    @PutMapping
    public AjaxResult edit(@RequestBody MesTerminal mesTerminal)
    {
        return toAjax(mesTerminalService.updateMesTerminal(mesTerminal));
    }

    /**
     * 删除工站
     */
    @PreAuthorize("@ss.hasPermi('product:terminal:remove')")
    @Log(title = "工站", businessType = BusinessType.DELETE)
	@DeleteMapping("/{terminalIds}")
    public AjaxResult remove(@PathVariable Long[] terminalIds)
    {
        return toAjax(mesTerminalService.deleteMesTerminalByTerminalIds(terminalIds));
    }


    /** ========================================================================*/

    /**
     * 查询可用 线别
     */
//    @PreAuthorize("@ss.hasPermi('product:terminal:list')")
    @GetMapping("/getMesLineOptions")
    public TableDataInfo getMesLineOptions(MesLine mesLine)
    {
        mesLine.setStatus("0");
        List<MesLine> list = mesLineService.selectMesLineList(mesLine);
        return getDataTable(list);
    }

    /**
     * 查询可用 区段
     */
//    @PreAuthorize("@ss.hasPermi('product:terminal:list')")
    @GetMapping("/getMesStageOptions")
    public TableDataInfo getMesStageOptions(MesStage mesStage)
    {
        mesStage.setStatus("0");
        List<MesStage> list = mesStageService.selectMesStageList(mesStage);
        return getDataTable(list);
    }
    /**
     * 通过stageId查询制程
     */
//    @PreAuthorize("@ss.hasPermi('product:terminal:list')")
    @GetMapping("/getMesProcessOptionsByStageId/{stageId}")
    public TableDataInfo getMesProcessOptionsByStageId(@PathVariable Long stageId)
    {
        MesProcess mesProcess = new MesProcess();
        mesProcess.setStageId(stageId);
        mesProcess.setStatus("0");
        List<MesProcess> list = mesProcessService.selectMesProcessList(mesProcess);
        return getDataTable(list);
    }


/** ========================================================================*/

//    @PreAuthorize("@ss.hasPermi('product:terminal:list')")
    @GetMapping("/getLineInTerminal")
    public TableDataInfo getLineInTerminal()
    {
        List<MesTerminal> list = mesTerminalService.selectLineList();
        return getDataTable(list);
    }


    /**
     * 通过lineId查询Terminal区段
     */
//    @PreAuthorize("@ss.hasPermi('product:terminal:list')")  //去掉权限管控（line =》 stage）
    @GetMapping("/getStageInTerminal/{lineId}")
    public TableDataInfo getStageInTerminal(@PathVariable String lineId)
    {
        MesTerminal mesTerminal = new MesTerminal();
        mesTerminal.setLineId(Long.valueOf(lineId));
        List<MesTerminal> list = mesTerminalService.selectStageList(mesTerminal);
        return getDataTable(list);
    }
//    /**
//     * 通过lineName查询Terminal区段
//     */
//    @GetMapping("/getStageInTerminal/{lineName}")
//    public TableDataInfo getStageInTerminal(@PathVariable String lineName)
//    {
//        MesTerminal mesTerminal = new MesTerminal();
//        mesTerminal.setLineName(lineName);
//        List<MesTerminal> list = mesTerminalService.selectStageList(mesTerminal);
//        return getDataTable(list);
//    }

    /**
     * 通过lineId & stageId查询Terminal制程
     */
//    @PreAuthorize("@ss.hasPermi('product:terminal:list')")  //去掉权限管控（line+stage =》 process）
    @GetMapping("/getProcessInTerminal/{lineId}/{stageId}")
    public TableDataInfo getProcessInTerminal(@PathVariable Long lineId, @PathVariable Long stageId)
    {
        MesTerminal mesTerminal = new MesTerminal();
        mesTerminal.setLineId(lineId);
        mesTerminal.setStageId(stageId);
        List<MesTerminal> list = mesTerminalService.selectProcessList(mesTerminal);
        return getDataTable(list);
    }

//    @PreAuthorize("@ss.hasPermi('product:terminal:list')")
    @PostMapping("/getProcessInTerminal")
    public TableDataInfo getProcessInTerminal(@RequestBody MesTerminal mesTerminal)
    {
        List<MesTerminal> list = mesTerminalService.selectProcessList(mesTerminal);
        return getDataTable(list);
    }

    /**
     * 通过lineId & stageId & processId查询Terminal工站
     */
//    @PreAuthorize("@ss.hasPermi('product:terminal:list')")
    @GetMapping("/selectTerminalByTerminalTable")
    public TableDataInfo selectTerminalByTerminalTable(MesTerminal mesTerminal)
    {
        startPage();
        List<MesTerminal> list = mesTerminalService.selectMesTerminalList(mesTerminal);
        return getDataTable(list);
    }
}
