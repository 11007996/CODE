package com.ruoyi.web.controller.dpms;

import com.ruoyi.common.core.controller.BaseController;
import com.ruoyi.common.core.page.TableDataInfo;
import com.ruoyi.dpms.domain.MesKpiReport;
import com.ruoyi.dpms.service.IMesKpiReportService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import java.util.List;

/**
 * KPI报表Controller
 *
 * @author ruoyi
 * @date 2022-11-17
 */
@RestController
@RequestMapping("/dpms/kpiReport")

public class MesKpiReportController extends BaseController {
    @Autowired
    private IMesKpiReportService mesKpiReportService;

    /**
     * 查询KPI报表
     */
    @PreAuthorize("@ss.hasPermi('dpms:kpiReport:list')")
    @GetMapping("/list")
    public TableDataInfo list(MesKpiReport mesKpiReport)
    {
        startPage();
        List<MesKpiReport> list = mesKpiReportService.selectMesKpiReportList(mesKpiReport);
        return getDataTable(list);
    }
}
