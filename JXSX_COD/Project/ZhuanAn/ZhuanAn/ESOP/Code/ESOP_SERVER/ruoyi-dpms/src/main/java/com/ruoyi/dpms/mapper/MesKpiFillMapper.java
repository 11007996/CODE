package com.ruoyi.dpms.mapper;

import com.ruoyi.dpms.domain.MesKpiFill;

import java.util.List;

public interface MesKpiFillMapper {


    /**
     * 查询KPI填报
     *
     * @param mesKpiFill KPI填报
     * @return KPI填报
     */
    public List<MesKpiFill> selectMesKpiFillList(MesKpiFill mesKpiFill);


}
