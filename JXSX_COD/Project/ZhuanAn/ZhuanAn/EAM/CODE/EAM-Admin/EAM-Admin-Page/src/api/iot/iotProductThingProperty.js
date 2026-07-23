import request from '@/utils/request'

/**
 * 产品物模型属性分页查询
 * @param {查询条件} data
 */
export function listIotProductThingProperty(query) {
  return request({
    url: 'iot/IotProductThingProperty/list',
    method: 'get',
    params: query
  })
}

/**
 * 新增产品物模型属性
 * @param data
 */
export function addIotProductThingProperty(data) {
  return request({
    url: 'iot/IotProductThingProperty',
    method: 'post',
    data: data
  })
}
/**
 * 修改产品物模型属性
 * @param data
 */
export function updateIotProductThingProperty(data) {
  return request({
    url: 'iot/IotProductThingProperty',
    method: 'PUT',
    data: data
  })
}
/**
 * 获取产品物模型属性详情
 * @param {Id}
 */
export function getIotProductThingProperty(id) {
  return request({
    url: 'iot/IotProductThingProperty/' + id,
    method: 'get'
  })
}

/**
 * 删除产品物模型属性
 * @param {主键} pid
 */
export function delIotProductThingProperty(pid) {
  return request({
    url: 'iot/IotProductThingProperty/delete/' + pid,
    method: 'delete'
  })
}

/**
 * 新增属性扩展描述
 * @param data
 */
export function addIotProductThingPropertyExtend(data) {
  return request({
    url: 'iot/IotProductThingProperty/extend',
    method: 'post',
    data: data
  })
}
/**
 * 修改属性扩展描述
 * @param data
 */
export function updateIotProductThingPropertyExtend(data) {
  return request({
    url: 'iot/IotProductThingProperty/extend',
    method: 'PUT',
    data: data
  })
}
/**
 * 获取属性扩展描述详情
 * @param {Id}
 */
export function getIotProductThingPropertyExtend(id) {
  return request({
    url: 'iot/IotProductThingProperty/extend/' + id,
    method: 'get'
  })
}

/**
 * 删除属性扩展描述
 * @param {主键} pid
 */
export function delIotProductThingPropertyExtend(pid) {
  return request({
    url: 'iot/IotProductThingProperty/extend/delete/' + pid,
    method: 'delete'
  })
}
