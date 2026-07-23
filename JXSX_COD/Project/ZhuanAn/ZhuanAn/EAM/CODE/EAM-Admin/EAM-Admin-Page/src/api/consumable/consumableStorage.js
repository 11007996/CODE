import request from '@/utils/request'

/**
 * 耗品存储表分页查询
 * @param {查询条件} data
 */
export function listConsumableStorage(query) {
  return request({
    url: 'consumable/consumableStorage/list',
    method: 'get',
    params: query
  })
}

/**
 * 获取耗品存储详情
 * @param {query}
 */
export function getConsumableStorage(query) {
  return request({
    url: 'consumable/consumableStorage/info',
    method: 'get',
    params: query
  })
}

/**
 * 删除耗品存储
 * @param {主键} query
 */
export function delConsumableStorage(query) {
  return request({
    url: 'consumable/consumableStorage/delete',
    method: 'delete',
    params: query
  })
}

/**
 * 入库
 * @param data
 */
export function inConsumableStorage(data) {
  return request({
    url: 'consumable/consumableStorage/in',
    method: 'PUT',
    data: data
  })
}
/**
 * 出库
 * @param data
 */
export function outConsumableStorage(data) {
  return request({
    url: 'consumable/consumableStorage/out',
    method: 'PUT',
    data: data
  })
}
/**
 * 报废
 * @param data
 */
export function scrappedConsumableStorage(data) {
  return request({
    url: 'consumable/consumableStorage/scrapped',
    method: 'PUT',
    data: data
  })
}
/**
 * 领用
 * @param data
 */
export function receiveConsumableStorage(data) {
  return request({
    url: 'consumable/consumableStorage/receive',
    method: 'PUT',
    data: data
  })
}

/**
 * 归还
 * @param data
 */
export function backConsumableStorage(data) {
  return request({
    url: 'consumable/consumableStorage/back',
    method: 'PUT',
    data: data
  })
}

/**
 * 转移
 * @param  data
 */
export function transferConsumableStorage(data) {
  return request({
    url: 'consumable/consumableStorage/transfer',
    method: 'PUT',
    data: data
  })
}

/**
 * 耗品出入库记录表分页查询
 * @param {查询条件} data
 */
export function listConsumableStorageRecord(query) {
  return request({
    url: 'consumable/consumableStorage/record/list',
    method: 'get',
    params: query
  })
}

/**
 * 导入
 * @param  data
 */
export function importConsumableStorage(data) {
  return request({
    url: 'consumable/consumableStorage/importData',
    method: 'POST',
    data: data
  })
}

/**
 * 【变动】导入
 * @param  data
 */
export function importConsumableStorageOperate(data) {
  return request({
    url: 'consumable/consumableStorage/importOperateData',
    method: 'POST',
    data: data
  })
}
