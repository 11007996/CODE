import Vue from "vue";

import Cookies from "js-cookie";

import Element from "element-ui";
import "./assets/styles/element-variables.scss";

import "@/assets/styles/index.scss"; // global css
import "@/assets/styles/ruoyi.scss"; // ruoyi css
import App from "./App";
import store from "./store";
import router from "./router";
import directive from "./directive"; // directive
import plugins from "./plugins"; // plugins
import { download } from "@/utils/request";

import AFTableColumn from 'af-table-column'
Vue.use(AFTableColumn);

import VueCliboard from "vue-clipboard2";
Vue.use(VueCliboard);
import "./assets/icons"; // icon
import "./permission"; // permission control
import { getDicts } from "@/api/system/dict/data";
import { getConfigKey } from "@/api/system/config";
import {
    parseTime,
    parseTableTime,
    resetForm,
    addDateRange,
    selectDictLabel,
    selectDictLabels,
    handleTree,
} from "@/utils/ruoyi";
import elDragDialog from "./dialog";
Vue.directive("el-drag-dialog", elDragDialog); //添加标签事件绑定 可以放大移动弹窗
//弹窗默认点击遮罩改为不关闭 为了防止和拖拽冲突 ，这句需要放在use ElementUI之前（也可以不加这句，自己测试区别）
Element.Dialog.props.closeOnClickModal.default = false;
// 分页组件
import Pagination from "@/components/Pagination";
// 自定义表格工具组件
import RightToolbar from "@/components/RightToolbar";
// 富文本组件
import Editor from "@/components/Editor";
// 文件上传组件
import FileUpload from "@/components/FileUpload";
// 图片上传组件
import ImageUpload from "@/components/ImageUpload";
// 图片预览组件
import ImagePreview from "@/components/ImagePreview";
// 字典标签组件
import DictTag from "@/components/DictTag";
// 头部标签组件
import VueMeta from "vue-meta";
// 字典数据组件
import DictData from "@/components/DictData";

import axios from "axios";
import Video from "video.js";
import "video.js/dist/video-js.css";
Vue.prototype.$video = Video;
import "videojs-flash";
import hls from "videojs-contrib-hls";
Vue.use(hls);

// 全局方法挂载
Vue.prototype.getDicts = getDicts;
Vue.prototype.getConfigKey = getConfigKey;
Vue.prototype.parseTime = parseTime;
Vue.prototype.parseTableTime = parseTableTime;
Vue.prototype.resetForm = resetForm;
Vue.prototype.addDateRange = addDateRange;
Vue.prototype.selectDictLabel = selectDictLabel;
Vue.prototype.selectDictLabels = selectDictLabels;
Vue.prototype.download = download;
Vue.prototype.handleTree = handleTree;

// 全局组件挂载
Vue.component("DictTag", DictTag);
Vue.component("Pagination", Pagination);
Vue.component("RightToolbar", RightToolbar);
Vue.component("Editor", Editor);
Vue.component("FileUpload", FileUpload);
Vue.component("ImageUpload", ImageUpload);
Vue.component("ImagePreview", ImagePreview);

// import {noDataMixins} from './mixins';
// Vue.mixin(noDataMixins);

Vue.use(directive);
Vue.use(plugins);
Vue.use(VueMeta);
DictData.install();
Element.Dialog.props.closeOnClickModal.default = false;
Element.Dialog.props.center.default = true;
Vue.use(Element, {
    size: Cookies.get("size") || "medium", // set element-ui default size
});

Vue.config.productionTip = false;
new Vue({
    el: "#app",
    router,
    store,
    render: (h) => h(App),
});
