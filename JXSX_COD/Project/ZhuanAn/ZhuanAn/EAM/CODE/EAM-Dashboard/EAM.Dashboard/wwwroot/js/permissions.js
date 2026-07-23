//界面权限检查
//eam-hasPermi="dashboard:02C1:equipment" eam-loginFactory="02C1"
$(document).ready(function () {
    userStore.getUser((userInfo) => {
        if (!userInfo) return;
        //对应厂区菜单
        let loginFactoryId = userInfo.factory.factoryId
        let lfEles = $('*[eam-loginFactory][eam-loginFactory!="' + loginFactoryId + '"]');
       
        if (lfEles && lfEles.length > 0) {
            for (let i = 0; i < lfEles.length; i++) {
                //如果是<body>标签，弹出提示
                if (lfEles[i].tagName === 'BODY') {
                    alert("当前登入厂区与看板不匹配");
                }
            }
        }
        lfEles.remove()

        //检查菜单权限
        let permissions = userInfo.permissions
        if (permissions.indexOf('*:*:*') < 0) {//非管理员权限
            let eles = $('*[eam-hasPermi]');
            if (eles && eles.length > 0) {
                for (let i = 0; i < eles.length; i++) {
                    let needPermi = $(eles[i]).attr('eam-hasPermi')
                    if (permissions.indexOf(needPermi) < 0) {
                        $(eles[i]).remove();
                        //如果是<body>标签，弹出提示
                        if (eles[i].tagName === 'BODY') {
                            alert("登入用户没有当前看板的权限");
                            break;
                        }
                    }
                   
                }
            }
        }
    })
})