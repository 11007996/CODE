package com.ruoyi.replace.domain;

import org.apache.commons.lang3.builder.ToStringBuilder;
import org.apache.commons.lang3.builder.ToStringStyle;
import com.ruoyi.common.annotation.Excel;
import com.ruoyi.common.core.domain.BaseEntity;

/**
 * 物料基础信息对象 mes_material_info
 *
 * @author ruoyi
 * @date 2023-01-06
 */
public class MesMaterialInfo extends BaseEntity
{
    private static final long serialVersionUID = 1L;

    /** 物料id */
    private Long id;

    /** 物料描述 */
    @Excel(name = "物料描述")
    private String materialName;

    /** 料号描述 */
    @Excel(name = "料号描述")
    private String materialDesc;

    /** 机种id */
    @Excel(name = "机种id")
    private Long modelId;

    /** 机种名称 */
    private String modelName;

    public void setId(Long id)
    {
        this.id = id;
    }

    public Long getId()
    {
        return id;
    }
    public void setMaterialName(String materialName)
    {
        this.materialName = materialName;
    }

    public String getMaterialName()
    {
        return materialName;
    }
    public void setMaterialDesc(String materialDesc)
    {
        this.materialDesc = materialDesc;
    }

    public String getMaterialDesc()
    {
        return materialDesc;
    }
    public void setModelId(Long modelId)
    {
        this.modelId = modelId;
    }

    public Long getModelId()
    {
        return modelId;
    }

    public String getModelName() {
        return modelName;
    }

    public void setModelName(String modelName) {
        this.modelName = modelName;
    }



    @Override
    public String toString() {
        return new ToStringBuilder(this,ToStringStyle.MULTI_LINE_STYLE)
                .append("id", getId())
                .append("materialName", getMaterialName())
                .append("materialDesc", getMaterialDesc())
                .append("modelId", getModelId())
                .append("modelName", getModelName())
                .append("createBy", getCreateBy())
                .append("createTime", getCreateTime())
                .append("updateBy", getUpdateBy())
                .append("updateTime", getUpdateTime())
                .toString();
    }
}
