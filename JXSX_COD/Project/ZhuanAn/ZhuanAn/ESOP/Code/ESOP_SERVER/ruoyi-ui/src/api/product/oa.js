import request from '@/utils/request'

// 查询oa签核列表
export function listOa(query) {
  return request({
    url: '/product/oa/list',
    method: 'get',
    params: query
  })
}

// 查询oa签核详细
export function getOa(oaId) {
  return request({
    url: '/product/oa/' + oaId,
    method: 'get'
  })
}

// // 新增oa签核
// export function addOa(data) {
//   return request({
//     url: '/product/oa',
//     method: 'post',
//     data: data
//   })
// }

// // 修改oa签核
// export function updateOa(data) {
//   return request({
//     url: '/product/oa',
//     method: 'put',
//     data: data
//   })
// }

// // 删除oa签核
// export function delOa(oaId) {
//   return request({
//     url: '/product/oa/' + oaId,
//     method: 'delete'
//   })
// }


export function getMesModelOptions(){
  return request({
    url: '/product/oa/getMesModelList',
    method: 'get'
  })
}

export function uploadFile(data) {
  return request({
    url: '/product/oa/upload',
    method: 'post',
    data: data
  })
}




export function addOaEsopInfo(data){
  return request({
    url: '/product/oa/addOaEsopInfo',
    method: 'post',
    data: data
  })
}




// // 查询oa签核详细
// export function sendOa(oaId) {
//   return request({
//     url: '/product/oa/sendOa/' + oaId,
//     method: 'get'
//   })
// }
// 查询oa签核详细
export function sendOa(luxId) {
  return request({
    url: '/product/oa/sendLuxLink/' + luxId,
    method: 'get'
  })
}

// 查获取有OA签核权限的人员信息
export function getOACountersignUserList() {
  return request({
    url: '/product/oa/selectOACountersignUserList',
    method: 'get'
  })
}