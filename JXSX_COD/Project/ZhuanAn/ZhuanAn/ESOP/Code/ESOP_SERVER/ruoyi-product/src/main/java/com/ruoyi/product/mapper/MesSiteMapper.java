package com.ruoyi.product.mapper;

import com.ruoyi.product.domain.MesSite;

import java.util.List;

/**
 * 厂区管理Mapper接口
 *
 * @author ruoyi
 * @date 2022-09-20
 */
public interface MesSiteMapper
{
    /**
     * 查询厂区管理
     *
     * @param siteId 厂区管理主键
     * @return 厂区管理
     */
    public MesSite selectMesSiteBySiteId(Long siteId);

    /**
     * 查询厂区管理列表
     *
     * @param mesSite 厂区管理
     * @return 厂区管理集合
     */
    public List<MesSite> selectMesSiteList(MesSite mesSite);

    /**
     * 校验厂区名称是否唯一
     *
     * @param siteName 厂区名称
     * @return 厂区信息
     */
    public MesSite checkSiteNameUnique(String siteName);



    /**
     * 新增厂区管理
     *
     * @param mesSite 厂区管理
     * @return 结果
     */
    public int insertMesSite(MesSite mesSite);

    /**
     * 修改厂区管理
     *
     * @param mesSite 厂区管理
     * @return 结果
     */
    public int updateMesSite(MesSite mesSite);

    /**
     * 删除厂区管理
     *
     * @param siteId 厂区管理主键
     * @return 结果
     */
    public int deleteMesSiteBySiteId(Long siteId);

    /**
     * 批量删除厂区管理
     *
     * @param siteIds 需要删除的数据主键集合
     * @return 结果
     */
    public int deleteMesSiteBySiteIds(Long[] siteIds);
}
