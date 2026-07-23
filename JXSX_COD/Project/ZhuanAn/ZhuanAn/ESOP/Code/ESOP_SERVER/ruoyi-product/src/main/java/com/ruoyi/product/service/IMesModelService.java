package com.ruoyi.product.service;

import com.ruoyi.product.domain.MesModel;

import java.util.List;

/**
 * 机种信息Service接口
 *
 * @author ruoyi
 * @date 2022-09-14
 */
public interface IMesModelService
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
     * @param model 机种信息
     * @return 结果
     */
    public String checkModelNameUnique(MesModel model);

    /**
     * 校验机种是否允许操作
     *
     * @param mesModel 机种
     */
    public void checkModelAllowed(MesModel mesModel);

    /**
     * 校验机种是否有数据权限
     *
     * @param modelId 机种id
     */
    public void checkModelDataScope(Long modelId);

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
     * 修改机种状态
     *
     * @param model 机种信息
     * @return 结果
     */
    public int updateModelStatus(MesModel model);

    /**
     * 批量删除机种信息
     *
     * @param modelIds 需要删除的机种信息主键集合
     * @return 结果
     */
    public int deleteMesModelByModelIds(Long[] modelIds);

    /**
     * 删除机种信息信息
     *
     * @param modelId 机种信息主键
     * @return 结果
     */
    public int deleteMesModelByModelId(Long modelId);

    /**
     * 根据机种名称查询机种详情信息
     */
    public MesModel modelInfoByName(String modelName);

}
