import request from '@/utils/request'

/**
 * 治具信息分页查询
 * @param {查询条件} data
 */
export function listFixtureBase(query) {
  return request({
    url: 'fixture/fixtureBase/list',
    method: 'get',
    params: query
  })
}

/**
 * 治具详情信息分页查询
 * @param {查询条件} data
 */
export function listFixtureDetail(query) {
  return request({
    url: 'fixture/fixtureBase/detailList',
    method: 'get',
    params: query
  })
}

/**
 * 查询闲置治具数量 分页查询
 * @param {查询条件} data
 */
export function idleFixtureBase(query) {
  return request({
    url: 'fixture/fixtureBase/idle',
    method: 'get',
    params: query
  })
}

/**
 * 新增治具信息
 * @param data
 */
export function addFixtureBase(data) {
  return request({
    url: 'fixture/fixtureBase',
    method: 'post',
    data: data
  })
}
/**
 * 修改治具信息
 * @param data
 */
export function updateFixtureBase(data) {
  return request({
    url: 'fixture/fixtureBase',
    method: 'PUT',
    data: data
  })
}
/**
 * 获取治具信息详情
 * @param {Id}
 */
export function getFixtureBase(id) {
  return request({
    url: 'fixture/fixtureBase/' + id,
    method: 'get'
  })
}

/**
 * 删除治具信息
 * @param {主键} pid
 */
export function delFixtureBase(pid) {
  return request({
    url: 'fixture/fixtureBase/delete/' + pid,
    method: 'delete'
  })
}

/**
 * 治具字典分页查询
 * @param {查询条件} data
 */
export function dictFixtureBase(query) {
  return request({
    url: 'fixture/fixtureBase/dict',
    method: 'get',
    params: query
  })
}
