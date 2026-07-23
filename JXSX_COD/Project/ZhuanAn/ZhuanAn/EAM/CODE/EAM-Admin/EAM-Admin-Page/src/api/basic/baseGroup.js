import request from '@/utils/request'

/**
* 基础分组分页查询
* @param {查询条件} data
*/
export function listBaseGroup(query) {
  return request({
    url: 'basic/BaseGroup/list',
    method: 'get',
    params: query,
  })
}

/**
* 新增基础分组
* @param data
*/
export function addBaseGroup(data) {
  return request({
    url: 'basic/BaseGroup',
    method: 'post',
    data: data,
  })
}
/**
* 修改基础分组
* @param data
*/
export function updateBaseGroup(data) {
  return request({
    url: 'basic/BaseGroup',
    method: 'PUT',
    data: data,
  })
}
/**
* 获取基础分组详情
* @param {Id}
*/
export function getBaseGroup(id) {
  return request({
    url: 'basic/BaseGroup/' + id,
    method: 'get'
  })
}

/**
* 删除基础分组
* @param {主键} pid
*/
export function delBaseGroup(pid) {
  return request({
    url: 'basic/BaseGroup/delete/' + pid,
    method: 'delete'
  })
}
