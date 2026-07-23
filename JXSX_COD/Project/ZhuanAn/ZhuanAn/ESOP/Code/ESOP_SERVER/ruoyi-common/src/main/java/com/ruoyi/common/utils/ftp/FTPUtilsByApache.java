package com.ruoyi.common.utils.ftp;

import org.apache.commons.net.ftp.FTP;
import org.apache.commons.net.ftp.FTPClient;
import org.apache.commons.net.ftp.FTPFile;
import org.apache.commons.net.ftp.FTPReply;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import java.io.*;

public class FTPUtilsByApache {
    private static final Logger logger = LoggerFactory.getLogger(FTPUtilsByApache.class);
    private String encoding = System.getProperty("file.encoding");
    private FTPClient ftpClient =null;
    private String url;
    private int port;
    private String username;
    private String password;

    /**
     * @Date: 2022/2/16 16:08
     * @Description: 默认为21端口
     **/
    public FTPUtilsByApache(String url,String username,String password){
        this.url=url;
        this.port=21;
        this.username=username;
        this.password=password;
    }
    public FTPUtilsByApache(String str){
        this.url=str.substring(str.indexOf("@")+1);
        this.port=21;
        this.username=str.substring(0,str.indexOf(":"));
        this.password=str.substring(str.indexOf(":")+1,str.indexOf("@"));
    }

    public FTPUtilsByApache(String url,int port,String username,String password){
        this.url=url;
        this.port=port;
        this.username=username;
        this.password=password;
    }


    public FTPClient getFTPClient(){
        int reply;
        if(this.ftpClient == null || !this.ftpClient.isConnected()){
            this.ftpClient=new FTPClient();
            try{
                this.ftpClient.connect(url,port);
                boolean login = this.ftpClient.login(username, password);
                if(login){
                    logger.info("FTP连接成功！");
                }
                // 设置文件传输类型为二进制传输
                this.ftpClient.setFileType(ftpClient.BINARY_FILE_TYPE);
                this.ftpClient.setControlEncoding(encoding);
                reply = ftpClient.getReplyCode();
                if(!FTPReply.isPositiveCompletion(reply)){
                    ftpClient.disconnect();
                    return null;
                }
                return this.ftpClient;
            }catch (IOException e){
                e.printStackTrace();
                logger.info("获取连接失败");
                return null;
            }
        }else{
            return this.ftpClient;
        }
    }

    /**
     * @Date: 2022/2/16 16:26
     * @Description: 指定ftp文件名下载到本地
     * @param remotePath ftp目录
     * @param fileName 指定下载的文件
     * @param localPath 本地目录
     **/

    public boolean downFile(String remotePath, String fileName,String localPath){
        boolean result=false;
        getFTPClient();
        if(!isCreateFtpClient(this.ftpClient)){
            return false;
        }
        try{
            this.ftpClient.changeWorkingDirectory(remotePath);
            FTPFile[] remoteFiles = this.ftpClient.listFiles();
            for(FTPFile rf:remoteFiles){
                if(rf.getName().equals(fileName)){
                    logger.info("获取到"+fileName+"文件");
                    // 创建本地存储路径
                    File lf = new File(localPath + File.separator + rf.getName());
                    FileOutputStream lfos = new FileOutputStream(lf);
                    // 通过文件检索系统,将文件写入流
                    this.ftpClient.retrieveFile(rf.getName(),lfos);
                    System.out.println("下载完毕");
                    lfos.close();
                }
            }
            this.ftpClient.logout();
            result=true;
        }catch (IOException e){
            e.printStackTrace();
            return result;
        }finally {
            if(ftpClient.isConnected()){
                try{
                    ftpClient.disconnect();
                }catch (Exception ee){
                    ee.printStackTrace();
                }
            }
        }
        return result;
    }

    public InputStream viewFile(String remotePath, String fileName){
        getFTPClient();
        if(!isCreateFtpClient(this.ftpClient)){
            return null;
        }
        try{
            this.ftpClient.changeWorkingDirectory(remotePath);
            FTPFile[] remoteFiles = this.ftpClient.listFiles();
            for(FTPFile rf:remoteFiles){
                if(rf.getName().equals(fileName)){
                    logger.info("获取到"+fileName+"文件");
                    // 创建本地存储路径
                    InputStream inputStream =  this.ftpClient.retrieveFileStream(rf.getName());
                    System.out.println("读取完毕");

                    return inputStream;


                }
            }
            this.ftpClient.logout();
        }catch (IOException e){
            e.printStackTrace();

        }finally {
            if(ftpClient.isConnected()){
                try{
                    ftpClient.disconnect();
                }catch (Exception ee){
                    ee.printStackTrace();
                }
            }
        }
        return null;
    }



    /**
     * @Date: 2022/2/16 16:42
     * @Description: 下载所有文件到本地目录,不包含目录
     * @param remotePath ftp目录
     * @param localPath 本地目录
     * @param prefix  前缀
     * @param suffix 后缀
     **/
    public boolean downAllPathFile(String remotePath, String localPath,String prefix,String suffix){
        boolean result=false;
        if(prefix==null||prefix.trim().equals("")){
            //return this.downAllPathFileSuffix(remotePath, localPath, suffix);
            return result;
        }else if(suffix==null||suffix.trim().equals("")){
            //return this.downAllPathFilePrefix(remotePath, localPath, prefix);
            return result;
        }else{
            getFTPClient();
            if(!isCreateFtpClient(this.ftpClient)){
                return false;
            }
            try{
                this.ftpClient.changeWorkingDirectory(remotePath);
                FTPFile[] remoteFiles = this.ftpClient.listFiles();
                for(FTPFile rf:remoteFiles){
                    if(rf.isFile()&&rf.getName().contains(prefix)&&rf.getName().endsWith(suffix)){
                        logger.info("获取到"+rf.getName()+"文件");
                        File lf = new File(localPath + "/" + rf.getName());
                        FileOutputStream lfos = new FileOutputStream(lf);
                        // 通过文件检索系统,将文件写入流
                        this.ftpClient.retrieveFile(rf.getName(),lfos);
                        lfos.close();
                    }
                }
                this.ftpClient.logout();
                result=true;
            }catch (IOException e){
                e.printStackTrace();
                return result;
            }finally {
                if(ftpClient.isConnected()){
                    try{
                        ftpClient.disconnect();
                    }catch (Exception ee){
                        ee.printStackTrace();
                    }
                }
            }
            return result;
        }
    }


    /**
     * @Date: 2022/2/16 16:51
     * @Description: 上传文件
     **/
    public boolean uploadFile(String remotePath, String filename, InputStream lin){
        boolean result = false;
        getFTPClient();
        if(!this.isCreateFtpClient(this.ftpClient)){
            return false;
        }
        try{

            boolean change = ftpClient.changeWorkingDirectory(remotePath);
            if(change){
                System.out.println("进入文件夹成功");
            }else {
                String[] dirs = remotePath.split("/");
                String tempPath = "";
                for (String dir : dirs) {
                    if (null == dir || "".equals(dir)) continue;
                    tempPath += "/" + dir;
                    if (!ftpClient.changeWorkingDirectory(tempPath)) {  //进不去目录，说明该目录不存在
                        if (!ftpClient.makeDirectory(tempPath)) { //创建目录
                            //如果创建文件目录失败，则返回
                            System.out.println("创建文件目录"+tempPath+"失败");
                            return result;
                        } else {
                            //目录存在，则直接进入该目录
                            ftpClient.changeWorkingDirectory(tempPath);
                        }
                    }
                }
            }
            ftpClient.setFileType(FTP.BINARY_FILE_TYPE);

            result = ftpClient.storeFile(new String(filename.getBytes(encoding),"iso-8859-1"), lin);

            logger.info("上传完毕，结果为"+result);
            lin.close();
            ftpClient.logout();
        }catch (Exception e){
            e.printStackTrace();
        }finally {
            if(ftpClient.isConnected()){
                try{
                    ftpClient.disconnect();
                }catch (Exception ee){
                    ee.printStackTrace();
                }
            }
        }
        return result;
    }

    /**
     * @Date: 2022/2/16 16:56
     * @Description: FTP文件上传
     * @param remotePath
     * @param localFilePath 完整的本地路径
     **/
    public boolean uploadFile(String remotePath,String localFilePath) {
        File lf=new File(localFilePath);
        try{
            FileInputStream lfis = new FileInputStream(lf);
            return uploadFile(remotePath,lf.getName(),lfis);
        }catch (Exception e){
            e.printStackTrace();
            return false;
        }
    }

    /**
     * @Date: 2022/2/16 17:01
     * @Description: 删除远程文件信息
     **/
    public boolean deleteFile(String filePath,String fileName){
        getFTPClient();
        boolean result = false;
        if(isCreateFtpClient(this.ftpClient)){
            try{
                ftpClient.changeWorkingDirectory(filePath);
                result = ftpClient.deleteFile(new String(fileName.getBytes(encoding),"iso-8859-1"));
            }catch (IOException e){
                e.printStackTrace();
                return result;
            }finally {
                if(ftpClient.isConnected()){
                    try{
                        ftpClient.disconnect();
                    }catch (Exception ee){
                        ee.printStackTrace();
                    }
                }
            }
        }
        return result;
    }


    /*
     * @Date: 2022/2/16 16:30
     * @Description: 是否创建ftp客户端
     **/
    public boolean isCreateFtpClient(FTPClient c){
        if(c == null|| !c.isConnected()){
            return false;
        }else{
            return true;
        }

    }
}
