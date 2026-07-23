import request from '@/utils/request'

/**
 * 履历维修记录分页查询
 * @param {查询条件} data
 */
export function listResumeRepair(query) {
  return request({
    url: 'equipment/ResumeRepair/list',
    method: 'get',
    params: query
  })
}

/**
 * 新增履历维修记录
 * @param data
 */
export function addResumeRepair(data) {
  return request({
    url: 'equipment/ResumeRepair',
    method: 'post',
    data: data
  })
}
/**
 * 修改履历维修记录
 * @param data
 */
export function updateResumeRepair(data) {
  return request({
    url: 'equipment/ResumeRepair',
    method: 'PUT',
    data: data
  })
}
/**
 * 获取履历维修记录详情
 * @param {Id}
 */
export function getResumeRepair(id) {
  return request({
    url: 'equipment/ResumeRepair/' + id,
    method: 'get'
  })
}

/**
 * 删除履历维修记录
 * @param {主键} pid
 */
export function delResumeRepair(pid) {
  return request({
    url: 'equipment/ResumeRepair/delete/' + pid,
    method: 'delete'
  })
}
