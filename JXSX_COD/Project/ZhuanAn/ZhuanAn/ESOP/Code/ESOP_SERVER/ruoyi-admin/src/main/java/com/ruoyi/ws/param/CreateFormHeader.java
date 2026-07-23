package com.ruoyi.ws.param;

import com.fasterxml.jackson.annotation.JsonAutoDetect;
import com.fasterxml.jackson.annotation.JsonProperty;

/**
 * @author peng
 */
@JsonAutoDetect(getterVisibility = JsonAutoDetect.Visibility.NONE)
public class CreateFormHeader {


    /**
     * 保管人工号（必填）
     */
    @JsonProperty("shenqr")
    private String shenQr = "";

    /**
     * 后续备查，可随意填(当前登录用户电脑IP)
     */
    @JsonProperty("ip")
    private String ip = "";

    /**
     * 接口名 (固定 唯一的 )
     */
    @JsonProperty("interfaceCode")
    private final String interfaceCode = "IMESAPI_ESOP";

    /**
     * 每次不重复，相当于传单ID  (申请单号 必须唯一）
     */
    @JsonProperty("dh")
    private String dh = "";

    /**
     * 固定
     */
    @JsonProperty("type")
    private final Integer type = 13;

    /**
     * 固定
     */
    @JsonProperty("password")
    private final String password = "fgtr3yh861g";

    /**
     * 固定
     */
    @JsonProperty("userName")
    private final String userName = "yea";

    /**
     * 区段
     */
    @JsonProperty("section")
    private String section = "";

    /**
     * 站点
     */
    @JsonProperty("station")
    private String station = "";

    /**
     * 机种
     */
    @JsonProperty("model")
    private String model = "";

    /**
     * 版本
     */
    @JsonProperty("version")
    private String version = "";



    /**
     * 会签人员
     */
    @JsonProperty("countersignUser")
    private String countersignUser = "";


    /**
     * line
     */
    @JsonProperty("line")
    private String line = "";

    public String getLine() {
        return line;
    }

    public void setLine(String line) {
        this.line = line;
    }

    public String getCountersignUser() {
        return countersignUser;
    }

    public void setCountersignUser(String countersignUser) {
        this.countersignUser = countersignUser;
    }

    public String getShenQr() {
        return shenQr;
    }

    public void setShenQr(String shenQr) {
        this.shenQr = shenQr;
    }

    public String getIp() {
        return ip;
    }

    public void setIp(String ip) {
        this.ip = ip;
    }

    public String getInterfaceCode() {
        return interfaceCode;
    }

    public String getDh() {
        return dh;
    }

    public void setDh(String dh) {
        this.dh = dh;
    }

    public Integer getType() {
        return type;
    }

    public String getPassword() {
        return password;
    }

    public String getUserName() {
        return userName;
    }

    public String getSection() {
        return section;
    }

    public void setSection(String section) {
        this.section = section;
    }

    public String getStation() {
        return station;
    }

    public void setStation(String station) {
        this.station = station;
    }

    public String getModel() {
        return model;
    }

    public void setModel(String model) {
        this.model = model;
    }

    public String getVersion() {
        return version;
    }

    public void setVersion(String version) {
        this.version = version;
    }

//    public static void main(String[] args) {
//
//        try {
//            System.out.println(new ObjectMapper().writerWithDefaultPrettyPrinter().writeValueAsString(new CreateFormHeader()));
//        } catch (JsonProcessingException e) {
//            e.printStackTrace();
//        }
//
//    }
}
