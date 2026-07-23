import request from '@/utils/request'

/**
* 产品开发需求单分页查询
* @param {查询条件} data
*/
export function listProductDevDemandTicket(query) {
  return request({
    url: 'business/ProductDevDemandTicket/list',
    method: 'get',
    params: query,
  })
}

/**
* 新增产品开发需求单
* @param data
*/
export function addProductDevDemandTicket(data) {
  return request({
    url: 'business/ProductDevDemandTicket',
    method: 'post',
    data: data,
  })
}
/**
* 修改产品开发需求单
* @param data
*/
export function updateProductDevDemandTicket(data) {
  return request({
    url: 'business/ProductDevDemandTicket',
    method: 'PUT',
    data: data,
  })
}
/**
* 获取产品开发需求单详情
* @param {Id}
*/
export function getProductDevDemandTicket(id) {
  return request({
    url: 'business/ProductDevDemandTicket/' + id,
    method: 'get'
  })
}

/**
* 删除产品开发需求单
* @param {主键} pid
*/
export function delProductDevDemandTicket(pid) {
  return request({
    url: 'business/ProductDevDemandTicket/delete/' + pid,
    method: 'delete'
  })
}

/**
* 同步产品开发需求单
* @param data
*/
export function asyncProductDevDemandTicket(id) {
  return request({
    url: 'business/ProductDevDemandTicket/'+id+'/async',
    method: 'PUT',
  })
}
