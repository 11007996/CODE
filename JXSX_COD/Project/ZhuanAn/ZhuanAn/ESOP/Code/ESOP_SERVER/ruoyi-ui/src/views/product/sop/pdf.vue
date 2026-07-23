<template>
    <div>
        <screenfull id="screenfull" class="right-menu-item hover-effect" />
        <pdf ref="pdf" v-for="i in numPages" :key="i" :src="pdfUrl" :page="i"></pdf>
    </div>
</template>

<script>
import Screenfull from "@/components/Screenfull";
import pdf from "vue-pdf";
export default {
    name: "VuePDF",
    props: {
        pdfUrl: {
            type: String,
            default: ""
        }
    },
    components: {
        pdf,
        Screenfull
    },

    data() {
        return {
            numPages: null
        };
    },
    watch: {
        pdfUrl: {
            deep: true,
            immediate: true,
            handler(val) {
                if (val) {
                    this.getNumPages(val);
                }
            }
        }
    },
    mounted() {},

    methods: {
        getNumPages() {
            console.log(this.pdfUrl);
            let loadingTask = pdf.createLoadingTask(this.pdfUrl);
            loadingTask.promise.then(pdf => {
                this.numPages = pdf.numPages;
            });
        }
    }
};
</script>