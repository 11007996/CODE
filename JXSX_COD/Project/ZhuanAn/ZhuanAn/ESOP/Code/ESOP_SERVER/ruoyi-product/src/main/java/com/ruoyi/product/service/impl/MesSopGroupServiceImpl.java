package com.ruoyi.product.service.impl;

import com.ruoyi.common.config.RuoYiConfig;
import com.ruoyi.common.core.domain.AjaxResult;
import com.ruoyi.common.utils.DateUtils;
import com.ruoyi.product.domain.MesSopGroup;
import com.ruoyi.product.mapper.MesSopGroupMapper;
import com.ruoyi.product.mapper.MesTerminalSopMapper;
import com.ruoyi.product.mapper.MesUploadTerminalPageMapper;
import com.ruoyi.product.service.IMesSopGroupService;
import org.apache.pdfbox.pdmodel.PDDocument;
import org.apache.pdfbox.pdmodel.PDPageTree;
import org.apache.pdfbox.pdmodel.common.PDRectangle;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import javax.crypto.BadPaddingException;
import javax.crypto.Cipher;
import javax.crypto.IllegalBlockSizeException;
import javax.crypto.NoSuchPaddingException;
import java.io.File;
import java.io.IOException;
import java.security.InvalidKeyException;
import java.security.KeyFactory;
import java.security.NoSuchAlgorithmException;
import java.security.PrivateKey;
import java.security.spec.InvalidKeySpecException;
import java.security.spec.PKCS8EncodedKeySpec;
import java.util.*;

/**
 * sop群组Service业务层处理
 *
 * @author ruoyi
 * @date 2022-12-26
 */
@Service
public class MesSopGroupServiceImpl implements IMesSopGroupService
{
    @Autowired
    private MesSopGroupMapper mesSopGroupMapper;

    /**
     * 查询sop群组
     *
     * @param sopGroupId sop群组主键
     * @return sop群组
     */
    @Override
    public  List<MesSopGroup> selectMesSopGroupBySopGroupId(String sopGroupId)
    {
        return mesSopGroupMapper.selectMesSopGroupBySopGroupId(sopGroupId);
    }

    /**
     * 查询sop群组列表
     *
     * @param mesSopGroup sop群组
     * @return sop群组
     */
    @Override
    public List<MesSopGroup> selectMesSopGroupList(MesSopGroup mesSopGroup)
    {
        return mesSopGroupMapper.selectMesSopGroupList(mesSopGroup);
    }

    /**
     * 新增sop群组
     *
     * @param mesSopGroup sop群组
     * @return 结果
     */
    @Override
    public int insertMesSopGroup(MesSopGroup mesSopGroup)
    {
        mesSopGroup.setCreateTime(DateUtils.getNowDate());
        return mesSopGroupMapper.insertMesSopGroup(mesSopGroup);
    }

    /**
     * 修改sop群组
     *
     * @param mesSopGroup sop群组
     * @return 结果
     */
    @Override
    public int updateMesSopGroup(MesSopGroup mesSopGroup)
    {
        mesSopGroup.setUpdateTime(DateUtils.getNowDate());
        return mesSopGroupMapper.updateMesSopGroup(mesSopGroup);
    }

    /**
     * 批量删除sop群组
     *
     * @param sopGroupIds 需要删除的sop群组主键
     * @return 结果
     */
    @Override
    public int deleteMesSopGroupBySopGroupIds(Long[] sopGroupIds)
    {
        return mesSopGroupMapper.deleteMesSopGroupBySopGroupIds(sopGroupIds);
    }

    /**
     * 删除sop群组信息
     *
     * @param sopGroupId sop群组主键
     * @return 结果
     */
    @Override
    public int deleteMesSopGroupBySopGroupId(Long sopGroupId,String type)
    {
        return mesSopGroupMapper.deleteMesSopGroupBySopGroupId(sopGroupId,type);
    }

    @Override
    public Long selectMesSopGroupTypeNum(MesSopGroup mesSopGroup) {
        return mesSopGroupMapper.selectMesSopGroupTypeNum(mesSopGroup);
    }

    @Autowired
    private MesUploadTerminalPageMapper uploadTerminalPageMapper;

    @Autowired
    private MesTerminalSopMapper mesTerminalSopMapper;

    @Override
    public AjaxResult updateSignedSopList(Long sopId, Long materialId) throws IOException {
//        MesUploadTerminalPage mesUploadTerminalPage = new MesUploadTerminalPage();
//        mesUploadTerminalPage.setSopId(sopId);
//        mesUploadTerminalPage.setMaterialId(materialId);
//        List<MesUploadTerminalPage> pageList =  uploadTerminalPageMapper.selectMesUploadTerminalPageList(mesUploadTerminalPage);
//
//
//
//        for (MesUploadTerminalPage page: pageList ) {
//            MesTerminalSop terminalSop = new MesTerminalSop();
//            terminalSop.setModelId(page.getModelId());
//            terminalSop.setMaterialId(page.getMaterialId());
//            terminalSop.setLineId(page.getLineId());
//            terminalSop.setStageId(page.getStageId());
//            terminalSop.setProcessId(page.getProcessId());
//            terminalSop.setTerminalId(page.getTerminalId());
//            Long id = mesTerminalSopMapper.selectIdByTerminalSopInfo(terminalSop);
//            String type = page.getType();
//            MesSopGroup sopGroup = new MesSopGroup();
//            sopGroup.setSopGroupId(id.toString());
//            sopGroup.setSopId(sopId);
//            sopGroup.setType(type);
//            sopGroup.setSopPage(page.getSopPage());
//            Long l =  mesSopGroupMapper.selectMesSopGroupTypeNum(sopGroup);
//            if(l > 0){
//                mesSopGroupMapper.deleteMesSopGroupBySopGroupId(id,type);
//            }else {
//                mesSopGroupMapper.insertMesSopGroup(sopGroup);
//            }
//
//            String sid = page.getModelId()+"_"+page.getMaterialId()+"_"+page.getLineId()+"_"+page.getStageId()+"_"+page.getProcessId()+"_"+page.getTerminalId();
//            List<MesSopGroup> list =   mesSopGroupMapper.selectMesSopGroupBySopGroupId(id.toString());
//            WebSocketServer.sendInfo(list.toString(), sid);
//        }

        return AjaxResult.success("OK");
    }

    /**
     * 加密pdf文件获取宽高
     * */
    //Base64编码的私钥字符串
    private static final String privateKey = "MIIEvQIBADANBgkqhkiG9w0BAQEFAASCBKcwggSjAgEAAoIBAQDKpekUQEnoZa2q\n" +
            "T70c4moKin7KdAB97JnbqOtR3TtMFtc9ANnug6dOOmjFJRbgL4iQsmX3BmAGOmTO\n" +
            "XblpKTybESZvmhNE99++VlowT2dVwMU9CTA44HtZiYZ81lwA9pwhpjqpS0wteDEO\n" +
            "QRcaYy5SO9zcsZ38VBYFusoP2Y91EMb0p23rOdiAttkau2XVIvGh1Ghul0FHz85Y\n" +
            "IThmbfkhP5SUISDwC746St2yLL3GN/dXc+VOs1slRvHv1CERXhqS784Y3qmgb2G9\n" +
            "GiwMD2WP63C0rBOCemoLhMAHAp+/pX0RxOjGaJnBWwG5tuApyhrRwm01auyl/Tjs\n" +
            "mhx3ZwwxAgMBAAECggEAIcZzSY/JgbVos4kkwOqvt+ALb9zTtCk6H5VQ200fM/he\n" +
            "mWlJ6WoB+ZTcn3cmD+l8PnmtavWiDYewA4E1hOR9mG7MVC9+5LDXlta3o3Ooim9d\n" +
            "sGWWpvQrOuokAyyLGxH/RdB52HuXT8DHlFOe8SP0tXoKvrHP3h15qizOvsOJGH6O\n" +
            "AhuqTRVnSbdO97r5peFT/kX0ChHiLHIIsMSk8gIWw73c+ejIWgboCsjFRlBuNqkT\n" +
            "pPWsA3j5scOYPvkXO0kueSi+GLwnr0PhT2k5xO6o1YEAFT0uhU4LCX8YD0L3Tvx2\n" +
            "19z+m8uLn1l2S5wlqk8lKk80UswDeufVJP1odcwTfQKBgQD9CGHhLrUDJONDj/WY\n" +
            "f84Fzo9TFujQ07dvrQaGAbQnKqeBjj52yyL+WWSGrmt3dgo8kcXPszlE+uT4+h38\n" +
            "nruDGyWqexN1gCCAdUu21kjIx5oXPxUoIAl/b9mX04y/6p1sBg8mXztaoxy8mhSG\n" +
            "JazL6RwRoztRpa2d2taMrPAeGwKBgQDNBkVOSIODI8BxltIM97PL1+leLvk4aI82\n" +
            "7JotHdorEuoWEzoB5V2yjNweva+XSQALTNUhdoRxFTxnMXGnwVl/e+7Py5VWB4Wv\n" +
            "ZnMA17zGQwNijBUn1wyfVwToKdPLrXm9LmZ2MQi+5eAz/VX29iaN/W36NvBL1WoQ\n" +
            "j+lCy9WzowKBgDDSPjh5j5l0s5jknOl4t2KtcUAB6pfoUbttchXHHGB2PW2k6W54\n" +
            "UV8sFlZaLwgUsXLwWW9y0Dj8A9P6RnDom5t3UHQtXRrNxveiKiK0A8UhphyYIlfk\n" +
            "npCFH0HJIp4hAZDHNoMb2tLpJ/FH9W/Qsx+A8daBXT+qrO4JPF5WO9pDAoGACWKY\n" +
            "GZVIL+CbFpgI1X8hQ9uGW0FbNzHSHHmINTiAnCgpfwkyRpPxThMUoHOebhZxYhMK\n" +
            "TpXWSjbmpPKmeT9okWVi8TAojd+aRwUxjoBRq+G1bfVron89nK2nE9mWUGSIhhhx\n" +
            "qEdmVxa+xKJ8JOnvqeBIAIQzS8VhLZDo5J3gEnECgYEAxBpef9O6iIBgDPJFfXRd\n" +
            "J93fkdr/GPKTr/33PqTl36DDeTjqmPDHFwXSn1wfMDtdsAq/cFai8wXBAqhDFHlg\n" +
            "/Rge8z0oUeXVpxUS4AQBsnIv9U/USwWcGlabdE4SgaStZa6eHfE3shmO6qvDVBLL\n" +
            "1Hgif0HlsQ0Q5tpaKGYhWXo=";

    // 添加PEM格式的开头和结尾标记
    private static final String pemPrivateKey = "-----BEGIN PRIVATE KEY-----"+privateKey+"-----END PRIVATE KEY-----";
    @Override
    public List<Map<String,Float>> pdfSize (String filePath, String passWord) {
        List<Map<String,Float>> mapList = new ArrayList<>();

//        filePath = "C:\\Users\\LiuYan\\Desktop\\ESOP-Config\\0313\\午夜自行车.pdf";
        filePath = filePath.replaceFirst("^/profile", "");
        try  {
            // 从PEM格式的私钥字符串中提取私钥
            PrivateKey privateKey = extractPrivateKey(pemPrivateKey);

            // 使用私钥解密密码
            Cipher cipher = Cipher.getInstance("RSA");
            cipher.init(Cipher.DECRYPT_MODE, privateKey);
            byte[] decryptedBytes = cipher.doFinal(Base64.getDecoder().decode(passWord));
            String decryptedPassword = new String(decryptedBytes);
            System.out.println(decryptedPassword);

            // 使用解密后的密码加载PDF文档
            try (PDDocument document = PDDocument.load(new File(RuoYiConfig.getProfile()+"/"+filePath), decryptedPassword)) {
//            try (PDDocument document = PDDocument.load(new File(filePath), decryptedPassword)) {
                PDPageTree pageTree = document.getPages();
                for (int i = 0; i < pageTree.getCount(); i++) {
                    Map<String,Float> map = new HashMap<>();
                    PDRectangle pageSize = pageTree.get(i).getMediaBox();
                    float width = pageSize.getWidth();
                    float height = pageSize.getHeight();
                    System.out.println("Page width: " + width + ", height: " + height);
                    map.put("width", width);
                    map.put("height", height);
                    mapList.add(map);
                }
            }
        } catch (IOException e) {
            e.printStackTrace();
        } catch (NoSuchAlgorithmException e) {
            throw new RuntimeException(e);
        } catch (InvalidKeySpecException e) {
            throw new RuntimeException(e);
        } catch (NoSuchPaddingException e) {
            throw new RuntimeException(e);
        } catch (IllegalBlockSizeException e) {
            throw new RuntimeException(e);
        } catch (BadPaddingException e) {
            throw new RuntimeException(e);
        } catch (InvalidKeyException e) {
            throw new RuntimeException(e);
        }
        return mapList;
    }

    private static PrivateKey extractPrivateKey(String key)
            throws NoSuchAlgorithmException, InvalidKeySpecException {
        String privateKeyPEM = key;
        privateKeyPEM = privateKeyPEM.replace("-----BEGIN PRIVATE KEY-----", "");
        privateKeyPEM = privateKeyPEM.replace("-----END PRIVATE KEY-----", "");
        privateKeyPEM = privateKeyPEM.replaceAll("\\s", "");
        byte[] encoded = Base64.getDecoder().decode(privateKeyPEM);
        PKCS8EncodedKeySpec keySpecPKCS8 = new PKCS8EncodedKeySpec(encoded);
        KeyFactory kf = KeyFactory.getInstance("RSA");
        return kf.generatePrivate(keySpecPKCS8);
    }


    /**
     * 不加密pdf文件获取宽高(仅供本地文件的测试用)
     * */
    @Override
    public List<Map<String,Float>> pdfSize1 (String filePath) {
        List<Map<String, Float>> list = new ArrayList<>();
        Map<String, Float> map = new HashMap<>();
        try {
//            filePath = filePath.replaceFirst("^/profile", "");
            filePath = "C:\\Users\\LiuYan\\Desktop\\ESOP-Config\\0313\\IGBT无密码.pdf";
            try (PDDocument document = PDDocument.load(new File(filePath))) {
//            try (PDDocument document = PDDocument.load(new File(RuoYiConfig.getProfile()+"/"+filePath))) {
                PDPageTree pageTree = document.getPages();
                for (int i = 0; i < pageTree.getCount(); i++) {
                    PDRectangle pageSize = pageTree.get(i).getMediaBox();
                    float width = pageSize.getWidth();
                    float height = pageSize.getHeight();
                    System.out.println("Page width: " + width + ", height: " + height);
                    map.put("width", width);
                    map.put("height", height);
                    list.add(map);
                }
            }
        } catch (IOException e) {
            throw new RuntimeException(e);
        }
        return list;
    }
}
