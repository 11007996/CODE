package com.ruoyi.replace.mapper;

import com.ruoyi.replace.domain.SopInfo;
import org.apache.ibatis.annotations.Mapper;
import org.apache.ibatis.annotations.Param;
import org.springframework.stereotype.Component;

import java.util.List;

@Mapper
@Component
public interface SopInfoMapper{
    /**
     * 查询processId列表
     *
     * @param
     * @return processId集合
     */
//    public List<Long> selectProcessIdByIds(@Param("modelId") Long modelId, @Param("lineId") Long lineId, @Param("stageId") Long stageId);
    public List<Long> selectProcessIdByIds(@Param("modelId") Long modelId, @Param("lineId") Long lineId, @Param("stageId") Long stageId);

//    public List<String> selectFilePathBySopId (@Param("sopId") Long sopId);
    public String selectFilePathBySopId (@Param("sopId") Long sopId);

    public List<SopInfo> selectSopInfoByIds (@Param("lineId") Long lineId, @Param("stageId") Long stageId,@Param("processId") Long processId,@Param("terminalId") Long terminalId);


}
