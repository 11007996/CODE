import http from '../utils/http'

//通过线别ID获取线别详情
export function GetLineDetailsById(param)
{
    return http({
    url: '/api/BasicData/GetLineDetailsById',
    method: 'GET',
    data: param
  });
}

//新增线别
export function AddLine(param)
{
    return http({
    url: '/api/BasicData/AddLine',
    method: 'POST',
    data: param
  });
}

//获取班别用于线别设置
export function GetShiftOverview(param)
{
    return http({
    url: '/api/BasicData/GetShiftOverview',
    method: 'GET',
    data: param
  });
}