package com.ruoyi.product.mapper;

import java.util.List;

import com.ruoyi.product.domain.MesLine;
import com.ruoyi.product.domain.MesModel;
import com.ruoyi.product.domain.MesStage;

/**
 * 区段管理Mapper接口
 *
 * @author ruoyi
 * @date 2022-09-14
 */
public interface MesStageMapper
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
     * @param stageName 区段名称
     * @return 区段信息
     */
    public MesStage checkStageNameUnique(String stageName);

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
     * 删除区段管理
     *
     * @param stageId 区段管理主键
     * @return 结果
     */
    public int deleteMesStageByStageId(Long stageId);

    /**
     * 批量删除区段管理
     *
     * @param stageIds 需要删除的数据主键集合
     * @return 结果
     */
    public int deleteMesStageByStageIds(Long[] stageIds);
    public MesStage stageInfoByName(String stageName);
}
