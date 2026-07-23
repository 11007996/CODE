package com.ruoyi.web.controller.product.JsonObjectLuxlink;

public class JsonObjectLuxlinkBodyDetail {
    private String boName;

    private String boItem;

    public String getBoName() {
        return boName;
    }

    public void setBoName(String boName) {
        this.boName = boName;
    }

    public String getBoItem() {
        return boItem;
    }

    public void setBoItem(String boItem) {
        this.boItem = boItem;
    }

    public String toJsonString() {
        return "[{" +
                "\"boName\":" + '\"' + boName + '\"' +
                ", \"boItem\":" + boItem +
                "}]";
    }
}
