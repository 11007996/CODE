package com.ruoyi.web.controller.product.Decipher;


import com.ruoyi.common.core.controller.BaseController;
import com.ruoyi.product.service.IMesCutVideoService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import javax.servlet.ServletOutputStream;
import javax.servlet.http.HttpServletResponse;
import java.io.*;
import java.util.List;

@RestController
@RequestMapping("/product")
public class DecipherFile extends BaseController {
//    private static final String ftpServer = "10.32.13.205";
//    private static final int ftpPort = 21;
//    private static final String ftpUser = "iMesLabelUser123";
//    private static final String ftpPsw = "Luxshare@123.com";

//    private static  String results;
    @Autowired
    private IMesCutVideoService cutVideoService;
//    MesCutVideoController mesCutVideoController;



    @GetMapping("/decipherFile")
    public void decryptFile(String urlPath, HttpServletResponse response) throws IOException {
//        FTPClient ftpClient = new FTPClient();
        try{
            String fileType = urlPath.substring(urlPath.length()-3);
            fileType = fileType.toLowerCase();
            //设置response的格式，告诉浏览器现在传的是什么文件，用相应的播放器或解码器打开
            if(fileType.equals("pdf")){
                response.setContentType("application/pdf");
            }
            else if(fileType.equals("mp4")){
//                response.setContentType("video/mp4");
                response.setContentType("application/mp4");
            }
            else{
                response.setContentType("video/mp2t");
            }
//            ftpClient.connect(ftpServer,ftpPort);//FTP的IP和账密
//            ftpClient.login(ftpUser, ftpPsw);

            // 设置传输模式为二进制
//            ftpClient.setFileType(FTP.BINARY_FILE_TYPE);

            // 获取远程文件流
//            urlPath = new String(urlPath.getBytes("GBK"),"ISO-8859-1");
//            ftpClient.enterLocalPassiveMode();
//            InputStream inputStream = ftpClient.retrieveFileStream(urlPath);
            if(fileType.equals("pdf")) {
                File file = new File(urlPath);
                InputStream inputStream = new FileInputStream(file);//之前是在FTP获取到文件，现在改成直接获取临时文件夹里面的文件
                // 输出文件流
//            byte[] buffer = new byte[1024];
//            int bytesRead;
//            System.out.println(inputStream.read(buffer));
//            byte[] bytes = IOUtils.toByteArray(inputStream);
                byte[] bytes = new byte[1024];
//            System.out.println(Arrays.toString(bytes));
//            String qqq = bytes.toString();
//            System.out.println(qqq);
//            String result = new String(bytes);
//            System.out.println("ftp上读取到的数据的字符串是："+result);
//            results = result;
//            String a = new String(bytes,"ISO-8859-1");

                //字节输出流
//            FileOutputStream fos = null;
//            a.substring(0,a.length()-2);
//            File outputFile = new File(tempDir.toFile(), fileName);
//            FileOutputStream outputStream = new FileOutputStream(outputFile);
//            outputStream.write(ssc.getBytes("ISO-8859-1"));
//            outputStream.close();
//            File f = new File("D:\\FTPFiles\\加工内部风扇.pdf");
//            try {
//                fos = new FileOutputStream(f);
////                String str = a.replace("巜",",");
////                String ssc = str.replace("丨",".");
//                String ssc = a.replace("Ξ",".");
//                fos.write(ssc.getBytes("ISO-8859-1"));
//            } catch (Exception e) {
//                e.printStackTrace();
//            } finally {
//                try {
//                    if (fos != null) {
//                        fos.close();
//                    }
//                } catch (IOException e) {
//                    e.printStackTrace();
//                }
//            }

//            ftpClient.logout();
//            ftpClient.disconnect();
//            ftpClient.completePendingCommand();
                //输出
//            System.out.println(bytes);
//            byte[] aa = null;
                ServletOutputStream out = null;
                ByteArrayOutputStream baos = null;
                int len;
                baos = new ByteArrayOutputStream();
                while ((len = inputStream.read(bytes)) != -1) {
                    baos.write(bytes, 0, len);
                }
                out = response.getOutputStream();
                out.write(baos.toByteArray());
                System.out.println(bytes);
                // 关闭输入流和 FTP 连接
                inputStream.close();
//            ftpClient.logout();
//            ftpClient.disconnect();
//            return bytes;

            }
            //对于视频，调用分割的接口，并将结果返回
            else {
                List<String> cutedVideoPaths = cutVideoService.cutVideo(urlPath);
                for (int i = 0; i < cutedVideoPaths.size(); i++) {
                    File file = new File(cutedVideoPaths.get(i));
                    InputStream inputStream = new FileInputStream(file);
                    byte[] bytes = new byte[1024];
                    ServletOutputStream out = null;
                    ByteArrayOutputStream baos = null;
                    int len;
                    baos = new ByteArrayOutputStream();
                    while ((len = inputStream.read(bytes)) != -1) {
                        baos.write(bytes, 0, len);
                    }
                    out = response.getOutputStream();
                    out.write(baos.toByteArray());
                    System.out.println(bytes);
                    // 关闭输入流和 FTP 连接
                    inputStream.close();
                }
            }
        }catch (Exception e){
            e.printStackTrace();
        }
//        return null;
    }
}
