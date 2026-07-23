package com.ruoyi.dpms.domain;

import com.ruoyi.common.core.domain.BaseEntity;

import java.util.Date;

public class MesKpiDetail extends BaseEntity {
    /** ID(自增) */
    private Long id;

    private  Long assignId;

    private String targetParam;

    private String targetValue;

    private Long targetStandard;

    private String actualValue;

    private String status;

    private String filledPerson;

    private String personLiable;

    private String notifyPerson;

    private Date filledDate;

    public Long getId() {
        return id;
    }

    public void setId(Long id) {
        this.id = id;
    }

    public Long getAssignId() { return assignId; }

    public void setAssignId(Long assignId) { this.assignId = assignId; }

    public String getTargetParam() {
        return targetParam;
    }

    public void setTargetParam(String targetParam) {
        this.targetParam = targetParam;
    }

    public String getTargetValue() {
        return targetValue;
    }

    public void setTargetValue(String targetValue) {
        this.targetValue = targetValue;
    }

    public Long getTargetStandard() {
        return targetStandard;
    }

    public void setTargetStandard(Long targetStandard) {
        this.targetStandard = targetStandard;
    }

    public String getActualValue() {
        return actualValue;
    }

    public void setActualValue(String actualValue) {
        this.actualValue = actualValue;
    }

    public String getStatus() {
        return status;
    }

    public void setStatus(String status) {
        this.status = status;
    }

    public String getFilledPerson() {
        return filledPerson;
    }

    public void setFilledPerson(String filledPerson) {
        this.filledPerson = filledPerson;
    }

    public String getPersonLiable() {
        return personLiable;
    }

    public void setPersonLiable(String personLiable) {
        this.personLiable = personLiable;
    }

    public String getNotifyPerson() {
        return notifyPerson;
    }

    public void setNotifyPerson(String notifyPerson) {
        this.notifyPerson = notifyPerson;
    }

    public Date getFilledDate() {
        return filledDate;
    }

    public void setFilledDate(Date filledDate) {
        this.filledDate = filledDate;
    }

    @Override
    public String toString() {
        return "MesKpiDetail{" +
                "id=" + id +
                ", assignId=" + assignId +
                ", targetParam='" + targetParam + '\'' +
                ", targetValue='" + targetValue + '\'' +
                ", targetStandard=" + targetStandard +
                ", actualValue='" + actualValue + '\'' +
                ", status='" + status + '\'' +
                ", filledPerson='" + filledPerson + '\'' +
                ", personLiable='" + personLiable + '\'' +
                ", notifyPerson='" + notifyPerson + '\'' +
                ", filledDate=" + filledDate +
                '}';
    }
}
