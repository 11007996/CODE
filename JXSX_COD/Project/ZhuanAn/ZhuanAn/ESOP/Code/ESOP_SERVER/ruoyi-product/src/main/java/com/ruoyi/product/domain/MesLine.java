package com.ruoyi.product.domain;

import com.ruoyi.common.annotation.Excel;
import com.ruoyi.common.core.domain.BaseEntity;
import org.apache.commons.lang3.builder.ToStringBuilder;
import org.apache.commons.lang3.builder.ToStringStyle;

import java.util.Arrays;

/**
 * 线别信息对象 mes_line
 *
 * @author ruoyi
 * @date 2022-09-14
 */
public class MesLine extends BaseEntity
{
    private static final long serialVersionUID = 1L;

    /** 线别id */
    private Long lineId;

    /** 线别名称 */
    @Excel(name = "线别名称")
    private String lineName;

    /** 线别类型 */
    @Excel(name = "线别类型")
    private String lineType;

    /** 工作中心 */
    @Excel(name = "工作中心")
    private String workCenter;

    /** 状态（0正常 1停用） */
    @Excel(name = "状态", readConverterExp = "0=正常,1=停用")
    private String status;

    /** 厂区id */
    private Long siteId;

    /** 厂区名称 */
    private String siteName;

    /** 料号id */
    private Long materialId;

    /** 料号名称 */
    private String materialName;


    private int[] materialIdList;
    private String[] materialNameList;

    /** 料号描述 */
    private String materialDesc;

    private  Long terminalId;

    private String terminalName;

    /** 用户id */
    private int userId;

    /** 用户工号 */
    private String userName;
    private int[] userIdList;


    public Long getSiteId() {
        return siteId;
    }

    public void setSiteId(Long siteId) {
        this.siteId = siteId;
    }

    public String getSiteName() {
        return siteName;
    }

    public void setSiteName(String siteName) {
        this.siteName = siteName;
    }

    public void setLineId(Long lineId)
    {
        this.lineId = lineId;
    }

    public Long getLineId()
    {
        return lineId;
    }

    public boolean isAdmin()
    {
        return isAdmin(this.lineId);
    }
    public static boolean isAdmin(Long lineId)
    {
        return lineId != null && 1L == lineId;
    }

    public void setLineName(String lineName)
    {
        this.lineName = lineName;
    }

    public String getLineName()
    {
        return lineName;
    }
    public void setLineType(String lineType)
    {
        this.lineType = lineType;
    }

    public String getLineType()
    {
        return lineType;
    }
    public void setWorkCenter(String workCenter)
    {
        this.workCenter = workCenter;
    }

    public String getWorkCenter()
    {
        return workCenter;
    }
    public void setStatus(String status)
    {
        this.status = status;
    }

    public String getStatus()
    {
        return status;
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

    public String getMaterialDesc() {
        return materialDesc;
    }

    public void setMaterialDesc(String materialDesc) {
        this.materialDesc = materialDesc;
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

    public int[] getMaterialIdList() {
        return materialIdList;
    }

    public void setMaterialIdList(int[] materialIdList) {
        this.materialIdList = materialIdList;
    }

    public String[] getMaterialNameList() {
        return materialNameList;
    }

    public void setMaterialNameList(String[] materialNameList) {
        this.materialNameList = materialNameList;
    }

    public int getUserId() {
        return userId;
    }

    public void setUserId(int userId) {
        this.userId = userId;
    }

    public String getUserName() {
        return userName;
    }

    public void setUserName(String userName) {
        this.userName = userName;
    }

    public int[] getUserIdList() {
        return userIdList;
    }

    public void setUserIdList(int[] userIdList) {
        this.userIdList = userIdList;
    }

    @Override
    public String toString() {
        return "MesLine{" +
                "lineId=" + lineId +
                ", lineName='" + lineName + '\'' +
                ", lineType='" + lineType + '\'' +
                ", workCenter='" + workCenter + '\'' +
                ", status='" + status + '\'' +
                ", siteId=" + siteId +
                ", siteName='" + siteName + '\'' +
                ", materialId=" + materialId +
                ", materialName='" + materialName + '\'' +
                ", materialIdList=" + Arrays.toString(materialIdList) +
                ", materialNameList=" + Arrays.toString(materialNameList) +
                ", materialDesc='" + materialDesc + '\'' +
                ", terminalId=" + terminalId +
                ", terminalName='" + terminalName + '\'' +
                ", userId=" + userId +
                ", userName='" + userName + '\'' +
                ", userIdList=" + Arrays.toString(userIdList) +
                '}';
    }

}
