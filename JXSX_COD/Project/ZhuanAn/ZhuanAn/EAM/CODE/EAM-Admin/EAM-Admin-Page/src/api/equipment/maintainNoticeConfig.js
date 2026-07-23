import request from '@/utils/request'

/**
 * 保养通知配置分页查询
 * @param {查询条件} data
 */
export function listMaintainNoticeConfig(query) {
  return request({
    url: 'equipment/MaintainNoticeConfig/list',
    method: 'get',
    params: query
  })
}

/**
 * 新增保养通知配置
 * @param data
 */
export function addMaintainNoticeConfig(data) {
  return request({
    url: 'equipment/MaintainNoticeConfig',
    method: 'post',
    data: data
  })
}
/**
 * 修改保养通知配置
 * @param data
 */
export function updateMaintainNoticeConfig(data) {
  return request({
    url: 'equipment/MaintainNoticeConfig',
    method: 'PUT',
    data: data
  })
}
/**
 * 获取保养通知配置详情
 * @param {Id}
 */
export function getMaintainNoticeConfig(id) {
  return request({
    url: 'equipment/MaintainNoticeConfig/' + id,
    method: 'get'
  })
}

/**
 * 删除保养通知配置
 * @param {主键} pid
 */
export function delMaintainNoticeConfig(pid) {
  return request({
    url: 'equipment/MaintainNoticeConfig/delete/' + pid,
    method: 'delete'
  })
}
