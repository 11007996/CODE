package com.ruoyi.product.domain;

import lombok.Data;
import org.apache.commons.lang3.builder.ToStringBuilder;
import org.apache.commons.lang3.builder.ToStringStyle;
import com.ruoyi.common.annotation.Excel;
import com.ruoyi.common.core.domain.BaseEntity;

/**
 * 印章对象 mes_seal
 *
 * @author ruoyi
 * @date 2024-04-18
 */
@Data
public class MesSeal extends BaseEntity
{
    private static final long serialVersionUID = 1L;

    /** 印章id */
    private Long sealId;

    /** 原文件名 */
    private String originalName;

    /** 印章名称 */
    @Excel(name = "印章名称")
    private String sealName;

    /** 文件保存路径 */
    @Excel(name = "文件保存路径")
    private String filePath;

    /** 印章类型 */
    @Excel(name = "印章类型")
    private String type;

    /** 印章状态（0正常 1停用） */
    @Excel(name = "印章状态", readConverterExp = "0=正常,1=停用")
    private String status;

    /** 保管人工号 */
    @Excel(name = "保管人工号")
    private String custodianNo;

    /** 厂区id */
    @Excel(name = "厂区id")
    private Long deptId;

    /** 删除标志（0代表存在 2代表删除） */
    private String delFlag;

    // ===================== 手动 Getter 方法 =====================
    public Long getSealId() {
        return sealId;
    }

    public String getOriginalName() {
        return originalName;
    }

    public String getSealName() {
        return sealName;
    }

    public String getFilePath() {
        return filePath;
    }

    public String getType() {
        return type;
    }

    public String getStatus() {
        return status;
    }

    public String getCustodianNo() {
        return custodianNo;
    }

    public Long getDeptId() {
        return deptId;
    }

    public String getDelFlag() {
        return delFlag;
    }

    @Override
    public String toString() {
        return new ToStringBuilder(this,ToStringStyle.MULTI_LINE_STYLE)
                .append("sealId", getSealId())
                .append("originalName", getOriginalName())
                .append("sealName", getSealName())
                .append("filePath", getFilePath())
                .append("type", getType())
                .append("status", getStatus())
                .append("custodianNo", getCustodianNo())
                .append("deptId", getDeptId())
                .append("createBy", getCreateBy())
                .append("createTime", getCreateTime())
                .append("updateBy", getUpdateBy())
                .append("updateTime", getUpdateTime())
                .append("delFlag", getDelFlag())
                .toString();
    }
}
