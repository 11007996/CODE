package com.ruoyi.replace.domain;

public class CopyTemplateObj{
    private Long parent;
    private Long[] child;

    private Long[] lineIds;
    private Long[] materialId;
//    private Long materialId;
    public Long getParent() {
        return parent;
    }

    public void setParent(Long parent) {
        this.parent = parent;
    }

    public Long[] getChild() {
        return child;
    }

    public void setChild(Long[] child) {
        this.child = child;
    }

    public Long[] getLineIds() {
        return lineIds;
    }

    public void setLineIds(Long[] lineIds) {
        this.lineIds = lineIds;
    }

//    public Long getMaterialId() {
//        return materialId;
//    }
//
//    public void setMaterialId(Long materialId) {
//        this.materialId = materialId;
//    }

    public Long[] getMaterialId() {
        return materialId;
    }

    public void setMaterialId(Long[] materialId) {
        this.materialId = materialId;
    }


//    @Override
//    public String toString() {
//        return "CopyTemplateObj{" +
//                "parent=" + parent +
//                ", child=" + Arrays.toString(child) +
//                '}';
//    }
}
