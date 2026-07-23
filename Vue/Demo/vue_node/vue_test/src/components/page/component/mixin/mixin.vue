<template>
	<h1>混入</h1>
	<p>{{message}}</p>
</template>
<script>
	import {mixinMsg} from './mixin.js'
	export default{
		mixins:[mixinMsg]
	}
</script>