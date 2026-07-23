import request from '@/utils/request'

/**
 * 员工信息分页查询
 * @param {查询条件} data
 */
export function listEmployee(query) {
  return request({
    url: 'basic/Employee/list',
    method: 'get',
    params: query
  })
}

/**
 * 新增员工信息
 * @param data
 */
export function addEmployee(data) {
  return request({
    url: 'basic/Employee',
    method: 'post',
    data: data
  })
}
/**
 * 修改员工信息
 * @param data
 */
export function updateEmployee(data) {
  return request({
    url: 'basic/Employee',
    method: 'PUT',
    data: data
  })
}
/**
 * 获取员工信息详情
 * @param {Id}
 */
export function getEmployee(id) {
  return request({
    url: 'basic/Employee/' + id,
    method: 'get'
  })
}

/**
 * 删除员工信息
 * @param {主键} empCode
 */
export function delEmployee(empCode) {
  return request({
    url: 'basic/Employee/delete/' + empCode,
    method: 'delete'
  })
}

/**
 * 员工字典分页查询
 * @param {查询条件} data
 */
export function dictEmployee(query) {
  return request({
    url: 'basic/Employee/dict',
    method: 'get',
    params: query
  })
}
