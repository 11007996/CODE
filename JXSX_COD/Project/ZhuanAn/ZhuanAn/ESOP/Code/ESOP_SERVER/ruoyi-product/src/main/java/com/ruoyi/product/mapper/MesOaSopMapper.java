package com.ruoyi.product.mapper;

import com.ruoyi.product.domain.MesOaSop;
import org.springframework.stereotype.Repository;

import java.util.List;

/**
 * oa签核Mapper接口
 *
 * @author ruoyi
 * @date 2022-09-21
 */
@Repository
public interface MesOaSopMapper
{
    /**
     * 查询oa签核
     *
     * @param oaId oa签核主键
     * @return oa签核
     */
    public MesOaSop selectMesOaSopByOaId(String oaId);
    public MesOaSop selectInfoByOaId(String oaId);

    /**
     * 查询oa签核列表
     *
     * @param mesOaSop oa签核
     * @return oa签核集合
     */
    public List<MesOaSop> selectMesOaSopList(MesOaSop mesOaSop);

    /**
     * 查询版本个数（根据 线别+站位序号stagId 确定唯一性）
     */
    public List<MesOaSop> selectMesOaSopVersionNum(MesOaSop mesOaSop);
    public List<MesOaSop> selectMesOaSopVersionNum1(MesOaSop mesOaSop);

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
     * 删除oa签核
     *
     * @param oaId oa签核主键
     * @return 结果
     */
    public int deleteMesOaSopByOaId(String oaId);

    /**
     * 批量删除oa签核
     *
     * @param oaIds 需要删除的数据主键集合
     * @return 结果
     */
    public int deleteMesOaSopByOaIds(String[] oaIds);

//    public String GetMaxOaId();

    public MesOaSop selectMesOaSopByRequestId(String requestId);
    public List<Long> selectSopIdsByPartNo(MesOaSop mesOaSop1);

    public Long updateMesOaSopByRequestId(MesOaSop mesOaSop);

    public Long updateMesOaSopByOaId(MesOaSop mesOaSop);

    public List<Long> selectSignedSopListByIds(MesOaSop mesOaSop);
    public List<MesOaSop> selectSignOffListByUser(MesOaSop mesOaSop);
    public List<MesOaSop> selectSignOffDetailByUser(MesOaSop mesOaSop);

}
