package com.ruoyi.product.mapper;

import java.util.List;

import com.ruoyi.product.domain.MesLine;
import com.ruoyi.product.domain.MesMac;
import com.ruoyi.product.domain.MesTerminal;
import org.springframework.stereotype.Repository;

/**
 * 工站Mapper接口
 *
 * @author ruoyi
 * @date 2022-09-14
 */
@Repository
public interface MesTerminalMapper
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
     * 删除工站
     *
     * @param terminalId 工站主键
     * @return 结果
     */
    public int deleteMesTerminalByTerminalId(Long terminalId);

    /**
     * 批量删除工站
     *
     * @param terminalIds 需要删除的数据主键集合
     * @return 结果
     */
    public int deleteMesTerminalByTerminalIds(Long[] terminalIds);




    /**
     * 获取line
     *
     * @param mesTerminal
     * @return
     */
    public Integer getMaxSequenceByProcessId(MesTerminal mesTerminal);

    public List<MesTerminal> selectLineList() ;

//    工站版：根据line查询stage
    public List<MesTerminal> selectStageList(MesTerminal mesTerminal) ;

//    MAC版：根据line查询stage
//    public List<MesTerminal> selectStageList(MesMac mesMac) ;
    public List<MesTerminal> selectProcessList(MesTerminal mesTerminal) ;
}
