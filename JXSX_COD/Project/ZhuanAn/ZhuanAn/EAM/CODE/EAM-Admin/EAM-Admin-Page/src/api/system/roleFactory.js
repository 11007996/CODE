import request from '@/utils/request'

/**
 * 角色厂区绑定分页查询
 * @param {查询条件} data
 */
export function listRoleFactory(query) {
  return request({
    url: 'system/RoleFactory/list',
    method: 'get',
    params: query
  })
}

// 添加工厂角色用户
export function addRoleFactoryUsers(data) {
  return request({
    url: '/system/RoleFactory/addUsers',
    method: 'post',
    data
  })
}

// 删除工厂角色用户
export function delRoleFactoryUsers(data) {
  return request({
    url: '/system/RoleFactory/delUsers',
    method: 'delete',
    data
  })
}
