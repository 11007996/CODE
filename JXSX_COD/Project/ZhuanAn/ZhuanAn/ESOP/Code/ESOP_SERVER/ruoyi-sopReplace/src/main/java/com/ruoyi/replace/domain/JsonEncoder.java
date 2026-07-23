package com.ruoyi.replace.domain;

import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.databind.ObjectMapper;

import javax.websocket.EncodeException;
import javax.websocket.Encoder;
import javax.websocket.EndpointConfig;

public class JsonEncoder implements Encoder.Text<JsonObj>{

    @Override//Initialization does nothing
    public void init(EndpointConfig config) {}

    @Override//Encoding process(Object → JSON)
    public String encode(JsonObj obj) throws EncodeException {
        ObjectMapper mapper = new ObjectMapper();
        String json = "";
        try {
            json = mapper.writeValueAsString(obj);
        } catch (JsonProcessingException e) {
            e.printStackTrace();
        }
        return json;
    }

    @Override//Do nothing to destroy
    public void destroy() {}
}
