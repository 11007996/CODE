package com.ruoyi.web.controller.product;

import com.alibaba.fastjson2.JSONObject;
import com.ruoyi.common.annotation.Log;
import com.ruoyi.common.constant.UserConstants;
import com.ruoyi.common.core.controller.BaseController;
import com.ruoyi.common.core.domain.AjaxResult;
import com.ruoyi.common.core.page.TableDataInfo;
import com.ruoyi.common.enums.BusinessType;
import com.ruoyi.common.utils.DateUtils;
import com.ruoyi.common.utils.SecurityUtils;
import com.ruoyi.common.utils.poi.ExcelUtil;
import com.ruoyi.product.domain.MesLine;
import com.ruoyi.product.domain.MesSite;
import com.ruoyi.product.service.IMesLineService;
import com.ruoyi.replace.domain.MesMaterialRelateLine;
import com.ruoyi.replace.service.IMesMaterialInfoService;
import com.ruoyi.replace.service.IMesMaterialRelateLineService;
import com.ruoyi.system.domain.MesUserRelateLine;
import com.ruoyi.system.service.IMesUserRelateLineService;
import org.apache.ibatis.annotations.Param;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.web.bind.annotation.*;

import javax.servlet.http.HttpServletResponse;
import java.util.ArrayList;
import java.util.List;
import java.util.Map;

/**
 * 线别信息Controller
 *
 * @author ruoyi
 * @date 2022-09-14
 */
@RestController
@RequestMapping("/product/line")
public class MesLineController extends BaseController
{
    @Autowired
    private IMesLineService mesLineService;

    @Autowired
    private IMesMaterialInfoService mesMaterialInfoService;

    @Autowired
    private IMesMaterialRelateLineService mesMaterialRelateLineService;
    @Autowired
    private IMesUserRelateLineService mesUserRelateLineService;

    /**
     * 查询线别信息列表
     */
//    @PreAuthorize("@ss.hasPermi('product:line:list')")
    @GetMapping("/list")
    public TableDataInfo list(MesLine mesLine)
    {
        startPageJson();
        List<MesLine> list = mesLineService.selectMesLineList(mesLine);
        return getDataTable(list);
    }

//    public TableDataInfo list(MesLine mesLine)
//    {
//        startPage();
//        List<MesLine> list = mesLineService.selectMesLineList(mesLine);
//
//        int[] materialIdList = mesLine.getMaterialIdList();
//        String[] materialNameList = mesLine.getMaterialNameList();
//        return getDataTable(list);
//    }

    /**
     * 根据料号查询线别(暂时没用，复用的/list接口:/product/line/list?materialId=xx)
     */
    @GetMapping(value = "/list/{materialId}")
    public TableDataInfo list(@PathVariable("materialId") Long materialId)
    {
        List<MesMaterialRelateLine> list = mesMaterialRelateLineService.selectLineByMaterial(materialId);
        return getDataTable(list);
    }

    /**
     * 获取线别信息详细信息
     */
    @PreAuthorize("@ss.hasPermi('product:line:query')")
    @GetMapping(value = "/{lineId}")
    public AjaxResult getInfo(@PathVariable("lineId") Long lineId)
    {
        return AjaxResult.success(mesLineService.selectMesLineByLineId(lineId));
    }

    /**
     * 根据线别获取料号信息
     */
//    @PreAuthorize("@ss.hasPermi('product:line:query')")
//    @GetMapping(value = "/getMaterialListByLineId/{lineId}")
//    public String getMaterialListByLineId(@PathVariable("lineId") Long lineId)
//    {
//        List<MesLine> list= mesLineService.selectMaterialListByLineId(lineId);
//        List<Integer> MaterialIdList = new ArrayList<Integer>();
//        List<String> MaterialNameList = new ArrayList<String>();
//        for (int i = 0; i < list.size(); i++) {
//            MaterialIdList.add(Math.toIntExact((list.get(i).getMaterialId())));
//            MaterialNameList.add(list.get(i).getMaterialName());
//        }
//        String a = "{\"data\":"+
//                "{\"MaterialIdList\":"+ MaterialIdList +
//                ",\"MaterialNameList\":" + JSONObject.toJSONString(MaterialNameList)+
//                "}}";
//        return a;
//    }

    /**
     * 根据线别获取会签人员信息
     */
    @PreAuthorize("@ss.hasPermi('product:user:search')")
    @GetMapping(value = "/getUserListByLineId/{lineId}")
    public String getUserListByLineId(@PathVariable("lineId") Long lineId)
    {
        List<MesLine> list= mesLineService.selectUserListByLineId(lineId);
        List<Integer> UserIdList = new ArrayList<Integer>();
        List<String> UserNameList = new ArrayList<String>();
        for (int i = 0; i < list.size(); i++) {
            UserIdList.add(Math.toIntExact((list.get(i).getUserId())));
            UserNameList.add(list.get(i).getUserName());
        }
        String a = "{\"data\":"+
                "{\"UserIdList\":"+ UserIdList +
                ",\"UserNameList\":" + JSONObject.toJSONString(UserNameList)+
                "}}";
        return a;
    }


    /**
     * 根据线别获取所有站点(区段、制程、工站)
     */
    @GetMapping("/getTerminalListByLineId")
    public AjaxResult getTerminalList(@Param("linId") Long lineId, @Param("materialId") Long materialId)
    {
        startPage();
        List<Map<String, String>> list = mesLineService.selectTerminalListByLineId(lineId,materialId);
        return AjaxResult.success(list);
    }


//    /**
//     * 新增线别信息(一个线体绑一个料号)
//     */
//    @PreAuthorize("@ss.hasPermi('product:line:add')")
//    @Log(title = "线别信息", businessType = BusinessType.INSERT)
//    @PostMapping("/add")
//    public AjaxResult add(@RequestBody MesLine mesLine)
//    {
//        if (UserConstants.NOT_UNIQUE.equals(mesLineService.checkLineNameUnique(mesLine)))
//        {
//            return AjaxResult.error("新增线别'" + mesLine.getLineName() + "'失败，线别名称已存在");
//        }
//        mesLine.setCreateBy(getUsername());
//        mesLine.setCreateTime(DateUtils.getNowDate());
//        mesLineService.insertMesLine(mesLine);//插入数据到mes_line表
//        //查询line的id（添加到mes_material_relate_line表）
//        Long lineId = mesLineService.selectLineIdByTime(mesLine.getCreateTime());
//        //添加线别时，选择好绑定的料号，添加到mes_material_relate_line表
//        MesMaterialRelateLine mrl = new MesMaterialRelateLine();
//        mrl.setLineId(lineId);
//        mrl.setMaterialId(mesLine.getMaterialId());
//        mrl.setCreateBy(getUsername());
//        mesMaterialRelateLineService.insertMesMaterialRelateLine(mrl);
//        return AjaxResult.success("操作成功");
////        return toAjax(mesLineService.insertMesLine(mesLine));
//    }


    /**
     * 新增线别信息(多个线体绑多个料号)(弃用)
     * 新增线别信息(多个线体绑多个会签人员)(mulan在用)
     */
    @PreAuthorize("@ss.hasPermi('product:line:add')")
    @Log(title = "线别信息", businessType = BusinessType.INSERT)
    @PostMapping("/add")
    public AjaxResult add(@RequestBody MesLine mesLine)
    {
        if (UserConstants.NOT_UNIQUE.equals(mesLineService.checkLineNameUnique(mesLine)))
        {
            return AjaxResult.error("新增线别'" + mesLine.getLineName() + "'失败，线别名称已存在");
        }
        mesLine.setCreateBy(getUsername());
        mesLine.setCreateTime(DateUtils.getNowDate());
        mesLineService.insertMesLine(mesLine);//插入数据到mes_line表
//        //查询line的id（添加到mes_material_relate_line表）
//        Long lineId = mesLineService.selectLineIdByTime(mesLine.getCreateTime());
//        //添加线别时，选择好绑定的料号，添加到mes_material_relate_line表
//        int[] materialIdList;
//        materialIdList = mesLine.getMaterialIdList();
//        for (int i = 0; i < materialIdList.length; i++) {
//            MesMaterialRelateLine mrl = new MesMaterialRelateLine();
//            mrl.setLineId(lineId);
//            mrl.setMaterialId((long) materialIdList[i]);
//            mrl.setCreateBy(getUsername());
//            mesMaterialRelateLineService.insertMesMaterialRelateLine(mrl);
//        }
        //查询line的id（添加到mes_material_relate_line表）
        Long lineId = mesLineService.selectLineIdByTime(mesLine.getCreateTime());
        //添加线别时，选择好绑定的会签人员，添加到mes_user_relate_line表
        int[] userIdList;
        userIdList = mesLine.getUserIdList();
        for (int i = 0; i < userIdList.length; i++) {
            MesUserRelateLine murl = new MesUserRelateLine();
            murl.setLineId(lineId);
//            Long userId = Long.valueOf(userIdList[i]);
            murl.setUserName(SecurityUtils.getUsername());
            murl.setUserId(userIdList[i]);
            murl.setCreateBy(getUsername());
            mesUserRelateLineService.insertMesUserRelateLine(murl);
        }
        return AjaxResult.success("操作成功!");
    }



//    /**
//     * 修改线别信息
//     */
//    @PreAuthorize("@ss.hasPermi('product:line:edit')")
//    @Log(title = "线别信息", businessType = BusinessType.UPDATE)
//    @PutMapping("/edit")
//    public AjaxResult edit(@RequestBody MesLine mesLine)
//    {
////        mesLineService.checkLineAllowed(mesLine);
////        mesLineService.checkLineDataScope(mesLine.getLineId());
//        if(UserConstants.NOT_UNIQUE.equals(mesLineService.checkLineNameUnique(mesLine)))
//        {
//            return AjaxResult.error("修改线别'" + mesLine.getLineName() + "'失败，线别名称已存在");
//        }
//        mesLine.setUpdateBy(getUsername());
//        mesLine.setSiteId(mesLine.getSiteId());
//        mesLineService.updateMesLine(mesLine);
//        //修改mes_material_relate_line表
//        //1、先根据lineId删掉所有的料号记录
//        mesMaterialRelateLineService.deleteMesMaterialRelateLineById(mesLine.getLineId());
//        //2、再根据lineId添加料号记录
//        int[] materialIdList;
//        materialIdList = mesLine.getMaterialIdList();
//        for (int i = 0; i < materialIdList.length; i++) {
//            MesMaterialRelateLine mrl = new MesMaterialRelateLine();
//            mrl.setLineId(mesLine.getLineId());
//            mrl.setMaterialId((long) materialIdList[i]);
//            mrl.setUpdateBy(getUsername());
//            mrl.setUpdateTime(DateUtils.getNowDate());
////            mesMaterialRelateLineService.updateMesMaterialRelateLine(mrl);
//            mesMaterialRelateLineService.insertMesMaterialRelateLine(mrl);
//        }
//        return AjaxResult.success("操作成功!");
//    }

    /**
     * 修改线别信息
     */
    @PreAuthorize("@ss.hasPermi('product:line:edit')")
    @Log(title = "线别信息", businessType = BusinessType.UPDATE)
    @PutMapping("/edit")
    public AjaxResult edit(@RequestBody MesLine mesLine)
    {
        if(UserConstants.NOT_UNIQUE.equals(mesLineService.checkLineNameUnique(mesLine)))
        {
            return AjaxResult.error("修改线别'" + mesLine.getLineName() + "'失败，线别名称已存在");
        }
        mesLine.setUpdateBy(getUsername());
        mesLine.setSiteId(mesLine.getSiteId());
        mesLineService.updateMesLine(mesLine);
        return AjaxResult.success("操作成功!");
    }

    /**
     * 修改线别状态
     */
    @PreAuthorize("@ss.hasPermi('product:line:edit')")
    @Log(title = "线别信息", businessType = BusinessType.UPDATE)
    @PutMapping("/changeStatus")
    public AjaxResult changeStatus(@RequestBody MesLine mesLine)
    {
//        mesLineService.checkLineAllowed(mesLine);
//        mesLineService.checkLineDataScope(mesLine.getLineId());
        mesLine.setUpdateBy(getUsername());
        mesLine.setUpdateTime(DateUtils.getNowDate());
        return toAjax(mesLineService.updateLineStatus(mesLine));
    }

    /**
     * 删除线别信息
     */
    @PreAuthorize("@ss.hasPermi('product:line:remove')")
    @Log(title = "线别信息", businessType = BusinessType.DELETE)
	@DeleteMapping("/{lineIds}")
    public AjaxResult remove(@PathVariable Long[] lineIds)
    {
        return toAjax(mesLineService.deleteMesLineByLineIds(lineIds));
    }

    /**
     * 导出线别信息
     */
    @PreAuthorize("@ss.hasPermi('product:line:export')")
    @Log(title = "线别信息", businessType = BusinessType.EXPORT)
    @PostMapping("/export")
    public void export(HttpServletResponse response, MesLine mesLine)
    {
        List<MesLine> list = mesLineService.selectMesLineList(mesLine);
        ExcelUtil<MesLine> util = new ExcelUtil<MesLine>(MesLine.class);
        util.exportExcel(response, list, "线别信息数据");
    }
}
