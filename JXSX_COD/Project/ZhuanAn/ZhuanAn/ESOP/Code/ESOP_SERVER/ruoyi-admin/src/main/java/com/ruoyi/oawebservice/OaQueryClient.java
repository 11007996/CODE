package com.ruoyi.oawebservice;

import org.springframework.beans.factory.annotation.Value;
import org.springframework.ws.client.core.support.WebServiceGatewaySupport;

public class OaQueryClient extends WebServiceGatewaySupport {

    @Value("${spring.profiles.active}")
    private String env;

//    public SapOpenWFResponse getSapOpenWFResponse(OaQueryParam param){
//        Assert.notNull(param, "OaQueryParam can't be null");
//
//        SapOpenWF request = new SapOpenWF();
//        request.setIn0(WsUtils.toJSONString(param));
//
//        String URI = "";
//        if("prd".equals(env)){
//            URI = WsURI.CREATE_WORKFLOW_SERVICE;
//        }else {
//            URI = WsURI.CREATE_WORKFLOW_SERVICE_DEV;
//        }
//
//        return (SapOpenWFResponse)getWebServiceTemplate()
//                .marshalSendAndReceive(URI, request, null);
//    }
}
