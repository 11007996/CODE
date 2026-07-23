package com.ruoyi.web.controller.product.Encrypt;

import com.ruoyi.common.config.RuoYiConfig;
import com.ruoyi.common.utils.StringUtils;
import com.ruoyi.web.controller.product.ThreadPoolExecutorUtils;

import java.io.BufferedWriter;
import java.io.File;
import java.io.FileWriter;
import java.io.IOException;
import java.io.InputStream;
import java.net.InetAddress;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;
import java.util.Calendar;
import java.util.HashMap;
import java.util.Map;
import java.util.concurrent.CompletableFuture;
import java.util.concurrent.ExecutionException;

/**
 * 优点：各种浏览器，手机，小程序都能兼容，通用性很好。
 缺点：公开的算法，还原也十分简单，有很多影音嗅探工具能直接下载还原，加密效果很弱，防小白可以。

 ①新建一个记事本，取名enc.key（名字可以随便取），添加16个字节的自定义的AES128加密的密匙，如：
 hsjissmart123456

 ②新建一个文件，enc.keyinfo，添加如下内容（注意：enc.keyinfo里面的enc.key路径绝对路径）
 http://localhost:8080/enc.key
 xxx\enc.key

 ③这里需要把enc.key和enc.keyinfo放在同一目录下

 * @Description:(MP4 转码 HLS m3u8 AES128 加密)
 * @Copyright:
 */
public class EncryptVideoController {

    //加密文件路径，如：enc.key、enc.keyinfo、m3u8文件、ts文件等
//    private static String ENC_DIRECTORY = "D:\\app\\openssl_key";
//    private static String FILE_NAME = "test.mp4";
    //执行成功0,失败1
    private static int CODE_SUCCESS = 0;
    private static int CODE_FAIL = 1;

    //将荣耀视频测试.MP4 --> HLS m3u8 AES128 加密（//注意绝对路径///）
    //视频路径：I:\\test-ffmpeg\\荣耀视频测试.mp4
    //视频路径：D:\app\openssl_key\test.mp4(当前测试使用的文件路径)
    //$encInfoPath、$encPath是需要替换的ENC_DIRECTORY文件路径
//    private static String cmd_enc = " -y -i I:\\test-ffmpeg\\荣耀视频测试.mp4 -hls_time 12 -hls_key_info_file $encInfoPath -hls_playlist_type vod -hls_segment_filename $encPath\\encfile_12s_%3d.ts $encPath\\荣耀视频测试_HLS.m3u8 ";
//    private static String cmd_enc =" -y -i D:\\app\\openssl_key\\test.mp4 -c:v libx264 -c:a copy -f hls -hls_time 15 -hls_list_size 0 -hls_key_info_file D:\\app\\openssl_key\\enc.keyinfo -hls_playlist_type vod -hls_segment_filename D:\\app\\openssl_key\\file%d.ts D:\\app\\openssl_key\\playlist.m3u8";

//    private static String cmd_enc =" -y -i "+ENC_DIRECTORY+"\\"+FILE_NAME+" -c:v libx264 -c:a copy -f hls -hls_time 25 -hls_list_size 0 -hls_key_info_file "+ENC_DIRECTORY+"\\enc.keyinfo -hls_playlist_type vod -hls_segment_filename "+ENC_DIRECTORY+"\\file%d.ts "+ENC_DIRECTORY+"\\playlist.m3u8";

    /**
     * 第一步：创建enc.keyinfo文件
     * 第二步：HLS m3u8 AES128 加密
     *
     * @throws
     * @param: @param args
     * @return: void
     */
    public static Map<String,Object> encryptVideo(String fileName, byte[] fileContent){
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
            String randompath =random.toString()+time.toString();
            //服务器真实地址
            Path tempDir = Paths.get(RuoYiConfig.getProfile(),"/upload","/"+year,"/"+month,"/"+day,"/"+randompath);

            //文件在浏览器获取的地址（不包含文件）
            String browserUrlExfilename = "/profile/upload/"+year+"/"+month+"/"+day+"/"+randompath;
            //文件在浏览器获取的地址(加密后的)
            String browserUrl = browserUrlExfilename+"/playlist.m3u8";

            if (!Files.exists(tempDir)) {
                Files.createDirectories(tempDir);
            }
            // 将文件内容写入临时文件
            Path targetPath = tempDir.resolve(fileName);
            Path parentPath = targetPath.getParent();
            Files.write(targetPath, fileContent);

            System.out.println("文件已上传到临时文件夹：" + targetPath.toString());
            String ENC_DIRECTORY = parentPath.toString();

            if(fileName.endsWith("mts")||fileName.endsWith("MTS")){
//            String cmd_exchange = "-i "+ENC_DIRECTORY + "\\" + fileName+" -s 640x480 -b:v 4000k "+ENC_DIRECTORY + "\\" + fileName.substring(0, fileName.lastIndexOf('.'))+".mp4";
                String cmd_exchange = "-i "+ENC_DIRECTORY + "/" + fileName+" -s 640x480 -b:v 4000k "+ENC_DIRECTORY + "/" + fileName.substring(0, fileName.lastIndexOf('.'))+".mp4";
                Integer codeTmp = cmdExecut(cmd_exchange);
                if (CODE_SUCCESS != codeTmp){
                    map.put("code",500);
                    map.put("msg","mts文件转mp4文件 失败");
                    return map;
                }
                System.out.println("mts文件转mp4文件 成功！");
                System.out.println(codeTmp);
            }

//        String cmd_enc = " -y -i " + ENC_DIRECTORY + "\\" + fileName + " -c:v libx264 -c:a copy -f hls -hls_time 5 -hls_list_size 0 -hls_key_info_file " + ENC_DIRECTORY + "\\enc.keyinfo -hls_playlist_type vod -hls_segment_filename " + ENC_DIRECTORY + "\\file%3d.ts " + ENC_DIRECTORY + "\\playlist.m3u8";
            String cmd_enc = " -y -i " + ENC_DIRECTORY + "/" + fileName + " -c:v libx264 -c:a copy -f hls -hls_time 5 -hls_list_size 0 -hls_key_info_file " + ENC_DIRECTORY + "/enc.keyinfo -hls_playlist_type vod -hls_segment_filename " + ENC_DIRECTORY + "/file%3d.ts " + ENC_DIRECTORY + "/playlist.m3u8";

            //异步执行
            //第一步：创建enc.keyinfo文件等
            CompletableFuture<String> completableFutureTask = CompletableFuture.supplyAsync(() -> {
                //创建enc.keyinfo文件,返回文件地址
                String encKeyInfoFilePath = null;

                //目录enc
                File encFilePathDir = new File(ENC_DIRECTORY);
                if (!encFilePathDir.exists()) {// 判断目录是否存在
                    encFilePathDir.mkdir();
                }

                //写入文件内容enc.key
                BufferedWriter bwkey = null;
                //写入文件内容enc.keyinfo
                BufferedWriter bwkeyInfo = null;

                try {//文件
//                String encKeyFilePath = ENC_DIRECTORY + "\\encrypt.key";
                    String encKeyFilePath = ENC_DIRECTORY + "/encrypt.key";
//                encKeyInfoFilePath = ENC_DIRECTORY + "\\enc.keyinfo";
                    encKeyInfoFilePath = ENC_DIRECTORY + "/enc.keyinfo";
                    File fileKey = new File(encKeyFilePath);
                    File fileKeyInfo = new File(encKeyInfoFilePath);

                    //初始化存在删除
                    if (fileKey.exists()) {
                        fileKey.delete();
                    }
                    if (fileKeyInfo.exists()) {
                        fileKeyInfo.delete();
                    }
                    bwkey = new BufferedWriter(new FileWriter(fileKey));
                    bwkeyInfo = new BufferedWriter(new FileWriter(fileKeyInfo));

                    //写入key--自定义的AES128加密的密匙
                    String AESKeyStr = "qwertyuiop123456";
                    String AESKeyStrEncryed = encryptKeyStr(AESKeyStr);
//                    String AESKeyStrEncryed = xor(AESKeyStr);
                    bwkey.write(AESKeyStrEncryed);
                    //写入keyInfo
                    //密匙URL地址，可以对该URL鉴权
                    String bwkeyPath = "http://"+ InetAddress.getLocalHost().getHostAddress()+":8090"+browserUrlExfilename+"/encrypt.key";
                    bwkeyInfo.write(bwkeyPath);
                    bwkeyInfo.newLine();
                    //全路径，绝对路径
                    bwkeyInfo.write(encKeyFilePath);

                    bwkey.flush();
                    bwkeyInfo.flush();
                } catch (IOException e) {
                    e.printStackTrace();
                    //恢复默认
                    encKeyInfoFilePath = null;
                } finally {
                    try {
                        //一定要关闭文件
                        bwkey.close();
                        bwkeyInfo.close();
                    } catch (IOException e) {
                        e.printStackTrace();
                    }
                }
                return encKeyInfoFilePath;
            }, ThreadPoolExecutorUtils.pool);
            //异步执行
            //第二步：HLS m3u8 AES128 加密
            CompletableFuture<Integer> completableFutureTaskHls = completableFutureTask.thenApplyAsync((String encKeyInfoFilePath) -> {
                if (encKeyInfoFilePath == null || encKeyInfoFilePath.length() == 0) {
                    return CODE_FAIL;
                }
                System.out.println("第一步：创建enc.keyinfo文件,成功！");
                Integer codeTmp = cmdExecut(cmd_enc.replace("$encInfoPath", encKeyInfoFilePath).replace("$encPath", ENC_DIRECTORY));
                if (CODE_SUCCESS != codeTmp) {
                    return CODE_FAIL;
                }
                System.out.println("第二步：HLS m3u8 AES128 加密,成功！");
                return codeTmp;
            }, ThreadPoolExecutorUtils.pool);

            //获取执行结果
            //code=0表示正常
            try {
                System.out.println(String.format("获取最终执行结果:%s", completableFutureTaskHls.get() == CODE_SUCCESS ? "成功！" : "失败！"));
            } catch (InterruptedException e) {
                Thread.currentThread().interrupt();
                e.printStackTrace();
            } catch (ExecutionException e) {
                e.printStackTrace();
            }
            System.out.println("key和ts文件所在文件路径为：" + ENC_DIRECTORY);
            originalDelete(ENC_DIRECTORY);
            map.put("code",200);
            map.put("msg","OK");


            map.put("browserUrl",browserUrl);
            map.put("filePath",targetPath);
        }catch (Exception ex){
            map.put("code",500);
            map.put("msg","OK");
        }
        return map;
    }

    /**
     * @throws
     * @Description: (执行ffmpeg自定义命令)
     * @param: @param cmdStr
     * @param: @return
     * @return: Integer
     */
    public static Integer cmdExecut(String cmdStr) {
        //code=0表示正常
        Integer code = null;
        FfmpegCmd ffmpegCmd = new FfmpegCmd();
        /**
         * 错误流
         */
        InputStream errorStream = null;
        try {
            //destroyOnRuntimeShutdown表示是否立即关闭Runtime
            //如果ffmpeg命令需要长时间执行，destroyOnRuntimeShutdown = false

            //openIOStreams表示是不是需要打开输入输出流:
            //	       inputStream = processWrapper.getInputStream();
            //	       outputStream = processWrapper.getOutputStream();
            //	       errorStream = processWrapper.getErrorStream();
            ffmpegCmd.execute(false, true, cmdStr);
            errorStream = ffmpegCmd.getErrorStream();

            //打印过程
            int len = 0;
            while ((len = errorStream.read()) != -1) {
                System.out.print((char) len);
            }

            //code=0表示正常
            code = ffmpegCmd.getProcessExitCode();
        } catch (IOException e) {
            e.printStackTrace();
        } finally {
            //关闭资源
            ffmpegCmd.close();
        }
        //返回
        return code;
    }

    public static String originalDelete(String url) {
        File file = new File(url);//里面输入特定目录
        File temp = null;
        File[] filelist = file.listFiles();
        for (int i = 0; i < filelist.length; i++) {
            temp = filelist[i];
            String endStr = temp.getName().substring(temp.getName().length()-3).toLowerCase();
            if (endStr.equals("mp4")||endStr.equals("mts"))//获得文件名，如果后缀为“”，就删除文件
            {
                temp.delete();//删除文件}
            }
        }
        return ("已删除原文件");
    }

    public static String encryptKeyStr(String input) {
        StringBuilder output = new StringBuilder();
        int offset = 10;

        for (int i = 0; i < input.length(); i++) {
            char c = input.charAt(i);
            // 移动字符的 ASCII 码值
            int encryptedCharCode = (int) c + offset;
            // 确保字符仍然在可打印的 ASCII 范围内
            encryptedCharCode %= 128;
            output.append((char) encryptedCharCode);
        }

        return output.toString();
    }
//        public static String xor(String input) {
//            char[] chs = input.toCharArray();//因为是对每一个字符进行加密，则需要转成数组
//            for(int i=0;i<chs.length;i++) {
//                chs[i] = (char) (chs[i]^100);//对每一位字符进行加密，即每个元素与100进行异或
//            }
//            return new String(chs);
//        }

        public static String xor(String input) {
            char[] arr = input.toCharArray();//因为是对每一个字符进行加密，则需要转成数组
            int xorVal = 0b00000100; // 二进制数00000100
            StringBuilder xorResult = new StringBuilder();

            for (char c : arr) {
                int intValue = c; // 将字符转换为ASCII码对应的整数值
                int result = intValue ^ xorVal; // 执行异或运算
                xorResult.append(result + ",");
                System.out.println(c + " 异或 " + Integer.toBinaryString(xorVal) + " 的结果为：" + Integer.toBinaryString(result));
            }
//            System.out.println(￥￥.toString().substring(0, ￥￥.length()-1)); // 输出结果，去掉最后一个逗号
            return (xorResult.toString().substring(0,xorResult.length()-1));
        }
}

