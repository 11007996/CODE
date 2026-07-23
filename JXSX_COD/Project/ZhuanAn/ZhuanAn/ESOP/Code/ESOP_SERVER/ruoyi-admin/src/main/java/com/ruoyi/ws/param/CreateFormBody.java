package com.ruoyi.ws.param;

import java.util.Collections;
import java.util.List;

/**
 * @author peng
 */
public class CreateFormBody {


    private List<CreateFormBodyDetails> details = Collections.emptyList();

    public List<CreateFormBodyDetails> getDetails() {
        return details;
    }

    public void setDetails(List<CreateFormBodyDetails> details) {
        this.details = details;
    }
}
