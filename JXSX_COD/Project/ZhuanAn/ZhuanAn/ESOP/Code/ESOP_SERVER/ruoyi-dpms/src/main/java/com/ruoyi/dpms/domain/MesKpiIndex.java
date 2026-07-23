package com.ruoyi.dpms.domain;

import com.ruoyi.common.annotation.Excel;
import com.ruoyi.common.core.domain.BaseEntity;

/**
 * KPI指标对象 mes_kpi_index
 * 
 * @author ruoyi
 * @date 2022-11-10
 */
public class MesKpiIndex extends BaseEntity
{
    private static final long serialVersionUID = 1L;

    /** id */
    private Long id;

    /** kpi名称 */
    @Excel(name = "kpi名称")
    private String kpiIndex;

    /** kpi类型名称 */
    @Excel(name = "kpi类型名称")
    private String kpiType;

    /** 计算公式 */
    @Excel(name = "计算公式")
    private String calculationFormula;

    /** 单位 */
    @Excel(name = "单位")
    private String units;

    private String status;

    public void setId(Long id) 
    {
        this.id = id;
    }

    public Long getId() 
    {
        return id;
    }
    public void setKpiIndex(String kpiIndex) 
    {
        this.kpiIndex = kpiIndex;
    }

    public String getKpiIndex() 
    {
        return kpiIndex;
    }
    public void setKpiType(String kpiType)
    {
        this.kpiType = kpiType;
    }

    public String getKpiType()
    {
        return kpiType;
    }
    public void setCalculationFormula(String calculationFormula) 
    {
        this.calculationFormula = calculationFormula;
    }

    public String getCalculationFormula() 
    {
        return calculationFormula;
    }
    public void setUnits(String units) 
    {
        this.units = units;
    }

    public String getUnits() 
    {
        return units;
    }

    public String getStatus() {
        return status;
    }

    public void setStatus(String status) {
        this.status = status;
    }

    @Override
    public String toString() {
        return "MesKpiIndex{" +
                "id=" + id +
                ", kpiIndex='" + kpiIndex + '\'' +
                ", kpiType='" + kpiType + '\'' +
                ", calculationFormula='" + calculationFormula + '\'' +
                ", units='" + units + '\'' +
                ", status='" + status + '\'' +
                '}';
    }
}
