
//$(document).ready(function () {
//    toastr.options = {
//        "newestOnTop": false,
//        "positionClass": "toast-bottom-center",
//        "preventDuplicates": false,
//        "showDuration": "300000",
//        "timeOut": "100000"
//    };
//});  


function fcAlertSuccessAdd(vjsAlertId, vjsLatestCode) {

    $(vjsAlertId).removeClass('d-none');
    document.querySelector("#data_code_alert_add").innerHTML = vjsLatestCode;

    setTimeout(function () {
        $(vjsAlertId).addClass('d-none');

    }, 1500);
}

function fcAlertSuccessEdit(vjsAlertId) {

    $(vjsAlertId).removeClass('d-none');

    setTimeout(function () {
        $(vjsAlertId).addClass('d-none');

    }, 1500);
}

function fcAlertSuccessDelete(vjsAlertId, vjsLatestCode) {

    $(vjsAlertId).removeClass('d-none');
    document.querySelector("#data_code_alert_del").innerHTML = vjsLatestCode;

    setTimeout(function () {
        $(vjsAlertId).addClass('d-none');

    }, 2000);
}