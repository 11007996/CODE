package com.ruoyi.product.service;

import com.ruoyi.common.core.domain.AjaxResult;
import com.ruoyi.product.domain.MesSopGroup;

import java.io.IOException;
import java.util.List;
import java.util.Map;

/**
 * sop群组Service接口
 *
 * @author ruoyi
 * @date 2022-12-26
 */
public interface IMesSopGroupService
{
    /**
     * 查询sop群组
     *
     * @param sopGroupId sop群组主键
     * @return sop群组
     */
    public List<MesSopGroup> selectMesSopGroupBySopGroupId(String sopGroupId);

    /**
     * 查询sop群组列表
     *
     * @param mesSopGroup sop群组
     * @return sop群组集合
     */
    public List<MesSopGroup> selectMesSopGroupList(MesSopGroup mesSopGroup);

    /**
     * 新增sop群组
     *
     * @param mesSopGroup sop群组
     * @return 结果
     */
    public int insertMesSopGroup(MesSopGroup mesSopGroup);

    /**
     * 修改sop群组
     *
     * @param mesSopGroup sop群组
     * @return 结果
     */
    public int updateMesSopGroup(MesSopGroup mesSopGroup);

    /**
     * 批量删除sop群组
     *
     * @param sopGroupIds 需要删除的sop群组主键集合
     * @return 结果
     */
    public int deleteMesSopGroupBySopGroupIds(Long[] sopGroupIds);

    /**
     * 删除sop群组信息
     *
     * @param sopGroupId sop群组主键
     * @return 结果
     */
    public int deleteMesSopGroupBySopGroupId(Long sopGroupId,String type);

    public Long selectMesSopGroupTypeNum(MesSopGroup mesSopGroup);

    AjaxResult updateSignedSopList(Long sopId, Long materialId) throws IOException;
    List<Map<String,Float>> pdfSize(String filePath, String passWord);
    List<Map<String,Float>> pdfSize1(String filePath);
}
