package com.ruoyi.product.domain;

import com.ruoyi.common.annotation.Excel;
import com.ruoyi.common.core.domain.BaseEntity;
import lombok.Data;

import java.util.List;

/**
 * sop配置对象 mes_terminal_sop
 *
 * @author ruoyi
 * @date 2022-09-21
 */
@Data
public class MesTerminalSop extends BaseEntity
{
    private static final long serialVersionUID = 1L;

    /** id */
    private Long id;

    /** sop id */
    private Long sopId;

    /** sop名称 */
    private String sopName;

    /** 机种 id */
    @Excel(name = "机种 id")
    private Long modelId;

    @Excel(name = "机种名称")
    private String modelName;

    /** 线别 id */
    @Excel(name = "线别 id")
    private Long lineId;

    @Excel(name = "线别名称")
    private String lineName;

    /** 区段 id */
    @Excel(name = "区段 id")
    private Long stageId;

    @Excel(name = "区段名称")
    private String stageName;

    /** 制程 id */
    @Excel(name = "制程 id")
    private Long processId;

    @Excel(name = "制程名称")
    private String processName;

    /** 工站 id */
    @Excel(name = "工站 id")
    private Long terminalId;

    @Excel(name = "工站名称")
    private String terminalName;

    /** mac地址id */
    private Long macId;

    /** mac地址名称 */
    private String macName;


    /** 料号 */
    private Long materialId;
    private String  materialName;
    private String  version;


    private List<MesSopGroup> sopGroupList;

    /** pdf文件名称 */
    private String pdfSopName;

    /** mp4文件名称 */
    private String videoSopName;

//
//    /** 类型（0 pdf 1 mp4） */
//    private String type;
//
    private  String sopPage;
    private  String interval;
    private  String passWord;



    /** 状态（0正常 1停用） */
    @Excel(name = "状态", readConverterExp = "0=正常,1=停用")
    private String status;

    public Long getId() {
        return id;
    }

    public void setId(Long id) {
        this.id = id;
    }

    public Long getSopId() {
        return sopId;
    }

    public void setSopId(Long sopId) {
        this.sopId = sopId;
    }

    public String getSopName() {
        return sopName;
    }

    public void setSopName(String sopName) {
        this.sopName = sopName;
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

    public String getMacName() {
        return macName;
    }

    public void setMacName(String macName) {
        this.macName = macName;
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

    public List<MesSopGroup> getSopGroupList() {
        return sopGroupList;
    }

    public void setSopGroupList(List<MesSopGroup> sopGroupList) {
        this.sopGroupList = sopGroupList;
    }

//    public String getType() {
//        return type;
//    }
//
//    public void setType(String type) {
//        this.type = type;
//    }


    public Long getMacId() {
        return macId;
    }

    public void setMacId(Long macId) {
        this.macId = macId;
    }

    public String getPdfSopName() {
        return pdfSopName;
    }

    public void setPdfSopName(String pdfSopName) {
        this.pdfSopName = pdfSopName;
    }

    public String getVideoSopName() {
        return videoSopName;
    }

    public void setVideoSopName(String videoSopName) {
        this.videoSopName = videoSopName;
    }

    public String getSopPage() {
        return sopPage;
    }

    public void setSopPage(String sopPage) {
        this.sopPage = sopPage;
    }

    public String getPassWord() {
        return passWord;
    }

    public void setPassWord(String passWord) {
        this.passWord = passWord;
    }

    @Override
    public String toString() {
        return "MesTerminalSop{" +
                "id=" + id +
                ", sopId=" + sopId +
                ", sopName=" + sopName + '\'' +
                ", modelId=" + modelId +
                ", modelName='" + modelName + '\'' +
                ", lineId=" + lineId +
                ", lineName='" + lineName + '\'' +
                ", stageId=" + stageId +
                ", stageName='" + stageName + '\'' +
                ", processId=" + processId +
                ", processName='" + processName + '\'' +
                ", terminalId=" + terminalId +
                ", terminalName='" + terminalName + '\'' +
                ", macId=" + macId +
                ", macName='" + macName + '\'' +
                ", materialId=" + materialId +
                ", status='" + status + '\'' +
                ", sopGroupList=" + sopGroupList + '\'' +
//                ", type=" + type +
                ", macId=" + macId +
                ", passWord='" + passWord + '\'' +
                '}';
    }
}

