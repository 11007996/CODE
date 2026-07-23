package com.ruoyi.replace.service.Impl;

import com.ruoyi.common.utils.DateUtils;
import com.ruoyi.replace.domain.MesMaterialRelateLine;
import com.ruoyi.replace.mapper.MesMaterialRelateLineMapper;
import com.ruoyi.replace.service.IMesMaterialRelateLineService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;

/**
 * 线料关系Service业务层处理
 *
 * @author ruoyi
 * @date 2022-12-24
 */
@Service
public class MesMaterialRelateLineServiceImpl implements IMesMaterialRelateLineService
{
    @Autowired
    private MesMaterialRelateLineMapper mesMaterialRelateLineMapper;

    /**
     * 查询线料关系
     *
     * @param id 线料关系主键
     * @return 线料关系
     */
    @Override
    public MesMaterialRelateLine selectMesMaterialRelateLineById(Long id)
    {
        return mesMaterialRelateLineMapper.selectMesMaterialRelateLineById(id);
    }

    /**
     * 查询线料关系列表
     *
     * @param mesMaterialRelateLine 线料关系
     * @return 线料关系
     */
    @Override
    public List<MesMaterialRelateLine> selectMesMaterialRelateLineList(MesMaterialRelateLine mesMaterialRelateLine)
    {
        return mesMaterialRelateLineMapper.selectMesMaterialRelateLineList(mesMaterialRelateLine);
    }

    /**
     * 新增线料关系
     *
     * @param mesMaterialRelateLine 线料关系
     * @return 结果
     */
    @Override
    public int insertMesMaterialRelateLine(MesMaterialRelateLine mesMaterialRelateLine)
    {
        mesMaterialRelateLine.setCreateTime(DateUtils.getNowDate());
        return mesMaterialRelateLineMapper.insertMesMaterialRelateLine(mesMaterialRelateLine);
    }

    /**
     * 修改料号、线体关系
     *
     * @param mesMaterialRelateLine 料号、线体关系
     * @return 结果
     */
    @Override
    public int updateMesMaterialRelateLine(MesMaterialRelateLine mesMaterialRelateLine)
    {
        mesMaterialRelateLine.setUpdateTime(DateUtils.getNowDate());
        return mesMaterialRelateLineMapper.updateMesMaterialRelateLine(mesMaterialRelateLine);
    }


    /**
     * 批量删除线料关系
     *
     * @param ids 需要删除的线料关系主键
     * @return 结果
     */
    @Override
    public int deleteMesMaterialRelateLineByIds(Long[] ids)
    {
        return mesMaterialRelateLineMapper.deleteMesMaterialRelateLineByIds(ids);
    }

    /**
     * 删除线料关系信息
     *
     * @param id 线料关系主键
     * @return 结果
     */
    @Override
    public int deleteMesMaterialRelateLineById(Long id)
    {
        return mesMaterialRelateLineMapper.deleteMesMaterialRelateLineById(id);
    }
    @Override
    public int deleteMesMaterialRelateLineByMId(Long id)
    {
        return mesMaterialRelateLineMapper.deleteMesMaterialRelateLineByMId(id);
    }


    /**
     * 根据料号查询线别（线别n：料号1）
     *
     * @return 结果
     */
    @Override
    public List<MesMaterialRelateLine> selectLineByMaterial(Long materialId)
    {
        return mesMaterialRelateLineMapper.selectLineByMaterial(materialId);
    }


}
