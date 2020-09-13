
function matchEmail(email) {
    const reg = /^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/;
    return reg.test(email);
}

function validationEmail()
{
    var vjsEmail = document.getElementById("email").value;
    debugger;
    if (matchEmail(vjsEmail)) {
        $("#email").removeClass("invalid");
    } else {
        $("#email").addClass("invalid");
    }
}


function matchPhone(phone) {
    const reg = /^[+]*[(]{0,1}[0-9]{1,4}[)]{0,1}[-\s\./0-9]*$/;
    return reg.test(phone);
}

function validationPhone() {
    var vjsPhone = document.getElementById("phone").value;

    if (matchPhone(vjsPhone)) {
        $("#phone").removeClass("invalid");
    } else {
        $("#phone").addClass("invalid");
    }
}

function validationName(vjsName) {

    if (vjsName == "") {
        $("#name").addClass("invalid");
    } else {
        $("#name").removeClass("invalid");
    }
}