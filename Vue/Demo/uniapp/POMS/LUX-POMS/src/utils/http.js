// showToast如果为false，产生的异常由对应接口处理
let showToast = true

// 添加请求拦截
uni.addInterceptor('request', {
  invoke(req) {

	  let BASE_URL=''
	  if(process.env.NODE_ENV === 'development') {
		  // #ifdef APP-PLUS
			if(req.url.startsWith('/api')) {
				BASE_URL = 'http://localhost:5018';
			}
		  // #endif
		  // #ifdef H5
				BASE_URL = ''
		  // #endif
	  } else {
		  if(req.url.startsWith('/api')) {
		  		 BASE_URL = 'http://localhost:5018';
		  }
	  }
	  req.url = BASE_URL + req.url;

  //  console.log(req)
  },
  success(res) {
    if (!res || !res?.data) {
      uni.showToast({
        title: '请求失败',
        duration: 3000,
        icon: 'none',
      });
      return;
    }
    const data = res?.data;

	let flag = null
	if(data?.status === 0) {
		flag = true
	}else {
		flag = false
	}
	const msg = data.msg
    if (!flag) {
      if(data?.status.toString().startsWith(1))
      {
        uni.showModal({
            title: '登录失效提醒',
            content: data?.msg,
            success(res) {
              if (res.confirm) {
                uni.reLaunch({
                  url: '/pages/login/login',
                });
              }
            },
          });
      }
      switch (data?.status) {
        case 103:
          uni.showModal({
            title: '登录失效提醒',
            content: '当前登录已失效，需要重新登录',
            success(res) {
              if (res.confirm) {
                uni.reLaunch({
                  url: '/pages/login/login',
                });
              }
            },
          });
          break;
        default:
		// if(showToast) {
    //       uni.showToast({
    //         title: msg,
    //         duration: 5000,
    //         icon: 'none',
    //       });
		// }
        if(res.statusCode!=200) {
              uni.showToast({
                title: data.msg,
                duration: 5000,
                icon: 'none',
              });
		    }
          break;
      }
    }
  },
  fail(err) {
    uni.showToast({
      title: err.errMsg,
      duration: 5000,
      icon: 'none',
    });
  },
  complete(res) {
    // 无论如何都清理标识
    if (res?.config) {
      const requestKey = `${res.config.url}_${JSON.stringify(res.config.data)}`
      pendingRequests.delete(requestKey)
    }
  },
  returnValue(res) {
      if (!isPromise(res)) {
      return res;
    }
    return new Promise((resolve, reject) => {
      res.then((res) => {
        if (res.statusCode!=200) {
          reject(res);
        } else {
          resolve(res);
        }
      });
    });
  }
});

function isPromise(obj) {
  return (
    !!obj &&
    (typeof obj === 'object' || typeof obj === 'function') &&
    typeof obj.then === 'function'
  );
}

export default function http(options) {
  const token = uni.getStorageSync('token');
  if (token) {
    options.header = {
      ...options.header,
      Authorization: 'Bearer ' + token,
    };
  }
  // if(options.toast === false) {
	//   showToast = false
  // }else {
	//   showToast = true
  // }
  return uni.request(options);
}
