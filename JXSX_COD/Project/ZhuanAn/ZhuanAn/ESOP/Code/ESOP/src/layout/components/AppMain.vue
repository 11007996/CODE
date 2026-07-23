<template>
    <section class="app-main">
        <transition name="fade-transform" mode="out-in">
            <keep-alive :include="cachedViews">
                <router-view v-if="!$route.meta.link" :key="key" />
            </keep-alive>
        </transition>
        <iframe-toggle />
        <div class="bottomCopyRight">
            <div class="department">
                <svg-icon
                    slot="prefix"
                    icon-class="version"
                    class="input-icon"
                /> <span class="versionSpan">version {{ version }}</span>©{{nowYear}} Luxshare-ict
                智能制造平台推广部提供技术支持
            </div>
            <!-- <div class="sendMail">
                <p>
                    <svg-icon
                        slot="prefix"
                        icon-class="heart"
                        class="input-icon"
                    /> 联系我们：
                </p>
                <div>
                    <a href="mailto:JK.Li@luxshare-ict.com?subject=MTM问题咨询">
                        李杰:JK.Li@luxshare-ict.com
                    </a>
                    <svg-icon
                        slot="prefix"
                        icon-class="copy"
                        class="input-icon"
                        @click="copyEmail('JK.Li@luxshare-ict.com')"
                    />
                </div>

                |
                <div>
                    <a
                        href="mailto:Yan-A.Liu@luxshare-ict.com?subject=MTM问题咨询"
                        >刘艳:Yan-A.Liu@luxshare-ict.com</a
                    >
                    <svg-icon
                        slot="prefix"
                        icon-class="copy"
                        class="input-icon"
                        @click="copyEmail('Yan-A.Liu@luxshare-ict.com')"
                    />
                </div>

                |
                <div>
                    <a
                        href="mailto:Xiaoqiang.Xiao@luxshare-ict.com?subject=MTM问题咨询"
                        >肖晓强:Xiaoqiang.Xiao@luxshare-ict.com</a
                    >
                    <svg-icon
                        slot="prefix"
                        icon-class="copy"
                        class="input-icon"
                        @click="copyEmail('Xiaoqiang.Xiao@luxshare-ict.com')"
                    />
                </div>
            </div> -->
        </div>
    </section>
</template>

<script>
import iframeToggle from "./IframeToggle/index";
import { removeWatermark, setWaterMark } from "@/utils/watermark";
// import Cookies from "js-cookie";
import { throttle } from "@/utils/throttle";
export default {
    name: "AppMain",
    components: { iframeToggle },
    computed: {
        cachedViews() {
            return this.$store.state.tagsView.cachedViews;
        },
        key() {
            return this.$route.path;
        },
    },
    data() {
        return {
            version: "",
            nowYear:new Date().getFullYear()
        };
    },
    mounted() {
        setWaterMark(localStorage.getItem("userName"), localStorage.getItem("nickName"));
        window.onresize = throttle(
            function () {
                setWaterMark(
                    localStorage.getItem("userName"),
                    localStorage.getItem("nickName")
                );
            },
            300,
            true
        );
        this.version = process.env.VUE_APP_VERSION;
    },
    methods: {
        // 复制邮箱
        copyEmail(val) {
            this.$copyText(val).then(
                (success) => {
                    this.$modal.msgSuccess("已将邮箱复制到剪切板");
                },
                (error) => {
                    this.$modal.msgError("复制失败，错误代码" + error);
                }
            );
        },
    },
    destroyed() {
        removeWatermark();
    },
};
</script>

<style lang="scss" scoped>
.app-main {
    background: #f2f4f7;
    /* 50= navbar  50  */
    min-height: calc(100vh - 50px);
    width: 100%;
    position: relative;
    overflow: hidden;
}

.fixed-header + .app-main {
    padding-top: 50px;
}

.hasTagsView {
    .app-main {
        /* 84 = navbar + tags-view = 50 + 34 */
        min-height: calc(100vh - 84px);
        padding-bottom: 60px;
    }

    .fixed-header + .app-main {
        padding-top: 84px;
    }
}
</style>

<style lang="scss">
// fix css style bug in open el-dialog
.el-popup-parent--hidden {
    .fixed-header {
        padding-right: 17px;
    }
}
@media screen and (max-width:1400px) {
    .bottomCopyRight{
        flex-direction: column;
    }
}
@media screen and (min-width:1401px) {
    .bottomCopyRight{
        flex-direction: row;
    }
}
.bottomCopyRight {
    width: 100%;
    position: absolute;
    bottom: 0;
    font-size: 12px;
    height: 60px;
    display: flex;
    color: #a0aec0;
    justify-content: space-around;
    align-items: center;
    .sendMail {
        height: 40px;
        line-height: 40px;
        display: flex;
        justify-content: space-around;
        align-items: center;
        div {
            margin: 0 10px;
        }
        a {
            color: #3182ce;
        }
        svg {
            margin-left: 5px;
            font-size: 14px;
            cursor: pointer;
        }
        p svg {
            font-size: 12px;
        }
    }
    .versionSpan{
        margin-right: 20px;
    }
}
</style>
