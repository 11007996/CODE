import request from '@/utils/request'

// 查询sop列表
export function listSop(query) {
  return request({
    url: '/product/sop/list',
    method: 'get',
    params: query
  })
}

// 查询sop详细
export function getSop(sopId) {
  return request({
    url: '/product/sop/' + sopId,
    method: 'get'
  })
}

// 新增sop
export function addSop(data) {
  return request({
    url: '/product/sop',
    method: 'post',
    data: data
  })
}

// 修改sop
export function updateSop(data) {
  return request({
    url: '/product/sop',
    method: 'put',
    data: data
  })
}

// 删除sop
export function delSop(sopId) {
  return request({
    url: '/product/sop/' + sopId,
    method: 'delete'
  })
}
