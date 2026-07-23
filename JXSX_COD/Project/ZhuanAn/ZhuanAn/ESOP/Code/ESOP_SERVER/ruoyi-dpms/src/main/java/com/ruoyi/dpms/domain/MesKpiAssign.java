package com.ruoyi.dpms.domain;

import com.ruoyi.common.annotation.Excel;
import com.ruoyi.common.core.domain.BaseEntity;

/**
 * KPI指派对象 mes_kpi_assign
 * 
 * @author ruoyi
 * @date 2022-11-10
 */
public class MesKpiAssign extends BaseEntity
{
    private static final long serialVersionUID = 1L;

    /** 指派流水ID(自增) */
    private Long id;

    /** KPI年（标题） */
    @Excel(name = "KPI年", readConverterExp = "标题")
    private Long kpiYear;

    /** 人员群组ID */
//    @Excel(name = "人员群组ID")
//    private Long groupId;

    /** kpi id */
    @Excel(name = "kpi id")
    private Long kpiId;

    /** 权重占比 */
    @Excel(name = "权重占比")
    private Float weightCoefficient;

    /** 统计周期 */
    @Excel(name = "统计周期")
    private String statisticalCycle;

    /** 目标值标准 */
    private Long targetStandard;

    /** 备注 */
    private String remark;

    /** 状态（0可编辑，1禁用） */
    @Excel(name = "状态", readConverterExp = "0=可编辑，1禁用")
    private String status;

    /** 今年 */
    @Excel(name = "今年")
    private Long thisYear;

    /** 今年达成值 */
    @Excel(name = "今年达成值")
    private String thisYearValue;

    /** 上一年 */
    @Excel(name = "上一年")
    private Long lastYear;

    /** 上一年达成值 */
    @Excel(name = "上一年达成值")
    private String lastYearValue;

//    private String userAccount;
//
//    private String userName;
////    private String nickName;
    private Long deptId;
//
//    private String deptName;

    private String kpiIndex;

    private String units;

    private String calculationFormula;

//    private String targetParam;
//
//    private String targetValue;


    private Object appraisedPersonGroup;

    private Object  statisticalPersonGroup;

    private Object targetList;

//    private Object actualList;


    public Long getId() {
        return id;
    }

    public void setId(Long id) {
        this.id = id;
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

    public Float getWeightCoefficient() {
        return weightCoefficient;
    }

    public void setWeightCoefficient(Float weightCoefficient) {
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


    public String getRemark() {
        return remark;
    }

    public void setRemark(String remark) {
        this.remark = remark;
    }

    public String getStatus() {
        return status;
    }

    public void setStatus(String status) {
        this.status = status;
    }

    public Long getThisYear() {
        return thisYear;
    }

    public void setThisYear(Long thisYear) {
        this.thisYear = thisYear;
    }

    public String getThisYearValue() {
        return thisYearValue;
    }

    public void setThisYearValue(String thisYearValue) {
        this.thisYearValue = thisYearValue;
    }

    public Long getLastYear() {
        return lastYear;
    }

    public void setLastYear(Long lastYear) {
        this.lastYear = lastYear;
    }

    public String getLastYearValue() {
        return lastYearValue;
    }

    public void setLastYearValue(String lastYearValue) {
        this.lastYearValue = lastYearValue;
    }

    public Long getDeptId() {
        return deptId;
    }

    public void setDeptId(Long deptId) {
        this.deptId = deptId;
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

//    public Object getActualList() {
//        return actualList;
//    }
//
//    public void setActualList(Object actualList) {
//        this.actualList = actualList;
//    }




    @Override
    public String toString() {
        return "MesKpiAssign{" +
                "id=" + id +
                ", kpiYear=" + kpiYear +
                ", kpiId=" + kpiId +
                ", weightCoefficient=" + weightCoefficient +
                ", statisticalCycle='" + statisticalCycle + '\'' +
                ", remark='" + remark + '\'' +
                ", status='" + status + '\'' +
                ", thisYear=" + thisYear +
                ", thisYearValue='" + thisYearValue + '\'' +
                ", lastYear=" + lastYear +
                ", lastYearValue='" + lastYearValue + '\'' +
//                ", userAccount='" + userAccount + '\'' +
//                ", userName='" + userName + '\'' +
                ", deptId=" + deptId +
//                ", deptName='" + deptName + '\'' +
                ", kpiIndex='" + kpiIndex + '\'' +
                ", units='" + units + '\'' +
                ", calculationFormula='" + calculationFormula + '\'' +
//                ", targetParam='" + targetParam + '\'' +
//                ", targetValue='" + targetValue + '\'' +
                ", appraisedPersonGroup='" + appraisedPersonGroup + '\'' +
                ", statisticalPersonGroup='" + statisticalPersonGroup + '\'' +
                ", targetList='" + targetList + '\'' +
                '}';
    }
}
