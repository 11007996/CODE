package com.ruoyi.product.service.impl;

import java.util.List;
import com.ruoyi.common.utils.DateUtils;
import com.ruoyi.product.domain.MesMac;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import com.ruoyi.product.mapper.MesTerminalMapper;
import com.ruoyi.product.domain.MesTerminal;
import com.ruoyi.product.service.IMesTerminalService;

/**
 * 工站Service业务层处理
 *
 * @author ruoyi
 * @date 2022-09-14
 */
@Service
public class MesTerminalServiceImpl implements IMesTerminalService
{
    @Autowired
    private MesTerminalMapper mesTerminalMapper;

    /**
     * 查询工站
     *
     * @param terminalId 工站主键
     * @return 工站
     */
    @Override
    public MesTerminal selectMesTerminalByTerminalId(Long terminalId)
    {
        return mesTerminalMapper.selectMesTerminalByTerminalId(terminalId);
    }

    /**
     * 查询工站列表
     *
     * @param mesTerminal 工站
     * @return 工站
     */
    @Override
    public List<MesTerminal> selectMesTerminalList(MesTerminal mesTerminal)
    {
        return mesTerminalMapper.selectMesTerminalList(mesTerminal);
    }

    /**
     * 新增工站
     *
     * @param mesTerminal 工站
     * @return 结果
     */
    @Override
    public int insertMesTerminal(MesTerminal mesTerminal)
    {

        mesTerminal.setCreateTime(DateUtils.getNowDate());
        return mesTerminalMapper.insertMesTerminal(mesTerminal);
    }

    /**
     * 修改工站
     *
     * @param mesTerminal 工站
     * @return 结果
     */
    @Override
    public int updateMesTerminal(MesTerminal mesTerminal)
    {
        mesTerminal.setUpdateTime(DateUtils.getNowDate());
        return mesTerminalMapper.updateMesTerminal(mesTerminal);
    }

    /**
     * 批量删除工站
     *
     * @param terminalIds 需要删除的工站主键
     * @return 结果
     */
    @Override
    public int deleteMesTerminalByTerminalIds(Long[] terminalIds)
    {
        return mesTerminalMapper.deleteMesTerminalByTerminalIds(terminalIds);
    }

    /**
     * 删除工站信息
     *
     * @param terminalId 工站主键
     * @return 结果
     */
    @Override
    public int deleteMesTerminalByTerminalId(Long terminalId)
    {
        return mesTerminalMapper.deleteMesTerminalByTerminalId(terminalId);
    }



    @Override
    public Integer getMaxSequenceByProcessId(MesTerminal mesTerminal){
        return mesTerminalMapper.getMaxSequenceByProcessId(mesTerminal);
    }

    @Override
    public List<MesTerminal> selectLineList() {
        return mesTerminalMapper.selectLineList();
    }

    //    工站版：根据line查询stage
//    @Override
//    public List<MesTerminal> selectStageList(MesTerminal mesTerminal) {
//        return mesTerminalMapper.selectStageList(mesTerminal);
//    }

    //    MAC版：根据line查询stage
    @Override
    public List<MesTerminal> selectStageList(MesTerminal mesTerminal) {
        return mesTerminalMapper.selectStageList(mesTerminal);
    }

    @Override
    public List<MesTerminal> selectProcessList(MesTerminal mesTerminal) {
        return mesTerminalMapper.selectProcessList(mesTerminal);
    }

}
