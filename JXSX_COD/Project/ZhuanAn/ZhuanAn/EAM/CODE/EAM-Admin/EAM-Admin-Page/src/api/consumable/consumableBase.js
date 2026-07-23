import request from '@/utils/request'
import { downFile } from '@/utils/request'

/**
 * 耗品表分页查询
 * @param {查询条件} data
 */
export function listConsumableBase(query) {
  return request({
    url: 'consumable/consumableBase/list',
    method: 'get',
    params: query
  })
}

/**
 * 耗品详情分页查询
 * @param {查询条件} data
 */
export function listConsumableDetail(query) {
  return request({
    url: 'consumable/consumableBase/detailList',
    method: 'get',
    params: query
  })
}

/**
 * 新增耗品表
 * @param data
 */
export function addConsumableBase(data) {
  return request({
    url: 'consumable/consumableBase',
    method: 'post',
    data: data
  })
}
/**
 * 修改耗品表
 * @param data
 */
export function updateConsumableBase(data) {
  return request({
    url: 'consumable/consumableBase',
    method: 'PUT',
    data: data
  })
}
/**
 * 获取耗品表详情
 * @param {Id}
 */
export function getConsumableBase(id) {
  return request({
    url: 'consumable/consumableBase/' + id,
    method: 'get'
  })
}

/**
 * 删除耗品表
 * @param {主键} pid
 */
export function delConsumableBase(pid) {
  return request({
    url: 'consumable/consumableBase/delete/' + pid,
    method: 'delete'
  })
}
// 导出耗品表
export async function exportConsumableBase(query) {
  await downFile('consumable/consumableBase/export', { ...query })
}

/**
 * 耗品字典分页查询
 * @param {查询条件} data
 */
export function dictConsumableBase(query) {
  return request({
    url: 'consumable/consumableBase/dict',
    method: 'get',
    params: query
  })
}

/**
 * 耗品类别字典分页查询
 * @param {查询条件} data
 */
export function dictConsumableCategory(query) {
  return request({
    url: 'consumable/consumableBase/dict/category',
    method: 'get',
    params: query
  })
}

/**
 * 耗品表分页查询
 * @param {查询条件} data
 */
export function idleConsumableBase(query) {
  return request({
    url: 'consumable/consumableBase/idle',
    method: 'get',
    params: query
  })
}
