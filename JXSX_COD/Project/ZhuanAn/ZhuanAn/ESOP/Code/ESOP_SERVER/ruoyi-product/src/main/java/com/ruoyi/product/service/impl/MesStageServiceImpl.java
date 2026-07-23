package com.ruoyi.product.service.impl;

import java.util.List;

import com.ruoyi.common.constant.UserConstants;
import com.ruoyi.common.core.domain.entity.SysUser;
import com.ruoyi.common.exception.ServiceException;
import com.ruoyi.common.utils.DateUtils;
import com.ruoyi.common.utils.SecurityUtils;
import com.ruoyi.common.utils.StringUtils;
import com.ruoyi.common.utils.spring.SpringUtils;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import com.ruoyi.product.mapper.MesStageMapper;
import com.ruoyi.product.domain.MesStage;
import com.ruoyi.product.service.IMesStageService;

/**
 * 区段管理Service业务层处理
 *
 * @author ruoyi
 * @date 2022-09-14
 */
@Service
public class MesStageServiceImpl implements IMesStageService
{
    @Autowired
    private MesStageMapper mesStageMapper;

    /**
     * 查询区段管理
     *
     * @param stageId 区段管理主键
     * @return 区段管理
     */
    @Override
    public MesStage selectMesStageByStageId(Long stageId)
    {
        return mesStageMapper.selectMesStageByStageId(stageId);
    }

    /**
     * 查询区段管理列表
     *
     * @param mesStage 区段管理
     * @return 区段管理
     */
    @Override
    public List<MesStage> selectMesStageList(MesStage mesStage)
    {
        return mesStageMapper.selectMesStageList(mesStage);
    }

    /**
     * 检查区段名称是否唯一
     *
     * @param mesStage 区段信息
     * @return
     */
    @Override
    public String checkStageNameUnique(MesStage mesStage) {
        Long stageId = StringUtils.isNull(mesStage.getStageId()) ? -1L : mesStage.getStageId();
        MesStage info = mesStageMapper.checkStageNameUnique(mesStage.getStageName());
        if (StringUtils.isNotNull(info) && info.getStageId().longValue() != stageId.longValue())
        {
            return UserConstants.NOT_UNIQUE;
        }
        return UserConstants.UNIQUE;
    }

    /**
     * 校验区段是否允许操作
     *
     * @param mesStage 区段
     */
    @Override
    public void checkStageAllowed(MesStage mesStage) {
        if(StringUtils.isNotNull(mesStage.getStageId()) && mesStage.isAdmin())
        {
            throw new ServiceException("不允许操作超级管理员角色");
        }
    }

    /**
     * 校验区段是否有数据权限
     *
     * @param stageId 区段id
     */
    @Override
    public void checkStageDataScope(Long stageId) {
        if(!SysUser.isAdmin(SecurityUtils.getUserId()))
        {
            MesStage stage = new MesStage();
            stage.setStageId(stageId);
            List<MesStage> stages = SpringUtils.getAopProxy(this).selectMesStageList(stage);
            if(StringUtils.isEmpty(stages))
            {
                throw new ServiceException("没有权限访问角色数据！");
            }
        }
    }

    /**
     * 新增区段管理
     *
     * @param mesStage 区段管理
     * @return 结果
     */
    @Override
    public int insertMesStage(MesStage mesStage)
    {
        mesStage.setCreateTime(DateUtils.getNowDate());
        return mesStageMapper.insertMesStage(mesStage);
    }

    /**
     * 修改区段管理
     *
     * @param mesStage 区段管理
     * @return 结果
     */
    @Override
    public int updateMesStage(MesStage mesStage)
    {
        mesStage.setUpdateTime(DateUtils.getNowDate());
        return mesStageMapper.updateMesStage(mesStage);
    }

    /**
     * 修改区段状态
     *
     * @param mesStage 状态信息
     * @return 结果
     */
    @Override
    public int updateStageStatus(MesStage mesStage) {
        mesStage.setUpdateTime(DateUtils.getNowDate());
        return mesStageMapper.updateMesStage(mesStage);
    }

    /**
     * 批量删除区段管理
     *
     * @param stageIds 需要删除的区段管理主键
     * @return 结果
     */
    @Override
    public int deleteMesStageByStageIds(Long[] stageIds)
    {
        return mesStageMapper.deleteMesStageByStageIds(stageIds);
    }

    /**
     * 删除区段管理信息
     *
     * @param stageId 区段管理主键
     * @return 结果
     */
    @Override
    public int deleteMesStageByStageId(Long stageId)
    {
        return mesStageMapper.deleteMesStageByStageId(stageId);
    }
    @Override
    public MesStage stageInfoByName(String stageName)
    {
        return mesStageMapper.stageInfoByName(stageName);
    }
}
