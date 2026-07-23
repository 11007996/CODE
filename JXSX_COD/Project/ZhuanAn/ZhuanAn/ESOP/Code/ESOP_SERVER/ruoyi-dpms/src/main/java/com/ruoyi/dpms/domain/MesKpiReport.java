package com.ruoyi.dpms.domain;

import com.ruoyi.common.core.domain.BaseEntity;

public class MesKpiReport extends BaseEntity {
    private static final long serialVersionUID = 1L;

    /** 指派流水ID(自增) */
    private Long id;

    /** 用户群组ID */
    private Long userGroupId;

    /** KPI年（标题） */
    private Long kpiYear;

    /** kpi id */
    private Long kpiId;

    /** 权重占比 */
    private Long weightCoefficient;

    /** 统计周期 */
    private String statisticalCycle;

    /** 目标值标准 */
    private Long targetStandard;

    /** 备注 */
    private String remark;

    /** 状态（0可编辑，1禁用） */
    private String status;

    /** 今年 */
    private Long thisYear;

    /** KPI名称 */
    private String kpiIndex;

    private String units;

    private String calculationFormula;

    private String targetParam;

    private String targetValue;

    private Object appraisedPersonGroup;

    private Object  statisticalPersonGroup;

    private Object targetList;

    private Object actualList;

    public Long getId() {
        return id;
    }

    public void setId(Long id) {
        this.id = id;
    }

    public Long getUserGroupId() {
        return userGroupId;
    }

    public void setUserGroupId(Long userGroupId) {
        this.userGroupId = userGroupId;
    }

    public Long getKpiYear() {
        return kpiYear;
    }

    public void setKpiYear(Long kpiYear) {
        this.kpiYear = kpiYear;
    }

    public Long getKpiId() {
        return kpiId;
    }

    public void setKpiId(Long kpiId) {
        this.kpiId = kpiId;
    }

    public Long getWeightCoefficient() {
        return weightCoefficient;
    }

    public void setWeightCoefficient(Long weightCoefficient) {
        this.weightCoefficient = weightCoefficient;
    }

    public String getStatisticalCycle() {
        return statisticalCycle;
    }

    public void setStatisticalCycle(String statisticalCycle) {
        this.statisticalCycle = statisticalCycle;
    }

    public Long getTargetStandard() {
        return targetStandard;
    }

    public void setTargetStandard(Long targetStandard) {
        this.targetStandard = targetStandard;
    }

    public String getStatus() {
        return status;
    }

    public void setStatus(String status) {
        this.status = status;
    }

    public String getRemark() { return remark; }

    public void setRemark(String remark) { this.remark = remark; }

    public Long getThisYear() {
        return thisYear;
    }

    public void setThisYear(Long thisYear) {
        this.thisYear = thisYear;
    }

    public String getKpiIndex() {
        return kpiIndex;
    }

    public void setKpiIndex(String kpiIndex) {
        this.kpiIndex = kpiIndex;
    }

    public String getUnits() {
        return units;
    }

    public void setUnits(String units) {
        this.units = units;
    }

    public String getCalculationFormula() {
        return calculationFormula;
    }

    public void setCalculationFormula(String calculationFormula) {
        this.calculationFormula = calculationFormula;
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

    public Object getAppraisedPersonGroup() {
        return appraisedPersonGroup;
    }

    public void setAppraisedPersonGroup(Object appraisedPersonGroup) {
        this.appraisedPersonGroup = appraisedPersonGroup;
    }

    public Object getStatisticalPersonGroup() {
        return statisticalPersonGroup;
    }

    public void setStatisticalPersonGroup(Object statisticalPersonGroup) {
        this.statisticalPersonGroup = statisticalPersonGroup;
    }

    public Object getTargetList() {
        return targetList;
    }

    public void setTargetList(Object targetList) {
        this.targetList = targetList;
    }

    public Object getActualList() {
        return actualList;
    }

    public void setActualList(Object actualList) {
        this.actualList = actualList;
    }

    @Override
    public String toString() {
        return "MesKpiReport{" +
                "id=" + id +
                ", userGroupId=" + userGroupId +
                ", kpiYear=" + kpiYear +
                ", kpiId=" + kpiId +
                ", weightCoefficient=" + weightCoefficient +
                ", statisticalCycle='" + statisticalCycle + '\'' +
                ", remark='" + remark + '\'' +
                ", status='" + status + '\'' +
                ", thisYear=" + thisYear +
                ", kpiIndex='" + kpiIndex + '\'' +
                ", units='" + units + '\'' +
                ", calculationFormula='" + calculationFormula + '\'' +
                ", targetParam='" + targetParam + '\'' +
                ", targetValue='" + targetValue + '\'' +
                ", appraisedPersonGroup=" + appraisedPersonGroup +
                ", statisticalPersonGroup=" + statisticalPersonGroup +
                ", targetList=" + targetList +
                ", actualList=" + actualList +
                '}';
    }
}
