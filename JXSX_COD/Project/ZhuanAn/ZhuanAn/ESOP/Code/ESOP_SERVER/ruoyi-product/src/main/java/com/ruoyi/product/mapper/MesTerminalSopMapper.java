package com.ruoyi.product.mapper;

import com.ruoyi.product.domain.MesTerminalSop;

import java.util.List;

/**
 * sop配置Mapper接口
 *
 * @author ruoyi
 * @date 2022-09-21
 */
public interface MesTerminalSopMapper
{
    /**
     * 查询sop配置
     *
     * @param id sop配置主键
     * @return sop配置
     */
    public MesTerminalSop selectMesTerminalSopById(Long id);

    /**
     * 查询sop配置列表
     *
     * @param mesTerminalSop sop配置
     * @return sop配置集合
     */
    public List<MesTerminalSop> selectMesTerminalSopList(MesTerminalSop mesTerminalSop);
    public List<MesTerminalSop> selectMesTerminalSopList1(MesTerminalSop mesTerminalSop);

    /**
     * 新增sop配置
     *
     * @param mesTerminalSop sop配置
     * @return 结果
     */
    public int insertMesTerminalSop(MesTerminalSop mesTerminalSop);

    /**
     * 修改sop配置
     *
     * @param mesTerminalSop sop配置
     * @return 结果
     */
    public int updateMesTerminalSop(MesTerminalSop mesTerminalSop);
    public int updateMesTerminalSop1(MesTerminalSop mesTerminalSop);

    public int insertHistory(MesTerminalSop mesTerminalSop);

    /**
     * 删除sop配置
     *
     * @param id sop配置主键
     * @return 结果
     */
    public int deleteMesTerminalSopById(Long id);

    /**
     * 批量删除sop配置
     *
     * @param ids 需要删除的数据主键集合
     * @return 结果
     */
    public int deleteMesTerminalSopByIds(Long[] ids);
    public int deleteMesTerminalSopByMacs(Long[] ids);

    public Long selectIdByTerminalSopInfo(MesTerminalSop terminalSop);

    /**
     * 根据lineId查询所有terminalId
     *
     * @param lineId
     * @return 结果
     */
    public List<MesTerminalSop> selectTerminalSopInfoByLineId(Long lineId);
    /**
     * 根据line+stage+process查询所有terminalId
     *
     * @param terminalSop
     * @return 结果
     */
    public List<MesTerminalSop> selectTerminalSopInfoByProcessId(MesTerminalSop terminalSop);

    /**
     * 查询sop已推送列表
     *
     * @param mesTerminalSop sop配置
     * @return sop配置集合
     */
    public List<MesTerminalSop> selectMesTerminalSopListPushed(MesTerminalSop mesTerminalSop);
}
