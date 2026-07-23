package com.ruoyi.web.controller.product.JsonObjectLuxlink;

public class JsonObjectLuxLinkBodyMaster {
    public String boName;
    public String luxid;

    public String countersignPerson;
    public String notifyPerson;
    public String writeBackUrl;

    public String getBoName() {
        return boName;
    }

    public void setBoName(String boName) {
        this.boName = boName;
    }

    public String getLuxid() {
        return luxid;
    }

    public void setLuxid(String luxid) {
        this.luxid = luxid;
    }

    public String getCountersignPerson() {
        return countersignPerson;
    }

    public void setCountersignPerson(String countersignPerson) {
        this.countersignPerson = countersignPerson;
    }

    public String getNotifyPerson() {
        return notifyPerson;
    }

    public void setNotifyPerson(String notifyPerson) {
        this.notifyPerson = notifyPerson;
    }

    public String getWriteBackUrl() {
        return writeBackUrl;
    }

    public void setWriteBackUrl(String writeBackUrl) {
        this.writeBackUrl = writeBackUrl;
    }

    public String toJsonString() {
        return "{" +
                "\"boName\":\"" + boName + '\"' +
                ", \"luxid\":\"" + luxid + '\"' +
                ", \"countersignPerson\":\"" + countersignPerson + '\"' +
                ", \"notifyPerson\":\"" + notifyPerson + '\"' +
                ", \"writeBackUrl\":\"" + writeBackUrl + '\"' +
                "}";
    }
}
