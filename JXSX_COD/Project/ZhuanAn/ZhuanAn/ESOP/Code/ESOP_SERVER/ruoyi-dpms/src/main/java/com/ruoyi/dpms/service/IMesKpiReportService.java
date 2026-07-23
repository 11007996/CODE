package com.ruoyi.dpms.service;

import com.ruoyi.dpms.domain.MesKpiReport;

import java.util.List;

/**
 * KPI报表Service接口
 *
 * @author ruoyi
 * @date 2022-11-17
 */

public interface IMesKpiReportService {

    /**
     * 查询KPI报表
     */
    public List<MesKpiReport> selectMesKpiReportList(MesKpiReport mesKpiReport);
}
