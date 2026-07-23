package com.ruoyi.dpms.service.impl;

import com.ruoyi.common.utils.DateUtils;
import com.ruoyi.dpms.domain.MesKpiIndex;
import com.ruoyi.dpms.mapper.MesKpiIndexMapper;
import com.ruoyi.dpms.service.IMesKpiIndexService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;

/**
 * KPI指标Service业务层处理
 * 
 * @author ruoyi
 * @date 2022-11-10
 */
@Service
public class MesKpiIndexServiceImpl implements IMesKpiIndexService 
{
    @Autowired
    private MesKpiIndexMapper mesKpiIndexMapper;

    /**
     * 查询KPI指标
     * 
     * @param id KPI指标主键
     * @return KPI指标
     */
    @Override
    public MesKpiIndex selectMesKpiIndexById(Long id)
    {
        return mesKpiIndexMapper.selectMesKpiIndexById(id);
    }

    /**
     * 查询KPI指标列表
     * 
     * @param mesKpiIndex KPI指标
     * @return KPI指标
     */
    @Override
    public List<MesKpiIndex> selectMesKpiIndexList(MesKpiIndex mesKpiIndex)
    {
//        switch(mesKpiIndex.getStatus()) {
//            case 0 :  mesKpiIndexMapper.selectMesKpiIndexListBystatus0(status);
//            case 1 :  mesKpiIndexMapper.selectMesKpiIndexListBystatus1(status);
//        }
//        if(mesKpiIndex.getStatus() == 0){
//            return mesKpiIndexMapper.selectMesKpiIndexListBystatus(mesKpiIndex.getStatus());
//        }
//        else
            return mesKpiIndexMapper.selectMesKpiIndexList(mesKpiIndex);
    }

    /**
     * 新增KPI指标
     * 
     * @param mesKpiIndex KPI指标
     * @return 结果
     */
    @Override
    public int insertMesKpiIndex(MesKpiIndex mesKpiIndex)
    {
        mesKpiIndex.setCreateTime(DateUtils.getNowDate());
        mesKpiIndex.setStatus("0");
        return mesKpiIndexMapper.insertMesKpiIndex(mesKpiIndex);
    }

    /**
     * 修改KPI指标
     * 
     * @param mesKpiIndex KPI指标
     * @return 结果
     */
    @Override
    public int updateMesKpiIndex(MesKpiIndex mesKpiIndex)
    {
        mesKpiIndex.setUpdateTime(DateUtils.getNowDate());
        return mesKpiIndexMapper.updateMesKpiIndex(mesKpiIndex);
    }

    /**
     * 批量删除KPI指标
     * 
     * @param ids 需要删除的KPI指标主键
     * @return 结果
     */
    @Override
    public int deleteMesKpiIndexByIds(Long[] ids)
    {
        return mesKpiIndexMapper.deleteMesKpiIndexByIds(ids);
    }

    /**
     * 删除KPI指标信息
     * 
     * @param id KPI指标主键
     * @return 结果
     */
    @Override
    public int deleteMesKpiIndexById(Long id)
    {
        return mesKpiIndexMapper.deleteMesKpiIndexById(id);
    }
}
