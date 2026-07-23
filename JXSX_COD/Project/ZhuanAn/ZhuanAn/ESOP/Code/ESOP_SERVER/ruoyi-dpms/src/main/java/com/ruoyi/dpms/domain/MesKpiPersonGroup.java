package com.ruoyi.dpms.domain;

import com.ruoyi.common.core.domain.BaseEntity;

public class MesKpiPersonGroup  extends BaseEntity {
    /** ID(自增) */
    private Long id;

    /** 部门id */
    private Long deptId;

    private String userAccount;

    private Long assignId;

    private String groupType;


    public Long getId() {
        return id;
    }

    public void setId(Long id) {
        this.id = id;
    }

    public Long getDeptId() {
        return deptId;
    }

    public void setDeptId(Long deptId) {
        this.deptId = deptId;
    }

    public String getUserAccount() {
        return userAccount;
    }

    public void setUserAccount(String userAccount) {
        this.userAccount = userAccount;
    }

    public Long getAssignId() {
        return assignId;
    }

    public void setAssignId(Long assignId) {
        this.assignId = assignId;
    }

    public String getGroupType() {
        return groupType;
    }

    public void setGroupType(String groupType) {
        this.groupType = groupType;
    }

    @Override
    public String toString() {
        return "MesKpiPersonGroup{" +
                "id=" + id +
                ", deptId=" + deptId +
                ", userAccount='" + userAccount + '\'' +
                ", assignId=" + assignId +
                ", groupType='" + groupType + '\'' +
                '}';
    }
}
