package com.ruoyi.product.service;

import java.io.IOException;
import java.util.List;

import com.ruoyi.common.core.domain.AjaxResult;
import com.ruoyi.product.domain.MesSeal;
import org.springframework.web.multipart.MultipartFile;

/**
 * 印章Service接口
 *
 * @author ruoyi
 * @date 2024-04-18
 */
public interface IMesSealService
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
    public AjaxResult addMesSeal(MesSeal mesSeal, MultipartFile multipartFile) throws IOException;

    /**
     * 修改印章
     *
     * @param mesSeal 印章
     * @return 结果
     */
    public int updateMesSeal(MesSeal mesSeal);

    /**
     * 批量删除印章
     *
     * @param sealIds 需要删除的印章主键集合
     * @return 结果
     */
    public int deleteMesSealBySealIds(Long[] sealIds);

    /**
     * 删除印章信息
     *
     * @param sealId 印章主键
     * @return 结果
     */
    public int deleteMesSealBySealId(Long sealId);
}
