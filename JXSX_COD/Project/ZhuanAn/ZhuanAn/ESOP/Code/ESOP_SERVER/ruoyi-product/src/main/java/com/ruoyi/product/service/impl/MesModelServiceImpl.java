package com.ruoyi.product.service.impl;

import com.ruoyi.common.constant.UserConstants;
import com.ruoyi.common.core.domain.entity.SysUser;
import com.ruoyi.common.exception.ServiceException;
import com.ruoyi.common.utils.DateUtils;
import com.ruoyi.common.utils.SecurityUtils;
import com.ruoyi.common.utils.StringUtils;
import com.ruoyi.common.utils.spring.SpringUtils;
import com.ruoyi.product.domain.MesModel;
import com.ruoyi.product.mapper.MesModelMapper;
import com.ruoyi.product.service.IMesModelService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;

/**
 * 机种信息Service业务层处理
 *
 * @author ruoyi
 * @date 2022-09-14
 */
@Service
public class MesModelServiceImpl implements IMesModelService
{
    @Autowired
    private MesModelMapper mesModelMapper;

    /**
     * 查询机种信息
     *
     * @param modelId 机种信息主键
     * @return 机种信息
     */
    @Override
    public MesModel selectMesModelByModelId(Long modelId)
    {
        return mesModelMapper.selectMesModelByModelId(modelId);
    }

    /**
     * 查询机种信息列表
     *
     * @param mesModel 机种信息
     * @return 机种信息
     */
    @Override
    public List<MesModel> selectMesModelList(MesModel mesModel)
    {
        return mesModelMapper.selectMesModelList(mesModel);
    }

    @Override
    public String checkModelNameUnique(MesModel model) {
        Long modelId = StringUtils.isNull(model.getModelId()) ? -1L : model.getModelId();
        MesModel info = mesModelMapper.checkModelNameUnique(model.getModelName());
        if (StringUtils.isNotNull(info) && info.getModelId().longValue() != modelId.longValue())
        {
            return UserConstants.NOT_UNIQUE;
        }
        return UserConstants.UNIQUE;
    }

    @Override
    public void checkModelAllowed(MesModel mesModel) {
        if (StringUtils.isNotNull(mesModel.getModelId()) && mesModel.isAdmin())
        {
            throw new ServiceException("不允许操作超级管理员角色");
        }
    }

    @Override
    public void checkModelDataScope(Long modelId) {
        if (!SysUser.isAdmin(SecurityUtils.getUserId()))
        {
            MesModel model = new MesModel();
            model.setModelId(modelId);
            List<MesModel> models = SpringUtils.getAopProxy(this).selectMesModelList(model);
            if (StringUtils.isEmpty(models))
            {
                throw new ServiceException("没有权限访问角色数据！");
            }
        }
    }

    /**
     * 新增机种信息
     *
     * @param mesModel 机种信息
     * @return 结果
     */
    @Override
    public int insertMesModel(MesModel mesModel)
    {
        mesModel.setCreateTime(DateUtils.getNowDate());
        return mesModelMapper.insertMesModel(mesModel);
    }

    /**
     * 修改机种信息
     *
     * @param mesModel 机种信息
     * @return 结果
     */
    @Override
    public int updateMesModel(MesModel mesModel)
    {
        mesModel.setUpdateTime(DateUtils.getNowDate());
        return mesModelMapper.updateMesModel(mesModel);
    }

    @Override
    public int updateModelStatus(MesModel model) {
        return mesModelMapper.updateMesModel(model);
    }

    /**
     * 批量删除机种信息
     *
     * @param modelIds 需要删除的机种信息主键
     * @return 结果
     */
    @Override
    public int deleteMesModelByModelIds(Long[] modelIds)
    {
        return mesModelMapper.deleteMesModelByModelIds(modelIds);
    }

    /**
     * 删除机种信息信息
     *
     * @param modelId 机种信息主键
     * @return 结果
     */
    @Override
    public int deleteMesModelByModelId(Long modelId)
    {
        return mesModelMapper.deleteMesModelByModelId(modelId);
    }
    @Override
    public MesModel modelInfoByName(String modelName) {
        return mesModelMapper.modelInfoByName(modelName);
    }
}
