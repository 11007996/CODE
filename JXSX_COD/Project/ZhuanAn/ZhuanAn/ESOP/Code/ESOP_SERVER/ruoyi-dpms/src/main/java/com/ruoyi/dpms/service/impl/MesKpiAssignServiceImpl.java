package com.ruoyi.dpms.service.impl;

import com.alibaba.fastjson2.JSON;
import com.alibaba.fastjson2.JSONArray;
import com.alibaba.fastjson2.JSONObject;
import com.ruoyi.common.core.domain.AjaxResult;
import com.ruoyi.common.utils.DateUtils;
import com.ruoyi.dpms.domain.*;
import com.ruoyi.dpms.mapper.*;
import com.ruoyi.dpms.service.IMesKpiAssignService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;

import static com.ruoyi.common.utils.SecurityUtils.*;

/**
 * KPI指派Service业务层处理
 * 
 * @author ruoyi
 * @date 2022-11-10
 */
@Service
public class MesKpiAssignServiceImpl implements IMesKpiAssignService
{
    @Autowired
    private MesKpiAssignMapper mesKpiAssignMapper;

    @Autowired
    private MesKpiPersonGroupMapper mesKpiPersonGroupMapper;

    @Autowired
    private MesKpiDetailMapper mesKpiDetailMapper;

    @Autowired
    private MesKpiObjectMapper mesKpiObjectMapper;

    @Autowired
    private MesKpiObject1Mapper mesKpiObject1Mapper;

//    private static final Logger log = LoggerFactory.getLogger(MesKpiAssignServiceImpl.class);


    /**
     * 查询KPI指派
     * 
     * @param id KPI指派主键
     * @return KPI指派
     */
    @Override
    public MesKpiAssign selectMesKpiAssignById(Long id)
    {
        return mesKpiAssignMapper.selectMesKpiAssignById(id);
    }


    /**
     * 查询KPI指派列表
     *
     * @param mesKpiAssign KPI指派
     * @return KPI指派
     */
    @Override
    public List<MesKpiAssign> selectMesKpiAssignList(MesKpiAssign mesKpiAssign) {
//        获取当前登录用户，筛选用户指派的任务和用户被指派的任务
//        mesKpiAssign.setCreateBy(getLoginUser().getUsername());
        mesKpiAssign.setCreateBy(getUsername());
        List<MesKpiAssign> list = mesKpiAssignMapper.selectMesKpiAssignList(mesKpiAssign);
        //1、循环遍历最外面的一层list
        for (int i = 0; i < list.size(); i++) {
//            mesKpiAssign.setCreateBy(getUserId().toString());
                MesKpiAssign mesKpiAssign1 = list.get(i);
                long id = mesKpiAssign1.getId();
                //1.1、循环遍历人员群组list（appraisedPersonGroup和statisticalPersonGroup）
                String s1 = "";
                String s2 = "";
                List<MesKpiObject> list1 = mesKpiObjectMapper.selectMesKpiObjectById(id);
                for (int j = 0; j < list1.size(); j++) {
                    MesKpiObject mko = list1.get(j);
                    if (mko.getGroupType() == 0) {//判断人员群组类型为0，是被考核人员群组appraisedPersonGroup
//                    if (j == 0) {
//                        s1 = mko.toJSON();
//                    } else {
//                        s1 = s           1 + "," + mko.toJSON();
//                    }
                        s1 = s1 + mko.toJSON();
                    } else if (mko.getGroupType() == 1) {//判断人员群组类型为1，是统计人员群组appraisedPersonGroup
//                    if (j == 0) {
//                        s2 = mko.toJSON1();
//                    } else {
//                        s2 = s2 + "," + mko.toJSON1();
//                    }
                        s2 = s2 + mko.toJSON();
                    }
                }
                s1 = "[" + s1 + "]";
                s2 = "[" + s2 + "]";

                //1.2、循环遍历目标值list（targetList）
                String s3 = "";
//            String s4 ="";
                List<MesKpiObject1> list2 = mesKpiObject1Mapper.selectMesKpiObject1ById(id);
                for (int k = 0; k < list2.size(); k++) {
                    MesKpiObject1 mko1 = list2.get(k);
                    if (k == 0) {
                        s3 = mko1.toJSON();
                    } else {
                        s3 = s3 + "," + mko1.toJSON();
                    }
                }
                s3 = "[" + s3 + "]";
                // JSONArray.parseArray(s1);
                list.set(i, mesKpiAssign1).setAppraisedPersonGroup(JSONArray.parseArray(s1));
                list.set(i, mesKpiAssign1).setStatisticalPersonGroup(JSONArray.parseArray(s2));
//            list.set(i,mesKpiAssign1).setTargetList(s3);
                list.set(i, mesKpiAssign1).setTargetList(JSONArray.parseArray(s3));

            }
//        String mka = "[{name:'userAccount',value:''},{name:'userName',value:''},{name:'deptId',value:''},{name:'deptName',value:''},]";
//        JSONArray json = JSONArray.parseArray(mka); //首先把字符串转成 JSONArray  对象
            return list;
//        return mesKpiAssignMapper.selectMesKpiAssignList(mesKpiAssign);
    }


    /**
     * 新增KPI指派
     *
     * @param mesKpiAssign KPI指派
     * @return 结果
     */

    @Override
    public AjaxResult insertMesKpiAssign(String mesKpiAssign) {
//        log.debug(mesKpiAssign);
        JSONObject jsonObject = JSON.parseObject(mesKpiAssign);//传的数据转为json对象
        //System.out.println(jsonObject);

        //在表mes_kpi_assign插入数据
        MesKpiAssign mka = new MesKpiAssign();//初始化一个新的容器，存放传的数据
        mka.setCreateBy(getUsername());
        mka.setKpiYear(jsonObject.getLong("kpiYear"));//kpi年度（标题）

        mka.setKpiId(jsonObject.getLong("kpiId"));
        mka.setStatisticalCycle(jsonObject.getString("statisticalCycle"));
        mka.setThisYear(jsonObject.getLong("thisYear"));
        mka.setThisYearValue(jsonObject.getString("thisYearValue"));
        mka.setLastYear(jsonObject.getLong("lastYear"));
        mka.setLastYearValue(jsonObject.getString("lastYearValue"));
        mka.setCreateTime(DateUtils.getNowDate());



//        BigDecimal left = new BigDecimal(1).subtract(sum);
//        BigDecimal a = jsonObject.getBigDecimal("weightCoefficient");
        Float a = jsonObject.getFloat("weightCoefficient");
//            Float b = left -a;
        // kpiYear指派过，再限制权重占比
        if (mesKpiAssignMapper.selectKpiYear(jsonObject.getLong("kpiYear")) != null) {
            Float left = mesKpiAssignMapper.selectWeightCoefficientSumByItems(getDeptId(),jsonObject.getLong("kpiYear"));
            //限制已经指派过kpi_year的KPI权重占比
            //System.out.println(mesKpiAssignMapper.selectWeightCoefficientSumByItems(getDeptId(),jsonObject.getLong("kpiYear")));
            if (left<a) {
                return AjaxResult.error("该部门在"+jsonObject.getLong("kpiYear")+"年度的权重占比剩下"+left+","+"无法指派！");
            }
        }
        //kpi_year未指派过，判断权重≤100%，就直接插数据到表里
        else if(a >100){
//            Float left = Float.valueOf(100);
            return AjaxResult.error("权重占比大于100%，无法指派！");
        }

        mka.setWeightCoefficient(a);
        mesKpiAssignMapper.insertMesKpiAssign(mka);//插入数据
        //assign表插入数据之后，再查询一下当前登录用户部门某一年的kpi剩余值，放到最后保存成功信息的剩余kpi值提醒
        Float left = mesKpiAssignMapper.selectWeightCoefficientSumByItems(getDeptId(),jsonObject.getLong("kpiYear"));
        mesKpiAssignMapper.updateMesKpiStatus(mka.getKpiId());//根据index表的id，修改mes_kpi_index表的status为可修改

        long id = mesKpiAssignMapper.selectMesKpiAssignId(mka);//查询数据

        //在表mes_kpi_person_group插入数据(重命名：mes_kpi_dept_group)
        MesKpiPersonGroup mkpg = new MesKpiPersonGroup();
        mkpg.setAssignId(id);
        mkpg.setCreateBy(getUsername());
        mkpg.setCreateTime(DateUtils.getNowDate());

        String deptId;
        String userAccount;
        JSONObject appraisedPersonGroupObject = null;
        for (int i = 0; i < jsonObject.getJSONArray("appraisedPersonGroup").size(); i++) {
            appraisedPersonGroupObject = JSONObject.parseObject(jsonObject.getJSONArray("appraisedPersonGroup").get(i).toString());
            if(appraisedPersonGroupObject.size()== 0) {
                return AjaxResult.error("被考核部门和人员不可为空！");
            }
            deptId = appraisedPersonGroupObject.getString("deptId");
            userAccount = appraisedPersonGroupObject.getString("userAccount");
            if(deptId.equals("") || userAccount.equals("") || deptId == null || userAccount == null){
                return AjaxResult.error("被考核部门和人员不可为空！");
            }
            mkpg.setUserAccount(userAccount);
            mkpg.setDeptId(Long.valueOf(deptId));
            mkpg.setGroupType("0");
            mesKpiPersonGroupMapper.insertMesKpiPersonGroup(mkpg);
        }
        JSONObject statisticalPersonGroupObject = null;
        for (int i = 0; i < jsonObject.getJSONArray("statisticalPersonGroup").size(); i++) {
            statisticalPersonGroupObject = JSONObject.parseObject(jsonObject.getJSONArray("statisticalPersonGroup").get(i).toString());
            if(statisticalPersonGroupObject.size()== 0) { break; }
            deptId = statisticalPersonGroupObject.getString("deptId");
            userAccount = statisticalPersonGroupObject.getString("userAccount");
            if(deptId.equals("") || userAccount.equals("") || deptId == null || userAccount == null){
                return AjaxResult.error("统计部门和人员不可为空！");
            }
            mkpg.setUserAccount(userAccount);
            mkpg.setDeptId(Long.valueOf(deptId));
            mkpg.setGroupType("1");
            mesKpiPersonGroupMapper.insertMesKpiPersonGroup(mkpg);
        }

        //在表mes_kpi_dept_detail插入数据
        MesKpiDetail mkd = new MesKpiDetail();
        mkd.setTargetStandard(jsonObject.getLong("targetStandard"));
        JSONObject targetList = null;
        for (int i = 0; i < jsonObject.getJSONArray("targetList").size(); i++) {
            targetList = JSONObject.parseObject(jsonObject.getJSONArray("targetList").get(i).toString());
            mkd.setAssignId(id);
            mkd.setTargetParam(targetList.getString("label"));
            mkd.setTargetValue(targetList.getString("value"));
            mesKpiDetailMapper.insertMesKpiDetail(mkd);
        }
        mesKpiAssignMapper.updateMesKpiDetailStatus(mka.getId());//根据detail表的assign_id，修改mes_kpi_dept_detail表的status为可填报
        return AjaxResult.success("保存成功"+","+"该部门在"+jsonObject.getLong("kpiYear")+"年度的权重占比剩下"+left+"%。");
    }




    /**
     * 修改KPI指派
     * 
     * @param mesKpiAssign KPI指派
     * @return 结果
     */
    @Override
    public AjaxResult updateMesKpiAssign(String mesKpiAssign)
    {
//        log.debug(mesKpiAssign);
        JSONObject jsonObject = JSON.parseObject(mesKpiAssign);
        //在表mes_kpi_assign修改数据
        MesKpiAssign mka = new MesKpiAssign();//初始化一个新的容器，存放传的数据
        mka.setId(jsonObject.getLong("id"));
        mka.setCreateBy(getUsername());
        mka.setKpiId(jsonObject.getLong("kpiId"));
        mka.setWeightCoefficient(jsonObject.getFloat("weightCoefficient"));
        mka.setStatisticalCycle(jsonObject.getString("statisticalCycle"));
        mka.setKpiYear(jsonObject.getLong("year"));//年（标题）
        mka.setCreateTime(DateUtils.getNowDate());

        mesKpiAssignMapper.updateMesKpiAssign(mka);//修改数据
//        long id = mesKpiAssignMapper.selectMesKpiAssignId(mka);//查询指派ID，再根据指派ID串表修改其他表数据


        //在表mes_kpi_person_group修改数据——先删后增
        Long mkpgAssignId =  mka.getId();//获取json数据里面的id，也就是assign表里面的id
        mesKpiPersonGroupMapper.deleteMesKpiPersonGroupByAssignId(mkpgAssignId);

        MesKpiPersonGroup mkpg = new MesKpiPersonGroup();
        mkpg.setAssignId(mka.getId());
        mkpg.setCreateBy(getUsername());
        mkpg.setCreateTime(DateUtils.getNowDate());

        String deptId;
        String userAccount;
        JSONObject appraisedPersonGroupObject = null;
        for (int i = 0; i < jsonObject.getJSONArray("appraisedPersonGroup").size(); i++) {
            appraisedPersonGroupObject = JSONObject.parseObject(jsonObject.getJSONArray("appraisedPersonGroup").get(i).toString());
            if(appraisedPersonGroupObject.size()== 0) {
                return AjaxResult.error("被考核部门和人员不可为空！");
            }
            deptId = appraisedPersonGroupObject.getString("deptId");
            userAccount = appraisedPersonGroupObject.getString("userAccount");
            if(deptId.equals("") || userAccount.equals("") || deptId == null || userAccount == null){
                return AjaxResult.error("被考核部门和人员不可为空！");
            }
            mkpg.setUserAccount(userAccount);
            mkpg.setDeptId(Long.valueOf(deptId));
            mkpg.setGroupType("0");
            mesKpiPersonGroupMapper.insertMesKpiPersonGroup(mkpg);
        }
        JSONObject statisticalPersonGroupObject = null;
        for (int i = 0; i < jsonObject.getJSONArray("statisticalPersonGroup").size(); i++) {
            statisticalPersonGroupObject = JSONObject.parseObject(jsonObject.getJSONArray("statisticalPersonGroup").get(i).toString());
            if(statisticalPersonGroupObject.size()== 0) { break; }
            deptId = statisticalPersonGroupObject.getString("deptId");
            userAccount = statisticalPersonGroupObject.getString("userAccount");
            if(deptId.equals("") || userAccount.equals("") || deptId == null || userAccount == null){
                return AjaxResult.error("统计部门和人员不可为空！");
            }
            mkpg.setUserAccount(userAccount);
            mkpg.setDeptId(Long.valueOf(deptId));
            mkpg.setGroupType("1");
            mesKpiPersonGroupMapper.insertMesKpiPersonGroup(mkpg);
        }

        //在表mes_kpi_dept_detail修改数据
        Long mkdAssignId =  mka.getId();//获取json数据里面的id，也就是assign表里面的id,赋值给mkd表里面的assign_id
        mesKpiDetailMapper.deleteMesKpiDetailByAssignId(mkdAssignId);

        MesKpiDetail mkd = new MesKpiDetail();
        mkd.setAssignId(mka.getId());
        mkd.setCreateBy(getUsername());
        mkd.setCreateTime(DateUtils.getNowDate());

        JSONObject targetListObject = null;
        for (int i = 0; i < jsonObject.getJSONArray("targetList").size(); i++) {
            targetListObject = JSONObject.parseObject(jsonObject.getJSONArray("targetList").get(i).toString());
            mkd.setTargetParam(targetListObject.getString("label"));
            mkd.setTargetValue(targetListObject.getString("value"));
            mesKpiDetailMapper.insertMesKpiDetail(mkd);
        }
        return AjaxResult.success("修改成功!");
    }

    /**
     * 批量删除KPI指派
     * 
     * @param ids 需要删除的KPI指派主键
     * @return 结果
     */
    @Override
    public int deleteMesKpiAssignByIds(Long[] ids)
    {
        return mesKpiAssignMapper.deleteMesKpiAssignByIds(ids);
    }

    /**
     * 删除KPI指派信息
     * 
     * @param id KPI指派主键
     * @return 结果
     */
    @Override
    public int deleteMesKpiAssignById(Long id)
    {
        return mesKpiAssignMapper.deleteMesKpiAssignById(id);
    }
}
