import request from '@/utils/request'

/**
* 数据解析脚本分页查询
* @param {查询条件} data
*/
export function listIotProductParserScript(query) {
  return request({
    url: 'iot/IotProductParserScript/list',
    method: 'get',
    params: query,
  })
}

/**
* 新增数据解析脚本
* @param data
*/
export function addIotProductParserScript(data) {
  return request({
    url: 'iot/IotProductParserScript',
    method: 'post',
    data: data,
  })
}
/**
* 修改数据解析脚本
* @param data
*/
export function updateIotProductParserScript(data) {
  return request({
    url: 'iot/IotProductParserScript',
    method: 'PUT',
    data: data,
  })
}
/**
* 获取数据解析脚本详情
* @param {Id}
*/
export function getIotProductParserScript(id) {
  return request({
    url: 'iot/IotProductParserScript/' + id,
    method: 'get'
  })
}

/**
* 删除数据解析脚本
* @param {主键} pid
*/
export function delIotProductParserScript(pid) {
  return request({
    url: 'iot/IotProductParserScript/delete/' + pid,
    method: 'delete'
  })
}
