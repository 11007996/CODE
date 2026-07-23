package com.ruoyi.replace.domain;

import com.ruoyi.common.core.domain.BaseEntity;
import lombok.Data;

import java.util.List;
import java.util.Map;

@Data
public class SopInfo extends BaseEntity {
//    private int modelId;
//
//    private int lineId;
//
//    private int stageId;
//
//    private int processId;

    private Long sopId;
    private String filePath;

    private String sopPage;
    private String sopInterval;

    private String type;

    private String passWord;

    private List<Map<String,Float>> pdfSizeList;
//    public int getModelId() {
//        return modelId;
//    }
//
//    public void setModelId(int modelId) {
//        this.modelId = modelId;
//    }
//
//    public int getLineId() {
//        return lineId;
//    }
//
//    public void setLineId(int lineId) {
//        this.lineId = lineId;
//    }
//
//    public int getStageId() {
//        return stageId;
//    }
//
//    public void setStageId(int stageId) {
//        this.stageId = stageId;
//    }
//
//    public int getProcessId() {
//        return processId;
//    }
//
//    public void setProcessId(int processId) {
//        this.processId = processId;
//    }


    public Long getSopId() {
        return sopId;
    }

    public void setSopId(Long sopId) {
        this.sopId = sopId;
    }

    public String getSopPage() {
        return sopPage;
    }

    public void setSopPage(String sopPage) {
        this.sopPage = sopPage;
    }

    public String getFilePath() {
        return filePath;
    }

    public void setFilePath(String filePath) {
        this.filePath = filePath;
    }

    public String getType() {
        return type;
    }

    public void setType(String type) {
        this.type = type;
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
        return "SopInfo{" +
                "sopId=" + sopId +
                ", filePath='" + filePath + '\'' +
                ", sopPage='" + sopPage + '\'' +
                ", type='" + type + '\'' +
                ", passWord='" + passWord + '\'' +
                ", pdfSizeList=" + pdfSizeList +
                '}';
    }
}
