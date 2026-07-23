package com.ruoyi.product.domain;

import com.ruoyi.common.annotation.Excel;

public class MacExcel {

        private Long macId;

        @Excel(name = "（必填）MAC名称")
        private String macName;

        @Excel(name = "（选填）MAC所在区域（格式：×楼_×层_台）")
        private String macArea;

        @Excel(name = "备注")
        private String remark;

        public Long getMacId() {
            return macId;
        }

        public void setMacId(Long macId) {
            this.macId = macId;
        }

        public String getMacName() {
            return macName;
        }

        public void setMacName(String macName) {
            this.macName = macName;
        }

        public String getMacArea() {
            return macArea;
        }

        public void setMacArea(String macArea) {
            this.macArea = macArea;
        }

        public String getRemark() {
            return remark;
        }

        public void setRemark(String remark) {
            this.remark = remark;
        }
    }
