import request from '@/utils/request'

/**
* 产品测量报告分页查询
* @param {查询条件} data
*/
export function listProdMeasureTicket(query) {
  return request({
    url: 'business/ProdMeasureTicket/list',
    method: 'get',
    params: query,
  })
}

/**
* 新增产品测量报告
* @param data
*/
export function addProdMeasureTicket(data) {
  return request({
    url: 'business/ProdMeasureTicket',
    method: 'post',
    data: data,
  })
}
/**
* 修改产品测量报告
* @param data
*/
export function updateProdMeasureTicket(data) {
  return request({
    url: 'business/ProdMeasureTicket',
    method: 'PUT',
    data: data,
  })
}
/**
* 获取产品测量报告详情
* @param {Id}
*/
export function getProdMeasureTicket(id) {
  return request({
    url: 'business/ProdMeasureTicket/' + id,
    method: 'get'
  })
}

/**
* 删除产品测量报告
* @param {主键} pid
*/
export function delProdMeasureTicket(pid) {
  return request({
    url: 'business/ProdMeasureTicket/delete/' + pid,
    method: 'delete'
  })
}
