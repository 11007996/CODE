import request from '@/utils/request'

// 查询厂区用户
export function getFactoryUsers(query) {
  return request({
    url: '/system/userFactory/list',
    method: 'get',
    params: query
  })
}

// 添加厂区用户
export function addFactoryUsers(data) {
  return request({
    url: '/system/userFactory',
    method: 'post',
    data
  })
}

// 删除厂区用户
export function deleteFactoryUsers(data) {
  return request({
    url: '/system/userFactory',
    method: 'delete',
    data
  })
}

// 查询厂区未添加用户列表
export function getExcludeUsers(query) {
  return request({
    url: '/system/userFactory/getExcludeUsers',
    method: 'get',
    params: query
  })
}
