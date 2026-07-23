package com.ruoyi.product.domain;

import org.apache.commons.lang3.builder.ToStringBuilder;
import org.apache.commons.lang3.builder.ToStringStyle;
import com.ruoyi.common.annotation.Excel;
import com.ruoyi.common.core.domain.BaseEntity;

/**
 * 区段管理对象 mes_stage
 *
 * @author ruoyi
 * @date 2022-09-14
 */
public class MesStage extends BaseEntity
{
    private static final long serialVersionUID = 1L;

    /** 区段id */
    private Long stageId;

    /** 区段名称 */
    @Excel(name = "区段名称")
    private String stageName;

    /** 状态（0正常 1停用） */
    @Excel(name = "状态", readConverterExp = "0=正常,1=停用")
    private String status;

    public void setStageId(Long stageId)
    {
        this.stageId = stageId;
    }

    public Long getStageId()
    {
        return stageId;
    }

    public boolean isAdmin()
    {
        return isAdmin(this.stageId);
    }
    public static boolean isAdmin(Long stageId)
    {
        return stageId != null && 1L == stageId;
    }

    public void setStageName(String stageName)
    {
        this.stageName = stageName;
    }

    public String getStageName()
    {
        return stageName;
    }
    public void setStatus(String status)
    {
        this.status = status;
    }

    public String getStatus()
    {
        return status;
    }

    @Override
    public String toString() {
        return new ToStringBuilder(this,ToStringStyle.MULTI_LINE_STYLE)
            .append("stageId", getStageId())
            .append("stageName", getStageName())
            .append("status", getStatus())
            .append("remark", getRemark())
            .append("createBy", getCreateBy())
            .append("createTime", getCreateTime())
            .append("updateBy", getUpdateBy())
            .append("updateTime", getUpdateTime())
            .toString();
    }
}
