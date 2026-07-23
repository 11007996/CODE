package com.ruoyi.product.service;

import com.ruoyi.product.domain.MesMac;
import com.ruoyi.product.domain.MesTerminal;

import java.util.List;

/**
 * 工站Service接口
 *
 * @author ruoyi
 * @date 2022-09-14
 */
public interface IMesTerminalService
{
    /**
     * 查询工站
     *
     * @param terminalId 工站主键
     * @return 工站
     */
    public MesTerminal selectMesTerminalByTerminalId(Long terminalId);

    /**
     * 查询工站列表
     *
     * @param mesTerminal 工站
     * @return 工站集合
     */
    public List<MesTerminal> selectMesTerminalList(MesTerminal mesTerminal);

    /**
     * 新增工站
     *
     * @param mesTerminal 工站
     * @return 结果
     */
    public int insertMesTerminal(MesTerminal mesTerminal);

    /**
     * 修改工站
     *
     * @param mesTerminal 工站
     * @return 结果
     */
    public int updateMesTerminal(MesTerminal mesTerminal);

    /**
     * 批量删除工站
     *
     * @param terminalIds 需要删除的工站主键集合
     * @return 结果
     */
    public int deleteMesTerminalByTerminalIds(Long[] terminalIds);

    /**
     * 删除工站信息
     *
     * @param terminalId 工站主键
     * @return 结果
     */
    public int deleteMesTerminalByTerminalId(Long terminalId);

    public Integer getMaxSequenceByProcessId(MesTerminal mesTerminal);

    public List<MesTerminal> selectLineList();
    public List<MesTerminal> selectStageList(MesTerminal mesTerminal);


    public List<MesTerminal> selectProcessList(MesTerminal mesTerminal);
}
