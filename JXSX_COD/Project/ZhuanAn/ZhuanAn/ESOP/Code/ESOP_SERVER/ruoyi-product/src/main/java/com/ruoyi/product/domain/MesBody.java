package com.ruoyi.product.domain;

import java.util.List;

public class MesBody  {
    private Long modelId;

    private Long materialId;
    private List<MesSop> sopIdList;
    private List<MesTerminalSop> terminalPageList;

    public Long getMaterialId() {
        return materialId;
    }

    public void setMaterialId(Long materialId) {
        this.materialId = materialId;
    }

    public Long getModelId() {
        return modelId;
    }

    public void setModelId(Long modelId) {
        this.modelId = modelId;
    }

    public List<MesSop> getSopIdList() {
        return sopIdList;
    }

    public void setSopIdList(List<MesSop> sopIdList) {
        this.sopIdList = sopIdList;
    }

    public List<MesTerminalSop> getTerminalPageList() {
        return terminalPageList;
    }

    public void setTerminalPageList(List<MesTerminalSop> terminalPageList) {
        this.terminalPageList = terminalPageList;
    }
}
