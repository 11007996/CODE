import request from '@/utils/request'

// 查询工站列表
export function listTerminal(query) {
  return request({
    url: '/product/terminal/list',
    method: 'get',
    params: query
  })
}

// 查询工站详细
export function getTerminal(terminalId) {
  return request({
    url: '/product/terminal/' + terminalId,
    method: 'get'
  })
}

// 新增工站
export function addTerminal(data) {
  return request({
    url: '/product/terminal',
    method: 'post',
    data: data
  })
}

// 修改工站
export function updateTerminal(data) {
  return request({
    url: '/product/terminal',
    method: 'put',
    data: data
  })
}

// 删除工站
export function delTerminal(terminalId) {
  return request({
    url: '/product/mac/' + terminalId,
    method: 'delete'
  })
}



export function getMesLineOptions(){
  return request({
    url: '/product/terminal/getMesLineOptions/',
    method: 'get'
  })
}
export function getMesStageOptions(){
  return request({
    url: '/product/terminal/getMesStageOptions/',
    method: 'get'
  })
}
export function getMesProcessOptionsByStageId(stageId){
  return request({
    url: '/product/terminal/getMesProcessOptionsByStageId/' + stageId,
    method: 'get'
  })
}


/** 在表 MesTerminal 查找数据 */
//
export function getLineInTerminal(){
  return request({
    url: '/product/terminal/getLineInTerminal/',
    method: 'get'
  })
}
//根据Line查询stage
export function getStageInTerminal(lineId){
  return request({
    url: '/product/terminal/getStageInTerminal/'+ lineId,
    method: 'get'
  })
}
//process
export function getProcessInTerminal(lineId,stageId){
  const data= {
    lineId,
    stageId
  }
  return request({
    url: '/product/terminal/getProcessInTerminal/',
    method: 'post',
    data: data
  })
}
