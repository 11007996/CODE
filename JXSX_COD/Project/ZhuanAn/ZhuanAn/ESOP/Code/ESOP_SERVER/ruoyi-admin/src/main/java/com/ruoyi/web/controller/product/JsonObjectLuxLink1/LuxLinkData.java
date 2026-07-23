package com.ruoyi.web.controller.product.JsonObjectLuxLink1;

import com.ruoyi.common.core.domain.BaseEntity;
import lombok.Data;

import java.util.Arrays;
import java.util.List;

@Data
public class LuxLinkData extends BaseEntity {
    private String modelName;
    private String materialName;
    private List<String> lineName;
    private String stageName;
    private List<String> processName;
    private List<String> sopPage;
    private String sopInterval;
    private List<FileDetail> fileDetail;
    //项目名
    private String projectName;


    public static class FileDetail {
        private byte[] fileContent; // 假设我们使用byte数组来表示字节流
        private String fileName; // pdf文件名称 String
        private String fileVersion; // 文件版本 String
        private int type; // 文件类型 （0:pdf  1:mp4）
        private int sopStatus; // 文件状态 （0正常 1停用）

        public byte[] getFileContent() {
            return fileContent;
        }

        public void setFileContent(byte[] fileContent) {
            this.fileContent = fileContent;
        }

        public String getFileName() {
            return fileName;
        }

        public void setFileName(String fileName) {
            this.fileName = fileName;
        }

        public String getFileVersion() {
            return fileVersion;
        }

        public void setFileVersion(String fileVersion) {
            this.fileVersion = fileVersion;
        }

        public int getType() {
            return type;
        }

        public void setType(int type) {
            this.type = type;
        }

        public int getSopStatus() {
            return sopStatus;
        }

        public void setSopStatus(int sopStatus) {
            this.sopStatus = sopStatus;
        }

        @Override
        public String toString() {
            return "FileDetail{" +
                    "fileContent=" + Arrays.toString(fileContent) +
                    ", fileName='" + fileName + '\'' +
                    ", fileVersion='" + fileVersion + '\'' +
                    ", type=" + type +
                    ", sopStatus=" + sopStatus +
                    '}';
        }

    }
}

