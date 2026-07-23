package com.ruoyi.replace.domain;


import java.util.List;

public class JsonObj {

    private List<SopInfo> sopInfoList;


    //constructor
    public JsonObj() {}

    public List<SopInfo> getSopInfoList() {
        return sopInfoList;
    }

    public void setSopInfoList(List<SopInfo> sopInfoList) {
        this.sopInfoList = sopInfoList;
    }
}