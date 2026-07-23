import request from '@/utils/request'

/**
* 治具尺寸量测验收单分页查询
* @param {查询条件} data
*/
export function listSizeMeasureTicket(query) {
  return request({
    url: 'business/SizeMeasureTicket/list',
    method: 'get',
    params: query,
  })
}

/**
* 新增治具尺寸量测验收单
* @param data
*/
export function addSizeMeasureTicket(data) {
  return request({
    url: 'business/SizeMeasureTicket',
    method: 'post',
    data: data,
  })
}
/**
* 修改治具尺寸量测验收单
* @param data
*/
export function updateSizeMeasureTicket(data) {
  return request({
    url: 'business/SizeMeasureTicket',
    method: 'PUT',
    data: data,
  })
}
/**
* 获取治具尺寸量测验收单详情
* @param {Id}
*/
export function getSizeMeasureTicket(id) {
  return request({
    url: 'business/SizeMeasureTicket/' + id,
    method: 'get'
  })
}

/**
* 删除治具尺寸量测验收单
* @param {主键} pid
*/
export function delSizeMeasureTicket(pid) {
  return request({
    url: 'business/SizeMeasureTicket/delete/' + pid,
    method: 'delete'
  })
}
/**
* 治具尺寸量测验收单_入库
* @param data
*/
export function sizeMeasureTicketInStorage(data) {
  return request({
    url: 'business/SizeMeasureTicket/inStorage',
    method: 'post',
    data: data,
  })
}
