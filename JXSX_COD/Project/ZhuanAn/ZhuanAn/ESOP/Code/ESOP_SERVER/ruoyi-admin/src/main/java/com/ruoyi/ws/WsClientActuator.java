package com.ruoyi.ws;

import com.luxshare.webservice.wsdl.CreateFormResponse;
import com.ruoyi.oawebservice.OaQueryClient;
import com.ruoyi.oawebservice.param.OaQueryParam;
import com.ruoyi.ws.param.CreateFormParam;
import com.ruoyi.ws.util.WsUtils;
import org.springframework.stereotype.Component;

import javax.annotation.Resource;
import java.util.Map;

/**
 * @author peng
 */
@Component
public class WsClientActuator {

    @Resource
    private CreateFormClient createFormClient;

    @Resource
    private OaQueryClient oaQueryClient;

    public Map<String, Object> getOutForCreateForm(CreateFormParam param) {

    CreateFormResponse response = createFormClient.getCreateFormResponse(param);
    return WsUtils.toMapObj(response.getOut());


    }

    public Map<String, Object> getOutForOaQuery(OaQueryParam param) {
        //SapOpenWFResponse response = oaQueryClient.getSapOpenWFResponse(param);
        //return WsUtils.toMapObj(response.getOut());
        return null;
    }

}
