<template>
    <div class="app-container"></div>
</template>

<script>
import Cookies from "js-cookie";
export default {
    name: "Index",
    data() {
        return {
            show: false,
            // 版本号
            version: "3.8.3",
        };
    },
    methods: {
        goTarget(href) {
            window.open(href, "_blank");
        },
    },
    created() {
        this.$router.push({ path: "/sopView" }).catch(() => {});
    },
    mounted() {
        console.log("✅ 我是最新代码！") // 加这行
        // this.connectWs()
    },
};
</script>

<style lang="scss" scoped>
.msgBox {
    padding: 10px;
    width: 90%;
    background: rgba($color: #0b97d8c4, $alpha: 0.8);
    background-image: url("../assets/images/Messages.png");
    background-size: 150px;
    margin: auto;
    margin-bottom: 10px;
    color: white;
    text-shadow: 2px 2px 4px #000000;

    div {
        margin-bottom: 22px;
    }
    .titleDiv {
        font-size: 44px;
    }
    .mainBox {
        font-size: 22px;
        text-indent: 44px;
    }
    .writeBox {
        font-size: 22px;
        text-align: right;
    }
}
.app-container {
    display: flex;
    flex-direction: column;
    align-items: center;
}
</style>
