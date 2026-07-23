package com.ruoyi.product.service;

import com.ruoyi.product.domain.MesOaSop;

import java.util.List;

/**
 * oa签核Service接口
 *
 * @author ruoyi
 * @date 2022-09-21
 */
public interface IMesOaSopService
{
    /**
     * 查询oa签核
     *
     * @param oaId oa签核主键
     * @return oa签核
     */
    public MesOaSop selectMesOaSopByOaId(String oaId);

    /**
     *
     */
    public MesOaSop selectInfoByOaId(String oaId);

    /**
     * 查询oa签核列表
     *
     * @param mesOaSop oa签核
     * @return oa签核集合
     */
    public List<MesOaSop> selectMesOaSopList(MesOaSop mesOaSop);

    List<MesOaSop> selectMesOaSopVersionNum(MesOaSop mesOaSop);
    List<MesOaSop> selectMesOaSopVersionNum1(MesOaSop mesOaSop);

    /**
     * 新增oa签核
     *
     * @param mesOaSop oa签核
     * @return 结果
     */
    public int insertMesOaSop(MesOaSop mesOaSop);

    /**
     * 修改oa签核
     *
     * @param mesOaSop oa签核
     * @return 结果
     */
    public int updateMesOaSop(MesOaSop mesOaSop);

    /**
     * 批量删除oa签核
     *
     * @param oaIds 需要删除的oa签核主键集合
     * @return 结果
     */
    public int deleteMesOaSopByOaIds(String[] oaIds);

    /**
     * 删除oa签核信息
     *
     * @param oaId oa签核主键
     * @return 结果
     */
    public int deleteMesOaSopByOaId(String oaId);

//    public String GetMaxOaId();




    public MesOaSop selectMesOaSopByRequestId(String  requestId);
    public List<Long> selectSopIdsByPartNo(MesOaSop mesOaSop1);

    public Long updateMesOaSopByRequestId(MesOaSop mesOaSop);
    public Long updateMesOaSopByOaId(MesOaSop mesOaSop);


    /**
     * 根据五个id（model 、line、stage、process、material）查询已签核的sopid列表
     *
     * @param mesOaSop
     * @return 结果
     */
    public List<Long> selectSignedSopListByIds(MesOaSop mesOaSop);

    /**
     * 查询某用户名下的签核单（未签核、已签核、终止）
     */
    public List<MesOaSop> selectSignOffListByUser(MesOaSop mesOaSop);
    public List<MesOaSop> selectSignOffDetailByUser(MesOaSop mesOaSop);

    /**
     * 查询某用户上传的签核单
     */
    public List<MesOaSop> selectUploadListByUser(MesOaSop mesOaSop);

}
