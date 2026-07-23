import request from '@/utils/request'

/**
 * 治具储位信息分页查询
 * @param {查询条件} data
 */
export function listStorageSpace(query) {
  return request({
    url: 'fixture/storageSpace/list',
    method: 'get',
    params: query
  })
}

/**
 * 治具储位信息tree查询
 * @param {查询条件} data
 */
export function treeFixtureStorageSpace(query) {
  return request({
    url: 'fixture/storageSpace/treelist',
    method: 'get',
    params: query
  })
}
/**
 * 新增治具储位信息
 * @param data
 */
export function addStorageSpace(data) {
  return request({
    url: 'fixture/storageSpace',
    method: 'post',
    data: data
  })
}
/**
 * 修改治具储位信息
 * @param data
 */
export function updateStorageSpace(data) {
  return request({
    url: 'fixture/storageSpace',
    method: 'PUT',
    data: data
  })
}
/**
 * 获取治具储位信息详情
 * @param {Id}
 */
export function getStorageSpace(id) {
  return request({
    url: 'fixture/storageSpace/' + id,
    method: 'get'
  })
}

/**
 * 删除治具储位信息
 * @param {主键} pid
 */
export function delStorageSpace(pid) {
  return request({
    url: 'fixture/storageSpace/delete/' + pid,
    method: 'delete'
  })
}
