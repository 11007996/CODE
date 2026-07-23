import request from '@/utils/request'

/**
* 产品事件处理动作分页查询
* @param {查询条件} data
*/
export function listIotProductEventAction(query) {
  return request({
    url: 'iot/IotProductEventAction/list',
    method: 'get',
    params: query,
  })
}

/**
* 新增产品事件处理动作
* @param data
*/
export function addIotProductEventAction(data) {
  return request({
    url: 'iot/IotProductEventAction',
    method: 'post',
    data: data,
  })
}
/**
* 修改产品事件处理动作
* @param data
*/
export function updateIotProductEventAction(data) {
  return request({
    url: 'iot/IotProductEventAction',
    method: 'PUT',
    data: data,
  })
}
/**
* 获取产品事件处理动作详情
* @param {Id}
*/
export function getIotProductEventAction(id) {
  return request({
    url: 'iot/IotProductEventAction/' + id,
    method: 'get'
  })
}

/**
* 删除产品事件处理动作
* @param {主键} pid
*/
export function delIotProductEventAction(pid) {
  return request({
    url: 'iot/IotProductEventAction/delete/' + pid,
    method: 'delete'
  })
}
