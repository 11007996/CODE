package com.ruoyi.product.service.impl;

import com.ruoyi.common.constant.UserConstants;
import com.ruoyi.common.core.domain.AjaxResult;
import com.ruoyi.common.utils.DateUtils;
import com.ruoyi.common.utils.StringUtils;
import com.ruoyi.common.utils.poi.ExcelUtil;
import com.ruoyi.product.domain.MacExcel;
import com.ruoyi.product.domain.MesMac;
import com.ruoyi.product.mapper.MesMacMapper;
import com.ruoyi.product.service.IMesMacService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.multipart.MultipartFile;

import java.io.InputStream;
import java.util.List;

import static org.apache.commons.lang3.SystemUtils.getUserName;

@Service
public class MesMacServiceImpl implements IMesMacService {

    @Autowired
    private MesMacMapper mesMacMapper;

    @Override
    public List<MesMac> selectMesMacList(MesMac mesMac)
    {
        return mesMacMapper.selectMesMacList(mesMac);
    }
    @Override
    public List<MesMac> selectLineByMac(MesMac mesMac)
    {
        return mesMacMapper.selectLineByMac(mesMac);
    }
    @Override
    public List<MesMac> selectStageByMac(MesMac mesMac)
    {
        return mesMacMapper.selectStageByMac(mesMac);
    }
    @Override
    public List<MesMac> selectProcessByMac(MesMac mesMac) {
        return mesMacMapper.selectProcessByMac(mesMac);
    }
    @Override
    public List<MesMac> selectProcessList(MesMac mesMac)
    {
        return mesMacMapper.selectProcessList(mesMac);
    }
    @Override
    public MesMac selectMesMacByMacId(Long macId)
    {
        return mesMacMapper.selectMesMacByMacId(macId);
    }

    /**
     * 检查mac名称是否唯一
     *
     * @param mesMac 区段信息
     * @return
     */
    @Override
    public String checkMacNameUnique(MesMac mesMac) {
        Long macId = StringUtils.isNull(mesMac.getMacId()) ? -1L : mesMac.getMacId();
        MesMac info = mesMacMapper.checkMacNameUnique(mesMac);
        if (StringUtils.isNotNull(info) && info.getMacId().longValue() != macId.longValue())
        {
            return UserConstants.NOT_UNIQUE;
        }
        return UserConstants.UNIQUE;
    }

    /**
     * 新增mac地址管理
     *
     * @param mesMac mac地址管理
     * @return 结果
     */
    @Override
    public int insertMesMac(MesMac mesMac)
    {
        mesMac.setCreateTime(DateUtils.getNowDate());
//        for (int i = 0; i < mesMac.getMacList().size(); i++) {.
//            mesMac.setMacName(mesMac.getMacList().get(i).getMacName());
//            mesMac.setMacArea(mesMac.getMacList().get(i).getMacArea());
//            mesMac.setRemark(mesMac.getMacList().get(i).getRemark());
//            mesMacMapper.insertMesMac(mesMac);
//        }
        return mesMacMapper.insertMesMac(mesMac);
    }

    /**
     * 修改区段管理
     *
     * @param mesMac mac地址管理
     * @return 结果
     */
    @Override
    public int updateMesMac(MesMac mesMac) {
        mesMac.setUpdateTime(DateUtils.getNowDate());
        return mesMacMapper.updateMesMac(mesMac);
    }

    /**
     * 修改mac地址状态
     *
     * @param mesMac 状态信息
     * @return 结果
     */
    @Override
    public int updateMacStatus(MesMac mesMac) {
//        mesMac.setUpdateTime(DateUtils.getNowDate());
        return mesMacMapper.updateMesMac(mesMac);
    }

    /**
     * 删除mac地址管理
     *
     * @param macId mac地址管理主键
     * @return 结果
     */
    @Override
    public int deleteMesMacByMacId(Long macId) {
        return mesMacMapper.deleteMesMacByMacId(macId);
    }

    /**
     * 批量删除mac地址管理
     *
     * @param macIds 需要删除的mac地址管理主键
     * @return 结果
     */
    @Override
    public int deleteMesMacByMacIds(Long[] macIds) {
        return mesMacMapper.deleteMesMacByMacIds(macIds);
    }

//    @Override
//    (参数只包含了file，加上id（lineId、stagId、processId）的情况下无法使用)
//    public AjaxResult importMacExcel(@RequestParam("file") MultipartFile file){
//        ExcelUtil<MacExcel> util = new ExcelUtil<MacExcel>(MacExcel.class);
//        InputStream is = null;
//        List<MacExcel> list = null;
//        String msg = "";
//        try {
//            is = file.getInputStream();
//            list = util.importExcel(is);
//            MesMac mesMac = new MesMac();
//            String macName;
//            String macArea;
//            for (MacExcel item:list){
//                macName = item.getMac_name();
//                macArea = item.getMac_area();
//                mesMac.setMacName(macName);
//                mesMac.setMacArea(macArea);
//                List<MesMac> macInfo = mesMacMapper.selectMesMacList(mesMac);
//                int macInfoNum = macInfo.size();
//                if(macInfoNum > 0){
//                    continue;
//                }
//                mesMac.setCreateTime(DateUtils.getNowDate());
//                mesMac.setCreateBy(getUserName());
//                mesMacMapper.insertMesMac(mesMac);
//            }
//            if ("".equals(msg)){
//                msg = "导入成功";
//                return AjaxResult.success(msg);
//            }
//        } catch (Exception e) {
//            e.printStackTrace();
//            return AjaxResult.error("导入异常" + e.toString());
//        }
//        return AjaxResult.success(msg);
//    }

    @Override
//    参数包含file和id
    public AjaxResult importMacExcel(@RequestParam("file") MultipartFile file,MesMac mac){
        ExcelUtil<MacExcel> util = new ExcelUtil<MacExcel>(MacExcel.class);
        InputStream is = null;
        List<MacExcel> list = null;
        String msg = "";
        try {
            is = file.getInputStream();
            list = util.importExcel(is);
            MesMac mesMac = new MesMac();
            String macName;
            String macArea;
            mesMac.setLineId(mac.getLineId());
            mesMac.setStageId(mac.getStageId());
            mesMac.setProcessId(mac.getProcessId());
            for (MacExcel item:list){
                macName = item.getMacName();
                macArea = item.getMacArea();
                mesMac.setMacName(macName);
                mesMac.setMacArea(macArea);
                List<MesMac> macInfo = mesMacMapper.selectMesMacList(mesMac);
                int macInfoNum = macInfo.size();
                if(macInfoNum > 0){
                    continue;
                }
                mesMac.setCreateTime(DateUtils.getNowDate());
                mesMac.setCreateBy(getUserName());
                mesMacMapper.insertMesMac(mesMac);
            }
            if ("".equals(msg)){
                msg = "导入成功";
                return AjaxResult.success(msg);
            }
        } catch (Exception e) {
            e.printStackTrace();
            return AjaxResult.error("导入异常" + e.toString());
        }
        return AjaxResult.success(msg);
    }


//    /**
//     * mac地址导入模板下载（可用，未用）
//     * @return 字节流
//     */
//    @Override
//    public void downloadExcel(HttpServletResponse response, HttpServletRequest request){
//        //方法一：直接下载路径下的文件模板（这种方式貌似在SpringCloud和Springboot中，打包成JAR包时，无法读取到指定路径下面的文件，不知道记错没，你们可以自己尝试下！！！）
//        try {
//            //获取要下载的模板名称
//            String fileName = "MacExcelTemplate.xlsx";
//            fileName = new String(fileName.getBytes("GBK"),"ISO-8859-1");//文件名称fileName从"GBK"转为"ISO-8859-1"
//            //设置要下载的文件的名称
//            response.setHeader("Content-disposition", "attachment;fileName=" + fileName);
//            //通知客服文件的MIME类型
//            response.setContentType("application/vnd.ms-excel;charset=UTF-8");
//            //获取文件的路径
//            String filePath = getClass().getResource("/Template/" + fileName).getPath();
//            FileInputStream input = new FileInputStream(filePath);
//            OutputStream out = response.getOutputStream();
//            byte[] b = new byte[2048];
//            int len;
//            while ((len = input.read(b)) != -1) {
//                out.write(b, 0, len);
//            }
//            //修正 Excel在“xxx.xlsx”中发现不可读取的内容。是否恢复此工作薄的内容？如果信任此工作簿的来源，请点击"是"
//            response.setHeader("Content-Length", String.valueOf(input.getChannel().size()));
//            input.close();
//        } catch (Exception ex) {
//            ex.printStackTrace();
//        }
//    }
}
