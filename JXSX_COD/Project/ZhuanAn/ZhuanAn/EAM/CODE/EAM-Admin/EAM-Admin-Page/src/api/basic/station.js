import request from '@/utils/request'

/**
 * 工站信息分页查询
 * @param {查询条件} data
 */
export function listStation(query) {
  return request({
    url: 'basic/Station/list',
    method: 'get',
    params: query
  })
}

/**
 * 新增工站信息
 * @param data
 */
export function addStation(data) {
  return request({
    url: 'basic/Station',
    method: 'post',
    data: data
  })
}
/**
 * 修改工站信息
 * @param data
 */
export function updateStation(data) {
  return request({
    url: 'basic/Station',
    method: 'PUT',
    data: data
  })
}
/**
 * 获取工站信息详情
 * @param {Id}
 */
export function getStation(id) {
  return request({
    url: 'basic/Station/' + id,
    method: 'get'
  })
}

/**
 * 删除工站信息
 * @param {主键} pid
 */
export function delStation(pid) {
  return request({
    url: 'basic/Station/delete/' + pid,
    method: 'delete'
  })
}

/**
 * 工站信息字典查询
 * @param {查询条件} data
 */
export function dictStation(query) {
  return request({
    url: 'basic/Station/dict',
    method: 'get',
    params: query
  })
}
