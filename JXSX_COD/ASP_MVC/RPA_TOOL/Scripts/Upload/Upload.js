
//const fileInput = document.getElementById('uploadFile');
//const nameSpan = document.getElementById('upload-txt');
//fileInput.addEventListener('change', function () {
//    if (this.files.length > 0) {
//        nameSpan.value = this.files[0].name;
//    } else {
//        nameSpan.innerText = "未选择文件";
//    }
//})

$(function () {
    $("#uploadFile").change(function () {
        let nameSpan = document.getElementById('upload-txt');
        if (this.files.length > 0) {
            nameSpan.value = this.files[0].name;
        } else {
            nameSpan.innerText = "未选择文件";
        }
    })
})
