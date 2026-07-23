package com.ruoyi.dpms.service;

import com.ruoyi.common.core.domain.AjaxResult;
import com.ruoyi.dpms.domain.MesKpiAssign;

import java.util.List;

/**
 * KPI指派Service接口
 * 
 * @author ruoyi
 * @date 2022-11-10
 */
public interface IMesKpiAssignService 
{
    /**
     * 通过ID查询KPI指派
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


    public AjaxResult insertMesKpiAssign(String mesKpiAssign);


//    @Override
//    public int insertMesKpiAssign(MesKpiAssign mesKpiAssign)
//    {
//        mesKpiAssign.setCreateTime(DateUtils.getNowDate());
//        return mesKpiAssignMapper.insertMesKpiAssign(mesKpiAssign);
//    }


    /**
     * 修改KPI指派
     * 
     * @param mesKpiAssign KPI指派
     * @return 结果
     */
    public AjaxResult updateMesKpiAssign(String mesKpiAssign);


    /**
     * 批量删除KPI指派
     * 
     * @param ids 需要删除的KPI指派主键集合
     * @return 结果
     */
    public int deleteMesKpiAssignByIds(Long[] ids);

    /**
     * 删除KPI指派信息
     * 
     * @param id KPI指派主键
     * @return 结果
     */
    public int deleteMesKpiAssignById(Long id);

    //    @Override
//    public int insertMesKpiAssign(MesKpiAssign mesKpiAssign)
//    {
//        mesKpiAssign.setCreateTime(DateUtils.getNowDate());
//        return mesKpiAssignMapper.insertMesKpiAssign(mesKpiAssign);
//    }


}
