package com.ruoyi.ws;

import com.ruoyi.oawebservice.OaQueryClient;
import com.ruoyi.ws.enums.WsURI;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.oxm.jaxb.Jaxb2Marshaller;

/**
 * @author peng
 */
@Configuration
public class WorkflowConfiguration {

    @Value("spring.profiles.active")
    private String env;

    @Bean
    public Jaxb2Marshaller marshaller() {
        Jaxb2Marshaller marshaller = new Jaxb2Marshaller();
        marshaller.setContextPath("com.luxshare.webservice.wsdl");
        return marshaller;
    }

    @Bean
    public CreateFormClient createFormClient(Jaxb2Marshaller marshaller) {
        CreateFormClient client = new CreateFormClient();
        String URI = "";
        if("prd".equals(env)){
            URI = WsURI.COMMON_WORKFLOW_SERVICE;
        }else {
            URI = WsURI.COMMON_WORKFLOW_SERVICE_DEV;
        }
        client.setDefaultUri(URI);
        client.setMarshaller(marshaller);
        client.setUnmarshaller(marshaller);
        return client;
    }

    @Bean
    public OaQueryClient oaQueryClient(Jaxb2Marshaller marshaller) {
        OaQueryClient client = new OaQueryClient();
        String URI = "";
        if("prd".equals(env)){
            URI = WsURI.CREATE_WORKFLOW_SERVICE;
        }else {
            URI = WsURI.CREATE_WORKFLOW_SERVICE_DEV;
        }
        client.setDefaultUri(URI);
        client.setMarshaller(marshaller);
        client.setUnmarshaller(marshaller);
        return client;
    }

}
