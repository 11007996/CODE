package com.ruoyi.ws;

import com.luxshare.webservice.wsdl.CreateForm;
import com.luxshare.webservice.wsdl.CreateFormResponse;
import com.ruoyi.system.service.ISysConfigService;
import com.ruoyi.ws.param.CreateFormParam;
import com.ruoyi.ws.util.WsUtils;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.util.Assert;
import org.springframework.ws.client.core.support.WebServiceGatewaySupport;

/**
 * @author peng
 */
public class CreateFormClient extends WebServiceGatewaySupport {

    @Value("${spring.profiles.active}")
    private String env;

    @Autowired
    private ISysConfigService configService;

    public CreateFormResponse getCreateFormResponse(CreateFormParam param) {
        Assert.notNull(param, "CreateFormParam can't be null");

        CreateForm request = new CreateForm();
        request.setIn0(WsUtils.toJSONString(param));

        String URI = configService.selectConfigByKey("product.oa.url");
//        if("prd".equals(env)){
//            URI = WsURI.COMMON_WORKFLOW_SERVICE_PRD;
//        }
//        else if(env.equals("dev")){
//            URI = WsURI.COMMON_WORKFLOW_SERVICE_DEV;
//        }
//        else {
//            URI = WsURI.CREATE_WORKFLOW_SERVICE;
//        }

        return (CreateFormResponse) getWebServiceTemplate()
                .marshalSendAndReceive(URI, request, null);
    }


}
