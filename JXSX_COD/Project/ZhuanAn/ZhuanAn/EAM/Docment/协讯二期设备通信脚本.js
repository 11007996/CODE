var EVENT_POST_METHOD = 'event.post'; //属性上报。
var PROP_POST_METHOD = 'property.post'; //属性上报。
var PROP_POST_TOPIC = 'property/post'; //属性上报。
/*
调用时机：非MQTT协议的数据上传(自定义协议)的设备上传数据时，会调用此方法
rawDataToProtocol：原始数据转为指定格式数据
示例数据：
设备上报属性数据：
传入参数：
    字节数组
输出结果：
    json对象，最少要返回一个method属性，说明此数据的作用，如 { "method":"property.post"}
*/
function rawDataToProtocol(bytes) {
    var uint8Array = new Uint8Array(bytes.length);
    for (var i = 0; i < bytes.length; i++) {
        uint8Array[i] = bytes[i] & 0xff;
    }
    var dataView = new DataView(uint8Array.buffer, 0);

    //解析数据到jsonMap
    var jsonMap = {};

    //检查数据是否合法
    let hexStr = byteArrayToHexString(bytes);
    if(hexStr.startsWith('ED-ED')&&hexStr.endsWith('EC-EC')&&bytes.length==17){
       jsonMap['equipmentCode'] = dataView.getUint16(2);
       jsonMap['operateCode'] = dataView.getUint8(4);
       jsonMap['runState'] = dataView.getUint8(5);
       jsonMap['lineCode'] = dataView.getUint8(6);
       jsonMap['productCount'] = dataView.getUint16(8);
       jsonMap['defectCount'] = dataView.getUint16(10);
       jsonMap['warnState'] = dataView.getUint8(12);
       jsonMap['wartCode'] = dataView.getUint16(13);
    }

    if(jsonMap['operateCode']==0){ 
      jsonMap['method'] = PROP_POST_METHOD
      
    }else if(jsonMap['operateCode']==1){
      var eventMap = {};
      eventMap['method'] = EVENT_POST_METHOD;
      eventMap['eventIdentifier'] ='maintainCheck';//保养检查
      eventMap['eventType'] ='info';
      eventMap['params'] = jsonMap;
      return eventMap;
    }

    return jsonMap;
}
/*
调用时机：非MQTT协议时，需要返回数据给设备，数据为非标准的json内容。输入参数会包含一个method属性来区分，当前数据的来源
protocolToRawData：协议对象转字节数组
示例数据：设备上报的返回结果
传入参数：
    标准josn对象
输出结果：
    字节数组
*/
function protocolToRawData(json) {
    var method = json['method'];
    var payloadArray = [];
	//根据不同method,处理json参数
    if (method == PROP_POST_METHOD) //属性设置。
    {

    }else if (method == EVENT_POST_METHOD) //事件。
    {
        let eventIdentifier =  json['eventIdentifier'];
        let equipmentCode = json['params']['equipmentCode']
        let operateCode = json['params']['operateCode']
        let maintainStatus = json['maintainStatus']?10:1; //10：表示保养ok ；1表示未保养
        if(eventIdentifier == "maintainCheck" ){//保养
           payloadArray = payloadArray.concat(hexStringToByteArray('EE-EE'));
           payloadArray = payloadArray.concat(buffer_int16(equipmentCode)); //设备编码，2字节
           payloadArray = payloadArray.concat(buffer_uint8(operateCode)) //操作指令，1字节
           payloadArray = payloadArray.concat(buffer_uint8(maintainStatus));//处理结果,1字节
           payloadArray = payloadArray.concat(buffer_uint8(0));//备用
           payloadArray = payloadArray.concat(hexStringToByteArray('FF-FF'));
        }
    }
    return payloadArray;
}

/*
  调用时机：MQTT协议，自定义topic
  注意：topic最后需要有字符串'?parser=default',才会走此方法
  transformPayload:转为标准格式
  示例数据
  自定义Topic内容转为平台数据格式
  输入参数：
     topic:主题
     bytes:内容
  输出参数：
	 平台格式
 */
 
function transformPayload(topic, bytes) {
    var uint8Array = new Uint8Array(bytes.length);
    for (var i = 0; i < bytes.length; i++) {
        uint8Array[i] = bytes[i] & 0xff;
    }
    var dataView = new DataView(uint8Array.buffer, 0);

    //解析数据到jsonMap
    var jsonMap = {};
    if(topic.includes(PROP_POST_TOPIC)){
      //检查数据是否合法
      let hexStr = byteArrayToHexString(bytes);
      if(hexStr.startsWith('ED-ED')&&hexStr.endsWith('EC-EC')&&bytes.length==17){
         jsonMap['equipmentCode'] = dataView.getUint16(2);
         jsonMap['operateCode'] = dataView.getUint8(4);
         jsonMap['runState'] = dataView.getUint8(5);
         jsonMap['lineCode'] = dataView.getUint8(6);
         jsonMap['productCount'] = dataView.getUint16(8);
         jsonMap['defectCount'] = dataView.getUint16(10);
         jsonMap['warnState'] = dataView.getUint8(12);
         jsonMap['wartCode'] = dataView.getUint16(13);
      }
    }

    return jsonMap;
}


//以下是部分辅助函数。
function buffer_uint8(value) {
    var uint8Array = new Uint8Array(1);
    var dv = new DataView(uint8Array.buffer, 0);
    dv.setUint8(0, value);
    return [].slice.call(uint8Array);
}
function buffer_int16(value) {
    var uint8Array = new Uint8Array(2);
    var dv = new DataView(uint8Array.buffer, 0);
    dv.setInt16(0, value);
    return [].slice.call(uint8Array);
}
function buffer_int32(value) {
    var uint8Array = new Uint8Array(4);
    var dv = new DataView(uint8Array.buffer, 0);
    dv.setInt32(0, value);
    return [].slice.call(uint8Array);
}
function buffer_float32(value) {
    var uint8Array = new Uint8Array(4);
    var dv = new DataView(uint8Array.buffer, 0);
    dv.setFloat32(0, value);
    return [].slice.call(uint8Array);
}

/*
字节数组转16进制字符串，如:ED-ED-00-EC-EC
**/
function byteArrayToHexString(bytes) {
  return bytes.map(b => b.toString(16).padStart(2, '0')).join('-').toUpperCase();
}

/**
16进制字符串转字节数组
*/
function hexStringToByteArray(hexString) {
    // 移除空格或-（可选）
    hexString = hexString.replace(/\s+/g, '');
    hexString = hexString.replace('-', '');
    // 检查是否为偶数长度
    if (hexString.length % 2 !== 0) {
        throw new Error("Invalid hex string length");
    }

    const byteArray = [];
    for (let i = 0; i < hexString.length; i += 2) {
        const byte = parseInt(hexString.substr(i, 2), 16);
        if (isNaN(byte)) {
            throw new Error(`Invalid hex character at index ${i}`);
        }
        byteArray.push(byte);
    }
    return byteArray;
}
