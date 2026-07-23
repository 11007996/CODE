package com.ruoyi.web.controller.system;

import java.util.*;

//import com.ruoyi.common.constant.SysDicKeyConstants;
import com.ruoyi.common.core.domain.AjaxResult;
import com.ruoyi.common.core.domain.SapEmployee;
import com.ruoyi.common.core.domain.entity.SysMenu;
import com.ruoyi.common.core.domain.entity.SysRole;
import com.ruoyi.common.core.domain.entity.SysUser;
import com.ruoyi.common.core.domain.model.LoginBody;
import com.ruoyi.common.core.domain.model.LoginUser;
import com.ruoyi.common.core.redis.RedisCache;
import com.ruoyi.common.utils.SecurityUtils;
import com.ruoyi.framework.web.service.SysLoginService;
import com.ruoyi.framework.web.service.SysPermissionService;
import com.ruoyi.system.web.TokenService;
import com.ruoyi.system.mapper.SysRoleMapper;
import com.ruoyi.system.service.*;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RestController;
import com.ruoyi.common.constant.Constants;
import com.ruoyi.common.utils.ServletUtils;

import javax.servlet.http.Cookie;
import javax.servlet.http.HttpServletRequest;

/**
 * 登录验证
 *
 * @author ruoyi
 */
@RestController
public class SysLoginController {
    @Autowired
    private SysLoginService loginService;

    @Autowired
    private ISysMenuService menuService;

    @Autowired
    private SysPermissionService permissionService;

    @Autowired
    private TokenService tokenService;

    @Autowired
    private ISysDeptService sysDeptService;

    @Autowired
    private ISysUserService sysUserService;

    @Autowired
    private ISysUserService userService;

    @Autowired
    private ISysRoleService roleService;

    @Autowired
    private ISysDeptService deptService;

    @Autowired
    private ISapEmployeeService sapEmployeeService;

//    @Autowired
//    private ISysDictTypeService sysDictTypeService;

    @Autowired
    private RedisCache redisCache;

//    @Value("${license.limitNumber}")
//    private String limitNumber;

    @Value("${spring.profiles.active}")
    private String env;
    @Autowired
    private SysRoleMapper roleMapper;

    /**
     * 登录方法
     *
     * @param loginBody 登录信息
     * @return 结果
     */
    @PostMapping("/login")
    public AjaxResult login(@RequestBody LoginBody loginBody) {
//        if(!getLimitLoginUser()){
//            return AjaxResult.error("登录人员超过限制，请确认后再次登录！");
//        }
        // 明文密码 123456 生成加密串
        AjaxResult ajax = AjaxResult.loginSuccess(loginBody);
        //admin本地登录，其他域登录
        SysUser user = sysUserService.selectUserByUserName(loginBody.getUsername());
        //test3用来免密登录，只要在数据库存在，给他一个最小的权限
        if (user != null && (loginBody.getUsername().equals("admin") || (loginBody.getUsername().equals("test3")))) {
            loginBody.setUsername(loginBody.getUsername().trim());
            if (SecurityUtils.matchesPassword(loginBody.getPassword(), user.getPassword())) {
                loginBody.setPassword(user.getPassword());
            } else {
                return AjaxResult.error("密码错误");
            }
        } else {
            try {
                //先检查域账号
                String status = loginService.ldapLogin(loginBody);
                //String status = loginService.ldapLogin_old(loginBody);
                if (status.equals("OK")) {
                    if (user == null) {
                        user = new SysUser();
                        user.setUserName(loginBody.getUsername());
                        user.setCreateBy("admin");
                        user.setPassword(SecurityUtils.encryptPassword(loginBody.getPassword()));
                        //赋予普通员工角色权限
                        SysRole role = new SysRole();
                        role.setRoleName("普通员工");
                        List<SysRole> list = roleService.selectRoleList(role);
                        if (list != null) {
                            Long roleArray[] = {list.get(0).getRoleId()};
                            user.setRoleIds(roleArray);
                        } else {
                            return AjaxResult.error("无普通员工角色，请先去创建普通员工角色");
                        }
                        //无账号用户进系统默认创建所选厂区的‘普通员工’角色的账号
                        //OA进入
//                        if(StringUtils.isNotNull(loginBody.getDeptId())&&loginBody.getDeptId()!=0){
//                            user.setDeptId((long) loginBody.getDeptId());
//                            user.setAuthorityDeptIds(new long[]{loginBody.getDeptId()});
//                        }
//                        //系统登录页面进入
//                        else {
//                            //赋予厂区
//                            List<SysDept> depts = deptService.selectDeptList(new SysDept());
//                            if (depts != null) {
//                                //赋予集团厂区
//                                for (SysDept dept : depts) {
//                                    if (dept.getParentId() == 0) {
//                                        user.setDeptId(dept.getDeptId());
//                                        user.setAuthorityDeptIds(new long[]{depts.get(0).getDeptId()});
//                                        loginBody.setDeptId(Math.toIntExact(dept.getDeptId()));
//                                        break;
//                                    }
//                                    //没有集团厂区就赋予第一个厂区
//                                    if (user.getUserId() == null) {
//                                        user.setDeptId(depts.get(0).getDeptId());
//                                        user.setAuthorityDeptIds(new long[]{depts.get(0).getDeptId()});
//                                        loginBody.setDeptId(Math.toIntExact(dept.getDeptId()));
//                                    }
//                                }
//                            } else {
//                                return AjaxResult.error("无可用厂区，请先去创建厂区");
//                            }
//                        }
                        //获取姓名
                        SapEmployee sapEmployee = sapEmployeeService.selectSapEmployeeByNo(loginBody.getUsername());
                        if (sapEmployee != null) {
                            user.setNickName(sapEmployee.getEmployeeName());
                            user.setEmail(sapEmployee.getMail());
                        }
                        userService.insertUser(user);
                        loginBody.setUsername(user.getUserName());
                        loginBody.setPassword(user.getPassword());
                    } else {
                        loginBody.setPassword(user.getPassword());
                    }
                } else {
                    return AjaxResult.error(status);
                }
            } catch (Exception e) {
                return AjaxResult.error(e.getMessage());
            }
        }

        // 生成令牌
//        String token = loginService.login(loginBody.getUsername(), loginBody.getPassword(), loginBody.getCode(),
//                loginBody.getUuid(),loginBody.getDeptId());
        String token = loginService.login(loginBody.getUsername(), loginBody.getPassword(), loginBody.getCode(), loginBody.getUuid());
        ajax.put(Constants.TOKEN, token);
//        SysDept sysDept = sysDeptService.selectDeptById(new Long(loginBody.getDeptId()));
//        ajax.put("materialRequiredFlag", sysDept.getMaterialRequiredFlag());
        System.out.println(loginBody.getUsername() + "-token:" + token);
        return ajax;
    }

    /**
     * 验证域账号
     *
     * @param loginBody 登录信息
     * @return 结果
     */
    @PostMapping("/ldapLogin")
    public AjaxResult ldapLogin(@RequestBody LoginBody loginBody) {
        AjaxResult ajax = AjaxResult.success();
        try {
            loginService.ldapLogin(loginBody);
        } catch (Exception e) {
            return AjaxResult.error(e.getMessage());
        }
        return ajax;
    }

    /**
     * 获取用户信息
     *
     * @return 用户信息
     */
    @GetMapping("getInfo")
    public AjaxResult getInfo(Long deptId) {
        LoginUser loginUser = tokenService.getLoginUser(ServletUtils.getRequest());
        SysUser user = loginUser.getUser();
//        if (user.getAuthorityIds() != null){
//            long[] authorityDeptIds = Arrays.stream(user.getAuthorityIds().split(","))
//                    .map(String::trim) // 去除可能的空格
//                    .map(Long::parseLong) // 将字符串转换为Long，这里生成的是Stream<Long>
//                    .distinct() // 去除重复元素
//                    .mapToLong(Long::longValue) // 将字符串转换为 Long
//                    .toArray(); // 转换为 Long 数组
//            user.setAuthorityDeptIds(authorityDeptIds);
//        }
        // 角色集合
        Set<String> roles = permissionService.getRolePermission(user);
        List<Long> userRoleIds = new ArrayList<>();
        List<SysRole> userRoles = new ArrayList<>();
        for (String element : roles) {
            Long roleId = roleMapper.selectRoleIdByName(element).getRoleId();
            userRoleIds.add(roleId);
            userRoles.add(roleMapper.selectRoleById(roleId));
        }
        Long[] userRoleIdsArr = userRoleIds.toArray(new Long[0]);
        user.setRoleIds(userRoleIdsArr);
        user.setRoles(userRoles);
        // 权限集合
        Set<String> permissions = permissionService.getMenuPermission(user);
        AjaxResult ajax = AjaxResult.success();
        ajax.put("user", user);
        ajax.put("roles", roles);
        ajax.put("permissions", permissions);
        return ajax;
    }

    /**
     * 获取路由信息
     *
     * @return 路由信息
     */
    @GetMapping("getRouters")
    public AjaxResult getRouters() {
        LoginUser loginUser = tokenService.getLoginUser(ServletUtils.getRequest());
        // 用户信息
        SysUser user = loginUser.getUser();
        String lang = getLanguageFromCookie(ServletUtils.getRequest());
        List<SysMenu> menus = menuService.selectMenuTreeByUserId(user.getUserId());
        return AjaxResult.success(menuService.buildMenus(menus));
    }

    /**
     * 从 Cookie 中获取语言标识
     *
     * @param request HttpServletRequest 对象
     * @return 语言标识（如 "en"、"zh"），默认为 "zh"
     */
    private String getLanguageFromCookie(HttpServletRequest request) {
        Cookie[] cookies = request.getCookies();
        if (cookies != null) {
            for (Cookie cookie : cookies) {
                if ("lang".equals(cookie.getName())) {
                    return cookie.getValue(); // 返回语言标识
                }
            }
        }
        return "zh"; // 默认返回中文
    }

    /**
     * 验证token
     *
     * @return
     */
    @GetMapping("checkToken")
    public AjaxResult checkToken() {
        LoginUser loginUser = tokenService.getLoginUser(ServletUtils.getRequest());
        // 用户信息
        SysUser user = loginUser.getUser();
        if (user == null) {
            return AjaxResult.error("token验证失败,查询不到用户信息");
        }
        return AjaxResult.success(user);
    }

    /**
     * 获取厂区信息
     *
     * @return 厂区信息
     */
//    @GetMapping("getDeptList")
//    public AjaxResult getDeptList()
//    {
//        SysDept sysDept = new SysDept();
//        sysDept.setStatus("0");
//        List<SysDept> deptList = sysDeptService.selectDeptList(sysDept);
//        return AjaxResult.success(sysDeptService.buildDeptSelect(deptList));
//    }

//    @GetMapping("getLimitedPerson")
//    public boolean getLimitLoginUser(){
////      从redis缓存中拿出当前登录用户
//        Collection<String> keys = redisCache.keys(Constants.LOGIN_TOKEN_KEY + "*");
//        List<LoginUser> loginUsers = new ArrayList<>();
//        for (String key : keys) {
//            LoginUser user = redisCache.getCacheObject(key);
//            loginUsers.add(user);
//        }
//        //返回一个不重复的List
//        loginUsers.removeAll(Collections.singleton(null));
////        当限制人数有值时才判断人数，没值，默认不限制人数
//        if(limitNumber!=null&&!limitNumber.equals("")){
//            //当前登录人员大于等于如果最大登录人数，返回false，抛出异常
//            if(Integer.valueOf(limitNumber)<=loginUsers.size()){
//                return false;
//            }
//        }
//        return true;
//    }

    /**
     * 登录方法
     *
     * @param loginBody 登录信息
     * @return 结果
     */
//    @PostMapping("/loginOA")
//    public AjaxResult loginOA(@RequestBody LoginBody loginBody)
//    {
//        AjaxResult ajax = AjaxResult.success();
//        String status = null;
//        try{
//            //先检查域账号
//            status = "OK";
//            if(status.equals("OK")){
//                SysUser user =sysUserService.selectUserByUserName(loginBody.getUsername());
//                if(user == null){
//                    user = new SysUser();
//                    user.setUserName(loginBody.getUsername());
//                    user.setCreateBy("admin");
//                    user.setPassword(SecurityUtils.encryptPassword("123456"));
//                    //赋予普通员工角色权限
//                    SysRole role = new SysRole();
//                    role.setRoleName("普通员工");
//                    List<SysRole> list = roleService.selectRoleList(role);
//                    if(list != null){
//                        Long roleArray[] ={list.get(0).getRoleId()};
//                        user.setRoleIds(roleArray);
//                    }else{
//                        return AjaxResult.error("无普通员工角色，请先去创建普通员工角色");
//                    }
//                    //无账号用户进系统默认创建所选厂区的‘普通员工’角色的账号
//                    //OA进入
//                    if(StringUtils.isNotNull(loginBody.getDeptId())&&loginBody.getDeptId()!=0){
//                        user.setDeptId((long) loginBody.getDeptId());
//                        user.setAuthorityDeptIds(new long[]{loginBody.getDeptId()});
//                    }
//                    //系统登录页面进入
//                    else {
//                        //赋予厂区
//                        List<SysDept> depts = deptService.selectDeptByName("资产信息共享");
//                        if (depts != null) {
//                            //赋予集团厂区
//                            for (SysDept dept : depts) {
//                                if (dept.getParentId()!=null&&dept.getParentId() == 0) {
//                                    user.setDeptId(dept.getDeptId());
//                                    user.setAuthorityDeptIds(new long[]{depts.get(0).getDeptId()});
//                                    loginBody.setDeptId(Math.toIntExact(dept.getDeptId()));
//                                    loginBody.setClient(dept.getClient());
//                                    break;
//                                }
//                                //没有集团厂区就赋予第一个厂区
//                                if (user.getUserId() == null) {
//                                    user.setDeptId(depts.get(0).getDeptId());
//                                    user.setAuthorityDeptIds(new long[]{depts.get(0).getDeptId()});
//                                    loginBody.setDeptId(Math.toIntExact(dept.getDeptId()));
//                                    loginBody.setClient(dept.getClient());
//                                }
//                            }
//                        } else {
//                            return AjaxResult.error("无可用厂区，请先去创建厂区");
//                        }
//                    }
//                    //获取姓名
//                    SapEmployee sapEmployee = sapEmployeeService.selectSapEmployeeByNo(loginBody.getUsername());
//                    if(sapEmployee != null){
//                        user.setNickName(sapEmployee.getEmployeeName());
//                        user.setEmail(sapEmployee.getMail());
//                    }
//                    userService.insertUser(user);
//                    loginBody.setUsername(user.getUserName());
//                    loginBody.setPassword(user.getPassword());
//                    loginBody.setDeptId(user.getDeptId().intValue());
//                } else{
//                    loginBody.setDeptId(user.getDeptId().intValue());
//                    loginBody.setPassword(user.getPassword());
//                }
//            }else {
//                return AjaxResult.error(status);
//            }
//        }
//        catch (Exception e){
//            return AjaxResult.error(e.getMessage());
//        }
//
//        String url=WsURI.RUOYI_SERVICE_PRD;
//        // 生成令牌
//        String token = loginService.login(loginBody.getUsername(), loginBody.getPassword(), loginBody.getCode(),
//                loginBody.getUuid(),loginBody.getDeptId());
//
//        if(loginBody.getLang()==null||loginBody.getLang().equals("")){
//            loginBody.setLang("zh");
//        }
//        String token = loginService.login(loginBody.getUsername(), loginBody.getPassword(), loginBody.getCode(), loginBody.getUuid());
//        if("prd".equals(env)){
//            url = WsURI.RUOYI_SERVICE_PRD;
//        }else if(env.equals("dev")){
//            url = WsURI.RUOYI_SERVICE_DEV;
//        }
//        url=url+"?deptId="+loginBody.getDeptId()+"&token="+token;
//        ajax.put("url", url);
//        return ajax;
//    }
}
