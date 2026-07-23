package com.ruoyi.web.controller.product.JsonObjectLuxlink;

public class JsonObjectLuxLinkBody {
    public String account;
    public String password;
    public String processDefId;
    public String createuser;
    public String master;

    public String detail;


    public String getAccount() {
        return account;
    }

    public void setAccount(String account) {
        this.account = account;
    }

    public String getPassword() {
        return password;
    }

    public void setPassword(String password) {
        this.password = password;
    }

    public String getProcessDefId() {
        return processDefId;
    }

    public void setProcessDefId(String processDefId) {
        this.processDefId = processDefId;
    }

    public String getCreateuser() {
        return createuser;
    }

    public void setCreateuser(String createuser) {
        this.createuser = createuser;
    }

    public String getMaster() {
        return master;
    }

    public void setMaster(String master) {
        this.master = master;
    }

    public String getDetail() {
        return detail;
    }

    public void setDetail(String detail) {
        this.detail = detail;
    }

    public String toJsonString() {
        return "{" +
                "\"account\":\"" + account + '\"' +
                ", \"password\":\"" + password + '\"' +
                ", \"processDefId\":\"" + processDefId + '\"' +
                ", \"createuser\":\"" + createuser + '\"' +
                ", \"master\":" + master +
                ", \"detail\":" + detail +
                '}';
    }
}
