import request from '@/utils/request'

/**
 * 厂区配置分页查询
 * @param {查询条件} data
 */
export function listFactoryConfig(query) {
  return request({
    url: 'basic/FactoryConfig/list',
    method: 'get',
    params: query
  })
}

/**
 * 新增厂区配置
 * @param data
 */
export function addFactoryConfig(data) {
  return request({
    url: 'basic/FactoryConfig',
    method: 'post',
    data: data
  })
}
/**
 * 修改厂区配置
 * @param data
 */
export function updateFactoryConfig(data) {
  return request({
    url: 'basic/FactoryConfig',
    method: 'PUT',
    data: data
  })
}
/**
 * 获取厂区配置详情
 * @param {Id}
 */
export function getFactoryConfig(id) {
  return request({
    url: 'basic/FactoryConfig/' + id,
    method: 'get'
  })
}

/**
 * 获取厂区配置详情
 * @param {key}
 */
export function getFactoryConfigBykey(keys) {
  return request({
    url: 'basic/FactoryConfig/key/' + keys,
    method: 'get'
  })
}

/**
 * 删除厂区配置
 * @param {主键} pid
 */
export function delFactoryConfig(pid) {
  return request({
    url: 'basic/FactoryConfig/delete/' + pid,
    method: 'delete'
  })
}
