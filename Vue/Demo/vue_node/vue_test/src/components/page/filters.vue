<template>
	<h1>过滤器</h1>
	<p>{{ number | greaterThanTen | convertToInteger}}</p>
	
	<hr>
</template>

<script>
export default {
	data() {
		return {
			number: 10,
		};
	},
	filters: {
		greaterThanTen:function(val) { // 过滤器名字更具描述性
			if (val > 10) {
				return true;
			} else {
				return false;
			}
		},
		convertToInteger:function(b) {
			if (b) {
				return 1;
			} else {
				return 0;
			}
		},
	},
};
</script>