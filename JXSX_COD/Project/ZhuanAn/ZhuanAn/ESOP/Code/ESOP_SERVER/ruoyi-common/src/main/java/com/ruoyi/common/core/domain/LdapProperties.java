package com.ruoyi.common.core.domain;
import com.ruoyi.common.core.domain.BaseEntity;
import lombok.Data;
import org.springframework.boot.context.properties.ConfigurationProperties;
import org.springframework.stereotype.Component;

import java.util.List;
@Component
@ConfigurationProperties(prefix = "ldap")
@Data
public class LdapProperties extends BaseEntity {

    private List<LdapServerConfig> servers;

    public List<LdapServerConfig> getServers() {
        return servers;
    }

    public void setServers(List<LdapServerConfig> servers) {
        this.servers = servers;
    }

    // 静态内部类表示单个LDAP服务器的配置
    @Data
    public static class LdapServerConfig {
        private String ldapUrl;
        private String baseDN;
        private String bindUserName;
        private String bindPassword;

    }
}