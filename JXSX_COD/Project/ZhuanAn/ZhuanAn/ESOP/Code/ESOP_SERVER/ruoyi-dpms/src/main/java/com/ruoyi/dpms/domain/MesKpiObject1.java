package com.ruoyi.dpms.domain;

import com.ruoyi.common.core.domain.BaseEntity;

public class MesKpiObject1 extends BaseEntity {
    private static final long serialVersionUID = 1L;

    /** 指派流水ID(自增) */
    private Long id;
    private String targetParam;

    private String targetValue;

    private String actualValue;

    public Long getId() {
        return id;
    }

    public void setId(Long id) {
        this.id = id;
    }

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

    public String getActualValue() {
        return actualValue;
    }

    public void setActualValue(String actualValue) {
        this.actualValue = actualValue;
    }

    @Override
    public String toString() {
        return "MesKpiObject1{" +
                "id=" + id +
                "targetParam='" + targetParam + '\'' +
                ", targetValue='" + targetValue + '\'' +
                ", actualValue='" + actualValue + '\'' +
                '}';
    }

    //目标值、实际值list
    public String toJSON() {
        return "{" +
                "label:'" + targetParam + '\'' +
                ", value:'" + targetValue + '\'' +
                '}';
    }
    //list
    public String toJSON1() {
        return "{" +
                "label:'" + targetParam + '\'' +
                ", value:'" + actualValue + '\'' +
                '}';
    }

}
