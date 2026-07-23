package com.ruoyi.web.controller.product.JsonObjectLuxlink;

public class JsonObjectLuxLinkDetailBoItem {

    private String partNo;
    private String model;
    private String line;
    private String stage;
    private String stationType;
    private String stationName;
    private String fileVersion;
    private String fileName;
    private String fiePage;

    public String getPartNo() {
        return partNo;
    }

    public void setPartNo(String partNo) {
        this.partNo = partNo;
    }

    public String getModel() {
        return model;
    }

    public void setModel(String model) {
        this.model = model;
    }

    public String getLine() {
        return line;
    }

    public void setLine(String line) {
        this.line = line;
    }

    public String getStage() {
        return stage;
    }

    public void setStage(String stage) {
        this.stage = stage;
    }

    public String getStationType() {
        return stationType;
    }

    public void setStationType(String stationType) {
        this.stationType = stationType;
    }

    public String getStationName() {
        return stationName;
    }

    public void setStationName(String stationName) {
        this.stationName = stationName;
    }

    public String getFileVersion() {
        return fileVersion;
    }

    public void setFileVersion(String fileVersion) {
        this.fileVersion = fileVersion;
    }

    public String getFileName() {
        return fileName;
    }

    public void setFileName(String fileName) {
        this.fileName = fileName;
    }

    public String getFiePage() {
        return fiePage;
    }

    public void setFiePage(String fiePage) {
        this.fiePage = fiePage;
    }


    public String toJsonString(){
        return "{" +
                "partNo:'" + partNo + '\'' +
                ", model:'" + model + '\'' +
                ", line:'" + line + '\'' +
                ", stage='" + stage + '\'' +
                ", stationType:'" + stationType + '\'' +
                ", stationName:'" + stationName + '\'' +
                ", fileVersion:" + fileVersion + '\'' +
                ", fileName:'" + fileName + '\'' +
                ", fiePage:'" + fiePage + '\'' +
                '}';
    }
}
