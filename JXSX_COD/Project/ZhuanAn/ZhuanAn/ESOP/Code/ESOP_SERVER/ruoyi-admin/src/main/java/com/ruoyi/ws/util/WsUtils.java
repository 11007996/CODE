package com.ruoyi.ws.util;

import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.core.type.TypeReference;
import com.fasterxml.jackson.databind.ObjectMapper;

import java.math.BigDecimal;
import java.math.BigInteger;
import java.util.List;
import java.util.Map;

/**
 * @author peng
 */
public class WsUtils {

    private static final ObjectMapper OBJECT_MAPPER = new ObjectMapper();

    private static final TypeReference<Map<String, Object>> TYPE_MAP_OBJ = new TypeReference<Map<String, Object>>() {
    };

    public static String toJSONString(Object value) {
        try {
            return OBJECT_MAPPER.writeValueAsString(value);
        } catch (JsonProcessingException e) {
            throw new RuntimeException(e);
        }
    }

    public static Map<String, Object> toMapObj(String value) {
        try {
            return OBJECT_MAPPER.readValue(value, TYPE_MAP_OBJ);
        } catch (JsonProcessingException e) {
            throw new RuntimeException(e);
        }
    }

    public static String symbolSeparated(List<String> list, String symbol) {
        StringBuilder sb = new StringBuilder();

        list.forEach(item -> sb.append(item).append(symbol));

        return sb.substring(0, sb.length() - 1);
    }

    public static String commaSeparated(List<String> list) {
        if(list.size()>0){
            return symbolSeparated(list, ",");
        }
        else{
            return "";
        }
    }

    public static BigDecimal toBigDecimal(Object value) {
        BigDecimal ret = null;
        if (value != null) {
            if (value instanceof BigDecimal) {
                ret = (BigDecimal) value;
            } else if (value instanceof String) {
                ret = new BigDecimal((String) value);
            } else if (value instanceof BigInteger) {
                ret = new BigDecimal((BigInteger) value);
            } else if (value instanceof Number) {
                ret = BigDecimal.valueOf(((Number) value).doubleValue());
            } else {
                throw new ClassCastException("Not possible to coerce [" + value + "] from class " + value.getClass() + " into a BigDecimal.");
            }
        }
        return ret;
    }

    private WsUtils() {
    }

}
