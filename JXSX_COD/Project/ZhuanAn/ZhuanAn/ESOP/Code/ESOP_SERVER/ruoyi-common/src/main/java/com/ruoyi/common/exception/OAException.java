package com.ruoyi.common.exception;

public class OAException extends RuntimeException{
    public OAException(String message) {
        super(message);
    }

    @Override
    public synchronized Throwable fillInStackTrace() {
        return this;
    }
}
