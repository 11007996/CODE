package com.ruoyi.dpms.mapper;

import com.ruoyi.dpms.domain.MesKpiAssign;
import org.apache.ibatis.annotations.Param;

import java.util.List;

/**
 * KPI指派Mapper接口
 * 
 * @author ruoyi
 * @date 2022-11-10
 */
public interface MesKpiAssignMapper
{
    /**
     * 查询KPI指派
     * 
     * @param id KPI指派主键
     * @return KPI指派
     */
    public MesKpiAssign selectMesKpiAssignById(Long id);


    /**
     * 查询KPI指派列表
     *
     * @param mesKpiAssign KPI指派
     * @return KPI指派集合
     */
    public List<MesKpiAssign> selectMesKpiAssignList(MesKpiAssign mesKpiAssign);



    /**
     * 新增KPI指派
     * 
     * @param mesKpiAssign KPI指派
     * @return 结果
     */
    public int insertMesKpiAssign(MesKpiAssign mesKpiAssign);
    public long selectMesKpiAssignId(MesKpiAssign mesKpiAssign);


    /**
     * 更新index表的status为可修改
     *
     * @param
     * @return
     */
    public int updateMesKpiStatus(Long id);

    /**
     * 更新Detail表的status为可填报
     *
     * @param
     * @return
     */
    public int updateMesKpiDetailStatus(Long id);

    /**
     * 修改KPI指派
     * 
     * @param mesKpiAssign KPI指派
     * @return 结果
     */
    public int updateMesKpiAssign(MesKpiAssign mesKpiAssign);

    /**
     * 删除KPI指派
     * 
     * @param id KPI指派主键
     * @return 结果
     */
    public int deleteMesKpiAssignById(Long id);

    /**
     * 批量删除KPI指派
     * 
     * @param ids 需要删除的数据主键集合
     * @return 结果
     */
    public int deleteMesKpiAssignByIds(Long[] ids);


    /**
     *
     * 根据传入的kpiYear值查询是否在assign表已经存在该值
     * @return
     */
    public Long selectKpiYear(@Param("kpiYear") Long kpiYear);

    /**
     *
     * 根据登录用户的部门id和年份查询部门的权重占比之和
     * @return
     */
    public Float selectWeightCoefficientSumByItems(@Param("deptId") Long deptId, @Param("kpiYear") Long kpiYear);
}
