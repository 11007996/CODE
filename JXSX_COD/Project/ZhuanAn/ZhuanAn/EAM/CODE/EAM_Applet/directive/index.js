import hasRole from './permission/hasRole'
import hasPermi from './permission/hasPermi'
import drag from './module/drag'
import waves from './module/waves'

export default function directive(app) {
	app.directive('hasRole', hasRole)
	app.directive('hasPermi', hasPermi)
	app.directive('drag', drag)
	app.directive('waves', waves)
}