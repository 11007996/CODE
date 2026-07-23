package com.ruoyi.web.controller.product.Decipher;


import com.ruoyi.common.core.controller.BaseController;
import com.ruoyi.product.service.IMesFfmpegService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import javax.servlet.http.HttpServletResponse;
import java.io.File;
import java.io.FileInputStream;
import java.util.HashMap;
import java.util.Map;

@RestController
@RequestMapping("/product")
public class DecipherFfmpegKey extends BaseController {
    @Autowired
    private IMesFfmpegService ffmpegService;

    @GetMapping("/ffmpegKey")
    public Map<String,StringBuilder> decryptKeyFile(String urlPath, HttpServletResponse response) throws Exception {
        Map<String,StringBuilder> map = new HashMap<String, StringBuilder>();
        File file=new File(urlPath);//里面输入特定目录
        File temp=null;
        File[] filelist= file.listFiles();
        for(int i=0;i<filelist.length;i++){
            temp=filelist[i];
//            String fileName = temp.substring(temp.lastIndexOf('/') + 1);
            String fileName = temp.getName();

//            InputStream inputStream = new FileInputStream(temp);//之前是在FTP获取到文件，现在改成直接获取临时文件夹里面的文件
//            byte[] bytes = new byte[1024];
//            ServletOutputStream out = null;
//            ByteArrayOutputStream baos = null;
//            int len;
//            baos = new ByteArrayOutputStream();
//            while ((len = inputStream.read(bytes)) != -1) {
//                baos.write(bytes, 0, len);
//            }
//            out = response.getOutputStream();
//            out.write(baos.toByteArray());
//            System.out.println(bytes);
//
//            map.put(fileName, String.valueOf(bytes));
//            //关闭输入流
//            inputStream.close();


            try {
                // 创建 FileInputStream 对象，并传入需要读取的文件对象
                FileInputStream fis = new FileInputStream(temp);

                // 定义一个byte数组，用于缓存读取到的字节
                byte[] buffer = new byte[1024];

                // 定义一个整型变量，用于记录已经读取的字节数量
                int count =0;

                // 持续读取文件直到到达文件结尾（read()返回-1），并将读取到的字节缓存在buffer中
                StringBuilder sum = new StringBuilder(); // 在循环外部定义sum
                while((count = fis.read(buffer)) != -1) {
                    // 处理读取到的字节，可以使用String构造器转换成字符串
                    String content = new String(buffer, 0, count);
                    System.out.println(content);
                    sum.append(content); // 将content添加到sum中
                }
                map.put(fileName,sum);

                // 关闭 FileInputStream
                fis.close();
            } catch (Exception e) {
                e.printStackTrace();
            }
        }
        return map;
    }
}
