package com.ruoyi.product.service;

import com.ruoyi.common.core.domain.AjaxResult;
import com.ruoyi.product.domain.MesMac;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.multipart.MultipartFile;

import java.util.List;

public interface IMesMacService {

    public List<MesMac> selectMesMacList(MesMac mesMac);

    List<MesMac> selectLineByMac(MesMac mesMac);

    List<MesMac> selectStageByMac(MesMac mesMac);

    List<MesMac> selectProcessByMac(MesMac mesMac);

    public List<MesMac> selectProcessList(MesMac mesMac);

    public MesMac selectMesMacByMacId(Long macId);

    public String checkMacNameUnique(MesMac mesMac);

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
     * 修改mac地址状态
     *
     * @param mesMac mac地址管理
     * @return 结果
     */
    public int updateMacStatus(MesMac mesMac);

    int deleteMesMacByMacId(Long macId);

    /**
     * 批量删除mac地址管理
     *
     * @param macIds 需要删除的区段管理主键集合
     * @return 结果
     */
    public int deleteMesMacByMacIds(Long[] macIds);

//    public AjaxResult importMacExcel(MultipartFile file);

    //    参数只包含file和id
    AjaxResult importMacExcel(@RequestParam("file") MultipartFile file, MesMac mesMac);

//    /**
//     * mac地址导入模板下载（可用，未用）
//     * @return 字节流
//     */
//    public void downloadExcel(HttpServletResponse response, HttpServletRequest request);
}
