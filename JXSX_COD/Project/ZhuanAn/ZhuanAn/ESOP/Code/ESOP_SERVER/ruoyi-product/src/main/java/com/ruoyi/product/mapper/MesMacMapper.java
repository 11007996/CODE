package com.ruoyi.product.mapper;

import com.ruoyi.product.domain.MesMac;

import java.util.List;

public interface MesMacMapper {
    /**
     * 获取mac列表
     *
     * @param mesMac
     * @return
     */
    public List<MesMac> selectMesMacList(MesMac mesMac);
    public List<MesMac> selectLineByMac(MesMac mesMac);
    public List<MesMac> selectStageByMac(MesMac mesMac);
    public List<MesMac> selectProcessByMac(MesMac mesMac);
    public List<MesMac> selectProcessList(MesMac mesMac);

    /**
     * 获取mac详细信息
     *
     * @param macId
     * @return
     */
    public MesMac selectMesMacByMacId(Long macId);

    /**
     * 校验mac名称是否唯一
     *
     * @param macName mac名称
     * @return 区段信息
     */
    public MesMac checkMacNameUnique(MesMac mesMac);

    /**
     * 新增mac地址管理
     *
     * @param mesMac mac地址管理
     * @return 结果
     */
    public int insertMesMac(MesMac mesMac);

    /**
     * 修改mac地址管理
     *
     * @param mesMac mac地址管理
     * @return 结果
     */
    public int updateMesMac(MesMac mesMac);

    /**
     * 删除mac地址管理
     *
     * @param macId mac地址管理主键
     * @return 结果
     */
    public int deleteMesMacByMacId(Long macId);

    /**
     * 批量删除mac地址管理
     *
     * @param macIds 需要删除的数据主键集合
     * @return 结果
     */
    public int deleteMesMacByMacIds(Long[] macIds);

}