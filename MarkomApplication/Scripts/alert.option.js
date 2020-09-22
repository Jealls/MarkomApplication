
//$(document).ready(function () {
//    toastr.options = {
//        "newestOnTop": false,
//        "positionClass": "toast-bottom-center",
//        "preventDuplicates": false,
//        "showDuration": "300000",
//        "timeOut": "100000"
//    };
//});  

function fcAlertDataExist(vjsAlertId, vjsMsg) {

    $(vjsAlertId).removeClass('d-none');
    document.querySelector("#warn_exist_from").innerHTML = vjsMsg;

    setTimeout(function () {
        $(vjsAlertId).addClass('d-none');

    }, 1500);
}



//------------------------COMPANY---------------------------------------------
function fcAlertSuccessAdd(vjsAlertId, vjsLatestCode) {

    $(vjsAlertId).removeClass('d-none');
    document.querySelector("#add_from").innerHTML = "New company has been add with code " + vjsLatestCode.bold() +" !";

    setTimeout(function () {
        $(vjsAlertId).addClass('d-none');

    }, 1500);
}

function fcAlertSuccessEdit(vjsAlertId) {

    $(vjsAlertId).removeClass('d-none');
    document.querySelector("#update_from").innerHTML = "company";

    setTimeout(function () {
        $(vjsAlertId).addClass('d-none');

    }, 1500);
}

function fcAlertSuccessDelete(vjsAlertId, vjsLatestCode) {

    $(vjsAlertId).removeClass('d-none');
    document.querySelector("#delete_from").innerHTML = " company with code " + vjsLatestCode.bold() + " has been deleted !";

    setTimeout(function () {
        $(vjsAlertId).addClass('d-none');

    }, 2000);
}




//------------------------EMPLOYEE---------------------------------------------

function fcAlertSuccessAddEmp(vjsAlertId, vjsLatestCode) {

    $(vjsAlertId).removeClass('d-none');
    document.querySelector("#add_from").innerHTML = "New employee has been add with with employee ID number " + vjsLatestCode.bold() + " !";

    setTimeout(function () {
        $(vjsAlertId).addClass('d-none');

    }, 1500);
} 

function fcAlertSuccessEditEmp(vjsAlertId) {

    $(vjsAlertId).removeClass('d-none');
    document.querySelector("#update_from").innerHTML = "employee";

    setTimeout(function () {
        $(vjsAlertId).addClass('d-none');

    }, 1500);
}

function fcAlertSuccessDelEmp(vjsAlertId, vjsLatestCode) {

    $(vjsAlertId).removeClass('d-none');
    document.querySelector("#delete_from").innerHTML = " employee with Employee ID Number " + vjsLatestCode.bold() + " has been deleted !";

    setTimeout(function () {
        $(vjsAlertId).addClass('d-none');

    }, 2000);
}




//------------------------ROLE---------------------------------------------
function fcAlertSuccessAddRole(vjsAlertId, vjsLatestCode) {

    $(vjsAlertId).removeClass('d-none');

    document.querySelector("#add_from").innerHTML = "New role has been add with code " + vjsLatestCode.bold() + " !";
    setTimeout(function () {
        $(vjsAlertId).addClass('d-none');

    }, 1500);
}

function fcAlertSuccessEditRole(vjsAlertId) {

    $(vjsAlertId).removeClass('d-none');
    document.querySelector("#update_from").innerHTML = "role";

    setTimeout(function () {
        $(vjsAlertId).addClass('d-none');

    }, 1500);
}

function fcAlertSuccessDelRole(vjsAlertId, vjsLatestCode) {

    $(vjsAlertId).removeClass('d-none');
    document.querySelector("#data_code_alert_del").innerHTML = vjsLatestCode;
    document.querySelector("#delete_from").innerHTML = " role with code " + vjsLatestCode.bold() + " has been deleted !";

    setTimeout(function () {
        $(vjsAlertId).addClass('d-none');

    }, 2000);
}

//------------------------EVENT---------------------------------------------
function fcAlertSuccessAddEvent(vjsAlertId, vjsLatestCode) {

    $(vjsAlertId).removeClass('d-none');

    document.querySelector("#add_from").innerHTML = "Transaction Event request has been add with code " + vjsLatestCode.bold() + " !";
    setTimeout(function () {
        $(vjsAlertId).addClass('d-none');

    }, 1500);
}

function fcAlertSuccessEditEvent(vjsAlertId, vjsLatestCode) {

    $(vjsAlertId).removeClass('d-none');

    document.querySelector("#update_from_t").innerHTML = "Transaction Event request with code " + vjsLatestCode.bold() + " has been update !";
    setTimeout(function () {
        $(vjsAlertId).addClass('d-none');

    }, 1500);
}
