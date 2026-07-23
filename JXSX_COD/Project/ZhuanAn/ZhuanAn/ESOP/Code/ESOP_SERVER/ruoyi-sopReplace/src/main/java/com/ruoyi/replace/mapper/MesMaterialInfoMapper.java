package com.ruoyi.replace.mapper;

import com.ruoyi.replace.domain.MesMaterialInfo;

import java.util.List;


/**
 * 物料基础信息Mapper接口
 *
 * @author ruoyi
 * @date 2022-12-24
 */
public interface MesMaterialInfoMapper
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

//    /**
//     * 删除物料基础信息
//     *
//     * @param id 物料基础信息主键
//     * @return 结果
//     */
//    public int deleteMesMaterialInfoById(Long id);
//
//    /**
//     * 批量删除物料基础信息
//     *
//     * @param ids 需要删除的数据主键集合
//     * @return 结果
//     */
//    public int deleteMesMaterialInfoByIds(Long[] ids);

    /**
     * 根据机种查询料号信息
     *
     */
    public List<MesMaterialInfo> selectMaterialByModel(Long modelId);
    MesMaterialInfo mesMaterialInfoByName(String materialName);
}
