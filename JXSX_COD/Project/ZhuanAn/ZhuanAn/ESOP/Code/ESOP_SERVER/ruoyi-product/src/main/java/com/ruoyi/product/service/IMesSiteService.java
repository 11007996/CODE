package com.ruoyi.product.service;

import com.ruoyi.product.domain.MesSite;

import java.util.List;

/**
 * 厂区管理Service接口
 *
 * @author ruoyi
 * @date 2022-09-20
 */
public interface IMesSiteService
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
     * 校验机种名称是否唯一
     *
     * @param mesSite 厂区信息
     * @return 结果
     */
    public String checkSiteNameUnique(MesSite mesSite);

    /**
     * 校验厂区是否允许操作
     *
     * @param mesSite 厂区
     */
    public void checkSiteAllowed(MesSite mesSite);

    /**
     * 校验厂区是否有数据权限
     *
     * @param siteId 厂区id
     */
    public void checkSiteDataScope(Long siteId);



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
     * 修改厂区状态
     *
     * @param mesSite 机厂区信息
     * @return 结果
     */
    public int updateSiteStatus(MesSite mesSite);

    /**
     * 批量删除厂区管理
     *
     * @param siteIds 需要删除的厂区管理主键集合
     * @return 结果
     */
    public int deleteMesSiteBySiteIds(Long[] siteIds);

    /**
     * 删除厂区管理信息
     *
     * @param siteId 厂区管理主键
     * @return 结果
     */
    public int deleteMesSiteBySiteId(Long siteId);
}
