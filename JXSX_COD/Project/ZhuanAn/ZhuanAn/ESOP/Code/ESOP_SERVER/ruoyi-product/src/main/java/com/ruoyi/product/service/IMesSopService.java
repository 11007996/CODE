package com.ruoyi.product.service;

import com.ruoyi.product.domain.MesSop;

import java.util.List;

/**
 * sopService接口
 *
 * @author ruoyi
 * @date 2022-09-23
 */
public interface IMesSopService
{
    /**
     * 查询sop
     *
     * @param sopId sop主键
     * @return sop
     */
    public MesSop selectMesSopBySopId(Long sopId);

    /**
     * 查询sop列表
     *
     * @param mesSop sop
     * @return sop集合
     */
    public List<MesSop> selectMesSopList(MesSop mesSop);

    /**
     * 新增sop
     *
     * @param mesSop sop
     * @return 结果
     */
    public int insertMesSop(MesSop mesSop);

    /**
     * 修改sop
     *
     * @param mesSop sop
     * @return 结果
     */
    public int updateMesSop(MesSop mesSop);

    /**
     * 批量删除sop
     *
     * @param sopIds 需要删除的sop主键集合
     * @return 结果
     */
    public int deleteMesSopBySopIds(Long[] sopIds);

    /**
     * 删除sop信息
     *
     * @param sopId sop主键
     * @return 结果
     */
    public int deleteMesSopBySopId(Long sopId);

    public MesSop selectSopIdByName(String sopName);


}
