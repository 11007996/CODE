<template>
	<view>
		求请错误，原因：{{msg}}
	</view>
</template>

<script>
	export default {
		data() {
			return {
				msg: ''
			}
		},
		onLoad(options) {
			this.msg = options.msg;
		},
		methods: {

		}
	}
</script>

<style scoped>
	view {
		font-size: 48rpx;
		text-align-last: center;
	}
</style>