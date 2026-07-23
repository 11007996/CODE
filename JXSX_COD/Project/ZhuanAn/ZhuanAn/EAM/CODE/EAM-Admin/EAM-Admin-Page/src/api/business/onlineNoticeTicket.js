import request from '@/utils/request'

/**
 * 上线通知单分页查询
 * @param {查询条件} data
 */
export function listOnlineNoticeTicket(query) {
  return request({
    url: 'business/OnlineNoticeTicket/list',
    method: 'get',
    params: query
  })
}

/**
 * 新增上线通知单
 * @param data
 */
export function addOnlineNoticeTicket(data) {
  return request({
    url: 'business/OnlineNoticeTicket',
    method: 'post',
    data: data
  })
}

/**
 * 修改上线通知单
 * @param data
 */
export function updateOnlineNoticeTicket(data) {
  return request({
    url: 'business/OnlineNoticeTicket',
    method: 'PUT',
    data: data
  })
}

/**
 * 获取上线通知单详情
 * @param {Id}
 */
export function getOnlineNoticeTicket(id) {
  return request({
    url: 'business/OnlineNoticeTicket/' + id,
    method: 'get'
  })
}

/**
 * 删除上线通知单
 * @param {主键} pid
 */
export function delOnlineNoticeTicket(pid) {
  return request({
    url: 'business/OnlineNoticeTicket/delete/' + pid,
    method: 'delete'
  })
}

/**
 * 获取上线通知单_设备清单概要
 * @param {Id}
 */
export function getOnlineNoticeTicketEquipmentSummary(id) {
  return request({
    url: 'business/OnlineNoticeTicket/' + id + '/equipment/summary',
    method: 'get'
  })
}

/**
 * 获取上线通知单_治具清单概要
 * @param {Id}
 */
export function getOnlineNoticeTicketFixtureSummary(id) {
  return request({
    url: 'business/OnlineNoticeTicket/' + id + '/fixture/summary',
    method: 'get'
  })
}
