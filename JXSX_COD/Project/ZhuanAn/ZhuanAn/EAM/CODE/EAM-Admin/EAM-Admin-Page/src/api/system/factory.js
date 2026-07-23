import request from '@/utils/request'

/**
* 厂区管理分页查询
* @param {查询条件} data
*/
export function listFactory(query) {
  return request({
    url: 'system/factory/list',
    method: 'get',
    params: query,
  })
}

/**
* 厂区详情分页查询
* @param {查询条件} data
*/
export function listFactoryDetail(query) {
  return request({
    url: 'system/factory/detailList',
    method: 'get',
    params: query,
  })
}


/**
* 新增厂区管理
* @param data
*/
export function addFactory(data) {
  return request({
    url: 'system/factory',
    method: 'post',
    data: data,
  })
}
/**
* 修改厂区管理
* @param data
*/
export function updateFactory(data) {
  return request({
    url: 'system/factory',
    method: 'PUT',
    data: data,
  })
}
/**
* 获取厂区管理详情
* @param {Id}
*/
export function getFactory(id) {
  return request({
    url: 'system/factory/' + id,
    method: 'get'
  })
}

/**
* 删除厂区管理
* @param {主键} pid
*/
export function delFactory(pid) {
  return request({
    url: 'system/factory/delete/' + pid,
    method: 'delete'
  })
}


/**
* 厂区字典查询
* @param {查询条件} data
*/
export function dictFactory() {
  return request({
    url: 'system/factory/dict',
    method: 'get',
  })
}
