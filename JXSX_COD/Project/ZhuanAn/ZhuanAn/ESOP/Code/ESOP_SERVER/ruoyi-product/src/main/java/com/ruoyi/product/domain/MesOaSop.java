package com.ruoyi.product.domain;

import com.ruoyi.common.annotation.Excel;
import com.ruoyi.common.core.domain.BaseEntity;

import java.util.List;
import java.util.Map;

/**
 * oa签核对象 mes_oa_sop
 *
 * @author ruoyi
 * @date 2022-09-23
 */
public class MesOaSop extends BaseEntity
{
    private static final long serialVersionUID = 1L;

    @Excel(name = "id")
    private Long id;

    /** oa id */
    @Excel(name = "oa id")
    private String oaId;

    /** request id */
    @Excel(name = "request id")
    private String requestId;

    /** sop id */
    @Excel(name = "sop id")
    private Long sopId;

    /** sop name */
    @Excel(name = "sop name")
    private String sopName;


    /** 料号 */
    private Long materialId;

    private String materialName;

    private Long terminalId;

    private String terminalName;

    /** sop文件版本 */
    private String version;

    /** sop文件描述 */
    private String remark;

    /** 翻页时间间隔 */
    private String sopInterval;


    /** 状态（0送签中 1成功 2失败） */
    @Excel(name = "状态", readConverterExp = "0=送签中,1=成功,2=失败")
    private String status;

    /** 机种id */
    @Excel(name = "机种id")
    private Long modelId;


    @Excel(name = "机种名称")
    private String modelName;

    /** 线别id */
    @Excel(name = "线别id")
    private Long lineId;

    @Excel(name = "线别名称")
    private String lineName;

    /** 区段Id */
    @Excel(name = "区段Id")
    private Long stageId;

    @Excel(name = "区段名称")
    private String stageName;

    /** 制程Id */
    @Excel(name = "制程Id")
    private Long processId;

    private String processName;

    @Excel(name = "会签人员工号")
    private String countersignUser;

    private String notifyUser;

    private String type;
    private String filePath;

    private List<MesUploadTerminalPage> terminalPageList;
    /** sop的操作状态 */
    private String operateStatus;

    /** 发起签核人名称 */
    private String createByName;
    private String passWord;
    private List<Map<String,Float>> pdfSizeList;

    public String getCountersignUser() {
        return countersignUser;
    }

    public void setCountersignUser(String countersignUser) {
        this.countersignUser = countersignUser;
    }

    public Long getId() {
        return id;
    }

    public void setId(Long id) {
        this.id = id;
    }

    public String getVersion() {
        return version;
    }

    public void setVersion(String version) {
        this.version = version;
    }

    public String getSopInterval() {
        return sopInterval;
    }

    public void setSopInterval(String sopInterval) {
        this.sopInterval = sopInterval;
    }

    public String getOaId() {
        return oaId;
    }

    public void setOaId(String oaId) {
        this.oaId = oaId;
    }

    public String getRequestId() {
        return requestId;
    }

    public void setRequestId(String requestId) {
        this.requestId = requestId;
    }

    public Long getSopId() {
        return sopId;
    }

    public void setSopId(Long sopId) {
        this.sopId = sopId;
    }

    public Long getMaterialId() {
        return materialId;
    }

    public void setMaterialId(Long materialId) {
        this.materialId = materialId;
    }

    public String getMaterialName() {
        return materialName;
    }

    public void setMaterialName(String materialName) {
        this.materialName = materialName;
    }

    public Long getTerminalId() {
        return terminalId;
    }

    public void setTerminalId(Long terminalId) {
        this.terminalId = terminalId;
    }

    public String getTerminalName() {
        return terminalName;
    }

    public void setTerminalName(String terminalName) {
        this.terminalName = terminalName;
    }

    public String getSopName() {
        return sopName;
    }

    public void setSopName(String sopName) {
        this.sopName = sopName;
    }

    public String getStatus() {
        return status;
    }

    public void setStatus(String status) {
        this.status = status;
    }

    public Long getModelId() {
        return modelId;
    }

    public void setModelId(Long modelId) {
        this.modelId = modelId;
    }

    public String getModelName() {
        return modelName;
    }

    public void setModelName(String modelName) {
        this.modelName = modelName;
    }

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

    public String getNotifyUser() {
        return notifyUser;
    }

    public void setNotifyUser(String notifyUser) {
        this.notifyUser = notifyUser;
    }

    public String getType() {
        return type;
    }

    public void setType(String type) {
        this.type = type;
    }


    @Override
    public String getRemark() {
        return remark;
    }

    @Override
    public void setRemark(String remark) {
        this.remark = remark;
    }

    public String getFilePath() {
        return filePath;
    }

    public void setFilePath(String filePath) {
        this.filePath = filePath;
    }

    public List<MesUploadTerminalPage> getTerminalPageList() {
        return terminalPageList;
    }

    public void setTerminalPageList(List<MesUploadTerminalPage> terminalPageList) {
        this.terminalPageList = terminalPageList;
    }

    public String getOperateStatus() {
        return operateStatus;
    }

    public void setOperateStatus(String operateStatus) {
        this.operateStatus = operateStatus;
    }

    public String getCreateByName() {
        return createByName;
    }

    public void setCreateByName(String createByName) {
        this.createByName = createByName;
    }

    public String getPassWord() {
        return passWord;
    }

    public void setPassWord(String passWord) {
        this.passWord = passWord;
    }

    public List<Map<String, Float>> getPdfSizeList() {
        return pdfSizeList;
    }

    public void setPdfSizeList(List<Map<String, Float>> pdfSizeList) {
        this.pdfSizeList = pdfSizeList;
    }

    @Override
    public String toString() {
        return "MesOaSop{" +
                "id=" + id +
                ", oaId='" + oaId + '\'' +
                ", requestId='" + requestId + '\'' +
                ", sopId=" + sopId +
                ", sopName='" + sopName + '\'' +
                ", materialId=" + materialId +
                ", materialName='" + materialName + '\'' +
                ", terminalId=" + terminalId +
                ", terminalName='" + terminalName + '\'' +
                ", version='" + version + '\'' +
                ", remark='" + remark + '\'' +
                ", sopInterval='" + sopInterval + '\'' +
                ", status='" + status + '\'' +
                ", modelId=" + modelId +
                ", modelName='" + modelName + '\'' +
                ", lineId=" + lineId +
                ", lineName='" + lineName + '\'' +
                ", stageId=" + stageId +
                ", stageName='" + stageName + '\'' +
                ", processId=" + processId +
                ", processName='" + processName + '\'' +
                ", countersignUser='" + countersignUser + '\'' +
                ", notifyUser='" + notifyUser + '\'' +
                ", type='" + type + '\'' +
                ", filePath='" + filePath + '\'' +
                ", terminalPageList=" + terminalPageList +
                ", operateStatus='" + operateStatus + '\'' +
                ", createByName='" + createByName + '\'' +
                ", passWord='" + passWord + '\'' +
                ", pdfSizeList=" + pdfSizeList +
                '}';
    }
}
