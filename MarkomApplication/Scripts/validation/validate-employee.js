
function matchEmail(email) {
    const reg = /^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/;
    return reg.test(email);
}


function validationEmail() {
    var vjsEmail = document.getElementById("email").value;
    debugger;
    if (matchEmail(vjsEmail)) {
        $("#email").removeClass("invalid");
    } else {
        $("#email").addClass("invalid");
    }
}

function matchCode(code) {
    const reg = /^[0-9]{1,2}[.][0-9]{1,2}[.][0-9]{1,2}[.][0-9]{1,2}?$/;
    return reg.test(code);
}

function validationCode() {
    var vjsCode = document.getElementById("code").value;

    if (matchCode(vjsCode)) {
        $("#code").removeClass("invalid");
    } else {
        $("#code").addClass("invalid");
    }
}

function validationEmpNumber(vjsEmpNumber) {

    if (vjsEmpNumber == "" || !matchCode(vjsEmpNumber)) {
        $("#code").addClass("invalid");
    } else {
        $("#code").removeClass("invalid");

    }
}

function validationCompanyName(vjsCompanyId) {

    if (isNaN(vjsCompanyId)) {
        $("#companyName").addClass("invalid");
    } else {
        $("#companyName").removeClass("invalid");
    }
}


function validationFirstName(FirstName) {
    debugger;
    if (FirstName == "") {
        $("#firstName").addClass("invalid");
    } else {
        $("#firstName").removeClass("invalid");
    }
}
function validationName() {
    var FirstName = document.getElementById("firstName").value;
    if (FirstName == "") {
        $("#firstName").addClass("invalid");
    } else {
        $("#firstName").removeClass("invalid");
    }
}


function cekValidationEmployee() {

    var code = $("#employeeNumber").hasClass("invalid");
    var firstName = $("#firstName").hasClass("invalid");
    var companyName = $("#companyName").hasClass("invalid");
    var email = $("#email").hasClass("invalid");

    var result = false;

    if (!code && !companyName && !firstName && !email) {
        result = true;
    }

    return result;
}