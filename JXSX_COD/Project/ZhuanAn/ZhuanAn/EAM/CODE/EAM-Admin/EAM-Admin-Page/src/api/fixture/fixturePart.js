import request from '@/utils/request'

/**
 * 治具料号关联表分页查询
 * @param {查询条件} data
 */
export function listFixturePart(query) {
  return request({
    url: 'fixture/fixturePart/list',
    method: 'get',
    params: query
  })
}

/**
 * 新增治具料号关联表
 * @param data
 */
export function addFixturePart(data) {
  return request({
    url: 'fixture/fixturePart',
    method: 'post',
    data: data
  })
}
/**
 * 修改治具料号关联表
 * @param data
 */
export function updateFixturePart(data) {
  return request({
    url: 'fixture/fixturePart',
    method: 'PUT',
    data: data
  })
}
/**
 * 获取治具料号关联表详情
 * @param {query}
 */
export function getFixturePart(query) {
  return request({
    url: 'fixture/fixturePart',
    method: 'get',
    params: query
  })
}

/**
 * 删除治具料号关联表
 * @param {主键} pid
 */
export function delFixturePart(data) {
  return request({
    url: 'fixture/fixturePart/delete',
    method: 'delete',
    data: data
  })
}
