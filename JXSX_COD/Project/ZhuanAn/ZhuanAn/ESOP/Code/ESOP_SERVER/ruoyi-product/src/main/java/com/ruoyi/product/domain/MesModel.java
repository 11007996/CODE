package com.ruoyi.product.domain;

import org.apache.commons.lang3.builder.ToStringBuilder;
import org.apache.commons.lang3.builder.ToStringStyle;
import com.ruoyi.common.annotation.Excel;
import com.ruoyi.common.core.domain.BaseEntity;

/**
 * 机种信息对象 mes_model
 *
 * @author ruoyi
 * @date 2022-09-14
 */
public class MesModel extends BaseEntity
{
    private static final long serialVersionUID = 1L;

    /** 机种id */
    private Long modelId;

    /** 机种名称 */
    @Excel(name = "机种名称")
    private String modelName;

    /** 机种状态（0正常 1停用） */
    @Excel(name = "机种状态", readConverterExp = "0=正常,1=停用")
    private String status;

    public void setModelId(Long modelId)
    {
        this.modelId = modelId;
    }

    public Long getModelId()
    {
        return modelId;
    }

    public boolean isAdmin()
    {
        return isAdmin(this.modelId);
    }
    public static boolean isAdmin(Long modelId)
    {
        return modelId != null && 1L == modelId;
    }


    public void setModelName(String modelName)
    {
        this.modelName = modelName;
    }

    public String getModelName()
    {
        return modelName;
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
            .append("modelId", getModelId())
            .append("modelName", getModelName())
            .append("status", getStatus())
            .append("remark", getRemark())
            .append("createBy", getCreateBy())
            .append("createTime", getCreateTime())
            .append("updateBy", getUpdateBy())
            .append("updateTime", getUpdateTime())
            .toString();
    }
}
