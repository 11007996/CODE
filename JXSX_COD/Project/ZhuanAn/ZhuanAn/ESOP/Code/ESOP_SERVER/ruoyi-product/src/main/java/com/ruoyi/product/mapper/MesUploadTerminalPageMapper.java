package com.ruoyi.product.mapper;

import com.ruoyi.product.domain.MesUploadTerminalPage;
import com.ruoyi.product.service.impl.MesUploadTerminalPageServiceImpl;
import org.apache.ibatis.annotations.Param;

import java.util.List;
import java.util.Map;

/**
 * 站点页码Mapper接口
 *
 * @author ruoyi
 * @date 2023-01-03
 */
public interface MesUploadTerminalPageMapper
{
    /**
     * 查询站点页码
     *
     * @param oaId 站点页码主键
     * @return 站点页码
     */
    public List<MesUploadTerminalPage> selectMesUploadTerminalPageByOaId(String oaId);

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
    public int updateMesUploadTerminalPageVersionStatus(MesUploadTerminalPage mesUploadTerminalPage);

    /**
     * 删除站点页码
     *
     * @param id 站点页码主键
     * @return 结果
     */
    public int deleteMesUploadTerminalPageById(Long id);

    /**
     * 批量删除站点页码
     *
     * @param ids 需要删除的数据主键集合
     * @return 结果
     */
    public int deleteMesUploadTerminalPageByIds(Long[] ids);

    public List<MesUploadTerminalPage> selectPageInfoList (MesUploadTerminalPage mesUploadTerminalPage);
    public List<MesUploadTerminalPage> selectPageInfoList1 (MesUploadTerminalPage mesUploadTerminalPage);

    /**
     * 根据sopId查询到对应的站点id信息（model、line、stage、process）
     * @return 结果
     */
    public List<MesUploadTerminalPage> selectTerminalListBySopId(Long sopId);

    /**
     * 表mes_upload_terminal_page中通过站点id信息（model、line、stage、process）查询name
     * @return 结果
     */
    public List<MesUploadTerminalPage> selectMesUploadTerminalPageListById(@Param("sopId") Long sopId);

    /**
     * 根据料号material,获取最新(createTime)的一条模板信息
     * @return 结果
     */

    public List<MesUploadTerminalPage> selectTemplateByPartNo(Long materialId);

    /**
     * 根据料号materialId+线别lineId,获取最新(createTime)的一条模板信息
     */
//    public List<MesUploadTerminalPage> selectTemplateByPartNo(@Param("materialId") Long materialId,@Param("lineId") Long lineId);

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
    public List<MesUploadTerminalPage> selectHistoricalVersionByTerminalId(Long terminalId);

    public List<MesUploadTerminalPage> selectHistoricalVersionByProcess(MesUploadTerminalPage mesUploadTerminalPage);

//    public List<MesUploadTerminalPage> mesUploadTerminalPage(MesUploadTerminalPage mesUploadTerminalPage);
    public List<MesUploadTerminalPage> selectSopVersionByProcess(Long processId);
    public MesUploadTerminalPage selectUploadPageByIds(MesUploadTerminalPage mesUploadTerminalPage);

}
