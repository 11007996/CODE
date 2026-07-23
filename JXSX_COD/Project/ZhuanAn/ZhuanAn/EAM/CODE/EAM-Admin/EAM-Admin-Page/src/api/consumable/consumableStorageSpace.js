import request from '@/utils/request'

/**
 * 耗品储位信息分页查询
 * @param {查询条件} data
 */
export function listStorageSpace(query) {
  return request({
    url: 'consumable/storageSpace/list',
    method: 'get',
    params: query
  })
}

/**
 * 耗品储位信息tree查询
 * @param {查询条件} data
 */
export function treeConsumableStorageSpace(query) {
  return request({
    url: 'consumable/storageSpace/treelist',
    method: 'get',
    params: query
  })
}
/**
 * 新增耗品储位信息
 * @param data
 */
export function addStorageSpace(data) {
  return request({
    url: 'consumable/storageSpace',
    method: 'post',
    data: data
  })
}
/**
 * 修改耗品储位信息
 * @param data
 */
export function updateStorageSpace(data) {
  return request({
    url: 'consumable/storageSpace',
    method: 'PUT',
    data: data
  })
}
/**
 * 获取耗品储位信息详情
 * @param {Id}
 */
export function getStorageSpace(id) {
  return request({
    url: 'consumable/storageSpace/' + id,
    method: 'get'
  })
}

/**
 * 删除耗品储位信息
 * @param {主键} pid
 */
export function delStorageSpace(pid) {
  return request({
    url: 'consumable/storageSpace/delete/' + pid,
    method: 'delete'
  })
}
