package com.ruoyi.product.mapper;

import java.util.List;
import com.ruoyi.product.domain.MesSop;

/**
 * sopMapper接口
 *
 * @author ruoyi
 * @date 2022-09-23
 */
public interface MesSopMapper
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
     * 删除sop
     *
     * @param sopId sop主键
     * @return 结果
     */
    public int deleteMesSopBySopId(Long sopId);

    /**
     * 批量删除sop
     *
     * @param sopIds 需要删除的数据主键集合
     * @return 结果
     */
    public int deleteMesSopBySopIds(Long[] sopIds);

    public MesSop selectSopIdByName(String sopName);
}
