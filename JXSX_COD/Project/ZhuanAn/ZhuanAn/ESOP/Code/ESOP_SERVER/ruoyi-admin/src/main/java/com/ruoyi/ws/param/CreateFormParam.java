package com.ruoyi.ws.param;

import java.util.Collections;
import java.util.List;

/**
 * @author peng
 */
public class CreateFormParam {

    private CreateFormHeader header = new CreateFormHeader();

    private List<CreateFormBody> body = Collections.emptyList();

    public CreateFormHeader getHeader() {
        return header;
    }

    public void setHeader(CreateFormHeader header) {
        this.header = header;
    }

    public List<CreateFormBody> getBody() {
        return body;
    }

    public void setBody(List<CreateFormBody> body) {
        this.body = body;
    }
}
