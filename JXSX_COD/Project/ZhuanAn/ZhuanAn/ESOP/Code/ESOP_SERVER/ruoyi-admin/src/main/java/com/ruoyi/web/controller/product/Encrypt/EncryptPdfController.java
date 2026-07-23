package com.ruoyi.web.controller.product.Encrypt;

import com.ruoyi.common.config.RuoYiConfig;
//import com.ruoyi.common.constant.HttpStatus;
//import com.ruoyi.common.core.domain.AjaxResult;
import com.ruoyi.common.utils.StringUtils;
//import com.ruoyi.common.utils.sign.Md5Utils;
import com.ruoyi.common.utils.sign.RsaUtils;
import org.apache.pdfbox.pdmodel.PDDocument;
import org.apache.pdfbox.pdmodel.encryption.AccessPermission;
import org.apache.pdfbox.pdmodel.encryption.StandardProtectionPolicy;

import java.io.File;
//import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;
import java.util.Calendar;
import java.util.HashMap;
import java.util.Map;


public class EncryptPdfController {
//    private static final String ftpServer = "10.32.13.205";
//    private static final int ftpPort = 21;
//    private static final String ftpUser = "iMesLabelUser123";
//    private static final String ftpPsw = "Luxshare@123.com";

    private static final String publicKey  ="MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAyqXpFEBJ6GWtqk+9HOJq" +
            "Cop+ynQAfeyZ26jrUd07TBbXPQDZ7oOnTjpoxSUW4C+IkLJl9wZgBjpkzl25aSk8" +
            "mxEmb5oTRPffvlZaME9nVcDFPQkwOOB7WYmGfNZcAPacIaY6qUtMLXgxDkEXGmMu" +
            "Ujvc3LGd/FQWBbrKD9mPdRDG9Kdt6znYgLbZGrtl1SLxodRobpdBR8/OWCE4Zm35" +
            "IT+UlCEg8Au+Okrdsiy9xjf3V3PlTrNbJUbx79QhEV4aku/OGN6poG9hvRosDA9l" +
            "j+twtKwTgnpqC4TABwKfv6V9EcToxmiZwVsBubbgKcoa0cJtNWrspf047Jocd2cM" +
            "MQIDAQAB";

        public static Map<String,Object> encryptPdf (String fileName, byte[] fileContent) {
            Map<String,Object> map = new HashMap<>();
            try{
                //获取时间创建文件夹
                Calendar calendar = Calendar.getInstance();
                int year = calendar.get(Calendar.YEAR);
                // StringUtils.leftPad() 左侧补齐 第一个参数：原始字符串，第二个参数：字符串的长度，第三个是补充的字符串
                String month = StringUtils.leftPad(String.valueOf(calendar.get(Calendar.MONTH) + 1), 2, '0');
                String day = StringUtils.leftPad(String.valueOf(calendar.get(Calendar.DAY_OF_MONTH)), 2, '0');

                //获取随机数
                Integer random = (int) (Math.random()*10000);
                Integer time =  (int)(System.currentTimeMillis() / 1000 % 1000);
//                String randompath =random.toString()+time.toString();
                String randompath =random+time.toString();
                //服务器真实地址
                Path tempDir = Paths.get(RuoYiConfig.getProfile(),"/upload","/"+year,"/"+month,"/"+day,"/"+randompath);

                //pdf明文密码
                Integer random1 = (int) (Math.random()*10000000);
                //文件在浏览器获取的地址（不包含文件）
                String browserUrlExfilename = "/profile/upload/"+year+"/"+month+"/"+day+"/"+randompath;
                //文件在浏览器获取的地址(加密后的)
                String browserUrl = browserUrlExfilename+"/"+fileName;

                if (!Files.exists(tempDir)) {
                    Files.createDirectories(tempDir);
                }

                // 将文件内容写入临时文件
                Path targetPath = tempDir.resolve(fileName);
                Files.write(targetPath, fileContent);

//                System.out.println("文件已上传到临时文件夹：" + targetPath.toString());
                System.out.println("文件已上传到临时文件夹：" + targetPath);

                // step 1. Loading the pdf file
                File f = new File(tempDir.toFile(),fileName);
                //load() 方法将接受 PDF 文件作为参数。
                PDDocument pdd = PDDocument.load(f);
                // step 2.Creating instance of AccessPermission
                // class
                AccessPermission ap = new AccessPermission();
                // step 3. Creating instance of
                // StandardProtectionPolicy
                StandardProtectionPolicy stpp = new StandardProtectionPolicy(random1.toString(), random1.toString(), ap);
                // step 4. Setting the length of Encryption key
                stpp.setEncryptionKeyLength(128);
                // step 5. Setting the permission
                stpp.setPermissions(ap);
                // step 6. Protecting the PDF file
                pdd.protect(stpp);
                // step 7. Saving and closing the the PDF Document
                // TODO: 将加密后的内容写入新文件
                pdd.save(f);
                pdd.close();

                String filePath = targetPath.toString();

                RsaUtils rsaUtils =new RsaUtils();

                map.put("code",200);
                map.put("msg","OK");
                map.put("browserUrl",browserUrl);
                map.put("filePath",filePath);
                //RSA加密PDF密码


                map.put("pdfPassWord",rsaUtils.encrypt(random1.toString(),publicKey));
            }catch (Exception ex){
                map.put("code",500);
                map.put("msg","PDF文件上传失败"+ex.getMessage());
            }
            return map;
        }

}
