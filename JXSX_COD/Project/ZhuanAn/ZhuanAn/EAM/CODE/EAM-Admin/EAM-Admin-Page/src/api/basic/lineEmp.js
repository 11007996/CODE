import request from '@/utils/request'

/**
* 产线员工关联分页查询
* @param {查询条件} data
*/
export function listLineEmp(query) {
  return request({
    url: 'basic/LineEmp/list',
    method: 'get',
    params: query,
  })
}

/**
* 新增产线员工关联
* @param data
*/
export function addLineEmp(data) {
  return request({
    url: 'basic/LineEmp',
    method: 'post',
    data: data,
  })
}
/**
* 修改产线员工关联
* @param data
*/
export function updateLineEmp(data) {
  return request({
    url: 'basic/LineEmp',
    method: 'PUT',
    data: data,
  })
}
/**
* 获取产线员工关联详情
* @param {Id}
*/
export function getLineEmp(id) {
  return request({
    url: 'basic/LineEmp/' + id,
    method: 'get'
  })
}

/**
* 删除产线员工关联
* @param {主键} pid
*/
export function delLineEmp(pid) {
  return request({
    url: 'basic/LineEmp/delete/' + pid,
    method: 'delete'
  })
}
