package com.ruoyi.product.service;

import com.ruoyi.product.domain.MesUploadTerminalPage;

import java.util.List;
import java.util.Map;

/**
 * 站点页码Service接口
 *
 * @author ruoyi
 * @date 2023-01-03
 */
public interface IMesUploadTerminalPageService
{
    /**
     * 查询站点页码
     *
     * @param id 站点页码主键
     * @return 站点页码
     */
    public List<MesUploadTerminalPage> selectMesUploadTerminalPageById(String id);

    /**
     * 查询站点页码列表
     *
     * @param mesUploadTerminalPage 站点页码
     * @return 站点页码集合
     */
    public List<MesUploadTerminalPage> selectMesUploadTerminalPageList(MesUploadTerminalPage mesUploadTerminalPage);

    /**
     * 新增站点页码
     *
     * @param mesUploadTerminalPage 站点页码
     * @return 结果
     */
    public int insertMesUploadTerminalPage(MesUploadTerminalPage mesUploadTerminalPage);

    /**
     * 修改站点页码
     *
     * @param mesUploadTerminalPage 站点页码
     * @return 结果
     */
    public int updateMesUploadTerminalPage(MesUploadTerminalPage mesUploadTerminalPage);
    /**
     * 修改mac地址的sop版本状态
     *
     */
    public int updateMesUploadTerminalPageVersionStatus(MesUploadTerminalPage mesUploadTerminalPage);

    /**
     * 批量删除站点页码
     *
     * @param ids 需要删除的站点页码主键集合
     * @return 结果
     */
    public int deleteMesUploadTerminalPageByIds(Long[] ids);

    /**
     * 删除站点页码信息
     *
     * @param id 站点页码主键
     * @return 结果
     */
    public int deleteMesUploadTerminalPageById(Long id);

    /**
     * 根据站点信息（model、line、stage、process）查询sopPage相关信息
     * @return 结果
     */
    public List<MesUploadTerminalPage> selectPageInfoList(MesUploadTerminalPage mesUploadTerminalPage);
    public List<MesUploadTerminalPage> selectPageInfoList1(MesUploadTerminalPage mesUploadTerminalPage);

    /**
     * 根据sopId查询到对应的站点信息（model、line、stage、process）
     * @return 结果
     */
    public List<MesUploadTerminalPage> selectTerminalListBySopId(Long sopId);

    /**
     * 表mes_upload_terminal_page中通过站点id（model、line、stage、process）查询name
     * @return 结果
     */
    public List<MesUploadTerminalPage> selectMesUploadTerminalPageListById(Long sopId);

    /**
     * 根据料号material,获取最新(createTime)的一条模板信息
     * @return 结果
     */
    public List<MesUploadTerminalPage> selectTemplateByPartNo(Long materialId);


    /**
     * 根据料号materialId+线别lineId,获取最新(createTime)的一条模板信息
     */
//    public List<MesUploadTerminalPage> selectTemplateByPartNo(@Param("materialId") Long materialId, @Param("lineId") Long lineId);


    /**
     * 根据料号material+modelId+stageId,获取已签核的sop信息(BU17在用)
     * @return 结果
     */
    public List<Map<String,String>> selectTemplateInfoByPartNo(MesUploadTerminalPage mesUploadTerminalPage);
    /**
     * 根据line+stage,获取已签核的sop信息（mulan在用）
     * @return 结果
     */
    public List<Map<String,String>> selectTemplateInfoByStage(String stageId);
    public List<Map<String,String>> selectHistoricalTemplateInfoByPartNo(MesUploadTerminalPage mesUploadTerminalPage);
    public List<Map<String,String>> selectVersionByIds(MesUploadTerminalPage mesUploadTerminalPage);
    /**
     * 根据terminalId查询某mac地址的历史版本
     */
    public List<MesUploadTerminalPage> selectHistoricalVersionByTerminalId(Long terminalId);

    /**
     * 根据line+stage+process查询某mac地址的历史版本
     */
    public List<MesUploadTerminalPage> selectHistoricalVersionByProcess(MesUploadTerminalPage mesUploadTerminalPage);

    /**
     * 根据line+stage+process查询某mac地址的历史版本
     */
    public List<MesUploadTerminalPage> selectSopVersionByProcess(Long processId);

    /**
     * 根据model+material+line+stage+stage+terminal查出某个sop页码
     * 条件不够，加上version_status和sop_id一起查询
     */
    public MesUploadTerminalPage selectUploadPageByIds(MesUploadTerminalPage mesUploadTerminalPage);
}
