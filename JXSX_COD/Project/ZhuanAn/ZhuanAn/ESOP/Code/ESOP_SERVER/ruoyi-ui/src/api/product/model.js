import request from '@/utils/request'

// 查询机种信息列表
export function listModel(query) {
  return request({
    url: '/product/model/list',
    method: 'get',
    params: query
  })
}

// 查询机种信息详细
export function getModel(modelId) {
  return request({
    url: '/product/model/' + modelId,
    method: 'get'
  })
}

// 新增机种信息
export function addModel(data) {
  return request({
    url: '/product/model',
    method: 'post',
    data: data
  })
}

// 修改机种信息
export function updateModel(data) {
  return request({
    url: '/product/model',
    method: 'put',
    data: data
  })
}
// 机种状态修改
export function changeModelStatus(modelId, status) {
  const data = {
    modelId,
    status
  }
  return request({
    url: '/product/model/changeStatus',
    method: 'put',
    data: data
  })
}

// 删除机种信息
export function delModel(modelId) {
  return request({
    url: '/product/model/' + modelId,
    method: 'delete'
  })
}
