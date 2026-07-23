package com.ruoyi.product.mapper;

import com.ruoyi.product.domain.MesSopGroup;
import org.apache.ibatis.annotations.Param;

import java.util.List;

/**
 * sop群组Mapper接口
 *
 * @author ruoyi
 * @date 2022-12-26
 */
public interface MesSopGroupMapper
{
    /**
     * 查询sop群组
     *
     * @param sopGroupId sop群组主键
     * @return sop群组
     */
    public  List<MesSopGroup> selectMesSopGroupBySopGroupId(String sopGroupId);

    /**
     * 查询sop群组列表
     *
     * @param mesSopGroup sop群组
     * @return sop群组集合
     */
    public List<MesSopGroup> selectMesSopGroupList(MesSopGroup mesSopGroup);


    public int updateMesSopGroup(MesSopGroup mesSopGroup);

    /**
     * 新增sop群组
     *
     * @param mesSopGroup sop群组
     * @return 结果
     */
    public int insertMesSopGroup(MesSopGroup mesSopGroup);


    /**
     * 删除sop群组
     *
     * @param sopGroupId sop群组主键
     * @return 结果
     */
    public int deleteMesSopGroupBySopGroupId(@Param("sopGroupId")Long sopGroupId,@Param("type") String type);

    /**
     * 批量删除sop群组
     *
     * @param sopGroupIds 需要删除的数据主键集合
     * @return 结果
     */
    public int deleteMesSopGroupBySopGroupIds(Long[] sopGroupIds);

    public Long selectMesSopGroupTypeNum(MesSopGroup mesSopGroup);
}
