package com.ruoyi.system.service;

import com.ruoyi.common.core.domain.SapEmployee;

import java.util.List;

/**
 * SAP员工信息Service接口
 *
 * @author ruoyi
 * @date 2021-07-15
 */
public interface ISapEmployeeService {
    /**
     * 查询SAP员工信息
     *
     * @param employeeId SAP员工信息ID
     * @return SAP员工信息
     */
    public SapEmployee selectSapEmployeeById(Long employeeId);


    /**
     * 查询SAP员工信息列表
     *
     * @param sapEmployee SAP员工信息
     * @return SAP员工信息集合
     */
    public List<SapEmployee> selectSapEmployeeList(SapEmployee sapEmployee);

    /**
     * 查询HR员工信息列表
     *
     * @param sapEmployee SAP员工信息
     * @return SAP员工信息集合
     */
    public List<SapEmployee> selectHrEmployeeList(SapEmployee sapEmployee);

    /**
     * 新增SAP员工信息
     *
     * @param sapEmployee SAP员工信息
     * @return 结果
     */
    public int insertSapEmployee(SapEmployee sapEmployee);

    /**
     * 新增SAP员工信息列表
     *
     * @param sapEmployee SAP员工信息
     * @return 结果
     */
    public int insertSapEmployeeList(List<SapEmployee> sapEmployee);

    /**
     * 修改SAP员工信息
     *
     * @param sapEmployee SAP员工信息
     * @return 结果
     */
    public int updateSapEmployee(SapEmployee sapEmployee);

    /**
     * 批量删除SAP员工信息
     *
     * @param employeeIds 需要删除的SAP员工信息ID
     * @return 结果
     */
    public int deleteSapEmployeeByIds(Long[] employeeIds);

    /**
     * 删除SAP员工信息信息
     *
     * @param employeeId SAP员工信息ID
     * @return 结果
     */
    public int deleteSapEmployeeById(Long employeeId);

    /**
     * 查询SAP员工信息
     *
     * @param employeeNo SAP员工信息工号
     * @return SAP员工信息
     */
    public SapEmployee selectSapEmployeeByNo(String employeeNo);

    /**
     * 查询SAP部门代码
     *
     * @param orgCode SAP部门代码
     * @return SAP员工信息
     */
//    SapEmployee selectOrgCode(String orgCode);

    /**
     * 将SAP主表没有的临时表数据放进主表
     * @return
     */
    public int selectSapEmployeeTempList();


    /**
     * 批量删除SAP临时表员工信息
     *
     * @return 结果
     */
    public int deleteSapEmployeeTempByIds();
}