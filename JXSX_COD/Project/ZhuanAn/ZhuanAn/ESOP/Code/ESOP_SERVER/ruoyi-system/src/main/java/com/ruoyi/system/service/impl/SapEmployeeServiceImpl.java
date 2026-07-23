package com.ruoyi.system.service.impl;

import com.ruoyi.common.core.domain.SapEmployee;
import com.ruoyi.common.utils.DateUtils;
import com.ruoyi.common.utils.SecurityUtils;
import com.ruoyi.system.mapper.SapEmployeeMapper;
import com.ruoyi.system.service.ISapEmployeeService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.stereotype.Service;

import java.util.List;

/**
 * SAP员工信息Service业务层处理
 *
 * @author ruoyi
 * @date 2021-07-15
 */
@Service
public class SapEmployeeServiceImpl implements ISapEmployeeService
{
    @Value("${spring.profiles.active}")
    private String env;
    @Autowired
    private SapEmployeeMapper sapEmployeeMapper;

    /**
     * 查询SAP员工信息
     *
     * @param employeeId SAP员工信息ID
     * @return SAP员工信息
     */
    @Override
    public SapEmployee selectSapEmployeeById(Long employeeId)
    {
        return sapEmployeeMapper.selectSapEmployeeById(employeeId);
    }
    /**
     * 查询SAP员工信息列表
     *
     * @param sapEmployee SAP员工信息
     * @return SAP员工信息
     */
    @Override
    public List<SapEmployee> selectSapEmployeeList(SapEmployee sapEmployee)
    {
        return sapEmployeeMapper.selectSapEmployeeList(sapEmployee);
    }

    /**
     * 查询HR员工信息列表
     *
     * @param sapEmployee HR员工信息
     * @return HR员工信息
     */
    @Override
    public List<SapEmployee> selectHrEmployeeList(SapEmployee sapEmployee)
    {
        return sapEmployeeMapper.selectHrEmployeeList(sapEmployee);
    }

    /**
     * 新增SAP员工信息
     *
     * @param sapEmployee SAP员工信息
     * @return 结果
     */
    @Override
    public int insertSapEmployee(SapEmployee sapEmployee)
    {
        sapEmployee.setCreateTime(DateUtils.getNowDate());
        return sapEmployeeMapper.insertSapEmployee(sapEmployee);
    }

    /**
     * 新增SAP员工信息列表
     *
     * @param sapEmployee SAP员工信息
     * @return 结果
     */
    @Override
    public int insertSapEmployeeList(List<SapEmployee> sapEmployee)
    {
        int i=0;
        for (SapEmployee emp:sapEmployee) {
            i++;
            sapEmployeeMapper.insertSapEmployeeList(emp);
        }
        return i;
    }

    /**
     * 修改SAP员工信息
     *
     * @param sapEmployee SAP员工信息
     * @return 结果
     */
    @Override
    public int updateSapEmployee(SapEmployee sapEmployee)
    {
        sapEmployee.setUpdateTime(DateUtils.getNowDate());
        return sapEmployeeMapper.updateSapEmployee(sapEmployee);
    }

    /**
     * 批量删除SAP员工信息
     *
     * @param employeeIds 需要删除的SAP员工信息ID
     * @return 结果
     */
    @Override
    public int deleteSapEmployeeByIds(Long[] employeeIds)
    {
        return sapEmployeeMapper.deleteSapEmployeeByIds(employeeIds);
    }

    /**
     * 删除SAP员工信息信息
     *
     * @param employeeId SAP员工信息ID
     * @return 结果
     */
    @Override
    public int deleteSapEmployeeById(Long employeeId)
    {
        return sapEmployeeMapper.deleteSapEmployeeById(employeeId);
    }

    /**
     * 查询SAP员工信息
     *
     * @param employeeNo SAP员工信息工号
     * @return SAP员工信息
     */
    @Override
    public SapEmployee selectSapEmployeeByNo(String employeeNo) {
        return sapEmployeeMapper.selectHrEmployeeByNo(employeeNo);
    }

    /**
     * 查询SAP部门代码
     *
     * @param orgCode SAP部门代码
     * @return SAP员工信息
     */
//    @Override
//    public SapEmployee selectOrgCode(String orgCode) {
//        return sapEmployeeMapper.selectOrgCode(orgCode, SecurityUtils.getLoginUser().getClient());
//    }

    /**
     * 将SAP主表没有的临时表数据放进主表
     * @return
     */
    @Override
    public int selectSapEmployeeTempList(){
        List<SapEmployee> sapEmployeeTempList = sapEmployeeMapper.selectSapEmployeeTempList();
        int i=0;
        for (SapEmployee emp:sapEmployeeTempList) {
            i++;
            sapEmployeeMapper.insertSapEmployee(emp);
        }
        return i;
    }


    /**
     * 批量删除SAP员工信息
     *
     * @return 结果
     */
    @Override
    public int deleteSapEmployeeTempByIds()
    {
        return sapEmployeeMapper.deleteSapEmployeeTempByIds();
    }
}

