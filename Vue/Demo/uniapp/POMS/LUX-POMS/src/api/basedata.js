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

//修改线别
export function ModifyLine(param)
{
    return http({
    url: '/api/BasicData/ModifyLine',
    method: 'PUT',
    data: param
  });
}

//删除线别
export function DeleteLine(param)
{
    return http({
    url: '/api/BasicData/DeleteLine?lineid='+param,
    method: 'DELETE',
    data: ""
  });
}

//获取所有线别
export function GetLineAll(param)
{
    let apiurl = '/api/BasicData/GetLineAll'
    if(param.linename != undefined)
    {
      apiurl = apiurl + '?lineName=' + param.linename
      if(param.enabled != undefined)
      {
        apiurl = apiurl + '&lineEnabled=' + param.enabled
      }
    }
    else
    {
       if(param.enabled != undefined)
       {
        apiurl = apiurl + '?lineEnabled=' + param.enabled
       }
    }

    return http({
    url: apiurl,
    method: 'GET',
    data: ""
  });
}

//获取所有部门
export function GetDepartAll(param)
{
    return http({
    url: '/api/BasicData/GetDepartAll?enabled='+param,
    method: 'GET'
  });
}

//修改部门资料
export function ModifyDepartById(param)
{
    return http({
    url: '/api/BasicData/ModifyDepartById',
    method: 'PUT',
    data:param
  });
}


//新增部门资料
export function AddDepart(param)
{
    return http({
    url: '/api/BasicData/AddDepart',
    method: 'POST',
    data:param
  });
}


//删除部门资料
export function DateleDepart(param)
{
    return http({
    url: '/api/BasicData/DateleDepart?departid='+param,
    method: 'DELETE'
  });
}