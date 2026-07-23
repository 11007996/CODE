import request from '@/utils/request'

/**
 * 保养计划班次分页查询
 * @param {查询条件} data
 */
export function listMaintainPlanClass(query) {
  return request({
    url: 'equipment/MaintainPlanClass/list',
    method: 'get',
    params: query
  })
}

/**
 * 新增保养计划班次
 * @param data
 */
export function addMaintainPlanClass(data) {
  return request({
    url: 'equipment/MaintainPlanClass',
    method: 'post',
    data: data
  })
}
/**
 * 修改保养计划班次
 * @param data
 */
export function updateMaintainPlanClass(data) {
  return request({
    url: 'equipment/MaintainPlanClass',
    method: 'PUT',
    data: data
  })
}
/**
 * 获取保养计划班次详情
 * @param {Id}
 */
export function getMaintainPlanClass(id) {
  return request({
    url: 'equipment/MaintainPlanClass/' + id,
    method: 'get'
  })
}

/**
 * 删除保养计划班次
 * @param {主键} pid
 */
export function delMaintainPlanClass(pid) {
  return request({
    url: 'equipment/MaintainPlanClass/delete/' + pid,
    method: 'delete'
  })
}

/**
 * 保养计划班次字典分页查询
 * @param {查询条件} data
 */
export function dictMaintainPlanClass(query) {
  return request({
    url: 'equipment/MaintainPlanClass/dict',
    method: 'get',
    params: query
  })
}
