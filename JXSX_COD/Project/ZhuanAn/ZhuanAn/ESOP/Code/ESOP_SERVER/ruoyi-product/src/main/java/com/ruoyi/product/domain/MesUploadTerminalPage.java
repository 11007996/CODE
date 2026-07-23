package com.ruoyi.product.domain;

import com.ruoyi.common.core.domain.BaseEntity;
import lombok.Data;

@Data
public class MesUploadTerminalPage extends BaseEntity {
    private String oaId;
    private Long sopId;

    private String sopName;

    private String version;

    /** 料号 */
    private Long materialId;
    private String materialName;
    /** 机种 id */
    private Long modelId;
    private String modelName;

    /** 线别 id */
    private Long lineId;
    private String lineName;

    /** 区段 id */
    private Long stageId;
    private String stageName;

    /** 制程 id */
    private Long processId;
    private String processName;

    /** sop页码 */
    private String sopPage;

    private String type;

    private String status;

    private Long terminalId;
    private String terminalName;

    private String sopInterval;

    private String versionStatus;
    private Long macId;

    private String pushStatus;
    private String project;

    @Override
    public String toString() {
        return "MesUploadTerminalPage{" +
                "oaId='" + oaId +
                ", sopId=" + sopId +
                ", sopName='" + sopName + '\'' +
                ", version='" + version + '\'' +
                ", materialId=" + materialId +
                ", materialName='" + materialName + '\'' +
                ", modelId=" + modelId +
                ", modelName='" + modelName + '\'' +
                ", lineId=" + lineId +
                ", lineName='" + lineName + '\'' +
                ", stageId=" + stageId +
                ", stageName='" + stageName + '\'' +
                ", processId=" + processId +
                ", processName='" + processName + '\'' +
                ", sopPage='" + sopPage + '\'' +
                ", type='" + type + '\'' +
                ", status='" + status + '\'' +
                ", terminalId=" + terminalId +
                ", terminalName='" + terminalName + '\'' +
                ", sopInterval='" + sopInterval +
                ", versionStatus='" + versionStatus + '\'' +
                ", macId=" + macId +
                ", pushStatus='" + pushStatus + '\'' +
                '}';
    }

    public String toJSONString() {
        return "{" +
                "\"model\":\"" + modelName + '\"' +
                ", \"partNo\":\"" + materialName + '\"' +
                ", \"line\":\"" + lineName + '\"' +
                ", \"STAGE\":\"" + stageName + '\"' +
                ", \"stationType\":\"" + processName + '\"' +
                ", \"stationName\":\"" + terminalName  + '\"' +
                ", \"fileName\":\"" + sopName + '\"' +
                ", \"fileVersion\":\"" + version + '\"' +
                ", \"filepage\":\"" + sopPage + '\"' +
                '}';
    }
}
