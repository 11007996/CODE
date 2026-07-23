package com.ruoyi.product.service;

import java.util.List;
import com.ruoyi.product.domain.MesProcess;
import com.ruoyi.product.domain.MesStage;

/**
 * 制程Service接口
 *
 * @author ruoyi
 * @date 2022-09-14
 */
public interface IMesProcessService
{
    /**
     * 查询制程
     *
     * @param processId 制程主键
     * @return 制程
     */
    public MesProcess selectMesProcessByProcessId(Long processId);

    /**
     * 查询制程列表
     *
     * @param mesProcess 制程
     * @return 制程集合
     */
    public List<MesProcess> selectMesProcessList(MesProcess mesProcess);

    /**
     * 校验制程名称是否唯一
     *
     * @param mesProcess 制程信息
     * @return 结果
     */
    public String checkProcessNameUnique(MesProcess mesProcess);

    /**
     * 校验制程是否允许操作
     *
     * @param mesProcess 制程
     */
    public void checkProcessAllowed(MesProcess mesProcess);

    /**
     * 校验制程是否有数据权限
     *
     * @param processId 制程id
     */
    public void checkProcessDataScope(Long processId);

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
     * 修改制程状态
     *
     * @param mesProcess 制程
     * @return 结果
     */
    public int updateMesProcessStatus(MesProcess mesProcess);

    /**
     * 批量删除制程
     *
     * @param processIds 需要删除的制程主键集合
     * @return 结果
     */
    public int deleteMesProcessByProcessIds(Long[] processIds);

    /**
     * 删除制程信息
     *
     * @param processId 制程主键
     * @return 结果
     */
    public int deleteMesProcessByProcessId(Long processId);
    MesProcess processInfoByName(String processName);
}
