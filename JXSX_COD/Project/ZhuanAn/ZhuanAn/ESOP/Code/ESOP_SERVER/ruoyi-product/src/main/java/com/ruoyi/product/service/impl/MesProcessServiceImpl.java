package com.ruoyi.product.service.impl;

import java.util.List;

import com.ruoyi.common.constant.UserConstants;
import com.ruoyi.common.core.domain.entity.SysUser;
import com.ruoyi.common.exception.ServiceException;
import com.ruoyi.common.utils.DateUtils;
import com.ruoyi.common.utils.SecurityUtils;
import com.ruoyi.common.utils.StringUtils;
import com.ruoyi.common.utils.spring.SpringUtils;
import com.ruoyi.product.domain.MesStage;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import com.ruoyi.product.mapper.MesProcessMapper;
import com.ruoyi.product.domain.MesProcess;
import com.ruoyi.product.service.IMesProcessService;

/**
 * 制程Service业务层处理
 *
 * @author ruoyi
 * @date 2022-09-14
 */
@Service
public class MesProcessServiceImpl implements IMesProcessService
{
    @Autowired
    private MesProcessMapper mesProcessMapper;

    /**
     * 查询制程
     *
     * @param processId 制程主键
     * @return 制程
     */
    @Override
    public MesProcess selectMesProcessByProcessId(Long processId)
    {
        return mesProcessMapper.selectMesProcessByProcessId(processId);
    }

    /**
     * 查询制程列表
     *
     * @param mesProcess 制程
     * @return 制程
     */
    @Override
    public List<MesProcess> selectMesProcessList(MesProcess mesProcess)
    {
        return mesProcessMapper.selectMesProcessList(mesProcess);
    }

    /**
     * 检查制程名称是否唯一
     *
     * @param mesProcess 制程信息
     * @return
     */
    @Override
    public String checkProcessNameUnique(MesProcess mesProcess) {
        Long processId = StringUtils.isNull(mesProcess.getProcessId()) ? -1L : mesProcess.getProcessId();
        MesProcess info = mesProcessMapper.checkProcessNameUnique(mesProcess.getProcessName());
        if (StringUtils.isNotNull(info) && info.getProcessId().longValue() != processId.longValue())
        {
            return UserConstants.NOT_UNIQUE;
        }
        return UserConstants.UNIQUE;
    }

    /**
     * 校验制程是否允许操作
     *
     * @param mesProcess 制程
     */
    @Override
    public void checkProcessAllowed(MesProcess mesProcess) {
        if(StringUtils.isNotNull(mesProcess.getProcessId()) && mesProcess.isAdmin())
        {
            throw new ServiceException("不允许操作超级管理员角色");
        }
    }

    /**
     * 校验制程是否有数据权限
     *
     * @param processId 制程id
     */
    @Override
    public void checkProcessDataScope(Long processId) {
        if(!SysUser.isAdmin(SecurityUtils.getUserId()))
        {
            MesProcess mesProcess = new MesProcess();
            mesProcess.setProcessId(processId);
            List<MesProcess> processes = SpringUtils.getAopProxy(this).selectMesProcessList(mesProcess);
            if(StringUtils.isEmpty(processes))
            {
                throw new ServiceException("没有权限访问角色数据！");
            }
        }
    }

    /**
     * 新增制程
     *
     * @param mesProcess 制程
     * @return 结果
     */
    @Override
    public int insertMesProcess(MesProcess mesProcess)
    {
        mesProcess.setCreateTime(DateUtils.getNowDate());
        return mesProcessMapper.insertMesProcess(mesProcess);
    }

    /**
     * 修改制程
     *
     * @param mesProcess 制程
     * @return 结果
     */
    @Override
    public int updateMesProcess(MesProcess mesProcess)
    {
        mesProcess.setUpdateTime(DateUtils.getNowDate());
        return mesProcessMapper.updateMesProcess(mesProcess);
    }

    @Override
    public int updateMesProcessStatus(MesProcess mesProcess) {
        mesProcess.setUpdateTime(DateUtils.getNowDate());
        return mesProcessMapper.updateMesProcess(mesProcess);
    }

    /**
     * 批量删除制程
     *
     * @param processIds 需要删除的制程主键
     * @return 结果
     */
    @Override
    public int deleteMesProcessByProcessIds(Long[] processIds)
    {
        return mesProcessMapper.deleteMesProcessByProcessIds(processIds);
    }

    /**
     * 删除制程信息
     *
     * @param processId 制程主键
     * @return 结果
     */
    @Override
    public int deleteMesProcessByProcessId(Long processId)
    {
        return mesProcessMapper.deleteMesProcessByProcessId(processId);
    }
    @Override
    public MesProcess processInfoByName(String processName)
    {
        return mesProcessMapper.processInfoByName(processName);
    }
}
