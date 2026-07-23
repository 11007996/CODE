import request from '@/utils/request'

// 查询sop配置列表
export function listSopConfig(query) {
  return request({
    url: '/product/sopConfig/list',
    method: 'get',
    params: query
  })
}

// 查询sop配置详细
export function getSopConfig(id) {
  return request({
    url: '/product/sopConfig/' + id,
    method: 'get'
  })
}

// 新增sop配置
export function addSopConfig(data) {
  return request({
    url: '/product/sopConfig',
    method: 'post',
    data: data
  })
}

// 修改sop配置
export function updateSopConfig(data) {
  return request({
    url: '/product/sopConfig',
    method: 'put',
    data: data
  })
}

// 删除sop配置
export function delSopConfig(id) {
  return request({
    url: '/product/sopConfig/' + id,
    method: 'delete'
  })
}


export function getSopConfigHt(modelId,lineId,stageId,processId){
  const data = {
    modelId,lineId,stageId,processId
  }
  return request({
    url: '/product/sopConfig/getSopConfigHt',
    method: 'put',
    data: data
  })
}




export function viewSopInfo(filePath){
  const data = {
    filePath
  }
  return request({
    url: '/product/sopConfig/view/',
    method: 'post',
    data: data
  })
}
