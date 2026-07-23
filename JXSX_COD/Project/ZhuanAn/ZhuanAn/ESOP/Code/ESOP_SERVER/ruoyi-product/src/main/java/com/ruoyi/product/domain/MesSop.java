package com.ruoyi.product.domain;

import org.apache.commons.lang3.builder.ToStringBuilder;
import org.apache.commons.lang3.builder.ToStringStyle;
import com.ruoyi.common.annotation.Excel;
import com.ruoyi.common.core.domain.BaseEntity;

/**
 * sop对象 mes_sop
 *
 * @author ruoyi
 * @date 2022-09-21
 */
public class MesSop extends BaseEntity
{
    private static final long serialVersionUID = 1L;

    /** sop id */
    private Long sopId;

    /** sop名称 */
    @Excel(name = "sop名称")
    private String sopName;

    /** 版本 */
    @Excel(name = "版本")
    private String version;

    /** 翻页时间间隔 */
    private String sopInterval;

    /** 类型（0 pdf 1 mp4） */
    @Excel(name = "类型", readConverterExp = "0=,p=df,1=,m=p4")
    private String type;

    /** 文件保存路径 */
    @Excel(name = "文件保存路径")
    private String filePath;

    /** 状态（0正常 1停用） */
    @Excel(name = "状态", readConverterExp = "0=正常,1=停用")
    private String status;

    /** 原文件名称 */
    @Excel(name = "原文件名称")
    private String originalName;

    private String url;

    /** 工站ID */
    private Long terminalId;

    private String passWord;

    public String getPassWord() {
        return passWord;
    }

    public void setPassWord(String passWord) {
        this.passWord = passWord;
    }

    public String getUrl() {
        return url;
    }

    public void setUrl(String url) {
        this.url = url;
    }

    public void setOriginalName(String originalName) {
        this.originalName = originalName;
    }

    public String getOriginalName() {
        return originalName;
    }

    public void setSopId(Long sopId)
    {
        this.sopId = sopId;
    }

    public Long getSopId()
    {
        return sopId;
    }
    public void setSopName(String sopName)
    {
        this.sopName = sopName;
    }

    public String getSopName()
    {
        return sopName;
    }
    public void setVersion(String version)
    {
        this.version = version;
    }

    public String getVersion()
    {
        return version;
    }

    public String getSopInterval() {
        return sopInterval;
    }

    public void setSopInterval(String sopInterval) {
        this.sopInterval = sopInterval;
    }

    public void setType(String type)
    {
        this.type = type;
    }

    public String getType()
    {
        return type;
    }

    public void setFilePath(String filePath)
    {
        this.filePath = filePath;
    }

    public String getFilePath()
    {
        return filePath;
    }

    public void setStatus(String status)
    {
        this.status = status;
    }

    public String getStatus()
    {
        return status;
    }

    public Long getTerminalId() {
        return terminalId;
    }

    public void setTerminalId(Long terminalId) {
        this.terminalId = terminalId;
    }

    @Override
    public String toString() {
        return new ToStringBuilder(this,ToStringStyle.MULTI_LINE_STYLE)
            .append("sopId", getSopId())
            .append("sopName", getSopName())
            .append("version", getVersion())
            .append("interval", getSopInterval())
            .append("type", getType())
            .append("filePath", getFilePath())
            .append("url", getUrl())
            .append("status", getStatus())
            .append("remark", getRemark())
            .append("createBy", getCreateBy())
            .append("createTime", getCreateTime())
            .append("updateBy", getUpdateBy())
            .append("updateTime", getUpdateTime())
            .append("passWord", getPassWord())
            .toString();
    }
}
