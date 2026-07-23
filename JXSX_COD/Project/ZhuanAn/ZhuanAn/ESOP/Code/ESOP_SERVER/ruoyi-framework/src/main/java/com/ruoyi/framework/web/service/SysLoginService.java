package com.ruoyi.framework.web.service;

import javax.annotation.Resource;
import javax.naming.*;
import javax.naming.directory.*;
import javax.naming.ldap.Control;
import javax.naming.ldap.InitialLdapContext;
import javax.naming.ldap.LdapContext;
import javax.naming.ldap.SortControl;

import com.ruoyi.common.core.domain.entity.SysRole;
import com.ruoyi.common.core.domain.model.LoginBody;
import com.ruoyi.common.core.domain.model.LoginUser;
import com.ruoyi.common.core.redis.RedisCache;
import com.ruoyi.common.utils.StringUtils;
import com.ruoyi.common.utils.http.HttpUtils;
import com.ruoyi.system.mapper.SysUserMapper;
import com.ruoyi.system.service.ISysDeptService;
import com.ruoyi.system.service.ISysRoleService;
import com.ruoyi.system.web.TokenService;
import org.springframework.beans.factory.annotation.Autowired;
//import org.springframework.boot.autoconfigure.ldap.LdapProperties;
import com.ruoyi.common.core.domain.LdapProperties;
import org.springframework.security.authentication.AuthenticationManager;
import org.springframework.security.authentication.BadCredentialsException;
import org.springframework.security.authentication.UsernamePasswordAuthenticationToken;
import org.springframework.security.core.Authentication;
import org.springframework.stereotype.Component;
import com.ruoyi.common.constant.Constants;
import com.ruoyi.common.exception.CustomException;
import com.ruoyi.common.exception.user.UserPasswordNotMatchException;
import com.ruoyi.common.utils.MessageUtils;
import com.ruoyi.framework.manager.AsyncManager;
import com.ruoyi.framework.manager.factory.AsyncFactory;
import org.springframework.stereotype.Service;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.UnsupportedEncodingException;
import java.net.*;
import java.nio.charset.StandardCharsets;
import java.security.MessageDigest;
import java.security.Security;
import java.util.ArrayList;
import java.util.Hashtable;
import java.util.List;

/**
 * 登录校验方法
 *
 * @author ruoyi
 */
@Component
@Service
public class SysLoginService
{

    @Autowired
    private TokenService tokenService;

    @Resource
    private AuthenticationManager authenticationManager;

    @Autowired
    private RedisCache redisCache;

    @Autowired
    private ISysRoleService sysRoleService;

    @Autowired
    private ISysDeptService sysDeptService;
    @Autowired
    private SysUserMapper userMapper;

    //    @Value("${ldap}")
    private String ldapUrl;

    //    @Value("${ldap.baseDN}")
    private String baseDN;

    //    @Value("${ldap.bindUserName}")
    private String bindUserName;

    //    @Value("${ldap.bindPassWord}")
    private String bindPassWord;

    private final Hashtable<String, String> env = new Hashtable<String, String>();

    private final Control[] sortConnCtls = new SortControl[1];

    private static DirContext ctx = null;

    {
        try {
            sortConnCtls[0] = new SortControl("sAMAccountName", Control.CRITICAL);
        } catch (IOException ex) {
        }
    }

    private final LdapProperties ldapProperties;
    @Autowired
    public SysLoginService(LdapProperties ldapProperties) {
        this.ldapProperties = ldapProperties;
    }

    /**
     * 登录验证
     *
     * @param username 用户名
     * @param password 密码
     * @param code 验证码
     * @param uuid 唯一标识
     * @return 结果
     */
    public String login(String username, String password, String code, String uuid)
    {
        /*String verifyKey = Constants.CAPTCHA_CODE_KEY + uuid;
        String captcha = redisCache.getCacheObject(verifyKey);
        redisCache.deleteObject(verifyKey);
        if (captcha == null)
        {
            AsyncManager.me().execute(AsyncFactory.recordLogininfor(username, Constants.LOGIN_FAIL, MessageUtils.message("user.jcaptcha.expire")));
            throw new CaptchaExpireException();
        }
        if (!code.equalsIgnoreCase(captcha))
        {
            AsyncManager.me().execute(AsyncFactory.recordLogininfor(username, Constants.LOGIN_FAIL, MessageUtils.message("user.jcaptcha.error")));
            throw new CaptchaException();
        }*/
        // 用户验证
        Authentication authentication = null;
        try
        {
            // 该方法会去调用UserDetailsServiceImpl.loadUserByUsername
            authentication = authenticationManager
                    .authenticate(new UsernamePasswordAuthenticationToken(username, password));
        }
        catch (Exception e)
        {
            if (e instanceof BadCredentialsException)
            {
                AsyncManager.me().execute(AsyncFactory.recordLogininfor(username, Constants.LOGIN_FAIL, MessageUtils.message("user.password.not.match")));
                throw new UserPasswordNotMatchException();
            }
            else
            {
                AsyncManager.me().execute(AsyncFactory.recordLogininfor(username, Constants.LOGIN_FAIL, e.getMessage()));
                throw new CustomException(e.getMessage());
            }
        }
        LoginUser loginUser = (LoginUser) authentication.getPrincipal();
        List<SysRole> roles =  loginUser.getUser().getRoles();
        if(roles.size() > 0)
        {
            List<Long> roleIds = new ArrayList<Long>();
            for (SysRole role : roles){
                roleIds.add(role.getRoleId());
            }
//            if(!roleIds.contains(1L)){
//                List<Integer> deptIds = sysRoleService.selectRoleDeptListByRoleId(roleIds);
//                List<Long> deptIds = userMapper.deptIdsByUserId(loginUser.getUser().getUserId());
//                if(deptIds.size() > 0)
//                {
//                    if(!deptIds.contains(Long.valueOf(deptId))){
//                        AsyncManager.me().execute(AsyncFactory.recordLogininfor(username, Constants.LOGIN_FAIL, "用户无该厂区数据权限"));
//                        throw new CustomException("用户无该厂区数据权限");
//                    }
//                }else {
//                    AsyncManager.me().execute(AsyncFactory.recordLogininfor(username, Constants.LOGIN_FAIL, "用户还未赋予厂区数据权限"));
//                    throw new CustomException("用户还未赋予厂区数据权限");
//                }
//            }
        }else {
            AsyncManager.me().execute(AsyncFactory.recordLogininfor(username, Constants.LOGIN_FAIL, "用户还未赋予角色"));
            throw new CustomException("用户还未赋予角色");
        }
        AsyncManager.me().execute(AsyncFactory.recordLogininfor(username, Constants.LOGIN_SUCCESS, MessageUtils.message("user.login.success")));
        //修改用户部门
//        loginUser.getUser().setDeptId((long)deptId);
//        //存储登录厂区名称
//        loginUser.getUser().setLoginFactory(sysDeptService.selectDeptById((long)deptId).getDeptName());
//        //添加用户当前登录语言
//        loginUser.getUser().setLoginLanguage(lang);
//        loginUser.setClient(client);
        // 生成token
        return tokenService.createToken(loginUser);
    }

    public String ldapLogin(LoginBody loginBody){
        // 获取用户名密码
        String username = loginBody.getUsername();
        String password = loginBody.getPassword();
        String encodedPassword;
        try {
            // 编码密码，处理特殊字符如 &
            encodedPassword = URLEncoder.encode(password, "UTF-8");
        } catch (UnsupportedEncodingException e) {
            // UTF-8 是标准编码，几乎不会触发异常，这里兜底处理
            e.printStackTrace();
            // 编码失败时直接用原密码，保证流程不中断
            encodedPassword = password;
        }

        try {
            //江西
            String cheStr = sendGet("http://172.18.20.172:8088/api/Login","empCode=" + username + "&password=" + encodedPassword);
            //越南
            //String cheStr = sendGet("http://10.173.4.51:8066/Login/Get","empCode=" + username + "&password=" + encodedPassword);
            if(cheStr.toUpperCase().equals("TRUE"))
            {
                return "OK";
            }
        } catch (Exception err) {
            err.printStackTrace();
            throw new CustomException("异常信息未知");
        } finally {
            closeConnect();
        }
        return "验证域账户失败";
    }

    public static String sendGet(String url, String param)
    {
        StringBuilder result = new StringBuilder();
        BufferedReader in = null;
        try
        {
            String urlNameString = StringUtils.isNotBlank(param) ? url + "?" + param : url;
            URL realUrl = new URL(urlNameString);
            URLConnection connection = realUrl.openConnection();
            connection.setRequestProperty("accept", "*/*");
            connection.setRequestProperty("connection", "Keep-Alive");
            connection.setRequestProperty("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1;SV1)");
            connection.connect();
            in = new BufferedReader(new InputStreamReader(connection.getInputStream(), StandardCharsets.UTF_8));
            String line;
            while ((line = in.readLine()) != null)
            {
                result.append(line);
            }
        }
        catch (Exception ignored)
        {
        }
        finally
        {
            try
            {
                if (in != null)
                {
                    in.close();
                }
            }
            catch (Exception ignored)
            {
            }
        }
        return result.toString();
    }

    public String ldapLogin_old(LoginBody loginBody){
        // 获取用户名密码
        String username = loginBody.getUsername();
        String password = loginBody.getPassword();

        LdapContext ldapContext = null;
        /**ldap登录，如果有异常说明登录失败*/
        try {
            //登录AD管理员账号
            LDAPConnector();
            //获取登录人组织
            String userDN = getUserDomain(username);
            if (userDN == null) {
                return "请检查账号是否存在";
            }

            Hashtable<String, Object> env = new Hashtable<String, Object>();
            //解决No subject alternative DNS name xxxxx的错误
            Security.setProperty("jdk.tls.disabledAlgorithms", "");
            System.setProperty("com.sun.jndi.ldap.object.disableEndpointIdentification", "true");
            env.put(Context.INITIAL_CONTEXT_FACTORY, "com.sun.jndi.ldap.LdapCtxFactory");
            env.put(Context.SECURITY_AUTHENTICATION, "simple");
            //验证登录人账密
            env.put(Context.SECURITY_PRINCIPAL, userDN);
            env.put(Context.SECURITY_CREDENTIALS, password);
            env.put(Context.PROVIDER_URL, ldapUrl);
            env.put(Context.SECURITY_PROTOCOL, "ssl");
            env.put("java.naming.ldap.factory.socket", "com.ruoyi.common.core.ldap.DummySSLSocketFactory");
            ctx = new InitialLdapContext(env, sortConnCtls);
            return "OK";
        } catch (AuthenticationException e) {
            e.printStackTrace();
            throw new CustomException("账号或密码错误！");
        }
        catch (javax.naming.CommunicationException e) {
//                e.printStackTrace();
//                throw new CustomException("ldap域连接失败");
            throw new CustomException("LDAP server not reachable: " + ldapUrl + ", " + e.getMessage());
        } catch (Exception err) {
            err.printStackTrace();
            throw new CustomException("异常信息未知");
        } finally {
            closeConnect();
        }
    }

    private String getUserDomain(String account) throws NamingException {
        String userDN = null;
        SearchControls contro = new SearchControls();
        contro.setSearchScope(2);

        NamingEnumeration<SearchResult> en = ctx.search(baseDN, "(sAMAccountName=" + account + ")", contro);
        if (en == null || !en.hasMoreElements()) {
            return null;
        }
        while (en.hasMoreElements()) {
            Object obj = en.nextElement();
            if (obj instanceof SearchResult) {
                SearchResult si = (SearchResult) obj;
                Attributes attrs = si.getAttributes();
                userDN = (String) attrs.get("distinguishedName").get();
                break;
            }
        }
        return userDN;
    }

    private void LDAPConnector() {
        for (LdapProperties.LdapServerConfig server : ldapProperties.getServers()) {
            try {
                baseDN = server.getBaseDN();
                ldapUrl = server.getLdapUrl();
                bindUserName = server.getBindUserName();
                bindPassWord = server.getBindPassword();
                Hashtable<String, Object> env = new Hashtable<String, Object>();
                //解决No subject alternative DNS name xxxxx的错误
                Security.setProperty("jdk.tls.disabledAlgorithms", "");
                System.setProperty("com.sun.jndi.ldap.object.disableEndpointIdentification", "true");
                env.put(Context.INITIAL_CONTEXT_FACTORY, "com.sun.jndi.ldap.LdapCtxFactory");
                env.put(Context.SECURITY_AUTHENTICATION, "simple");
                env.put(Context.SECURITY_PRINCIPAL, bindUserName);
                env.put(Context.SECURITY_CREDENTIALS, bindPassWord);
                env.put(Context.PROVIDER_URL, ldapUrl);
                env.put(Context.SECURITY_PROTOCOL, "ssl");
                env.put("java.naming.ldap.factory.socket", "com.ruoyi.common.core.ldap.DummySSLSocketFactory");

                ctx = new InitialLdapContext(env, sortConnCtls);
            } catch (Exception e) {
                // ignore error
                e.printStackTrace();
            }
        }
    }

    public static void closeConnect() {
        try {
            if (ctx != null) {
                ctx.close();
            }
        } catch (NamingException e) {
            e.printStackTrace();
        }
    }

//    public String loginOA(String username, String password, String code, String uuid,int deptId)
//    {
//        // 用户验证
//        Authentication authentication = null;
//        try
//        {
//            // 该方法会去调用UserDetailsServiceImpl.loadUserByUsername
//            UsernamePasswordAuthenticationToken token = new UsernamePasswordAuthenticationToken(username, password);
//            authentication = authenticationManager
//                    .authenticate(token);
//        }
//        catch (Exception e)
//        {
//            if (e instanceof BadCredentialsException)
//            {
//                AsyncManager.me().execute(AsyncFactory.recordLogininfor(username, Constants.LOGIN_FAIL, MessageUtils.message("user.password.not.match")));
//                throw new UserPasswordNotMatchException();
//            }
//            else
//            {
//                AsyncManager.me().execute(AsyncFactory.recordLogininfor(username, Constants.LOGIN_FAIL, e.getMessage()));
//                throw new CustomException(e.getMessage());
//            }
//        }
//        LoginUser loginUser = (LoginUser) authentication.getPrincipal();
//        List<SysRole> roles =  loginUser.getUser().getRoles();
//        if(roles.size() > 0){
//            List<Long> roleIds = new ArrayList<Long>();
//            for (SysRole role : roles){
//                roleIds.add(role.getRoleId());
//            }
//            if(!roleIds.contains(1L)){
//                List<Integer> deptIds = sysRoleService.selectRoleDeptListByRoleId(roleIds);
//                if(deptIds.size() > 0){
//                    if(!deptIds.contains(deptId)){
//                        AsyncManager.me().execute(AsyncFactory.recordLogininfor(username, Constants.LOGIN_FAIL, "用户无该群组数据权限"));
//                        throw new CustomException("用户无该群组数据权限");
//                    }
//                }else {
//                    AsyncManager.me().execute(AsyncFactory.recordLogininfor(username, Constants.LOGIN_FAIL, "用户还未赋予数据权限"));
//                    throw new CustomException("用户还未赋予数据权限");
//                }
//            }
//        }else {
//            AsyncManager.me().execute(AsyncFactory.recordLogininfor(username, Constants.LOGIN_FAIL, "用户还未赋予角色"));
//            throw new CustomException("用户还未赋予角色");
//        }
//        AsyncManager.me().execute(AsyncFactory.recordLogininfor(username, Constants.LOGIN_SUCCESS, MessageUtils.message("user.login.success")));
//        //修改用户部门
//        loginUser.getUser().setDeptId((long)deptId);
//        //存储登录厂区名称
//        loginUser.getUser().setLoginFactory(sysDeptService.selectDeptById((long)deptId).getDeptName());
//        // 生成token
//        return tokenService.createToken(loginUser);
//    }

}
