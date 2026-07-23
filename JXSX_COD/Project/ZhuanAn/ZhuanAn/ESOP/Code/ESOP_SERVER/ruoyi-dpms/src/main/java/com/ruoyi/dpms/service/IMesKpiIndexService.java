package com.ruoyi.dpms.service;

import com.ruoyi.dpms.domain.MesKpiIndex;

import java.util.List;

/**
 * KPI指标Service接口
 * 
 * @author ruoyi
 * @date 2022-11-10
 */
public interface IMesKpiIndexService 
{
    /**
     * 查询KPI指标
     * 
     * @param id KPI指标主键
     * @return KPI指标
     */
    public MesKpiIndex selectMesKpiIndexById(Long id);

    /**
     * 查询KPI指标列表
     * 
     * @param mesKpiIndex KPI指标
     * @return KPI指标集合
     */
    public List<MesKpiIndex> selectMesKpiIndexList(MesKpiIndex mesKpiIndex);

    /**
     * 新增KPI指标
     * 
     * @param mesKpiIndex KPI指标
     * @return 结果
     */
    public int insertMesKpiIndex(MesKpiIndex mesKpiIndex);

    /**
     * 修改KPI指标
     * 
     * @param mesKpiIndex KPI指标
     * @return 结果
     */
    public int updateMesKpiIndex(MesKpiIndex mesKpiIndex);

    /**
     * 批量删除KPI指标
     * 
     * @param ids 需要删除的KPI指标主键集合
     * @return 结果
     */
    public int deleteMesKpiIndexByIds(Long[] ids);

    /**
     * 删除KPI指标信息
     * 
     * @param id KPI指标主键
     * @return 结果
     */
    public int deleteMesKpiIndexById(Long id);
}
