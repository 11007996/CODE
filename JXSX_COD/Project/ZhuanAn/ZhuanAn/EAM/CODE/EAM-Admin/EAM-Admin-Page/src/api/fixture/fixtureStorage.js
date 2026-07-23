import request from '@/utils/request'

/**
 * 治具存储分页查询
 * @param {查询条件} data
 */
export function listFixtureStorage(query) {
  return request({
    url: 'fixture/fixtureStorage/list',
    method: 'get',
    params: query
  })
}

/**
 * 获取治具存储详情
 * @param {query}
 */
export function getFixtureStorage(query) {
  return request({
    url: 'fixture/fixtureStorage/info',
    method: 'get',
    params: query
  })
}

/**
 * 删除治具存储
 * @param {主键} query
 */
export function delFixtureStorage(query) {
  return request({
    url: 'fixture/fixtureStorage/delete',
    method: 'delete',
    params: query
  })
}

/**
 * 入库
 * @param data
 */
export function inFixtureStorage(data) {
  return request({
    url: 'fixture/fixtureStorage/in',
    method: 'PUT',
    data: data
  })
}

/**
 * 出库
 * @param data
 */
export function outFixtureStorage(data) {
  return request({
    url: 'fixture/fixtureStorage/out',
    method: 'PUT',
    data: data
  })
}

/**
 * 报废
 * @param data
 */
export function scrappedFixtureStorage(data) {
  return request({
    url: 'fixture/fixtureStorage/scrapped',
    method: 'PUT',
    data: data
  })
}

/**
 * 领用
 * @param data
 */
export function receiveFixtureStorage(data) {
  return request({
    url: 'fixture/fixtureStorage/receive',
    method: 'PUT',
    data: data
  })
}

/**
 * 批量领用
 * @param data
 */
export function batchReceiveFixtureStorage(data) {
  return request({
    url: 'fixture/fixtureStorage/receive/batch',
    method: 'PUT',
    data: data
  })
}

/**
 * 归还
 * @param data
 */
export function backFixtureStorage(data) {
  return request({
    url: 'fixture/fixtureStorage/back',
    method: 'PUT',
    data: data
  })
}

/**
 * 批量归还
 * @param data
 */
export function batchBackFixtureStorage(data) {
  return request({
    url: 'fixture/fixtureStorage/back/batch',
    method: 'PUT',
    data: data
  })
}

/**
 * 转移
 * @param data
 */
export function transferFixtureStorage(data) {
  return request({
    url: 'fixture/fixtureStorage/transfer',
    method: 'PUT',
    data: data
  })
}

/**
 * 治具出入库记录表分页查询
 * @param {查询条件} data
 */
export function listFixtureStorageRecord(query) {
  return request({
    url: 'fixture/fixtureStorage/record/list',
    method: 'get',
    params: query
  })
}

/**
 * 治具存储查询(使用中的治具)
 * @param {查询条件} data
 */
export function listFixtureStorageUsing(query) {
  return request({
    url: 'fixture/fixtureStorage/using/list',
    method: 'get',
    params: query
  })
}

/**
 * 获取领用详情
 * @param {查询条件} id
 */
export function getFixtureStorageUsing(id) {
  return request({
    url: 'fixture/fixtureStorage/using/' + id,
    method: 'get'
  })
}

/**
 * 导入
 * @param data
 */
export function importFixtureStorage(data) {
  return request({
    url: 'fixture/fixtureStorage/importData',
    method: 'POST',
    data: data
  })
}

/**
 * 操作导入
 * @param data
 */
export function importFixtureStorageOperate(data) {
  return request({
    url: 'fixture/fixtureStorage/importOperateData',
    method: 'POST',
    data: data
  })
}
