package com.ruoyi.dpms.service;

import com.ruoyi.common.core.domain.AjaxResult;
import com.ruoyi.dpms.domain.MesKpiFill;

import java.util.List;

public interface IMesKpiFillService {
    /**
     * 查询KPI填报
     *
     * @param mesKpiFill KPI填报
     * @return KPI填报
     */

    public List<MesKpiFill> selectMesKpiFillList(MesKpiFill mesKpiFill);

//    AjaxResult insertMesKpiAssign(String mesKpiFill);

    /**
     * 修改KPI指派
     *
     * @param mesKpiFill KPI指派
     * @return 结果
     */
    public AjaxResult updateMesKpiFill(String mesKpiFill);
}
