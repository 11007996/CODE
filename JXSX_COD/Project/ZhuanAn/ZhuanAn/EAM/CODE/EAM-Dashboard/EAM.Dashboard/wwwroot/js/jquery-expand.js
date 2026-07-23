//表单数据序列化为json对象
$.fn.serializeJSON = function () {
    let json = {};
    let formData = this.serializeArray();

    $.each(formData, function (i, field) {
        json[field.name] = field.value;
    });

    return json;
};