package com.ruoyi.product.mapper;

import java.util.List;

import com.ruoyi.common.core.domain.entity.SysRole;
import com.ruoyi.product.domain.MesProcess;

/**
 * 制程Mapper接口
 *
 * @author ruoyi
 * @date 2022-09-14
 */
public interface MesProcessMapper {
    /**
     * 查询制程
     *
     * @param processId 制程主键
     * @return 制程
     */
    public MesProcess selectMesProcessByProcessId(Long processId);

    /**
     * 校验制程名称是否唯一
     *
     * @param processName 制程名称
     * @return 制程信息
     */
    public MesProcess checkProcessNameUnique(String processName);


    /**
     * 查询制程列表
     *
     * @param mesProcess 制程
     * @return 制程集合
     */
    public List<MesProcess> selectMesProcessList(MesProcess mesProcess);

    /**
     * 新增制程
     *
     * @param mesProcess 制程
     * @return 结果
     */
    public int insertMesProcess(MesProcess mesProcess);

    /**
     * 修改制程
     *
     * @param mesProcess 制程
     * @return 结果
     */
    public int updateMesProcess(MesProcess mesProcess);

    /**
     * 删除制程
     *
     * @param processId 制程主键
     * @return 结果
     */
    public int deleteMesProcessByProcessId(Long processId);

    /**
     * 批量删除制程
     *
     * @param processIds 需要删除的数据主键集合
     * @return 结果
     */
    public int deleteMesProcessByProcessIds(Long[] processIds);
    MesProcess processInfoByName(String processName);

}
