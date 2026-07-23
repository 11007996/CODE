import request from '@/utils/request'

/*******************治具关联文件*************** */
/**
 * 治具文件关联表分页查询
 * @param {查询条件} data
 */
export function listFixtureFile(query) {
  return request({
    url: 'fixture/fixtureFile/list',
    method: 'get',
    params: query
  })
}

/**
 * 新增治具文件关联
 * @param data
 */
export function bacthAddFixtureFile(data) {
  return request({
    url: 'fixture/fixtureFile/batch',
    method: 'post',
    data: data
  })
}

/**
 * 删除治具文件关联
 * @param {主键} pid
 */
export function delFixtureFile(data) {
  return request({
    url: 'fixture/fixtureFile/delete',
    method: 'delete',
    data: data
  })
}
