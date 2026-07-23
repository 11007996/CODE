package com.ruoyi.product.domain;

import com.ruoyi.common.annotation.Excel;
import com.ruoyi.common.core.domain.BaseEntity;

/**
 * sop配置对象 mes_terminal_sop
 *
 * @author ruoyi
 * @date 2022-09-21
 */
public class MesTerminalSopHt extends BaseEntity
{
    private static final long serialVersionUID = 1L;

    /** id */
    private Long id;

    /** 机种 id */
    @Excel(name = "机种 id")
    private Long modelId;

    /** 线别 id */
    @Excel(name = "线别 id")
    private Long lineId;

    /** 区段 id */
    @Excel(name = "区段 id")
    private Long stageId;

    /** 制程 id */
    @Excel(name = "制程 id")
    private Long processId;

    /** 工站 id */
    @Excel(name = "工站 id")
    private Long terminalId;

    /** sop id */
    @Excel(name = "sop id")
    private Long sopId;

    @Excel(name = "SOP版本")
    private String version;

    /** 状态（0正常 1停用） */
    @Excel(name = "状态", readConverterExp = "0=正常,1=停用")
    private String status;

    public Long getId() {
        return id;
    }

    public void setId(Long id) {
        this.id = id;
    }

    public Long getModelId() {
        return modelId;
    }

    public void setModelId(Long modelId) {
        this.modelId = modelId;
    }

    public Long getLineId() {
        return lineId;
    }

    public void setLineId(Long lineId) {
        this.lineId = lineId;
    }

    public Long getStageId() {
        return stageId;
    }

    public void setStageId(Long stageId) {
        this.stageId = stageId;
    }

    public Long getProcessId() {
        return processId;
    }

    public void setProcessId(Long processId) {
        this.processId = processId;
    }

    public Long getTerminalId() {
        return terminalId;
    }

    public void setTerminalId(Long terminalId) {
        this.terminalId = terminalId;
    }

    public Long getSopId() {
        return sopId;
    }

    public void setSopId(Long sopId) {
        this.sopId = sopId;
    }

    public String getVersion() {
        return version;
    }

    public void setVersion(String version) {
        this.version = version;
    }

    public String getStatus() {
        return status;
    }

    public void setStatus(String status) {
        this.status = status;
    }


}
