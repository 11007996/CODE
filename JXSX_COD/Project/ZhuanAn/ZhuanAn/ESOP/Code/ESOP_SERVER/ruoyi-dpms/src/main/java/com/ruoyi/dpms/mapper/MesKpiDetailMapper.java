package com.ruoyi.dpms.mapper;

import com.ruoyi.dpms.domain.MesKpiDetail;

import java.util.List;

public interface MesKpiDetailMapper{
    public int insertMesKpiDetail(MesKpiDetail MmesKpiDetail);

    public List<MesKpiDetail> selectMesKpiDetailList(MesKpiDetail mesKpiDetail);

    public int  updateMesKpiDetail(MesKpiDetail mesKpiDetail);

    public int deleteMesKpiDetailByAssignId(Long mkdAssignId);

    public int deleteMesKpiDetailById(Long id);
    




}
