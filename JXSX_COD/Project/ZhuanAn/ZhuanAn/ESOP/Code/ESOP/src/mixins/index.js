import { tableNodDataHandler } from "../utils/tableNodDataHandler";
import JsEncrypt from "jsencrypt";
export const noDataMixins = {
    watch: {
        tableData: {
            handler(val) {
                tableNodDataHandler(val, 300);
            },
        },
    },
    data() {
        return {
            pdfStyle: "",
            videoStyle: "",
            pdfSizeList: [],
            jsEncrypt: null,
            pdfPassword: null,
            pdfDialogVisible:false,
            pdfPages:[],
            pdfInterval:10,
            videoDialogVisible:false,
            videoUrl:''
        }
    },
    methods: {
        close_PDF_dialog() {
            this.pdfDialogVisible = false;
        },
        setPDF(flag) {
            this.videoDialogVisible = !flag;
        },
        splitScreen(flag) {
            this.$nextTick(() => {
                if (flag) {
                    this.pdfStyle = "left:0;width:50%;";
                    this.videoStyle = "left:50%;width:50%;height:100%";
                    document.querySelectorAll(
                        ".videoDialog"
                    )[0].children[0].style =
                        "width:100%;height:100%;background:#f4f7fd;";
                } else {
                    this.pdfStyle = "";
                    this.videoStyle = "";
                    document.querySelectorAll(
                        ".videoDialog"
                    )[0].children[0].style =
                        "width:70%;background:#f4f7fd;";
                }
            });
        },
    },
    mounted() {
        this.jsEncrypt = new JsEncrypt();
        const prikey = `-----BEGIN PRIVATE KEY-----
MIIEvQIBADANBgkqhkiG9w0BAQEFAASCBKcwggSjAgEAAoIBAQDKpekUQEnoZa2q
T70c4moKin7KdAB97JnbqOtR3TtMFtc9ANnug6dOOmjFJRbgL4iQsmX3BmAGOmTO
XblpKTybESZvmhNE99++VlowT2dVwMU9CTA44HtZiYZ81lwA9pwhpjqpS0wteDEO
QRcaYy5SO9zcsZ38VBYFusoP2Y91EMb0p23rOdiAttkau2XVIvGh1Ghul0FHz85Y
IThmbfkhP5SUISDwC746St2yLL3GN/dXc+VOs1slRvHv1CERXhqS784Y3qmgb2G9
GiwMD2WP63C0rBOCemoLhMAHAp+/pX0RxOjGaJnBWwG5tuApyhrRwm01auyl/Tjs
mhx3ZwwxAgMBAAECggEAIcZzSY/JgbVos4kkwOqvt+ALb9zTtCk6H5VQ200fM/he
mWlJ6WoB+ZTcn3cmD+l8PnmtavWiDYewA4E1hOR9mG7MVC9+5LDXlta3o3Ooim9d
sGWWpvQrOuokAyyLGxH/RdB52HuXT8DHlFOe8SP0tXoKvrHP3h15qizOvsOJGH6O
AhuqTRVnSbdO97r5peFT/kX0ChHiLHIIsMSk8gIWw73c+ejIWgboCsjFRlBuNqkT
pPWsA3j5scOYPvkXO0kueSi+GLwnr0PhT2k5xO6o1YEAFT0uhU4LCX8YD0L3Tvx2
19z+m8uLn1l2S5wlqk8lKk80UswDeufVJP1odcwTfQKBgQD9CGHhLrUDJONDj/WY
f84Fzo9TFujQ07dvrQaGAbQnKqeBjj52yyL+WWSGrmt3dgo8kcXPszlE+uT4+h38
nruDGyWqexN1gCCAdUu21kjIx5oXPxUoIAl/b9mX04y/6p1sBg8mXztaoxy8mhSG
JazL6RwRoztRpa2d2taMrPAeGwKBgQDNBkVOSIODI8BxltIM97PL1+leLvk4aI82
7JotHdorEuoWEzoB5V2yjNweva+XSQALTNUhdoRxFTxnMXGnwVl/e+7Py5VWB4Wv
ZnMA17zGQwNijBUn1wyfVwToKdPLrXm9LmZ2MQi+5eAz/VX29iaN/W36NvBL1WoQ
j+lCy9WzowKBgDDSPjh5j5l0s5jknOl4t2KtcUAB6pfoUbttchXHHGB2PW2k6W54
UV8sFlZaLwgUsXLwWW9y0Dj8A9P6RnDom5t3UHQtXRrNxveiKiK0A8UhphyYIlfk
npCFH0HJIp4hAZDHNoMb2tLpJ/FH9W/Qsx+A8daBXT+qrO4JPF5WO9pDAoGACWKY
GZVIL+CbFpgI1X8hQ9uGW0FbNzHSHHmINTiAnCgpfwkyRpPxThMUoHOebhZxYhMK
TpXWSjbmpPKmeT9okWVi8TAojd+aRwUxjoBRq+G1bfVron89nK2nE9mWUGSIhhhx
qEdmVxa+xKJ8JOnvqeBIAIQzS8VhLZDo5J3gEnECgYEAxBpef9O6iIBgDPJFfXRd
J93fkdr/GPKTr/33PqTl36DDeTjqmPDHFwXSn1wfMDtdsAq/cFai8wXBAqhDFHlg
/Rge8z0oUeXVpxUS4AQBsnIv9U/USwWcGlabdE4SgaStZa6eHfE3shmO6qvDVBLL
1Hgif0HlsQ0Q5tpaKGYhWXo=
-----END PRIVATE KEY-----`;
        this.jsEncrypt.setPrivateKey(prikey);
    }
}
