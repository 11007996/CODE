package com.ruoyi.system.domain;

import org.apache.commons.lang3.builder.ToStringBuilder;
import org.apache.commons.lang3.builder.ToStringStyle;
import com.ruoyi.common.annotation.Excel;
import com.ruoyi.common.core.domain.BaseEntity;

/**
 * 会签人员、线体关系对象 mes_user_relate_line
 *
 * @author ruoyi
 * @date 2023-10-20
 */
public class MesUserRelateLine extends BaseEntity
{
    private static final long serialVersionUID = 1L;

    /** 线体id（线体：会签人员=1：n） */
    @Excel(name = "线体id", readConverterExp = "线=体：会签人员=1：n")
    private Long lineId;

    /** 会签人员id */
    @Excel(name = "会签人员id")
    private int userId;

    /** 会签人员名称 */
    @Excel(name = "会签人员名称")
    private String userName;

    public void setLineId(Long lineId)
    {
        this.lineId = lineId;
    }

    public Long getLineId()
    {
        return lineId;
    }

    public int getUserId() {
        return userId;
    }

    public void setUserId(int userId) {
        this.userId = userId;
    }

    public void setUserName(String userName)
    {
        this.userName = userName;
    }

    public String getUserName()
    {
        return userName;
    }

    @Override
    public String toString() {
        return new ToStringBuilder(this,ToStringStyle.MULTI_LINE_STYLE)
                .append("lineId", getLineId())
                .append("userId", getUserId())
                .append("userName", getUserName())
                .toString();
    }
}
