import request from '@/utils/request'

// 查询区段管理列表
export function listStage(query) {
  return request({
    url: '/product/stage/list',
    method: 'get',
    params: query
  })
}

// 查询区段管理详细
export function getStage(stageId) {
  return request({
    url: '/product/stage/' + stageId,
    method: 'get'
  })
}

// 新增区段管理
export function addStage(data) {
  return request({
    url: '/product/stage',
    method: 'post',
    data: data
  })
}

// 修改区段管理
export function updateStage(data) {
  return request({
    url: '/product/stage',
    method: 'put',
    data: data
  })
}

// 修改stage状态
export function changeStageStatus(stageId, status) {
  const data = {
    stageId,
    status
  }
  return request({
    url: '/product/stage/changeStatus',
    method: 'put',
    data: data
  })
}


// 删除区段管理
export function delStage(stageId) {
  return request({
    url: '/product/stage/' + stageId,
    method: 'delete'
  })
}
