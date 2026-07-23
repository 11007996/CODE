import request from '@/utils/request'

/**
* 表单模板分页查询
* @param {查询条件} data
*/
export function listFormTemplate(query) {
  return request({
    url: 'workflow/FormTemplate/list',
    method: 'get',
    params: query,
  })
}

/**
* 表单字典(所有)
* @param {查询条件} data
*/
export function dictFormTemplate(query) {
  return request({
    url: 'workflow/FormTemplate/dict',
    method: 'get',
    params: query,
  })
}


/**
* 新增表单模板
* @param data
*/
export function addFormTemplate(data) {
  return request({
    url: 'workflow/FormTemplate',
    method: 'post',
    data: data,
  })
}
/**
* 修改表单模板
* @param data
*/
export function updateFormTemplate(data) {
  return request({
    url: 'workflow/FormTemplate',
    method: 'PUT',
    data: data,
  })
}
/**
* 获取表单模板详情
* @param {Id}
*/
export function getFormTemplate(id) {
  return request({
    url: 'workflow/FormTemplate/' + id,
    method: 'get'
  })
}

/**
* 删除表单模板
* @param {主键} pid
*/
export function delFormTemplate(pid) {
  return request({
    url: 'workflow/FormTemplate/delete/' + pid,
    method: 'delete'
  })
}
