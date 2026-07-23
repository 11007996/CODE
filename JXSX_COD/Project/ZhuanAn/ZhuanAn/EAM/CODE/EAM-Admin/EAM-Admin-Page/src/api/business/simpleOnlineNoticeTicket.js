import request from '@/utils/request'

/**
 * 上线通知单分页查询
 * @param {查询条件} data
 */
export function listSimpleOnlineNoticeTicket(query) {
  return request({
    url: 'business/SimpleOnlineNoticeTicket/list',
    method: 'get',
    params: query
  })
}

/**
 * 新增上线通知单
 * @param data
 */
export function addSimpleOnlineNoticeTicket(data) {
  return request({
    url: 'business/SimpleOnlineNoticeTicket',
    method: 'post',
    data: data
  })
}

/**
 * 修改上线通知单
 * @param data
 */
export function updateSimpleOnlineNoticeTicket(data) {
  return request({
    url: 'business/SimpleOnlineNoticeTicket',
    method: 'PUT',
    data: data
  })
}

/**
 * 获取上线通知单详情
 * @param {Id}
 */
export function getSimpleOnlineNoticeTicket(id) {
  return request({
    url: 'business/SimpleOnlineNoticeTicket/' + id,
    method: 'get'
  })
}

/**
 * 删除上线通知单
 * @param {主键} pid
 */
export function delSimpleOnlineNoticeTicket(pid) {
  return request({
    url: 'business/SimpleOnlineNoticeTicket/delete/' + pid,
    method: 'delete'
  })
}

/**
 * 结案 上线通知单
 * @param {主键} pid
 */
export function closeSimpleOnlineNoticeTicket(pid) {
  return request({
    url: 'business/SimpleOnlineNoticeTicket/close/' + pid,
    method: 'put'
  })
}

/**
 * 上线通知单项目名称字典分页查询
 * @param {查询条件} data
 */
export function dictSimpleOnlineNoticeTicketItem(query) {
  return request({
    url: 'business/SimpleOnlineNoticeTicket/item/dict',
    method: 'get',
    params: query
  })
}

/**
 * 上线通知单料号名称字典分页查询
 * @param {查询条件} data
 */
export function dictSimpleOnlineNoticeTicketPartName(query) {
  return request({
    url: 'business/SimpleOnlineNoticeTicket/partName/dict',
    method: 'get',
    params: query
  })
}

/**
 * 根据料号查询历史上线通知单关联项目列表
 * @param {查询条件} data
 */
export function listSimpleOnlineNoticeTicketItemsByPart(query) {
  return request({
    url: 'business/SimpleOnlineNoticeTicket/itemsByPart',
    method: 'get',
    params: query
  })
}
