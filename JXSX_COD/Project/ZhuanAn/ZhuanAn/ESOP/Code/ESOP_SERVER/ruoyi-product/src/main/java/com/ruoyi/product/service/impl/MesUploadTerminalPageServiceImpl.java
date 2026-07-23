package com.ruoyi.product.service.impl;

import com.ruoyi.product.domain.MesUploadTerminalPage;
import com.ruoyi.product.mapper.MesUploadTerminalPageMapper;
import com.ruoyi.product.service.IMesUploadTerminalPageService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;
import java.util.Map;

/**
 * 站点页码Service业务层处理
 *
 * @author ruoyi
 * @date 2023-01-03
 */
@Service
public class MesUploadTerminalPageServiceImpl implements IMesUploadTerminalPageService
{
    @Autowired
    private MesUploadTerminalPageMapper mesUploadTerminalPageMapper;

    /**
     * 查询站点页码
     *
     * @param id 站点页码主键
     * @return 站点页码
     */
    @Override
    public List<MesUploadTerminalPage> selectMesUploadTerminalPageById(String id)
    {
        return mesUploadTerminalPageMapper.selectMesUploadTerminalPageByOaId(id);
    }

    /**
     * 查询站点页码列表
     *
     * @param mesUploadTerminalPage 站点页码
     * @return 站点页码
     */
    @Override
    public List<MesUploadTerminalPage> selectMesUploadTerminalPageList(MesUploadTerminalPage mesUploadTerminalPage)
    {
        return mesUploadTerminalPageMapper.selectMesUploadTerminalPageList(mesUploadTerminalPage);
    }

    /**
     * 新增站点页码
     *
     * @param mesUploadTerminalPage 站点页码
     * @return 结果
     */
    @Override
    public int insertMesUploadTerminalPage(MesUploadTerminalPage mesUploadTerminalPage)
    {
        return mesUploadTerminalPageMapper.insertMesUploadTerminalPage(mesUploadTerminalPage);
    }

    /**
     * 修改站点页码
     *
     * @param mesUploadTerminalPage 站点页码
     * @return 结果
     */
    @Override
    public int updateMesUploadTerminalPage(MesUploadTerminalPage mesUploadTerminalPage)
    {
        return mesUploadTerminalPageMapper.updateMesUploadTerminalPage(mesUploadTerminalPage);
    }
    @Override
    public int updateMesUploadTerminalPageVersionStatus(MesUploadTerminalPage mesUploadTerminalPage)
    {
        return mesUploadTerminalPageMapper.updateMesUploadTerminalPageVersionStatus(mesUploadTerminalPage);
    }

    /**
     * 批量删除站点页码
     *
     * @param ids 需要删除的站点页码主键
     * @return 结果
     */
    @Override
    public int deleteMesUploadTerminalPageByIds(Long[] ids)
    {
        return mesUploadTerminalPageMapper.deleteMesUploadTerminalPageByIds(ids);
    }

    /**
     * 删除站点页码信息
     *
     * @param id 站点页码主键
     * @return 结果
     */
    @Override
    public int deleteMesUploadTerminalPageById(Long id)
    {
        return mesUploadTerminalPageMapper.deleteMesUploadTerminalPageById(id);
    }

    /**
     * 根据站点信息（model、line、stage、process）查询sopPage相关信息
     * @return 结果
     */
    @Override
    public List<MesUploadTerminalPage> selectPageInfoList(MesUploadTerminalPage mesUploadTerminalPage)
    {
        return mesUploadTerminalPageMapper.selectPageInfoList(mesUploadTerminalPage);
    }
    @Override
    public List<MesUploadTerminalPage> selectPageInfoList1(MesUploadTerminalPage mesUploadTerminalPage)
    {
        return mesUploadTerminalPageMapper.selectPageInfoList1(mesUploadTerminalPage);
    }

    /**
     * 根据sopId查询到对应的站点信息list（model、line、stage、process）
     * @return 结果
     */
    public List<MesUploadTerminalPage> selectTerminalListBySopId(Long sopId){
        return mesUploadTerminalPageMapper.selectTerminalListBySopId(sopId);
    }

    /**
     * 表mes_upload_terminal_page中通过站点id（model、line、stage、process）查询name
     * @return 结果
     */
    @Override
    public List<MesUploadTerminalPage> selectMesUploadTerminalPageListById(Long sopId) {
        return mesUploadTerminalPageMapper.selectMesUploadTerminalPageListById(sopId);
    }

    /**
     * 根据料号material,获取最新(createTime)的一条模板信息
     * @return 结果
     */
    public List<MesUploadTerminalPage>  selectTemplateByPartNo(Long materialId){
        return  mesUploadTerminalPageMapper.selectTemplateByPartNo(materialId);
    }


    /**
     * 根据料号materialId+线别lineId,获取最新(createTime)的一条模板信息
     */
//    public List<MesUploadTerminalPage> selectTemplateByPartNo(@Param("materialId") Long materialId, @Param("lineId") Long lineId){
//        return  mesUploadTerminalPageMapper.selectTemplateByPartNo(materialId,lineId);
//    }

    /**
     * 根据料号material+modelId+stageId,获取已签核的sop信息(BU17在用)
     * @return 结果
     */
    public List<Map<String,String>> selectTemplateInfoByPartNo(MesUploadTerminalPage mesUploadTerminalPage){
        return  mesUploadTerminalPageMapper.selectTemplateInfoByPartNo(mesUploadTerminalPage);
    }
    /**
     * 根据line+stage,获取已签核的sop信息（mulan在用）
     * @return 结果
     */
    public List<Map<String,String>> selectTemplateInfoByStage(String stageId){
        return  mesUploadTerminalPageMapper.selectTemplateInfoByStage(stageId);
    }
    /**
     * 查询sop的历史版本（只到制程）
     * @param mesUploadTerminalPage
     * @return
     */
    public List<Map<String,String>> selectHistoricalTemplateInfoByPartNo(MesUploadTerminalPage mesUploadTerminalPage){
        return  mesUploadTerminalPageMapper.selectHistoricalTemplateInfoByPartNo(mesUploadTerminalPage);
    }
    public List<Map<String,String>> selectVersionByIds(MesUploadTerminalPage mesUploadTerminalPage){
        return  mesUploadTerminalPageMapper.selectVersionByIds(mesUploadTerminalPage);
    }
    public List<MesUploadTerminalPage> selectHistoricalVersionByTerminalId(Long terminalId){
        return  mesUploadTerminalPageMapper.selectHistoricalVersionByTerminalId(terminalId);
    }
    public List<MesUploadTerminalPage> selectHistoricalVersionByProcess(MesUploadTerminalPage mesUploadTerminalPage){
        return  mesUploadTerminalPageMapper.selectHistoricalVersionByProcess(mesUploadTerminalPage);
    }

    public List<MesUploadTerminalPage> selectSopVersionByProcess(Long processId){
        return  mesUploadTerminalPageMapper.selectSopVersionByProcess(processId);
    }

    /**
     * 根据model+material+line+stage+stage+terminal查出某个sop页码
     * 条件不够，加上version_status和sop_id一起查询
    */
    public MesUploadTerminalPage selectUploadPageByIds(MesUploadTerminalPage mesUploadTerminalPage){
        return  mesUploadTerminalPageMapper.selectUploadPageByIds(mesUploadTerminalPage);
    }


}
