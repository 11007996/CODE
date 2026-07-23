package com.ruoyi.replace.service;

import com.ruoyi.replace.domain.MesMaterialInfo;

import java.util.List;

/**
 * 物料基础信息Service接口
 *
 * @author ruoyi
 * @date 2022-12-24
 */
public interface IMesMaterialInfoService
{
    /**
     * 查询物料基础信息
     *
     * @param id 物料基础信息主键
     * @return 物料基础信息
     */
    public MesMaterialInfo selectMesMaterialInfoById(Long id);

    /**
     * 查询物料基础信息列表
     *
     * @param mesMaterialInfo 物料基础信息
     * @return 物料基础信息集合
     */
    public List<MesMaterialInfo> selectMesMaterialInfoList(MesMaterialInfo mesMaterialInfo);

    /**
     * 新增物料基础信息
     *
     * @param mesMaterialInfo 物料基础信息
     * @return 结果
     */
    public int insertMesMaterialInfo(MesMaterialInfo mesMaterialInfo);

    /**
     * 修改物料基础信息
     *
     * @param mesMaterialInfo 物料基础信息
     * @return 结果
     */
    public int updateMesMaterialInfo(MesMaterialInfo mesMaterialInfo);


    /**
     * 根据机种查询料号（机种1：料号n）
     *
     */
    public List<MesMaterialInfo> selectMaterialByModel(Long modelId);

    MesMaterialInfo mesMaterialInfoByName(String materialName);

    /**
     * sop模板复制(根据料号获取制程)
     *
     */
//    public List<String> selectTerminalByModel(MesMaterialInfo mesMaterialInfo);
}
