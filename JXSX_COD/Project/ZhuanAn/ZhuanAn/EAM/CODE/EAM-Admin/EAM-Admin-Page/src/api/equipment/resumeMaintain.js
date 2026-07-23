import request from '@/utils/request'

/**
 * 履历保养记录分页查询
 * @param {查询条件} data
 */
export function listResumeMaintain(query) {
  return request({
    url: 'equipment/ResumeMaintain/list',
    method: 'get',
    params: query
  })
}

/**
 * 新增履历保养记录
 * @param data
 */
export function addResumeMaintain(data) {
  return request({
    url: 'equipment/ResumeMaintain',
    method: 'post',
    data: data
  })
}
/**
 * 修改履历保养记录
 * @param data
 */
export function updateResumeMaintain(data) {
  return request({
    url: 'equipment/ResumeMaintain',
    method: 'PUT',
    data: data
  })
}
/**
 * 获取履历保养记录详情
 * @param {Id}
 */
export function getResumeMaintain(id) {
  return request({
    url: 'equipment/ResumeMaintain/' + id,
    method: 'get'
  })
}

/**
 * 删除履历保养记录
 * @param {主键} pid
 */
export function delResumeMaintain(pid) {
  return request({
    url: 'equipment/ResumeMaintain/delete/' + pid,
    method: 'delete'
  })
}
