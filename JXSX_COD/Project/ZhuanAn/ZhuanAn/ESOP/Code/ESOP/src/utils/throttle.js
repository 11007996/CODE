export function throttle(fn, wait, immediate) {
    let timeout;
    let previous = 0;
    return function () {
        let context = this;
        let args = arguments;
        if (immediate) {
            let now = Date.now();
            if (now - previous > wait) {
                fn.apply(context, args);
                previous = now;
            }
        } else {
            if (!timeout) {
                timeout = setTimeout(() => {
                    timeout = null;
                    fn.apply(context, args);
                }, wait);
            }
        }
    };
}