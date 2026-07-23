// utils/app-init.js
import useUserStore from '@/store/modules/user.js' // 引入你的用户 Store

let isResolved = false; // 标记是否已经解决过
let resolveFunc = null; // 存放 resolve 函数
let waitPromise = null; // 存放 Promise 实例

// 1. 初始化监听器（在 main.js 或 App.vue 中调用一次）
export function initAppGuard() {
	//const userStore = useUserStore();

	// 监听 Pinia Store 的变化
	// 当用户信息更新（即 getInfo 完成）时，自动 resolve
	useUserStore().$subscribe((mutation, state) => {
		// 这里可以根据具体字段判断，比如 state.userInfo 存在
		if (state.isLogin == true && !isResolved) {
			finishInit();
		}
	});
}

// 2. 标记初始化完成
function finishInit() {
	if (!isResolved) {
		isResolved = true;
		if (resolveFunc) {
			resolveFunc();
			resolveFunc = null; // 释放引用
		}
	}
}

// 3. 供页面调用的等待函数
export function waitForInit() {
	// 如果已经初始化过了，直接返回 resolved Promise
	if (isResolved) {
		return Promise.resolve();
	}

	// 如果还没初始化，返回等待 Promise
	if (!waitPromise) {
		waitPromise = new Promise((resolve) => {
			resolveFunc = resolve;
		});
	}
	return waitPromise;
}