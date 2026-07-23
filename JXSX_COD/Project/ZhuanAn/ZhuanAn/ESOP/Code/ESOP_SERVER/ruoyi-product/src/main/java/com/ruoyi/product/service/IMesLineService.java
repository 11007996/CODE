package com.ruoyi.product.service;

import com.ruoyi.product.domain.MesLine;

import java.util.Date;
import java.util.List;
import java.util.Map;

/**
 * 线别信息Service接口
 *
 * @author ruoyi
 * @date 2022-09-14
 */
public interface IMesLineService
{
    /**
     * 获取线别信息详细信息
     * @param lineId 线别信息主键
     * @return 线别信息
     */
    public MesLine selectMesLineByLineId(Long lineId);
    public List<MesLine> selectMaterialListByLineId(Long lineId);
    public List<MesLine> selectUserListByLineId(Long lineId);

    public List<Map<String,String>> selectMaterialListByLineIds(Long lineId);

    /**
     * 根据线别获取所有工站
     */
//    public List<Map<String, String>> selectTerminalListByLineId(Long lineId);
    public List<Map<String, String>> selectTerminalListByLineId(Long lineId, Long materialId);

    /**
     * 查询线别信息列表
     *
     * @param mesLine 线别信息
     * @return 线别信息集合
     */
    public List<MesLine> selectMesLineList(MesLine mesLine);

    /**
     * 校验线别名称是否唯一
     *
     * @param line 线别信息
     * @return 结果
     */
    public String checkLineNameUnique(MesLine line);

    /**
     * 校验线别是否允许操作
     *
     * @param line 线别
     */
    public void checkLineAllowed(MesLine line);

    /**
     * 校验线别是否有数据权限
     *
     * @param lineId 线别id
     */
    public void checkLineDataScope(Long lineId);

    /**
     * 新增线别信息
     *
     * @param mesLine 线别信息
     * @return 结果
     */
    public int insertMesLine(MesLine mesLine);

    /**
     * 修改线别信息
     *
     * @param mesLine 线别信息
     * @return 结果
     */
    public int updateMesLine(MesLine mesLine);

    /**
     * 修改线别状态
     *
     * @param line 线别信息
     * @return 结果
     */
    public int updateLineStatus(MesLine line);

    /**
     * 批量删除线别信息
     *
     * @param lineIds 需要删除的线别信息主键集合
     * @return 结果
     */
    public int deleteMesLineByLineIds(Long[] lineIds);

    /**
     * 删除线别信息信息
     *
     * @param lineId 线别信息主键
     * @return 结果
     */
    public int deleteMesLineByLineId(Long lineId);


    /**
     * 查询最新增加的line的id
     */

    Long selectLineIdByTime(Date latestTime);
    MesLine lineInfoByName(String lineName);
}
