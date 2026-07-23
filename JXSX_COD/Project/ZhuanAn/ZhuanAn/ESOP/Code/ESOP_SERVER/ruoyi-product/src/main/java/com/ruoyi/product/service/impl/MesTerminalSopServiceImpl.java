package com.ruoyi.product.service.impl;

import com.ruoyi.common.utils.DateUtils;
import com.ruoyi.product.domain.MesSopGroup;
import com.ruoyi.product.domain.MesTerminalSop;
import com.ruoyi.product.mapper.MesSopGroupMapper;
import com.ruoyi.product.mapper.MesTerminalSopMapper;
import com.ruoyi.product.service.IMesTerminalSopService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;

import static com.ruoyi.common.utils.SecurityUtils.getUsername;

/**
 * sop配置Service业务层处理
 *
 * @author ruoyi
 * @date 2022-09-21
 */
@Service
public class MesTerminalSopServiceImpl implements IMesTerminalSopService
{
    @Autowired
    private MesTerminalSopMapper mesTerminalSopMapper;

    @Autowired
    private MesSopGroupMapper mesSopGroupMapper;



    /**
     * 查询sop配置
     *
     * @param id sop配置主键
     * @return sop配置
     */
    @Override
    public MesTerminalSop selectMesTerminalSopById(Long id)
    {
        return mesTerminalSopMapper.selectMesTerminalSopById(id);
    }

    /**
     * 查询sop配置列表
     *
     * @param mesTerminalSop sop配置
     * @return sop配置
     */
    @Override
    public List<MesTerminalSop> selectMesTerminalSopList(MesTerminalSop mesTerminalSop)
    {
        return mesTerminalSopMapper.selectMesTerminalSopList(mesTerminalSop);
    }

    public List<MesTerminalSop> selectMesTerminalSopList1(MesTerminalSop mesTerminalSop)
    {
        return mesTerminalSopMapper.selectMesTerminalSopList1(mesTerminalSop);
    }

    /**
     * 新增sop配置
     *
     * @param mesTerminalSop sop配置
     * @return 结果
     */
    @Override
    public int insertMesTerminalSop(MesTerminalSop mesTerminalSop)
    {
        mesTerminalSop.setCreateTime(DateUtils.getNowDate());
        mesTerminalSopMapper.insertMesTerminalSop(mesTerminalSop);
        return 0;
    }

    /**
     * 修改sop配置
     *
     * @param mesTerminalSop sop配置
     * @return 结果
     */
    @Override
    public int updateMesTerminalSop(MesTerminalSop mesTerminalSop)
    {
//        mesTerminalSop.setUpdateTime(DateUtils.parseDate(DateUtils.getTime()));
//        try{
            //修改表mes_sop_group数据  先删后增
            List<MesSopGroup> list = mesTerminalSop.getSopGroupList();
            for (int i = 0; i < list.size(); i++) {
                String type = list.get(i).getType();
                mesSopGroupMapper.deleteMesSopGroupBySopGroupId(mesTerminalSop.getId(),type);

                MesSopGroup sop = list.get(i);
                sop.setSopGroupId(mesTerminalSop.getId().toString());
                sop.setSopId(list.get(i).getSopId());
                sop.setSopPage(list.get(i).getSopPage());
                sop.setType(type);
                sop.setCreateTime(DateUtils.parseDate(DateUtils.getTime()));
                sop.setCreateBy(getUsername());
                mesSopGroupMapper.insertMesSopGroup(sop);
            }
//        catch (Exception e){
//            return -1;
//        }
        return 1;

//
//        JSONObject jsonObject = JSON.parseObject(String.valueOf(mesTerminalSop));
//
//        Long sopGroupId = jsonObject.getLong("id");
//        mesSopGroupMapper.deleteMesSopGroupBySopGroupId(sopGroupId);
//        MesSopGroup msg = new MesSopGroup();
//        msg.setSopGroupId(jsonObject.getLong("id"));
//        msg.setSopId(jsonObject.getLong("sopId"));
//        msg.setSopPage(jsonObject.getString("sopPage"));

//        return mesTerminalSopMapper.updateMesTerminalSop(mesTerminalSop);
    }
    public int updateMesTerminalSop1(MesTerminalSop mesTerminalSop)
    {
        return mesTerminalSopMapper.updateMesTerminalSop(mesTerminalSop);
//        return mesTerminalSopMapper.updateMesTerminalSop1(mesTerminalSop);
    }

    @Override
    public int insertHistory(MesTerminalSop mesTerminalSop)
    {
        return mesTerminalSopMapper.insertHistory(mesTerminalSop);
    }

    /**
     * 批量删除sop配置
     *
     * @param ids 需要删除的sop配置主键
     * @return 结果
     */
    @Override
    public int deleteMesTerminalSopByIds(Long[] ids)
    {
        return mesTerminalSopMapper.deleteMesTerminalSopByIds(ids);
    }

    @Override
    public int deleteMesTerminalSopByMacs(Long[] macIds)
    {
        return mesTerminalSopMapper.deleteMesTerminalSopByMacs(macIds);
    }

    /**
     * 删除sop配置信息
     *
     * @param id sop配置主键
     * @return 结果
     */
    @Override
    public int deleteMesTerminalSopById(Long id)
    {
        return mesTerminalSopMapper.deleteMesTerminalSopById(id);
    }

    @Override
    public Long selectIdByTerminalSopInfo(MesTerminalSop mesTerminalSop) {
        return mesTerminalSopMapper.selectIdByTerminalSopInfo(mesTerminalSop);
    }

    /**
     * 根据lineId查询所有terminalId
     *
     * @param lineId
     * @return 结果
     */
    @Override
    public List<MesTerminalSop> selectTerminalSopInfoByLineId(Long lineId) {
        return mesTerminalSopMapper.selectTerminalSopInfoByLineId(lineId);
    }
    /**
     * 根据line+stage+process查询所有terminalId
     *
     * @param terminalSop
     * @return 结果
     */
    public List<MesTerminalSop> selectTerminalSopInfoByProcessId (MesTerminalSop terminalSop){
        return mesTerminalSopMapper.selectTerminalSopInfoByProcessId(terminalSop);
    }


    /**
     * 查询sop已推送列表
     *
     * @param mesTerminalSop sop配置
     * @return sop配置集合
     */
    public List<MesTerminalSop> selectMesTerminalSopListPushed(MesTerminalSop mesTerminalSop){
        return mesTerminalSopMapper.selectMesTerminalSopListPushed(mesTerminalSop);
    }
}
