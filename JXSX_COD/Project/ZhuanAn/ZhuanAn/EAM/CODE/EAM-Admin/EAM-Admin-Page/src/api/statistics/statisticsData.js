import request from '@/utils/request'

/**
* 统计数据分页查询
* @param {查询条件} data
*/
export function listStatStatisticsData(query) {
  return request({
    url: 'statistics/StatStatisticsData/list',
    method: 'get',
    params: query,
  })
}

/**
* 新增统计数据
* @param data
*/
export function addStatStatisticsData(data) {
  return request({
    url: 'statistics/StatStatisticsData',
    method: 'post',
    data: data,
  })
}
/**
* 修改统计数据
* @param data
*/
export function updateStatStatisticsData(data) {
  return request({
    url: 'statistics/StatStatisticsData',
    method: 'PUT',
    data: data,
  })
}
/**
* 获取统计数据详情
* @param {Id}
*/
export function getStatStatisticsData(id) {
  return request({
    url: 'statistics/StatStatisticsData/' + id,
    method: 'get'
  })
}

/**
* 删除统计数据
* @param {主键} pid
*/
export function delStatStatisticsData(pid) {
  return request({
    url: 'statistics/StatStatisticsData/delete/' + pid,
    method: 'delete'
  })
}


/**
* 最新统计数据
* @param {查询条件} data
*/
export function newestStatStatisticsData(query) {
  return request({
    url: 'statistics/StatStatisticsData/newest',
    method: 'get',
    params: query,
  })
}



/**
* 最新统计数据
* @param {查询条件} data
*/
export function daysStatStatisticsData(query) {
  return request({
    url: 'statistics/StatStatisticsData/days',
    method: 'get',
    params: query,
  })
}
