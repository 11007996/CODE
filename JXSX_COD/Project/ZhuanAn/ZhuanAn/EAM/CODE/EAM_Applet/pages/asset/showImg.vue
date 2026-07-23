<template>
	<div id="img-container">
		<ul>
			<li v-for="(item, index) of imgArr" :key="index">
				<img :src="item.url" :alt="item.fileAliasName" @click="viewerUpdate" />
			</li>
		</ul>
	</div>
</template>

<script setup>
	import { ref, reactive, onMounted, getCurrentInstance } from 'vue';
	import Viewer from 'viewerjs';
	import 'viewerjs/dist/viewer.css';
	import {
		downloadFile,
		getPreviewFilesApi
	} from '@/api/equipment/file.js'
	let viewer;

	// export default {

	// 	// 页面周期函数--监听页面加载
	// 	onLoad(option) {
	// 		this.sourceFileId = option.fileId;
	// 		this.getPreviewFiles();
	// 		//this.downloadPreview();
	// 	},
	// 	mounted() {

	// 	},
	// };
	const curInstance = getCurrentInstance()
	const sourceFileId = ref('')
	const imgArr = ref([{
		fileId: '',
		fileAliasName: '',
		url: ''
	}])

	//获取缩略图信息
	function getPreviewFiles() {
		getPreviewFilesApi({
			'sourceFileId': sourceFileId.value,
			'previewType': 1,
			'hideLoading': false
		}).then(res => {
			imgArr.value = res.data;
			if (res) {
				for (var i = 0; i < res.length; i++) {
					this.downloadPreview(i);
					//res[i]["url"] =  `/File/DownloadPreview?fileId=${res[i].fileId}`;
				}
			}
			//	this.imgArr = res;
			this.viewerUpdate();
		});
	}

	//下载预览图片到本地
	function downloadPreview(index) {
		let fileId = this.imgArr[index].fileId;
		downloadFile({
			url: `/File/DownloadPreview?fileId=${fileId}`
		}).then(res => {
			this.imgArr[index]["url"] = res;
		});
	}

	//解决异步加载图片，导致viewer无法预览的问题。更新viewer的状态
	function viewerUpdate() {
		viewer.update();
	}

	onMounted(() => {
		if (curInstance.attrs.fileId) {
			sourceFileId.value = curInstance.attrs.fileId
		}
		getPreviewFiles()

		const ViewerDom = document.getElementById('img-container')
		viewer = new Viewer(ViewerDom, {});
	})
</script>



<style scoped>
	* {
		padding: 0;
		margin: 0;
	}

	ul {
		display: flex;
		flex-wrap: wrap;
		justify-content: center;
	}

	ul li {
		width: 100%;
		height: auto;
		list-style: none;
		border: 2px solid #ccc;
		border-radius: 3px;
		padding: 1px;
		margin: 10px;
		cursor: pointer;
	}

	ul li img {
		width: 100%;
		height: 100%;
	}
</style>