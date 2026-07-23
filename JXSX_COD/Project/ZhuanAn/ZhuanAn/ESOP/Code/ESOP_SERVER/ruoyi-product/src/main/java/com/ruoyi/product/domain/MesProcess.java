package com.ruoyi.product.domain;

import org.apache.commons.lang3.builder.ToStringBuilder;
import org.apache.commons.lang3.builder.ToStringStyle;
import com.ruoyi.common.annotation.Excel;
import com.ruoyi.common.core.domain.BaseEntity;

/**
 * 制程对象 mes_process
 *
 * @author ruoyi
 * @date 2022-09-14
 */
public class MesProcess extends BaseEntity
{
    private static final long serialVersionUID = 1L;

    /** 制程id */
    private Long processId;

    /** 制程名称 */
    @Excel(name = "制程名称")
    private String processName;

    /** 制程类型 */
    @Excel(name = "制程类型")
    private String processType;

    /** 区段id */
    @Excel(name = "区段id")
    private Long stageId;

    /** 状态（0正常 1停用） */
    @Excel(name = "状态", readConverterExp = "0=正常,1=停用")
    private String status;

    public void setProcessId(Long processId)
    {
        this.processId = processId;
    }

    public Long getProcessId()
    {
        return processId;
    }

    public void setProcessName(String processName)
    {
        this.processName = processName;
    }

    public String getProcessName()
    {
        return processName;
    }

    public void setProcessType(String processType)
    {
        this.processType = processType;
    }

    public String getProcessType()
    {
        return processType;
    }

    public void setStageId(Long stageId)
    {
        this.stageId = stageId;
    }

    public Long getStageId()
    {
        return stageId;
    }

    public void setStatus(String status)
    {
        this.status = status;
    }

    public String getStatus()
    {
        return status;
    }

    @Excel(name = "区段名称")
    private String stageName;

    public String getStageName() {
        return stageName;
    }

    public void setStageName(String stageName) {
        this.stageName = stageName;
    }
    //    private MesStage stage;
//
//    public MesStage getStage() {
//        return stage;
//    }
//
//    public void setStage(MesStage stage) {
//        this.stage = stage;
//    }

    @Override
    public String toString() {
        return new ToStringBuilder(this,ToStringStyle.MULTI_LINE_STYLE)
            .append("processId", getProcessId())
            .append("processName", getProcessName())
            .append("processType", getProcessType())
            .append("stageId", getStageId())
            .append("status", getStatus())
            .append("remark", getRemark())
            .append("createBy", getCreateBy())
            .append("createTime", getCreateTime())
            .append("updateBy", getUpdateBy())
            .append("updateTime", getUpdateTime())
            .toString();
    }


    public boolean isAdmin()
    {
        return isAdmin(this.processId);
    }

    public static boolean isAdmin(Long processId)
    {
        return processId != null && 1L == processId;
    }
}
