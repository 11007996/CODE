package com.ruoyi.dpms.mapper;

import com.ruoyi.dpms.domain.MesKpiObject1;

import java.util.List;

public interface MesKpiObject1Mapper {
    /**
     * 通过Assign表的ID关联Detail表，查询三个字段targetParam、targetParam、targetParam
     *
     * @param
     * @return
     */

    public List<MesKpiObject1> selectMesKpiObject1ById(Long id);
}