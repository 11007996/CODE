package com.ruoyi.product.service.impl;

import com.ruoyi.common.core.domain.AjaxResult;
import com.ruoyi.product.service.IMesFfmpegService;
import org.springframework.stereotype.Service;

import java.io.*;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;
import java.util.concurrent.CompletableFuture;
import java.util.concurrent.ExecutionException;

@Service
public class FfmpegCmdHls4M3u8EncImpl implements IMesFfmpegService {
    //加密文件路径，如：enc.key、enc.keyinfo、m3u8文件、ts文件等
    private static String ENC_DIRECTORY = "D:\\app\\openssl_key";
    private static String FILE_NAME = "test.mp4";

    //执行成功0,失败1
    private static int CODE_SUCCESS = 0;
    private static int CODE_FAIL = 1;

    //将荣耀视频测试.MP4 --> HLS m3u8 AES128 加密（//注意绝对路径///）
    //视频路径：I:\\test-ffmpeg\\荣耀视频测试.mp4
    //视频路径：D:\app\openssl_key\test.mp4(当前测试使用的文件路径)
    //$encInfoPath、$encPath是需要替换的ENC_DIRECTORY文件路径
//    private static String cmd_enc = " -y -i I:\\test-ffmpeg\\荣耀视频测试.mp4 -hls_time 12 -hls_key_info_file $encInfoPath -hls_playlist_type vod -hls_segment_filename $encPath\\encfile_12s_%3d.ts $encPath\\荣耀视频测试_HLS.m3u8 ";
//    private static String cmd_enc =" -y -i D:\\app\\openssl_key\\test.mp4 -c:v libx264 -c:a copy -f hls -hls_time 15 -hls_list_size 0 -hls_key_info_file D:\\app\\openssl_key\\enc.keyinfo -hls_playlist_type vod -hls_segment_filename D:\\app\\openssl_key\\file%d.ts D:\\app\\openssl_key\\playlist.m3u8";
    private static String cmd_enc =" -y -i "+ENC_DIRECTORY+"\\"+FILE_NAME+" -c:v libx264 -c:a copy -f hls -hls_time 25 -hls_list_size 0 -hls_key_info_file "+ENC_DIRECTORY+"\\enc.keyinfo -hls_playlist_type vod -hls_segment_filename "+ENC_DIRECTORY+"\\file%d.ts "+ENC_DIRECTORY+"\\playlist.m3u8";

    /**
     * 第一步：创建enc.keyinfo文件
     * 第二步：HLS m3u8 AES128 加密
     * @param: @param args
     * @return: void
     * @throws
     */
    public AjaxResult encryptVideo(String fileName, byte[] fileContent) throws IOException {

        // 创建临时文件夹，创建一个名为“upload”的子目录
        Path tempDir = Paths.get(System.getProperty("java.io.tmpdir"), "upload");
        if (!Files.exists(tempDir)) {
            Files.createDirectories(tempDir);
        }
        // 将文件内容写入临时文件
        Path targetPath = tempDir.resolve(fileName);
        Path parentPath = targetPath.getParent();
        Files.write(targetPath, fileContent);

        System.out.println("文件已上传到临时文件夹：" + targetPath.toString());
//        String ENC_DIRECTORY = targetPath.toString();
        String ENC_DIRECTORY = parentPath.toString();
        String cmd_enc =" -y -i "+ENC_DIRECTORY+"\\"+fileName+" -c:v libx264 -c:a copy -f hls -hls_time 10 -hls_list_size 0 -hls_key_info_file "+ENC_DIRECTORY+"\\enc.keyinfo -hls_playlist_type vod -hls_segment_filename "+ENC_DIRECTORY+"\\file%3d.ts "+ENC_DIRECTORY+"\\playlist.m3u8";


        //异步执行
        //第一步：创建enc.keyinfo文件等
        CompletableFuture<String> completableFutureTask = CompletableFuture.supplyAsync(() ->{
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

            try{//文件
                String encKeyFilePath = ENC_DIRECTORY + "\\encrypt.key";
                encKeyInfoFilePath = ENC_DIRECTORY + "\\enc.keyinfo";
                File fileKey = new File(encKeyFilePath);
                File fileKeyInfo = new File(encKeyInfoFilePath);

                //初始化存在删除
                if(fileKey.exists()) {
                    fileKey.delete();
                }
                if(fileKeyInfo.exists()) {
                    fileKeyInfo.delete();
                }
                bwkey = new BufferedWriter(new FileWriter(fileKey));
                bwkeyInfo = new BufferedWriter(new FileWriter(fileKeyInfo));

                //写入key--自定义的AES128加密的密匙
//                bwkey.write("hsjissmart123456");
                bwkey.write("qwertyuiop123456");
                //写入keyInfo
                //密匙URL地址，可以对该URL鉴权
                bwkeyInfo.write("http://localhost:8080/encrypt.key");
                bwkeyInfo.newLine();
                //全路径，绝对路径
                bwkeyInfo.write(encKeyFilePath);

                bwkey.flush();
                bwkeyInfo.flush();
            }catch(IOException e){
                e.printStackTrace();
                //恢复默认
                encKeyInfoFilePath = null;
            } finally{
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
        CompletableFuture<Integer> completableFutureTaskHls = completableFutureTask.thenApplyAsync((String encKeyInfoFilePath)->{
            if(encKeyInfoFilePath == null || encKeyInfoFilePath.length() == 0) {return CODE_FAIL;}
            System.out.println("第一步：创建enc.keyinfo文件,成功！");
            Integer codeTmp =  cmdExecut(cmd_enc.replace("$encInfoPath", encKeyInfoFilePath).replace("$encPath", ENC_DIRECTORY));
            if(CODE_SUCCESS != codeTmp) {return CODE_FAIL;}
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
        System.out.println("key和ts文件所在文件路径为："+ENC_DIRECTORY);
        return AjaxResult.success(ENC_DIRECTORY);
    }

    /**
     *
     * @Description: (执行ffmpeg自定义命令)
     * @param: @param cmdStr
     * @param: @return
     * @return: Integer
     * @throws
     */
    public static Integer cmdExecut(String cmdStr) {
        //code=0表示正常
        Integer code  = null;
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
            while ((len=errorStream.read())!=-1){
                System.out.print((char)len);
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

}
