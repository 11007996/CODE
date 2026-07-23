import modal from './modal'

export default function installPlugins(app) {
	// 模态框对象
	app.config.globalProperties.$modal = modal

	Array.prototype.showColumn = function(column) {
		var item = this.find((x) => x.prop == column)
		// console.log('showColumn方法', this, column, item)
		if (item) {
			return item.visible
		}
		return true
	}
}