
//$(document).ready(function () {
//    toastr.options = {
//        "newestOnTop": false,
//        "positionClass": "toast-bottom-center",
//        "preventDuplicates": false,
//        "showDuration": "300000",
//        "timeOut": "100000"
//    };
//});  


function fcAlertSuccessAdd(vjsAlertId) {

    $(vjsAlertId).removeClass('d-none');

    setTimeout(function () {
        $(vjsAlertId).addClass('d-none');

    }, 5000);
}