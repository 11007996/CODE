package com.ruoyi.product.service.impl;

import java.io.File;
import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;
import java.util.Calendar;
import java.util.List;
import java.util.UUID;

import com.ruoyi.common.config.RuoYiConfig;
import com.ruoyi.common.core.domain.AjaxResult;
import com.ruoyi.common.utils.DateUtils;
import com.ruoyi.common.utils.StringUtils;
import com.ruoyi.common.utils.file.FileUtils;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import com.ruoyi.product.mapper.MesSealMapper;
import com.ruoyi.product.domain.MesSeal;
import com.ruoyi.product.service.IMesSealService;
import org.springframework.web.multipart.MultipartFile;

/**
 * 印章Service业务层处理
 *
 * @author ruoyi
 * @date 2024-04-18
 */
@Service
public class MesSealServiceImpl implements IMesSealService
{
    @Autowired
    private MesSealMapper mesSealMapper;

    /**
     * 查询印章
     *
     * @param sealId 印章主键
     * @return 印章
     */
    @Override
    public MesSeal selectMesSealBySealId(Long sealId)
    {
        return mesSealMapper.selectMesSealBySealId(sealId);
    }

    /**
     * 查询印章列表
     *
     * @param mesSeal 印章
     * @return 印章
     */
    @Override
    public List<MesSeal> selectMesSealList(MesSeal mesSeal)
    {
        return mesSealMapper.selectMesSealList(mesSeal);
    }

    /**
     * 新增印章
     *
     * @param mesSeal 印章
     * @return 结果
     */
    @Override
    public AjaxResult addMesSeal(MesSeal mesSeal, MultipartFile file) throws IOException {
        String fileName = file.getOriginalFilename();
        if (file.isEmpty()) {
            return AjaxResult.error(400, "图片参数错误");
        }else {
            if (FileUtils.chakeFileSize(file.getSize(), 10, "M")) {
                return AjaxResult.error(500, "图片太大，请上传10MB以内的图片!");
            }
        }
        //获取时间创建文件夹
        Calendar calendar = Calendar.getInstance();
        int year = calendar.get(Calendar.YEAR);
        String month = StringUtils.leftPad(String.valueOf(calendar.get(Calendar.MONTH) + 1), 2, '0');
        String day = StringUtils.leftPad(String.valueOf(calendar.get(Calendar.DAY_OF_MONTH)), 2, '0');

        //获取随机数
        Integer random = (int) (Math.random()*10000);
        Integer time =  (int)(System.currentTimeMillis() / 1000 % 1000);
        String randompath = random +time.toString();

        //服务器真实地址
        Path tempDir = Paths.get(RuoYiConfig.getProfile(),"/upload","/"+year,"/"+month,"/"+day,"/"+randompath);
        if (!Files.exists(tempDir)) {
            Files.createDirectories(tempDir);
        }

        //文件在浏览器获取的地址（不包含文件）
        String browserUrlExfilename = "/profile/upload/seal/"+year+"/"+month+"/"+day+"/"+randompath;
        //文件在浏览器获取的地址(包含文件)
        String browserUrl = browserUrlExfilename+"/"+fileName;

        mesSeal.setOriginalName(browserUrlExfilename);
        mesSeal.setSealName(browserUrlExfilename);
        mesSeal.setFilePath(browserUrl);
        mesSeal.setCreateTime(DateUtils.getNowDate());

        mesSealMapper.insertMesSeal(mesSeal);
        return AjaxResult.success();
    }

    /**
     * 修改印章
     *
     * @param mesSeal 印章
     * @return 结果
     */
    @Override
    public int updateMesSeal(MesSeal mesSeal)
    {
        mesSeal.setUpdateTime(DateUtils.getNowDate());
        return mesSealMapper.updateMesSeal(mesSeal);
    }

    /**
     * 批量删除印章
     *
     * @param sealIds 需要删除的印章主键
     * @return 结果
     */
    @Override
    public int deleteMesSealBySealIds(Long[] sealIds)
    {
        return mesSealMapper.deleteMesSealBySealIds(sealIds);
    }

    /**
     * 删除印章信息
     *
     * @param sealId 印章主键
     * @return 结果
     */
    @Override
    public int deleteMesSealBySealId(Long sealId)
    {
        return mesSealMapper.deleteMesSealBySealId(sealId);
    }
}
