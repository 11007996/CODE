<template>
	<view class="content">
		<video id="video-canvas" autoplay :controls="false"></video>
	</view>
</template>

<script>
	export default {
		data() {
			return {
				codeReader: null,
				deviceId: null,
				videoInputDevice: []
			}
		},
		onLoad() {
			this.initEvent();
		},
		mounted() {
			const video = document.getElementById('video-canvas').getElementsByTagName('video')[0]
			video.setAttribute('id', 'video_id')
			video.setAttribute('class', 'video_class')
		},
		methods: {
			initEvent() {
				this.codeReader = new BrowserMultiFormatReader();
				this.codeReader.listVideoInputDevices().then(res => {
					if (res.length) {
						this.videoInputDevice = res
						this.deviceId = res[1].deviceId
					}
					this.scanCode();
				}).catch((err) => {
					uni.showModal({
						title: '提示',
						content: '当前浏览器环境不支持获取视频通道',
						showCancel: false
					});
				})
			},
			scanCode() {
				try {
					this.codeReader.decodeFromVideoDevice(this.deviceId, 'video_id', (res, err) => {
						if (res) {
							uni.showModal({
								title: '扫码结果',
								content: JSON.stringify(res)
							})
						}
						if (err) {
							uni.showModal({
								title: '扫码失败结果',
								content: JSON.stringify(err)
							})
						}
					})
				} catch (e) {
					uni.showToast({
						title: `初始化失败${e}`,
						icon: 'none'
					})
				}
			}
		}
	}
</script>

<style>

</style>