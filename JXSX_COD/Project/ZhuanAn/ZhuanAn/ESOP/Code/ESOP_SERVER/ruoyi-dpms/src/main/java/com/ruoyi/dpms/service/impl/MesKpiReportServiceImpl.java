package com.ruoyi.dpms.service.impl;

import com.alibaba.fastjson2.JSONArray;
import com.ruoyi.dpms.domain.MesKpiObject;
import com.ruoyi.dpms.domain.MesKpiObject1;
import com.ruoyi.dpms.domain.MesKpiReport;
import com.ruoyi.dpms.mapper.MesKpiObject1Mapper;
import com.ruoyi.dpms.mapper.MesKpiObjectMapper;
import com.ruoyi.dpms.mapper.MesKpiReportMapper;
import com.ruoyi.dpms.service.IMesKpiReportService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
public class MesKpiReportServiceImpl implements IMesKpiReportService {
    @Autowired
    private MesKpiReportMapper mesKpiReportMapper;

    @Autowired
    private MesKpiObjectMapper mesKpiObjectMapper;

    @Autowired
    private MesKpiObject1Mapper mesKpiObject1Mapper;


    /**
     * 查询KPI报表
     */
//    @Override
//    public List<MesKpiReport> selectMesKpiReportList(MesKpiReport mesKpiReport) {
//        return mesKpiReportMapper.selectMesKpiReportList(mesKpiReport);
//    }

    @Override
    public List<MesKpiReport> selectMesKpiReportList(MesKpiReport mesKpiReport) {
        List<MesKpiReport> list = mesKpiReportMapper.selectMesKpiReportList(mesKpiReport);
        //1、循环遍历最外面的一层list
        for (int i = 0; i < list.size(); i++) {
            MesKpiReport mesKpiReport1 = list.get(i);
            long id =  mesKpiReport1.getId();
            //1.1、循环遍历人员群组list（appraisedPersonGroup和statisticalPersonGroup）
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
                }
                else if(mko.getGroupType() == 1){
//                    if (j == 0) {
//                        s2 = mko.toJSON1();
//                    } else {
//                        s2 = s2 + "," + mko.toJSON1();
//                    }
                    s2 = s2 + mko.toJSON();
                }
            }
            s1 = "["+s1+"]";
            s2 = "["+s2+"]";
            //1.2、循环遍历目标值、实际值list（targetList）
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
                s4 = s4 + mko1.toJSON();
            }
            s3 = "["+s3+"]";
            s4 = "["+s4+"]";
            list.set(i,mesKpiReport1).setAppraisedPersonGroup(JSONArray.parseArray(s1));
            list.set(i,mesKpiReport1).setStatisticalPersonGroup(JSONArray.parseArray(s2));
            list.set(i,mesKpiReport1).setTargetList(JSONArray.parseArray(s3));
            list.set(i,mesKpiReport1).setActualList(JSONArray.parseArray(s4));
        }
        return list;
    }
}
