const TokenKey = 'EAM-Token'
const TokenCreateTimeKey = 'EAM-Token_Create'
const LoginFactoryKey ='EAM-Login-FactoryId'
function getToken() {
    return window.localStorage.getItem(TokenKey)
}

function setToken(token) {
    window.localStorage.setItem(TokenCreateTimeKey, new Date())
    return window.localStorage.setItem(TokenKey, token)
}

function removeToken() {
    window.localStorage.removeItem(TokenCreateTimeKey)
    return window.localStorage.removeItem(TokenKey)
}

function getTokenCreateTime() {
    return window.localStorage.getItem(TokenCreateTimeKey)
}


function getLoginFactory() {
    return window.localStorage.getItem(LoginFactoryKey)
}

function setLoginFactory(factoryId) {
    return window.localStorage.setItem(LoginFactoryKey, factoryId)
}

function removeLoginFactory() {
    return window.localStorage.removeItem(LoginFactoryKey)
}