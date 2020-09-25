
//$(document).ready(function () {
//    toastr.options = {
//        "newestOnTop": false,
//        "positionClass": "toast-bottom-center",
//        "preventDuplicates": false,
//        "showDuration": "300000",
//        "timeOut": "100000"
//    };
//});  

function fcAlertBlue(vjsMsg) {

    $("#alert_blue").removeClass('d-none');
    document.querySelector("#txt_alert_blue").innerHTML = vjsMsg;

    setTimeout(function () {
        $("#alert_blue").addClass('d-none');
        window.location.reload();
    }, 1800);
}


function fcAlertRed(vjsMsg) {

    $("#alert_red").removeClass('d-none');
    document.querySelector("#txt_alert_red").innerHTML = vjsMsg;

    setTimeout(function () {
        $("#alert_red").addClass('d-none');
        window.location.reload();
    }, 1800);
}

function fcAlertRedNonReload(vjsMsg) {

    $("#alert_red").removeClass('d-none');
    document.querySelector("#txt_alert_red").innerHTML = vjsMsg;

    setTimeout(function () {
        $("#alert_red").addClass('d-none');
    }, 1800);
}
