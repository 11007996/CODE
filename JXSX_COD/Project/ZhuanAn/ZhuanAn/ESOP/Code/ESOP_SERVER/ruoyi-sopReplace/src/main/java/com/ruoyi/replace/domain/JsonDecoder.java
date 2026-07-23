package com.ruoyi.replace.domain;


import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.databind.ObjectMapper;

import javax.websocket.DecodeException;
import javax.websocket.Decoder;
import javax.websocket.EndpointConfig;

public class JsonDecoder  implements Decoder.Text<JsonObj> {

    @Override//Initialization does nothing
    public void init(EndpointConfig config) {}

    @Override//Judgment of whether decoding is possible
    public boolean willDecode(String text) {
        return (text != null);
    }

    @Override//Decoding process(JSON → object)
    public JsonObj decode(String text) throws DecodeException {
        ObjectMapper mapper = new ObjectMapper();
        JsonObj obj = null;
        try {
            obj = mapper.readValue(text, JsonObj.class);
        } catch (JsonProcessingException e) {
            e.printStackTrace();
        }
        return obj;
    }

    @Override//Do nothing to destroy
    public void destroy() {}
}