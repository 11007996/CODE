package com.ruoyi.product.mapper;

import java.util.List;
import com.ruoyi.product.domain.MesSeal;

/**
 * 印章Mapper接口
 *
 * @author ruoyi
 * @date 2024-04-18
 */
public interface MesSealMapper
{
    /**
     * 查询印章
     *
     * @param sealId 印章主键
     * @return 印章
     */
    public MesSeal selectMesSealBySealId(Long sealId);

    /**
     * 查询印章列表
     *
     * @param mesSeal 印章
     * @return 印章集合
     */
    public List<MesSeal> selectMesSealList(MesSeal mesSeal);

    /**
     * 新增印章
     *
     * @param mesSeal 印章
     * @return 结果
     */
    public int insertMesSeal(MesSeal mesSeal);

    /**
     * 修改印章
     *
     * @param mesSeal 印章
     * @return 结果
     */
    public int updateMesSeal(MesSeal mesSeal);

    /**
     * 删除印章
     *
     * @param sealId 印章主键
     * @return 结果
     */
    public int deleteMesSealBySealId(Long sealId);

    /**
     * 批量删除印章
     *
     * @param sealIds 需要删除的数据主键集合
     * @return 结果
     */
    public int deleteMesSealBySealIds(Long[] sealIds);
}
