package com.ruoyi.product.domain;

import com.ruoyi.common.core.domain.BaseEntity;
import lombok.Data;

import java.util.List;
import java.util.Map;
@Data
public class MesSopGroup extends BaseEntity {

    private String sopGroupId;

    /** sop id */
    private Long sopId;

    private String sopName;
    private String pdfSopName;
    private String videoSopName;

    /** sop页码 */
    private String sopPage;

    private String sopUrl;

    private String filePath;

    /** 类型（0 pdf 1 mp4） */
    private String type;

    private Long typeNum;
    private String sopInterval;

    private String passWord;

    private String pushStatus;
    private List<Map<String,Float>> pdfSizeList;

    public String getPassWord() {
        return passWord;
    }

    public void setPassWord(String passWord) {
        this.passWord = passWord;
    }

    public String getSopGroupId() {
        return sopGroupId;
    }

    public void setSopGroupId(String sopGroupId) {
        this.sopGroupId = sopGroupId;
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

    public String getSopUrl() {
        return sopUrl;
    }

    public void setSopUrl(String sopUrl) {
        this.sopUrl = sopUrl;
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

    public Long getTypeNum() {
        return typeNum;
    }

    public void setTypeNum(Long typeNum) {
        this.typeNum = typeNum;
    }


    public String getPushStatus() {
        return pushStatus;
    }

    public void setPushStatus(String pushStatus) {
        this.pushStatus = pushStatus;
    }

    public List<Map<String, Float>> getPdfSizeList() {
        return pdfSizeList;
    }

    public void setPdfSizeList(List<Map<String, Float>> pdfSizeList) {
        this.pdfSizeList = pdfSizeList;
    }

    @Override
    public String toString() {
        return "MesSopGroup{" +
                "sopGroupId='" + sopGroupId + '\'' +
                ", sopId=" + sopId +
                ", sopName='" + sopName + '\'' +
                ", pdfSopName='" + pdfSopName + '\'' +
                ", videoSopName='" + videoSopName + '\'' +
                ", sopPage='" + sopPage + '\'' +
                ", sopUrl='" + sopUrl + '\'' +
                ", filePath='" + filePath + '\'' +
                ", type='" + type + '\'' +
                ", typeNum=" + typeNum +
                ", sopInterval=" + sopInterval +
                ", passWord='" + passWord + '\'' +
                ", pushStatus='" + pushStatus + '\'' +
                ", pdfSizeList=" + pdfSizeList +
                '}';
    }

//    public String toJSON() {
//        return "{" +
//                "sopGroupId:" + sopGroupId +
//                ", sopId:" + sopId +
//                ", sopPage:'" + sopPage + '\'' +
//                ", sopUrl:'" + sopUrl + '\'' +
//                '}';
//    }
}
