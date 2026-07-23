package com.ruoyi.product.mapper;

import java.util.List;
import com.ruoyi.system.domain.MesUserRelateLine;

/**
 * 会签人员、线体关系Mapper接口
 *
 * @author ruoyi
 * @date 2023-10-20
 */
public interface MesUserRelateLineMapper
{
    /**
     * 查询会签人员、线体关系
     *
     * @param lineId 会签人员、线体关系主键
     * @return 会签人员、线体关系
     */
    public MesUserRelateLine selectMesUserRelateLineByLineId(Long lineId);

    /**
     * 查询会签人员、线体关系列表
     *
     * @param mesUserRelateLine 会签人员、线体关系
     * @return 会签人员、线体关系集合
     */
    public List<MesUserRelateLine> selectMesUserRelateLineList(MesUserRelateLine mesUserRelateLine);

    /**
     * 新增会签人员、线体关系
     *
     * @param mesUserRelateLine 会签人员、线体关系
     * @return 结果
     */
    public int insertMesUserRelateLine(MesUserRelateLine mesUserRelateLine);

    /**
     * 修改会签人员、线体关系
     *
     * @param mesUserRelateLine 会签人员、线体关系
     * @return 结果
     */
    public int updateMesUserRelateLine(MesUserRelateLine mesUserRelateLine);

    /**
     * 删除会签人员、线体关系
     *
     * @param lineId 会签人员、线体关系主键
     * @return 结果
     */
    public int deleteMesUserRelateLineByLineId(Long lineId);

    /**
     * 批量删除会签人员、线体关系
     *
     * @param lineIds 需要删除的数据主键集合
     * @return 结果
     */
    public int deleteMesUserRelateLineByLineIds(Long[] lineIds);
}
