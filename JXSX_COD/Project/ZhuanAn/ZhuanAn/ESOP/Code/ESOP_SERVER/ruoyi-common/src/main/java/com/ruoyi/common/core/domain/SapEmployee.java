package com.ruoyi.common.core.domain;

import com.ruoyi.common.annotation.Excel;
import lombok.Data;
import lombok.Getter;
import lombok.Setter;
import org.apache.commons.lang3.builder.ToStringBuilder;
import org.apache.commons.lang3.builder.ToStringStyle;

/**
 * SAP员工信息对象 emsdata.sap_employee
 *
 * @author ruoyi
 * @date 2021-07-15
 */
//@Getter
//@Setter
@Data
public class SapEmployee extends BaseEntity
{
    private static final long serialVersionUID = 1L;

    /** ID */
    private Long employeeId;

    /** 工号 */
    private String employeeNo;

    /** 姓名 */
    @Excel(name = "姓名")
    private String employeeName;

    /** 部门代码 */
    @Excel(name = "部门代码")
    private String orgCode;

    /** 部门名称 */
    @Excel(name = "部门名称")
    private String orgName;

    /** 0:在职 1:离职 */
    @Excel(name = "0:在职 1:离职")
    private String status;

    /** 邮箱 */
    @Excel(name = "邮箱")
    private String mail;

    /** 直属主管工号 */
    @Excel(name = "直属主管工号")
    private String directManager;

    /** 人事厂区 */
    @Excel(name = "人事厂区")
    private String employeeSide;

    /** 删除标志（0代表存在 1代表删除） */
    private String delFlag;

    /** 备用栏位01 */
    @Excel(name = "备用栏位01")
    private String property01;

    /** 备用栏位02 */
    @Excel(name = "备用栏位02")
    private String property02;

    /** 备用栏位03 */
    @Excel(name = "备用栏位03")
    private String property03;

    /** 备用栏位04 */
    @Excel(name = "备用栏位04")
    private String property04;

    /** 备用栏位05 */
    @Excel(name = "备用栏位05")
    private String property05;
    private String sex;

    // ===== 手动 Getter 开始 =====
    public Long getEmployeeId() {
        return employeeId;
    }

    public String getEmployeeNo() {
        return employeeNo;
    }

    public String getEmployeeName() {
        return employeeName;
    }

    public String getOrgCode() {
        return orgCode;
    }

    public String getOrgName() {
        return orgName;
    }

    public String getStatus() {
        return status;
    }

    public String getMail() {
        return mail;
    }

    public String getDirectManager() {
        return directManager;
    }

    public String getEmployeeSide() {
        return employeeSide;
    }

    public String getDelFlag() {
        return delFlag;
    }

    public String getProperty01() {
        return property01;
    }

    public String getProperty02() {
        return property02;
    }

    public String getProperty03() {
        return property03;
    }

    public String getProperty04() {
        return property04;
    }

    public String getProperty05() {
        return property05;
    }

    public String getSex() {
        return sex;
    }
    // ===== 手动 Getter 结束 =====

    @Override
    public String toString() {
        return new ToStringBuilder(this, ToStringStyle.MULTI_LINE_STYLE)
                .append("employeeId", getEmployeeId())
                .append("employeeNo", getEmployeeNo())
                .append("employeeName", getEmployeeName())
                .append("orgCode", getOrgCode())
                .append("orgName", getOrgName())
                .append("status", getStatus())
                .append("mail", getMail())
                .append("directManager", getDirectManager())
                .append("employeeSide", getEmployeeSide())
                .append("delFlag", getDelFlag())
                .append("createBy", getCreateBy())
                .append("createTime", getCreateTime())
                .append("updateBy", getUpdateBy())
                .append("updateTime", getUpdateTime())
                .append("property01", getProperty01())
                .append("property02", getProperty02())
                .append("property03", getProperty03())
                .append("property04", getProperty04())
                .append("property05", getProperty05())
                .toString();
    }
}
