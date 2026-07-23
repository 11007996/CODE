import request from '@/utils/request'

/**
 * 分组用户分页查询
 * @param {查询条件} data
 */
export function listBaseGroupUser(query) {
  return request({
    url: 'basic/BaseGroupUser/list',
    method: 'get',
    params: query
  })
}

/**
 * 新增分组用户
 * @param data
 */
export function addBaseGroupUser(data) {
  return request({
    url: 'basic/BaseGroupUser',
    method: 'post',
    data: data
  })
}

/**
 * 批量新增分组用户
 * @param data
 */
export function batchAddBaseGroupUser(data) {
  return request({
    url: 'basic/BaseGroupUser/batch',
    method: 'post',
    data: data
  })
}

/**
 * 获取分组用户详情
 * @param {Id}
 */
export function getBaseGroupUser(id) {
  return request({
    url: 'basic/BaseGroupUser/' + id,
    method: 'get'
  })
}

/**
 * 删除分组用户
 * @param {主键} data
 */
export function delBaseGroupUser(data) {
  return request({
    url: 'basic/BaseGroupUser/delete',
    method: 'delete',
    data: data
  })
}

// 查询分组未添加用户列表
export function getExcludeUsers(query) {
  return request({
    url: 'basic/BaseGroupUser/getExcludeUsers',
    method: 'get',
    params: query
  })
}
