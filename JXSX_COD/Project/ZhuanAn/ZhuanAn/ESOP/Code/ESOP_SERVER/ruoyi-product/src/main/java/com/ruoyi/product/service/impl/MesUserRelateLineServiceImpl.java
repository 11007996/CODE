package com.ruoyi.product.service.impl;

import java.util.List;

import com.ruoyi.product.mapper.MesUserRelateLineMapper;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import com.ruoyi.product.mapper.MesUserRelateLineMapper;
import com.ruoyi.system.domain.MesUserRelateLine;
import com.ruoyi.system.service.IMesUserRelateLineService;

/**
 * 会签人员、线体关系Service业务层处理
 *
 * @author ruoyi
 * @date 2023-10-20
 */
@Service
public class MesUserRelateLineServiceImpl implements IMesUserRelateLineService
{
    @Autowired
    private MesUserRelateLineMapper mesUserRelateLineMapper;

    /**
     * 查询会签人员、线体关系
     *
     * @param lineId 会签人员、线体关系主键
     * @return 会签人员、线体关系
     */
    @Override
    public MesUserRelateLine selectMesUserRelateLineByLineId(Long lineId)
    {
        return mesUserRelateLineMapper.selectMesUserRelateLineByLineId(lineId);
    }

    /**
     * 查询会签人员、线体关系列表
     *
     * @param mesUserRelateLine 会签人员、线体关系
     * @return 会签人员、线体关系
     */
    @Override
    public List<MesUserRelateLine> selectMesUserRelateLineList(MesUserRelateLine mesUserRelateLine)
    {
        return mesUserRelateLineMapper.selectMesUserRelateLineList(mesUserRelateLine);
    }

    /**
     * 新增会签人员、线体关系
     *
     * @param mesUserRelateLine 会签人员、线体关系
     * @return 结果
     */
    @Override
    public int insertMesUserRelateLine(MesUserRelateLine mesUserRelateLine)
    {
        return mesUserRelateLineMapper.insertMesUserRelateLine(mesUserRelateLine);
    }

    /**
     * 修改会签人员、线体关系
     *
     * @param mesUserRelateLine 会签人员、线体关系
     * @return 结果
     */
    @Override
    public int updateMesUserRelateLine(MesUserRelateLine mesUserRelateLine)
    {
        return mesUserRelateLineMapper.updateMesUserRelateLine(mesUserRelateLine);
    }

    /**
     * 批量删除会签人员、线体关系
     *
     * @param lineIds 需要删除的会签人员、线体关系主键
     * @return 结果
     */
    @Override
    public int deleteMesUserRelateLineByLineIds(Long[] lineIds)
    {
        return mesUserRelateLineMapper.deleteMesUserRelateLineByLineIds(lineIds);
    }

    /**
     * 删除会签人员、线体关系信息
     *
     * @param lineId 会签人员、线体关系主键
     * @return 结果
     */
    @Override
    public int deleteMesUserRelateLineByLineId(Long lineId)
    {
        return mesUserRelateLineMapper.deleteMesUserRelateLineByLineId(lineId);
    }
}
