//依赖bootstrap，消息弹框

function msgSuccess(message) {
    const alert = document.createElement('div');
    alert.className = 'alert alert-success alert-dismissible fade show position-fixed start-50 translate-middle-x w-auto';
    alert.style.zIndex = '1060';
    alert.style.top = '30px'
    alert.style.maxWidth = '500px'
    alert.style.overflow = 'hidden'
    alert.style.textOverflow = 'ellipsis'
    alert.style.padding = '15px 50px 15px 20px';
    alert.style.fontSize = '16px';
    alert.style.borderRadius = '5px';
    alert.innerHTML = `
        ${message}
        <button type="button" class="btn-close" style="width:16px;height:16px;padding:15px;" data-bs-dismiss="alert"></button>
    `;
    document.body.appendChild(alert);

    // 3秒后自动关闭
    setTimeout(() => {
        const bsAlert = bootstrap.Alert.getOrCreateInstance(alert);
        if (bsAlert)
            bsAlert.close();
    }, 3000);
}


function msgError(message) {
    const alert = document.createElement('div');
    alert.className = 'alert alert-danger  alert-dismissible fade show position-fixed start-50 translate-middle-x w-auto';
    alert.style.zIndex = '1060';
    alert.style.top = '30px'
    alert.style.maxWidth = '500px'
    alert.style.overflow = 'hidden'
    alert.style.textOverflow = 'ellipsis'
    alert.style.padding = '15px 50px 15px 20px';
    alert.style.fontSize = '16px';
    alert.style.borderRadius = '5px';
    alert.innerHTML = `
        ${message}
        <button type="button" class="btn-close" style="width:16px;height:16px;padding:15px;" data-bs-dismiss="alert"></button>
    `;
    document.body.appendChild(alert);

    // 3秒后自动关闭
    setTimeout(() => {
        const bsAlert = bootstrap.Alert.getOrCreateInstance(alert);
        if (bsAlert)
            bsAlert.close();
    }, 3000);
}

