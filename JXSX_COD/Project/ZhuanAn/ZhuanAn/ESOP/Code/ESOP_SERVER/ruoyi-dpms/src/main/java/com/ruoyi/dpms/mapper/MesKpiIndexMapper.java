package com.ruoyi.dpms.mapper;

import com.ruoyi.dpms.domain.MesKpiIndex;

import java.util.List;

/**
 * KPI指标Mapper接口
 * 
 * @author ruoyi
 * @date 2022-11-10
 */
public interface MesKpiIndexMapper 
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

//    public  List<MesKpiIndex> selectMesKpiIndexListBystatus(Long status);




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
     * 删除KPI指标
     * 
     * @param id KPI指标主键
     * @return 结果
     */
    public int deleteMesKpiIndexById(Long id);

    /**
     * 批量删除KPI指标
     * 
     * @param ids 需要删除的数据主键集合
     * @return 结果
     */
    public int deleteMesKpiIndexByIds(Long[] ids);
}
