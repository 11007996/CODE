package com.ruoyi.dpms.mapper;

import com.ruoyi.dpms.domain.MesKpiReport;

import java.util.List;

public interface MesKpiReportMapper {

    /**
     * 查询KPI报表
     */
    public List<MesKpiReport> selectMesKpiReportList(MesKpiReport mesKpiReport);


}
