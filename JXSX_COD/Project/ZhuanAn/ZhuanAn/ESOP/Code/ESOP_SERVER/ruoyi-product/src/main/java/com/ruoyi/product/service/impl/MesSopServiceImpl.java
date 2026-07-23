package com.ruoyi.product.service.impl;

import com.ruoyi.common.utils.DateUtils;
import com.ruoyi.product.domain.MesSop;
import com.ruoyi.product.mapper.MesSopMapper;
import com.ruoyi.product.service.IMesSopService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;

import static com.ruoyi.common.utils.SecurityUtils.getUsername;

/**
 * sopService业务层处理
 *
 * @author ruoyi
 * @date 2022-09-23
 */
@Service
public class MesSopServiceImpl implements IMesSopService
{
    @Autowired
    private MesSopMapper mesSopMapper;

    /**
     * 查询sop
     *
     * @param sopId sop主键
     * @return sop
     */
    @Override
    public MesSop selectMesSopBySopId(Long sopId)
    {
        return mesSopMapper.selectMesSopBySopId(sopId);
    }

    /**
     * 查询sop列表
     *
     * @param mesSop sop
     * @return sop
     */
    @Override
    public List<MesSop> selectMesSopList(MesSop mesSop)
    {
        return mesSopMapper.selectMesSopList(mesSop);
    }

    /**
     * 新增sop
     *
     * @param mesSop sop
     * @return 结果
     */
    @Override
    public int insertMesSop(MesSop mesSop)
    {
        mesSop.setCreateTime(DateUtils.getNowDate());
        return mesSopMapper.insertMesSop(mesSop);
    }

    /**
     * 修改sop
     *
     * @param mesSop sop
     * @return 结果
     */
    @Override
    public int updateMesSop(MesSop mesSop)
    {
        mesSop.setUpdateTime(DateUtils.getNowDate());
        mesSop.setUpdateBy(getUsername());
        return mesSopMapper.updateMesSop(mesSop);
    }

    /**
     * 批量删除sop
     *
     * @param sopIds 需要删除的sop主键
     * @return 结果
     */
    @Override
    public int deleteMesSopBySopIds(Long[] sopIds)
    {
        return mesSopMapper.deleteMesSopBySopIds(sopIds);
    }

    /**
     * 删除sop信息
     *
     * @param sopId sop主键
     * @return 结果
     */
    @Override
    public int deleteMesSopBySopId(Long sopId)
    {
        return mesSopMapper.deleteMesSopBySopId(sopId);
    }

    @Override
    public MesSop selectSopIdByName(String sopName) {
        return mesSopMapper.selectSopIdByName(sopName);
    }
}
