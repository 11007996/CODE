package com.ruoyi.product.service.impl;

import com.ruoyi.common.constant.UserConstants;
import com.ruoyi.common.core.domain.entity.SysUser;
import com.ruoyi.common.exception.ServiceException;
import com.ruoyi.common.utils.DateUtils;
import com.ruoyi.common.utils.SecurityUtils;
import com.ruoyi.common.utils.StringUtils;
import com.ruoyi.common.utils.spring.SpringUtils;
import com.ruoyi.product.domain.MesSite;
import com.ruoyi.product.mapper.MesSiteMapper;
import com.ruoyi.product.service.IMesSiteService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;

/**
 * 厂区管理Service业务层处理
 *
 * @author ruoyi
 * @date 2022-09-20
 */
@Service
public class MesSiteServiceImpl implements IMesSiteService
{
    @Autowired
    private MesSiteMapper mesSiteMapper;

    /**
     * 查询厂区管理
     *
     * @param siteId 厂区管理主键
     * @return 厂区管理
     */
    @Override
    public MesSite selectMesSiteBySiteId(Long siteId)
    {
        return mesSiteMapper.selectMesSiteBySiteId(siteId);
    }

    /**
     * 查询厂区管理列表
     *
     * @param mesSite 厂区管理
     * @return 厂区管理
     */
    @Override
    public List<MesSite> selectMesSiteList(MesSite mesSite)
    {
        return mesSiteMapper.selectMesSiteList(mesSite);
    }

    @Override
    public String checkSiteNameUnique(MesSite mesSite) {
        Long siteId = StringUtils.isNull(mesSite.getSiteId()) ? -1L : mesSite.getSiteId();
        MesSite info = mesSiteMapper.checkSiteNameUnique(mesSite.getSiteName());
        if (StringUtils.isNotNull(info) && info.getSiteId().longValue() != siteId.longValue())
        {
            return UserConstants.NOT_UNIQUE;
        }
        return UserConstants.UNIQUE;
    }

    @Override
    public void checkSiteAllowed(MesSite mesSite) {
        if (StringUtils.isNotNull(mesSite.getSiteId()) && mesSite.isAdmin())
        {
            throw new ServiceException("不允许操作超级管理员角色");
        }
    }

    @Override
    public void checkSiteDataScope(Long siteId) {
        if (!SysUser.isAdmin(SecurityUtils.getUserId()))
        {
            MesSite site = new MesSite();
            site.setSiteId(siteId);
            List<MesSite> sites = SpringUtils.getAopProxy(this).selectMesSiteList(site);
            if (StringUtils.isEmpty(sites))
            {
                throw new ServiceException("没有权限访问角色数据！");
            }
        }
    }

    /**
     * 新增厂区管理
     *
     * @param mesSite 厂区管理
     * @return 结果
     */
    @Override
    public int insertMesSite(MesSite mesSite)
    {
        mesSite.setCreateTime(DateUtils.getNowDate());
        return mesSiteMapper.insertMesSite(mesSite);
    }

    /**
     * 修改厂区管理
     *
     * @param mesSite 厂区管理
     * @return 结果
     */
    @Override
    public int updateMesSite(MesSite mesSite)
    {
        mesSite.setUpdateTime(DateUtils.getNowDate());
        return mesSiteMapper.updateMesSite(mesSite);
    }

    @Override
    public int updateSiteStatus(MesSite mesSite) {
        mesSite.setUpdateTime(DateUtils.getNowDate());
        return mesSiteMapper.updateMesSite(mesSite);
    }

    /**
     * 批量删除厂区管理
     *
     * @param siteIds 需要删除的厂区管理主键
     * @return 结果
     */
    @Override
    public int deleteMesSiteBySiteIds(Long[] siteIds)
    {
        return mesSiteMapper.deleteMesSiteBySiteIds(siteIds);
    }

    /**
     * 删除厂区管理信息
     *
     * @param siteId 厂区管理主键
     * @return 结果
     */
    @Override
    public int deleteMesSiteBySiteId(Long siteId)
    {
        return mesSiteMapper.deleteMesSiteBySiteId(siteId);
    }
}
