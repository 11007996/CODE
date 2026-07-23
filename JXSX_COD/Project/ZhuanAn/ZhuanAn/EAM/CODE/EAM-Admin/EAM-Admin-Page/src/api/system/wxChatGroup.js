import request from '@/utils/request'

/**
 * 微信聊天群分页查询
 * @param {查询条件} data
 */
export function listWxChatGroup(query) {
  return request({
    url: 'system/WxChatGroup/list',
    method: 'get',
    params: query
  })
}

/**
 * 新增微信聊天群
 * @param data
 */
export function addWxChatGroup(data) {
  return request({
    url: 'system/WxChatGroup',
    method: 'post',
    data: data
  })
}
/**
 * 修改微信聊天群
 * @param data
 */
export function updateWxChatGroup(data) {
  return request({
    url: 'system/WxChatGroup',
    method: 'PUT',
    data: data
  })
}
/**
 * 获取微信聊天群详情
 * @param {Id}
 */
export function getWxChatGroup(id) {
  return request({
    url: 'system/WxChatGroup/' + id,
    method: 'get'
  })
}

/**
 * 删除微信聊天群
 * @param {主键} pid
 */
export function delWxChatGroup(pid) {
  return request({
    url: 'system/WxChatGroup/delete/' + pid,
    method: 'delete'
  })
}

/**
 * 微信聊天群字典分页查询
 * @param {查询条件} data
 */
export function dictWxChatGroup(query) {
  return request({
    url: 'system/WxChatGroup/dict',
    method: 'get',
    params: query
  })
}
