import request from '@/utils/request'

// 查询厂区管理列表
export function listSite(query) {
  return request({
    url: '/product/site/list',
    method: 'get',
    params: query
  })
}

// 查询厂区管理详细
export function getSite(siteId) {
  return request({
    url: '/product/site/' + siteId,
    method: 'get'
  })
}

// 新增厂区管理
export function addSite(data) {
  return request({
    url: '/product/site',
    method: 'post',
    data: data
  })
}

// 修改厂区管理
export function updateSite(data) {
  return request({
    url: '/product/site',
    method: 'put',
    data: data
  })
}

export function changeSiteStatus(siteId, status){
  const data = {
    siteId,
    status
  }
  return request({
    url: '/product/site/changeStatus',
    method: 'put',
    data: data
  })
}

// 删除厂区管理
export function delSite(siteId) {
  return request({
    url: '/product/site/' + siteId,
    method: 'delete'
  })
}
