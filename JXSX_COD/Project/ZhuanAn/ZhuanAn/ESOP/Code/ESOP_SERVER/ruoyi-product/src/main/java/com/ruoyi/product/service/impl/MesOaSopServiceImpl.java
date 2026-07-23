package com.ruoyi.product.service.impl;

import com.ruoyi.common.utils.DateUtils;
import com.ruoyi.product.domain.MesOaSop;
import com.ruoyi.product.mapper.MesOaSopMapper;
import com.ruoyi.product.service.IMesOaSopService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;

import static com.ruoyi.common.utils.SecurityUtils.getUsername;

/**
 * oa签核Service业务层处理
 *
 * @author ruoyi
 * @date 2022-09-21
 */
@Service
public class MesOaSopServiceImpl implements IMesOaSopService
{
    @Autowired
    private MesOaSopMapper mesOaSopMapper;

    /**
     * 查询oa签核
     *
     * @param oaId oa签核主键
     * @return oa签核
     */
    @Override
    public MesOaSop selectMesOaSopByOaId(String oaId)
    {
        return mesOaSopMapper.selectMesOaSopByOaId(oaId);
    }

    @Override
    public MesOaSop selectInfoByOaId(String oaId)
    {
        return mesOaSopMapper.selectInfoByOaId(oaId);
    }



    /**
     * 查询oa签核列表
     *
     * @param mesOaSop oa签核
     * @return oa签核
     */
    @Override
    public List<MesOaSop> selectMesOaSopList(MesOaSop mesOaSop)
    {
        return mesOaSopMapper.selectMesOaSopList(mesOaSop);
    }

    /**
     * 查询版本个数（根据 线别+站位序号stagId 确定唯一性）
     */
    @Override
    public List<MesOaSop> selectMesOaSopVersionNum(MesOaSop mesOaSop)
    {
        return mesOaSopMapper.selectMesOaSopVersionNum(mesOaSop);
    }
    @Override
    public List<MesOaSop> selectMesOaSopVersionNum1(MesOaSop mesOaSop)
    {
        return mesOaSopMapper.selectMesOaSopVersionNum1(mesOaSop);
    }

    /**
     * 新增oa签核
     *
     * @param mesOaSop oa签核
     * @return 结果
     */
    @Override
    public int insertMesOaSop(MesOaSop mesOaSop)
    {
        mesOaSop.setCreateTime(DateUtils.getNowDate());
        return mesOaSopMapper.insertMesOaSop(mesOaSop);
    }

    /**
     * 修改oa签核
     *
     * @param mesOaSop oa签核
     * @return 结果
     */
    @Override
    public int updateMesOaSop(MesOaSop mesOaSop)
    {
        mesOaSop.setUpdateTime(DateUtils.getNowDate());
        return mesOaSopMapper.updateMesOaSop(mesOaSop);
    }

    /**
     * 批量删除oa签核
     *
     * @param oaIds 需要删除的oa签核主键
     * @return 结果
     */
    @Override
    public int deleteMesOaSopByOaIds(String[] oaIds)
    {
        return mesOaSopMapper.deleteMesOaSopByOaIds(oaIds);
    }

    /**
     * 删除oa签核信息
     *
     * @param oaId oa签核主键
     * @return 结果
     */
    @Override
    public int deleteMesOaSopByOaId(String oaId)
    {
        return mesOaSopMapper.deleteMesOaSopByOaId(oaId);
    }

//    @Override
//    public String GetMaxOaId() {
//        return mesOaSopMapper.GetMaxOaId();
//    }

    public MesOaSop selectMesOaSopByRequestId(String requestId){
        return mesOaSopMapper.selectMesOaSopByRequestId(requestId);
    }

    public List<Long> selectSopIdsByPartNo(MesOaSop mesOaSop1){
        return mesOaSopMapper.selectSopIdsByPartNo(mesOaSop1);
    }

    public Long updateMesOaSopByRequestId(MesOaSop mesOaSop){
        return mesOaSopMapper.updateMesOaSopByRequestId(mesOaSop);
    }

    public Long updateMesOaSopByOaId(MesOaSop mesOaSop){
        return mesOaSopMapper.updateMesOaSopByOaId(mesOaSop);
    }

    public List<Long> selectSignedSopListByIds(MesOaSop mesOaSop){
        return mesOaSopMapper.selectSignedSopListByIds(mesOaSop);
    }

    /**
     * 查询某用户名下的签核单（未签核、已签核、终止）
     */
    public List<MesOaSop> selectSignOffListByUser(MesOaSop mesOaSop){
        mesOaSop.setCountersignUser(getUsername());
        return mesOaSopMapper.selectSignOffListByUser(mesOaSop);
    }
    /**
     * 查询某用户名下的签核单详情（未签核、已签核、终止）
     */
    public List<MesOaSop> selectSignOffDetailByUser(MesOaSop mesOaSop){
        mesOaSop.setCountersignUser(getUsername());
        return mesOaSopMapper.selectSignOffDetailByUser(mesOaSop);
    }

    public List<MesOaSop> selectUploadListByUser(MesOaSop mesOaSop){
        mesOaSop.setCreateBy(getUsername());
        return mesOaSopMapper.selectSignOffListByUser(mesOaSop);
    }

}
