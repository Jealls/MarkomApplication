
function validationName(name) {
    if (name == "") {
        $("#name").addClass("invalid");
    } else {
        $("#name").removeClass("invalid");
    }
}

function validationController(con) {
    if (con == "") {
        $("#controller").addClass("invalid");
    } else {
        $("#controller").removeClass("invalid");
    }
}
function cekValidationMenu() {

    var name = $("#name").hasClass("invalid");
    var con = $("#controller").hasClass("invalid");

    var result = false;

    if (!name && !con) {
        result = true;
    }

    return result;
}