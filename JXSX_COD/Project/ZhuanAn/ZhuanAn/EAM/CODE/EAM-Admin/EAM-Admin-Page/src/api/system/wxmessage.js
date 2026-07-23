import request from '@/utils/request'
import QS from 'qs'

/**
* 企业微信发送记录表分页查询
* @param {查询条件} data
*/
export function listWxMessage(query) {
  return request({
    url: 'system/WxMessage/list',
    method: 'get',
    params: query,
    paramsSerializer: function (params) {
      return QS.stringify(params, { indices: false })
    }
  })
}

/**
* 新增企业微信发送记录表
* @param data
*/
export function addWxMessage(data) {
  return request({
    url: 'system/WxMessage',
    method: 'post',
    data: data,
  })
}
/**
* 修改企业微信发送记录表
* @param data
*/
export function updateWxMessage(data) {
  return request({
    url: 'system/WxMessage',
    method: 'PUT',
    data: data,
  })
}
/**
* 获取企业微信发送记录表详情
* @param {Id}
*/
export function getWxMessage(id) {
  return request({
    url: 'system/WxMessage/' + id,
    method: 'get'
  })
}

/**
* 删除企业微信发送记录表
* @param {主键} pid
*/
export function delWxMessage(pid) {
  return request({
    url: 'system/WxMessage/delete/' + pid,
    method: 'delete'
  })
}
