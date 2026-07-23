package com.ruoyi.web.controller.product;

import com.ruoyi.common.annotation.Log;
import com.ruoyi.common.constant.UserConstants;
import com.ruoyi.common.core.controller.BaseController;
import com.ruoyi.common.core.domain.AjaxResult;
import com.ruoyi.common.core.domain.entity.SysUser;
import com.ruoyi.common.core.page.TableDataInfo;
import com.ruoyi.common.enums.BusinessType;
import com.ruoyi.common.utils.DateUtils;
import com.ruoyi.common.utils.poi.ExcelUtil;
import com.ruoyi.product.domain.MacExcel;
import com.ruoyi.product.domain.MesMac;
import com.ruoyi.product.domain.MesTerminalSop;
import com.ruoyi.product.service.IMesMacService;
import com.ruoyi.product.service.IMesSopGroupService;
import com.ruoyi.product.service.IMesTerminalSopService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.multipart.MultipartFile;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.net.InetAddress;
import java.net.NetworkInterface;
import java.util.List;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

@RestController
@RequestMapping("/product/mac")
public class MesMacController extends BaseController {

    @Autowired
    private IMesMacService mesMacService;
    @Autowired
    private IMesTerminalSopService mesTerminalSopService;
    @Autowired
    private IMesSopGroupService mesSopGroupService;
    public static final String MAC_ADDRESS_PREFIX01 = "MAC Address = ";
    public static final String MAC_ADDRESS_PREFIX02 = "MAC 地址 = ";
    public static final String LOOPBACK_ADDRESS = "127.0.0.1";
    public static final String IPv6Address = "0:0:0:0:0:0:0:1";

    @GetMapping("/getMac")
    public String getMac(HttpServletRequest request) throws Exception {
        try{
            String ip = extractClientIp(request);
            InetAddress ia = InetAddress.getLocalHost();
            String mac1 = getMacAddrByIp(ip);
            String mac2 = getMACAddress(ip);
            System.out.println(ip);
            System.out.println(mac1);
            System.out.println(mac2);
            return mac1+"&&"+mac2;
        }catch (Exception ex){
            throw new Exception("请检查是否和服务器在同一MES局域网！");
//            return ex.getMessage();
        }
    }

    public String extractClientIp(HttpServletRequest request) {
        String ip = null;
        //X-Forwarded-For：Squid 服务代理
        String ipAddresses = request.getHeader("X-Forwarded-For");
        if (ipAddresses == null || ipAddresses.length() == 0 || "unknown".equalsIgnoreCase(ipAddresses)) {
            //Proxy-Client-IP：apache 服务代理
            ipAddresses = request.getHeader("Proxy-Client-IP");
        }
        if (ipAddresses == null || ipAddresses.length() == 0 || "unknown".equalsIgnoreCase(ipAddresses)) {
            //WL-Proxy-Client-IP：weblogic 服务代理
            ipAddresses = request.getHeader("WL-Proxy-Client-IP");
        }
        if (ipAddresses == null || ipAddresses.length() == 0 || "unknown".equalsIgnoreCase(ipAddresses)) {
            //HTTP_CLIENT_IP：有些代理服务器
            ipAddresses = request.getHeader("HTTP_CLIENT_IP");
        }
        if (ipAddresses == null || ipAddresses.length() == 0 || "unknown".equalsIgnoreCase(ipAddresses)) {
            //X-Real-IP：nginx服务代理
            ipAddresses = request.getHeader("X-Real-IP");
        }
        //有些网络通过多层代理，那么获取到的ip就会有多个，一般都是通过逗号（,）分割开来，并且第一个ip为客户端的真实IP
        if (ipAddresses != null && ipAddresses.length() != 0) {
            ip = ipAddresses.split(",")[0];
        }
        //还是不能获取到，最后再通过request.getRemoteAddr();获取
        if (ip == null || ip.length() == 0 || "unknown".equalsIgnoreCase(ipAddresses)) {
            ip = request.getRemoteAddr();
        }
        return ip;
    }

    static String getMacAddrByIp(String ip) {
        String macAddr = null;
        try {
            Process process = Runtime.getRuntime().exec("nbtstat -a " +  ip);
            BufferedReader br = new BufferedReader(
                    new InputStreamReader(process.getInputStream()));
            Pattern pattern = Pattern.compile("([A-F0-9]{2}-){5}[A-F0-9]{2}");
            Matcher matcher;
            for (String strLine = br.readLine(); strLine != null;
                 strLine = br.readLine()) {
                matcher = pattern.matcher(strLine);
                if (matcher.find()) {
                    macAddr = matcher.group();
                    break;
                }
            }
        } catch (IOException e) {
            e.printStackTrace();
        }
        System.out.println(macAddr);
        return macAddr;
    }

    /**
     * 通过IP地址获取MAC地址
     *
     * @param ip String,127.0.0.1格式
     * @return mac String
     * @throws Exception
     */
    public String getMACAddress(String ip) throws Exception {
        String line = "";
        String macAddress = "";
        //如果为127.0.0.1,则获取本地MAC地址。
        if (LOOPBACK_ADDRESS.equals(ip)) {
            InetAddress inetAddress = InetAddress.getLocalHost();
            byte[] mac = NetworkInterface.getByInetAddress(inetAddress).getHardwareAddress();
            //下面代码是把mac地址拼装成String
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < mac.length; i++) {
                if (i != 0) {
                    sb.append("-");
                }
                String s = Integer.toHexString(mac[i] & 0xFF);
                sb.append(s.length() == 1 ? 0 + s : s);
            }
            //把字符串所有小写字母改为大写成为正规的mac地址并返回
            macAddress = sb.toString().trim().toUpperCase();
            return macAddress;
        }
        //获取非本地IP的MAC地址
        Process p = Runtime.getRuntime().exec("nbtstat -A " + ip);
        InputStreamReader isr = new InputStreamReader(p.getInputStream(), "GBK");
        BufferedReader br = new BufferedReader(isr);
        while ((line = br.readLine()) != null) {
            if (line != null) {
                //英文环境下，命令行执行nbtstat -A [ip] 返回结果包含"MAC Address ="
                if (line.contains(MAC_ADDRESS_PREFIX01)) {
                    macAddress = fromLineToGetMacAddress(line, MAC_ADDRESS_PREFIX01);
                }
                //中文环境下，命令行执行nbtstat -A [ip] 返回结果包含"MAC 地址 ="
                if (line.contains(MAC_ADDRESS_PREFIX02)) {
                    macAddress = fromLineToGetMacAddress(line, MAC_ADDRESS_PREFIX02);
                }
            }
        }
        br.close();
        return macAddress;
    }

    public String fromLineToGetMacAddress(String line, String MAC_ADDRESS_PREFIX) {
        String macAddress = "";
        int index = line.indexOf(MAC_ADDRESS_PREFIX);
        if (index != -1) {
            macAddress = line.substring(index + MAC_ADDRESS_PREFIX.length()).trim().toUpperCase();
        }
        return macAddress;
    }

    /**
     * 查询mac信息列表
     */
    //@PreAuthorize("@ss.hasPermi('product:mac:list')")
    @GetMapping("/list")
    public TableDataInfo list(MesMac mesMac)
    {
        startPage();
        List<MesMac> list = mesMacService.selectMesMacList(mesMac);
        return getDataTable(list);
    }
    /**
     * 免密登录——根据mac带line
     */
    @GetMapping("/selectLineByMac")
    public TableDataInfo selectLineByMac(MesMac mesMac)
    {
        startPage();
        List<MesMac> list = mesMacService.selectLineByMac(mesMac);
        return getDataTable(list);
    }
    /**
     * 免密登录——根据mac+line带stage
     */
    @GetMapping("/selectStageByMac")
    public TableDataInfo selectStageByMac(MesMac mesMac)
    {
        startPage();
        List<MesMac> list = mesMacService.selectStageByMac(mesMac);
        return getDataTable(list);
    }
    /**
     * 免密登录——根据mac+line+stage带process
     */
    @GetMapping("/selectProcessByMac")
    public TableDataInfo selectProcessByMac(MesMac mesMac)
    {
        startPage();
        List<MesMac> list = mesMacService.selectProcessByMac(mesMac);
        return getDataTable(list);
    }
    /**
     * 查询line+stage+process信息列表
     */
    @PreAuthorize("@ss.hasPermi('product:mac:list')")
    @GetMapping("/processList")
    public TableDataInfo processList(MesMac mesMac)
    {
        startPage();
        List<MesMac> list = mesMacService.selectProcessList(mesMac);
        return getDataTable(list);
    }

    /**
     * 获取mac地址详细信息
     */
    @PreAuthorize("@ss.hasPermi('product:mac:query')")
    @GetMapping(value = "/{macId}")
    public AjaxResult getInfo(@PathVariable("macId") Long macId)
    {
        MesMac mesMac = mesMacService.selectMesMacByMacId(macId);
        return AjaxResult.success(mesMac);
    }

    /**
     * 新增mac地址管理
     * @return
     */
    @Log(title = "mac地址管理", businessType = BusinessType.INSERT)
    @PostMapping
    public AjaxResult add(@RequestBody MesMac mac)
    {
        if (UserConstants.NOT_UNIQUE.equals(mesMacService.checkMacNameUnique(mac)))
        {
            return AjaxResult.error("新增mac地址'" + mac.getMacName() + "'失败，名称已存在");
        }
        mac.setCreateBy(getUsername());
        return toAjax(mesMacService.insertMesMac(mac));
    }

    /**
     * 修改mac地址管理
     * 加个卡控：当前mac一旦被删除，那么mes_terminal_sop表mes_sop_group和里面的与该mac相关的都删掉
     */
    @PreAuthorize("@ss.hasPermi('product:mac:edit')")
    @Log(title = "mac地址管理", businessType = BusinessType.UPDATE)
    @PutMapping
    public AjaxResult edit(@RequestBody MesMac mac)
    {
        if(UserConstants.NOT_UNIQUE.equals(mesMacService.checkMacNameUnique(mac)))
        {
            return AjaxResult.error("修改mac地址'" + mac.getMacName() + "'失败，名称已存在");
        }
        mac.setUpdateBy(getUsername());
        mesMacService.updateMesMac(mac);

        return toAjax(1);
    }

    /**
     * 修改mac地址状态
     */
    @PreAuthorize("@ss.hasPermi('product:mac:edit')")
    @Log(title = "mac地址管理", businessType = BusinessType.UPDATE)
    @PutMapping("/changeStatus")
    public AjaxResult changeStatus(@RequestBody MesMac mac)
    {
        mac.setUpdateBy(getUsername());
        mac.setUpdateTime(DateUtils.getNowDate());
        return toAjax(mesMacService.updateMacStatus(mac));
    }

    /**
     * 删除mac地址管理
     */
    @PreAuthorize("@ss.hasPermi('product:mac:remove')")
    @Log(title = "mac地址管理", businessType = BusinessType.DELETE)
    @DeleteMapping("/{macIds}")
    public AjaxResult remove(@PathVariable Long[] macIds)
    {
        mesMacService.deleteMesMacByMacIds(macIds);
        //加个卡控：当前mac一旦被删除，那么mes_terminal_sop表和mes_sop_group表里面的与该mac相关的都删掉
        for (Long macId:macIds) {
            MesTerminalSop a = new MesTerminalSop();
            a.setTerminalId(macId);
            Long b = mesTerminalSopService.selectIdByTerminalSopInfo(a);
            mesSopGroupService.deleteMesSopGroupBySopGroupId(b,"0");
            mesSopGroupService.deleteMesSopGroupBySopGroupId(b,"1");
            mesTerminalSopService.deleteMesTerminalSopById(macId);
        }
        return toAjax(1);
    }


    /**
     * mac地址批量导入
     * @return
     */
    @PostMapping("/importMacExcel")
    @ResponseBody
    public AjaxResult importMacExcel(@RequestParam("file") MultipartFile file,MesMac mesMac){
        try {
            return mesMacService.importMacExcel(file,mesMac);
        } catch (Exception e) {
            e.printStackTrace();
            return AjaxResult.error(500, e.getMessage());
        }
    }


//    /**
//     * mac地址导入模板下载（可用，未用）
//     * @return 字节流
//     */
//    @RequestMapping("/downloadExcel")
//    @ResponseBody
//    public void downloadExcel(HttpServletResponse response,HttpServletRequest request) {
//        mesMacService.downloadExcel(response,request);
//    }

    /**
     * mac地址导入模板下载(在用)
     * @return
     */
    @PostMapping("/downloadTemplate")
    public void downloadTemplate(HttpServletResponse response){
        ExcelUtil<MacExcel> util = new ExcelUtil<MacExcel>(MacExcel.class);
        util.importTemplateExcel(response, "MAC模板");
    }

}

