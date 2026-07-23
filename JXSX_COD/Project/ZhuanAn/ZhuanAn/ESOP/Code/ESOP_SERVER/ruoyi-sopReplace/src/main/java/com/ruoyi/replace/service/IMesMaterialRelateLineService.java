package com.ruoyi.replace.service;

import com.ruoyi.replace.domain.MesMaterialRelateLine;

import java.util.List;

/**
 * 线料关系Service接口
 *
 * @author ruoyi
 * @date 2022-12-24
 */
public interface IMesMaterialRelateLineService
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
     * 修改料号、线体关系
     *
     * @param mesMaterialRelateLine 料号、线体关系
     * @return 结果
     */
    public int updateMesMaterialRelateLine(MesMaterialRelateLine mesMaterialRelateLine);

    /**
     * 批量删除线料关系
     *
     * @param ids 需要删除的线料关系主键集合
     * @return 结果
     */
    public int deleteMesMaterialRelateLineByIds(Long[] ids);

    /**
     * 删除线料关系信息
     *
     * @param id 线料关系主键
     * @return 结果
     */
    public int deleteMesMaterialRelateLineById(Long id);
    public int deleteMesMaterialRelateLineByMId(Long id);

    /**
     * 根据料号查询线体
     *
     */
    public List<MesMaterialRelateLine> selectLineByMaterial(Long materialId);



}
