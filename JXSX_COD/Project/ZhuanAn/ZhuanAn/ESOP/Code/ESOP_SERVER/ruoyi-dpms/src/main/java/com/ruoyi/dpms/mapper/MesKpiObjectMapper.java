package com.ruoyi.dpms.mapper;

import com.ruoyi.dpms.domain.MesKpiObject;

import java.util.List;

public interface MesKpiObjectMapper {
    /**
     * 通过Assign表的ID查询四个字段userAccount、userName、deptId、deptName
     *
     * @param
     * @return
     */

    public List<MesKpiObject> selectMesKpiObjectById(Long id);
}
