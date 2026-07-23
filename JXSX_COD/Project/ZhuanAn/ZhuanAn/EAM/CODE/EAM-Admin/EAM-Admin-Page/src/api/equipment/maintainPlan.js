import request from '@/utils/request'

/**
 * 保养计划分页查询
 * @param {查询条件} data
 */
export function listMaintainPlan(query) {
  return request({
    url: 'equipment/MaintainPlan/list',
    method: 'get',
    params: query
  })
}

/**
 * 新增保养计划
 * @param data
 */
export function addMaintainPlan(data) {
  return request({
    url: 'equipment/MaintainPlan',
    method: 'post',
    data: data
  })
}
/**
 * 修改保养计划
 * @param data
 */
export function updateMaintainPlan(data) {
  return request({
    url: 'equipment/MaintainPlan',
    method: 'PUT',
    data: data
  })
}
/**
 * 获取保养计划详情
 * @param {Id}
 */
export function getMaintainPlan(id) {
  return request({
    url: 'equipment/MaintainPlan/' + id,
    method: 'get'
  })
}

/**
 * 删除保养计划
 * @param {主键} pid
 */
export function delMaintainPlan(pid) {
  return request({
    url: 'equipment/MaintainPlan/delete/' + pid,
    method: 'delete'
  })
}

/**
 * 未配置保养计划的设备分页查询
 * @param {查询条件} data
 */
export function listExcludeEquipment(query) {
  return request({
    url: 'equipment/MaintainPlan/excludeEquipment',
    method: 'get',
    params: query
  })
}
