import request from '@/utils/request'

// 查询角色用户
export function getRoleUsers(query) {
  return request({
    url: '/system/userRole/list',
    method: 'get',
    params: query
  })
}

// 添加角色用户
export function createRoleUsers(data) {
  return request({
    url: '/system/userRole/create',
    method: 'post',
    data
  })
}
// 删除角色用户
export function deleteRoleUsers(data) {
  return request({
    url: '/system/userRole/delete',
    method: 'post',
    data
  })
}
// 查询角色未添加用户列表
export function getExcludeUsers(query) {
  return request({
    url: '/system/userRole/getExcludeUsers',
    method: 'get',
    params: query
  })
}

// 查询角色未添加的工厂用户列表
export function getExcludeFactoryUsers(query) {
  return request({
    url: '/system/userRole/GetExcludeFactoryUsers',
    method: 'get',
    params: query
  })
}

// 查询角色用户
export function getRoleFactoryUsers(query) {
  return request({
    url: '/system/userRole/factory/list',
    method: 'get',
    params: query
  })
}
