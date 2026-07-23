package com.ruoyi.product.domain;

import com.ruoyi.common.core.domain.BaseEntity;

import java.util.List;

public class MesMac extends BaseEntity {
    /** 线别id */
    private Long lineId;
    private String lineName;
    /** 区段id */
    private Long stageId;
    private String stageName;
    /** 制程id */
    private Long processId;
    private String processName;
    /** mac自增id */
    private Long macId;

    /** mac地址名称 */
    private String macName;
    private List<MesMac> macList;

    /** mac地址所在区域 */
    private String macArea;

    /** 状态（0正常 1停用） */
//    @Excel(name = "状态", readConverterExp = "0=正常,1=停用")
    private String status;

    /** 备注 */
    private String remark;
    private String delFlag;

    public Long getLineId() {
        return lineId;
    }

    public void setLineId(Long lineId) {
        this.lineId = lineId;
    }

    public String getLineName() {
        return lineName;
    }

    public void setLineName(String lineName) {
        this.lineName = lineName;
    }

    public Long getStageId() {
        return stageId;
    }

    public void setStageId(Long stageId) {
        this.stageId = stageId;
    }

    public String getStageName() {
        return stageName;
    }

    public void setStageName(String stageName) {
        this.stageName = stageName;
    }

    public Long getProcessId() {
        return processId;
    }

    public void setProcessId(Long processId) {
        this.processId = processId;
    }

    public String getProcessName() {
        return processName;
    }

    public void setProcessName(String processName) {
        this.processName = processName;
    }

    public Long getMacId() {
        return macId;
    }

    public void setMacId(Long macId) {
        this.macId = macId;
    }

    public String getMacName() {
        return macName;
    }

    public void setMacName(String macName) {
        this.macName = macName;
    }

    public List<MesMac> getMacList() {
        return macList;
    }

    public void setMacList(List<MesMac> macList) {
        this.macList = macList;
    }

    public String getMacArea() {
        return macArea;
    }

    public void setMacArea(String macArea) {
        this.macArea = macArea;
    }

    public String getStatus() {
        return status;
    }

    public void setStatus(String status) {
        this.status = status;
    }

    public String getDelFlag() {
        return delFlag;
    }

    public void setDelFlag(String delFlag) {
        this.delFlag = delFlag;
    }

    @Override
    public String getRemark() {
        return remark;
    }

    @Override
    public void setRemark(String remark) {
        this.remark = remark;
    }
}
