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
        >
            <h3 class="title">ESOP管理系统</h3>
            <el-form-item prop="username">
                <el-input
                    v-model="loginForm.username"
                    type="text"
                    auto-complete="off"
                    placeholder="账号"
                    clearable
                >
                    <svg-icon
                        slot="prefix"
                        icon-class="user"
                        class="el-input__icon input-icon"
                    />
                </el-input>
            </el-form-item>
            <el-form-item prop="password">
                <el-input
                    v-model="loginForm.password"
                    type="password"
                    auto-complete="off"
                    placeholder="密码"
                    clearable
                    @keyup.enter.native="handleLogin"
                >
                    <svg-icon
                        slot="prefix"
                        icon-class="password"
                        class="el-input__icon input-icon"
                    />
                </el-input>
            </el-form-item>
            <!-- <el-form-item prop="code" v-if="captchaEnabled">
                <el-input
                    v-model="loginForm.code"
                    auto-complete="off"
                    placeholder="验证码"
                    style="width: 63%"
                    @keyup.enter.native="handleLogin"
                >
                    <svg-icon
                        slot="prefix"
                        icon-class="validCode"
                        class="el-input__icon input-icon"
                    />
                </el-input>
                <div class="login-code">
                    <img
                        :src="codeUrl"
                        @click="getCode"
                        class="login-code-img"
                    />
                </div>
            </el-form-item> -->
            <el-form-item class="flexItem">
                <el-checkbox
                    v-model="loginForm.rememberMe"
                    style="margin: 0px 0px 25px 0px"
                    >记住密码</el-checkbox
                >
                <el-link type="primary" @click="passwordFree">免密登录</el-link>
            </el-form-item>

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
                <div style="float: right" v-if="register">
                    <router-link class="link-type" :to="'/register'"
                        >立即注册</router-link
                    >
                </div>
            </el-form-item>
        </el-form>
        <!--  底部  -->
        <div class="el-login-footer">
            <!-- <span>Copyright © 2018-2022 ruoyi.vip All Rights Reserved.</span> -->
        </div>
        <div class="copyRight">
            ©{{ year }} Luxshare-ict 智能制造平台推广部提供技术支持
        </div>
    </div>
</template>

<script>
// import { getCodeImg } from "@/api/login";
import Cookies from "js-cookie";
import { encrypt, decrypt } from "@/utils/jsencrypt";

export default {
    name: "Login",
    data() {
        return {
            year: new Date().getFullYear(),
            codeUrl: "",
            loginForm: {
                username: "",
                password: "",
                rememberMe: false,
                code: "",
                uuid: "",
            },
            loginRules: {
                username: [
                    {
                        required: true,
                        trigger: "blur",
                        message: "请输入您的账号",
                    },
                ],
                password: [
                    {
                        required: true,
                        trigger: "blur",
                        message: "请输入您的密码",
                    },
                ],
                code: [
                    {
                        required: true,
                        trigger: "change",
                        message: "请输入验证码",
                    },
                ],
            },
            loading: false,
            // 验证码开关
            captchaEnabled: true,
            // 注册开关
            register: false,
            redirect: undefined,
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
        this.getCookie();
    },
    mounted() {
        if (this.$route.query.mac) {
            localStorage.setItem("mac", this.$route.query.mac);
        }
    },
    methods: {
        // getCode() {
        //     getCodeImg().then((res) => {
        //         this.captchaEnabled =
        //             res.captchaEnabled === undefined
        //                 ? true
        //                 : res.captchaEnabled;
        //         if (this.captchaEnabled) {
        //             this.codeUrl = "data:image/gif;base64," + res.img;
        //             this.loginForm.uuid = res.uuid;
        //         }
        //     });
        // },
        passwordFree() {
            this.$router.push({ path: "/passwordFree" });
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
                    if (this.loginForm.rememberMe) {
                        Cookies.set("username", this.loginForm.username, {
                            expires: 30,
                        });
                        Cookies.set(
                            "password",
                            encrypt(this.loginForm.password),
                            { expires: 30 }
                        );
                        Cookies.set("rememberMe", this.loginForm.rememberMe, {
                            expires: 30,
                        });
                    } else {
                        Cookies.remove("username");
                        Cookies.remove("password");
                        Cookies.remove("rememberMe");
                    }
                    this.$store
                        .dispatch("Login", this.loginForm)
                        .then(() => {
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
};
</script>

<style rel="stylesheet/scss" lang="scss">
.login {
    display: flex;
    justify-content: center;
    align-items: center;
    height: 100%;
    background-image: url("../assets/images/login-background.png");
    background-size: cover;
    position: relative;
    .copyRight {
        font-size: 12px;
        color: #000000;
        position: absolute;
        bottom: 10px;
    }
}
.title {
    margin: 0px auto 30px auto;
    font-size: 32px;
    font-weight: bold;
    text-align: center;
    color: #000;
}

.login-form {
    border-radius: 6px;
    background: #ffffffab;
    width: 400px;
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
::v-deep .el-input__inner {
    background: transparent;
    color: #000;
    border-color: #000;
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
::v-deep .flexItem {
    display: flex;
    .el-form-item__content {
        flex: 1;
        display: flex;
        justify-content: space-between;
        align-items: center;
    }
    .el-checkbox {
        margin: 0 !important;
    }
    .el-form-item__content::before,
    .el-form-item__content::after {
        content: unset;
    }
}
.fixedLogo {
    width: 250px;
    height: 80px;
    top: 10px;
    left: 20px;
    position: absolute;
    img {
        width: 100%;
    }
}
</style>
