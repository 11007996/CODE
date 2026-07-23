package com.ruoyi.dpms.domain;

import com.ruoyi.common.core.domain.BaseEntity;

public class MesKpiObject extends BaseEntity {
    private static final long serialVersionUID = 1L;

    /** 指派流水ID(自增) */
    private Long id;

    private String userAccount;

    private String userName;
    private Long deptId;

    private String deptName;

    private Long groupType;

    public Long getId() {
        return id;
    }

    public void setId(Long id) {
        this.id = id;
    }

    public String getUserAccount() {
        return userAccount;
    }

    public void setUserAccount(String userAccount) {
        this.userAccount = userAccount;
    }

    public String getUserName() {
        return userName;
    }

    public void setUserName(String userName) {
        this.userName = userName;
    }

    public Long getDeptId() {
        return deptId;
    }

    public void setDeptId(Long deptId) {
        this.deptId = deptId;
    }

    public String getDeptName() {
        return deptName;
    }

    public void setDeptName(String deptName) {
        this.deptName = deptName;
    }

    public Long getGroupType() {
        return groupType;
    }

    public void setGroupType(Long groupType) {
        this.groupType = groupType;
    }

    @Override
    public String toString() {
        return "MesKpiObject{" +
                "id=" + id +
                ", userAccount='" + userAccount + '\'' +
                ", userName='" + userName + '\'' +
                ", deptId=" + deptId +
                ", deptName='" + deptName + '\'' +
                ", groupType='" + groupType + '\'' +
                '}';
    }

    //   被考核人员群组、统计人员群组list
    public String toJSON() {
        return "{" +
                "userAccount:'" + userAccount + '\'' +
                ", userName:'" + userName + '\'' +
                ", deptId:" + deptId +
                ", deptName:'" + deptName + '\'' +
                '}';
    }


}
