<template>
    <div id="container">
        <!-- 上一页、下一页 -->
        <div class="right-btn">
            <!-- 输入页码 -->
            <div class="pageNum">
                <input
                    v-model.number="currentPage"
                    type="number"
                    class="inputNumber"
                    @input="inputEvent()"
                />
                / {{ pageCount }}
            </div>

            <div @click="changePdfPage('first')" class="turn">首页</div>
            <!-- 在按钮不符合条件时禁用 -->
            <div
                @click="changePdfPage('pre')"
                class="turn-btn"
                :style="
                    currentPage === pdfPages[0] ? 'cursor: not-allowed;' : ''
                "
            >
                上一页
            </div>
            <div
                @click="changePdfPage('next')"
                class="turn-btn"
                :style="
                    currentPage === pdfPages[pdfPages.length - 1]
                        ? 'cursor: not-allowed;'
                        : ''
                "
            >
                下一页
            </div>
            <div @click="changePdfPage('last')" class="turn">尾页</div>

            <!-- <div @click="changeScreenfull()" class="turn">全屏</div> -->
            <!-- <div class="turn">
                <screenfull id="screenfull" class="right-menu-item hover-effect" @click="Screenfull" >全屏</screenfull>
            </div> -->
            <!-- <div @click="changePdfPage('last')" class="turn">关闭</div> -->
        </div>
        <div class="pdfArea">
            <!-- // 不要改动这里的方法和属性,下次用到复制就直接可以用 -->
            <!-- <iframe :src="src" frameborder="0" width="100%" height="100%" ></iframe> -->
            <pdf
                :src="src"
                ref="pdf"
                v-show="loadedRatio === 1"
                :page="currentPage"
                @num-pages="pageCount = $event"
                @progress="loadedRatio = $event"
                @page-loaded="onPageLoaded"
                @loaded="loadPdfHandler"
                @link-clicked="currentPage = $event"
                id="pdfID"
                @password="password"
            />
            <div class="fixButton r20" @click="closeDialog">
                <i class="el-icon-circle-close"></i>
            </div>
            <el-tooltip
                class="item"
                effect="dark"
                :content="content"
                placement="top-start"
            >
                <div class="fixButton r60" @click="setTop">
                    <svg-icon
                        slot="prefix"
                        :icon-class="iconClass"
                        class="input-icon"
                    />
                </div>
            </el-tooltip>
            <el-tooltip
                class="item"
                effect="dark"
                content="分割屏幕"
                placement="top-start"
            >
                <div class="fixButton r100" @click="splitScreen">
                    <svg-icon
                        slot="prefix"
                        icon-class="splitScreen"
                        class="input-icon"
                    />
                </div>
            </el-tooltip>

            <div class="scaleInput">
                <el-input-number
                    size="mini"
                    v-model="scale"
                    :step="5"
                    @change="changeScale"
                    :min="70"
                    :max="150"
                />
            </div>
        </div>
        <!-- 加载未完成时，展示进度条组件并计算进度 -->
        <div class="progress" v-if="loadedRatio != 1">
            <el-progress
                type="circle"
                :width="70"
                color="#53a7ff"
                :percentage="
                    Math.floor(loadedRatio * 100)
                        ? Math.floor(loadedRatio * 100)
                        : 0
                "
            />
            <br />
            <!-- 加载提示语 -->
            <span>{{ remindShow }}</span>
        </div>
    </div>
</template>
<script>
import pdf from "vue-pdf";
import Screenfull from "@/components/Screenfull";
export default {
    name: "VuePDF",
    props: {
        pdfUrl: {
            type: String,
        },
        pdfPages: {
            type: Array,
            default() {
                return [];
            },
        },
        pdfInterval: {
            type: String,
            default: "110",
        },
        pdfSizeList: {
            type: Array,
            default() {
                return [];
            },
        },
        pdfPassword: String,
    },
    components: {
        pdf,
        Screenfull,
    },
    data() {
        return {
            iconClass: "setTop",
            content: "将PDF置顶",
            // ----- loading -----
            remindText: {
                loading: "加载文件中，文件较大请耐心等待...",
                refresh: "若卡住不动，可刷新页面重新加载...",
            },
            remindShow: "加载文件中，文件较大请耐心等待...",
            intervalID: "",
            // ----- vuepdf -----
            // src静态路径: /static/xxx.pdf
            // src服务器路径: 'http://.../xxx.pdf'
            src: "",
            // 当前页数
            currentPage: 0,
            // 总页数
            pageCount: 0,
            // 加载进度
            loadedRatio: 0,
            is_PDF_top: false,
            isSplitScreen: false,
            //缩放
            scale: 100,
            pdfIntervalID: null,
        };
    },

    created() {
        // this.src = pdf.createLoadingTask({
        //     url: this.pdfUrl,
        //     cMapUrl: "https://cdn.jsdelivr.net/npm/pdfjs-dist@2.5.207/cmaps/",
        //     cMapPacked: true,
        // });
        this.src = this.pdfUrl;
    },
    mounted() {
        // // 更改 loading 文字
        this.intervalID = setInterval(() => {
            this.remindShow === this.remindText.refresh
                ? (this.remindShow = this.remindText.loading)
                : (this.remindShow = this.remindText.refresh);
        }, 4000);
    },
    methods: {
        initPdfWidth() {
            let pdf_width = this.pdfSizeList[this.currentPage - 1].width;
            let pdf_height = this.pdfSizeList[this.currentPage - 1].height;
            this.$nextTick(() => {
                let boxHeight = document.querySelector(".pdfArea").clientHeight;
                let boxWidth = document.querySelector(".pdfArea").clientWidth;
                let parentEl = document.querySelector("#pdfID");
                let el = document.querySelector("#pdfID canvas");
                if (pdf_height / pdf_width > boxHeight / boxWidth) {
                    parentEl.style.height = boxHeight + "px";
                    parentEl.style.width =
                        boxHeight * (pdf_width / pdf_height) + "px";
                    el.style.height = boxHeight + "px";
                    el.style.width =
                        boxHeight * (pdf_width / pdf_height) + "px";
                    parentEl.style.left =
                        (boxWidth - boxHeight * (pdf_width / pdf_height)) / 2 +
                        "px";
                    el.style.left =
                        (boxWidth - boxHeight * (pdf_width / pdf_height)) / 2 +
                        "px";
                } else {
                    if (pdf_width > boxWidth) {
                        parentEl.style.width = boxWidth + "px";
                        el.style.width = boxWidth + "px";
                        parentEl.style.height =
                            boxWidth * (pdf_height / pdf_width) + "px";
                        el.style.height =
                            boxWidth * (pdf_height / pdf_width) + "px";
                    } else {
                        parentEl.style.width = pdf_width + "px";
                        el.style.width = pdf_width + "px";
                        parentEl.style.height = pdf_height + "px";
                        el.style.height = pdf_height + "px";
                    }
                }
            });
        },
        onPageLoaded(e){
            this.currentPage=e;
            this.initPdfWidth();
        },
        password(updatePassword, reason) {
            updatePassword(this.pdfPassword);
        },
        intervalPDF() {
            if (this.pdfIntervalID) {
                clearInterval(this.pdfIntervalID);
            } else {
                this.pdfIntervalID = setInterval(() => {
                    if (this.pdfPages.length === 0) {
                        if (this.currentPage < this.pageCount) {
                            this.changePdfPage("next");
                        } else if (this.currentPage === this.pageCount) {
                            this.changePdfPage("first");
                        } else {
                            this.changePdfPage("pre");
                        }
                    } else {
                        let pageIndex = this.pdfPages.indexOf(this.currentPage);
                        if (this.pdfPages[pageIndex + 1]) {
                            this.changePdfPage("next");
                        } else {
                            if (pageIndex === this.pdfPages.length - 1) {
                                this.currentPage = this.pdfPages[0];
                            }
                        }
                    }
                }, this.pdfInterval * 1000);
            }
        },
        changeScale(val) {
            // this.$refs.pdf.$el.style.width = parseInt(val) + "%";
            // this.$refs.pdf.$el.style.height = parseInt(val) + "%";
            this.$refs.pdf.$el.style.transform = `scale(${parseFloat(
                val / 100
            )},${parseFloat(val / 100)})`;
            this.$refs.pdf.$el.style.transformOrigin = "0% 0%";
        },
        setTop() {
            this.is_PDF_top = !this.is_PDF_top;
            this.$emit("setPDF", this.is_PDF_top);
        },
        // 页面回到顶部
        toTop() {
            document.getElementById("container").scrollTop = 0;
        },
        // 输入页码时校验
        inputEvent() {
            // 如果输入的页码不在配置的页码内，返回配置页码的第一页
            if (this.pdfPages.indexOf(this.currentPage) == -1) {
                this.currentPage =
                    this.pdfPages.length === 0 ? 1 : this.pdfPages[0];
            }
            if (this.currentPage > this.pageCount) {
                // 1. 大于max
                this.currentPage = this.pageCount;
            } else if (this.currentPage < 1) {
                // 2. 小于min
                this.currentPage = 1;
            }
        },
        // 切换页数
        changePdfPage(val) {
            let noSopPages = this.pdfPages.length === 0;
            if (val === "pre") {
                if (noSopPages) {
                    if (this.currentPage == 1) {
                        this.currentPage = 1;
                    } else {
                        this.currentPage -= 1;
                    }
                } else {
                    let pageIndex = this.pdfPages.indexOf(this.currentPage);
                    if (pageIndex != 0) {
                        this.currentPage = this.pdfPages[pageIndex - 1];
                    } else {
                        this.currentPage = this.pdfPages[pageIndex];
                    }
                }
                this.toTop();
            } else if (val === "next" && this.currentPage < this.pageCount) {
                if (noSopPages) {
                    this.currentPage += 1;
                } else {
                    let pageIndex = this.pdfPages.indexOf(this.currentPage);
                    if (pageIndex != this.pdfPages.length - 1) {
                        this.currentPage = this.pdfPages[pageIndex + 1];
                    } else {
                        this.currentPage = this.pdfPages[pageIndex];
                    }
                    this.toTop();
                }
            } else if (val === "first") {
                this.currentPage = noSopPages ? 1 : this.pdfPages[0];
                this.toTop();
            } else if (val === "last" && this.currentPage < this.pageCount) {
                this.currentPage = noSopPages
                    ? this.pageCount
                    : this.pdfPages[this.pdfPages.length - 1];
                this.toTop();
            }
        },
        // pdf加载时
        loadPdfHandler(e) {
            // 加载的时候先加载第一页
            this.currentPage = this.pdfPages[0];
            let timeoutId = null;
            if (timeoutId) {
                clearTimeout(timeoutId);
            } else {
                timeoutId = setTimeout(() => {
                    this.intervalPDF();
                }, this.pdfInterval * 1000);
            }
        },
        splitScreen() {
            this.isSplitScreen = !this.isSplitScreen;
            this.$emit("splitScreen", this.isSplitScreen);
        },
        changeScreenfull() {
            Screenfull();
        },
        closeDialog() {
            this.$emit("close_PDF_dialog");
            clearInterval(this.intervalID);
            clearInterval(this.pdfIntervalID);
            this.pdfIntervalID = null;
        },
    },
    destroyed() {
        // 在页面销毁时记得清空 setInterval
        clearInterval(this.intervalID);
        clearInterval(this.pdfIntervalID);
    },
    watch: {
        pdfPages: {
            handler(val, val2) {
                this.currentPage = val.length === 0 ? 1 : val[0];
            },
            deep: true,
            immediate: true,
        },
        pdfUrl: {
            deep: true,
            immediate: true,
            handler(val) {
                if (val) {
                    console.log(val);
                    this.src = val;
                }
            },
        },
        is_PDF_top: {
            immediate: true,
            handler(newVal, oldVal) {
                if (newVal) {
                    this.iconClass = "setBottom";
                    this.content = "取消PDF置顶";
                } else {
                    this.iconClass = "setTop";
                    this.content = "将PDF置顶";
                }
            },
        },
    },
};
</script>
<style scoped lang="scss">
#container {
    position: absolute !important;
    left: 0;
    right: 0;
    bottom: 0;
    top: 0;
    background: #f4f7fd;
    height: 100%;
    // font-family: PingFang SC;
    width: 100%;
    display: flex;
    /* justify-content: center; */
    position: relative;
}

/* 右侧功能按钮区 */
.right-btn {
    position: absolute;
    background: #cccccc38;
    right: 10px;
    bottom: 15%;
    width: 100px;
    display: flex;
    flex-wrap: wrap;
    justify-content: center;
    z-index: 99;
}

.pdfArea {
    position: absolute;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    margin: auto;
    //margin-top: 30px;

}

/* ------------------- 输入页码 ------------------- */
.pageNum {
    margin: 10px 0;
    font-size: 18px;
    color: #000;
}

/*在谷歌下移除input[number]的上下箭头*/
input::-webkit-outer-spin-button,
input::-webkit-inner-spin-button {
    -webkit-appearance: none !important;
    margin: 0;
}

/*在firefox下移除input[number]的上下箭头*/
// input[type="number"] {
//     -moz-appearance: textfield;
// }

.inputNumber {
    border-radius: 8px;
    border: 1px solid #999999;
    height: 30px;
    background: #1d53c861;
    color: #fff;
    font-size: 18px;
    width: 40px;
    text-align: center;
}

.inputNumber:focus {
    border: 1px solid #00aeff;
    background-color: rgba(18, 163, 230, 0.096);
    outline: none;
    transition: 0.2s;
}

/* ------------------- 切换页码 ------------------- */
.turn {
    background-color: #164fcc;
    opacity: 0.9;
    color: #ffffff;
    height: 54px;
    width: 54px;
    border-radius: 50%;
    display: flex;
    align-items: center;
    justify-content: center;
    margin: 5px 0;
}

.turn-btn {
    background-color: #164fcc;
    opacity: 0.9;
    color: #ffffff;
    height: 54px;
    width: 54px;
    border-radius: 50%;
    margin: 5px 0;
    display: flex;
    align-items: center;
    justify-content: center;
}

.turn-btn:hover,
.turn:hover {
    transition: 0.3s;
    opacity: 0.5;
    cursor: pointer;
}

/* ------------------- 进度条 ------------------- */
.progress {
    position: absolute;
    right: 50%;
    top: 50%;
    text-align: center;
}

.progress > span {
    color: #199edb;
    font-size: 14px;
}
.fixButton {
    cursor: pointer;
    position: absolute;
    top: 0;
    width: 30px;
    height: 30px;
    font-size: 30px;
}
::v-deep .scaleInput {
    position: absolute;
    top: 7px;
    right: 140px;
}
.r20 {
    right: 20px;
}
.r60 {
    right: 60px;
}
.r100 {
    right: 100px;
}
.r140 {
    right: 140px;
}
.r180 {
    right: 180px;
}
</style>
