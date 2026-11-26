import http from '../utils/http'
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