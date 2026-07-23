package com.ruoyi.replace.mapper;

import com.ruoyi.replace.domain.MesMaterialRelateLine;

import java.util.List;

/**
 * 线料关系Mapper接口
 *
 * @author ruoyi
 * @date 2022-12-24
 */
public interface MesMaterialRelateLineMapper
{
    /**
     * 查询线料关系
     *
     * @param id 线料关系主键
     * @return 线料关系
     */
    public MesMaterialRelateLine selectMesMaterialRelateLineById(Long id);

    /**
     * 查询线料关系列表
     *
     * @param mesMaterialRelateLine 线料关系
     * @return 线料关系集合
     */
    public List<MesMaterialRelateLine> selectMesMaterialRelateLineList(MesMaterialRelateLine mesMaterialRelateLine);

    /**
     * 新增线料关系
     *
     * @param mesMaterialRelateLine 线料关系
     * @return 结果
     */
    public int insertMesMaterialRelateLine(MesMaterialRelateLine mesMaterialRelateLine);

    /**
     * 修改线料关系
     *
     * @param mesMaterialRelateLine 线料关系
     * @return 结果
     */
    public int updateMesMaterialRelateLine(MesMaterialRelateLine mesMaterialRelateLine);

    /**
     * 删除线料关系
     *
     * @param id 线料关系主键
     * @return 结果
     */
    public int deleteMesMaterialRelateLineById(Long id);
    public int deleteMesMaterialRelateLineByMId(Long id);

    /**
     * 批量删除线料关系
     *
     * @param ids 需要删除的数据主键集合
     * @return 结果
     */
    public int deleteMesMaterialRelateLineByIds(Long[] ids);

    public List<MesMaterialRelateLine> selectLineByMaterial(Long materialId);
}
