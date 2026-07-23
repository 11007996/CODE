package com.ruoyi.product.mapper;

import java.util.List;
import com.ruoyi.product.domain.MesModel;

/**
 * 机种信息Mapper接口
 *
 * @author ruoyi
 * @date 2022-09-14
 */
public interface MesModelMapper
{
    /**
     * 查询机种信息
     *
     * @param modelId 机种信息主键
     * @return 机种信息
     */
    public MesModel selectMesModelByModelId(Long modelId);

    /**
     * 查询机种信息列表
     *
     * @param mesModel 机种信息
     * @return 机种信息集合
     */
    public List<MesModel> selectMesModelList(MesModel mesModel);

    /**
     * 校验机种名称是否唯一
     *
     * @param modelName 机种名称
     * @return 机种信息
     */
    public MesModel checkModelNameUnique(String modelName);

    /**
     * 新增机种信息
     *
     * @param mesModel 机种信息
     * @return 结果
     */
    public int insertMesModel(MesModel mesModel);

    /**
     * 修改机种信息
     *
     * @param mesModel 机种信息
     * @return 结果
     */
    public int updateMesModel(MesModel mesModel);

    /**
     * 删除机种信息
     *
     * @param modelId 机种信息主键
     * @return 结果
     */
    public int deleteMesModelByModelId(Long modelId);

    /**
     * 批量删除机种信息
     *
     * @param modelIds 需要删除的数据主键集合
     * @return 结果
     */
    public int deleteMesModelByModelIds(Long[] modelIds);

    MesModel modelInfoByName(String modelName);
}
