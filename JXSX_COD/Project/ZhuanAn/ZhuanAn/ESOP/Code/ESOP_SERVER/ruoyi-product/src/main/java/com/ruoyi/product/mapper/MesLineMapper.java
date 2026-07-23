package com.ruoyi.product.mapper;

import com.ruoyi.product.domain.MesLine;
import org.apache.ibatis.annotations.Param;

import java.util.Date;
import java.util.List;
import java.util.Map;

/**
 * 线别信息Mapper接口
 *
 * @author ruoyi
 * @date 2022-09-14
 */
public interface MesLineMapper
{
    /**
     * 获取线别信息详细信息
     * @param lineId 线别信息主键
     * @return 线别信息
     */
    public MesLine selectMesLineByLineId(Long lineId);
    public List<MesLine> selectMaterialListByLineId(Long lineId);
    public List<MesLine> selectUserListByLineId(Long lineId);

    /**
     * 根据线别获取所有工站
     *
     * @param lineId 线别信息主键
     * @return 线别信息
     */
//    public List<Map<String, String>> selectTerminalListByLineId(Long lineId,);
    public List<Map<String, String>> selectTerminalListByLineId(@Param("lineId") Long lineId,@Param("materialId") Long materialId);

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
     * @param lineName 线别名称
     * @return 线别信息
     */
    public MesLine checkLineNameUnique(String lineName);

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
     * 删除线别信息
     *
     * @param lineId 线别信息主键
     * @return 结果
     */
    public int deleteMesLineByLineId(Long lineId);

    /**
     * 批量删除线别信息
     *
     * @param lineIds 需要删除的数据主键集合
     * @return 结果
     */
    public int deleteMesLineByLineIds(Long[] lineIds);

    public Long selectLineIdByTime(Date latestTime);

    public List<Map<String, String>> selectMaterialListByLineIds(Long lineId);
    MesLine lineInfoByName(String lineName);
}
