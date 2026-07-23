import request from '@/utils/request'

/**
 * 耗品通知人员配置分页查询
 * @param {查询条件} data
 */
export function listConsumableConfigNotice(query) {
  return request({
    url: 'consumable/ConsumableConfigNotice/list',
    method: 'get',
    params: query
  })
}

/**
 * 新增耗品通知人员配置
 * @param data
 */
export function addConsumableConfigNotice(data) {
  return request({
    url: 'consumable/ConsumableConfigNotice',
    method: 'post',
    data: data
  })
}
/**
 * 修改耗品通知人员配置
 * @param data
 */
export function updateConsumableConfigNotice(data) {
  return request({
    url: 'consumable/ConsumableConfigNotice',
    method: 'PUT',
    data: data
  })
}
/**
 * 获取耗品通知人员配置详情
 * @param {Id}
 */
export function getConsumableConfigNotice(id) {
  return request({
    url: 'consumable/ConsumableConfigNotice/' + id,
    method: 'get'
  })
}

/**
 * 删除耗品通知人员配置
 * @param {主键} pid
 */
export function delConsumableConfigNotice(pid) {
  return request({
    url: 'consumable/ConsumableConfigNotice/delete/' + pid,
    method: 'delete'
  })
}
