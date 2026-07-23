package com.ruoyi.dpms.service.impl;

import com.alibaba.fastjson2.JSON;
import com.alibaba.fastjson2.JSONArray;
import com.alibaba.fastjson2.JSONObject;
import com.ruoyi.common.core.domain.AjaxResult;
import com.ruoyi.common.utils.DateUtils;
import com.ruoyi.dpms.domain.MesKpiDetail;
import com.ruoyi.dpms.domain.MesKpiFill;
import com.ruoyi.dpms.domain.MesKpiObject;
import com.ruoyi.dpms.domain.MesKpiObject1;
import com.ruoyi.dpms.mapper.MesKpiDetailMapper;
import com.ruoyi.dpms.mapper.MesKpiFillMapper;
import com.ruoyi.dpms.mapper.MesKpiObject1Mapper;
import com.ruoyi.dpms.mapper.MesKpiObjectMapper;
import com.ruoyi.dpms.service.IMesKpiFillService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;

import static com.ruoyi.common.utils.SecurityUtils.getUsername;


/**
 * KPI填报Service业务层处理
 *
 * @author ruoyi
 * @date 2022-11-16
 */

@Service
public class MesKpiFillServiceImpl implements IMesKpiFillService
{

    @Autowired
    private MesKpiFillMapper mesKpiFillMapper;
    @Autowired
    private MesKpiDetailMapper mesKpiDetailMapper;

    @Autowired
    private MesKpiObjectMapper mesKpiObjectMapper;

    @Autowired
    private MesKpiObject1Mapper mesKpiObject1Mapper;




    /**
     * 查询KPI填报
     *
     * @param mesKpiFill KPI填报
     * @return KPI填报
     */
//    @Override
//    public List<MesKpiFill> selectMesKpiFillList(MesKpiFill mesKpiFill) {
//        return mesKpiFillMapper.selectMesKpiFillList(mesKpiFill);
//    }

    @Override
    public List<MesKpiFill> selectMesKpiFillList(MesKpiFill mesKpiFill) {
        List<MesKpiFill> list = mesKpiFillMapper.selectMesKpiFillList(mesKpiFill);
        for (int i = 0; i < list.size(); i++) {
            MesKpiFill mesKpiFill1 = list.get(i);
            long id =  mesKpiFill1.getId();
            String s1 ="";
            String s2 ="";
            List<MesKpiObject> list1= mesKpiObjectMapper.selectMesKpiObjectById(id);
            for (int j = 0; j < list1.size(); j++) {
                MesKpiObject mko = list1.get(j);
                if (mko.getGroupType() == 0) {
//                    if (j == 0) {
//                        s1 = mko.toJSON();
//                    } else {
//                        s1 = s1 + "," + mko.toJSON();
//                    }
                    s1 = s1 + mko.toJSON();
                } else if(mko.getGroupType() == 1){
//                    if (j == 0) {
//                        s2 = mko.toJSON();
//                    } else {
//                        s2 = s2 + "," + mko.toJSON();
//                    }
                    s2 = s2 + mko.toJSON();
                }
            }
            s1 = "["+s1+"]";
            s2 = "["+s2+"]";
            //1.2、循环遍历目标值、实际值list（targetList和actualList）
            String s3 ="";
            String s4 ="";
            List<MesKpiObject1> list2= mesKpiObject1Mapper.selectMesKpiObject1ById(id);
            for (int k = 0; k < list2.size(); k++) {
                MesKpiObject1 mko1 = list2.get(k);
//                if(k == 0){
//                    s3 = mko1.toJSON();
//                    s4 = mko1.toJSON();
//                }
//                else{
//                    s3 = s3+","+mko1.toJSON();
//                    s4 = s4+","+mko1.toJSON();
//                }
                    s3 = s3 + mko1.toJSON();
                    s4 = s4 + mko1.toJSON1();
            }
            s3 = "["+s3+"]";
            s4 = "["+s4+"]";
            // JSONArray.parseArray(s1);
            list.set(i,mesKpiFill1).setAppraisedPersonGroup(JSONArray.parseArray(s1));
            list.set(i,mesKpiFill1).setStatisticalPersonGroup(JSONArray.parseArray(s2));
            list.set(i,mesKpiFill1).setTargetList(JSONArray.parseArray(s3));
            list.set(i,mesKpiFill1).setActualList(JSONArray.parseArray(s4));
        }
        return list;
    }

    /**
     * 修改KPI填报
     *
     * @param mesKpiFill KPI填报
     * @return 结果
     */
    @Override
    public AjaxResult updateMesKpiFill(String mesKpiFill)
    {

//        log.debug(mesKpiFill);
//        JSONObject jsonObject = JSON.parseObject(mesKpiFill);
//        MesKpiFill mkf = new MesKpiFill();//初始化一个新的容器，存放传的数据

        //在表mes_kpi_dept_detail修改数据——先删后增
//        Long id = mkf.getId();//获取json的id,也就是对应表mes_kpi_dept_detail里面的id，赋值给Id
//        mesKpiDetailMapper.deleteMesKpiDetailById(id);//根据id删除该条数据，再新增一条修改之后的数据
//
//        MesKpiDetail mkd = new MesKpiDetail();
//        mkd.setCreateBy(getUsername());
//        mkd.setCreateTime(DateUtils.getNowDate());
//
//        JSONObject actualListObject = null;
//        for (int i = 0; i < jsonObject.getJSONArray("actualList").size(); i++) {
//            actualListObject = JSONObject.parseObject(jsonObject.getJSONArray("actualList").get(i).toString());
//            mkd.setTargetParam(actualListObject.getString("label"));
//            mkd.setActualValue(actualListObject.getString("value"));
//            mesKpiDetailMapper.insertMesKpiDetail(mkd);
//        }

        //在表mes_kpi_dept_detail修改数据——update
        MesKpiDetail mkd = new MesKpiDetail();

        mkd.setCreateBy(getUsername());
        mkd.setCreateTime(DateUtils.getNowDate());

        JSONObject jsonObject = JSON.parseObject(mesKpiFill);
//        Long id = Long.valueOf(jsonObject.getJSONObject("id").toString());

        Long id = Long.valueOf(jsonObject.getString("id"));//这个地方的(key:"id")是前端传过来的id
        mkd.setId(id);//这个地方的setId是把前端传过来的id赋值，对应selectMesKpiFillList（http://localhost:8090/dpms/kpiFill/list）结果中的id（是assign_id）

//        JSONObject actualListObject = JSONObject.parseObject(jsonObject.getJSONArray("actualList").toString());
//        mkd.setTargetParam(actualListObject.getString("label"));
//        mkd.setActualValue(actualListObject.getString("value"));
        JSONObject actualListObject = null;
        for (int i = 0; i < jsonObject.getJSONArray("actualList").size(); i++) {
            actualListObject = JSONObject.parseObject(jsonObject.getJSONArray("actualList").get(i).toString());
            String targetParam = actualListObject.getString("label");
            mkd.setTargetParam(targetParam);
            String actualValue = actualListObject.getString("value");
            mkd.setActualValue(actualValue);
            mesKpiDetailMapper.updateMesKpiDetail(mkd);
        }
//        jsonObject = JSONObject.parseObject(jsonObject.getJSONArray("actualList").toString());
//            mkd.setTargetParam(jsonObject.getString("label"));
//            mkd.setActualValue(jsonObject.getString("value"));


//        mkd.setTargetParam(jsonObject.getJSONObject("label").toString());
//        String actualValue = jsonObject.getJSONObject("value").toString();
//        mkd.setActualValue(jsonObject.getJSONObject("value").toString());
        return AjaxResult.success("修改成功!");
    }

}
