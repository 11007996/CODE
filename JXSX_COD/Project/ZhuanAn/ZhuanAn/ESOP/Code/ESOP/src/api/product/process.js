import request from '@/utils/request'

// 查询站位名列表
export function listProcess(query) {
  return request({
    url: '/product/process/list',
    method: 'get',
    params: query
  })
}

// 查询站位名详细
export function getProcess(processId) {
  return request({
    url: '/product/process/' + processId,
    method: 'get'
  })
}

// 新增站位名
export function addProcess(data) {
  return request({
    url: '/product/process',
    method: 'post',
    data: data
  })
}

// 修改站位名
export function updateProcess(data) {
  return request({
    url: '/product/process',
    method: 'put',
    data: data
  })
}

export  function changeProcessStatus(processId, status){
  const data = {
    processId,
    status
  }
  return request({
    url: '/product/process/changeStatus',
    method: 'put',
    data: data
  })
}

// 删除站位名
export function delProcess(processId) {
  return request({
    url: '/product/process/' + processId,
    method: 'delete'
  })
}

export function stageList(){
  return request({
    url: '/product/process/stageList',
    method: 'get'
  })
}

