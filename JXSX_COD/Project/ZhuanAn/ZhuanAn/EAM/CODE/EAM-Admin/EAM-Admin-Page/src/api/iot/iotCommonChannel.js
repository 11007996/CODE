import request from '@/utils/request'

/**
 * 传输通道分页查询
 * @param {查询条件} data
 */
export function listIotCommonChannel(query) {
  return request({
    url: 'iot/IotCommonChannel/list',
    method: 'get',
    params: query
  })
}

/**
 * 新增传输通道
 * @param data
 */
export function addIotCommonChannel(data) {
  return request({
    url: 'iot/IotCommonChannel',
    method: 'post',
    data: data
  })
}
/**
 * 修改传输通道
 * @param data
 */
export function updateIotCommonChannel(data) {
  return request({
    url: 'iot/IotCommonChannel',
    method: 'PUT',
    data: data
  })
}
/**
 * 获取传输通道详情
 * @param {Id}
 */
export function getIotCommonChannel(id) {
  return request({
    url: 'iot/IotCommonChannel/' + id,
    method: 'get'
  })
}

/**
 * 删除传输通道
 * @param {主键} pid
 */
export function delIotCommonChannel(pid) {
  return request({
    url: 'iot/IotCommonChannel/delete/' + pid,
    method: 'delete'
  })
}

/**
 * 传输通道字典分页查询
 * @param {查询条件} data
 */
export function dictIotCommonChannel(query) {
  return request({
    url: 'iot/IotCommonChannel/dict',
    method: 'get',
    params: query
  })
}
