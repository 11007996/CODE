package com.ruoyi.product.domain;

import org.apache.commons.lang3.builder.ToStringBuilder;
import org.apache.commons.lang3.builder.ToStringStyle;
import com.ruoyi.common.annotation.Excel;
import com.ruoyi.common.core.domain.BaseEntity;

/**
 * 工站对象 mes_terminal
 *
 * @author ruoyi
 * @date 2022-09-14
 */
public class MesTerminal extends BaseEntity
{
    private static final long serialVersionUID = 1L;

    /** MAC/工站id */
    private Long terminalId;

    /** MAC/工站名称 */
    @Excel(name = "工站名称")
    private String terminalName;

//    private String[] macList;
    private String[] macName;

    /** 次序 */
    @Excel(name = "次序")
    private int sequence;

    /** 线别id */
    @Excel(name = "线别id")
    private Long lineId;

    /** 线别名称 */
    @Excel(name = "线别名称")
    private String lineName;

    /** 区段id */
    @Excel(name = "区段id")
    private Long stageId;

    /** 区段名称 */
    @Excel(name = "区段名称")
    private String stageName;

    /** 制程id */
    @Excel(name = "制程id")
    private Long processId;

    /** 制程名称 */
    @Excel(name = "制程名称")
    private String processName;

    private int terminalNum;

    public void setTerminalNum(int terminalNum) {
        this.terminalNum = terminalNum;
    }

    public int getTerminalNum() {
        return terminalNum;
    }

    /** 状态（0正常 1停用） */
    @Excel(name = "状态", readConverterExp = "0=正常,1=停用")
    private String status;

    public void setTerminalId(Long terminalId)
    {
        this.terminalId = terminalId;
    }

    public Long getTerminalId()
    {
        return terminalId;
    }

    public void setTerminalName(String terminalName)
    {
        this.terminalName = terminalName;
    }
    public String getTerminalName()
    {
        return terminalName;
    }

    public String[] getMacName() {
        return macName;
    }

    public void setMacName(String[] macName) {
        this.macName = macName;
    }

    public int getSequence() {
        return sequence;
    }

    public void setSequence(int sequence) {
        this.sequence = sequence;
    }

    public void setLineId(Long lineId)
    {
        this.lineId = lineId;
    }

    public Long getLineId()
    {
        return lineId;
    }

    public void setLineName(String lineName) {
        this.lineName = lineName;
    }

    public String getLineName() {
        return lineName;
    }

    public void setStageId(Long stageId)
    {
        this.stageId = stageId;
    }

    public Long getStageId()
    {
        return stageId;
    }

    public void setStageName(String stageName) {
        this.stageName = stageName;
    }

    public String getStageName() {
        return stageName;
    }

    public void setProcessId(Long processId)
    {
        this.processId = processId;
    }

    public Long getProcessId()
    {
        return processId;
    }

    public void setProcessName(String processName) {
        this.processName = processName;
    }

    public String getProcessName() {
        return processName;
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
            .append("terminalId", getTerminalId())
            .append("terminalName", getTerminalName())
//            .append("macList", getMacList())
            .append("macName", getMacName())
            .append("sequence",getSequence())
            .append("lineId", getLineId())
            .append("lineName", getLineName())
            .append("stageId", getStageId())
            .append("stageName", getStageName())
            .append("processId", getProcessId())
            .append("processName", getProcessName())
            .append("status", getStatus())
            .append("remark", getRemark())
            .append("createBy", getCreateBy())
            .append("createTime", getCreateTime())
            .append("updateBy", getUpdateBy())
            .append("updateTime", getUpdateTime())
            .toString();
    }
}
