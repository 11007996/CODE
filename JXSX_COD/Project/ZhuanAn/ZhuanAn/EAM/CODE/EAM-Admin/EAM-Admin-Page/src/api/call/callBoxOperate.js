import request from '@/utils/request'

/**
* 盒子操作记录分页查询
* @param {查询条件} data
*/
export function listCallBoxOperate(query) {
  return request({
    url: 'call/CallBoxOperate/list',
    method: 'get',
    params: query,
  })
}

/**
* 新增盒子操作记录
* @param data
*/
export function addCallBoxOperate(data) {
  return request({
    url: 'call/CallBoxOperate',
    method: 'post',
    data: data,
  })
}
/**
* 获取盒子操作记录详情
* @param {Id}
*/
export function getCallBoxOperate(id) {
  return request({
    url: 'call/CallBoxOperate/' + id,
    method: 'get'
  })
}

