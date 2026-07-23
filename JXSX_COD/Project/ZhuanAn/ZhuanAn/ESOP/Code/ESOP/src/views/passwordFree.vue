<template>
    <div class="login">
        <div class="fixedLogo">
            <img src="../assets/logo/logo_big.png" alt="" />
        </div>
        <el-form
            ref="loginForm"
            :model="loginForm"
            :rules="loginRules"
            class="login-form"
            label-width="100px"
        >
            <h3 class="title">ESOP管理系统</h3>
            <el-form-item label="线别" prop="lineId">
                <el-select
                    v-model="loginForm.lineId"
                    clearable
                    filterable
                    @change="changeLine"
                    placeholder="请选择线别"
                    ref="lineSelect"
                    size="mini"
                >
                    <el-option
                        v-for="(item, index) in lineOption"
                        :key="index"
                        :label="item.lineName"
                        :value="item.lineId"
                    />
                </el-select>
            </el-form-item>
            <el-form-item label="站位序号" prop="stageId">
                <el-select
                    v-model="loginForm.stageId"
                    clearable
                    filterable
                    @change="changeStage"
                    placeholder="请选择站位序号"
                    ref="stageSelect"
                    size="mini"
                >
                    <el-option
                        v-for="(item, index) in stageOption"
                        :key="index"
                        :label="item.stageName"
                        :value="item.stageId"
                    />
                </el-select>
            </el-form-item>
            <el-form-item label="站位名" prop="processId">
                <el-select
                    v-model="loginForm.processId"
                    clearable
                    filterable
                    placeholder="请选择站位名"
                    @change="changeProcess"
                    size="mini"
                    ref="processSelect"
                >
                    <el-option
                        v-for="(item, index) in processOption"
                        :key="index"
                        :label="item.processName"
                        :value="item.processId"
                    />
                </el-select>
            </el-form-item>
            <!-- <el-form-item label="工站" prop="terminalId">
                <el-select
                    v-model="loginForm.terminalId"
                    clearable
                    filterable
                    placeholder="请选择工站"
                    size="mini"
                    ref="terminalSelect"
                >
                    <el-option
                        v-for="(item, index) in terminalOption"
                        :key="index"
                        :label="item.terminalName"
                        :value="item.terminalId"
                    />
                </el-select>
            </el-form-item> -->
            <el-form-item style="width: 100%">
                <el-button
                    :loading="loading"
                    size="medium"
                    type="primary"
                    style="width: 100%"
                    @click.native.prevent="handleLogin"
                >
                    <span v-if="!loading">登 录</span>
                    <span v-else>登 录 中...</span>
                </el-button>
            </el-form-item>
        </el-form>
        <!--  底部  -->
        <div class="el-login-footer"></div>
    </div>
</template>

<script>
import { getMacList } from "@/api/product/mac";
import { listLine } from "@/api/product/line";
import { listTerminal } from "@/api/product/terminal";
import {
    selectLineByMac,
    selectStageByMac,
    selectProcessByMac,
} from "@/api/passwordFree.js";
// import {
//     getStageInTerminal,
//     getProcessInTerminal,
// } from "@/api/product/terminal";
import Cookies from "js-cookie";
import { encrypt, decrypt } from "@/utils/jsencrypt";

export default {
    name: "Login",
    data() {
        return {
            codeUrl: "",
            loginForm: {
                username: "test3",
                password: "2k$1XgR#yT9@zLc%5F*p",
                rememberMe: false,
                lineId: "",
                stageId: "",
                processId: "",
                terminalId: "",
                code: "",
                uuid: "",
            },
            loginRules: {
                lineId: [
                    {
                        required: true,
                        trigger: ["blur", "change"],
                        message: "请选择一个线体",
                    },
                ],
                stageId: [
                    {
                        required: true,
                        trigger: ["blur", "change"],
                        message: "请选择站位序号",
                    },
                ],
                processId: [
                    {
                        required: true,
                        trigger: ["blur", "change"],
                        message: "请选择站位名",
                    },
                ],
                // terminalId: [
                //     {
                //         required: true,
                //         trigger: ["blur", "change"],
                //         message: "请选择工站",
                //     },
                // ],
            },
            loading: false,
            // 验证码开关
            captchaEnabled: true,
            // 注册开关
            register: false,
            redirect: undefined,
            lineOption: [],
            stageOption: [],
            processOption: [],
            terminalOption: [],
        };
    },
    watch: {
        $route: {
            handler: function (route) {
                this.redirect = route.query && route.query.redirect;
            },
            immediate: true,
        },
    },
    created() {
        // this.getCode();
        // this.getCookie();
        this.getLine();
    },
    methods: {
        //查询线体
        getLine() {
            selectLineByMac({ macName: localStorage.getItem("mac") }).then(
                (res) => {
                    this.lineOption = res.rows;
                }
            );
            // listLine({pageSize:99999,pageNum:1}).then((res) => {
            //     this.lineOption = res.rows;
            // });
        },
        //线别change查询站位序号
        changeLine(val) {
            if (val) {
                console.log({
                    macName: localStorage.getItem("mac"),
                    lineId: val,
                })
                selectStageByMac({
                    macName: localStorage.getItem("mac"),
                    lineId: val,
                }).then((res) => {
                    this.stageOption = res.rows;
                });
            }
        },
        //站位序号change查询站位名
        changeStage(val) {
            if (val) {
                selectProcessByMac({
                    macName: localStorage.getItem("mac"),
                    lineId: this.loginForm.lineId,
                    stage: val,
                }).then((res) => {
                    this.processOption = res.rows;
                });
            }
        },
        //站位名change查询工站
        changeProcess(val) {
            if (val) {
                listTerminal({ processId: val }).then((res) => {
                    this.terminalOption = res.rows;
                });
            }
        },
        getCookie() {
            const username = Cookies.get("username");
            const password = Cookies.get("password");
            const rememberMe = Cookies.get("rememberMe");
            this.loginForm = {
                username:
                    username === undefined ? this.loginForm.username : username,
                password:
                    password === undefined
                        ? this.loginForm.password
                        : decrypt(password),
                rememberMe:
                    rememberMe === undefined ? false : Boolean(rememberMe),
            };
        },
        handleLogin() {
            this.$refs.loginForm.validate((valid) => {
                if (valid) {
                    this.loading = true;
                    this.$store
                        .dispatch("Login", this.loginForm)
                        .then(() => {
                            Cookies.set("lineId", this.loginForm.lineId, {
                                expires: 30,
                            });
                            Cookies.set(
                                "stageId",
                                encrypt(this.loginForm.stageId),
                                { expires: 30 }
                            );
                            Cookies.set("processId", this.loginForm.processId, {
                                expires: 30,
                            });
                            this.$router
                                .push({ path: this.redirect || "/sopView" })
                                .catch(() => {});
                        })
                        .catch(() => {
                            this.loading = false;
                        });
                }
            });
        },
    },
    mounted() {
        if (this.$route.query.mac) {
            localStorage.setItem("mac", this.$route.query.mac);
        }
    },
};
</script>

<style rel="stylesheet/scss" lang="scss" scoped>
.login {
    display: flex;
    // justify-content: center;
    align-items: center;
    height: 100%;
    background-image: url("../assets/images/background.png");
    background-size: cover;
}

.title {
    margin: 0px auto 30px auto;
    font-size: 32px;
    font-weight: bold;
    text-align: center;
    color: #fff;
}

.login-form {
    border-radius: 6px;
    background: #001b5ca1;
    margin-left: 60%;
    /* 需要做自适应 */
    width: 500px;
    height: 580px;
    display: flex;
    flex-direction: column;
    justify-content: space-around;
    box-shadow: 2px 2px 10px #06c;
    padding: 25px 25px 5px 25px;

    .el-input {
        height: 38px;
        background: transparent;

        input {
            height: 38px;
            background: transparent;
        }
    }

    .input-icon {
        height: 39px;
        width: 14px;
        margin-left: 2px;
    }
}

.login-tip {
    font-size: 13px;
    text-align: center;
    color: #bfbfbf;
}

.login-code {
    width: 33%;
    height: 38px;
    float: right;

    img {
        cursor: pointer;
        vertical-align: middle;
    }
}

.el-login-footer {
    height: 40px;
    line-height: 40px;
    position: fixed;
    bottom: 0;
    width: 100%;
    text-align: center;
    color: #fff;
    // font-family: Arial;
    font-size: 12px;
    letter-spacing: 1px;
}

.login-code-img {
    height: 38px;
}
</style>
<style lang="scss" scoped>
::v-deep .el-input__icon {
    color: #000;
}

::v-deep .el-select {
    width: 100%;
}

::v-deep .el-select .el-input .el-select__caret {
    color: #8cabff;
}

::v-deep .el-form {
    .el-form-item {
        display: flex;
    }

    .el-form-item__label {
        color: #fff;
    }

    .el-form-item__content {
        flex: 1;
        margin-left: 0 !important;
    }
}

::v-deep .el-input__inner {
    background: transparent;
    color: #fff;
    border-color: #3a67e26c;
}

::v-deep .el-input .el-input__inner:focus {
    border-color: #409eff !important;
}

::v-deep {
    .el-input__inner:-internal-autofill-previewed,
    .el-input__inner:-internal-autofill-selected {
        -webkit-text-fill-color: #000 !important;
        transition: background-color 5000s ease-in-out 0s !important;
    }
}

.fixedLogo {
    width: 250px;
    height: 80px;
    top: 10px;
    right: 20px;
    position: absolute;

    img {
        width: 100%;
    }
}
</style>
