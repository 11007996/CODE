package com.ruoyi.product.service;

import java.util.List;

import com.ruoyi.product.domain.MesLine;
import com.ruoyi.product.domain.MesModel;
import com.ruoyi.product.domain.MesStage;

/**
 * 区段管理Service接口
 *
 * @author ruoyi
 * @date 2022-09-14
 */
public interface IMesStageService
{
    /**
     * 查询区段管理
     *
     * @param stageId 区段管理主键
     * @return 区段管理
     */
    public MesStage selectMesStageByStageId(Long stageId);

    /**
     * 查询区段管理列表
     *
     * @param mesStage 区段管理
     * @return 区段管理集合
     */
    public List<MesStage> selectMesStageList(MesStage mesStage);

    /**
     * 校验区段名称是否唯一
     *
     * @param mesStage 区段信息
     * @return 结果
     */
    public String checkStageNameUnique(MesStage mesStage);

    /**
     * 校验区段是否允许操作
     *
     * @param mesStage 区段
     */
    public void checkStageAllowed(MesStage mesStage);

    /**
     * 校验区段是否有数据权限
     *
     * @param stageId 区段id
     */
    public void checkStageDataScope(Long stageId);

    /**
     * 新增区段管理
     *
     * @param mesStage 区段管理
     * @return 结果
     */
    public int insertMesStage(MesStage mesStage);

    /**
     * 修改区段管理
     *
     * @param mesStage 区段管理
     * @return 结果
     */
    public int updateMesStage(MesStage mesStage);

    /**
     * 修改区段状态
     *
     * @param mesStage 区段信息
     * @return 结果
     */
    public int updateStageStatus(MesStage mesStage);

    /**
     * 批量删除区段管理
     *
     * @param stageIds 需要删除的区段管理主键集合
     * @return 结果
     */
    public int deleteMesStageByStageIds(Long[] stageIds);

    /**
     * 删除区段管理信息
     *
     * @param stageId 区段管理主键
     * @return 结果
     */
    public int deleteMesStageByStageId(Long stageId);
    MesStage stageInfoByName(String stageName);
}
