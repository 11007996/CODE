package com.ruoyi.replace.service.Impl;

import com.ruoyi.replace.domain.MesMaterialInfo;
import com.ruoyi.replace.mapper.MesMaterialInfoMapper;
import com.ruoyi.replace.service.IMesMaterialInfoService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;

/**
 * 物料基础信息Service业务层处理
 *
 * @author ruoyi
 * @date 2022-12-24
 */
@Service
public class MesMaterialInfoServiceImpl implements IMesMaterialInfoService
{
    @Autowired
    private MesMaterialInfoMapper mesMaterialInfoMapper;

    /**
     * 查询物料基础信息
     *
     * @param id 物料基础信息主键
     * @return 物料基础信息
     */
    @Override
    public MesMaterialInfo selectMesMaterialInfoById(Long id)
    {
        return mesMaterialInfoMapper.selectMesMaterialInfoById(id);
    }

    /**
     * 查询物料基础信息列表
     *
     * @param mesMaterialInfo 物料基础信息
     * @return 物料基础信息
     */
    @Override
    public List<MesMaterialInfo> selectMesMaterialInfoList(MesMaterialInfo mesMaterialInfo)
    {
        return mesMaterialInfoMapper.selectMesMaterialInfoList(mesMaterialInfo);
    }

    /**
     * 新增物料基础信息
     *
     * @param mesMaterialInfo 物料基础信息
     * @return 结果
     */
    @Override
    public int insertMesMaterialInfo(MesMaterialInfo mesMaterialInfo)
    {
        return mesMaterialInfoMapper.insertMesMaterialInfo(mesMaterialInfo);
    }

    /**
     * 修改物料基础信息
     *
     * @param mesMaterialInfo 物料基础信息
     * @return 结果
     */
    @Override
    public int updateMesMaterialInfo(MesMaterialInfo mesMaterialInfo)
    {
        return mesMaterialInfoMapper.updateMesMaterialInfo(mesMaterialInfo);
    }


    @Override
    public List<MesMaterialInfo> selectMaterialByModel(Long modelId)
    {
        return mesMaterialInfoMapper.selectMaterialByModel(modelId);
    }
    @Override
    public MesMaterialInfo mesMaterialInfoByName(String materialName)
    {
        return mesMaterialInfoMapper.mesMaterialInfoByName(materialName);
    }

}
