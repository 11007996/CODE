package com.ruoyi.dpms.mapper;

import com.ruoyi.dpms.domain.MesKpiPersonGroup;

public interface MesKpiPersonGroupMapper {
    /**
     * 新增KPI指派
     *
     * @param mesKpiPersonGroup KPI指派
     * @return 结果
     */
    public int insertMesKpiPersonGroup(MesKpiPersonGroup mesKpiPersonGroup);

    /**
     * 修改KPI指派
     *
     * @param mesKpiPersonGroup KPI指派
     * @return 结果
     */
    public int updateMesKpiPersonGroup(MesKpiPersonGroup mesKpiPersonGroup);

    public int deleteMesKpiPersonGroupByAssignId(Long mkpgAssignId);
}
