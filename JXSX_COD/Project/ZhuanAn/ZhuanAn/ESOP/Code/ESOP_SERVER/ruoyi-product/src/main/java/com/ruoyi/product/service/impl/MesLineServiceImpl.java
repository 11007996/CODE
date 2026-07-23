package com.ruoyi.product.service.impl;

import com.ruoyi.common.constant.UserConstants;
import com.ruoyi.common.core.domain.entity.SysUser;
import com.ruoyi.common.exception.ServiceException;
import com.ruoyi.common.utils.DateUtils;
import com.ruoyi.common.utils.SecurityUtils;
import com.ruoyi.common.utils.StringUtils;
import com.ruoyi.common.utils.spring.SpringUtils;
import com.ruoyi.product.domain.MesLine;
import com.ruoyi.product.mapper.MesLineMapper;
import com.ruoyi.product.mapper.MesUserRelateLineMapper;
import com.ruoyi.product.service.IMesLineService;
import com.ruoyi.system.domain.MesUserRelateLine;
import com.ruoyi.system.mapper.SysUserMapper;
import com.ruoyi.system.service.ISysUserService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.Date;
import java.util.List;
import java.util.Map;

/**
 * 线别信息Service业务层处理
 *
 * @author ruoyi
 * @date 2022-09-14
 */
@Service
public class MesLineServiceImpl implements IMesLineService
{
    @Autowired
    private MesLineMapper mesLineMapper;
    @Autowired
    private MesUserRelateLineMapper mesUserRelateLineMapper;
    @Autowired
    private SysUserMapper sysUserMapper;


    /**
     * 获取线别信息详细信息
     * @param lineId 线别信息主键
     * @return 线别信息
     */
    @Override
    public MesLine selectMesLineByLineId(Long lineId)
    {
        return mesLineMapper.selectMesLineByLineId(lineId);
    }

    @Override
    public List<MesLine> selectMaterialListByLineId(Long lineId)
    {
        return mesLineMapper.selectMaterialListByLineId(lineId);
    }
    @Override
    public List<MesLine> selectUserListByLineId(Long lineId)
    {
        return mesLineMapper.selectUserListByLineId(lineId);
    }

    @Override
    public List<Map<String, String>> selectMaterialListByLineIds(Long lineId) {
        return mesLineMapper.selectMaterialListByLineIds(lineId);
    }

    /**
     * 根据线别获取所有工站
     */
    @Override
//    public List<Map<String, String>> selectTerminalListByLineId(Long lineId)
//    {
//        return mesLineMapper.selectTerminalListByLineId(lineId);
//    }
    public List<Map<String, String>> selectTerminalListByLineId(Long lineId,Long materialId)
    {
        return mesLineMapper.selectTerminalListByLineId(lineId,materialId);
    }

    /**
     * 查询线别信息列表
     *
     * @param mesLine 线别信息
     * @return 线别信息
     */
    @Override
    public List<MesLine> selectMesLineList(MesLine mesLine)
    {
        return mesLineMapper.selectMesLineList(mesLine);
    }

    @Override
    public String checkLineNameUnique(MesLine line) {
        Long lineId = StringUtils.isNull(line.getLineId()) ? -1L : line.getLineId();
        MesLine info = mesLineMapper.checkLineNameUnique(line.getLineName());
        if (StringUtils.isNotNull(info) && info.getLineId().longValue() != lineId.longValue())
        {
            return UserConstants.NOT_UNIQUE;
        }
        return UserConstants.UNIQUE;
    }

    @Override
    public void checkLineAllowed(MesLine line) {
        if (StringUtils.isNotNull(line.getLineId()) && line.isAdmin())
        {
            throw new ServiceException("不允许操作超级管理员角色");
        }
    }

    @Override
    public void checkLineDataScope(Long lineId) {
        if (!SysUser.isAdmin(SecurityUtils.getUserId()))
        {
            MesLine line = new MesLine();
            line.setLineId(lineId);
            List<MesLine> lines = SpringUtils.getAopProxy(this).selectMesLineList(line);
            if (StringUtils.isEmpty(lines))
            {
                throw new ServiceException("没有权限访问角色数据！");
            }
        }
    }

    /**
     * 新增线别信息
     *
     * @param mesLine 线别信息
     * @return 结果
     */
    @Override
    public int insertMesLine(MesLine mesLine)
    {
        mesLine.setCreateTime(DateUtils.getNowDate());
        return mesLineMapper.insertMesLine(mesLine);
    }

    /**
     * 修改线别信息
     *
     * @param mesLine 线别信息
     * @return 结果
     */
    @Override
    public int updateMesLine(MesLine mesLine)
    {
        mesUserRelateLineMapper.deleteMesUserRelateLineByLineId(mesLine.getLineId());
        int[] userIdList = mesLine.getUserIdList();
        MesUserRelateLine mesUserRelateLine = new MesUserRelateLine();
        for (int i = 0; i < userIdList.length; i++) {
            mesUserRelateLine.setLineId(mesLine.getLineId());
            mesUserRelateLine.setUserId(userIdList[i]);
            SysUser sysUser = sysUserMapper.selectUserById((long) userIdList[i]);
            mesUserRelateLine.setUserName(sysUser.getUserName());
            mesUserRelateLineMapper.insertMesUserRelateLine(mesUserRelateLine);
        }
        mesLine.setUpdateTime(DateUtils.getNowDate());
        return mesLineMapper.updateMesLine(mesLine);
    }

    @Override
    public int updateLineStatus(MesLine line) {
        return mesLineMapper.updateMesLine(line);
    }

    /**
     * 批量删除线别信息
     * 删除线别时，把mes_user_relate_line表的数据也一起删除
     * @param lineIds 需要删除的线别信息主键
     * @return 结果
     */
    @Override
    public int deleteMesLineByLineIds(Long[] lineIds)
    {
        mesUserRelateLineMapper.deleteMesUserRelateLineByLineIds(lineIds);
        return mesLineMapper.deleteMesLineByLineIds(lineIds);
    }

    /**
     * 删除线别信息信息
     *
     * @param lineId 线别信息主键
     * @return 结果
     */
    @Override
    public int deleteMesLineByLineId(Long lineId)
    {
        return mesLineMapper.deleteMesLineByLineId(lineId);
    }


    /**
     * 查询最新增加的line的id
     */
    @Override
    public Long selectLineIdByTime(Date latestTime){
        return mesLineMapper.selectLineIdByTime(latestTime);
    }
    @Override
    public MesLine lineInfoByName(String lienName){
        return mesLineMapper.lineInfoByName(lienName);
    }
}
