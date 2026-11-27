import http from '../utils/http'

//登陆
export function login(param)
{
    return http({
    url: '/api/Base/Login',
    method: 'POST',
    data: param
  });
}

export function test(param)
{
    return http({
    url: '/api/Base/test',
    method: 'POST',
    data: param
  });
}

//获取最新版本号
export function getAppVersion()
{
    return http({
    url: '/api/Base/getAppVersion',
    method: 'GET'
  });
}

//验证JWT
export function CheckToken()
{
    return http({
    url: '/api/Base/CheckToken',
    method: 'GET'
  });
}